﻿var app = angular.module('app', []);
app.run(['$http', '$window', function ($http, $window) {
    $http.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest';
    $http.defaults.headers.common['__RequestVerificationToken'] = $('input[name=__RequestVerificationToken]').val();
}]);
app.service('appService', ['$http', function ($http) {
    this.CreateBook = function (o) {
        return $http.post("Book1/CreateBook", o);
    };
app.controller('CreateCtrl', ['$scope', '$window', 'appService', function ($scope, $window, appService) {
    $scope.Book = {};
    appService.CreateBook({ id: $window.bookid})
        .then(function (ret) {
            $scope.Book = ret.data;
        });
}]);