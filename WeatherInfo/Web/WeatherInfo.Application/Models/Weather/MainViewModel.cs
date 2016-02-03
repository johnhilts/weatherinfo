using System.Collections.Generic;

namespace WeatherInfo.Application.Models.Weather
{
    public class MainViewModel
    {
        public string UnitType { get; set; }
        public List<MainItemViewModel> MainItems { get; set; }
    }
}