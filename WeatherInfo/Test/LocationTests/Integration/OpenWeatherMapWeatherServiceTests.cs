using System;

using NUnit.Framework;

using Location.Weather.OpenWeatherMap.api;

namespace LocationTests.Integration
{
    [TestFixture]
    public class OpenWeatherMapWeatherServiceTests : BaseIntegrationTest
    {

        [Test]
        public void CurrentWeather_ValidLocation_GetWeather()
        {
            // arrange 
            var weatherService = new OpenWeatherMapWeatherService(_openWeatherMapKeyPath);

            // act
            var currentWeather = weatherService.GetCurrentWeatherByLocation(0, 0);

            // assert
            Assert.That(currentWeather.Temperature, Is.Not.EqualTo(0));
        }

        [Test]
        public void ForecastWeather_ValidLocation_GetWeather()
        {
            // arrange 
            var weatherService = new OpenWeatherMapWeatherService(_openWeatherMapKeyPath);

            // act
            var forecastWeather = weatherService.GetForecastWeatherByLocation(0, 0);

            // assert
            Assert.That(forecastWeather.Temperature, Is.Not.EqualTo(0));
        }

        [Test, ExpectedException(ExpectedException = typeof(NotImplementedException), ExpectedMessage = "OpenWeatherMap does not offer free historical data")]
        public void HistoricalWeather_ValidLocation_GetWeather()
        {
            // arrange 
            var weatherService = new OpenWeatherMapWeatherService(_openWeatherMapKeyPath);

            // act
            var historicalWeather = weatherService.GetHistoricalWeatherByLocation(0, 0);

            // assert
            Assert.Fail("Should have thrown an exception.");
        }

    }
}
