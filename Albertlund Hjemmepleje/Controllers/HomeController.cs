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
            return View();


        }

        public ActionResult OpretBruger(){
            return View();
        }

        public ActionResult Log(){
            {
                return View();
            }
        }
    }
}

