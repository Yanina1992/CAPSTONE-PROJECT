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
    public class ClassiController : Controller
    {
        private Context db = new Context();

        // GET: Classi
        public ActionResult Index()
        {
            return View(db.Classi.ToList());
        }

        // GET: Classi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classi classi = db.Classi.Find(id);
            if (classi == null)
            {
                return HttpNotFound();
            }
            return View(classi);
        }

        // GET: Classi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Classi/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdClasse,Anno,Sezione")] Classi classi)
        {
            if (ModelState.IsValid)
            {
                db.Classi.Add(classi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(classi);
        }

        // GET: Classi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classi classi = db.Classi.Find(id);
            if (classi == null)
            {
                return HttpNotFound();
            }
            return View(classi);
        }

        // POST: Classi/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdClasse,Anno,Sezione")] Classi classi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(classi);
        }

        // GET: Classi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classi classi = db.Classi.Find(id);
            if (classi == null)
            {
                return HttpNotFound();
            }
            return View(classi);
        }

        // POST: Classi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Classi classi = db.Classi.Find(id);
            db.Classi.Remove(classi);
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
