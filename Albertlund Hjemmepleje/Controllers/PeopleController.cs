﻿using Albertlund_Hjemmepleje.Models;
using Albertlund_Hjemmepleje.Models.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Text;

namespace Albertlund_Hjemmepleje.Controllers
{
    public class PeopleController : Controller
    {
        private Albertlund_HjemmeplejeContext db = new Albertlund_HjemmeplejeContext();

        // GET: People
        public ActionResult Index()
        {
            if (Session["login"] == null)
            {
                return RedirectToAction("Login");
                
            }
            return View(db.People.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(string email)
        {
            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Person person = db.People.Find(email);
            if (person == null)
            {
                return HttpNotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            if (Session["login"] == null)
            {
                return RedirectToAction("Login");
            }
            if (Session["admin"].Equals("admin"))
            {
                return View();
            }

            return null;
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "email,name,phone,occupation,role")] Person person)
        {

            
            System.Diagnostics.Debug.WriteLine("Hej2");
             //if (ModelState.IsValid)
             { 
                
                person.password = SecurePasswordHasher.Hash("NewUser123456");
                //person.role = true;
                //person.occupation = "ged";
                //person.phone = 12345678;
                db.People.Add(person); 
                db.SaveChanges();
                string email = person.email;

                string body = "Hej \n" + "Du er oprettet hos Albertslund Hjemmepleje \n" +
                              "Dette er dit password: \b NewUser123456 \n" +
                              "Vi anbefaler dig, at du ændrer det første gang, du logger ind.";
                System.Diagnostics.Debug.WriteLine(email);
                sendMail(email, body);

                return RedirectToAction("Index");
            }

            //return View(person);
        }

        // GET: People/Edit/5
        public ActionResult Edit(string email)
        {
            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Person person = db.People.Find(email);
            if (person == null)
            {
                return HttpNotFound();
            }

            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "email,name,password,role,occupation")]
            Person person)
        {
            if (ModelState.IsValid)
            {
                person.password = SecurePasswordHasher.Hash(person.password);
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(person);
        }

        // GET: People/Delete/5
        public ActionResult Delete(string email)
        {
            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Person person = db.People.Find(email);
            if (person == null)
            {
                return HttpNotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string email)
        {
            Person person = db.People.Find(email);
            db.People.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }

        public ActionResult Login()
        {
            string email = Request["email"];
            string password = Request["password"];

            Person person = db.People.Find(email);

            if (person == null)
            {
                Console.WriteLine("User does not exist");
            }
            else
            {
                Boolean verify = SecurePasswordHasher.Verify(password, person.password);
                if (verify == true)
                {
                    Session["login"] = email;
                    if (person.role)
                    {
                        Session["admin"] = "admin";
                    }
                    else
                    {
                        Session["admin"] = "user";
                    }

                    return RedirectToAction("Index");

                }
            }

            


            return View();
        }


        public ActionResult Log()
        {
            if (Session["login"] == null)
            {
                return RedirectToAction("Login");
            }

            if (Session["admin"].Equals("admin"))
            {
                return View();
            }

            return null;

        }

      
        public ActionResult ForgottenPassword()
        {
            string email = Request["email"];
            Person person = db.People.Find(email);

            if (person == null)
            {

            }
            else
            {
                string body = "Hej \n Dit password er nulstillet. \n " +
                          "Dit nye password er: NewPassword123456";
                person.password = SecurePasswordHasher.Hash("NewPassword123456");
                db.Entry(person).Property("password").IsModified = true;
                db.SaveChanges();
                sendMail(email, body);
            }
            return View();
            
        }

        public void sendMail(string toMail, string body)
        {
            if (String.IsNullOrEmpty(toMail))
                return;
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(toMail);
                //mail.To.Add("xxx@gmail.com");
                mail.From = new MailAddress("albertslundhjemmepleje@gmail.com");
                mail.Subject = "Your password to Albertslund Hjemmepleje.";

                mail.Body = body;

                mail.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com"; 
                smtp.Credentials = new System.Net.NetworkCredential
                     ("albertslundhjemmepleje@gmail.com", "svendoliviajulie"); 
                smtp.Port = 587;
                
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception in sendEmail:" + ex.Message);
            }
        }
    }
}
