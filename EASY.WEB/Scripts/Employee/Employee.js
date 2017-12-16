var formApp = angular.module("formularioEasy", ["ngRoute"]);

formApp.controller("formularioEasyCtrl", function ($scope, $location, conectionAPI) {
    $scope.employees = conectionAPI.getEmployees();

    //console.log($location.url());

    $scope.employees.forEach(function (obj) {
        obj.selected = false;
    });

    $scope.app = "Cadastro de Programadores da Easy Comtec";

    $scope.canEdit = function (employees) {
        $scope.hasSelected = employees.some(function (employee) {
            return employee.selected;
        });

        $scope.hasOneSelected = employees.filter(function (employee) {
            return employee.selected;
        }).length === 1;
    };

    $scope.callEdit = function (employees) {
        $location.path("/editEmployee/" + employees.filter(function (employee) {
            return employee.selected;
        })[0].id);
        employees.forEach(function (employee) {
            employee.selected = false;
        });
    }

    $scope.deleteEmployees = function (employees) {
        employees.forEach(function (employee) {
            if (employee.selected)
                conectionAPI.deleteEmployee(employee.id);
        });

        $scope.employees = conectionAPI.getEmployees();

        $scope.canEdit($scope.employees);
    };

    $scope.selectRow = function ($event, employee) {
        employee.selected = !employee.selected;

        $scope.canEdit($scope.employees);
    };
});

formApp.controller("newEmployeeController", function ($scope, $location, $routeParams, conectionAPI, employee) {
	$scope.employee = employee;
    
    $scope.knowledges = conectionAPI.getKnowledges();
    
    if (employee.knowledges) {
        employee.knowledges.forEach(function(knowledge) {
            for (var i = 0; i < $scope.knowledges.length; i++) {
                if (knowledge.id == $scope.knowledges[i].id)
                    $scope.knowledges[i].level = knowledge.level;
            }
        });
    }
    
    $scope.stage = 1;
    
    $scope.foward = function() {
        $scope.stage++;
    };
    
    $scope.back = function() {
        $scope.stage--;
    };
        
    $scope.addEmployee = function (employee) {
        employee.knowledges = [];
        $scope.knowledges.forEach(function (knowledge) {
            if(knowledge)
                employee.knowledges.push(knowledge);
        });
        $scope.employees = conectionAPI.saveEmployee(employee);
        $location.path("/employees");
        delete $scope.employee;
        $scope.newEmployeeOccupation.$setPristine();
        $scope.newEmployeeBankInformation.$setPristine();
        $scope.newEmployeeKnowledges.$setPristine();
	};
    
    $scope.workingHour = {};
    $scope.workingHour.choices = [];
    $scope.workingHour.validate = function ($event) {
        value = $event.currentTarget.getAttribute("ng-model");
        if ($scope.workingHour.choices.indexOf(value) == -1){
            $scope.workingHour.choices.push(value);
        }
        else {
            $scope.workingHour.choices.splice($scope.workingHour.choices.indexOf(value), 1);
        }
    }
    if (employee.workingHour) {
        if (employee.workingHour.morning)
            $scope.workingHour.choices.push("employee.workingHour.morning");
        if (employee.workingHour.afternoon)
            $scope.workingHour.choices.push("employee.workingHour.afternoon");
        if (employee.workingHour.night)
            $scope.workingHour.choices.push("employee.workingHour.night");
        if (employee.workingHour.dawn)
            $scope.workingHour.choices.push("employee.workingHour.dawn");
        if (employee.workingHour.business)
            $scope.workingHour.choices.push("employee.workingHour.business");
    }
    $scope.availability = {};
    $scope.availability.choices = [];
    $scope.availability.validate = function ($event) {
        value = $event.currentTarget.getAttribute("ng-model");
        if ($scope.availability.choices.indexOf(value) == -1){
            $scope.availability.choices.push(value);
        }
        else {
            $scope.availability.choices.splice($scope.availability.choices.indexOf(value), 1);
        }
    }
    if (employee.availability) {
        if (employee.availability.until4Hours)
            $scope.availability.choices.push("employee.availability.until4Hours");
        if (employee.availability.from4To6Hours)
            $scope.availability.choices.push("employee.availability.from4To6Hours");
        if (employee.availability.from6To8Hours)
            $scope.availability.choices.push("employee.availability.from6To8Hours");
        if (employee.availability.upto8Hours)
            $scope.availability.choices.push("employee.availability.upto8Hours");
    }
});