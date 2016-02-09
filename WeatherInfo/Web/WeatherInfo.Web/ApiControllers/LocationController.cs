using System.Collections.Generic;
using WeatherInfo.Application.Models.Location;

namespace WeatherInfo.Web.ApiControllers
{
    public class LocationController : BaseApiController
    {
        public List<LocationInputModel> Get()
        {
            return PresentationService.Location.GetLocations();
        }

        public dynamic Post(LocationInputModel model)
        {
            return PresentationService.Location.AddLocation(model);
        }
    }
}
