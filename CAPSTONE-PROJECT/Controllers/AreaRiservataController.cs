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
    [Authorize(Roles = "Admin")]
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
            //Student will be categorized in a list based on the age and whether they follow a full-time or part-time schedule
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

        //There a is dedicated view for each list, allowing the admin to review each case
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

        //If everything is okay, every list will be saved in the "listaClassi" as a class entity
        public JsonResult SalvaClassi()
        {
            GestioneClassi();

            //Every class must have at least 5 students
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

            //Retrieve all the "AnnoScolastico" values from the database
            var listaAnnoScDb = db.Classi.Select(m => new { m.AnnoScolastico }).ToList();
            //Save the input value that admin had insert in the view, which is the parameter "annoSc", in an "anno" variable, just to initialize it (it could change its value...)
            var anno = annoSc;
            //Save this value in TempData, as it will be needed in another method
            TempData["anno"] = anno;

            //For each item in the list of years retrieved from the database...
            foreach (var item2 in listaAnnoScDb)
                {
                //if the value of "AnnoScolastico" item is different from the parameter "annoSc"...
                if (item2.AnnoScolastico != annoSc)
                {
                    //the "anno" value remains unchamged;
                    anno = annoSc;
                }
                //if the value of "AnnoScolatico" item is the same from the parameter "annoSc"...
                else if (item2.AnnoScolastico == annoSc)
                {
                    //the "anno" value will be set to an empty string...
                    anno = "";
                }
             }

            //and if "anno" remains unchanged, it indicates that it does not match with any other value in the database.
            //In this case, all the classes in the "listaClassi" can be saved in the database;
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
            //but if "anno" is set as an empty string, it means that classes have already been generated in the database for the year specified by the parameter (annoSc).
            //Therefore, classes won't be saved in the database.
            else
            {
                TempData["ErrorMessage"] = "Sono già state generate delle classi per l'anno scolastico inserito.";
            }
            return Json("success");
        }

        [HttpPost]
        public JsonResult SalvaStudenti()
        {
            GestioneClassi();
            SalvaClassi();

            var anno = TempData["anno"].ToString();

            foreach (var item in classiPrimeTP)
            {
                item.DomandaAccolta = true;
                var fkDom = db.Alunni.Select(m => new { m.FKDomandaIscrizione }).Where(m => m.FKDomandaIscrizione == item.IdDomanda).FirstOrDefault();

                if (fkDom == null)
                {
                    Alunni alunno = new Alunni();
                    alunno.FKDomandaIscrizione = item.IdDomanda;
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
                else if (fkDom != null)
                {
                    //fk è diverso da null e quindi mi seleziono l'alunno e ne modifico la classe
                    var alunno = db.Alunni.Where(m => m.FKDomandaIscrizione == item.IdDomanda).FirstOrDefault();
                    var findClasse = db.Classi.Where(m => m.Anno == "1" && m.Sezione == "A" && m.AnnoScolastico == anno).FirstOrDefault();
                    var findIdClasse = findClasse.IdClasse;
                    alunno.FKClasse = findIdClasse;

                    if (ModelState.IsValid)
                    {
                        db.Entry(alunno).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
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
                else if (fkDom != null)
                {
                    //fk è diverso da null e quindi mi seleziono l'alunno e ne modifico la classe
                    var alunno = db.Alunni.Where(m => m.FKDomandaIscrizione == item.IdDomanda).FirstOrDefault();
                    var findClasse = db.Classi.Where(m => m.Anno == "1" && m.Sezione == "B" && m.AnnoScolastico == anno).FirstOrDefault();
                    var findIdClasse = findClasse.IdClasse;
                    alunno.FKClasse = findIdClasse;

                    if (ModelState.IsValid)
                    {
                        db.Entry(alunno).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
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
                else if (fkDom != null)
                {
                    //fk è diverso da null e quindi mi seleziono l'alunno e ne modifico la classe
                    var alunno = db.Alunni.Where(m => m.FKDomandaIscrizione == item.IdDomanda).FirstOrDefault();
                    var findClasse = db.Classi.Where(m => m.Anno == "2" && m.Sezione == "A" && m.AnnoScolastico == anno).FirstOrDefault();
                    var findIdClasse = findClasse.IdClasse;
                    alunno.FKClasse = findIdClasse;

                    if (ModelState.IsValid)
                    {
                        db.Entry(alunno).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
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
                else if (fkDom != null)
                {
                    //fk è diverso da null e quindi mi seleziono l'alunno e ne modifico la classe
                    var alunno = db.Alunni.Where(m => m.FKDomandaIscrizione == item.IdDomanda).FirstOrDefault();
                    var findClasse = db.Classi.Where(m => m.Anno == "2" && m.Sezione == "B" && m.AnnoScolastico == anno).FirstOrDefault();
                    var findIdClasse = findClasse.IdClasse;
                    alunno.FKClasse = findIdClasse;

                    if (ModelState.IsValid)
                    {
                        db.Entry(alunno).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
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
                else if (fkDom != null)
                {
                    //fk è diverso da null e quindi mi seleziono l'alunno e ne modifico la classe
                    var alunno = db.Alunni.Where(m => m.FKDomandaIscrizione == item.IdDomanda).FirstOrDefault();
                    var findClasse = db.Classi.Where(m => m.Anno == "3" && m.Sezione == "A" && m.AnnoScolastico == anno).FirstOrDefault();
                    var findIdClasse = findClasse.IdClasse;
                    alunno.FKClasse = findIdClasse;

                    if (ModelState.IsValid)
                    {
                        db.Entry(alunno).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
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
                else if (fkDom != null)
                {
                    //fk è diverso da null e quindi mi seleziono l'alunno e ne modifico la classe
                    var alunno = db.Alunni.Where(m => m.FKDomandaIscrizione == item.IdDomanda).FirstOrDefault();
                    var findClasse = db.Classi.Where(m => m.Anno == "3" && m.Sezione == "B" && m.AnnoScolastico == anno).FirstOrDefault();
                    var findIdClasse = findClasse.IdClasse;
                    alunno.FKClasse = findIdClasse;

                    if (ModelState.IsValid)
                    {
                        db.Entry(alunno).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }

            return Json("success");
        }
    }
}            