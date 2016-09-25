angular.module('Administrador').controller('SystemController', ['$scope', '$mdDialog', '$rootScope', '$location', '$cookieStore', 'SystemServices',
function ($scope, $mdDialog, $rootScope, $location, $cookieStore, SystemServices) {
    $scope.openMenu = function ($mdOpenMenu, ev) {
        originatorEv = ev;
        $mdOpenMenu(ev);
    };
}]);