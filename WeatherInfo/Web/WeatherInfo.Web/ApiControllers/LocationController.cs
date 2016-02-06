using WeatherInfo.Application.Models.Location;

namespace WeatherInfo.Web.ApiControllers
{
    public class LocationController : BaseApiController
    {
        public dynamic Post(LocationInputModel model)
        {
            return PresentationService.Location.AddLocation(model);
        }
    }
}
