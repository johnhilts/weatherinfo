var weatherModule = angular.module("weather-main");

weatherModule.factory("commonService", function ($http, $q) {

    var _updateQueryTimeText = { text: '', };

    var _getQueryTimeText = function (queryTime) {
        var d = $q.defer();

        $http({
            method: 'GET',
            url: '/api/Common/QueryTimeText',
            params: { queryTime: queryTime, },
        })
        .then(function (r) { _updateQueryTimeText.text = r.data; d.resolve(); }, function () { d.reject(); });

        return d.promise;
    };

    return {
        getQueryTimeText: _getQueryTimeText,
        updateQueryTimeText: _updateQueryTimeText,
    };

});
