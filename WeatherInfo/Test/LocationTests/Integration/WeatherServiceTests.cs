using AutoMapper;
using Location.Map;
using Location.Weather;
using NUnit.Framework;
using System.IO;

namespace LocationTests.Integration
{
    [TestFixture]
    public class WeatherServiceTests
    {

        private readonly string _keyPath = "../../Integration/env/key.txt";

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            // TODO: this needs to be run only *once* for the entire test suite
            Mapper.Initialize(x => x.AddProfile<MapProfile>());
        }

        [SetUp]
        protected void SetUp()
        {
            if (!File.Exists(_keyPath)) Assert.Ignore("Integration Test: do not run outside of test environment.");
        }

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

    }
}
