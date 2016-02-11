using WeatherInfo.Application.Models.Weather;

namespace WeatherInfo.Web.ApiControllers
{
    public class WeatherController : BaseApiController
    {
        public WeatherModel Get(decimal latitude, decimal longitude)
        {
            return PresentationService.Weather.GetCurrentWeatherByLocation(latitude, longitude);
        }

    }
}
