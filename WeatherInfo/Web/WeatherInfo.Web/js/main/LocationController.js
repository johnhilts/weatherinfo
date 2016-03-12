weatherInfo.weatherApp
    .directive("addLocation", function () {
        return {
            restrict: 'E',
            templateUrl: '/Templates/Location/Add.html', 
        };
    })

    .controller('locationController', function ($scope, $interval, locationService, weatherService, commonService) {

        $scope.init = function () {
            $scope.showModal = false;
            $scope.address = "";
            $scope.city = "locating ...";

            $scope.locations = [];
            //$scope.locations.push({ city: 'Burbank, ', state: 'CA', country: 'US', temperature: 69.1, unitType: 'F', });

            $scope.addSuccess = false;
            $scope.addFail = false;

            $scope.indexes = { previousSortIndex: -1, };
            $scope.showGetMore = false;
            $scope.isLoading = false;

            $interval($scope.updateQueryTimeText, 10000);
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
            $scope.addLocationAlertReset();
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

            $scope.startFeedback();

            $scope.errorMessages = [];

            locationService.getLocations($scope.indexes.previousSortIndex)
                .then(
                    function () {
                        $scope.errorMessages = locationService.errorMessages;
                        var locationDataList = locationService.locationData.pop();
                        for (i = 0; i < locationDataList.length; i++) {
                            $scope.setLocations(locationDataList[i]);
                        }
                        if (successCallback) {
                            successCallback(locationDataList);
                        }
                        $scope.endFeedback();
                    },
                    function () {
                        alert("location failed");
                        $scope.endFeedback();
                    }
                );

        }

        $scope.setLocations = function (locationData) { // HACK
            // TODO: change to locations.push(location) - we will need to make the property names the same
            $scope.locations.push({ inputName: locationData.InputName, city: locationData.City, state: locationData.StateCode, country: locationData.CountryCode, latitude: locationData.Latitude, longitude: locationData.Longitude, sortOrder: locationData.sortOrder, temperatureTimeText: "...", });
            if ($scope.indexes.previousSortIndex == -1 || locationData.SortOrder < $scope.indexes.previousSortIndex) {
                $scope.indexes.previousSortIndex = locationData.SortOrder;
            }
        }

        $scope.setLocation = function (locationData) {
            // TODO: change to locations.push(location) - we will need to make the property names the same
            $scope.locations.splice(0, 0, { inputName: locationData.inputName, city: locationData.city, state: locationData.stateCode, country: locationData.countryCode, latitude: locationData.latitude, longitude: locationData.longitude, });
        }

        $scope.addLocation = function (locationData) {

            $scope.errorMessages = [];

            locationService.addLocation(locationData)
                .then(
                    function () {
                        $scope.errorMessages = locationService.errorMessages;
                        $scope.addSuccess = locationService.response.success;
                        $interval($scope.addLocationAlertReset, 2000, 1);
                    },
                    function () {
                        $scope.addFail = !locationService.response.success;
                    }
                );

        };

        $scope.addLocationAlertReset = function () {
            $scope.addSuccess = false;
            $scope.addFail = false;
        }

        $scope.updateQueryTimeText = function () {
            for (var i = 0; i < $scope.locations.length; i++) {
                (function (i) {
                    commonService.getQueryTimeText($scope.locations[i].weatherQueryTime)
                    .then(
                        function () { 
                            $scope.locations[i].temperatureTimeText = commonService.updateQueryTimeText.text;
                        },
                        function () {
                            // don't do anything ... it's just a text update, not important if it fails
                        }
                    );
                })(i);
            }
        };

        $scope.startFeedback =
            function () {
                $scope.isLoading = true;
                //$scope.$apply();
            };

        $scope.endFeedback =
            function () {
                $scope.isLoading = false;
            };

        // TODO: the logic for this needs to go into main JS
        $scope.isIos =
            function () {
                return (navigator.userAgent.match(/(iPad|iPhone|iPod)/g) ? true : false);
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