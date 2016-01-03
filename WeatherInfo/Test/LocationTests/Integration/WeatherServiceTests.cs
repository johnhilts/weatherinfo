using AutoMapper;
using Location.Map;
using Location.Weather;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationTests.Integration
{
    [TestFixture]
    public class WeatherServiceTests
    {
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            // TODO: this needs to be run only *once* for the entire test suite
            Mapper.Initialize(x => x.AddProfile<MapProfile>());
        }

        [Test]
        public void CurrentWeather_ValidLocation_GetWeather()
        {
            // arrange 
            var weatherService = new OpenWeatherMapWeatherService("../../Integration/env/key.txt");

            // act
            var currentWeather = weatherService.GetCurrentWeatherByLocation(0, 0);

            // assert
            Assert.That(currentWeather.Temperature, Is.Not.EqualTo(0));
        }

    }
}
