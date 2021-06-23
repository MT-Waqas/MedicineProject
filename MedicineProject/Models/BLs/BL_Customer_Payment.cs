using System;
using System.Collections.Generic;
using System.Linq;
using MedicineProject.Models;
using MedicineProject.Models.Custom;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.ComponentModel.DataAnnotations;

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
                new SqlParameter("RemainingAmount",custpay.RemainingAmount=custpay.TotalAmount-custpay.PaidAmount),
                new SqlParameter("PaymentMethodID",custpay.PaymentMethodID),
                new SqlParameter("MethodDetail",custpay.MethodDetail),
                new SqlParameter("TotalAmount",custpay.TotalAmount),
               new SqlParameter("OrderID",custpay.OrderID),
                new SqlParameter("type",Actions.Insert)
            };
            Helper.sp_ExecuteQuery("sp_CustomerPayment", prm);
        }
        public static void Update(CustomerPayment custpay)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                 new SqlParameter("CustomerPaymentID",custpay.CustomerPaymentID),
                 new SqlParameter("CustomerID",custpay.CustomerID),
                new SqlParameter("PaidAmount",custpay.PaidAmount),
                new SqlParameter("PaymentDate",custpay.PaymentDate),
                new SqlParameter("RemainingAmount",custpay.RemainingAmount=custpay.TotalAmount-custpay.PaidAmount),
                new SqlParameter("PaymentMethod",custpay.PaymentMethodID),
                new SqlParameter("MethodDetail",custpay.MethodDetail),
                new SqlParameter("TotalAmount",custpay.TotalAmount),
               new SqlParameter("OrderID",custpay.OrderID),
                new SqlParameter("type",Actions.Update)
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
                cmppayment.RemainingAmount = Convert.ToDecimal(dt.Rows[0]["RemainingAmount"]);
                cmppayment.PaymentMethodID = Convert.ToInt32(dt.Rows[0]["PaymentMethodID"]);
                cmppayment.PaymentMethodName = Convert.ToString(dt.Rows[0]["PaymentMethodName"]);
                cmppayment.MethodDetail = Convert.ToString(dt.Rows[0]["MethodDetail"]);
                cmppayment.TotalAmount = Convert.ToInt32(dt.Rows[0]["TotalAmount"]);
                cmppayment.ClientID = Convert.ToInt32(dt.Rows[0]["ClientID"]);
                cmppayment.OrderID = Convert.ToInt32(dt.Rows[0]["OrderID"]);


            }
            return cmppayment;
        }

        public static List<CustomerPayment> GetCustomerpayments(int OrderID)
         {
            List<CustomerPayment> clients = new List<CustomerPayment>();
            SqlParameter[] prm = new SqlParameter[]
            {
             new SqlParameter("OrderID",OrderID),
               new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_CustomerPayment", prm);
            foreach (DataRow dr in dt.Rows)
            {
                CustomerPayment client = new CustomerPayment();
                client.CustomerPaymentID = Convert.ToInt32(dr["CustomerPaymentID"]);
                client.PaidAmount = Convert.ToInt32(dr["PaidAmount"]);
                client.TotalAmount = Convert.ToDecimal(dr["TotalAmount"]);
                client.RemainingAmount = Convert.ToDecimal(dr["RemainingAmount"]);
                client.PaidAmount = Convert.ToDecimal(dr["PaidAmount"]);
                client.PaymentDate = Convert.ToDateTime(dr["PaymentDate"]);
                client.PaymentMethodID = Convert.ToInt32(dr["PaymentMethodID"]);
                client.PaymentMethodName = Convert.ToString(dr["PaymentMethodName"]);
                client.MethodDetail = Convert.ToString(dr["MethodDetail"]);
                client.OrderID = Convert.ToInt32(dr["OrderID"]);
                client.CustomerID = Convert.ToInt32(dr["CustomerID"]);
                client.CustomerName = Convert.ToString(dr["CustomerName"]);
                clients.Add(client);
            }
            return clients;
        }
    }
    public class CustomerPayment
    {
        public int CustomerPaymentID { get; set; }
        [Required]
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        [Required]
        public decimal PaidAmount { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        public string PaymentMethodName { get; set; }
        [Required]
        public int PaymentMethodID { get; set; }
        [Required]
        public string MethodDetail { get; set; }
        public int IsDelete { get; set; }
        public int ClientID { get; set; }
        [Required]
        public int OrderID { get; set; }
        //public int InvoiceID { get; set; }
    }
}