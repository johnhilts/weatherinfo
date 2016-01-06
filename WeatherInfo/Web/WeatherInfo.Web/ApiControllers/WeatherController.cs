using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WeatherInfo.Web.Models.Weather;

namespace WeatherInfo.Web.ApiControllers
{
    public class WeatherController : ApiController
    {
        public MainViewModel Get(decimal latitude, decimal longitude)
        {
            var model = new MainViewModel
            {
                MainItems =
                    new List<MainItemViewModel>
                    {
                        new MainItemViewModel{ CityName="Glendale, CA, USA", CurrentTemperature=62, },
                    },
                UnitType = "F",
            };

            return model;
        }

    }
}
