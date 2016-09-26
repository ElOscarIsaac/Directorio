angular.module('RecoverCredentials', ['ngMaterial', 'ngMessages'])
    .config(function ($mdThemingProvider) {
        $mdThemingProvider.theme('default')
            .primaryPalette('blue')
            .accentPalette('orange');
    });
angular.module('RecoverCredentials')
    .factory('RecoverCredentialsServices', ['$http',
        function ($http) {
            var service = {};
            service.RecoverCredentials = function (mail, callback) {
                $http.post('http://' + location.host + '/Directorio/DirectorioService.svc/json/RecoverCredentials',
                        { request: mail }
                    ).success(function (response) {
                        callback(response);
                    });
            };
            return service;
        }
    ]);
angular.module('RecoverCredentials')
    .controller('RecoverCredentialsController',
    ['$scope', '$mdDialog', '$rootScope', '$location', 'RecoverCredentialsServices',
    function ($scope, $mdDialog, $rootScope, $location, RecoverCredentialsServices) {
        $scope.RecuperarCredeneciales = function (mail, event) {
            $rootScope.dataLoading = true;
            try {
                RecoverCredentialsServices.RecoverCredentials(mail, function (response) {
                    try {
                        if (response.RecoverCredentialsResult) {
                            $rootScope.dataLoading = false;
                            ShowDialog('Se ha mandado un correo electrónico con sus credenciales', event);
                        }
                        else {
                            $rootScope.dataLoading = false;
                            ShowDialog('No fue posible enviar el correo electrónico, debido a que no está registrado', event);
                        }
                        $rootScope.dataLoading = false;
                    } catch (e) {
                        ShowDialog('Error en el sistema: ' + e, event);
                        $rootScope.dataLoading = false;
                    }
                });
            } catch (ex) {
                ShowDialog('Error en el sistema: ' + e, event);
                $rootScope.dataLoading = false;
            }
        };
    var ShowDialog = function (message, ev) {
        $mdDialog.show(
            $mdDialog.alert()
            .parent(angular.element(document.querySelector('#RecoverForm')))
            .clickOutsideToClose(false)
            .title('Mensaje de sistema')
            .textContent(message)
            .ariaLabel('Mensaje')
            .ok('Aceptar')
            .targetEvent(ev)
        ).then(function () {
            location.href = 'http://' + location.host + '/Directorio/';
        });
    };
}]);
