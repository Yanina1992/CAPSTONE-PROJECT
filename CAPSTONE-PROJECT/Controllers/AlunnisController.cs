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
    public class AlunnisController : Controller
    {
        private Context db = new Context();

        public ActionResult Index()
        {
            var alunni = db.Alunni.Include(a => a.Classi).Include(a => a.DomandeIscrizione).Include(a => a.Pagamenti);
            return View(alunni.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alunni alunni = db.Alunni.Find(id);
            if (alunni == null)
            {
                return HttpNotFound();
            }
            return View(alunni);
        }

        public ActionResult Create()
        {
            ViewBag.FKClasse = new SelectList(db.Classi, "IdClasse", "Anno");
            ViewBag.FKDomandaIscrizione = new SelectList(db.DomandeIscrizione, "IdDomanda", "NomeAlunno");
            ViewBag.FKPagamento = new SelectList(db.Pagamenti, "IdPagamento", "IdPagamento");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAlunno,FKDomandaIscrizione,FKPagamento,FKClasse")] Alunni alunni)
        {
            if (ModelState.IsValid)
            {
                db.Alunni.Add(alunni);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FKClasse = new SelectList(db.Classi, "IdClasse", "Anno", alunni.FKClasse);
            ViewBag.FKDomandaIscrizione = new SelectList(db.DomandeIscrizione, "IdDomanda", "NomeAlunno", alunni.FKDomandaIscrizione);
            ViewBag.FKPagamento = new SelectList(db.Pagamenti, "IdPagamento", "IdPagamento", alunni.FKPagamento);
            return View(alunni);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alunni alunni = db.Alunni.Find(id);
            if (alunni == null)
            {
                return HttpNotFound();
            }
            ViewBag.FKClasse = new SelectList(db.Classi, "IdClasse", "Anno", alunni.FKClasse);
            ViewBag.FKDomandaIscrizione = new SelectList(db.DomandeIscrizione, "IdDomanda", "NomeAlunno", alunni.FKDomandaIscrizione);
            ViewBag.FKPagamento = new SelectList(db.Pagamenti, "IdPagamento", "IdPagamento", alunni.FKPagamento);
            return View(alunni);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAlunno,FKDomandaIscrizione,FKPagamento,FKClasse")] Alunni alunni)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alunni).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FKClasse = new SelectList(db.Classi, "IdClasse", "Anno", alunni.FKClasse);
            ViewBag.FKDomandaIscrizione = new SelectList(db.DomandeIscrizione, "IdDomanda", "NomeAlunno", alunni.FKDomandaIscrizione);
            ViewBag.FKPagamento = new SelectList(db.Pagamenti, "IdPagamento", "IdPagamento", alunni.FKPagamento);
            return View(alunni);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alunni alunni = db.Alunni.Find(id);
            if (alunni == null)
            {
                return HttpNotFound();
            }
            return View(alunni);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Alunni alunni = db.Alunni.Find(id);
            db.Alunni.Remove(alunni);
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
