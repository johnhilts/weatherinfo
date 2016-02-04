var weatherModule = angular.module("weather-main");

weatherModule.factory("geoLocationService", function ($http, $q) {

    var _hasGeoLocation = navigator && navigator.geolocation;
    var _errorMessage = "";
    var _externalSetAddressCallback = null;

    var _locationData = { latitude: 0, longitude: 0, city: '', stateCode: '', countryCode: '', };
    var _errorMessages = [];

    var _getCurrentLocation = function (externalSetAddressCallback) {
        var d = $q.defer();

        if (_hasGeoLocation) {
            _externalSetAddressCallback = externalSetAddressCallback;
            navigator.geolocation.getCurrentPosition(
                function (position) {
                    _getCurrentLocationSuccess(position);
                    d.resolve();
                }
                ,
                _getCurrentLocationError);
        }

        return d.promise;
    };

    var _getCurrentLocationSuccess = function (position) {
        _locationData.latitude = position.coords.latitude;
        _locationData.longitude = position.coords.longitude;
        _setAddress();
    };

    var _getCurrentLocationError = function (msg) {
        _errorMessage = msg;
    };

    // NOTE: this method has a dependency on Google maps api
    var _setAddress = function () {

        var geocoder = new google.maps.Geocoder();

        var location = new google.maps.LatLng(_locationData.latitude, _locationData.longitude);

        geocoder.geocode({ 'latLng': location }, _setAddressCallback);
    };

    var _setAddressCallback = function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            if (results && results[0]) {
                var addressInfo = results[0].address_components;
                var addressComponentsCount = addressInfo.length;
                for (var i = 0; i < addressComponentsCount; i++) {
                    var longName = addressInfo[i].long_name;
                    var shortName = addressInfo[i].short_name;
                    switch (addressInfo[i].types[0]) {
                        case "locality":
                            _locationData.city = longName;
                            break;
                        case "administrative_area_level_1":
                            _locationData.stateCode = shortName;
                            break;
                        case "country":
                            _locationData.countryCode = shortName;
                            break;
                    }
                }
                if (_externalSetAddressCallback) {
                    _externalSetAddressCallback(_locationData);
                }
            }
        }
    };

    // NOTE: this method has a dependency on Google maps api
    // TODO: make this more int'l friendly
    var _searchLocation = function (address, externalSetAddressCallback) {
        var d = $q.defer();

        if (_hasGeoLocation) {
            var geocoder = new google.maps.Geocoder();
            _externalSetAddressCallback = externalSetAddressCallback;
            geocoder.geocode({ 'address': address },
                function (results, status) {
                    _setLocationByAddressCallback(results, status);
                    d.resolve();
                }
                ,
                _getCurrentLocationError);
        }

        return d.promise;
    };

    var _setLocationByAddressCallback =
            function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    if (results[0]) {
                        _locationData.latitude = results[0].geometry.location.lat();
                        _locationData.longitude = results[0].geometry.location.lng();
                        if (_externalSetAddressCallback) {
                            _externalSetAddressCallback(_locationData);
                        }
                    }
                }
            };

    return {
        getCurrentLocation: _getCurrentLocation,
        searchLocation: _searchLocation,
        errorMessages: _errorMessages,
    };

});
