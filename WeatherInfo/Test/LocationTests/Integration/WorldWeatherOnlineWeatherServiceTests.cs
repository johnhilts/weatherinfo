using NUnit.Framework;

using Location.Weather.WorldWeatherOnline.api;

namespace LocationTests.Integration
{
    [TestFixture]
    public class WorldWeatherOnlineWeatherServiceTests : BaseIntegrationTest
    {

        [Test]
        public void HistoricalWeather_ValidLocation_GetWeather()
        {
            // arrange 
            var weatherService = new WorldWeatherOnlineWeatherService(_worldWeatherOnlineKeyPath);

            // act
            var historicalWeather = weatherService.GetHistoricalWeatherByLocation(36, 114);

            // assert
            Assert.That(historicalWeather.Temperature, Is.Not.EqualTo(0));
        }

    }
}
