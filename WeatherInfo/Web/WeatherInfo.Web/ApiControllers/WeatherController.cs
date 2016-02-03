﻿using WeatherInfo.Application.Models.Weather;

namespace WeatherInfo.Web.ApiControllers
{
    public class WeatherController : BaseApiController
    {
        public MainViewModel Get(decimal latitude, decimal longitude)
        {
            return _weatherService.GetCurrentWeatherByLocation(latitude, longitude);
        }

    }
}
