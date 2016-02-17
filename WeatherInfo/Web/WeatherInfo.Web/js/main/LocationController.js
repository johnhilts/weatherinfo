﻿weatherInfo.weatherApp
    .directive("addLocation", function () {
        return {
            restrict: 'E',
            templateUrl: '/Templates/Location/Add.html', 
        };
    })

    .controller('locationController', function ($scope, locationService, weatherService) {

        $scope.init = function () {
            $scope.showModal = false;
            $scope.address = "";
            $scope.city = "locating ...";

            $scope.locations = [];
            //$scope.locations.push({ city: 'Burbank, ', state: 'CA', country: 'US', temperature: 69.1, unitType: 'F', });

            $scope.isNgReady = false;
        };

        $scope.modalOpen = function () {
            $scope.showModal = true;
        };

        $scope.modalOk = function () {
            $scope.showModal = false;
        };

        $scope.modalCancel = function () {
            $scope.showModal = false;
        };

        $scope.showAddLocationForm = function () {
            $scope.modalOpen();
        };

        $scope.searchLocation = function () {

            $scope.errorMessages = [];

            locationService.searchLocation($scope.address)
                .then(
                    function () {
                        $scope.errorMessages = locationService.errorMessages;
                        var locationData = locationService.locationData.pop();
                        $scope.setLocation(locationData);
                        $scope.modalCancel();
                        $scope.addLocation(locationData);
                    },
                    function () {
                        alert("location add failed");
                    }
                );

        };

        $scope.getCurrentLocation = function (successCallback) {

            $scope.errorMessages = [];

            locationService.getCurrentLocation()
                .then(
                    function () {
                        $scope.errorMessages = locationService.errorMessages;
                        var locationData = locationService.locationData.pop();
                        $scope.setCurrentLocation(locationData);
                        if (successCallback) {
                            successCallback(locationData);
                        }
                    },
                    function () {
                        alert("location failed");
                    }
                );

        }

        $scope.setCurrentLocation = function (locationData) {
            $scope.inputName = locationData.inputName;
            $scope.city = locationData.city;
            $scope.state = locationData.stateCode;
            $scope.country = locationData.countryCode;
        }

        $scope.getLocations = function (successCallback) {

            $scope.errorMessages = [];

            locationService.getLocations()
                .then(
                    function () {
                        $scope.errorMessages = locationService.errorMessages;
                        var locationDataList = locationService.locationData.pop();
                        for (i = 0; i < locationDataList.length; i++) {
                            $scope.setLocations(locationDataList[i]);
                        }
                        $scope.isNgReady = true;
                        if (successCallback) {
                            successCallback(locationDataList);
                        }
                    },
                    function () {
                        alert("location failed");
                    }
                );

        }

        $scope.setLocations = function (locationData) { // HACK
            // TODO: change to locations.push(location) - we will need to make the property names the same
            $scope.locations.push({ inputName: locationData.InputName, city: locationData.City, state: locationData.StateCode, country: locationData.CountryCode, latitude: locationData.Latitude, longitude: locationData.Longitude, });
        }

        $scope.setLocation = function (locationData) {
            // TODO: change to locations.push(location) - we will need to make the property names the same
            $scope.locations.push({ inputName: locationData.inputName, city: locationData.city, state: locationData.stateCode, country: locationData.countryCode, latitude: locationData.latitude, longitude: locationData.longitude, });
        }

        $scope.addLocation = function (locationData) {

            $scope.errorMessages = [];

            locationService.addLocation(locationData)
                .then(
                    function () {
                        $scope.errorMessages = locationService.errorMessages;
                    },
                    function () {
                        alert("location add failed");
                    }
                );

        };

        /*
        $scope.addLocation = function (locationData) {

            $scope.errorMessages = [];

            locationService.addLocation(locationData.latitude, locationData.longitude)
                .then(
                    function () {
                        $scope.errorMessages = locationService.errorMessages;
                        //var weatherData = weatherService.weatherData.pop();
                        //$scope.setWeather(weatherData);
                    },
                    function () {
                        alert("location add failed");
                    }
                );

        };
        */

    });