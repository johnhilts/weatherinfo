using WeatherInfo.Application.Models.Weather;

namespace WeatherInfo.Application.Services
{
    public class WeatherService : BaseService
    {
        public WeatherService(SettingsService settingsService) : base(settingsService)
        {
        }

        public WeatherModel GetCurrentWeatherByLocation(decimal latitude, decimal longitude)
        {

            var location = new Location.Location(Settings);
            var currentWeather = location.GetCurrentWeatherByLocation(latitude, longitude);

            var model = new WeatherModel
            {
                CurrentTemperature = decimal.Round(currentWeather.Temperature, 1),
                UnitType = currentWeather.UnitType.ToString("g").Substring(0, 1).ToUpper(),
            };

            return model;
        }
    }
}
