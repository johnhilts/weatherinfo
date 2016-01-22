var weatherModule = angular.module("weather-main");

weatherModule.factory("locationService", function ($http, $q, geoLocationService) {

    var _locationData = [];
    var _errorMessages = [];

    var _getCurrentLocation = function () {
        var d = $q.defer();

        geoLocationService.getCurrentLocation(
                function (g) {
                    _locationData.push(g);
                    d.resolve();
                }
            );

        return d.promise;
    };

    return {
        locationData: _locationData,
        getCurrentLocation: _getCurrentLocation,
        errorMessages: _errorMessages,
    };

});
