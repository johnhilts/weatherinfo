using System.Web.Mvc;
using WeatherInfo.Application.Models.Weather;

namespace WeatherInfo.Web.Controllers
{
    public class WeatherController : BaseController
    {
        public ActionResult Main()
        {
            var model = new MainViewModel
            {
                MainItems = PresentationService.Location.GetLocations(),
                UnitType = "F",
            };

            return View(model);
        }

    }
}