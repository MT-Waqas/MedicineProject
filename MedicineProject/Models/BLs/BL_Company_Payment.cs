using MedicineProject.Models.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace MedicineProject.Models.BLs
{
    public class BL_Company_Payment
    {
        public static void Save(CompanyPayment cmppay)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("CompanyID",cmppay.CompanyID),
                new SqlParameter("PaidAmount",cmppay.PaidAmount),
                new SqlParameter("PaymentDate",cmppay.PaymentDate),
                new SqlParameter("BankName",cmppay.BankName),
                new SqlParameter("ReceiptNumber",cmppay.ReceiptNumber),
                new SqlParameter("PaidAmount",cmppay.TotallAmount),
                new SqlParameter("RemaningPayment",cmppay.RemaningPayment),
                new SqlParameter("type",Actions.Insert)
            };
            Helper.sp_ExecuteQuery("sp_CompanyPayment", prm);
        }
        public static void Update(CompanyPayment cmppay)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                 new SqlParameter("CompanyPaymentID",cmppay.CompanyPaymentID),
               
                new SqlParameter("PaidAmount",cmppay.PaidAmount),
                new SqlParameter("PaymentDate",cmppay.PaymentDate),
                new SqlParameter("BankName",cmppay.BankName),
                new SqlParameter("ReceiptNumber",cmppay.ReceiptNumber),
                new SqlParameter("PaidAmount",cmppay.TotallAmount),
                new SqlParameter("RemaningPayment",cmppay.RemaningPayment),
                new SqlParameter("type",Actions.Update)
            };
            Helper.sp_ExecuteQuery("sp_CompanyPayment", prm);
        }
        public static void Delete(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
               new SqlParameter("CompanyPaymentID",ID),
               new SqlParameter("type",Actions.Delete),
            };
            Helper.sp_ExecuteQuery("sp_CompanyPayment", prm);
        }
        public static CompanyPayment GetCompanyPayment(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
               new SqlParameter("CompanyPaymentID",ID),
               new SqlParameter("type",Actions.Delete),
            };
            DataTable dt = Helper.sp_Execute_Table("sp_CompanyPayment", prm);
            CompanyPayment cmppayment = new CompanyPayment();
            if (dt.Rows.Count>0)
            {
                cmppayment.CompanyID = Convert.ToInt32(dt.Rows[0]["CompanyID"]);
                cmppayment.PaidAmount = Convert.ToInt32(dt.Rows[0]["PaidAmount"]);
                cmppayment.PaymentDate = Convert.ToDateTime(dt.Rows[0]["PaymentDate"]);
                cmppayment.BankName = Convert.ToString(dt.Rows[0]["BankName"]);
                cmppayment.ReceiptNumber = Convert.ToString(dt.Rows[0]["ReceiptNumber"]);
                cmppayment.TotallAmount = Convert.ToInt32(dt.Rows[0]["TotallAmount"]);
                cmppayment.RemaningPayment = Convert.ToInt32(dt.Rows[0]["RemaningPayment"]);
             

            }
            return cmppayment;
        }

        public static List<CompanyPayment> GetClients(int ClientID)
        {
            List<CompanyPayment> clients = new List<CompanyPayment>();
            SqlParameter[] prm = new SqlParameter[]
            {
             new SqlParameter("ClientID",ClientID),
               new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_CompanyPayment", prm);


            foreach (DataRow dr in dt.Rows)
            {
                CompanyPayment client = new CompanyPayment();
                client.CompanyPaymentID = Convert.ToInt32(dr["CompanyPaymentID"]);
                client.CompanyID = Convert.ToInt32(dr["CompanyID"]);
                client.TotallAmount = Convert.ToDecimal(dr["TotallAmount"]);
                client.RemaningPayment = Convert.ToDecimal(dr["RemaningPayment"]);
                client.PaidAmount = Convert.ToDecimal(dr["PaidAmount"]);
                client.PaymentDate = Convert.ToDateTime(dr["PaymentDate"]);
                client.BankName = Convert.ToString(dr["BankName"]);
                client.ReceiptNumber = Convert.ToString(dr["ReceiptNumber"]);
                clients.Add(client);
            }
            return clients;
        }
    }
    public class CompanyPayment
    {
        public int CompanyPaymentID { get; set; }
        public int CompanyID { get; set; }
        public decimal TotallAmount { get; set; }
        public decimal RemaningPayment { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string BankName { get; set; }
        public string ReceiptNumber { get; set; }     
        public int IsDelete { get; set; }
        public int ClientID { get; set; }
    }
}