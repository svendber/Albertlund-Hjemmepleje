using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Albertlund_Hjemmepleje.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RetProfil()
        {
            ViewBag.Message = "Ret profil page";

            return View();
        }

        public ActionResult OpretBruger()
        {
            ViewBag.Message = "Opret bruger page";

            return View();
        }

        public ActionResult RetProfil()
        {

            return View();
        }
    }
}