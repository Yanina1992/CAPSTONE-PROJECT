using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CAPSTONE_PROJECT.Controllers
{
    public class StudentiController : Controller
    {
        private Models.Context db = new Models.Context();
        // GET: Studenti
        public ActionResult Index()
        {
            return View();
        }
    }
}