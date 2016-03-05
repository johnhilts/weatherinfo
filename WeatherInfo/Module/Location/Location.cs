using System;
using Infrastructure.Core;
using Location.Model;
using Location.Weather;
using Location.Weather.OpenWeatherMap.api;
using Data.Repository;
using AutoMapper;
using Data.Model;
using System.Collections.Generic;

namespace Location
{
    /// <summary>
    /// Location Aggregate
    /// </summary>
    public class Location
    {
        protected Settings _settings{ get; private set; }

        public Location(Settings settings)
        {
            _settings = settings;
        }

        public WeatherInfoModel GetCurrentWeatherByLocation(decimal latitude, decimal longitude)
        {
            var weatherService = new OpenWeatherMapWeatherService(_settings.KeyPath);
            var currentWeather = weatherService.GetCurrentWeatherByLocation(latitude, longitude);
            currentWeather.UnitType = UnitType.Fahrenheit;
            currentWeather.Temperature = currentWeather.Temperature * 9 / 5 - 459.67m;

            return currentWeather;
        }

        public List<Address> GetLocationsByUserId(Guid userId, int currentPageIndex)
        {
            var repo = new LocationRepository(_settings);
            return Mapper.Map<List<Address>>(repo.GetLocationsByUserId(userId, currentPageIndex));
        }

        public void Add(Address address)
        {
            var repo = new LocationRepository(_settings);
            var dataModel = Mapper.Map<UserLocationDataModel>(address);
            dataModel.UserId = new Guid("C49200FC-C271-4CC3-8905-086A2CE9AB4E");
            repo.AddUserLocation(dataModel);
        }

    }
}
