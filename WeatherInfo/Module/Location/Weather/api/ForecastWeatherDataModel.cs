using System.Collections.Generic;

namespace Location.Weather.api
{
    public class ForecastWeatherDataModel
    {
        public List<list> List { get; set; }

        public ForecastWeatherDataModel()
        {
            List = new List<list>();
        }

        public class list // TODO: can this be combined with "main" somehow?
        {
            public list()
            {
                Weather = new List<weather>();
            }

            public temp Temp { get; set; }
            public List<weather> Weather { get; set; }
        }

        public class temp
        {
            public string Day { get; set; }
        }

    }
}
