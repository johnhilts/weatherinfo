var weatherInfo = (function () {
    var _weatherInfo = weatherInfo || {};

    _weatherInfo.geoLocator = function () {
        this.hasGeoLocation = navigator && navigator.geolocation;
        this.location = 0;
        this.errorMessage = "";
        this.City = $("#City");
        this.StateCode = $("#StateCode");
        this.CountryCode = $("#CountryCode");
    };

    _weatherInfo.geoLocator.prototype.getCurrentLocation =
        function () {
            if (this.hasGeoLocation) {
                navigator.geolocation.getCurrentPosition(this.getCurrentLocationSuccess.bind(this), this.getCurrentLocationError.bind(this));
            }
        };

    _weatherInfo.geoLocator.prototype.getCurrentLocationSuccess =
        function (position) {
            this.location = position;
            this.setAddress();
            this.setWeather();
        };

    _weatherInfo.geoLocator.prototype.getCurrentLocationError =
        function (msg) {
            this.errorMessage = msg;
        };

        // TODO: this code has a dependency on Google maps api .... need to segregate this in case we need to disable or switch out
    _weatherInfo.geoLocator.prototype.setAddress =
        function () {

            var geocoder = new google.maps.Geocoder();

            var location = new google.maps.LatLng(this.location.coords.latitude, this.location.coords.longitude);

            geocoder.geocode({ 'latLng': location }, this.setAddressCallback.bind(this));
        };

    _weatherInfo.geoLocator.prototype.setAddressCallback =
        function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                if (results && results[0]) {
                    var addressInfo = results[0].address_components;
                    var addressComponentsCount = addressInfo.length;
                    for (var i = 0; i < addressComponentsCount; i++) {
                        var longName = addressInfo[i].long_name;
                        var shortName = addressInfo[i].short_name;
                        switch (addressInfo[i].types[0]) {
                            case "locality":
                                this.City.val(longName);
                                break;
                            case "administrative_area_level_1":
                                this.StateCode.val(shortName);
                                break;
                            case "country":
                                this.CountryCode.val(shortName);
                                break;
                        }
                    }
                }
            }
        };

    _weatherInfo.geoLocator.prototype.setWeather =
        function () {
            weatherHelper = new _weatherInfo.weatherHelper(this.location.coords.latitude, this.location.coords.longitude);
            weatherHelper.getCurrentWeather();
        };

    _weatherInfo.weatherHelper = function (latitude, longitude) {
        this.latitude = latitude;
        this.longitude = longitude;
        this.currentTemperature = "";
        this.currentUnitType = "";
    };

    _weatherInfo.weatherHelper.prototype.getCurrentWeather =
        function () {
            var latitude = this.latitude;
            var longitude = this.longitude;

            $.ajax({
                url: "/api/Weather",
                type: "GET",
                data: { latitude: latitude, longitude: longitude, },
                context: $(this),
                success: this.getGetCurrentWeatherSuccess.bind(this),
                error: _weatherInfo.AjaxCallbackErrorHandler,
            });
        };

    _weatherInfo.weatherHelper.prototype.getGetCurrentWeatherSuccess =
        function (result) {
            if (result) {
                this.currentTemperature = result.MainItems[0].CurrentTemperature;
                this.currentUnitType = result.UnitType;

                // TODO: this has to go away
                $("#currentLocationTemperature").text(this.currentTemperature);
                $("#currentLocationUnitType").text(this.currentUnitType);
            }
        };

    return _weatherInfo;
}());
