using MedicineProject.Models.Custom;
using System;
using MedicineProject.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MedicineProject.Models.BLs
{
    public class BL_SaleItems
    {
        public static void Save(SaleItems item)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("OrderID",item.OrderID),
                new SqlParameter("MedicineID",item.MedicineID),
                new SqlParameter("CompanyID",item.CompanyID),
                new SqlParameter("Quantity",item.Quantity),
                new SqlParameter("SalePrice",item.SalePrice),
                new SqlParameter("type",Actions.Insert)
            };
            Helper.sp_ExecuteQuery("sp_SaleItem", prm);
        }
        public static void Update(SaleItems item)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("SaleID",item.SaleID),
                new SqlParameter("MedicineID",item.MedicineID),
                new SqlParameter("CompanyID",item.CompanyID),
                new SqlParameter("Quantity",item.Quantity),
                new SqlParameter("SalePrice",item.SalePrice),
                new SqlParameter("OrderID",item.OrderID),
                new SqlParameter("type",Actions.Update)
            };
            Helper.sp_ExecuteQuery("sp_SaleItem", prm);
        }
        public static void Delete(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("ItemID",ID),
                new SqlParameter("type",Actions.Delete)
            };
            Helper.sp_ExecuteQuery("sp_SaleItem", prm);
        }
        public static SaleItems GetItem(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("SaleID",ID),
                new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_SaleItem", prm);
            SaleItems item = new SaleItems();
            if (dt.Rows.Count > 0)
            {
                item.SaleID = Convert.ToInt32(dt.Rows[0]["SaleID"]);
                item.MedicineID = Convert.ToInt32(dt.Rows[0]["MedicineID"]);
                item.MedicineName = Convert.ToString(dt.Rows[0]["MedicineName"]);
                item.CompanyID = Convert.ToInt32(dt.Rows[0]["CompanyID"]);
                item.CompanyName = Convert.ToString(dt.Rows[0]["CompanyName"]);
                item.Quantity = Convert.ToInt32(dt.Rows[0]["Quantity"]);
                item.SalePrice = Convert.ToDecimal(dt.Rows[0]["SalePrice"]);
                //item.DateOfManufacturing = Convert.ToDateTime(dt.Rows[0]["DateOfManufacturing"]);
                //item.DateOfExpiry = Convert.ToDateTime(dt.Rows[0]["DateOfExpiry"]);
                item.OrderID = Convert.ToInt32(dt.Rows[0]["OrderID"]);
            }
            return item;
        }
        public static List<SaleItems> GetItems(int? OrderID,int? CustomerID)
        {
            List<SaleItems> items = new List<SaleItems>();
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("OrderID",OrderID),
                new SqlParameter("CustomerID",CustomerID),
                new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_SaleItem", prm);

            foreach (DataRow dr in dt.Rows)
            {
                SaleItems item = new SaleItems();
                item.SaleID = Convert.ToInt32(dr["SaleID"]);
                item.MedicineID = Convert.ToInt32(dr["MedicineID"]);
                item.MedicineName = Convert.ToString(dr["MedicineName"]);
                item.CompanyID = Convert.ToInt32(dr["CompanyID"]);
                item.CompanyName = Convert.ToString(dr["CompanyName"]);
                item.Quantity = Convert.ToInt32(dr["Quantity"]);
                item.SalePrice = Convert.ToDecimal(dr["SalePrice"]);
                //item.DateOfManufacturing = Convert.ToDateTime(dr["DateOfManufacturing"]);
                //item.DateOfExpiry = Convert.ToDateTime(dr["DateOfExpiry"]);
                item.OrderID = Convert.ToInt32(dr["OrderID"]);
                items.Add(item);
            }
            return items;
        }
    }
    public class SaleItems
    {
        public int SaleID { get; set; }
        public int MedicineID { get; set; }
        public string MedicineName { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }

        public int Quantity { get; set; }
        public int AvailableStock { get; set; }
        public decimal SalePrice { get; set; }
        public DateTime DateOfManufacturing { get; set; }
        public DateTime DateOfExpiry { get; set; }
        public int OrderID { get; set; }
        public int IsDelete { get; set; }
        public int ClientID { get; set; }
    }
}