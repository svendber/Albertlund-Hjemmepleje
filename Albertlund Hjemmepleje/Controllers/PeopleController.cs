using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Albertlund_Hjemmepleje.Models;
using Albertlund_Hjemmepleje.Models.Entities;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Reflection.Emit;
using System.Text;
using System.Web.UI.WebControls.WebParts;

namespace Albertlund_Hjemmepleje.Controllers
{
    public class PeopleController : Controller
    {
        private Albertlund_HjemmeplejeContext db = new Albertlund_HjemmeplejeContext();

        // GET: People
        public ActionResult Index()
        {
            return View(db.People.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "email,name,phone,occupation,role")] Person person)
        {
            Console.Write("Hej2");
             //if (ModelState.IsValid)
             { 
                person.password = "NewUser123456";
                //person.role = true;
                //person.occupation = "ged";
                //person.phone = 12345678;
                db.People.Add(person); 
                db.SaveChanges();

                string body = "Hej \n" + "Du er oprettet hos Albertslund Hjemmepleje \n" +
                              "Dette er dit password: \b NewUser123456 \n" +
                              "Vi anbefaler dig, at du ændrer det første gang, du logger ind.";

                sendMail(person.email, body);

                return RedirectToAction("Index");
            }

            //return View(person);
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Person person = db.People.Find(id);
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
        public ActionResult Edit([Bind(Include = "iD,email,name,password,role,occupation")]
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
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.People.Find(id);
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
                    return RedirectToAction("Index");
                }
            }

            Session["login"] = true;
            return View();
        }


        public ActionResult Log()
        {
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgottenPassword(string email)
        {
            Person person = db.People.Find(email);

            string body = "Hej \n Dit password er nulstillet. \n " +
                          "Dit nye password er: NewUser123456";
            person.password = "NewUser123456";
            db.Entry(person).State = EntityState.Modified;
            db.SaveChanges();
            sendMail(email, body);
            {
                return View();
            }
        }

        public void sendMail(string toMail, string body)
        {
            Console.WriteLine("Send mail!!!!");
            MailMessage mail = new MailMessage("albertslundhjemmepleje@gmail.com", toMail);
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("albertslundhjemmepleje@gmail.com", "svendoliviajulie");
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            mail.Subject = "Your password to Albertslund Hjemmepleje.";
            mail.Body = body;
            client.EnableSsl = true;
            client.Send(mail);
        }
    }
}
