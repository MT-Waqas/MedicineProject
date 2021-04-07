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
    public class BL_Items
    {
        public static void Save(Items item)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("MedicineID",item.MedicineID),
                new SqlParameter("Quantity",item.Quantity),
                new SqlParameter("ItemPrice",item.ItemPrice),
                new SqlParameter("DateOfManufacturing",item.DateOfManufacturing),
                new SqlParameter("DateOfExpiry",item.DateOfExpiry),
                new SqlParameter("OrderID",item.OrderID),
                new SqlParameter("type",Actions.Insert)
            };
            Helper.sp_ExecuteQuery("sp_Item", prm);
        }
        public static void Update(Items item)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("ItemID",item.ItemID),
                new SqlParameter("MedicineID",item.MedicineID),
                new SqlParameter("Quantity",item.Quantity),
                new SqlParameter("ItemPrice",item.ItemPrice),
                new SqlParameter("DateOfManufacturing",item.DateOfManufacturing),
                new SqlParameter("DateOfExpiry",item.DateOfExpiry),
                new SqlParameter("OrderID",item.OrderID),
                new SqlParameter("type",Actions.Update)
            };
            Helper.sp_ExecuteQuery("sp_Item", prm);
        }
        public static void Delete(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("ItemID",ID),
                new SqlParameter("type",Actions.Delete)
            };
            Helper.sp_ExecuteQuery("sp_Item", prm);
        }
        public static Items GetItem(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("ItemID",ID),
                new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_Item", prm);
            Items item = new Items();
            if (dt.Rows.Count>0)
            {
                item.ItemID = Convert.ToInt32(dt.Rows[0]["ItemID"]);
                item.MedicineID = Convert.ToInt32(dt.Rows[0]["MedicineID"]);
                item.MedicineName = Convert.ToString(dt.Rows[0]["MedicineName"]);
                item.Quantity = Convert.ToInt32(dt.Rows[0]["Quantity"]);
                item.ItemPrice = Convert.ToDecimal(dt.Rows[0]["ItemPrice"]);
                item.DateOfManufacturing = Convert.ToDateTime(dt.Rows[0]["DateOfManufacturing"]);
                item.DateOfExpiry = Convert.ToDateTime(dt.Rows[0]["DateOfExpiry"]);
                item.OrderID = Convert.ToInt32(dt.Rows[0]["OrderID"]);
            }
            return item;
        }
        public static List<Items> GetItems(int ClientID)
        {
            List<Items> items = new List<Items>();
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("ClientID",ClientID),
                new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_Item", prm);
           
            foreach (DataRow dr in dt.Rows)
            {
                Items item = new Items();
                item.ItemID = Convert.ToInt32(dr["ItemID"]);
                item.MedicineID = Convert.ToInt32(dr["MedicineID"]);
                item.MedicineName = Convert.ToString(dr["MedicineName"]);
                item.Quantity = Convert.ToInt32(dr["Quantity"]);
                item.ItemPrice = Convert.ToDecimal(dr["ItemPrice"]);
                item.DateOfManufacturing = Convert.ToDateTime(dr["DateOfManufacturing"]);
                item.DateOfExpiry = Convert.ToDateTime(dr["DateOfExpiry"]);
                item.OrderID = Convert.ToInt32(dr["OrderID"]);
                items.Add(item);
            }
            return items;
        }
    }
    public class Items
    {
        public int ItemID { get; set; }
        public int MedicineID { get; set; }
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
        public decimal ItemPrice { get; set; }
        public DateTime DateOfManufacturing { get; set; }
        public DateTime DateOfExpiry { get; set; }
        public int OrderID   { get; set; }
        public int IsDelete { get; set; }
        public int ClientID { get; set; }
    }
}