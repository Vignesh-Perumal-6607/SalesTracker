using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
/*
    #01 Home page
        Created by Manikandan S on 20-Jan-2024
*/
namespace SalesTracker.Areas.Sales.Controllers
{
    public class SalesTrackerController : Controller
    {
        // GET: Sales/SalesTracker
        public ActionResult Index()
        {
            Session["Username"] = "Admin";

            return View();
        }

        // Billing
        public ActionResult Billing()
        {
            return View();
        }

        // Report
        public ActionResult Report()
        {
            return View();
        }

        public ActionResult ChangeProfile()
        {
            Session["Profile"] = "~/Areas/Sales/Content/userPic.jpg";
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}