using System.Web.Mvc;

namespace WeatherInfo.Web.Controllers
{
    public class WeatherController : BaseController
    {
        public ActionResult Main()
        {
            return View();
        }

    }
}