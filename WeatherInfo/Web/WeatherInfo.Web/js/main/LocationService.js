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

    var _getLocations = function () {
        var d = $q.defer();

        $http({
            method: 'GET',
            url: '/api/Location',
            //params: { latitude: latitude, longitude: longitude, },  // pass in data as strings
        })
        .then(function (r) { _getLocationsSuccess(r); d.resolve(); }, function () { d.reject(); });

        return d.promise;
    };

    var _getLocationsSuccess = function (result) {
            if (result) {
                //var locationData = {
                //    currentTemperature: currentTemperature = result.data.MainItems[0].CurrentTemperature,
                //    currentUnitType: result.data.UnitType,
                //};
                _locationData.push(result.data);
            }
        };

    var _searchLocation = function (address) {
        var d = $q.defer();

        geoLocationService.searchLocation(address, 
                function (g) {
                    _locationData.push(g);
                    d.resolve();
                }
            );

        return d.promise;
    };

    var _addLocation = function (locationData) {
        var d = $q.defer();
        $http({
            method: 'POST',
            url: '/api/Location',
            data: $.param({ InputName: locationData.inputName, City: locationData.city, StateCode: locationData.stateCode, CountryCode: locationData.countryCode, Latitude: locationData.latitude, Longitude: locationData.longitude, }),  // pass in data as strings
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }  // set the headers so angular passing info as form data (not request payload)
        })
        .then(
            function (r) {
                if (r.data.Success) {
                    _errorMessages.length = 0;
                    alert("add success");
                }
                else {
                    r.data.ErrorMessages.forEach(function (x) { _errorMessages.push(x); });
                    alert("add fail");
                }
                 d.resolve();
            },
            function () { d.reject(); });
        return d.promise;
    };

    return {
        locationData: _locationData,
        getCurrentLocation: _getCurrentLocation,
        getLocations: _getLocations,
        addLocation: _addLocation,
        searchLocation: _searchLocation,
        errorMessages: _errorMessages,
    };

});
