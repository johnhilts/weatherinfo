using AutoMapper;
using Location.Address;
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
            var address = Mapper.Map<Address>(model);

            var location = new Location.Location(Settings);
            location.Add(address);

            return true;
        }

    }
}
