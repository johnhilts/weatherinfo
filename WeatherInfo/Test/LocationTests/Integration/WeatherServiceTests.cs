using NUnit.Framework;

using Location.Weather;

namespace LocationTests.Integration
{
    [TestFixture]
    public class WeatherServiceTests : BaseIntegrationTest
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

    }
}
