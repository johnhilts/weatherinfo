using System.Web.Http;
using WeatherInfo.Web.Services;

namespace WeatherInfo.Web.ApiControllers
{
    public class BaseApiController: ApiController
    {
        protected PresentationService PresentationService { get; private set; }

        public BaseApiController()
        {
            PresentationService = new PresentationService();
        }
    }
}