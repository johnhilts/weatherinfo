using System;
using System.Collections.Generic;
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
            var configurationPath = System.Web.Hosting.HostingEnvironment.MapPath("/env/WeatherInfoConnectionString.txt");
            _settingsService = new SettingsService(applicationRootPath, keyPath, configurationPath);
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

            public List<LocationInputModel> GetLocations(int currentPageIndex, int previousSortOrder)
            {
                return _locationService.GetLocations(currentPageIndex, previousSortOrder);
            }

            public dynamic AddLocation(LocationInputModel model)
            {
                if (_locationService.AddLocation(model))
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

            public WeatherModel GetCurrentWeatherByLocation(decimal latitude, decimal longitude)
            {
                var cacheKey = string.Format("{0}x{1},", latitude.ToString("F4"), longitude.ToString("F4"));
                var model = context.Cache[cacheKey] as WeatherModel;
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