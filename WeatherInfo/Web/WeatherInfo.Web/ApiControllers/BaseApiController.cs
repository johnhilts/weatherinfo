using System.Web.Http;
using WeatherInfo.Application.Services;

namespace WeatherInfo.Web.ApiControllers
{
    public class BaseApiController: ApiController
    {
        private readonly SettingsService _settingsService;
        protected readonly WeatherService _weatherService;

        public BaseApiController()
        {
            _settingsService = new SettingsService(System.Web.Hosting.HostingEnvironment.MapPath("/"), System.Web.Hosting.HostingEnvironment.MapPath("/env/openWeatherMapKey.txt"));
            _weatherService = new WeatherService(_settingsService);
        }
    }
}