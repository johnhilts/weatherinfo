using NUnit.Framework;

using Location.Weather;
using System;

namespace LocationTests.Integration
{
    [TestFixture]
    public class OpenWeatherMapWeatherServiceTests : BaseIntegrationTest
    {

        [Test]
        public void CurrentWeather_ValidLocation_GetWeather()
        {
            // arrange 
            var weatherService = new OpenWeatherMapWeatherService(_keyPath);

            // act
            var currentWeather = weatherService.GetCurrentWeatherByLocation(0, 0);

            // assert
            Assert.That(currentWeather.Temperature, Is.Not.EqualTo(0));
        }

        [Test]
        public void ForecastWeather_ValidLocation_GetWeather()
        {
            // arrange 
            var weatherService = new OpenWeatherMapWeatherService(_keyPath);

            // act
            var currentWeather = weatherService.GetForecastWeatherByLocation(0, 0);

            // assert
            Assert.That(currentWeather.Temperature, Is.Not.EqualTo(0));
        }

        [Test, ExpectedException(ExpectedException = typeof(NotImplementedException), ExpectedMessage = "OpenWeatherMap does not offer free historical data")]
        public void HistoricalWeather_ValidLocation_GetWeather()
        {
            // arrange 
            var weatherService = new OpenWeatherMapWeatherService(_keyPath);

            // act
            var currentWeather = weatherService.GetHistoricalWeatherByLocation(0, 0);

            // assert
            Assert.Fail("Should have thrown an exception.");
        }

    }
}
