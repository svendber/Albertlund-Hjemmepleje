using Albertlund_Hjemmepleje.Models;
using Albertlund_Hjemmepleje.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Albertlund_Hjemmepleje.Controllers
{
    public class PeopleController : Controller
    {
        private Albertlund_HjemmeplejeContext db = new Albertlund_HjemmeplejeContext();
        
        
        public ActionResult Index()
        {
            if (Session["login"] == null)
            {
                return RedirectToAction("Login");
                
            }
            return View(db.People.ToList());
        }
     

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

  
        public ActionResult Create()
        {
            if (Session["admin"] == "user")
            {
                TempData["notAdmin"] = "user";
            }
            if (Session["login"] == null)
            {
                return RedirectToAction("Login");
            }
            if (Session["admin"].Equals("admin"))
            {
                return View();
            }
                //ViewBag.Error = TempData["error"];
                return RedirectToAction("Home");
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "email,name,phone,occupation,role")] Person person)
        {
             { 
                person.password = SecurePasswordHasher.Hash("NewUser123456");
                db.People.Add(person); 
                db.SaveChanges();
                string email = person.email;

                string body = "Hej \n" + "Du er oprettet hos Albertslund Hjemmepleje \n" +
                              "Dette er dit password: \b NewUser123456 \n" +
                              "Vi anbefaler dig, at du ændrer det første gang, du logger ind.";
                
                sendMail(email, body);

                return RedirectToAction("Home");
            }
            
        }

        
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
            DateTime dateTime = DateTime.Now;


            if (person == null)
            {
                Console.WriteLine("User does not exist");
            }
            else
            {
                Boolean verify = SecurePasswordHasher.Verify(password, person.password);
                if (verify)
                {
                    
                    Session["login"] = email;
                    if (person.role)
                    {
                        Session["admin"] = "admin";
                        //TempData["notAdmin"] = "admin";
                    }
                    else
                    {
                        Session["admin"] = "user";
                    }
                    Log log = new Log();
                    log.email = person.email;
                    log.time = dateTime;
                    db.Logs.Add(log);
                    db.SaveChanges(); 
                    return RedirectToAction("Home");
                }
            }
            string navbar = "hide";
            TempData["NavBar"] = navbar;

            return View();
        }

        public ActionResult Logout()
        {
            Session["login"] = null;
            TempData["NavBar"] = null;
            TempData["notAdmin"] = null;
            return RedirectToAction("Login");
        }


     
        public ActionResult ForgottenPassword()
        {
            string email = Request["email"];
            Person person = db.People.Find(email);
            string navbar = "hide";
            TempData["NavBar"] = navbar;

            if (person == null)
            {}
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

        public ActionResult Settings()
        {

            if (Session["admin"] == "user")
            {
                TempData["notAdmin"] = "user";
            }

            if (Session["login"] == null)
            {
                return RedirectToAction("Login");
            }
            string email = Session["login"].ToString();
            Person person = db.People.Find(email);

            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Settings([Bind(Include = "email,name,phone,password, occupation")]
            Person person)
        {



            string email = Request["email"];
            

            System.Diagnostics.Debug.WriteLine(person.phone);
            if (person == null)
            {
                System.Diagnostics.Debug.WriteLine("person is null");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("går den ud");
                string confirmPassword = Request["password_confirm"];
                person.name = Request["name"];
                person.email = Request["email"];
                person.phone = Request["phone"];
                bool isHashed = SecurePasswordHasher.IsHashSupported(Request["password"]);
                person.password = SecurePasswordHasher.Hash(Request["password"]);

               
                db.People.AddOrUpdate(person);
              
                db.SaveChanges();
                

            }

            return RedirectToAction("Home");
                
        
        }

       public ActionResult Home()
        {
            if (Session["login"] == null)
            {

                return RedirectToAction("Login");
            }
            if (Session["admin"] == "user")
            {
                TempData["notAdmin"] = "user";
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
               
                mail.From = new MailAddress("albertslundhjemmepleje@gmail.com");
                mail.Subject = "Your password to Albertslund Hjemmepleje.";

                mail.Body = body;
            //Console.Write();
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
