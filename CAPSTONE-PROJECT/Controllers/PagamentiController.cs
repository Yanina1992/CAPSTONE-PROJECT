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
    public class PagamentiController : Controller
    {
        private Context db = new Context();

        // GET: Pagamenti
        public ActionResult Index()
        {
            return View(db.Pagamenti.ToList());
        }

        // GET: Pagamenti/Details/5
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

        // GET: Pagamenti/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pagamenti/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Pagamenti/Edit/5
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

        // POST: Pagamenti/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Pagamenti/Delete/5
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

        // POST: Pagamenti/Delete/5
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

        public ActionResult CalcoloPagamenti()
        {
            var alunni = db.Alunni.Select(m => new { m.FKDomandaIscrizione, m.IdAlunno }).ToList();
            foreach (var item in alunni)
            {
                Pagamenti pagamento = new Pagamenti();

            }
            return View();
        }
    }
}
