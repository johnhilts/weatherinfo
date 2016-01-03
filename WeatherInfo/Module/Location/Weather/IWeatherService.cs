namespace Location.Weather
{
    /// <summary>
    /// Service to retrieve weather related information
    /// </summary>
    public interface IWeatherService
    {
        /// <summary>
        /// Get Current Weather by Location
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns>stongly typed weather information</returns>
        WeatherInfoModel GetCurrentWeatherByCurrentLocation(decimal latitude, decimal longitude);

        /// <summary>
        /// Get Forecasted Weather by Location
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns>stongly typed weather information</returns>
        WeatherInfoModel GetForecastedWeatherByCurrentLocation(decimal latitude, decimal longitude);

        /// <summary>
        /// Get Historical Weather by Location
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns>stongly typed weather information</returns>
        WeatherInfoModel GetHistoricalWeatherByCurrentLocation(decimal latitude, decimal longitude);
    }
}