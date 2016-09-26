angular.module('Administrador').controller('SystemController', ['$scope', '$mdDialog', '$rootScope', '$location', '$cookieStore', 'SystemServices',
function ($scope, $mdDialog, $rootScope, $location, $cookieStore, SystemServices) {
    $scope.openMenu = function ($mdOpenMenu, ev) {
        originatorEv = ev;
        $mdOpenMenu(ev);
    };
    $scope.SafeEmpleados = [];
    $scope.Empleados = [];
    $scope.LoadEmpleados = function () {
        $rootScope.dataLoading = true;
        SystemServices.ConsultaEmpleados(function (response) {
            $rootScope.dataLoading = false;
            $scope.Empleados = response.ConsultaEmpleadosResult.Empleados;
            $scope.SafeEmpleados = response.ConsultaEmpleadosResult.Empleados;
        });
    };
    $scope.LoadEmpleados();
    $scope.itemsByPage = 5;
    $scope.AgregarEmpleado = function (evt, empleado) {
        $mdDialog.show({
            locals: { Usuario: $rootScope.globals.currentUser, Services: SystemServices, empleado:empleado },
            controller: DialogController,
            templateUrl: 'Presentation/Home/Empleado/Empleado.html',
            parent: angular.element(document.body),
            targetEvent: evt,
            clickOutsideToClose: false,
            fullscreen: $scope.customFullscreen // Only for -xs, -sm breakpoints.
        })
        .then(function (data) {
            $scope.LoadEmpleados();
            alert(data.Message);
        }, function () {

        });
    };
    function DialogController($scope, $mdDialog, Usuario, Services, empleado) {
        var Empleados = [];
        var DivisionesTodas = [];
        $scope.empleados = [];
        $scope.ubicaciones = [];
        $scope.areas = [];
        $scope.divisiones = [];
        $scope.loadEmpleados = function () {
            Services.CatalogoEmpleados(Usuario.Token, function (response) {
                $scope.empleados = response.CatalogoEmpleadosResult.Lista;
            });
        }
        $scope.loadUbicaciones = function () {
            Services.CatalogoUbicaciones(Usuario.Token, function (response) {
                $scope.ubicaciones = response.CatalogoUbicacionesResult.Lista;
            });
        }
        $scope.loadAreas = function () {
            Services.CatalogoAreas(Usuario.Token, function (response) {
                $scope.areas = response.CatalogoAreasResult.Lista;
            });
        }
        $scope.loadDivisiones = function () {
            Services.CatalogoDivisiones(Usuario.Token, function (response) {
                DivisionesTodas = response.CatalogoDivisionesResult.Lista;
            });
        }
        $scope.loadEmpleados();
        $scope.loadUbicaciones();
        $scope.loadAreas();
        $scope.loadDivisiones();
        $scope.querySearch = function (query) {
            Empleados = [];
            for (i = 0; i < $scope.empleados.length; i++) {
                if ($scope.empleados[i].descripcion.includes(query)) {
                    Empleados.push($scope.empleados[i]);
                }
            }
            return Empleados;
        }
        $scope.Empleado = {
            Nombre: '',
            Extension:'',
            Puesto: '',
            Departamento: {},
            Division: {},
            Ubicacion: {},
            Correo: '',
            Celular: '',
            NumeroDirecto: '',
            Foto: '',
            Activo: true,
            AUDUSUARIO: Usuario.Id
        };

        $scope.Initialize = function () {
            if (empleado) {
                $scope.Empleado = empleado;
            }
        };
        $scope.Initialize();
        $scope.selectedItemChange = selectedItemChange
        function selectedItemChange(item) {
            $scope.Empleado.Nombre = item.descripcion;
            $scope.Empleado.Puesto = item.codigo;
        }
        $scope.DivisionesDisponibles = function () {
            $scope.divisiones = [];
            for (i = 0; i < DivisionesTodas.length; i++) {
                if (DivisionesTodas[i].codigo == $scope.Empleado.Departamento.id) {
                    $scope.divisiones.push(DivisionesTodas[i]);
                }
            }
            return $scope.divisiones;
        };
        $scope.hide = function () {
            $mdDialog.hide();
        };
        $scope.cancel = function () {
            $mdDialog.cancel();
        };
        $scope.FotoLoaded = false;
        $scope.save = function (empleado) {
            var request = { entidad: empleado, Token: Usuario.Token };
            SystemServices.AgregarEmpleado(request, document.getElementById("Foto").files[0], function (response) {
                $rootScope.dataLoading = true;
                if (response.AgregarEmpleadoResult.Success) {
                    $mdDialog.hide(response.AgregarEmpleadoResult);
                    $rootScope.dataLoading = false;
                } else {
                    alert(response.AgregarEmpleadoResult.Message);
                    $rootScope.dataLoading = false;
                }
            });
            
        };
    }
}]);