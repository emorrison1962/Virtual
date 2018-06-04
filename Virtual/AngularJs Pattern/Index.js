var myApp = angular.module("myApp", []);

var indexController = myApp.controller("indexController", ['$scope', '$window', '$log', '$http', '$location', function ($scope, $window, $log, $http, $location) {

    $scope.isBusy = true;

    $scope.recipes = [];
    $scope.visibleRecipes = [];
    $scope.newRecipe = '';
    $scope.filter = '';


    $scope.init = function (model) {
    	$scope.recipes = model;
    	$scope.visibleRecipes = model;
        $log.debug($scope.recipes);
    }

    $scope.insert = function () {
        $http.post("/Recipe/Insert", { url: newRecipe }).then(function(response) {
            $scope.status = response.status;
            $scope.data = response.data;
        }, function(response) {
            $scope.data = response.data || 'Request failed';
            $scope.status = response.status;
        });
    }

    $scope.update = function (recipe) {
        $http.get("/Recipe/Update", { recipe: recipe }).then(
        function (response) {
            $scope.status = response.status;
            $scope.data = response.data;
            $window.location.href("/Recipe/Update");
        }, function (response) {
            $scope.data = response.data || 'Request failed';
            $scope.status = response.status;
        });
    }

    $scope.filterChanged = function (model) {
    	$log.info('+filterChanged');
    	$scope.visibleRecipes = $scope.recipes.filter($scope.filterFunc);
    	$log.info('-filterChanged');
    }

    	
    $scope.filterFunc = function (recipe, index, arr) {
    	$log.info('+filterFunc');

    	var result = false;
    	if (-1 < recipe.Name.toLowerCase().search($scope.filter.toLowerCase()))
    		result = true;

    	$log.info('-filterFunc');
    	return result;
    }





}]);

