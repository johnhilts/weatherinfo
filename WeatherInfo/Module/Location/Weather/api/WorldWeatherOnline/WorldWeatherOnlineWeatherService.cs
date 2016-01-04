using Newtonsoft.Json;
using System.Net;
using System.Configuration;
using System;

using AutoMapper;

namespace Location.Weather.WorldWeatherOnline.api
{
    /// <summary>
    /// Service to retrieve weather related information
    /// </summary>
    /// <remarks>OpenWeatherMap implementation</remarks>
    public class WorldWeatherOnlineWeatherService : IWeatherService
    {
        private readonly string _keyFilePath;

        public WorldWeatherOnlineWeatherService(string keyFilePath)
        {
            _keyFilePath = keyFilePath;
        }

        /// <summary>
        /// Get Current Weather by Location
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns>stongly typed weather information</returns>
        /// <remarks>Not Implemented</remarks>
        public WeatherInfoModel GetCurrentWeatherByLocation(decimal latitude, decimal longitude)
        {
            throw new NotImplementedException("Not Implemented");
        }

        /// <summary>
        /// Get Forecasted Weather by Location
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns>stongly typed weather information</returns>
        /// <remarks>Not Implemented</remarks>
        public WeatherInfoModel GetForecastWeatherByLocation(decimal latitude, decimal longitude)
        {
            throw new NotImplementedException("Not Implemented");
        }

        /// <summary>
        /// Get Historical Weather by Location
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns>stongly typed weather information</returns>
        public WeatherInfoModel GetHistoricalWeatherByLocation(decimal latitude, decimal longitude)
        {
            var weatherDataModel = GetHistoricalWeatherInfo(latitude, longitude);
            return Mapper.Map<WeatherInfoModel>(weatherDataModel);
        }

        private HistoricalWeatherDataModel GetHistoricalWeatherInfo(decimal latitude, decimal longitude)
        {
            var jsonString = GetHistoricalWeatherInfoApi(latitude, longitude);
            var weatherData = JsonConvert.DeserializeObject<HistoricalWeatherDataModel>(jsonString);

            if (weatherData == null || weatherData.Data == null || weatherData.Data.Weather == null || weatherData.Data.Weather[0].MaxTempF == null)
            {
                throw new Exception("No valid data retrieved");
            }

            return weatherData;
        }

        private string GetHistoricalWeatherInfoApi(decimal latitude, decimal longitude)
        {
            var keyFromConfig = ConfigurationManager.AppSettings["WorldWeatherOnlineApiKey"];
            var apiKey = string.IsNullOrWhiteSpace(keyFromConfig) ? System.IO.File.ReadAllText(_keyFilePath) : keyFromConfig;
            if (string.IsNullOrWhiteSpace(apiKey)) throw new Exception("No API Key");

            var yesterday = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");

            var worldWeatherOnlineApiFormat = "http://api.worldweatheronline.com/free/v2/past-weather.ashx?key={0}&q={1},{2}&cc=no&date={3}&format=json";
            var requestUrl = string.Format(worldWeatherOnlineApiFormat, apiKey, latitude, longitude, yesterday);
            var client = new WebClient();
            var response = client.DownloadString(requestUrl);
            return response;
        }

    }
}
