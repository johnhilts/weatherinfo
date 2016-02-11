using System.Web.Mvc;
using WeatherInfo.Web.Services;

namespace WeatherInfo.Web.Controllers
{
    public class BaseController : Controller
    {
        protected PresentationService PresentationService { get; private set; }

        public BaseController()
        {
            PresentationService = new PresentationService();
        }
    }
}
