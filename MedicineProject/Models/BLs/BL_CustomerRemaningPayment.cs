using System;
using MedicineProject.Models.Custom;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace MedicineProject.Models.BLs
{
    public class BL_CustomerRemaningPayment
    {
        public static void Save(CustomerRemaningPayment c)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
               new SqlParameter("CustomerRemAmount",c.CustomerRemAmount),
               new SqlParameter("CustomerID",c.CustomerID),
               new SqlParameter("OrderID",c.OrderID),
               new SqlParameter("type",Actions.Insert)
            };
            Helper.sp_ExecuteQuery("sp_CustomerRemPayment", prm);
        }
        public static void Update(CustomerRemaningPayment c)
        {
            SqlParameter[] prm = new SqlParameter[]
             {
               new SqlParameter("CustomerRemaningPaymentID",c.CustomerRemaningPaymentID),
                 new SqlParameter("CustomerRemAmount",c.CustomerRemAmount),
               new SqlParameter("CustomerID",c.CustomerID),
               new SqlParameter("OrderID",c.OrderID),
               new SqlParameter("type",Actions.Update)
             };
            Helper.sp_ExecuteQuery("sp_CustomerRemPayment", prm);
        }
        public static void Delete(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
               new SqlParameter("CustomerRemaningPaymentID",ID),
               new SqlParameter("type",Actions.Delete)
            };
            Helper.sp_ExecuteQuery("sp_CustomerRemPayment", prm);
        }
        public static CustomerRemaningPayment GetDetail(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
               new SqlParameter("CustomerRemaningPaymentID",ID),
               new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_CustomerRemPayment", prm);
            CustomerRemaningPayment client = new CustomerRemaningPayment();
            if (dt.Rows.Count > 0)
            {
                client.CustomerRemaningPaymentID = Convert.ToInt32(dt.Rows[0]["CustomerRemaningPaymentID"]);
                client.CustomerRemAmount = Convert.ToDecimal(dt.Rows[0]["CustomerRemAmount"]);
                client.CustomerID = Convert.ToInt32(dt.Rows[0]["CustomerID"]);
                client.OrderID = Convert.ToInt32(dt.Rows[0]["OrderID"]);
          
            }
            return client;
        }


        public static List<CustomerRemaningPayment> GetDetails(int ClientID)
        {
            List<CustomerRemaningPayment> clients = new List<CustomerRemaningPayment>();
            SqlParameter[] prm = new SqlParameter[]
            {
               new SqlParameter("ClientID",ClientID),
               new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_CustomerRemPayment", prm);


            foreach (DataRow dr in dt.Rows)
            {
                CustomerRemaningPayment client = new CustomerRemaningPayment();
                client.ClientID = Convert.ToInt32(dr["ClientID"]);
                client.CustomerRemaningPaymentID = Convert.ToInt32(dr["CustomerRemaningPaymentID"]);
                client.CustomerRemAmount = Convert.ToDecimal(dr["CustomerRemAmount"]);
                client.CustomerID = Convert.ToInt32(dr["CustomerID"]);
                client.OrderID = Convert.ToInt32(dr["OrderID"]);
               
                clients.Add(client);
            }
            return clients;
        }



    }
    public class CustomerRemaningPayment
    {
        public  int CustomerRemaningPaymentID { get; set; }
        public  decimal CustomerRemAmount { get; set; }
        public  int CustomerID { get; set; }
        public  int OrderID { get; set; }
        public  int IsDelete { get; set; }
        public int ClientID { get; set; }
       
    
    }
}