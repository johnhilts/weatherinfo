using AutoMapper;
using Location.Address;
using System.Collections.Generic;
using WeatherInfo.Application.Models.Location;

namespace Application.Map
{
    public class MapProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<LocationInputModel, Address>();

            CreateMap<List<Address>, List<LocationInputModel>>();

        }
    }
}
