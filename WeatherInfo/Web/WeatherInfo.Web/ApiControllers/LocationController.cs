using System.Collections.Generic;
using WeatherInfo.Application.Models.Location;

namespace WeatherInfo.Web.ApiControllers
{
    public class LocationController : BaseApiController
    {
        public List<LocationInputModel> Get(int? currentPageIndex, int? previousSortOrder)
        {
            return PresentationService.Location.GetLocations(currentPageIndex.GetValueOrDefault(), previousSortOrder.GetValueOrDefault());
        }

        public dynamic Post(LocationInputModel model)
        {
            return PresentationService.Location.AddLocation(model);
        }
    }
}
