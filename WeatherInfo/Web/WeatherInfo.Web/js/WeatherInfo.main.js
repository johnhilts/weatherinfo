var weatherInfo = (function () {
    var _weatherInfo = weatherInfo || {};

    ///
    /// Ajax Callback (error)
    ///            
    _weatherInfo.AjaxCallbackErrorHandler =
        function (xhr, status, errorThrown) {
            if (xhr) {
                alert("an unexpected error occurred");
            }
        };

    _weatherInfo.hasLocalStorage =
        function () {
            return (typeof (Storage) !== "undefined");
        };

    _weatherInfo.weatherApp = angular.module("weather-main", ["ui.bootstrap.modal"]);

    var _isEditMode = false;

    _weatherInfo.setEditMode =
        function (isEditMode) {
            _isEditMode = isEditMode;
        };

    _weatherInfo.isEditMode =
        function () {
            return _isEditMode;
        };

    return _weatherInfo;
}());
