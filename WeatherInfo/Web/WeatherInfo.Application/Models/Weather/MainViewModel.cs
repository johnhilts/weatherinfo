using System.Collections.Generic;
using WeatherInfo.Application.Models.Location;

namespace WeatherInfo.Application.Models.Weather
{
    public class MainViewModel
    {
        public string UnitType { get; set; }
        public List<LocationInputModel> MainItems { get; set; }
    }
}