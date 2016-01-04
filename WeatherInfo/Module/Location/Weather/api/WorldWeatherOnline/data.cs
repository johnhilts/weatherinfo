using System.Collections.Generic;

namespace Location.Weather.WorldWeatherOnline.api
{
    public class data
    {
        public List<weather> Weather { get; set; }

        public data()
        {
            Weather = new List<weather>();
        }

    }
}
