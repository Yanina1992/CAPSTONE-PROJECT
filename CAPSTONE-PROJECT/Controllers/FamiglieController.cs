using CAPSTONE_PROJECT.Models;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using System.Web.UI;

namespace CAPSTONE_PROJECT.Controllers
{
    public class FamiglieController : Controller
    {
        private Models.Context db = new Models.Context();
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
                if (domanda != null)
                {
                    try
                    {
                        //Find the student who has a parent with that specific CF value
                        Alunni alunno = db.Alunni.Where(m => m.FKDomandaIscrizione.ToString() == domanda.IdDomanda.ToString()).FirstOrDefault();

                        if (alunno != null)
                        {
                            //Check if this student already has a generated payment:
                            //Case 1: if they don't have a payment, create a new one by calculating all the required services
                            if (alunno.FKPagamento == null)
                            {
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
                                //The Isee value is taken into account to calculate the amout of main services
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

                                //Total amount due to the school
                                pagamenti.Totale = totale;

                                if (ModelState.IsValid)
                                {
                                    db.Pagamenti.Add(pagamenti);
                                    db.SaveChanges();
                                }

                                //Bind the payment to its student in the db
                                alunno.FKPagamento = pagamenti.IdPagamento;

                                if (ModelState.IsValid)
                                {
                                    db.Entry(alunno).State = System.Data.Entity.EntityState.Modified;
                                    db.SaveChanges();
                                }

                                //Send data to the view
                                TempData["totale"] = (pagamenti.Totale).ToString();
                                TempData["id"] = pagamenti.IdPagamento;
                            }
                            //Case 2: If they do have a payment, take data from db...
                            else
                            {
                                var totale = alunno.Pagamenti.Totale;
                                var id = alunno.FKPagamento;

                                //and send them to the view
                                TempData["totale"] = totale.ToString();
                                TempData["id"] = id;
                            }
                        }
                        else
                        {
                            TempData["alunnoNonTrovato"] = "Il codice fiscale inserito non corrisponde a nessun alunno iscritto; è possibile che la domanda di ammissione non sia ancora stata accettata dall'istituto. Contattare la segreteria per maggiori informazioni.";
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                    }
                }
                else
                {
                    TempData["cfNotFound"] = "Il Codice Fiscale inserito non è associato a nessun alunno. Riprovare o contattare la segreteria per maggiori informazioni.";
                }
            return Json("success");
        }
    }
}