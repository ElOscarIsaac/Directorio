angular.module('Login', ['ngMaterial', 'ngMessages']);
angular.module('Administrador', ['ngMaterial', 'ngRoute', 'ngCookies', 'Login', 'smart-table', 'ui.bootstrap'])
.config(['$routeProvider', '$httpProvider', function ($routeProvider, $httpProvider) {
    $routeProvider
        .when('/login', {
            templateUrl: 'Presentation/Login/Login.html',
            controller: 'LoginController'
        })
        .when('/', {
            templateUrl: 'Presentation/Home/Home.html',
            controller: 'SystemController'
        })
        .otherwise({ redirectTo: '/login' });
    delete $httpProvider.defaults.headers.common['X-Requested-With'];
}])
.run(['$rootScope', '$location', '$cookieStore', '$http',
    function ($rootScope, $location, $cookieStore, $http) {
        // keep user logged in after page refresh
        $rootScope.logeado = false;
        $rootScope.globals = $cookieStore.get('globals') || {};
        if ($rootScope.globals.currentUser) {
            $rootScope.logeado = true;
        }
        $rootScope.$on('$locationChangeStart', function (event, next, current) {
            // redirect to login page if not logged in
            if ($location.path() !== '/login' && !$rootScope.globals.currentUser) {
                $location.path('/login');
            }
        })
    }]);