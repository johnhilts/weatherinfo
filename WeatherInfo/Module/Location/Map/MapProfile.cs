using AutoMapper;
using Data.Model;
using Location.Model;
using Location.Weather;
using Location.Weather.OpenWeatherMap.api;
using Location.Weather.WorldWeatherOnline.api;
using Microsoft.SqlServer.Types;
using System.Collections.Generic;
using System;

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

            CreateMap<UserLocationDataModel, Address>() ;

        }
    }

}
