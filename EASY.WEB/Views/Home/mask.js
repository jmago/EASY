formApp.directive("maskPhone", function ($filter) {
	return {
		require: "ngModel",
		link: function (scope, element, attrs, ctrl) {
			var _formatPhone = function (phone) {
				if (!phone) return phone;
				phone = phone.replace(/[^0-9]+/g, "");
                if(phone.length > 0) {
					phone = "(" + phone.substring(0);
				}
				if(phone.length > 3) {
					phone = phone.substring(0,3) + ")" + phone.substring(3);
				}
				if(phone.length > 8 && phone.length < 13) {
					phone = phone.substring(0,8) + "-" + phone.substring(8,12);
				} else if(phone.length > 9){
                    phone = phone.substring(0,9) + "-" + phone.substring(9,13);
                }
                
				return phone;
			};

			element.bind("keyup", function () {
				ctrl.$setViewValue(_formatPhone(ctrl.$viewValue));
				ctrl.$render();
			});

		}
	};
}).directive("maskCpf", function ($filter) {
	return {
		require: "ngModel",
		link: function (scope, element, attrs, ctrl) {
			var _formatCpf = function (cpf) {
				if (!cpf) return cpf;
				cpf = (cpf + "").replace(/[^0-9]+/g, "");
				if(cpf.length > 3) {
					cpf = cpf.substring(0,3) + "." + cpf.substring(3);
				}
				if(cpf.length > 7) {
					cpf = cpf.substring(0,7) + "." + cpf.substring(7,13);
				} 
                if(cpf.length > 11){
                    cpf = cpf.substring(0,11) + "-" + cpf.substring(11,13);
                }
                
				return cpf;
			};

			element.bind("keyup", function () {
				ctrl.$setViewValue(_formatCpf(ctrl.$viewValue));
				ctrl.$render();
			});

            ctrl.$formatters.push(function (value) {
				return _formatCpf(value);
			});
		}
	};
});