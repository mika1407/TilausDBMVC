using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TilausDBMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Sovelluksen kuvaussivu / Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Yhteystieto sivu / Your contact page.";

            return View();
        }

        public ActionResult Map()
        {
            ViewBag.Message = "Kartta missä asun / Your map page.";

            return View();
        }
    }
}