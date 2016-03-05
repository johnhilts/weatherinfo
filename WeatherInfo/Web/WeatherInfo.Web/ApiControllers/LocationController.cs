using System.Collections.Generic;
using WeatherInfo.Application.Models.Location;

namespace WeatherInfo.Web.ApiControllers
{
    public class LocationController : BaseApiController
    {
        public List<LocationInputModel> Get(int? currentPageIndex)
        {
            return PresentationService.Location.GetLocations(currentPageIndex.GetValueOrDefault());
        }

        public dynamic Post(LocationInputModel model)
        {
            return PresentationService.Location.AddLocation(model);
        }
    }
}
