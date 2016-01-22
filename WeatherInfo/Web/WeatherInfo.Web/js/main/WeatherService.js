var weatherModule = angular.module("weather-main");

weatherModule.factory("weatherService", function ($http, $q) {
    
    var _weatherData = [];
    var _errorMessages = [];

    var _getCurrentWeather = function (latitude, longitude) {
        var d = $q.defer();

        $http({
            method: 'GET',
            url: '/api/Weather',
            params: { latitude: latitude, longitude: longitude, },  // pass in data as strings
        })
        .then(function (r) { _getGetCurrentWeatherSuccess(r); d.resolve(); }, function () { d.reject(); });

        return d.promise;
    };

    var _getGetCurrentWeatherSuccess = function (result) {
            if (result) {
                var weatherData = {
                    currentTemperature: currentTemperature = result.data.MainItems[0].CurrentTemperature,
                    currentUnitType: result.data.UnitType,
                };
                _weatherData.push(weatherData);
            }
        };

    return {
        weatherData: _weatherData,
        getCurrentWeather: _getCurrentWeather,
        errorMessages: _errorMessages,
    };
});
