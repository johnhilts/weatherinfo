using AutoMapper;
using Data.Model;
using Location.Model;
using Location.Weather;
using Location.Weather.OpenWeatherMap.api;
using Location.Weather.WorldWeatherOnline.api;
using System.Collections.Generic;

namespace Location.Map
{
    public class MapProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<CurrentWeatherDataModel, WeatherInfoModel>()
                .ForMember(d => d.Temperature, o => o.MapFrom(source => source.Main.Temp));

            CreateMap<ForecastWeatherDataModel, WeatherInfoModel>()
                .ForMember(d => d.Temperature, o => o.MapFrom(source => source.List[0].Temp.Day));

            CreateMap<HistoricalWeatherDataModel, WeatherInfoModel>()
                .ForMember(d => d.Temperature, o => o.MapFrom(source => source.Data.Weather[0].MaxTempF));

            CreateMap<Address, UserLocationDataModel>();

            CreateMap<UserLocationDataModel, Address>()
                //.ForMember(d=>d.Latitude, o=>o.MapFrom(source=>source.GeoLocation.Lat))
                //.ForMember(d=>d.Longitude, o=>o.MapFrom(source=>source.GeoLocation.Long))
                //.Ignore(s => s.GeoLocation)
                .ForSourceMember(x => x.GeoLocation, y => y.Ignore())
                ;

            CreateMap<List<UserLocationDataModel>, List<Address>>();

        }
    }
}
