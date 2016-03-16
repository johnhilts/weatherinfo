weatherInfo.weatherApp
    .directive("locationTemperature", function () {
        var linkFunction = function (scope, element, attributes) {
            var setWeather = function (weatherData) {
                scope.temperature = weatherData.currentTemperature;
                scope.$parent.locationData.temperatureTimeText = weatherData.temperatureTimeText;
                scope.$parent.locationData.weatherQueryTime = weatherData.weatherQueryTime;
            };
            var locationData = { 'latitude': attributes["latitude"], 'longitude': attributes["longitude"] };
            scope.temperature = '...';
            scope.getWeather({ 'locationData': locationData, 'successCallback': setWeather });
        };
        return {
            restrict: 'E',
            template: '{{temperature}}', 
            link: linkFunction,
            scope: { 'getWeather': '&', },
        };
    })

    .controller('weatherController', function ($scope, locationService, weatherService) {
        $scope.init = function () {
            var previousUnitType = $scope.getPreviousUnitType("current");
            if (previousUnitType) {
                $scope.unitType = $scope.getPreviousUnitType("current");
            }
            else {
                $scope.unitType = "F";
            }
        };

        $scope.getWeather = function (locationData, successCallback) {

            $scope.errorMessages = [];

            if (!locationData.latitude || !locationData.longitude) {
                return;
            }

            weatherService.getCurrentWeather(locationData.latitude, locationData.longitude)
                .then(
                    function () {
                        $scope.errorMessages = weatherService.errorMessages;
                        var weatherData = weatherService.weatherData.pop();
                        if (successCallback) {
                            successCallback(weatherData);
                        }
                    },
                    function () {
                        alert("weather failed");
                    }
                );

        };

        $scope.getPreviousUnitType = function (key) {

            if (!weatherInfo.hasLocalStorage()) {
                return '';
            }

            var previousUnitType = localStorage.getItem(key);
            if (previousUnitType && previousUnitType !== "undefined") {
                return previousUnitType;
            }
            else {
                return '';
            }
        }

        $scope.setPreviousUnitType = function (key, unitType) {

            if (!weatherInfo.hasLocalStorage()) {
                return;
            }

            localStorage.setItem(key, unitType);
        }

        angular.element(document).ready(function () {

            var setShowMoreButton = function () {
                $scope.showGetMore = $(window).scrollTop() == 0; // TODO: is there a good way to detect a scroll bar?
            };

            $scope.getCurrentLocation();
            $scope.getLocations(setShowMoreButton);

            $(window).scroll(function () {
                if ($(window).scrollTop() + $(window).height() == $(document).height()) {
                    $scope.getLocations(setShowMoreButton);
                }
            });

        });
    });
