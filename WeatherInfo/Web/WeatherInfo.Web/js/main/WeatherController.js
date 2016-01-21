angular.module("weather-main", [])
    .controller('weatherController', function ($scope, locationService) {
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
                            $scope.setLocation(locationService.locationData);
                        },
                        function () {
                            alert("failed");
                        }
                    );

        }

        $scope.setLocation = function (locationData) {
            $scope.city = locationData.city;
            $scope.state = ", " + locationData.stateCode;
            $scope.country = locationData.countryCode;

            weatherHelper = new weatherInfo.weatherHelper(locationData.latitude, locationData.longitude);
            weatherHelper.getCurrentWeather();
        }

        angular.element(document).ready(function () {
            
            $scope.getCurrentLocation();

        });
    });
