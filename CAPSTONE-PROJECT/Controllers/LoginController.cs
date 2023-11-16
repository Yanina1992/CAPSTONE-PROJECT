using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CAPSTONE_PROJECT.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;

namespace CAPSTONE_PROJECT.Controllers
{
    public class LoginController : Controller
    {
        private Context db = new Context();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users us)
        { 
            if (ModelState.IsValid)
            {
                Users user = db.Users.Where(u => u.Username == us.Username && u.Password == us.Password).FirstOrDefault();
                FormsAuthentication.SetAuthCookie(user.Username, false);
                db.SaveChanges();

                Session["NomeUser"] = user.Username;

                return RedirectToAction("Index", "AreaRiservata");
            }

            return View();
        }
        
    }
}