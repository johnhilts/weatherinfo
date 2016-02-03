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

    return _weatherInfo;
}());
