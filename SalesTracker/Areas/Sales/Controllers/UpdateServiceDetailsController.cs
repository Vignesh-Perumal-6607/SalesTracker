using SalesTracker.Areas.Sales.DAL;
using SalesTracker.Areas.Sales.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesTracker.Areas.Sales.Controllers
{
    public class UpdateServiceDetailsController : Controller
    {
        ServiceDetails_DAL _UpdateServiceDetails_DAL = new ServiceDetails_DAL();
        // GET: Sales/UpdateServiceDetails
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult UpdateServices(Service service)
        {
            bool IsUpdated = false;
            try
            {
                if (service.Service_Type != null && service.Service_ID != null && service.Rate.ToString() != null && service.Amount != null)
                {
                    IsUpdated = _UpdateServiceDetails_DAL.UpdateServiceDetails(service);
                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Service Detail Updated Successfully...!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Service Detail Not in List/Unable To Update the Person detail.";
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View("~/Areas/Sales/Views/UpdateServices/UpdateService.cshtml");
        }

       
        public ActionResult SelectServiceValue(string Service_Type)
        {
            
                Service service = _UpdateServiceDetails_DAL.SelectServiceDetails(Service_Type).FirstOrDefault();
                if (service == null)
                {
                    TempData["InfoMessage"] = "Service Type is not available" + Service_Type.ToString();
                    return RedirectToAction("Index");
                }
            Debug.WriteLine($"Amount: {service.Amount}");
            Debug.WriteLine($"Service_ID: {service.Service_ID}");
            Debug.WriteLine($"Service_Type: {service.Service_Type}");
            Debug.WriteLine($"Rate: {service.Rate}");
            return View("~/Areas/Sales/Views/UpdateServices/UpdateService.cshtml", service);
            }
           
        
    }
}