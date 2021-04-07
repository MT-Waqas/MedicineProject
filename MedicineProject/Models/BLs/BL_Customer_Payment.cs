using System;
using System.Collections.Generic;
using System.Linq;
using MedicineProject.Models;
using MedicineProject.Models.Custom;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace MedicineProject.Models.BLs
{
    public class BL_Customer_Payment
    {
        public static void Save(CustomerPayment custpay)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("CustomerID",custpay.CustomerID),
                new SqlParameter("PaidAmount",custpay.PaidAmount),
                new SqlParameter("PaymentDate",custpay.PaymentDate),
                new SqlParameter("RemaningPayment",custpay.RemaningPayment),
                new SqlParameter("BankName",custpay.BankName),
                new SqlParameter("ReceiptNumber",custpay.ReceiptNumber),
                new SqlParameter("TotallAmount",custpay.TotallAmount),
               new SqlParameter("ClientID",custpay.ClientID),
                new SqlParameter("type",Actions.Insert)
            };
            Helper.sp_ExecuteQuery("sp_CustomerPayment", prm);
        }
        public static void Update(CustomerPayment cmppay)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                 new SqlParameter("CustomerPaymentID",cmppay.CustomerPaymentID),
                 new SqlParameter("CustomerID",cmppay.CustomerID),
                new SqlParameter("PaidAmount",cmppay.PaidAmount),
                new SqlParameter("PaymentDate",cmppay.PaymentDate),
                new SqlParameter("RemaningPayment",cmppay.RemaningPayment),
                new SqlParameter("BankName",cmppay.BankName),
                new SqlParameter("ReceiptNumber",cmppay.ReceiptNumber),
                new SqlParameter("TotallAmount",cmppay.TotallAmount),
                new SqlParameter("type",Actions.Insert)
            };
            Helper.sp_ExecuteQuery("sp_CustomerPayment", prm);
        }
        public static void Delete(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
               new SqlParameter("CustomerPaymentID",ID),
               new SqlParameter("type",Actions.Delete),
            };
            Helper.sp_ExecuteQuery("sp_CustomerPayment", prm);
        }
        public static CustomerPayment GetCustomerPayment(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
               new SqlParameter("CustomerPaymentID",ID),
               new SqlParameter("type",Actions.Delete),
            };
            DataTable dt = Helper.sp_Execute_Table("sp_CustomerPayment", prm);
            CustomerPayment cmppayment = new CustomerPayment();
            if (dt.Rows.Count > 0)
            {
                cmppayment.CustomerPaymentID = Convert.ToInt32(dt.Rows[0]["CustomerPaymentID"]);
                cmppayment.CustomerID = Convert.ToInt32(dt.Rows[0]["CustomerID"]);
                cmppayment.PaidAmount = Convert.ToDecimal(dt.Rows[0]["PaidAmount"]);
                cmppayment.PaymentDate = Convert.ToDateTime(dt.Rows[0]["PaymentDate"]);
                cmppayment.RemaningPayment = Convert.ToDecimal(dt.Rows[0]["RemaningPayment"]);
                cmppayment.BankName = Convert.ToString(dt.Rows[0]["BankName"]);
                cmppayment.ReceiptNumber = Convert.ToString(dt.Rows[0]["ReceiptNumber"]);
                cmppayment.TotallAmount = Convert.ToInt32(dt.Rows[0]["TotallAmount"]);
                cmppayment.ClientID = Convert.ToInt32(dt.Rows[0]["ClientID"]);
                cmppayment.OrderID = Convert.ToInt32(dt.Rows[0]["OrderID"]);


            }
            return cmppayment;
        }

        public static List<CustomerPayment> GetCustomerpayment(int ClientID)
        {
            List<CustomerPayment> clients = new List<CustomerPayment>();
            SqlParameter[] prm = new SqlParameter[]
            {
             new SqlParameter("ClientID",ClientID),
               new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_CustomerPayment", prm);


            foreach (DataRow dr in dt.Rows)
            {
                CustomerPayment client = new CustomerPayment();
                client.CustomerPaymentID = Convert.ToInt32(dr["CustomerPaymentID"]);
                client.PaidAmount = Convert.ToInt32(dr["PaidAmount"]);
                client.TotallAmount = Convert.ToDecimal(dr["TotallAmount"]);
                client.RemaningPayment = Convert.ToDecimal(dr["RemaningPayment"]);
                client.PaidAmount = Convert.ToDecimal(dr["PaidAmount"]);
                client.PaymentDate = Convert.ToDateTime(dr["PaymentDate"]);
                client.BankName = Convert.ToString(dr["BankName"]);
                client.ReceiptNumber = Convert.ToString(dr["ReceiptNumber"]);
                client.ClientID = Convert.ToInt32(dr["ClientID"]);
                client.OrderID = Convert.ToInt32(dr["OrderID"]);
                clients.Add(client);
            }
            return clients;
        }
    }
    public class CustomerPayment
    {
        public int CustomerPaymentID { get; set; }
        public int CustomerID { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal RemaningPayment { get; set; }
        public string BankName { get; set; }
        public string ReceiptNumber { get; set; }
        public decimal TotallAmount { get; set; }
        public int IsDelete { get; set; }
        public int ClientID { get; set; }
        public int OrderID { get; set; }
    }
}