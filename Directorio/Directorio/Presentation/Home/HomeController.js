angular.module('Administrador').controller('SystemController', ['$scope', '$mdDialog', '$rootScope', '$location', '$cookieStore', 'SystemServices',
function ($scope, $mdDialog, $rootScope, $location, $cookieStore, SystemServices) {
    $scope.openMenu = function ($mdOpenMenu, ev) {
        originatorEv = ev;
        $mdOpenMenu(ev);
    };
    $scope.Empleados = [];
    $scope.LoadEmpleados = function () {
        $rootScope.dataLoading = true;
        SystemServices.ConsultaEmpleados(function (response) {
            $rootScope.dataLoading = false;
            $scope.Empleados = response.ConsultaEmpleadosResult.Empleados;
            $scope.groupToPages($scope.Empleados);
        });
    };
    $scope.LoadEmpleados();
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
            $scope.showAlert(data.Message);
        }, function () {

        });
    };
    function DialogController($scope, $mdDialog, Usuario, Services, empleado) {
        var Empleados = [];
        var DivisionesTodas = [];
        $scope.loading = false;
        $scope.empleados = [];
        $scope.ubicaciones = [];
        $scope.areas = [];
        $scope.divisiones = [];
        $scope.loadEmpleados = function () {
            $scope.loading = true;
            Services.CatalogoEmpleados(Usuario.Token, function (response) {
                $scope.empleados = response.CatalogoEmpleadosResult.Lista;
                $scope.loadUbicaciones();
            });
        }
        $scope.loadUbicaciones = function () {
            Services.CatalogoUbicaciones(Usuario.Token, function (response) {
                $scope.ubicaciones = response.CatalogoUbicacionesResult.Lista;
                $scope.loadAreas();
            });
        }
        $scope.loadAreas = function () {
            Services.CatalogoAreas(Usuario.Token, function (response) {
                $scope.areas = response.CatalogoAreasResult.Lista;
                $scope.loadDivisiones();
            });
        }
        $scope.loadDivisiones = function () {
            Services.CatalogoDivisiones(Usuario.Token, function (response) {
                DivisionesTodas = response.CatalogoDivisionesResult.Lista;
                $scope.Initialize();
            });
        }
        $scope.loadEmpleados();
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
        $scope.Actualizar = false;
        $scope.EmpleadoSeleccionado = {  };
        
        $scope.Initialize = function () {
            $scope.loading = false;
            if (empleado) {
                $scope.Empleado = empleado;
                for (i = 0; i < $scope.empleados.length; i++) {
                    if ($scope.empleados[i].codigo == empleado.Puesto && $scope.empleados[i].descripcion == empleado.Nombre) {
                        $scope.EmpleadoSeleccionado = $scope.empleados[i];
                        break;
                    }
                }
                $scope.Actualizar = true;
            }
        };
        $scope.selectedItemChange = function selectedItemChange(item) {
            $scope.Empleado.Nombre = item == undefined ? '' : item.descripcion;
            $scope.Empleado.Puesto = item == undefined ? '' : item.codigo;
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
            if (!$scope.Actualizar) {
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
            } else {
                SystemServices.ActualizarEmpleado(request, document.getElementById("Foto").files[0], function (response) {
                    $rootScope.dataLoading = true;
                    if (response.ActualizarEmpleadoResult.Success) {
                        $mdDialog.hide(response.ActualizarEmpleadoResult);
                        $rootScope.dataLoading = false;
                    } else {
                        alert(response.ActualizarEmpleadoResult.Message);
                        $rootScope.dataLoading = false;
                    }
                });
            }
        };
    }

    $scope.BuscarEmpleado = function (empleado) {
        var lista = [];
        for (i = 0; i < $scope.Empleados.length; i++) {
            if ($scope.Empleados[i].Nombre.includes(empleado.toUpperCase()))
                lista.push($scope.Empleados[i]);
        }
        $scope.groupToPages(lista);
    };
    $scope.showAlert = function (Mensaje) {
        $mdDialog.show(
            $mdDialog.alert()
            .parent(angular.element(document.querySelector('#MainContainer')))
            .clickOutsideToClose(true)
            .title('Mensaje del sistema')
            .content(Mensaje)
            .ariaLabel('MENSAJE DIALOGO')
            .ok('Aceptar')
            .targetEvent()
        )
    };
    $scope.showConfirmOnDelete = function (ev, empleado) {
        var confirm = $mdDialog.confirm()
              .title('Eliminar empleado')
              .textContent('¿Desea eliminar al empleado seleccionado?')
              .ariaLabel('Eliminar empleado')
              .targetEvent(ev)
              .ok('Aceptar')
              .cancel('Cancelar');
        $mdDialog.show(confirm).then(function () {
            DeleteRequest = { EmpleadoId: empleado.Id, Token: $rootScope.globals.currentUser.Token };
            SystemServices.EliminarEmpleado(DeleteRequest, function (response) {
                if (response.EliminarEmpleadoResult.Success)
                    $scope.LoadEmpleados();
                else
                    alert(response.EliminarEmpleadoResult.Message);
            });
        }, function () {
            //Nothing to do so...
        });
    };
    //Paginación de la tabla
    $scope.itemsPerPage = 10;
    $scope.pagedItems = [];
    $scope.currentPage = 0;
    $scope.groupToPages = function (lista) {
        $scope.pagedItems = [];
        for (var i = 0; i < lista.length; i++) {
            if (i % $scope.itemsPerPage === 0) {
                $scope.pagedItems[Math.floor(i / $scope.itemsPerPage)] = [lista[i]];
            } else {
                $scope.pagedItems[Math.floor(i / $scope.itemsPerPage)].push(lista[i]);
            }
        }
    };
    $scope.range = function (start, end) {
        var ret = [];
        if (!end) {
            end = start;
            start = 0;
        }
        for (var i = start; i < end; i++) {
            ret.push(i);
        }
        return ret;
    };
    $scope.prevPage = function () {
        if ($scope.currentPage > 0) {
            $scope.currentPage--;
        }
    };
    $scope.nextPage = function () {
        if ($scope.currentPage < $scope.pagedItems.length - 1) {
            $scope.currentPage++;
        }
    };
    $scope.setPage = function () {
        $scope.currentPage = this.n;
    };
    //---------------------
}]);