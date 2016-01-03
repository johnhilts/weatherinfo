using System;

namespace Location.Weather
{
    /// <summary>
    /// Weather Entity
    /// </summary>
    public class Weather
    {
        private UnitType _unitType;
        private Address.Address _address;
        private IWeatherService _weatherService;

        public Weather(UnitType unitType, Address.Address address, IWeatherService weatherService)
        {
            _unitType = unitType;
            _address = address;
            _weatherService = weatherService;
        }

        public WeatherInfoModel GetCurrentWeather()
        {
            var currentWeather = _weatherService.GetCurrentWeatherByCurrentLocation(_address.Latitude, _address.Longitude);
            return currentWeather;
        }

        public WeatherInfoModel ConvertTo(UnitType unitType, WeatherInfoModel model)
        {
            switch (unitType)
            {
                case UnitType.Celsius:
                    _unitType = UnitType.Celsius;
                    var celsius = (model.Temperature - 32) / 1.8m;
                    model.Temperature = celsius;
                    model.UnitType = UnitType.Celsius;
                    return model;
                case UnitType.Fahrenheit:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Unsupported Unit Type");
            }

            return model;
        }

    }
}
