using Infrastructure.Core;
using Location.Weather;
using Location.Weather.OpenWeatherMap.api;

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
    }
}
