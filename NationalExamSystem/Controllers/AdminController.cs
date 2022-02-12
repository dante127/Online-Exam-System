using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NationalExamSystem.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(string email , string password)
        {
            if (email.Equals("admin@admin.com") && password.Equals("admin"))
            {
                return RedirectToAction("index");
            }
            else
            {
                ViewBag.msg = " Invalid Email or password ";

                return View("Login");
            }
        }




    }
}