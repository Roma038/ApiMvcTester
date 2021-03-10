using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ApiMvcTester.Models;
using System.Net.Mail;
//using MailKit.Net.Smtp;
//using System.Web.Http;

namespace ApiMvcTester.Controllers
{
    public class HomeController : Controller
    {
        int confirm_code;

        public ActionResult Main()
        {
            return View();

        }

        public ActionResult AllStudents()
        {
            var db = new StudentTestApiContext();

            return View(db.Students);
        }

        
        public ActionResult Update(int id)
        {

            using (var db = new StudentTestApiContext())
            {
                var stud = db.Students.Where(x => x.Id == id).FirstOrDefault();

                return View(stud);
            }
            
        }

        [HttpPost]
        public ActionResult Update(Student stud)
        {
            using (var db = new StudentTestApiContext())
            {
                var student = db.Students.Where(x => x.Id == stud.Id).FirstOrDefault();

                if (student != null)
                {
                    if (student.email != stud.email)
                    {
                        SendMail(stud.email, stud.ConfirmCode);
                    }
                    student.Name = stud.Name;
                    student.email = stud.email;
                    student.Degree = stud.Degree;
                    db.SaveChanges();
                    return RedirectToAction("AllStudents");
                }
                return HttpNotFound($"Could not update student data with Id = {stud.Id}");
            }
           
        }

        public ActionResult Delete(int id)
        {

            using (var db = new StudentTestApiContext())
            {
                var stud = db.Students.Where(x => x.Id == id).FirstOrDefault();

                return View(stud);
            }

        }

        [HttpPost]
        public ActionResult Delete(Student stud)
        {
            using (var db = new StudentTestApiContext())
            {
                var student = db.Students.Where(x => x.Id == stud.Id).FirstOrDefault();
                if (student != null)
                {
                    db.Students.Remove(student);
                    db.SaveChanges();
                    return RedirectToAction("AllStudents");
                }
                return HttpNotFound($"Could not find a student with Id = {stud.Id}");
            }
        }

        public ActionResult Add()
        {
            
            return View();
        }


        [HttpPost]
        public ActionResult ConfirmEmail(Student stud)
        {
            confirm_code = GenerateCode();
            stud.ConfirmCode = confirm_code;
            SendMail(stud.email, stud.ConfirmCode);

            return View(stud);
        }

        [HttpPost]
        public ActionResult Add(Student stud)
        {

            using (var db = new StudentTestApiContext())
            {
                string _code = Request.Form["Code_"].ToString();
                int conCode = Convert.ToInt32(_code);

                if (stud.ConfirmCode == conCode)
                {
                    if (stud != null)
                    {
                        db.Students.Add(stud);
                        db.SaveChanges();

                        return RedirectToAction("AllStudents");
                    }
                }

                return HttpNotFound();   
            }
            
        }

        private void SendMail(string toAddress, int code)
        {
            var fromMail = new MailAddress("johnueak654@gmail.com");
            const string fromPasword = "0507903730";
            var toMail = new MailAddress(toAddress);
            MailMessage message = new MailMessage();

            message.From = fromMail;
            message.To.Add(toMail);
            message.Subject = "Confirmation";
            message.Body = "Hi there!" +
                           "Please, confirm your email address." +
                           $"Your confirmation code is {code}!";


            using (var client = new SmtpClient())
            {

                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("johnueak654@gmail.com", fromPasword);
                client.EnableSsl = true;
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Timeout = 10000;
                client.Send(message);
            }
        }

        private int GenerateCode()
        {
            int min = 1000;
            int max = 9999;
            Random random = new Random();
            return random.Next(min, max);
        }

    }
}
