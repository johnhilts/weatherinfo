﻿using System;
using System.Web;
using System.Web.Caching;
using WeatherInfo.Application.Models.Location;
using WeatherInfo.Application.Models.Weather;
using WeatherInfo.Application.Services;

namespace WeatherInfo.Web.Services
{
    public class PresentationService
    {
        public readonly PresentationLocation Location;
        public readonly PresentationWeather Weather;

        private readonly SettingsService _settingsService;

        private readonly HttpContext context;

        public PresentationService()
        {
            var applicationRootPath = System.Web.Hosting.HostingEnvironment.MapPath("/");
            var keyPath = System.Web.Hosting.HostingEnvironment.MapPath("/env/openWeatherMapKey.txt");
            _settingsService = new SettingsService(applicationRootPath, keyPath);
            context = HttpContext.Current;
            Location = new PresentationLocation(context, _settingsService);
            Weather = new PresentationWeather(context, _settingsService);
        }

        public class PresentationLocation
        {
            private readonly LocationService _locationService;
            private readonly HttpContext context;

            public PresentationLocation(HttpContext context, SettingsService settingsService)
            {
                this.context = context;
                _locationService = new LocationService(settingsService);
            }

            public dynamic AddLocation(LocationInputModel model)
            {

                if (!string.IsNullOrWhiteSpace(model.City) && !string.IsNullOrWhiteSpace(model.StateCode) && !string.IsNullOrWhiteSpace(model.CountryCode))
                    return new { Success = true, };
                else
                    return new { Success = false, };
            }
        }

        public class PresentationWeather
        {
            private readonly WeatherService _weatherService;
            private readonly HttpContext context;

            public PresentationWeather(HttpContext context, SettingsService settingsService)
            {
                this.context = context;
                _weatherService = new WeatherService(settingsService);
            }

            public MainViewModel GetCurrentWeatherByLocation(decimal latitude, decimal longitude)
            {
                var cacheKey = string.Format("{0}x{1},", latitude.ToString("F4"), longitude.ToString("F4"));
                var model = context.Cache[cacheKey] as MainViewModel;
                if (model == null)
                {
                    model = _weatherService.GetCurrentWeatherByLocation(latitude, longitude);
                    context.Cache.Insert(cacheKey, model, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(30));
                }

                return model;
            }
        }
    }
}