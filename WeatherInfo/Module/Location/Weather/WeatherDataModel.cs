using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.Weather
{
    public class WeatherDataModel
    {
        public List<weather> Weather { get; set; }
        public main Main { get; set; }
        public WeatherDataModel()
        {
            this.Weather = new List<weather>();
            this.Main = new main();
        }

        public class main
        {
            public string Temp { get; set; }
        }

        public class weather
        {
            public string Description { get; set; }
        }
    }
}
