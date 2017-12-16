var formApp = angular.module("formularioEasy", ["ngRoute"]);

formApp.controller("formularioEasyCtrl", function ($scope, $location, $window, conectionAPI) {
    $scope.title = "Banco de Talentos - Easy Communication & Technology (Developers/Desenvolvedores)";

    $scope.init = function (model, viewbag) {
        $scope.employees = model;
        if (viewbag.Message && viewbag.Type)
            $.notify({ message: viewbag.Message }, { type: viewbag.Type });
    };

    $scope.canEdit = function (employees) {
        $scope.hasSelected = employees.some(function (employee) {
            return employee.Selected;
        });

        $scope.hasOneSelected = employees.filter(function (employee) {
            return employee.Selected;
        }).length === 1;
    };

    $scope.newEmployee = function (employees) {
        $scope.clearSelect(employees);
        $scope.canEdit($scope.employees);
        conectionAPI.open("Employee");
    }

    $scope.callEdit = function (employees) {
        employeeSelected = employees.filter(function (employee) { return employee.Selected; })[0];

        $scope.clearSelect(employees);

        conectionAPI.open("Employee", employeeSelected.EmployeeID);
    }

    $scope.deleteEmployees = function (employees) {
        deleteUser = $window.confirm('Deseja Remover o(s) registro(s) selecionado(s)?');
        if (deleteUser) {
            toDelete = [];

            employees.forEach(function (employee) {
                if (employee.Selected)
                    toDelete.push(employee.EmployeeID);
            });

            conectionAPI.delete({ ToDelete: toDelete });
        }
    };

    $scope.selectRow = function ($event, employee) {
        employee.Selected = !employee.Selected;

        $scope.canEdit($scope.employees);
    };

    $scope.clearSelect = function (employees) {
        employees.forEach(function (employee) { employee.Selected = false; });
    }
});

formApp.controller("newEmployeeController", function ($scope, $location, conectionAPI) {
    $scope.init = function (model, viewbag) {
        $scope.employee = model;
        if ($scope.employee && $scope.employee.WorkingHour) {
            if ($scope.employee.WorkingHour.Morning)
                $scope.workingHour.choices.push("employee.WorkingHour.Morning");
            if ($scope.employee.WorkingHour.Afternoon)
                $scope.workingHour.choices.push("employee.WorkingHour.Afternoon");
            if ($scope.employee.WorkingHour.Night)
                $scope.workingHour.choices.push("employee.WorkingHour.Night");
            if ($scope.employee.WorkingHour.Dawn)
                $scope.workingHour.choices.push("employee.WorkingHour.Dawn");
            if ($scope.employee.WorkingHour.Business)
                $scope.workingHour.choices.push("employee.WorkingHour.Business");
        }
        if ($scope.employee && $scope.employee.Availability) {
            if ($scope.employee.Availability.Until4Hours)
                $scope.availability.choices.push("employee.Availability.Until4Hours");
            if ($scope.employee.Availability.From4To6Hours)
                $scope.availability.choices.push("employee.Availability.From4To6Hours");
            if ($scope.employee.Availability.From6To8Hours)
                $scope.availability.choices.push("employee.Availability.From6To8Hours");
            if ($scope.employee.Availability.Upto8Hours)
                $scope.availability.choices.push("employee.Availability.Upto8Hours");
        }

        if ($scope.employee) {
            conectionAPI.getData("GetKnowledgeXEmployee", { EmployeeID: $scope.employee.EmployeeID }).then(function (response) {
                $scope.employee.Knowledges = response.data;
                if ($scope.employee.Knowledges) {
                    $scope.employee.Knowledges.forEach(function (knowledge) {
                        if (knowledge.Knowledge.Custom) {
                            $scope.employee.otherKnowledge = knowledge;
                        } else {
                            for (var i = 0; i < $scope.knowledges.length; i++) {
                                if (knowledge.Knowledge.KnowledgeID == $scope.knowledges[i].KnowledgeID)
                                    $scope.knowledges[i].Level = knowledge.Level;
                            }
                        }
                    });
                }
            });

            if ($scope.employee.PaymentType == 0) {
                conectionAPI.getData("GetPaypalInfo", { EmployeeID: $scope.employee.EmployeeID }).then(function (response) {
                    $scope.employee.PaypalInfo = response.data;
                });
            }
            if ($scope.employee.PaymentType == 1) {
                conectionAPI.getData("GetBankInfo", { EmployeeID: $scope.employee.EmployeeID }).then(function (response) {
                    $scope.employee.BankInfo = response.data;
                });
            }
        }
    };

    conectionAPI.getData("GetKnowledges", { EmployeeID: $scope.employee ? $scope.employee.EmployeeID : null }).then(function (response) {
        $scope.knowledges = response.data;
    });

    $scope.stage = 1;

    $scope.foward = function () {
        $scope.stage++;
    };

    $scope.back = function () {
        $scope.stage--;
    };

    $scope.home = function () {
        conectionAPI.open("Index");
    }

    $scope.addEmployee = function (employee) {
        data = {};
        data.employee = {
            EmployeeID: employee.EmployeeID,
            Name: employee.Name,
            City: employee.City,
            State: employee.State,
            Portifolio: employee.Portifolio,
            LinkCrud: employee.LinkCrud,
            SalaryRequirement: employee.SalaryRequirement,
            PaymentType: employee.PaymentType,
            Contact: {
                ContactID: employee.Contact.ContactID,
                Linkedin: employee.Contact.Linkedin,
                Skype: employee.Contact.Skype,
                Email: employee.Contact.Email,
                Phone: employee.Contact.Phone
            },
            Availability: {
                AvailabilityID: employee.Availability.AvailabilityID,
                Until4Hours: employee.Availability.Until4Hours,
                From4To6Hours: employee.Availability.From4To6Hours,
                From6To8Hours: employee.Availability.From6To8Hours,
                Upto8Hours: employee.Availability.Upto8Hours
            },
            WorkingHour: {
                WorkingHourID: employee.WorkingHour.WorkingHourID,
                Morning: employee.WorkingHour.Morning,
                Afternoon: employee.WorkingHour.Afternoon,
                Night: employee.WorkingHour.Night,
                Dawn: employee.WorkingHour.Dawn,
                Business: employee.WorkingHour.Business
            }
        };

        if (employee.PaymentType == 0) {
            data.paypal = {
                PaypalInfoID: employee.PaypalInfo.PaypalInfoID,
                Description: employee.PaypalInfo.Description
            };
        }

        if (employee.PaymentType == 1) {
            data.bank = {
                BankInfoID: employee.BankInfo.BankInfoID,
                HolderName: employee.BankInfo.HolderName,
                Cpf: employee.BankInfo.Cpf,
                BankName: employee.BankInfo.BankName,
                Agency: employee.BankInfo.Agency,
                AccountType: employee.BankInfo.AccountType,
                AccountNumber: employee.BankInfo.AccountNumber
            }
        }

        _Knowledges = [];

        $scope.knowledges.forEach(function (knowledge) {
            if (knowledge.Level >= 0)
                _Knowledges.push({ Knowledge: { KnowledgeID: knowledge.KnowledgeID }, Level: knowledge.Level });
        });

        customKnowledge = null;
        if ($scope.employee.otherKnowledge && $scope.employee.otherKnowledge.Description != "" && $scope.employee.otherKnowledge.Level >= 0) {
            customKnowledge = {
                KnowledgeID: $scope.employee.otherKnowledge.Knowledge.KnowledgeID,
                Description: $scope.employee.otherKnowledge.Knowledge.Description,
                Custom: true
            };
            _Knowledges.push({ Knowledge: { KnowledgeID: customKnowledge.KnowledgeID }, Level: $scope.employee.otherKnowledge.Level });
        }

        data.knowledges = _Knowledges;
        data.customKnowledge = customKnowledge;

        conectionAPI.sendData("SaveEmployee", data).then(function (response) {
            conectionAPI.open("Index", null, { Name: 'message', Value: response.data.type + '|' + response.data.message });
        });

        delete $scope.employee;
        delete $scope.knowledges;
    };

    $scope.workingHour = {};
    $scope.workingHour.choices = [];
    $scope.workingHour.validate = function ($event) {
        value = $event.currentTarget.getAttribute("ng-model");
        if ($scope.workingHour.choices.indexOf(value) == -1) {
            $scope.workingHour.choices.push(value);
        }
        else {
            $scope.workingHour.choices.splice($scope.workingHour.choices.indexOf(value), 1);
        }
    }

    $scope.availability = {};
    $scope.availability.choices = [];
    $scope.availability.validate = function ($event) {
        value = $event.currentTarget.getAttribute("ng-model");
        if ($scope.availability.choices.indexOf(value) == -1) {
            $scope.availability.choices.push(value);
        }
        else {
            $scope.availability.choices.splice($scope.availability.choices.indexOf(value), 1);
        }
    }
});