'use strict';
var myApp = angular.module('myApp', ['ngRoute']);

/*
AngularJS GitHub Pull #14202 Changed default hashPrefix to '!' 从 1.6+版本以后。。。坑死了
stackoverflow https://stackoverflow.com/questions/41211875/angularjs-1-6-0-latest-now-routes-not-working
或者采用此写法 href="#!/about"
*/
myApp.config(['$locationProvider', function ($locationProvider) {
    $locationProvider.hashPrefix('');
}]);

//配置路由  
myApp.config(["$routeProvider", function ($routeProvider) {

    $routeProvider

      //home  
      .when('/', {
          templateUrl: '/routeDemo/home.html',
          controller: 'mainController'
      })

      //about  
      .when('/about', {
          templateUrl: '/routeDemo/about.html',
          controller: 'aboutController'
      })

      //contact  
      .when('/contact/:id/:name', {
          templateUrl: '/routeDemo/contact.html',
          controller: 'contactController'
      });

}]);

//main控制器  
myApp.controller('mainController', ["$scope", function ($scope) {
    // create a message to display in our view  
    $scope.message = 'Everyone come and see how good I look!';
}]);
//about控制器  
myApp.controller('aboutController', ["$scope", function ($scope) {
    $scope.message = 'Look! I am an about page.';
}]);
//contact控制器  
myApp.controller('contactController', ["$scope", "$routeParams", function ($scope, $routeParams) {
    $scope.id = $routeParams.id;
    $scope.name = $routeParams.name;
    $scope.message = 'Contact us! JK. This is just a demo.';
}]);