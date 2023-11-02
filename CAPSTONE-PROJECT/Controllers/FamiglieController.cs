using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public ActionResult TrovaPagamenti(string CodiceFiscale) 
        {
            return View();
        }
    }
}