using ASPNETMVC.Context;
using ASPNETMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BC = BCrypt.Net.BCrypt;

namespace ASPNETMVC.Controllers
{
    public class AccountsController : Controller
    {
        private MyContext myContext = new MyContext();
        // GET: Accounts/Logins
        public ActionResult Logins()
        {
            return View();
        }
        // GET : Accounts/Logouts
        public ActionResult Logouts()
        {
            Session.RemoveAll();
            Session.Clear();
            return RedirectToAction("Logins");
        }
        // GET : Accounts/SignUps
        public ActionResult SignUps()
        {
            List<Division> list = myContext.Divisions.ToList();
            ViewBag.DivisionList = new SelectList(list, "Id", "Division_Name");
            return View();
        }
        // LOGINS
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logins(Login login)
        {
            if (ModelState.IsValid)
            {
                var dataList = myContext.Employees
                    .Join(myContext.Accounts,
                    emp => emp.Id,
                    acc => acc.Id,
                    (emp, acc) => new

                    {
                        EmpEmail = emp.Email,
                        EmpPhone = emp.Phone,
                        AccPassword = acc.Password
                    }
                    ).ToList();

                var mySign = dataList.FirstOrDefault(
                    m => (m.EmpEmail == login.Email || m.EmpPhone == login.Email)
                    && BC.Verify(login.Password, m.AccPassword));

                if (mySign != null)
                {
                    //add session
                    Session["Email"] = mySign.EmpEmail.ToString();
                    Session["Password"] = mySign.AccPassword.ToString();
                    var dEmployees = myContext.Employees.Where(e => e.Email == login.Email || e.Phone == login.Email).ToList();
                    Session["Id"] = dEmployees.FirstOrDefault().Id;
                    int logSignIn = Convert.ToInt32(Session["Id"]);

                    return RedirectToAction("Index", "Employees");
                }
                else
                {
                    ViewBag.error = "Login Failed";
                    return RedirectToAction("Logins");
                }
            }
            return View();
        }
        // SIGNUPS
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUps(Account account, Employee employee)
        {
            List<Division> list = myContext.Divisions.ToList();
            ViewBag.DivisionList = new SelectList(list, "Id", "Name");
            if (ModelState.IsValid)
            {
                var check = myContext.Employees.FirstOrDefault(s => s.Email == employee.Email);
                if (check == null)
                {
                    myContext.Accounts.Add(account);
                    myContext.Employees.Add(employee);
                    myContext.SaveChanges();
                    return RedirectToAction("Logins", "Accounts", new { area = "" });
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return View();
        }
        // Forgot Password
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(Login login)
        {
            var check = myContext.Employees.FirstOrDefault(s => s.Email == login.Email);
            if (check != null)
            {
                string newPass = Guid.NewGuid().ToString();
                var accountID = myContext.Accounts.FirstOrDefault(m => m.Id == check.Id);
                accountID.Password = newPass;
                myContext.SaveChanges();

                MailAddress to = new MailAddress(login.Email); //To address    
                MailAddress from = new MailAddress("dikarandika.rn@gmail.com"); //From address    
                MailMessage message = new MailMessage(from, to);

                string mailbody = "Your account password has changed, please Sign In again with this Password: " + newPass;
                message.Subject = "Request Reset Account Password";
                message.Body = mailbody;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
                System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential("dikarandika.rn@gmail.com", "Randika12345678");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = basicCredential1;
                try
                {
                    client.Send(message);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
                return RedirectToAction("Logins", "Accounts");
            }
            else
            {
                ViewBag.error = "Email doesn't exists";
                return View();
            }
            // https://accounts.google.com/b/0/DisplayUnlockCaptcha
        }
        // Hashing
        public class Hashing
        {
            private static string GetRandomSalt()
            {
                return BC.GenerateSalt(12);
            }

            public static string HashPassword(string password)
            {
                return BC.HashPassword(password, GetRandomSalt());
            }

            public static bool ValidatePassword(string password, string correctHash)
            {
                return BC.Verify(password, correctHash);
            }
        }
    }
}