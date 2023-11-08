using CAPSTONE_PROJECT.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace CAPSTONE_PROJECT.Controllers
{
    public class AreaRiservataController : Controller
    {
        private Models.Context db = new Models.Context();

        List<DomandeIscrizione> classiPrimeTP = new List<DomandeIscrizione>();
        List<DomandeIscrizione> classiPrimeTC = new List<DomandeIscrizione>();

        List<DomandeIscrizione> classiSecondeTP = new List<DomandeIscrizione>();
        List<DomandeIscrizione> classiSecondeTC = new List<DomandeIscrizione>();

        List<DomandeIscrizione> classiTerzeTP = new List<DomandeIscrizione>();
        List<DomandeIscrizione> classiTerzeTC = new List<DomandeIscrizione>();

        List<DomandeIscrizione> alunniPrimeInEccesso = new List<DomandeIscrizione>();
        List<DomandeIscrizione> alunniSecondeInEccesso = new List<DomandeIscrizione>();
        List<DomandeIscrizione> alunniTerzeInEccesso = new List<DomandeIscrizione>();

        List<Classi> listaClassi = new List<Classi>();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GestioneClassi()
        {
            foreach (var item in db.DomandeIscrizione)
            {
                if (item.Eta == "11")
                {
                    if (item.Mensa == true)
                    {
                        if (classiPrimeTP.Count < 10)
                        {
                            classiPrimeTP.Add(item);
                        }
                    }
                    else if (item.Mensa != true)
                    {
                        if (classiPrimeTC.Count < 10)
                        {
                            classiPrimeTC.Add(item);
                        }
                    }
                    else
                    {
                        alunniPrimeInEccesso.Add(item);
                    }
                }
                else if (item.Eta == "12")
                {
                    if (item.Mensa == true)
                    {
                        if (classiSecondeTP.Count < 10)
                        {
                            classiSecondeTP.Add(item);
                        }
                    }
                    else if (item.Mensa != true)
                    {
                        if (classiSecondeTC.Count < 10)
                        {
                            classiSecondeTC.Add(item);
                        }
                    }
                    else
                    {
                        alunniSecondeInEccesso.Add(item);
                    }
                }
                else if (item.Eta == "13")
                {
                    if (item.Mensa == true)
                    {
                        if (classiTerzeTP.Count < 10)
                        {
                            classiTerzeTP.Add(item);
                        }
                    }
                    else if (item.Mensa != true)
                    {
                        if (classiTerzeTC.Count < 10)
                        {
                            classiTerzeTC.Add(item);
                        }
                    }
                    else
                    {
                        alunniTerzeInEccesso.Add(item);
                    }
                }
                else
                {
                    ViewBag.ErrorMessage("Incongruenza con l'età dell'alunno relativo alla domanda" + item.IdDomanda);
                };

            }

            return View();
        }
        public ActionResult GestionePrimeTP()
        {
            GestioneClassi();
            return View(classiPrimeTP.ToList());
        }
        public ActionResult GestionePrimeTC()
        {
            GestioneClassi();
            return View(classiPrimeTC.ToList());
        }
        public ActionResult GestioneSecondeTP()
        {
            GestioneClassi();
            return View(classiSecondeTP.ToList());
        }
        public ActionResult GestioneSecondeTC()
        {
            GestioneClassi();
            return View(classiSecondeTC.ToList());
        }
        public ActionResult GestioneTerzeTP()
        {
            GestioneClassi();
            return View(classiTerzeTP.ToList());
        }
        public ActionResult GestioneTerzeTC()
        {
            GestioneClassi();
            return View(classiTerzeTC.ToList());
        }
        public JsonResult SalvaClassi()
        {
            GestioneClassi();

            if (classiPrimeTP.Count > 5)
            {
                Classi PrimaA = new Classi();
                PrimaA.Anno = "1";
                PrimaA.Sezione = "A";

                listaClassi.Add(PrimaA);
            }

            if (classiPrimeTC.Count > 5)
            {
                Classi PrimaB = new Classi();
                PrimaB.Anno = "1";
                PrimaB.Sezione = "B";

                listaClassi.Add(PrimaB);
            }

            if (classiSecondeTP.Count > 5)
            {
                Classi SecondaA = new Classi();
                SecondaA.Anno = "2";
                SecondaA.Sezione = "A";

                listaClassi.Add(SecondaA);
            }

            if (classiSecondeTC.Count > 5)
            {
                Classi SecondaB = new Classi();
                SecondaB.Anno = "2";
                SecondaB.Sezione = "B";

                listaClassi.Add(SecondaB);
            }

            if (classiTerzeTP.Count > 5)
            {
                Classi TerzaA = new Classi();
                TerzaA.Anno = "3";
                TerzaA.Sezione = "A";

                listaClassi.Add(TerzaA);
            }

            if (classiTerzeTC.Count > 5)
            {
                Classi TerzaB = new Classi();
                TerzaB.Anno = "3";
                TerzaB.Sezione = "B";

                listaClassi.Add(TerzaB);
            }

            return Json(listaClassi, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SalvaC(string annoSc)
        {
            GestioneClassi();
            SalvaClassi();

            //Save classes
            var listaAnnoScDb = db.Classi.Select(m => new { m.AnnoScolastico }).ToList();
            var anno = annoSc;
            TempData["anno"] = anno;

            foreach (var item2 in listaAnnoScDb)
                {
                if (item2.AnnoScolastico != annoSc)
                {
                    anno = annoSc;
                }
                else if (item2.AnnoScolastico == annoSc)
                {
                    anno = "";
                }
             }

            if (anno != "")
            {
               foreach (var item in listaClassi)
               {
                   if (ModelState.IsValid)
                   {
                      item.AnnoScolastico = anno;
                      try
                      {
                        db.Classi.Add(item);
                        db.SaveChanges();
                        item.ConfermaClasse = true;
                      }
                      catch (Exception ex)
                      {
                        ViewBag.Error = ex.Message;
                      }

                   }
                   else
                   {
                   Exception exception = new Exception();
                   }
               }
            }

            return Json("success");
        }

        [HttpPost]
        public JsonResult SalvaStudenti()
        {
            GestioneClassi();
            SalvaClassi();
            foreach (var item in classiPrimeTP)
            {
                item.DomandaAccolta = true;
                var fkDom = db.Alunni.Select(m => new { m.FKDomandaIscrizione }).Where(m => m.FKDomandaIscrizione == item.IdDomanda).FirstOrDefault();

                if (fkDom == null)
                {
                    Alunni alunno = new Alunni();
                    alunno.FKDomandaIscrizione = item.IdDomanda;
                    var anno = TempData["anno"].ToString();
                    var findClasse = db.Classi.Where(m => m.Anno == "1" && m.Sezione == "A" && m.AnnoScolastico == anno).FirstOrDefault();
                    findClasse.ConfermaClasse = true;
                    var findIdClasse = findClasse.IdClasse;
                    alunno.FKClasse = findIdClasse;

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            db.Alunni.Add(alunno);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Error = ex.Message;
                        }
                    }
                }
                else
                {
                    //fk è diverso da null e quindi mi seleziono l'alunno e ne modifico la classe
                }
            }

            foreach (var item in classiPrimeTC)
            {
                item.DomandaAccolta = true;
                var fkDom = db.Alunni.Select(m => new { m.FKDomandaIscrizione }).Where(m => m.FKDomandaIscrizione == item.IdDomanda).FirstOrDefault();

                if (fkDom == null)
                {
                    Alunni alunno = new Alunni();
                    alunno.FKDomandaIscrizione = item.IdDomanda;
                    var anno = TempData["anno"].ToString();
                    var findClasse = db.Classi.Where(m => m.Anno == "1" && m.Sezione == "B" && m.AnnoScolastico == anno).FirstOrDefault();
                    findClasse.ConfermaClasse = true;
                    var findIdClasse = findClasse.IdClasse;
                    alunno.FKClasse = findIdClasse;

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            db.Alunni.Add(alunno);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Error = ex.Message;
                        }
                    }
                }
            }

            foreach (var item in classiSecondeTP)
            {
                item.DomandaAccolta = true;
                var fkDom = db.Alunni.Select(m => new { m.FKDomandaIscrizione }).Where(m => m.FKDomandaIscrizione == item.IdDomanda).FirstOrDefault();

                if (fkDom == null)
                {
                    Alunni alunno = new Alunni();
                    alunno.FKDomandaIscrizione = item.IdDomanda;
                    var anno = TempData["anno"].ToString();
                    var findClasse = db.Classi.Where(m => m.Anno == "2" && m.Sezione == "A" && m.AnnoScolastico == anno).FirstOrDefault();
                    findClasse.ConfermaClasse = true;
                    var findIdClasse = findClasse.IdClasse;
                    alunno.FKClasse = findIdClasse;

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            db.Alunni.Add(alunno);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Error = ex.Message;
                        }
                    }
                }
            }

            foreach (var item in classiSecondeTC)
            {
                item.DomandaAccolta = true;
                var fkDom = db.Alunni.Select(m => new { m.FKDomandaIscrizione }).Where(m => m.FKDomandaIscrizione == item.IdDomanda).FirstOrDefault();

                if (fkDom == null)
                {
                    Alunni alunno = new Alunni();
                    alunno.FKDomandaIscrizione = item.IdDomanda;
                    //riprendi da qui
                    var anno = TempData["anno"].ToString();
                    var findClasse = db.Classi.Where(m => m.Anno == "2" && m.Sezione == "B" && m.AnnoScolastico == anno).FirstOrDefault();
                    findClasse.ConfermaClasse = true;
                    var findIdClasse = findClasse.IdClasse;
                    alunno.FKClasse = findIdClasse;

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            db.Alunni.Add(alunno);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Error = ex.Message;
                        }
                    }
                }
            }

            foreach (var item in classiTerzeTP)
            {
                item.DomandaAccolta = true;
                var fkDom = db.Alunni.Select(m => new { m.FKDomandaIscrizione }).Where(m => m.FKDomandaIscrizione == item.IdDomanda).FirstOrDefault();

                if (fkDom == null)
                {
                    Alunni alunno = new Alunni();
                    alunno.FKDomandaIscrizione = item.IdDomanda;
                    var anno = TempData["anno"].ToString();
                    var findClasse = db.Classi.Where(m => m.Anno == "3" && m.Sezione == "A" && m.AnnoScolastico == anno).FirstOrDefault();
                    findClasse.ConfermaClasse = true;
                    var findIdClasse = findClasse.IdClasse;
                    alunno.FKClasse = findIdClasse;

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            db.Alunni.Add(alunno);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Error = ex.Message;
                        }
                    }
                }
            }

            foreach (var item in classiTerzeTC)
            {
                item.DomandaAccolta = true;
                var fkDom = db.Alunni.Select(m => new { m.FKDomandaIscrizione }).Where(m => m.FKDomandaIscrizione == item.IdDomanda).FirstOrDefault();

                if (fkDom == null)
                {
                    Alunni alunno = new Alunni();
                    alunno.FKDomandaIscrizione = item.IdDomanda;
                    var anno = TempData["anno"].ToString();
                    var findClasse = db.Classi.Where(m => m.Anno == "3" && m.Sezione == "B" && m.AnnoScolastico == anno).FirstOrDefault();
                    findClasse.ConfermaClasse = true;
                    var findIdClasse = findClasse.IdClasse;
                    alunno.FKClasse = findIdClasse;

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            db.Alunni.Add(alunno);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Error = ex.Message;
                        }
                    }
                }
            }


            return Json("success");
        }
    }
}            