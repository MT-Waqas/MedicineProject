using MedicineProject.Models.Custom;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
                new SqlParameter("RemainingAmount",cmppay.TotalAmount-cmppay.PaidAmount),
                new SqlParameter("PaymentDate",cmppay.PaymentDate),
                new SqlParameter("PaymentMethod",cmppay.PaymentMethod),
                new SqlParameter("ReceiptNumber_or_BankName",cmppay.ReceiptNumber_or_BankName),
                new SqlParameter("InvoiceID",cmppay.InvoiceID),
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
                new SqlParameter("RemainingAmount",cmppay.TotalAmount-cmppay.PaidAmount),
                new SqlParameter("PaymentDate",cmppay.PaymentDate),
                new SqlParameter("PaymentMethod",cmppay.PaymentMethod),
                new SqlParameter("ReceiptNumber_or_BankName",cmppay.ReceiptNumber_or_BankName),
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
        public static CompanyPayment GetCompanyPayment(int? ID, int? InvoiceId)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
               new SqlParameter("CompanyPaymentID",ID),
                new SqlParameter("InvoiceID",InvoiceId),
               new SqlParameter("type",Actions.Select),
            };
            DataTable dt = Helper.sp_Execute_Table("sp_CompanyPayment", prm);
            CompanyPayment cmppayment = new CompanyPayment();
            if (dt.Rows.Count > 0 && ID>0)
            {
                cmppayment.CompanyPaymentID = Convert.ToInt32(dt.Rows[0]["CompanyPaymentID"]);
                cmppayment.CompanyID = Convert.ToInt32(dt.Rows[0]["CompanyID"]);
                cmppayment.TotalAmount = Convert.ToInt32(dt.Rows[0]["TotalAmount"]);
                cmppayment.PaidAmount = Convert.ToInt32(dt.Rows[0]["PaidAmount"]);
                cmppayment.RemainingAmount = Convert.ToInt32(dt.Rows[0]["RemainingAmount"]);
                cmppayment.PaymentDate = Convert.ToDateTime(dt.Rows[0]["PaymentDate"]);
                cmppayment.PaymentMethod = Convert.ToString(dt.Rows[0]["PaymentMethod"]);
                cmppayment.ReceiptNumber_or_BankName = Convert.ToString(dt.Rows[0]["ReceiptNumber_or_BankName"]);
                cmppayment.InvoiceID = Convert.ToInt32(dt.Rows[0]["InvoiceID"]);
            } 
            return cmppayment;
        }
        public static decimal GetRemaining(int InvoiceId)
        {
            decimal RemainingAmount=0;
            SqlParameter[] prm = new SqlParameter[]
            {
               new SqlParameter("InvoiceID",InvoiceId),
               new SqlParameter("type",Actions.Select_Remaining),
            };
            DataTable dt = Helper.sp_Execute_Table("sp_CompanyPayment", prm); 
            if (dt.Rows.Count >0)
            { 
                RemainingAmount  = Convert.ToDecimal(dt.Rows[0]["RemainingAmount"]);
            }
            return RemainingAmount;
        }
        public static List<CompanyPayment> GetCompanyPayments(int ClientID,int InvoiceID,int CompanyID)
        {
            List<CompanyPayment> cmppayments = new List<CompanyPayment>();
            SqlParameter[] prm = new SqlParameter[]
            {
             new SqlParameter("ClientID",ClientID),
             new SqlParameter("InvoiceID",InvoiceID),
             new SqlParameter("CompanyID",CompanyID),
             new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_CompanyPayment", prm);
            foreach (DataRow dr in dt.Rows)
            {
                CompanyPayment cmppayment = new CompanyPayment();
                cmppayment.CompanyPaymentID = Convert.ToInt32(dr["CompanyPaymentID"]);
                cmppayment.CompanyID = Convert.ToInt32(dr["CompanyID"]);
                cmppayment.CompanyName = Convert.ToString(dr["CompanyName"]);
                cmppayment.TotalAmount = Convert.ToDecimal(dr["TotalAmount"]);
                cmppayment.PaidAmount = Convert.ToDecimal(dr["PaidAmount"]);
                cmppayment.RemainingAmount = Convert.ToDecimal(dr["RemainingAmount"]);
                cmppayment.PaymentDate = Convert.ToDateTime(dr["PaymentDate"]);
                cmppayment.PaymentMethod = Convert.ToString(dr["PaymentMethod"]);
                cmppayment.ReceiptNumber_or_BankName = Convert.ToString(dr["ReceiptNumber_or_BankName"]);
                cmppayment.InvoiceID = Convert.ToInt32(dr["InvoiceID"]);
                cmppayments.Add(cmppayment);
            }
            return cmppayments;
        }
    }
    public class CompanyPayment
    {
        public int? CompanyPaymentID { get; set; }
        [Required(ErrorMessage = "Please Select the Company Name")]
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Please Enter Total Amount")]
        public decimal TotalAmount { get; set; }
        [Required(ErrorMessage = "Please Enter Remaining Amount")]
        public decimal RemainingAmount { get; set; }
        [Required(ErrorMessage = "Please Enter Paid Amount")]
        public decimal PaidAmount { get; set; }
        [Required(ErrorMessage = "Please Select Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PaymentDate { get; set; }
        [Required(ErrorMessage = "Please Enter Bank Name")]
        public string PaymentMethod { get; set; }
        [Required]
        public string ReceiptNumber_or_BankName { get; set; }
        public int InvoiceID { get; set; }
        public int IsDelete { get; set; }
        public int ClientID { get; set; }

    }
}