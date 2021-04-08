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
                new SqlParameter("TotalAmount",cmppay.TotalAmount),
                new SqlParameter("PaidAmount",cmppay.PaidAmount),
                new SqlParameter("RemainingAmount",cmppay.RemainingAmount),
                new SqlParameter("PaymentDate",cmppay.PaymentDate),
                new SqlParameter("BankName",cmppay.BankName),
                new SqlParameter("ReceiptNumber",cmppay.ReceiptNumber),
                new SqlParameter("type",Actions.Insert)
            };
            Helper.sp_ExecuteQuery("sp_CompanyPayment", prm);
        }
        public static void Update(CompanyPayment cmppay)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("CompanyPaymentID",cmppay.CompanyPaymentID),
                new SqlParameter("CompanyID",cmppay.CompanyID),
                new SqlParameter("TotalAmount",cmppay.TotalAmount),
                new SqlParameter("PaidAmount",cmppay.PaidAmount),
                new SqlParameter("RemainingAmount",cmppay.RemainingAmount),
                new SqlParameter("PaymentDate",cmppay.PaymentDate),
                new SqlParameter("BankName",cmppay.BankName),
                new SqlParameter("ReceiptNumber",cmppay.ReceiptNumber),
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
            if (dt.Rows.Count > 0)
            {
                cmppayment.CompanyID = Convert.ToInt32(dt.Rows[0]["CompanyID"]);
                cmppayment.TotalAmount = Convert.ToInt32(dt.Rows[0]["TotallAmount"]);
                cmppayment.PaidAmount = Convert.ToInt32(dt.Rows[0]["PaidAmount"]);
                cmppayment.RemainingAmount = Convert.ToInt32(dt.Rows[0]["RemainingAmount"]);
                cmppayment.PaymentDate = Convert.ToDateTime(dt.Rows[0]["PaymentDate"]);
                cmppayment.BankName = Convert.ToString(dt.Rows[0]["BankName"]);
                cmppayment.ReceiptNumber = Convert.ToString(dt.Rows[0]["ReceiptNumber"]);
            }
            return cmppayment;
        }

        public static List<CompanyPayment> GetCompanyPayments(int ClientID)
        {
            List<CompanyPayment> cmppayments = new List<CompanyPayment>();
            SqlParameter[] prm = new SqlParameter[]
            {
             new SqlParameter("ClientID",ClientID),
               new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_CompanyPayment", prm);
            foreach (DataRow dr in dt.Rows)
            {
                CompanyPayment cmppayment = new CompanyPayment();
                cmppayment.CompanyPaymentID = Convert.ToInt32(dr["CompanyPaymentID"]);
                cmppayment.CompanyID = Convert.ToInt32(dr["CompanyID"]);
                cmppayment.CompanyName = Convert.ToString(dr["CompanyName"]);
                cmppayment.TotalAmount = Convert.ToDecimal(dr["TotallAmount"]);
                cmppayment.PaidAmount = Convert.ToDecimal(dr["PaidAmount"]);
                cmppayment.RemainingAmount = Convert.ToDecimal(dr["RemainingAmount"]);
                cmppayment.PaymentDate = Convert.ToDateTime(dr["PaymentDate"]);
                cmppayment.BankName = Convert.ToString(dr["BankName"]);
                cmppayment.ReceiptNumber = Convert.ToString(dr["ReceiptNumber"]);
                cmppayments.Add(cmppayment);
            }
            return cmppayments;
        }
    }
    public class CompanyPayment
    {
        public int CompanyPaymentID { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string BankName { get; set; }
        public string ReceiptNumber { get; set; }
        public int IsDelete { get; set; }
        public int ClientID { get; set; }
    }
}