namespace Location.Weather
{
    /// <summary>
    /// Service to retrieve weather related information
    /// </summary>
    /// <remarks>OpenWeatherMap implementation</remarks>
    public class OpenWeatherMapWeatherService : IWeatherService
    {
        /// <summary>
        /// Get Current Weather by Location
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns>stongly typed weather information</returns>
        public WeatherInfoModel GetCurrentWeatherByLocation(decimal latitude, decimal longitude)
        {
            return new WeatherInfoModel();
        }

        /// <summary>
        /// Get Forecasted Weather by Location
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns>stongly typed weather information</returns>
        public WeatherInfoModel GetForecastedWeatherByLocation(decimal latitude, decimal longitude)
        {
            return new WeatherInfoModel();
        }

        /// <summary>
        /// Get Historical Weather by Location
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns>stongly typed weather information</returns>
        public WeatherInfoModel GetHistoricalWeatherByLocation(decimal latitude, decimal longitude)
        {
            return new WeatherInfoModel();
        }
    }
}
