var weatherModule = angular.module("weather-main");

weatherModule.factory("locationService", function ($http, $q) {

    var _locationData = { latitude: 0, longitude: 0, city: '', stateCode: '', countryCode: '', };
    var _errorMessages = [];

    function getLocation_Success(geoLocator) {
        _locationData.latitude = geoLocator.location.coords.latitude;
        _locationData.longitude = geoLocator.location.coords.longitude;
        _locationData.city = geoLocator.city;
        _locationData.stateCode = geoLocator.stateCode;
        _locationData.countryCode = geoLocator.countryCode;
    }

    var _getCurrentLocation = function () {
        var d = $q.defer();
        var geoLocator;
        geoLocator = new weatherInfo.geoLocator();
        geoLocator.getCurrentLocation(
                function (g) {
                    getLocation_Success(g);
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
