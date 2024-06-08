using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesTracker.Areas.Sales.DAL;
using SalesTracker.Areas.Sales.Models;

namespace SalesTracker.Areas.Sales.Controllers
{
    public class InsertServiceDetailsController : Controller
    {
        ServiceDetails_DAL _insertServiceDetails_DAL = new ServiceDetails_DAL();
        // GET: Sales/InsertServiceDetails
        public ActionResult Index(Service service)
        {
            
            return View();
        }
        public ActionResult InsertService(Service service)
        {
            bool IsInserted = false;
            try
            {
                if (service.Service_Type != null && service.Service_ID != null && service.Rate.ToString() != null && service.Amount != null)
                {
                    //if (ModelState.IsValid)
                    //{
                        IsInserted = _insertServiceDetails_DAL.InserServiceDetails(service);
                        if (IsInserted)
                        {
                            TempData["SuccessMessage"] = "Service Detail Added Successfully...!";
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Service Detail Already Added/Unable To Add the Service detail.";
                        }
                    //}
                }
            }
            catch (Exception ex)
            {

            }
            return View("~/Areas/Sales/Views/InsertService/InsertService.cshtml");
        }
    }
}