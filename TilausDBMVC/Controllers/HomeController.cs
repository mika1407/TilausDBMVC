using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TilausDBMVC.Models;

namespace TilausDBMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //lisätään
            if (Session["UserName"] == null)
            {
                ViewBag.LoginError = 0; //Ei virhettä
                ViewBag.LoggedStatus = "Out";
                return RedirectToAction("login", "home");
            }
            else 
                ViewBag.LoggedStatus = "In";
                ViewBag.LoginError = 0; //Ei virhettä
                return View();
        }

        public ActionResult About()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                ViewBag.Message = "Sovelluksen kuvaussivu / Your application description page.";

                return View();
            }
        }

        public ActionResult Contact()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                ViewBag.Message = "Yhteystieto sivu / Your contact page.";

                return View();
            }
        }

        public ActionResult Map()
        {
            ViewBag.Message = "Kartta missä asun / Your map page.";

            return View();
        }

        //Login
        public ActionResult Login()
        {
            ViewBag.LoginError = 0; //Ei virhettä
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(Logins LoginModel)
        {
            TilausDBEntities db = new TilausDBEntities();
            //Haetaan käyttäjän/Loginin tiedot annetuilla tunnustiedoilla tietokannasta LINQ kyselyllä
            var LoggedUser = db.Logins.SingleOrDefault(x => x.UserName == LoginModel.UserName && x.PassWord == LoginModel.PassWord);
            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Successfull login";
                ViewBag.LoggedStatus = "In";
                ViewBag.LoginError = 0; //Ei virhettä
                Session["UserName"] = LoggedUser.UserName;
                Session["LoginID"] = LoggedUser.LoginId;  //lisäsin
                return RedirectToAction("Index", "Home"); //Tässä määritellään mihin onnistunut kirjautuminen johtaa ----> Home/Index
            }
            else
            {
                ViewBag.LoginMessage = "Login unsuccessfull";
                ViewBag.LoggedStatus = "Out";
                ViewBag.LoginError = 1; // modaali login-ruutu uudelleen
                LoginModel.LoginErrorMessage = "Tuntematon käyttäjätunnus tai salasana";
                return View ("Login", LoginModel);
                //return View("Index", LoginModel);
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            ViewBag.LoggedStatus = "Out";
            return RedirectToAction ("Index", "Home"); //Uloskirjautumisen jälkeen pääsivulle
        }
    }
}