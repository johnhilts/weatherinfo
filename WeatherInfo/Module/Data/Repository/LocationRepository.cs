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

        public List<UserLocationDataModel> GetLocationsByUserId(Guid userId)
        {
            using (var db = GetConnection())
            {
                const string query = "select UserId, Latitude, Longitude, InputName, City, StateCode, CountryCode, SortOrder from dbo.UserLocations (nolock) where UserId = @UserId";
                var locations = db.Query<UserLocationDataModel>(query, new { UserId = userId, }).ToList();

                return locations;
            }
        }
    }
}
