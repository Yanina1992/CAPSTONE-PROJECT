using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CAPSTONE_PROJECT.Models;
using Microsoft.Ajax.Utilities;

namespace CAPSTONE_PROJECT.Controllers
{
    public class DomandeIscrizioneController : Controller
    {
        private Models.Context db = new Models.Context();

        // GET: DomandeIscrizione
        public ActionResult Index()
        {
            return View(db.DomandeIscrizione.ToList());
        }

        // GET: DomandeIscrizione/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DomandeIscrizione domandeIscrizione = db.DomandeIscrizione.Find(id);
            if (domandeIscrizione == null)
            {
                return HttpNotFound();
            }
            return View(domandeIscrizione);
        }

        // GET: DomandeIscrizione/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DomandeIscrizione/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDomanda,NomeAlunno,CognomeAlunno,CFAlunno,Eta,Allergie,Bilinguismo,Assicurazione,CFPapa,CFMamma,Isee,DomandaAccolta,Mensa,TrasportoScolastico")] DomandeIscrizione domandeIscrizione, string CFAlunno)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var cercaCF = db.DomandeIscrizione.Where(x => x.CFAlunno == CFAlunno).FirstOrDefault();
                    if (cercaCF == null)
                    {
                        db.DomandeIscrizione.Add(domandeIscrizione);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.MessageError = "Codice fiscale già presente";
                        return View();
                    }
                }
                catch (Exception ex) { }
                }
               

            return View(domandeIscrizione);
        }

        // GET: DomandeIscrizione/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DomandeIscrizione domandeIscrizione = db.DomandeIscrizione.Find(id);
            if (domandeIscrizione == null)
            {
                return HttpNotFound();
            }
            return View(domandeIscrizione);
        }

        // POST: DomandeIscrizione/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDomanda,NomeAlunno,CognomeAlunno,CFAlunno,Eta,Allergie,Bilinguismo,Assicurazione,CFPapa,CFMamma,Isee,DomandaAccolta,Mensa,TrasportoScolastico")] DomandeIscrizione domandeIscrizione, bool? DomandaAccolta, int IdDomanda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(domandeIscrizione).State = EntityState.Modified;
                db.SaveChanges();

                if (DomandaAccolta==true)
                {
                    AggiungiAlunno(IdDomanda);
                }else if (DomandaAccolta == false)
                {
                    AggiungiAlunnoListaAttesa(IdDomanda);
                }else
                {
                    return View();
                }

                return RedirectToAction("Index");
            }
            return View(domandeIscrizione);
        }

        // GET: DomandeIscrizione/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DomandeIscrizione domandeIscrizione = db.DomandeIscrizione.Find(id);
            if (domandeIscrizione == null)
            {
                return HttpNotFound();
            }
            return View(domandeIscrizione);
        }

        // POST: DomandeIscrizione/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DomandeIscrizione domandeIscrizione = db.DomandeIscrizione.Find(id);
            db.DomandeIscrizione.Remove(domandeIscrizione);
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

        public ActionResult AggiungiAlunno(int IdDomanda)
        {
            Alunni alunno = new Alunni();
            alunno.FKDomandaIscrizione = IdDomanda;
            db.Alunni.Add(alunno);
            db.SaveChanges();
            return View();
        }

        public ActionResult AggiungiAlunnoListaAttesa(int IdDomanda)
        {

            AlunniListaAttesa alunno = new AlunniListaAttesa();
            alunno.FKDomandaIscrizione = IdDomanda;
            db.AlunniListaAttesa.Add(alunno);
            db.SaveChanges();
            return View();
        }
    }
}
