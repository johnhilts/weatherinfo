using NUnit.Framework;
using Moq;

using Location.Model;
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

        [TestCase(32, 0, UnitType.Fahrenheit, UnitType.Celsius)]
        [TestCase(50, 10, UnitType.Fahrenheit, UnitType.Celsius)]
        [TestCase(-4, -20, UnitType.Fahrenheit, UnitType.Celsius)]
        [TestCase(23, -5, UnitType.Fahrenheit, UnitType.Celsius)]
        [TestCase(68, 20, UnitType.Fahrenheit, UnitType.Celsius)]
        [TestCase(86, 30, UnitType.Fahrenheit, UnitType.Celsius)]
        [TestCase(104, 40, UnitType.Fahrenheit, UnitType.Celsius)]
        [TestCase(212, 100, UnitType.Fahrenheit, UnitType.Celsius)]
        [TestCase(0, 32, UnitType.Celsius, UnitType.Fahrenheit)]
        [TestCase(10, 50, UnitType.Celsius, UnitType.Fahrenheit)]
        [TestCase(-20, -4, UnitType.Celsius, UnitType.Fahrenheit)]
        [TestCase(-5, 23, UnitType.Celsius, UnitType.Fahrenheit)]
        [TestCase(20, 68, UnitType.Celsius, UnitType.Fahrenheit)]
        [TestCase(30, 86, UnitType.Celsius, UnitType.Fahrenheit)]
        [TestCase(40, 104, UnitType.Celsius, UnitType.Fahrenheit)]
        [TestCase(100, 212, UnitType.Celsius, UnitType.Fahrenheit)]
        public void Temperature_Typical_Converts(decimal actualTemperature, decimal expectedTemperature, UnitType fromUnitType, UnitType toUnitType)
        {
            // arrange
            var address = new Address();
            var weatherServiceMock = new Mock<IWeatherService>();
            var fahrenheitWeather = new WeatherInfoModel { UnitType = fromUnitType, Temperature = actualTemperature, };
            weatherServiceMock.Setup(m => m.GetCurrentWeatherByLocation(It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(fahrenheitWeather);

            var weather = new Weather(UnitType.Fahrenheit, address, weatherServiceMock.Object);

            // act
            var currentWeather = weather.GetCurrentWeather();
            currentWeather = weather.ConvertTo(toUnitType, currentWeather);

            // assert
            Assert.That(currentWeather.Temperature, Is.EqualTo(expectedTemperature), "Wrong Temperature");

        }

    }
}
