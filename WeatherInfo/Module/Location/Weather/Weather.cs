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
            var currentWeather = _weatherService.GetCurrentWeatherByLocation(_address.Latitude, _address.Longitude);
            return currentWeather;
        }

        public WeatherInfoModel ConvertTo(UnitType unitType, WeatherInfoModel model)
        {
            switch (unitType)
            {
                case UnitType.Celsius:
                    _unitType = model.UnitType = UnitType.Celsius;
                    var celsius = (model.Temperature - 32) / 1.8m;
                    model.Temperature = celsius;
                    return model;
                case UnitType.Fahrenheit:
                    _unitType = model.UnitType = UnitType.Fahrenheit;
                    var fahrenheit = model.Temperature * 1.8m + 32;
                    model.Temperature = fahrenheit;
                    return model;
                default:
                    throw new ArgumentOutOfRangeException("Unsupported Unit Type");
            }
        }

    }
}
