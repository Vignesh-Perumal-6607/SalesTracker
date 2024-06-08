using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesTracker.Areas.Sales.Models;
using SalesTracker.Areas.Sales.ViewModel;
namespace SalesTracker.Areas.Sales.Controllers
{
    public class InvoiceController : Controller
    {
        //private readonly ApplicationDbContext _context;
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        //connection string
        void connectionString()
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Contact_Info"].ToString();
        }

        // GET: Sales/Invoice
        public ActionResult Index(string Quantity, string CusType, string Service_Type, string Service_ID)
        {
            List<Service> SalesList = new List<Service>();
            if (Session["SalesData"] != null)
            {
                SalesList = (List<Service>)Session["SalesData"];
            }
            if (TempData["ActionExecuted"] != null)
            {
                //TempData["ActionExecuted"] = true;
                // Set a flag in TempData to indicate that the action has been executed

                if (!string.IsNullOrEmpty(Service_Type))
                {

                    connectionString();
                    con.Open();
                    com.Connection = con;
                    com.CommandText = "select * from TBLService_details where Service_Type='" + Service_Type + "' or Service_ID='" + Service_ID + "'";
                    //dr = com.ExecuteReader();
                    SqlDataAdapter sqlDA = new SqlDataAdapter(com);
                    DataTable dtPeople = new DataTable();
                    // con.Open();
                    sqlDA.Fill(dtPeople);
                    con.Close();

                    foreach (DataRow dr in dtPeople.Rows)
                    {
                        if (CusType == "Premium")
                        {
                            SalesList.Add(new Service
                            {
                                Service_ID = dr["Service_ID"].ToString(),
                                Service_Type = dr["Service_Type"].ToString(),
                                Rate = dr["Primium_Rate"].ToString(),
                                Quantity = Quantity,
                                CusType = CusType,
                                Amount = Convert.ToString(Convert.ToInt32(Quantity) * Convert.ToInt32(dr["Primium_Rate"]))
                            });
                        }
                        else
                        {
                            SalesList.Add(new Service
                            {
                                Service_ID = dr["Service_ID"].ToString(),
                                Service_Type = dr["Service_Type"].ToString(),
                                Rate = dr["Rate"].ToString(),
                                Quantity = Quantity,
                                CusType = CusType,
                                Amount = Convert.ToString(Convert.ToInt32(Quantity) * Convert.ToInt32(dr["Rate"]))
                            });
                        }
                    }
                    Session["SalesData"] = SalesList;
                }
                TempData.Keep("ActionExecuted");
                return View(SalesList);
            }
            TempData["ActionExecuted"] = true;
            return View(SalesList);
        }
        public ActionResult resetDataTable(int button, string ReSetType, string Qty)
        {
            List<Service> lstRows = (List<Service>)Session["SalesData"];
            try
            {
                if (TempData["ActionExecuted"] != null)
                {
                    switch (ReSetType.ToUpper().Trim())
                    {
                        case "REMOVE" :
                        lstRows = (List<Service>)Session["SalesData"];
                        lstRows.RemoveAt(button - 1);
                        TempData.Keep("ActionExecuted");
                        return View("Invoice", lstRows);
        
                        case "UPDATE":
                            lstRows = (List<Service>)Session["SalesData"];
                            lstRows[button-1].Quantity= Qty.Split(',')[0];
                            lstRows[button - 1].Amount = Qty.Split(',')[1];
                            return View("Invoice", lstRows);
                    }
            }
            }   
            catch (Exception ex)
            {

            }
            TempData["ActionExecuted"] = true;
            return View("Invoice", lstRows);
        }

        [HttpGet]
        public ActionResult ServiceDrop()
        {
            try
            {
                // Fetch distinct Service_Type values from the database
                List<SelectListItem> serviceTypes = new List<SelectListItem>();
                // Pass the list to your view  
                
                connectionString();
                con.Open();
                com.Connection = con;
                com.CommandText = "select * from TBLService_details";
                //dr = com.ExecuteReader();
                
                SqlDataAdapter sqlDA = new SqlDataAdapter(com);
                DataTable dtPeople = new DataTable();
                
                sqlDA.Fill(dtPeople);
                con.Close();
                
                foreach (DataRow dr in dtPeople.Rows)
                {

                    serviceTypes.Add( new SelectListItem { 
                        //Service_ID = dr["Service_ID"].ToString(),
                        Value = dr["Service_Type"].ToString(),
                        Text = dr["Service_Type"].ToString()
                    });

                }
                

                return Json(serviceTypes, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        [HttpGet]
        public ActionResult CustemerType()
        {
            try
            {
                List<SelectListItem> CusType = new List<SelectListItem>();

                connectionString();
                con.Open();
                com.Connection = con;
                com.CommandText = "Select * from TBLService_details";
                DataTable DtCusType = new DataTable();
                SqlDataAdapter sqlDBA = new SqlDataAdapter(com);
                sqlDBA.Fill(DtCusType);
                con.Close();
                foreach (DataRow dr in DtCusType.Rows)
                {

                    CusType.Add(new SelectListItem
                    {
                        //Service_ID = dr["Service_ID"].ToString(),
                        Value = dr["Service_Type"].ToString(),
                        Text = dr["Service_Type"].ToString()
                    });

                }
                return Json(CusType, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

            }
            return View();
        }
    }
}