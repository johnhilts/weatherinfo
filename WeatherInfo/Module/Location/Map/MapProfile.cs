using AutoMapper;
using Location.Weather;

namespace Location.Map
{
    public class MapProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<WeatherDataModel, WeatherInfoModel>()
                .ForMember(d => d.Temperature, o => o.MapFrom(source => source.Main.Temp));
        }
    }
}
