using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CAPSTONE_PROJECT.Controllers
{
    public class IstitutoController : Controller
    {
        private Models.Context db = new Models.Context();
        // GET: Istituto
        public ActionResult Index()
        {
            return View();
        }
    }
}