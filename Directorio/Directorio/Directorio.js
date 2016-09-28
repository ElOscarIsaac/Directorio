angular.module('Directorio', ['ngMaterial']);
angular.module('Directorio').config(['$mdThemingProvider',
        function ($mdThemingProvider) {
            var tealFuji = $mdThemingProvider.extendPalette('teal', {
                '500': '#01916D',
                'A100': '#01916D',
                'contrastDefaultColor': 'light'
            });
            $mdThemingProvider.definePalette('FujiTheme', tealFuji);
            $mdThemingProvider.theme('default')
              .primaryPalette('FujiTheme')
              .backgroundPalette('grey', {
                  'default': '200'
              })
        }])
angular.module('Directorio').factory('DirectorioService', ['$http',
    function ($http) {
        var service = {};
        service.ConsultaEmpleados = function (callback) {
            $http.post('http://' + location.host + '/Directorio/DirectorioService.svc/json/ConsultaEmpleados'
                ).success(function (response) {
                    callback(response);
                });
        };
        return service;
    }
]);
angular.module('Directorio').controller('aplicacion', ['$scope', 'DirectorioService', function ($scope, DirectorioService) {
    $scope.nombre = '';
    //data:image/jpeg;base64,
    $scope.ListaCompletaColaboradores = [];
    $scope.CargarLista = function () {
        DirectorioService.ConsultaEmpleados(function (response) {
            $scope.ListaCompletaColaboradores = response.ConsultaEmpleadosResult.Empleados;
            $scope.BuscarColaborador('');
        });
    };
    $scope.CargarLista();
    $scope.colaboradores = [];
    $scope.BuscarColaborador = function (nombre) {
        $scope.colaboradores = [];
        for (i = 0; i < $scope.ListaCompletaColaboradores.length; i++) {
            if ($scope.ListaCompletaColaboradores[i].Nombre.toLowerCase().includes(nombre.toLowerCase()))
                $scope.colaboradores.push($scope.ListaCompletaColaboradores[i]);
        }
    };
    $scope.ObtenerFoto = function(empleado){
        if (empleado.Foto.length > 0)
            return 'data:image/jpeg;base64,' + empleado.Foto;
        else
            return 'data:image/png;base64,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';
    }
}]);