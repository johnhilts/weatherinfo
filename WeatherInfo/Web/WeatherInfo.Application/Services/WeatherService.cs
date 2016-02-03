using System.Collections.Generic;
using WeatherInfo.Application.Models.Weather;

namespace WeatherInfo.Application.Services
{
    public class WeatherService : BaseService
    {
        public WeatherService(SettingsService settingsService) : base(settingsService)
        {
        }

        public MainViewModel GetCurrentWeatherByLocation(decimal latitude, decimal longitude)
        {

            var location = new Location.Location(Settings);
            var currentWeather = location.GetCurrentWeatherByLocation(latitude, longitude);

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
