using Location.Weather.OpenWeatherMap.api;
using System.Collections.Generic;
using System.Web.Http;
using WeatherInfo.Web.Models.Weather;

namespace WeatherInfo.Web.ApiControllers
{
    public class WeatherController : ApiController
    {
        public MainViewModel Get(decimal latitude, decimal longitude)
        {

            var weatherService = new OpenWeatherMapWeatherService(System.Web.Hosting.HostingEnvironment.MapPath("/env/openWeatherMapKey.txt"));
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
