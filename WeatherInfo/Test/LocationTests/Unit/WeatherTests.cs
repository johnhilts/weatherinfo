using NUnit.Framework;

using Location.Address;
using Location.Weather;

namespace LocationTests.Unit
{
    [TestFixture]
    public class WeatherTests
    {
        [SetUp]
        protected void SetUp()
        {
            // do nothing
        }

        [Test]
        public void Temperature_Fahrenheit_ConvertsCelsius()
        {
            // arrange
            var address = new Address();

            var weather = new Weather(UnitType.Fahrenheit, address, weatherServiceMock.Object);

            // act
            var currentWeather = weather.GetCurrentWeather();

            // assert
            Assert.That(currentWeather.Temperature, Is.EqualTo(0), "Wrong Temperature");

        }

    }
}
