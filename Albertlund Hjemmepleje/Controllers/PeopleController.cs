﻿using System;
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
using System.Text;

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
        public ActionResult Create([Bind(Include = "email,name,role,occupation")] Person person)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                    builder.DataSource = "Server=tcp:webappserver-1.database.windows.net,1433;Initial Catalog=personDB;Persist Security Info=False;User ID={your_username};Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
                    builder.UserID = "svendber";
                    builder.Password = "svendsej123";
                    builder.InitialCatalog = "PersonDB";

                    using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                    {
                        Console.WriteLine("\nQuery data example:");
                        Console.WriteLine("=========================================\n");

                        connection.Open();
                        StringBuilder sb = new StringBuilder();
                        sb.Append("INSERT INTO ");
                        sb.Append("FROM [SalesLT].[ProductCategory] pc ");
                        sb.Append("JOIN [SalesLT].[Product] p ");
                        sb.Append("ON pc.productcategoryid = p.productcategoryid;");
                        String sql = sb.ToString();

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                                }
                            }
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                }
                Console.ReadLine();

                person.password = "NewUser123456";
                db.People.Add(person);
                db.SaveChanges();
                
                string body = "Hej \n" + "Du er oprettet hos Albertslund Hjemmepleje \n" +
                              "Dette er dit password: \b NewUser123456 \n" + 
                              "Vi anbefaler dig, at du ændrer det første gang, du logger ind.";
             
                sendMail(person.email, body);

                return RedirectToAction("Index");
            }

            return View(person);
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
        public ActionResult Edit([Bind(Include = "iD,email,name,password,role,occupation")] Person person)
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

        public void sendMail(string toMail, string body)
        {
            Console.WriteLine("Send mail!!!!");
            MailMessage mail = new MailMessage("albertslundhjemmepleje@gmail.com", toMail);
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("albertslundhjemmepleje@gmail.com", "svendoliviajulie");
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Host = "smtp.gmail.com";
            mail.Subject = "Your password to Albertslund Hjemmepleje.";
            mail.Body = body;
            client.EnableSsl = true;
            client.Send(mail);
        }
    }
}
