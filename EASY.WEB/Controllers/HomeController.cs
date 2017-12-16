using EASY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EASY.Enum;

namespace EASY.WEB.Controllers
{
    public class HomeController : Controller
    {
        private EasyContext db = new EasyContext();

        public ActionResult Index(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                var notify = message.Split('|');
                if (notify.Length > 1)
                {
                    ViewBag.Type = notify[0];
                    ViewBag.Message = notify[1];
                }
            }
            var _return = db.Employee.ToList();
            return View(_return);
        }

        public ActionResult Employees()
        {
            return View();
        }

        public ActionResult Employee(int? EmployeeID)
        {
            try
            {
                var retorno = db.Employee.FirstOrDefault(x => x.EmployeeID == EmployeeID);
                return View(retorno);
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message });
            }
        }

        [HttpPost]
        public JsonResult Delete(int[] ToDelete)
        {
            try
            {
                foreach (int id in ToDelete)
                {
                    var bank = db.BankInfo.FirstOrDefault(x => x.Employee.EmployeeID == id);
                    if (bank != null) db.BankInfo.Remove(bank);

                    var paypal = db.PaypalInfo.FirstOrDefault(x => x.Employee.EmployeeID == id);
                    if (paypal != null) db.PaypalInfo.Remove(paypal);

                    var knowledges = db.KnowledgeXEmployee.Where(x => x.Employee.EmployeeID == id).ToList();
                    if (knowledges.Count > 0)
                        foreach (var k in knowledges)
                        {
                            var know = k.Knowledge;
                            db.KnowledgeXEmployee.Remove(k);
                            if (know.Custom)
                                db.Knowledge.Remove(know);
                        }

                    Employee employee = db.Employee.FirstOrDefault(x => x.EmployeeID == id);
                    if (employee != null)
                    {
                        var tempContact = employee.Contact;
                        var tempAvailability = employee.Availability;
                        var tempWorkingHour = employee.WorkingHour;

                        db.Employee.Remove(employee);

                        var Contact = db.Contact.FirstOrDefault(x => x.ContactID == tempContact.ContactID);
                        if (Contact != null) db.Contact.Remove(Contact);

                        var Availability = db.Availability.FirstOrDefault(x => x.AvailabilityID == tempAvailability.AvailabilityID);
                        if (Availability != null) db.Availability.Remove(Availability);

                        var WorkingHour = db.WorkingHour.FirstOrDefault(x => x.WorkingHourID == tempWorkingHour.WorkingHourID);
                        if (WorkingHour != null) db.WorkingHour.Remove(WorkingHour);


                        db.SaveChanges();
                    }
                }
                return Json(new { type = "success", message = "Registro(s) removido(s) com sucesso!" });
            }
            catch (Exception e)
            {
                return Json(new { type = "danger", message = e.Message });
            }
        }

        public JsonResult GetKnowledgeXEmployee(int EmployeeID)
        {
            try
            {
                var _return = db.KnowledgeXEmployee.Where(x => x.Employee.EmployeeID == EmployeeID).ToList();
                return Json(_return, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message });
            }
        }

        public JsonResult GetBankInfo(int EmployeeID)
        {
            try
            {
                var _return = db.BankInfo.FirstOrDefault(x => x.Employee.EmployeeID == EmployeeID);
                return Json(_return, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetPaypalInfo(int EmployeeID)
        {
            try
            {
                var _return = db.PaypalInfo.FirstOrDefault(x => x.Employee.EmployeeID == EmployeeID);
                return Json(_return, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message });
            }
        }

        public JsonResult GetKnowledges()
        {
            try
            {
                var _return = db.Knowledge.Where(x => !x.Custom).ToList();

                return Json(_return, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SaveEmployee(Employee employee, BankInfo bank, PaypalInfo paypal, List<KnowledgeXEmployee> knowledges, Knowledge customKnowledge)
        {
            try
            {
                var _availability = db.Availability.FirstOrDefault(x => x.AvailabilityID == employee.Availability.AvailabilityID);
                if (_availability == null)
                {
                    db.Availability.Add(employee.Availability);
                    db.SaveChanges();
                    _availability = employee.Availability;
                }
                else
                {
                    _availability.Until4Hours = employee.Availability.Until4Hours;
                    _availability.From4To6Hours = employee.Availability.From4To6Hours;
                    _availability.From6To8Hours = employee.Availability.From6To8Hours;
                    _availability.Upto8Hours = employee.Availability.Upto8Hours;

                    db.Entry(_availability).State = System.Data.Entity.EntityState.Modified;
                }

                var _WorkingHour = db.WorkingHour.FirstOrDefault(x => x.WorkingHourID == employee.WorkingHour.WorkingHourID);
                if (_WorkingHour == null)
                {
                    db.WorkingHour.Add(employee.WorkingHour);
                    db.SaveChanges();
                    _WorkingHour = employee.WorkingHour;
                }
                else
                {
                    _WorkingHour.Morning = employee.WorkingHour.Morning;
                    _WorkingHour.Afternoon = employee.WorkingHour.Afternoon;
                    _WorkingHour.Night = employee.WorkingHour.Night;
                    _WorkingHour.Dawn = employee.WorkingHour.Dawn;
                    _WorkingHour.Business = employee.WorkingHour.Business;
                    db.Entry(_WorkingHour).State = System.Data.Entity.EntityState.Modified;
                }

                var _Contact = db.Contact.FirstOrDefault(x => x.ContactID == employee.Contact.ContactID);
                if (_Contact == null)
                {
                    db.Contact.Add(employee.Contact);
                    db.SaveChanges();
                    _Contact = employee.Contact;
                }
                else
                {
                    _Contact.Linkedin = employee.Contact.Linkedin;
                    _Contact.Skype = employee.Contact.Skype;
                    _Contact.Email = employee.Contact.Email;
                    _Contact.Phone = employee.Contact.Phone;
                    db.Entry(_Contact).State = System.Data.Entity.EntityState.Modified;
                }

                var _Employee = db.Employee.FirstOrDefault(x => x.EmployeeID == employee.EmployeeID);
                if (_Employee == null)
                {
                    db.Employee.Add(employee);
                    db.SaveChanges();
                    _Employee = employee;
                }
                else
                {
                    _Employee.Name = employee.Name;
                    _Employee.City = employee.City;
                    _Employee.State = employee.State;
                    _Employee.Portifolio = employee.Portifolio;
                    _Employee.LinkCrud = employee.LinkCrud;
                    _Employee.SalaryRequirement = employee.SalaryRequirement;
                    _Employee.PaymentType = employee.PaymentType;
                    _Employee.JoiningDate = employee.JoiningDate;
                    _Employee.Contact = _Contact;
                    _Employee.Availability = _availability;
                    _Employee.WorkingHour = _WorkingHour;
                    db.Entry(_Employee).State = System.Data.Entity.EntityState.Modified;
                }

                switch (employee.PaymentType)
                {
                    case (int)PAYMENTTYPE.PAYPAL:
                        var _PaypalInfo = db.PaypalInfo.FirstOrDefault(x => x.PaypalInfoID == paypal.PaypalInfoID);
                        if (_PaypalInfo == null)
                        {
                            paypal.Employee = employee;
                            db.PaypalInfo.Add(paypal);
                        }
                        else
                        {
                            _PaypalInfo.Description = paypal.Description;
                            _PaypalInfo.Employee = _Employee;
                            db.Entry(_PaypalInfo).State = System.Data.Entity.EntityState.Modified;
                        }
                        break;
                    case (int)PAYMENTTYPE.BANK:
                        var _BankInfo = db.BankInfo.FirstOrDefault(x => x.BankInfoID == bank.BankInfoID);
                        if (_BankInfo == null)
                        {
                            bank.Employee = employee;
                            db.BankInfo.Add(bank);
                        }
                        else
                        {
                            _BankInfo.HolderName = bank.HolderName;
                            _BankInfo.Cpf = bank.Cpf;
                            _BankInfo.BankName = bank.BankName;
                            _BankInfo.Agency = bank.Agency;
                            _BankInfo.AccountType = bank.AccountType;
                            _BankInfo.AccountNumber = bank.AccountNumber;
                            _BankInfo.Employee = _Employee;
                            db.Entry(_BankInfo).State = System.Data.Entity.EntityState.Modified;
                        }
                        break;
                }

                if (customKnowledge != null)
                {
                    var _customKnowledge = db.Knowledge.FirstOrDefault(x => x.KnowledgeID == customKnowledge.KnowledgeID);
                    if (_customKnowledge == null)
                    {
                        db.Knowledge.Add(customKnowledge);

                        db.SaveChanges();

                        knowledges.ForEach(x => x.Knowledge = x.Knowledge ?? customKnowledge);
                    }
                    else
                    {
                        _customKnowledge.Description = customKnowledge.Description;
                        db.Entry(_customKnowledge).State = System.Data.Entity.EntityState.Modified;
                    }
                }

                foreach (KnowledgeXEmployee ke in knowledges)
                {
                    var _KnowledgeXEmployee = db.KnowledgeXEmployee.FirstOrDefault(x => x.Knowledge.KnowledgeID == ke.Knowledge.KnowledgeID && x.Employee.EmployeeID == _Employee.EmployeeID);
                    if (_KnowledgeXEmployee == null)
                    {
                        ke.Knowledge = db.Knowledge.FirstOrDefault(x => x.KnowledgeID == ke.Knowledge.KnowledgeID);
                        ke.Employee = _Employee;
                        db.KnowledgeXEmployee.Add(ke);
                    }
                    else
                    {
                        _KnowledgeXEmployee.Level = ke.Level;
                        db.Entry(_KnowledgeXEmployee).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                db.SaveChanges();

                return Json(new { type = "success", message = "Salvo com Sucesso!" });
            }
            catch (Exception e)
            {
                return Json(new { type = "warning", message = e.Message });
            }
        }
    }
}