using System;
using System.Web;
using System.Web.Caching;
using WeatherInfo.Application.Models.Weather;
using WeatherInfo.Application.Services;

namespace WeatherInfo.Web.Services
{
    public class PresentationService
    {
        public readonly PresentationWeather Weather;

        private readonly SettingsService _settingsService;

        private readonly HttpContext context;

        public PresentationService()
        {
            var applicationRootPath = System.Web.Hosting.HostingEnvironment.MapPath("/");
            var keyPath = System.Web.Hosting.HostingEnvironment.MapPath("/env/openWeatherMapKey.txt");
            _settingsService = new SettingsService(applicationRootPath, keyPath);
            context = HttpContext.Current;
            Weather = new PresentationWeather(context, _settingsService);
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
                var cacheKey = "CurrentLocationTemperature";
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