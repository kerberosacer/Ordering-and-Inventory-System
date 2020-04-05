using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace InventoryTest
{
    public static class DALforStoredProc
    {
        public static void ResetInventoryLevel(int productID)
        {
            string connectionstring= System.Configuration.ConfigurationManager.ConnectionStrings["InventoryTest.Properties.Settings.InventoryDBConnectionString"].ConnectionString;
            using (SqlConnection sqlconn = new SqlConnection(connectionstring))
            {
                using (SqlCommand sqlcomm = new SqlCommand("dbo.ResetInventoryLevel", sqlconn))
                {
                    sqlcomm.Parameters.Add(new SqlParameter("@ProductID", productID));
                    sqlcomm.CommandType = CommandType.StoredProcedure;
                    sqlconn.Open();
                    sqlcomm.ExecuteNonQuery();
                }
            }
                
        }

        public static DataTable GetInventoryReport()
        {
            string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["InventoryTest.Properties.Settings.InventoryDBConnectionString"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection sqlconn = new SqlConnection(connectionstring))
            {
                using (SqlCommand sqlcomm = new SqlCommand("dbo.SelectInventoryReport", sqlconn))
                {
                    sqlcomm.CommandType = CommandType.StoredProcedure;
                    sqlconn.Open();
                    SqlDataAdapter db = new SqlDataAdapter(sqlcomm);
                    
                    try
                    {
                        db.Fill(dt);
                    }
                    catch(Exception)
                    {

                    }
                    return dt;
                }
            }
        }

        public static DataTable GetOrderItem(DateTime datefrom,DateTime dateto)
        {
            DataTable dt=new DataTable();
            string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["InventoryTest.Properties.Settings.InventoryDBConnectionString"].ConnectionString;
            using (SqlConnection sqlconn = new SqlConnection(connectionstring))
            {
                using (SqlCommand sqlcomm = new SqlCommand("dbo.OrderItemFilterByDate",sqlconn))
                {
                    sqlcomm.CommandType = CommandType.StoredProcedure;
                    string datefromformattedforsql = datefrom.ToString("yyyy-MM-dd");
                    SqlParameter fromdateparam = new SqlParameter("@DateFrom", datefromformattedforsql);
                    fromdateparam.SqlDbType = SqlDbType.Date;
                    sqlcomm.Parameters.Add(fromdateparam);

                    string datetoformattedforsql = dateto.ToString("yyyy-MM-dd");
                    SqlParameter todateparam = new SqlParameter("@DateTo", datetoformattedforsql);
                    fromdateparam.SqlDbType = SqlDbType.Date;
                    sqlcomm.Parameters.Add(todateparam);

                    sqlconn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(sqlcomm);
                    DataSet ds = new DataSet();
                    //try
                    {

                        da.Fill(ds);
                        dt = ds.Tables[0];
                    }
                    //catch(Exception)
                    {

                    }
                }
            }

                return dt;
        }
    }
}
