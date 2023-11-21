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
    public class PagamentiEffettuatiController : Controller
    {
        private Context db = new Context();

        // GET: PagamentiEffettuati
        public ActionResult Index()
        {
            var pagamentiEffettuati = db.PagamentiEffettuati.Include(p => p.Pagamenti);
            return View(pagamentiEffettuati.ToList());
        }

        // GET: PagamentiEffettuati/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagamentiEffettuati pagamentiEffettuati = db.PagamentiEffettuati.Find(id);
            if (pagamentiEffettuati == null)
            {
                return HttpNotFound();
            }
            return View(pagamentiEffettuati);
        }

        // GET: PagamentiEffettuati/Create
        public ActionResult Create()
        {
            ViewBag.FKPagamento = new SelectList(db.Pagamenti, "IdPagamento", "IdPagamento");
            return View();
        }

        // POST: PagamentiEffettuati/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TotalePagato")] PagamentiEffettuati pagamentiEffettuati)
        {
            var id = Convert.ToInt32(Session["fkPag"]);
            pagamentiEffettuati.FKPagamento = id;
            var tot = Convert.ToDecimal(Session["Totale"]);
            var totPagato = Convert.ToDecimal(pagamentiEffettuati.TotalePagato);
            pagamentiEffettuati.TotaleDaPagare = tot - totPagato;
            pagamentiEffettuati.TotalePagato = totPagato;

            if (ModelState.IsValid)
            {
              

                db.PagamentiEffettuati.Add(pagamentiEffettuati);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = pagamentiEffettuati.IdPagamentoEffettuato });
            }

            ViewBag.FKPagamento = new SelectList(db.Pagamenti, "IdPagamento", "IdPagamento", pagamentiEffettuati.FKPagamento);
            return View(pagamentiEffettuati);
        }

        // GET: PagamentiEffettuati/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagamentiEffettuati pagamentiEffettuati = db.PagamentiEffettuati.Find(id);
            if (pagamentiEffettuati == null)
            {
                return HttpNotFound();
            }
            ViewBag.FKPagamento = new SelectList(db.Pagamenti, "IdPagamento", "IdPagamento", pagamentiEffettuati.FKPagamento);
            return View(pagamentiEffettuati);
        }

        // POST: PagamentiEffettuati/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPagamentoEffettuato,TotalePagato,TotaleDaPagare,FKPagamento")] PagamentiEffettuati pagamentiEffettuati)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pagamentiEffettuati).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FKPagamento = new SelectList(db.Pagamenti, "IdPagamento", "IdPagamento", pagamentiEffettuati.FKPagamento);
            return View(pagamentiEffettuati);
        }

        // GET: PagamentiEffettuati/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagamentiEffettuati pagamentiEffettuati = db.PagamentiEffettuati.Find(id);
            if (pagamentiEffettuati == null)
            {
                return HttpNotFound();
            }
            return View(pagamentiEffettuati);
        }

        // POST: PagamentiEffettuati/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PagamentiEffettuati pagamentiEffettuati = db.PagamentiEffettuati.Find(id);
            db.PagamentiEffettuati.Remove(pagamentiEffettuati);
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
