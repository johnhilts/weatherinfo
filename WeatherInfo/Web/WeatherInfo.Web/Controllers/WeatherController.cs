using System.Collections.Generic;
using System.Web.Mvc;
using WeatherInfo.Web.Models.Weather;

namespace WeatherInfo.Web.Controllers
{
    public class WeatherController : Controller
    {
        public ActionResult Main()
        {
            var model = new MainViewModel
            {
                MainItems =
                    new List<MainItemViewModel>
                    {
                        new MainItemViewModel{ CityName="Burbank", CurrentTemperature=60, },
                        new MainItemViewModel {CityName="Granada Hills", CurrentTemperature=64, },
                        new MainItemViewModel {CityName="Los Angeles", CurrentTemperature=58, },
                    },
                UnitType = "F",
            };

            return View(model);
        }

    }
}