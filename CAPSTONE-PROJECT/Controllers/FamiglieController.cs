using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CAPSTONE_PROJECT.Controllers
{
    public class FamiglieController : Controller
    {
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