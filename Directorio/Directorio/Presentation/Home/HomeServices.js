angular.module('Administrador').factory('SystemServices', ['$http',
    function ($http) {
        var service = {};
        service.CatalogoEmpleados = function (data, callback) {
            $http.post('http://' + location.host + '/Directorio/DirectorioService.svc/json/CatalogoEmpleados',
                    { Request: { Token: data } }
                ).success(function (response) {
                    callback(response);
                });
        };
        service.CatalogoUbicaciones = function (data, callback) {
            $http.post('http://' + location.host + '/Directorio/DirectorioService.svc/json/CatalogoUbicaciones',
                    { Request: { Token: data } }
                ).success(function (response) {
                    callback(response);
                });
        };
        service.CatalogoAreas = function (data, callback) {
            $http.post('http://' + location.host + '/Directorio/DirectorioService.svc/json/CatalogoAreas',
                    { Request: { Token: data } }
                ).success(function (response) {
                    callback(response);
                });
        };
        service.CatalogoDivisiones = function (data, callback) {
            $http.post('http://' + location.host + '/Directorio/DirectorioService.svc/json/CatalogoDivisiones',
                    { Request: { Token: data } }
                ).success(function (response) {
                    callback(response);
                });
        };
        service.ConsultaEmpleados = function (callback) {
            $http.post('http://' + location.host + '/Directorio/DirectorioService.svc/json/ConsultaEmpleados'
                ).success(function (response) {
                    callback(response);
                });
        };
        service.AgregarEmpleado = function (data, Foto, callback) {
            var fd = new FormData();
            fd.append('request', JSON.stringify(data));
            fd.append('Foto', Foto);
            $http.post('http://' + location.host + '/Directorio/DirectorioService.svc/json/AgregarEmpleado',
                fd, { transofrmRequest: angular.identity, headers: { 'Content-Type': undefined } }).success(function (response) {
                    callback(response);
                });
        };
        return service;
    }]);