angular.module("formularioEasy").config(function ($routeProvider) {
	$routeProvider.when("/employees", {
		templateUrl: "Views/Home/Employees.html",
        controller: "formularioEasyCtrl",
	});
	$routeProvider.when("/newEmployee", {
        templateUrl: "NewEmployee.html",
        controller: "newEmployeeController",
        resolve: {
            employee: function() { return {}; }
        }
	});
    $routeProvider.when("/editEmployee/:id", {
        templateUrl: "NewEmployee.html",
        controller: "newEmployeeController",
        resolve: {
			employee: function ($route, conectionAPI) {
                return conectionAPI.getEmployee($route.current.params.id);
			}
		}
	});
	$routeProvider.otherwise({redirectTo: "/employees"});
});