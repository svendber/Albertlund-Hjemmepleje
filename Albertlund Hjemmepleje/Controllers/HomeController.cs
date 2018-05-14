using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Albertlund_Hjemmepleje.Models.Entities;

namespace Albertlund_Hjemmepleje.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string email = Request["email"];
            string password = Request["password"];

            //datebase kald ... er brugeren oprettet?

            Session["login"] = true;
            return View();
        }

        public ActionResult RetProfil()
        {
            return View();
        }

        public ActionResult OpretBruger()
        {
            string name = Request["fullname"];
            string email = Request["email"];
            uint phone = Convert.ToUInt16(Request["phone"]);
            string occupation = Request["phone"];
            string admin = Request["userType"];

            User user = new User(name, email, phone, occupation, admin);

            //saveToDatabase(user);

            var loggedIn = Session["login"].Equals(true);

            if (loggedIn == true)
            {
                return View();
            }
            else
            {
                return Index();
            }

        }

        public ActionResult Log()
        {
            {
                return View();
            }
        }
    }
}

