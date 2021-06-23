using MedicineProject.Models.Custom;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MedicineProject.Models.BLs
{
    public class BL_Invoice
    {
        public static void Save(Invoice invoice)
        {
            SqlParameter[] prm = new SqlParameter[]
           {
                new SqlParameter("OrderID",invoice.OrderID),
                new SqlParameter("TotalAmount",invoice.TotalAmount),
                new SqlParameter("OrderDate",invoice.OrderDate),
                new SqlParameter("IsPaid",0),
                new SqlParameter("type",Actions.Insert)
           };
            Helper.sp_ExecuteQuery("sp_Purchase_Invoice", prm);
        }
        public static void Update(Invoice invoice)
        {
            SqlParameter[] prm = new SqlParameter[]
          {
                new SqlParameter("OrderID",invoice.OrderID),
                 new SqlParameter("IsPaid",invoice.IsPaid),
                new SqlParameter("type",Actions.Update)
          };
            Helper.sp_ExecuteQuery("sp_Purchase_Invoice", prm);
        }
        public static List<Invoice> GetInvoices(int ID, int? InvoiceID)
        {
            List<Invoice> invoices = new List<Invoice>();
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@ClientID",ID),
                new SqlParameter("@InvoiceID",InvoiceID),
                new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_Purchase_Invoice", prm);
            foreach (DataRow dr in dt.Rows)
            {
                Invoice invoice = new Invoice();
                invoice.InvoiceID = Convert.ToInt32(dr["InvoiceID"]);
                invoice.OrderID = Convert.ToInt32(dr["OrderID"]);
                invoice.TotalAmount = Convert.ToDecimal(dr["TotalAmount"]);
                invoice.OrderDate = Convert.ToDateTime(dr["OrderDate"]);
                invoice.IsPaid = Convert.ToInt32(dr["IsPaid"]);
                invoice.CompanyID = Convert.ToInt32(dr["CompanyID"]);
                invoice.CompanyName = Convert.ToString(dr["CompanyName"]);
                invoices.Add(invoice);
            }
            return invoices;
        }
        public static Invoice GetInvoice(int InvoiceID)
        {
            List<Invoice> invoices = new List<Invoice>();
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@InvoiceID",InvoiceID),
                new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_Purchase_Invoice", prm);
            Invoice invoice = new Invoice();
            if (dt.Rows.Count>0)
            {  
                invoice.InvoiceID = Convert.ToInt32(dt.Rows[0]["InvoiceID"]);
                invoice.OrderID = Convert.ToInt32(dt.Rows[0]["OrderID"]);
                invoice.TotalAmount = Convert.ToDecimal(dt.Rows[0]["TotalAmount"]);
                invoice.OrderDate = Convert.ToDateTime(dt.Rows[0]["OrderDate"]);
                invoice.IsPaid = Convert.ToInt32(dt.Rows[0]["IsPaid"]);
                invoice.CompanyID = Convert.ToInt32(dt.Rows[0]["CompanyID"]);
                invoice.CompanyName = Convert.ToString(dt.Rows[0]["CompanyName"]);
            }
            return invoice;
        }
    }
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public int OrderID { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public int IsPaid { get; set; }
        public int IsDelete { get; set; }
        public int CompanyID { get; set; }
        public decimal RemainingAmount { get; set; }
        public string CompanyName { get; set; }
    }

}