using Newtonsoft.Json;
using System.Net;
using System.Configuration;
using System;

using AutoMapper;

namespace Location.Weather.OpenWeatherMap.api
{
    /// <summary>
    /// Service to retrieve weather related information
    /// </summary>
    /// <remarks>OpenWeatherMap implementation</remarks>
    public class OpenWeatherMapWeatherService : IWeatherService
    {
        private readonly string _keyFilePath;

        public OpenWeatherMapWeatherService(string keyFilePath)
        {
            _keyFilePath = keyFilePath;
        }

        /// <summary>
        /// Get Current Weather by Location
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns>stongly typed weather information</returns>
        public WeatherInfoModel GetCurrentWeatherByLocation(decimal latitude, decimal longitude)
        {
            var weatherDataModel = GetWeatherInfo(latitude, longitude);
            return Mapper.Map<WeatherInfoModel>(weatherDataModel);
        }

        private CurrentWeatherDataModel GetWeatherInfo(decimal latitude, decimal longitude)
        {
            var jsonString = GetWeatherInfoApi(latitude, longitude);
            var weatherData = JsonConvert.DeserializeObject<CurrentWeatherDataModel>(jsonString);

            if (weatherData == null || weatherData.Weather == null || weatherData.Weather[0].Description == null || weatherData.Main.Temp == null)
            {
                throw new System.Exception("No valid data retrieved");
            }

            return weatherData;
        }

        private string GetWeatherInfoApi(decimal latitude, decimal longitude)
        {
            var keyFromConfig = ConfigurationManager.AppSettings["OpenWeatherMapApiKey"];
            var apiKey = string.IsNullOrWhiteSpace(keyFromConfig) ? System.IO.File.ReadAllText(_keyFilePath) : keyFromConfig;
            if (string.IsNullOrWhiteSpace(apiKey)) throw new Exception("No API Key");

            var openWeatherMapApiFormat = "http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid={2}";
            var requestUrl = string.Format(openWeatherMapApiFormat, latitude, longitude, apiKey);
            var client = new WebClient();
            var response = client.DownloadString(requestUrl);
            return response;
        }

        /// <summary>
        /// Get Forecasted Weather by Location
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns>stongly typed weather information</returns>
        public WeatherInfoModel GetForecastWeatherByLocation(decimal latitude, decimal longitude)
        {
            var weatherDataModel = GetFutureWeatherInfo(latitude, longitude);
            return Mapper.Map<WeatherInfoModel>(weatherDataModel);
        }

        private ForecastWeatherDataModel GetFutureWeatherInfo(decimal latitude, decimal longitude)
        {
            var jsonString = GetFutureWeatherInfoApi(latitude, longitude);
            var weatherData = JsonConvert.DeserializeObject<ForecastWeatherDataModel>(jsonString);

            if (weatherData == null || weatherData.List == null || weatherData.List[0].Weather == null || weatherData.List[0].Weather[0].Description == null || weatherData.List[0].Temp == null)
            {
                throw new System.Exception("No valid data retrieved");
            }

            return weatherData;
        }

        private string GetFutureWeatherInfoApi(decimal latitude, decimal longitude)
        {
            var keyFromConfig = ConfigurationManager.AppSettings["OpenWeatherMapApiKey"];
            var apiKey = string.IsNullOrWhiteSpace(keyFromConfig) ? System.IO.File.ReadAllText(_keyFilePath) : keyFromConfig;
            if (string.IsNullOrWhiteSpace(apiKey)) throw new Exception("No API Key");

            var openWeatherMapApiFormat = "http://api.openweathermap.org/data/2.5/forecast/daily?lat={0}&lon={1}&cnt=16&mode=json&appid={2}";
            var requestUrl = string.Format(openWeatherMapApiFormat, latitude, longitude, apiKey);
            var client = new WebClient();
            var response = client.DownloadString(requestUrl);
            return response;
        }

        /// <summary>
        /// Get Historical Weather by Location
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns>stongly typed weather information</returns>
        /// <remarks>Not Implemented</remarks>
        public WeatherInfoModel GetHistoricalWeatherByLocation(decimal latitude, decimal longitude)
        {
            throw new NotImplementedException("OpenWeatherMap does not offer free historical data");
        }
    }
}
