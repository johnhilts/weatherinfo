using System.Collections.Generic;

namespace Location.Weather.api
{
    public class CurrentWeatherDataModel
    {
        public List<weather> Weather { get; set; }
        public main Main { get; set; }

        public CurrentWeatherDataModel()
        {
            Weather = new List<weather>();
            Main = new main();
        }

        public class main
        {
            public string Temp { get; set; }
        }

    }
}
