using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TilausDBMVC.Models;


namespace TilausDBMVC.Controllers
{
    public class TilastojaController : Controller
    {
        private TilausDBEntities db = new TilausDBEntities();
        // GET: Tilastoja
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult ProductSalesAllTimes()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                string topNimiLista;
                string topMyyntiLista;
                List<TuotteetMyynti10Parasta_kaikkinaAikoina> product_sales_10foralltimes = new List<TuotteetMyynti10Parasta_kaikkinaAikoina>();

                var product10SalesData = from psall in db.TuotteetMyynti10Parasta_kaikkinaAikoina
                                         select psall;

                foreach (TuotteetMyynti10Parasta_kaikkinaAikoina productsalesforalltimes in product10SalesData)
                {
                    TuotteetMyynti10Parasta_kaikkinaAikoina OneSalesRow = new TuotteetMyynti10Parasta_kaikkinaAikoina();
                    OneSalesRow.Nimi = productsalesforalltimes.Nimi;
                    OneSalesRow.TuotteidenMyynti = (int)productsalesforalltimes.TuotteidenMyynti;
                    product_sales_10foralltimes.Add(OneSalesRow);
                }

                topNimiLista = "'" + string.Join("','", product_sales_10foralltimes.Select(n => n.Nimi).ToList()) + "'";
                topMyyntiLista = string.Join(",", product_sales_10foralltimes.Select(n => n.TuotteidenMyynti).ToList());

                ViewBag.Nimi = topNimiLista;
                ViewBag.TuotteidenMyynti = topMyyntiLista;


                return View();
            }
        }

            public ActionResult ViikonPaivat()
            {
                if (Session["UserName"] == null)
                {
                    return RedirectToAction("login", "home");
                }
                else
                {
                    string viikonPaiva;
                    string topMyyntiLista;
                    List<Paivat> product_sales_week = new List<Paivat>();

                    var productWeekData = from pwd in db.Paivat
                                             select pwd;

                    foreach (Paivat productsalesforalltimes in productWeekData)
                    {
                        Paivat OneSalesRow = new Paivat();
                        OneSalesRow.Weekday = productsalesforalltimes.Weekday;
                        OneSalesRow.TuotteidenMyynti = (int)productsalesforalltimes.TuotteidenMyynti;
                        product_sales_week.Add(OneSalesRow);
                    }

                    viikonPaiva = "'" + string.Join("','", product_sales_week.Select(n => n.Weekday).ToList()) + "'";
                    topMyyntiLista = string.Join(",", product_sales_week.Select(n => n.TuotteidenMyynti).ToList());

                    ViewBag.Weekday = viikonPaiva;
                    ViewBag.TuotteidenMyynti = topMyyntiLista;


                    return View();
                }
            }
    }
}