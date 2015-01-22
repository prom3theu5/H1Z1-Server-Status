var app = angular.module("h1z1", []);
app.controller("MainController", function($scope, $http) {
    $http.get('http://prom3theu5api.azurewebsites.net/api/Services/GetServerStatuses').
    success(function(data, status, headers, config) {
      $scope.Servers = data;
    }).
    error(function(data, status, headers, config) {
      console.log(data);
    });
});