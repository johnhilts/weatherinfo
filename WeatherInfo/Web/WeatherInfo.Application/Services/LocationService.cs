using WeatherInfo.Application.Models.Location;

namespace WeatherInfo.Application.Services
{
    public class LocationService : BaseService
    {
        public LocationService(SettingsService settingsService) : base(settingsService)
        {
        }

        public dynamic AddLocation(LocationInputModel model)
        {
            return true;
        }

    }
}
