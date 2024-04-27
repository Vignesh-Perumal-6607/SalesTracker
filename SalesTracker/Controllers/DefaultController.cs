using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesTracker.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string username, string password)
        {
            return RedirectToAction("Index", "Sales/SalesTracker");
        }
    }
}