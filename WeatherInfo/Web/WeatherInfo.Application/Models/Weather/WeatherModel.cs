using System;

namespace WeatherInfo.Application.Models.Weather
{
    public class WeatherModel
    {
        public string UnitType { get; set; }
        public decimal CurrentTemperature { get; set; }
        public DateTime WeatherQueryTime { get; set; }
        public string TemperatureTimeText { get; set; }
    }
}
