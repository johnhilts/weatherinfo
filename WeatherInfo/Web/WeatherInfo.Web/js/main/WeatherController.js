﻿angular.module("weather-main", [])
    .controller('weatherController', function ($scope, locationService, weatherService) {
        // TODO: get stuff from the server on page-load

        // TODO: pass a list of lat/long's from the server
        $scope.init = function () {
            $scope.city = "locating ...";
        };

        $scope.getCurrentLocation = function () {

            $scope.errorMessages = [];

            locationService.getCurrentLocation()
                .then(
                    function () {
                        $scope.errorMessages = locationService.errorMessages;
                        var locationData = locationService.locationData.pop();
                        $scope.setLocation(locationData);
                        $scope.getCurrentWeather(locationData);
                    },
                    function () {
                        alert("location failed");
                    }
                );

        }

        $scope.setLocation = function (locationData) {
            $scope.city = locationData.city;
            $scope.state = ", " + locationData.stateCode;
            $scope.country = locationData.countryCode;
        }

        $scope.getCurrentWeather = function (locationData) {

            $scope.errorMessages = [];

            weatherService.getCurrentWeather(locationData.latitude, locationData.longitude)
                .then(
                    function () {
                        $scope.errorMessages = weatherService.errorMessages;
                        var weatherData = weatherService.weatherData.pop();
                        $scope.setWeather(weatherData);
                    },
                    function () {
                        alert("weather failed");
                    }
                );

        };

        $scope.setWeather = function (weatherData) {
            $scope.currentLocationTemperature = weatherData.currentTemperature;
            $scope.currentLocationUnitType = weatherData.currentUnitType;
        }

        angular.element(document).ready(function () {

            $scope.getCurrentLocation();

        });
    });
