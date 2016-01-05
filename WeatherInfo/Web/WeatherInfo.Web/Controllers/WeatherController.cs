using System.Web.Mvc;

namespace WeatherInfo.Web.Controllers
{
    public class WeatherController : Controller
    {
        public ActionResult Main()
        {
            return View();
        }

    }
}