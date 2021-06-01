using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TilausDBMVC.Models;

namespace TilausDBMVC.Controllers
{
    public class AsiakkaatController : Controller
    {
        private TilausDBEntities db = new TilausDBEntities();

        // GET: Asiakkaat
        public ActionResult Index()
        {
            var asiakkaat = db.Asiakkaat.Include(a => a.Postitoimipaikat);
            return View(asiakkaat.ToList());
        }

        // GET: Asiakkaat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
            if (asiakkaat == null)
            {
                return HttpNotFound();
            }
            return View(asiakkaat);
        }

        // GET: Asiakkaat/Create
        public ActionResult Create()
        {
            var posti = db.Postitoimipaikat;
            IEnumerable<SelectListItem> selectPostiLists = from p in posti
                                                           select new SelectListItem
                                                           {
                                                               Value = p.PostiID.ToString(),
                                                               Text = p.Postinumero + " " + p.Postitoimipaikka.ToString()
                                                           };

            //ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka");
            ViewBag.PostiID = new SelectList(selectPostiLists, "Value", "Text");
            return View();
        }

        // POST: Asiakkaat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AsiakasID,Nimi,Osoite,Postinumero,Postitoimipaikka,PostiID,Etunimi,Sukunimi")] Asiakkaat asiakkaat)
        {
            if (ModelState.IsValid)
            {
                db.Asiakkaat.Add(asiakkaat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var posti = db.Postitoimipaikat;
            IEnumerable<SelectListItem> selectPostiLists = from p in posti
                                                           select new SelectListItem
                                                           {
                                                               Value = p.PostiID.ToString(),
                                                               Text = p.Postinumero + " " + p.Postitoimipaikka.ToString()
                                                           };

            //ViewBag.PostiID = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka", asiakkaat.Postinumero);
            ViewBag.PostiID = new SelectList(selectPostiLists, "Value", "Text", asiakkaat.PostiID);
            return View(asiakkaat);
        }

        // GET: Asiakkaat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
            if (asiakkaat == null)
            {
                return HttpNotFound();
            }

            var asiakas = db.Asiakkaat;
            IEnumerable<SelectListItem> selectLists = from s in asiakas
                                                      select new SelectListItem
                                                      {
                                                          Value = s.AsiakasID.ToString(),
                                                          Text = s.Etunimi + " " + s.Sukunimi.ToString()
                                                      };
            var posti = db.Postitoimipaikat;
            IEnumerable<SelectListItem> selectPostiLists = from p in posti
                                                           select new SelectListItem
                                                           {
                                                               Value = p.PostiID.ToString(),
                                                               Text = p.Postinumero + " " + p.Postitoimipaikka.ToString()
                                                           };


            //ViewBag.PostiID = new SelectList(db.Postitoimipaikat,"PostiID", "Postinumero", "Postitoimipaikka", asiakkaat.PostiID);
            ViewBag.PostiID = new SelectList(selectPostiLists, "Value", "Text", asiakkaat.PostiID);
            ViewBag.AsiakasID = new SelectList(selectLists, "Value", "Text", asiakkaat.AsiakasID);
            return View(asiakkaat);
        }

        // POST: Asiakkaat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AsiakasID,Nimi,Osoite,Postinumero,Postitoimipaikka,PostiID,Etunimi,Sukunimi")] Asiakkaat asiakkaat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asiakkaat).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.PostiID = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka", "PostiID", asiakkaat.PostiID);
                return RedirectToAction("Index");
            }

            var posti = db.Postitoimipaikat;
            IEnumerable<SelectListItem> selectPostiLists = from p in posti
                                                           select new SelectListItem
                                                           {
                                                               Value = p.PostiID.ToString(),
                                                               Text = p.Postinumero + " " + p.Postitoimipaikka.ToString()
                                                           };

            //ViewBag.PostiID = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka", "PostiID", asiakkaat.PostiID);
            ViewBag.PostiID = new SelectList(selectPostiLists, "Value", "Text", asiakkaat.PostiID);
            return View(asiakkaat);
        }

        // GET: Asiakkaat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
            if (asiakkaat == null)
            {
                return HttpNotFound();
            }
            return View(asiakkaat);
        }

        // POST: Asiakkaat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
            db.Asiakkaat.Remove(asiakkaat);
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
    }
}
