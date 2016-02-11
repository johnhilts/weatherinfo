using AutoMapper;
using Location.Model;
using System;
using System.Collections.Generic;
using WeatherInfo.Application.Models.Location;

namespace WeatherInfo.Application.Services
{
    public class LocationService : BaseService
    {
        public LocationService(SettingsService settingsService) : base(settingsService)
        {
        }

        public List<LocationInputModel> GetLocations()
        {
            var userId = new Guid("C49200FC-C271-4CC3-8905-086A2CE9AB4E");
            var location = new Location.Location(Settings);
            return Mapper.Map<List<LocationInputModel>>(location.GetLocationsByUserId(userId));
        }

        // TODO: return a more complex type
        public dynamic AddLocation(LocationInputModel model)
        {
            var address = Mapper.Map<Address>(model);

            var location = new Location.Location(Settings);
            location.Add(address);

            return true;
        }

    }
}
