formApp.factory("conectionAPI", function ($http) {
    var employees = [
        { id: 1, name: "Jonatas Souza", skype: "jonatas105", phone: "045 99929-5418", city: "Toledo", state: "Paraná" },
        {
            availability: { from4To6Hours: true },
            bankInformation: { accountNumber: "1081978", accountType: "chain", agency: "57355", bankName: "Banco do Brasil", cpf: "08354821978", holder: "Jonatas Souza", type: "bank" },
            city: "Marechal Candido Rondon",
            email: "merabe.tuani@gmail.com",
            id: 2,
            name: "Merabe Tuani",
            phone: "(45)95165-5161",
            salaryRequirement: 20,
            skype: "merabe.tuani",
            state: "PR",
            workingHour: { afternoon: true }
        }
    ];

    var _getEmployees = function () {
        //$http.get(url).then(function (response) {
        //    return response.data;
        //});
        return employees;
    };


    var _getKnowledges = function () {
        return [{ id: 0, desc: "Ionic", required: false },
        { id: 1, desc: ".Net", required: true },
        { id: 2, desc: "Java", required: false },
        { id: 3, desc: "Angular JS", required: true },
        { id: 4, desc: "Angular 2.0", required: true }
        ];
    };

    var _getEmployee = function (id) {
        return employees.filter(function (employee) {
            return employee.id == id;
        })[0];
    };

    var _saveEmployee = function (employee) {
        if (employee.id) {
            for (var i in employees) {
                if (employees[i].id == employee.id) {
                    employees[i] = employee;
                    break;
                }
            }
        } else {
            employee.id = getNextId();
            employees.push(employee);
        }
        return employees;
    };

    var _deleteEmployee = function (id) {
        employees = employees.filter(function (employee) {
            if (employee.id != id) return employee;
        });

        return employees;
    };

    let lastID = 2;

    var getNextId = function () {
        lastID++;
        return lastID;
    };



    return {
        getKnowledges: _getKnowledges,
        getEmployees: _getEmployees,
        getEmployee: _getEmployee,
        saveEmployee: _saveEmployee,
        deleteEmployee: _deleteEmployee
    };
});