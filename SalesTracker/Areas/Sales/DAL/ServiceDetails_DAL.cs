using SalesTracker.Areas.Sales.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SalesTracker.Areas.Sales.DAL
{
    public class ServiceDetails_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["Contact_Info"].ToString();

        public bool InserServiceDetails(Service Service)
        {
            try
            {
                int id = 0;
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    SqlCommand Command = new SqlCommand("sp_InsertServiceDetails",connection);
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@Service_ID", Service.Service_ID);
                    Command.Parameters.AddWithValue("@Service_Type",Service.Service_Type);
                    Command.Parameters.AddWithValue("@Rate", Service.Rate);
                    Command.Parameters.AddWithValue("@Primium_Rate", Service.Amount);
                    connection.Open();
                    id = Command.ExecuteNonQuery();
                    connection.Close();
                }
                if(id > 0)
                {
                    return true;
                }
               
            }
            catch(Exception ex)
            {
                return false;
            }
            return false;
        }

        public bool UpdateServiceDetails(Service service)
        {
            try
            {
                int ChkQryExecution = 0;
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    SqlCommand Command = new SqlCommand("sp_UpdateServiceDetails", connection);
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@Service_ID",service.Service_ID);
                    Command.Parameters.AddWithValue("@Service_Type", service.Service_Type);
                    Command.Parameters.AddWithValue("@Rate", service.Rate);
                    Command.Parameters.AddWithValue("@Primium_Rate", service.Amount);
                    connection.Open();
                    ChkQryExecution = Command.ExecuteNonQuery();
                    connection.Close();

                    if(ChkQryExecution > 0)
                    {
                        return true;
                    }
                }

            }
            catch(Exception ex)
            {
                return false;
            }
            return false;
        }


        public List<Service> SelectServiceDetails(string Service_Type)
        {
            List<Service> serviceLst = new List<Service>();

            try
            {
              
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    SqlCommand Command = new SqlCommand("sp_SelectServiceByServiceType", connection);
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@Service_Type", Service_Type);
                 
                    SqlDataAdapter sqlDA = new SqlDataAdapter(Command);
                    DataTable dtService = new DataTable();
                    connection.Open();
                    sqlDA.Fill(dtService);
                    connection.Close();
                    foreach (DataRow dr in dtService.Rows)
                    {
                        serviceLst.Add(new Service
                        {
                            Service_ID =dr["Service_ID"].ToString(),
                            Service_Type = dr["Service_Type"].ToString(),
                            Rate = dr["Rate"].ToString(),
                            Amount = Convert.ToString(dr["Primium_Rate"]),
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                return serviceLst;
            }
            return serviceLst;
        }
    }
}