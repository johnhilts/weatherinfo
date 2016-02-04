weatherInfo.weatherApp
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
                        alert("Lat = " + locationData.latitude + "\r\nLon = " + locationData.longitude);
                        //$scope.setWeather(weatherData);
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