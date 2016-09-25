angular.module('Login').factory('LoginServices', ['$http',
    function ($http) {
        var service = {};
        service.Login = function (usuario, callback) {
            $http.post('http://' + location.host + '/Directorio/DirectorioService.svc/json/Login',
                    { Request: usuario }
                ).success(function (response) {
                    callback(response);
                });
        };
        return service;
    }]);