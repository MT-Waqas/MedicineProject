using MedicineProject.Models.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MedicineProject.Models.BLs
{
    public class BL_Purchase
    {
        public static void Save(Purchase purchase)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("CompanyID",purchase.CompanyID),
                new SqlParameter("MedicineID",purchase.MedicineID),
                new SqlParameter("Quantity",purchase.Quantity),
                new SqlParameter("ItemPrice",purchase.ItemPrice),
                new SqlParameter("DateOfExpiry",purchase.DateOfExpiry),
                new SqlParameter("DateOfManufacturing",purchase.DateOfManufacturing),
                new SqlParameter("ClientID",1),
                new SqlParameter("type",Actions.Insert)
            };
            Helper.sp_ExecuteQuery("sp_Purchase", prm);
        }
        public static void Update(Purchase purchase)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
               new SqlParameter("CompanyID",purchase.CompanyID),
               new SqlParameter("PurchaseID",purchase.PurchaseID),
               new SqlParameter("MedicineID",purchase.MedicineID),
               new SqlParameter("Quantity",purchase.Quantity),
               new SqlParameter("ItemPrice",purchase.ItemPrice),
               new SqlParameter("DateOfManufacturing",purchase.DateOfManufacturing),
               new SqlParameter("DateOfExpiry",purchase.DateOfExpiry),
               new SqlParameter("type",Actions.Update)
           };
            Helper.sp_ExecuteQuery("sp_Purchase", prm);
        }
        public static void Delete(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
               new SqlParameter("PurchaseID",ID),
               new SqlParameter("type",Actions.Delete)
            };
            Helper.sp_ExecuteQuery("sp_Purchase", prm);
        }
        public static Purchase GetPurchase(int ID)
        {

            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("PurchaseID",ID),
                new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_Purchase", prm);
            Purchase purchase = new Purchase();
            if (dt.Rows.Count > 0)
            {
                purchase.PurchaseID = Convert.ToInt32(dt.Rows[0]["PurchaseID"]);
                purchase.CompanyID = Convert.ToInt32(dt.Rows[0]["CompanyID"]);
                purchase.MedicineID = Convert.ToInt32(dt.Rows[0]["MedicineID"]);
                purchase.MedicineName = Convert.ToString(dt.Rows[0]["MedicineName"]);
                purchase.CompanyName = Convert.ToString(dt.Rows[0]["CompanyName"]);
                purchase.Quantity = Convert.ToInt32(dt.Rows[0]["Quantity"]);
                purchase.DateOfManufacturing = Convert.ToDateTime(dt.Rows[0]["DateOfManufacturing"]);
                purchase.DateOfExpiry = Convert.ToDateTime(dt.Rows[0]["DateOfExpiry"]);

            }
            return purchase;
        }
        public static List<Purchase> GetPurchases(int ClientID)
        {
            List<Purchase> purchases = new List<Purchase>();
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("ClientID",ClientID),
                new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_Purchase", prm);
            foreach (DataRow dr in dt.Rows)
            {
                Purchase purchase = new Purchase();
                purchase.PurchaseID = Convert.ToInt32(dr["PurchaseID"]);
                purchase.CompanyID = Convert.ToInt32(dr["CompanyID"]);
                purchase.MedicineID = Convert.ToInt32(dr["MedicineID"]);
                purchase.MedicineName = Convert.ToString(dr["MedicineName"]);
                purchase.CompanyName = Convert.ToString(dr["CompanyName"]);
                purchase.Quantity = Convert.ToInt32(dr["Quantity"]);
                purchase.ItemPrice = Convert.ToDecimal(dr["ItemPrice"]);
                purchase.DateOfManufacturing = Convert.ToDateTime(dr["DateOfManufacturing"]);
                purchase.DateOfExpiry = Convert.ToDateTime(dr["DateOfExpiry"]);
                purchases.Add(purchase);
            }
            return purchases;
        }

    }
    public class Purchase
    {
        public int PurchaseID { get; set; }
        public int MedicineID { get; set; }
        public string MedicineName { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public int Quantity { get; set; }
        public decimal ItemPrice { get; set; }
        public DateTime DateOfManufacturing { get; set; }
        public DateTime DateOfExpiry { get; set; }
        public int IsDeleted { get; set; }
        public int ClientID { get; set; }
    }
}