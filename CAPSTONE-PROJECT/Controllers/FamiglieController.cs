using CAPSTONE_PROJECT.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CAPSTONE_PROJECT.Controllers
{
    public class FamiglieController : Controller
    {
        private Models.Context db = new Models.Context();
        // GET: Famiglie
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TrovaPagamenti() 
        {
            return View();
        }

        [HttpPost]
        public JsonResult Pagamento(string cf)
        {
            DomandeIscrizione domanda = db.DomandeIscrizione.FirstOrDefault(e => e.CFPapa == cf || e.CFMamma == cf);
            try
            {
                Alunni alunno = db.Alunni.Where(m => m.FKDomandaIscrizione.ToString() == domanda.IdDomanda.ToString()).FirstOrDefault();

                var bilinguismo = domanda.Bilinguismo;
                var assicurazione = domanda.Assicurazione;
                var isee = domanda.Isee;
                var mensa = domanda.Mensa;
                var trasportoScolastico = domanda.TrasportoScolastico;
                decimal totale = 0;

                Pagamenti pagamenti = new Pagamenti();

                if (bilinguismo == true)
                {
                    pagamenti.Bilinguismo = 450;
                    totale += 450;
                }
                if (assicurazione == true)
                {
                    pagamenti.Assicurazione = 6;
                    totale += 6;
                }

                if (isee < 23120)
                {
                    if (mensa == true) 
                    {
                        pagamenti.Mensa = 30;
                        totale += 30;
                    }
                    if (trasportoScolastico == true)
                    {
                        pagamenti.TrasportoScolastico = 15;
                        totale += 15;
                    }
                }
                else if (isee > 23121 && isee < 27000)
                {
                    if (mensa == true)
                    {
                        pagamenti.Mensa = 40;
                        totale += 40;
                    }
                    if (trasportoScolastico == true)
                    {
                        pagamenti.TrasportoScolastico = 20;
                        totale += 20;
                    }
                }
                else if (isee > 27001 && isee < 31000)
                {
                    if (mensa == true)
                    {
                        pagamenti.Mensa = 50;
                        totale += 50;
                    }
                    if (trasportoScolastico == true)
                    {
                        pagamenti.TrasportoScolastico = 25;
                        totale += 25;
                    }
                }
                else if (isee > 31001 && isee > 40000)
                {
                    if (mensa == true)
                    {
                        pagamenti.Mensa = 60;
                        totale += 60;
                    }
                    if (trasportoScolastico == true)
                    {
                        pagamenti.TrasportoScolastico = 30;
                        totale += 30;
                    }
                }
                else if (isee > 41000)
                {
                    if (mensa == true)
                    {
                        pagamenti.Mensa = 70;
                        totale += 70;
                    }
                    if (trasportoScolastico == true)
                    {
                        pagamenti.TrasportoScolastico = 35;
                        totale += 35;
                    }
                }
                else
                {
                    Exception exception = new Exception();
                }

                pagamenti.Totale = totale;

                if (ModelState.IsValid)
                {
                    db.Pagamenti.Add( pagamenti );
                    db.SaveChanges();
                }

                alunno.FKPagamento = pagamenti.IdPagamento;

                if (ModelState.IsValid)
                {
                    db.Entry(alunno).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                TempData["totale"] = pagamenti.Totale;
                TempData["id"] = pagamenti.IdPagamento;

            }
            catch (Exception ex) 
            {
                ViewBag.Error = ex.Message;
            }
            return Json("success");
        }
    }
}