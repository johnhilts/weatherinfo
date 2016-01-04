using AutoMapper;

using Location.Weather;
using Location.Weather.api;

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

        }
    }
}
