using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CAPSTONE_PROJECT.Models;

namespace CAPSTONE_PROJECT.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class PagamentiController : Controller
    {
        private Context db = new Context();

        public ActionResult Index()
        {
            return View(db.Pagamenti.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagamenti pagamenti = db.Pagamenti.Find(id);

            if (pagamenti == null)
            {
                return HttpNotFound();
            }
            return View(pagamenti);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPagamento,Mensa,TrasportoScolastico,Assicurazione,Bilinguismo,Totale")]Pagamenti pagamenti)
        {
            if (ModelState.IsValid)
            {
                db.Pagamenti.Add(pagamenti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pagamenti);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagamenti pagamenti = db.Pagamenti.Find(id);
            if (pagamenti == null)
            {
                return HttpNotFound();
            }
            return View(pagamenti);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPagamento,Mensa,TrasportoScolastico,Assicurazione,Bilinguismo,Totale")] Pagamenti pagamenti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pagamenti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pagamenti);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagamenti pagamenti = db.Pagamenti.Find(id);
            if (pagamenti == null)
            {
                return HttpNotFound();
            }
            return View(pagamenti);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pagamenti pagamenti = db.Pagamenti.Find(id);
            db.Pagamenti.Remove(pagamenti);
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
