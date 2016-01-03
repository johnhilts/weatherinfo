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
            return new WeatherInfoModel();
        }

        public WeatherInfoModel ConvertTo(UnitType unitType, WeatherInfoModel model)
        {
            switch (unitType)
            {
                case UnitType.Celsius:
                    break;
                case UnitType.Fahrenheit:
                    break;
                default:
                    break;
            }

            return model;
        }

    }
}
