using System;
using System.Collections.Generic;

namespace EASY.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Portifolio { get; set; }
        public string LinkCrud { get; set; }
        public decimal SalaryRequirement { get; set; }
        public int PaymentType { get; set; }
        public DateTime? JoiningDate { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual Availability Availability { get; set; }
        public virtual WorkingHour WorkingHour { get; set; }
    }
}
