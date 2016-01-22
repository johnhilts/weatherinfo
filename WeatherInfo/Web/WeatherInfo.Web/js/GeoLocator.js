var weatherInfo = (function () {
    var _weatherInfo = weatherInfo || {};

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

                // TODO: all this has to go away
                $("#currentLocationTemperature").text(this.currentTemperature);
                $("#currentLocationUnitType").text(this.currentUnitType);
            }
        };

    return _weatherInfo;
}());
