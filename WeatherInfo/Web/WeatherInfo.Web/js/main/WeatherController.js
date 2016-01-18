angular.module("weather-main", [])
    .controller('weatherController', function ($scope) {
        // TODO: get stuff from the server on page-load

        // TODO: pass a list of lat/long's from the server
        $scope.init = function () {
            $scope.city = "locating ...";
        };

        $scope.setLocation = function (geoLocator) {
            $scope.city = geoLocator.City;
            $scope.$apply(); // NOTE: this is necessary because assignment occurs as part of async callback
        }

        angular.element(document).ready(function () {
            
            var geoLocator;

            geoLocator = new weatherInfo.geoLocator();
            geoLocator.getCurrentLocation($scope.setLocation);

        });
    });
