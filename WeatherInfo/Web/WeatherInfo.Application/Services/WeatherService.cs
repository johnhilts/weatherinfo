using System.Collections.Generic;
using WeatherInfo.Application.Models.Weather;
using Location.Weather.OpenWeatherMap.api;

namespace WeatherInfo.Application.Services
{
    public class WeatherService : BaseService
    {
        public WeatherService(SettingsService settingsService) : base(settingsService)
        {
        }

        public MainViewModel GetCurrentWeatherByLocation(decimal latitude, decimal longitude)
        {

            var weatherService = new OpenWeatherMapWeatherService(SettingsService.GetKeyPath());
            var currentWeather = weatherService.GetCurrentWeatherByLocation(latitude, longitude);
            currentWeather.UnitType = Location.Weather.UnitType.Fahrenheit;
            currentWeather.Temperature = currentWeather.Temperature * 9 / 5 - 459.67m;

            var model = new MainViewModel
            {
                MainItems =
                    new List<MainItemViewModel>
                    {
                        new MainItemViewModel{ CityName="Glendale, CA, USA", CurrentTemperature= decimal.Round(currentWeather.Temperature, 1), },
                    },
                UnitType = currentWeather.UnitType.ToString("g").Substring(0, 1).ToUpper(),
            };

            return model;
        }
    }
}
