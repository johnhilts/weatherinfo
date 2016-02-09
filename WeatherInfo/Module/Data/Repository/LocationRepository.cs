﻿using Dapper;
using Data.Model;
using Infrastructure.Core;
using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;

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
            var geoFormat = "POINT({0} {1})";
            dataModel.GeoLocation = SqlGeography.STPointFromText(new SqlChars(new SqlString(string.Format(geoFormat, dataModel.Longitude, dataModel.Latitude))), 4326);
            using (var db = GetConnection())
            {
                db.Execute(
                    @"
                declare @MaxUserSortOrder tinyint = (select max(SortOrder) from dbo.UserLocations where UserId = @UserId)
                insert into dbo.UserLocations(UserId, GeoLocation, City, StateCode, CountryCode, SortOrder) 
                values (@UserId, @GeoLocation, @City, @StateCode, @CountryCode, @MaxUserSortOrder + 1)",
                    dataModel);
            }
        }

        public List<UserLocationDataModel> GetLocationsByUserId(Guid userId)
        {
            using (var db = GetConnection())
            {
                const string query = "select UserId, GeoLocation, City, StateCode, CountryCode, SortOrder where UserId = @UserId";
                return (List<UserLocationDataModel>)db.Query<UserLocationDataModel>(query, new { UserId = userId, });
            }
        }
    }
}
