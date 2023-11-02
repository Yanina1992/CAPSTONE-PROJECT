using CAPSTONE_PROJECT.Models;
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

        // GET: AreaRiservata
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

        //COMMENTO PER RINO:
            //Con l'Action Result "SalvaClassi", in GET voglio stampare la listaClassi (lo faccio con una ViewBag, e funziona correttamente);
            //in POST, invece, vorrei salvare con un'iterazione le Classi che ho nella lista sul db, ma non ci riesco.
            //Il metodo "SalvaClassi" dovrebbe essere innescato dall'action link che si trova nella visualizzazione del metodo GestioneClassi di questo stesso controller.
        public ActionResult SalvaClassi()
        {
            GestioneClassi();
            
            if (classiPrimeTP.Count > 5) 
            {
                Classi PrimaA = new Classi();
                PrimaA.Anno = "Prima";
                PrimaA.Sezione = "A";

                listaClassi.Add( PrimaA );
            }

            if (classiPrimeTC.Count > 5)
            {
                Classi PrimaB = new Classi();
                PrimaB.Anno = "Prima";
                PrimaB.Sezione = "B";

                listaClassi.Add(PrimaB);
            }

            if (classiSecondeTP.Count > 5)
            {
                Classi SecondaA = new Classi();
                SecondaA.Anno = "Seconda";
                SecondaA.Sezione = "A";

                listaClassi.Add(SecondaA);
            }

            if (classiSecondeTC.Count > 5)
            {
                Classi SecondaB = new Classi();
                SecondaB.Anno = "Seconda";
                SecondaB.Sezione = "B";

                listaClassi.Add(SecondaB);
            }

            if (classiTerzeTP.Count > 5)
            {
                Classi TerzaA = new Classi();
                TerzaA.Anno = "Terza";
                TerzaA.Sezione = "A";

                listaClassi.Add(TerzaA);
            }

            if (classiTerzeTC.Count > 5)
            {
                Classi TerzaB = new Classi();
                TerzaB.Anno = "Terza";
                TerzaB.Sezione = "B";

                listaClassi.Add(TerzaB);
            }

            Session["ClassiGenerate"] = listaClassi;
            ViewBag.classi = listaClassi;
           
            return View();
        }

        [HttpPost]
        public ActionResult SalvaClassi(Classi classe)
        {
            var listaDaCiclare = Session["ClassiGenerate"];
            //foreach (var classi in listaDaCiclare as Array)
            foreach (var classi in listaClassi)
            {
            //    Classi classe = new Classi();
            //    if (ModelState.IsValid)
            //    {   
            //        db.Classi.Add(classe);
            //    }
            //    try
            //    {
            //    db.SaveChanges();
            //    ViewBag.Message = "Classi salvate con successo";
            //    }
            //    catch (Exception ex)
            //    {
            //    ViewBag.Message = ex.Message;
            //    }
           
            db.Classi.Add(classi);
            db.SaveChanges();
            return View();
            }

            return View();
        }
                //foreach (var item in classiPrimeTP)
                //{
                //    //item.DomandaAccolta = true;
                //    Alunni alunno1A = new Alunni();
                //    alunno1A.FKDomandaIscrizione = item.IdDomanda;
                //    alunno1A.FKClasse = PrimaA.IdClasse;

                //    if (ModelState.IsValid)
                //    {
                //        alunno1A = alunno;
                //        db.Alunni.Add(alunno);

                //        db.SaveChanges();
                //    }
                //}
            }

        }
               

//            if (classiPrimeTC.Count > 5)
//            {
//                Classi PrimaB = new Classi();
//                PrimaB.Anno = "Prima";
//                PrimaB.Sezione = "B";
//                db.Classi.Add(PrimaB);

//                foreach (var item in classiPrimeTC)
//                {
//                    item.DomandaAccolta = true;
//                    Alunni alunno = new Alunni();
//                    db.Alunni.Add(alunno);
//                }

//            }

//            if (classiSecondeTP.Count > 5)
//            {
//                Classi SecondaA = new Classi();
//                SecondaA.Anno = "Seconda";
//                SecondaA.Sezione = "A";
//                db.Classi.Add(SecondaA);

//                foreach (var item in classiSecondeTP)
//                {
//                    item.DomandaAccolta = true;
//                    Alunni alunno = new Alunni();
//                    db.Alunni.Add(alunno);
//                }

//            }

//            if (classiSecondeTC.Count > 5)
//            {
//                Classi SecondaB = new Classi();
//                SecondaB.Anno = "Seconda";
//                SecondaB.Sezione = "B";
//                db.Classi.Add(SecondaB);

//                foreach (var item in classiSecondeTC)
//                {
//                    item.DomandaAccolta = true;
//                    Alunni alunno = new Alunni();
//                    db.Alunni.Add(alunno);
//                }

//            }

//            if (classiTerzeTP.Count > 5)
//            {
//                Classi TerzaA = new Classi();
//                TerzaA.Anno = "Terza";
//                TerzaA.Sezione = "A";
//                db.Classi.Add(TerzaA);

//                foreach (var item in classiTerzeTP)
//                {
//                    item.DomandaAccolta = true;
//                    Alunni alunno = new Alunni();
//                    db.Alunni.Add(alunno);
//                }

//            }

//            if (classiTerzeTC.Count > 5)
//            {
//                Classi TerzaB = new Classi();
//                TerzaB.Anno = "Terza";
//                TerzaB.Sezione = "B";
//                db.Classi.Add(TerzaB);

//                foreach (var item in classiTerzeTC)
//                {
//                    item.DomandaAccolta = true;
//                    Alunni alunno = new Alunni();
//                    db.Alunni.Add(alunno);
//                }

//            }

//            db.SaveChanges();

//            return View();
//        }
//    }
//}