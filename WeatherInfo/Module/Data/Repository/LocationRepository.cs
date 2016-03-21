using Dapper;
using Data.Model;
using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;

namespace Data.Repository
{
    public class LocationRepository
    {
        protected Settings _settings{ get; private set; }

        public LocationRepository(Settings settings)
        {
            _settings = settings;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_settings.GetWeatherInfoConnectionString());
        }

        public void AddUserLocation(UserLocationDataModel dataModel)
        {
            if (dataModel.StateCode.Length > 2) dataModel.StateCode = string.Empty;
            using (var db = GetConnection())
            {
                db.Execute(
                    @"
                declare @MaxUserSortOrder tinyint = isnull((select max(SortOrder) from dbo.UserLocations where UserId = @UserId), 0)
                insert into dbo.UserLocations(UserId, Latitude, Longitude, InputName, City, StateCode, CountryCode, SortOrder) 
                values (@UserId, @Latitude, @Longitude, @InputName, @City, @StateCode, @CountryCode, @MaxUserSortOrder + 1)",
                    dataModel);
            }
        }

        public List<UserLocationDataModel> GetLocationsByUserId(Guid userId, int previousSortOrder)
        {
            var take = 4; // TODO: get from configuration / settings
            using (var db = GetConnection())
            {
                const string query = @"
select top(@Take) UserId, Latitude, Longitude, InputName, City, StateCode, CountryCode, SortOrder 
from dbo.UserLocations (nolock) 
where UserId = @UserId 
and (@PreviousSortOrder = -1 or SortOrder < @PreviousSortOrder)
order by SortOrder desc";

                var locations = db.Query<UserLocationDataModel>(query, new { UserId = userId, PreviousSortOrder = previousSortOrder, Take = take, }).ToList();

                return locations;
            }
        }

        public void DeleteLocationsByUserId(Guid userId, decimal longitude, decimal latitude)
        {
            using (var db = GetConnection())
            {
                db.Execute(
                    @"
                delete from dbo.UserLocations 
                where UserId = @UserId and Latitude = @Latitude and Longitude = @Longitude",
                    new { UserId = userId, Longitude = longitude, Latitude = latitude, });
            }
        }
    }
}
