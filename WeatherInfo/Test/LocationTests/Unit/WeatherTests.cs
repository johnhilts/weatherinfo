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
        [TestCase(-4, -20)]
        [TestCase(23, -5)]
        [TestCase(68, 20)]
        [TestCase(86, 30)]
        [TestCase(104, 40)]
        [TestCase(212, 100)]
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

        [TestCase(0, 32)]
        [TestCase(10, 50)]
        [TestCase(-20, -4)]
        [TestCase(-5, 23)]
        [TestCase(20, 68)]
        [TestCase(30, 86)]
        [TestCase(40, 104)]
        [TestCase(100, 212)]
        public void Temperature_Celsius_ConvertsFahrenheit(decimal actualTemperature, decimal expectedTemperature)
        {
            // arrange
            var address = new Address();
            var weatherServiceMock = new Mock<IWeatherService>();
            var celsiusWeather = new WeatherInfoModel { UnitType = UnitType.Celsius, Temperature = actualTemperature, };
            weatherServiceMock.Setup(m => m.GetCurrentWeatherByCurrentLocation(It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(celsiusWeather);

            var weather = new Weather(UnitType.Fahrenheit, address, weatherServiceMock.Object);

            // act
            var currentWeather = weather.GetCurrentWeather();
            currentWeather = weather.ConvertTo(UnitType.Fahrenheit, currentWeather);

            // assert
            Assert.That(currentWeather.Temperature, Is.EqualTo(expectedTemperature), "Wrong Temperature");

        }

    }
}
