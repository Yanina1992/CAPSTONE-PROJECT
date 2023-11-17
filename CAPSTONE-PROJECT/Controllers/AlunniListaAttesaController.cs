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
    public class AlunniListaAttesaController : Controller
    {
        private Context db = new Context();

        // GET: AlunniListaAttesa
        public ActionResult Index()
        {
            var alunniListaAttesa = db.AlunniListaAttesa.Include(a => a.DomandeIscrizione);
            return View(alunniListaAttesa.ToList());
        }

        // GET: AlunniListaAttesa/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlunniListaAttesa alunniListaAttesa = db.AlunniListaAttesa.Find(id);
            if (alunniListaAttesa == null)
            {
                return HttpNotFound();
            }
            return View(alunniListaAttesa);
        }

        // GET: AlunniListaAttesa/Create
        public ActionResult Create()
        {
            ViewBag.FKDomandaIscrizione = new SelectList(db.DomandeIscrizione, "IdDomanda", "NomeAlunno");
            return View();
        }

        // POST: AlunniListaAttesa/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAlunnoLista,FKDomandaIscrizione")] AlunniListaAttesa alunniListaAttesa)
        {
            if (ModelState.IsValid)
            {
                db.AlunniListaAttesa.Add(alunniListaAttesa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FKDomandaIscrizione = new SelectList(db.DomandeIscrizione, "IdDomanda", "NomeAlunno", alunniListaAttesa.FKDomandaIscrizione);
            return View(alunniListaAttesa);
        }

        // GET: AlunniListaAttesa/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlunniListaAttesa alunniListaAttesa = db.AlunniListaAttesa.Find(id);
            if (alunniListaAttesa == null)
            {
                return HttpNotFound();
            }
            ViewBag.FKDomandaIscrizione = new SelectList(db.DomandeIscrizione, "IdDomanda", "NomeAlunno", alunniListaAttesa.FKDomandaIscrizione);
            return View(alunniListaAttesa);
        }

        // POST: AlunniListaAttesa/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAlunnoLista,FKDomandaIscrizione")] AlunniListaAttesa alunniListaAttesa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alunniListaAttesa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FKDomandaIscrizione = new SelectList(db.DomandeIscrizione, "IdDomanda", "NomeAlunno", alunniListaAttesa.FKDomandaIscrizione);
            return View(alunniListaAttesa);
        }

        // GET: AlunniListaAttesa/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlunniListaAttesa alunniListaAttesa = db.AlunniListaAttesa.Find(id);
            if (alunniListaAttesa == null)
            {
                return HttpNotFound();
            }
            return View(alunniListaAttesa);
        }

        // POST: AlunniListaAttesa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AlunniListaAttesa alunniListaAttesa = db.AlunniListaAttesa.Find(id);
            db.AlunniListaAttesa.Remove(alunniListaAttesa);
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
