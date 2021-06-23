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
    public class BL_Purchase
    {
        public static void Save(Purchase purchase)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("CompanyID",purchase.CompanyID),
                new SqlParameter("MedicineID",purchase.MedicineID),
                new SqlParameter("Quantity",purchase.Quantity),
                new SqlParameter("SoldQuantity",purchase.SoldQuantity),
                new SqlParameter("PurchasePrice",purchase.PurchasePrice),
                new SqlParameter("SalePrice",purchase.SalePrice),
                new SqlParameter("DateOfExpiry",purchase.DateOfExpiry),
                new SqlParameter("DateOfManufacturing",purchase.DateOfManufacturing),
                new SqlParameter("ClientID",1),
                new SqlParameter("OrderID",purchase.OrderID),
                new SqlParameter("type",Actions.Insert)
            };
            Helper.sp_ExecuteQuery("sp_Purchase", prm);
        }
        public static void Update(Purchase purchase)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("PurchaseID",purchase.PurchaseID),
                new SqlParameter("CompanyID",purchase.CompanyID),
                new SqlParameter("MedicineID",purchase.MedicineID),
                new SqlParameter("Quantity",purchase.Quantity),
                new SqlParameter("SoldQuantity",purchase.SoldQuantity),
                new SqlParameter("PurchasePrice",purchase.PurchasePrice),
                new SqlParameter("SalePrice",purchase.SalePrice),
               new SqlParameter("DateOfManufacturing",purchase.DateOfManufacturing),
               new SqlParameter("DateOfExpiry",purchase.DateOfExpiry),
               new SqlParameter("OrderID",purchase.OrderID),
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
                purchase.SoldQuantity = Convert.ToInt32(dt.Rows[0]["SoldQuantity"]);
                purchase.PurchasePrice = Convert.ToDecimal(dt.Rows[0]["PurchasePrice"]);
                purchase.SalePrice = Convert.ToDecimal(dt.Rows[0]["SalePrice"]);
                purchase.DateOfManufacturing = Convert.ToDateTime(dt.Rows[0]["DateOfManufacturing"]);
                purchase.DateOfExpiry = Convert.ToDateTime(dt.Rows[0]["DateOfExpiry"]);
                purchase.OrderID = Convert.ToInt32(dt.Rows[0]["OrderID"]);
                purchase.ImageName = Convert.ToString(dt.Rows[0]["ImageName"]);

            }
            return purchase;
        }
        public static List<Purchase> GetPurchases(int ClientID, int? OrderID)
        {
            List<Purchase> purchases = new List<Purchase>();
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("ClientID",ClientID),
                new SqlParameter("OrderID",OrderID),
                new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_Purchase", prm);
            foreach (DataRow dr in dt.Rows)
            {
                Purchase purchase = new Purchase();
                //purchase.PurchaseID = Convert.ToInt32(dr["PurchaseID"]);
                //purchase.CompanyID = Convert.ToInt32(dr["CompanyID"]);
                purchase.MedicineID = Convert.ToInt32(dr["MedicineID"]);
                purchase.MedicineName = Convert.ToString(dr["MedicineName"]);
                purchase.CompanyName = Convert.ToString(dr["CompanyName"]);
                purchase.Quantity = Convert.ToInt32(dr["Quantity"]);
                purchase.SoldQuantity = Convert.ToInt32(dr["SoldQuantity"]);
                purchase.PurchasePrice = Convert.ToDecimal(dr["PurchasePrice"]);
                purchase.SalePrice = Convert.ToDecimal(dr["SalePrice"]);
                purchase.DateOfManufacturing = Convert.ToDateTime(dr["DateOfManufacturing"]);
                purchase.DateOfExpiry = Convert.ToDateTime(dr["DateOfExpiry"]);
                purchase.OrderID = Convert.ToInt32(dr["OrderID"]);
                purchase.ImageName = Convert.ToString(dr["ImageName"]);

                purchases.Add(purchase);
            }
            return purchases;
        }

    }
    public class Purchase
    {
        public int? PurchaseID { get; set; }
        [Required]
        public int MedicineID { get; set; }
        public string MedicineName { get; set; }
        [Required]
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal PurchasePrice { get; set; }
        [Display(Name = "DateOfManufacturing")]
        [Required]
        public DateTime DateOfManufacturing { get; set; }
        [Required]
        public DateTime DateOfExpiry { get; set; }
        public string ImageName { get; set; }
        [Required]
        public int OrderID { get; set; }
        public int IsDeleted { get; set; }
        public int ClientID { get; set; }
        public int SoldQuantity { get; set; }
        public decimal SalePrice { get; set; }
        public List<Purchase> PurchaseList { get; set; }
    }
}