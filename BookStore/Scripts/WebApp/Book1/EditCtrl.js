var app = angular.module('app', []);
app.run(['$http', '$window', function ($http, $window) {
    $http.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest';
    $http.defaults.headers.common['__RequestVerificationToken'] = $('input[name=__RequestVerificationToken]').val();
}]);
app.service('appService', ['$http', function ($http) {
    this.EditBook = function (o) {
        return $http.post("Book1/EditBook", o);
    };
}]);
app.controller('EditCtrl', ['$scope', '$window', 'appService', function ($scope, $window, appService) {
    $scope.Book = {};
    appService.EditBook({ id: $window.bookid})
        .then(function (ret) {
            $scope.Book = ret.data;
        });
    }

    //appService.EditBook({ id: $window.id })
    //    .then(function (ret) {
    //        $scope.Book = ret.data;
    //    });
}]);