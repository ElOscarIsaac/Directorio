angular.module('Login')

.controller('LoginController',
    ['$scope', '$mdDialog', '$rootScope', '$location', '$cookieStore', 'LoginServices',
    function ($scope, $mdDialog, $rootScope, $location, $cookieStore, LoginServices) {
        $scope.logeando = false;
        var clearGlobals = function () {
            $rootScope.globals = {};
            $cookieStore.remove('globals');
            $rootScope.dataLoading = false;
            $rootScope.logeado = false;
        };
        clearGlobals();
        var setGlobals = function (data) {
            $rootScope.globals.currentUser = data;
            $cookieStore.put('globals', $rootScope.globals);
        };
        var ShowDialog = function (message, ev) {
            $mdDialog.show(
                $mdDialog.alert()
                .parent(angular.element(document.querySelector('#MainContainer')))
                .clickOutsideToClose(false)
                .title('Mensaje de sistema')
                .textContent(message)
                .ariaLabel('Mensaje')
                .ok('Aceptar')
                .targetEvent(ev)
            );
        };
        $scope.Ingresar = function (data, event) {
            $rootScope.dataLoading = true;
            $scope.logeando = true;
            LoginServices.Login(data, function (response) {
                if (response.LoginResult.Id > 0) {
                    setGlobals(response.LoginResult);
                    $rootScope.logeado = true;
                    $location.path('/');
                }
                else {
                    ShowDialog('Usuario o contraseña incorrectos', event);
                }
                $rootScope.dataLoading = false;
                $scope.logeando = false;
            });
        }
        $scope.RecuperarCredenciales = function (evt) {
            location.href = 'http://' + location.host + '/Directorio/Presentation/Recuperacion/RecoverCredentials.html';
        };
    }]);