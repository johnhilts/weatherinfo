using NUnit.Framework;
using Moq;

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

        [TestCase(32, 0)]
        [TestCase(50, 10)]
        public void Temperature_Fahrenheit_ConvertsCelsius(decimal actualTemperature, decimal expectedTemperature)
        {
            // arrange
            var address = new Address();
            var weatherServiceMock = new Mock<IWeatherService>();
            var fahrenheitWeather = new WeatherInfoModel { UnitType = UnitType.Fahrenheit, Temperature = actualTemperature, };
            weatherServiceMock.Setup(m => m.GetCurrentWeatherByCurrentLocation(It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(fahrenheitWeather);

            var weather = new Weather(UnitType.Fahrenheit, address, weatherServiceMock.Object);

            // act
            var currentWeather = weather.GetCurrentWeather();
            currentWeather = weather.ConvertTo(UnitType.Celsius, currentWeather);

            // assert
            Assert.That(currentWeather.Temperature, Is.EqualTo(expectedTemperature), "Wrong Temperature");

        }

    }
}
