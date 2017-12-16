using EASY.Enum;
using EASY.Models;
using System;
using System.Collections.Generic;

namespace EASY
{
    public class SeedInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<EasyContext>
    {
        protected override void Seed(EasyContext context)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Databases"));

            var Knowledges = new List<Knowledge>
            {
            new Knowledge{Description="Ionic",Required=true,Custom=false},
            new Knowledge{Description="Android",Required=true,Custom=false},
            new Knowledge{Description="IOS",Required=true,Custom=false},
            new Knowledge{Description="HTML",Required=false,Custom=false},
            new Knowledge{Description="CSS",Required=false,Custom=false},
            new Knowledge{Description="Bootstrap",Required=true,Custom=false},
            new Knowledge{Description="Jquery",Required=true,Custom=false},
            new Knowledge{Description="Angular JS",Required=true,Custom=false},
            new Knowledge{Description="Java",Required=false,Custom=false},
            new Knowledge{Description="Asp.Net MVC",Required=true,Custom=false},
            new Knowledge{Description="C",Required=false,Custom=false},
            new Knowledge{Description="C++",Required=false,Custom=false},
            new Knowledge{Description="Cake",Required=false,Custom=false},
            new Knowledge{Description="Django",Required=false,Custom=false},
            new Knowledge{Description="Majento",Required=false,Custom=false},
            new Knowledge{Description="PHP",Required=true,Custom=false},
            new Knowledge{Description="Wordpress",Required=true,Custom=false},
            new Knowledge{Description="Phyton",Required=false,Custom=false},
            new Knowledge{Description="Ruby",Required=false,Custom=false},
            new Knowledge{Description="My SQL Server",Required=false,Custom=false},
            new Knowledge{Description="Salesforce",Required=false,Custom=false},
            new Knowledge{Description="Photoshop",Required=false,Custom=false},
            new Knowledge{Description="Illustrator",Required=false,Custom=false},
            new Knowledge{Description="SEO",Required=false,Custom=false}
            };
            Knowledges.ForEach(x => context.Knowledge.Add(x));

            var employee = new Employee
            {
                Name = "Abner Al Mey Da",
                City = "Jeffersonville",
                State = "Kentuky",
                SalaryRequirement = 20,
                LinkCrud = "http://",
                PaymentType = (int)PAYMENTTYPE.BANK,
                JoiningDate = DateTime.Now,
                Contact = new Contact
                {
                    Linkedin = "ab.net",
                    Skype = "abner.almeyda",
                    Email = "abneraa@gmail.com",
                    Phone = "045998546251"
                },
                Availability = new Availability
                {
                    From4To6Hours = true,
                    From6To8Hours = false,
                    Until4Hours = false,
                    Upto8Hours = false
                },
                WorkingHour = new WorkingHour
                {
                    Afternoon = false,
                    Dawn = false,
                    Morning = true,
                    Night = true,
                    Business = true
                }
            };
            context.Employee.Add(employee);
            var KnowledgeXEmployees = new List<KnowledgeXEmployee>
            {
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[0],Level=0},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[1],Level=2},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[2],Level=1},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[5],Level=4},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[6],Level=5},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[7],Level=3},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[9],Level=2},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[15],Level=1},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[16],Level=0}
            };
            KnowledgeXEmployees.ForEach(x => context.KnowledgeXEmployee.Add(x));

            context.BankInfo.Add(new BankInfo
            {
                AccountNumber = "23592-9",
                AccountType = ACCOUNTTYPE.CHAIN,
                Agency = "2649-8",
                BankName = "Banco do Brasil",
                Cpf = 05946978123,
                HolderName = employee.Name,
                Employee = employee
            });

            employee = new Employee
            {
                Name = "Jonatas Souza",
                City = "Toledo",
                State = "Paraná",
                SalaryRequirement = 25,
                LinkCrud = "http://",
                PaymentType = (int)PAYMENTTYPE.BANK,
                JoiningDate = DateTime.Now,
                Contact = new Contact
                {
                    Linkedin = "j.souza",
                    Skype = "jonatas105",
                    Email = "jonatas92.js@gmail.com",
                    Phone = "045999295418"
                },
                Availability = new Availability
                {
                    From4To6Hours = false,
                    From6To8Hours = true,
                    Until4Hours = false,
                    Upto8Hours = true
                },
                WorkingHour = new WorkingHour
                {
                    Afternoon = false,
                    Dawn = false,
                    Morning = false,
                    Night = true,
                    Business = true
                }
            };
            context.Employee.Add(employee);
            KnowledgeXEmployees = new List<KnowledgeXEmployee>
            {
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[0],Level=0},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[1],Level=3},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[2],Level=3},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[5],Level=4},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[6],Level=5},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[7],Level=3},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[9],Level=1},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[15],Level=1},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[16],Level=0}
            };
            KnowledgeXEmployees.ForEach(x => context.KnowledgeXEmployee.Add(x));

            context.BankInfo.Add(new BankInfo
            {
                AccountNumber = "3254-5",
                AccountType = ACCOUNTTYPE.CHAIN,
                Agency = "45315-7",
                BankName = "ITAU",
                Cpf = 32543254325,
                HolderName = employee.Name,
                Employee = employee
            });

            employee = new Employee
            {
                Name = "Marcos de Assis",
                City = "Salvador",
                State = "Bahia",
                SalaryRequirement = 21,
                LinkCrud = "http://",
                PaymentType = (int)PAYMENTTYPE.PAYPAL,
                JoiningDate = DateTime.Now,
                Contact = new Contact
                {
                    Linkedin = "m.assis",
                    Skype = "marco.1",
                    Email = "assis.ue@gmail.com",
                    Phone = "045212121228"
                },
                Availability = new Availability
                {
                    From4To6Hours = true,
                    From6To8Hours = true,
                    Until4Hours = false,
                    Upto8Hours = true
                },
                WorkingHour = new WorkingHour
                {
                    Afternoon = true,
                    Dawn = false,
                    Morning = true,
                    Night = false,
                    Business = false
                }
            };
            context.Employee.Add(employee);
            KnowledgeXEmployees = new List<KnowledgeXEmployee>
            {
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[0],Level=2},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[1],Level=3},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[2],Level=4},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[5],Level=5},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[6],Level=2},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[7],Level=3},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[9],Level=5},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[15],Level=1},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[16],Level=1}
            };
            KnowledgeXEmployees.ForEach(x => context.KnowledgeXEmployee.Add(x));

            context.PaypalInfo.Add(new PaypalInfo
            {
                Description = "idontknow",
                Employee = employee
            });

            employee = new Employee
            {
                Name = "Jairson Da Silva",
                City = "Cubatão",
                State = "São Paulo",
                SalaryRequirement = 18,
                LinkCrud = "http://",
                PaymentType = (int)PAYMENTTYPE.PAYPAL,
                JoiningDate = DateTime.Now,
                Contact = new Contact
                {
                    Linkedin = "m.mendes",
                    Skype = "mendesss",
                    Email = "mendesss.ue@gmail.com",
                    Phone = "045212121228"
                },
                Availability = new Availability
                {
                    From4To6Hours = true,
                    From6To8Hours = true,
                    Until4Hours = false,
                    Upto8Hours = true
                },
                WorkingHour = new WorkingHour
                {
                    Afternoon = true,
                    Dawn = false,
                    Morning = true,
                    Night = false,
                    Business = false
                }
            };
            context.Employee.Add(employee);
            KnowledgeXEmployees = new List<KnowledgeXEmployee>
            {
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[0],Level=2},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[1],Level=3},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[2],Level=4},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[5],Level=5},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[6],Level=2},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[7],Level=3},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[9],Level=5},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[15],Level=1},
            new KnowledgeXEmployee{Employee=employee,Knowledge=Knowledges[16],Level=1}
            };
            KnowledgeXEmployees.ForEach(x => context.KnowledgeXEmployee.Add(x));
            context.PaypalInfo.Add(new PaypalInfo
            {
                Description = "http:link",
                Employee = employee
            });
            context.SaveChanges();
        }
    }
}