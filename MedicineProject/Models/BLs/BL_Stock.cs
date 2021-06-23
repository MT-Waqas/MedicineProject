using MedicineProject.Models.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MedicineProject.Models.BLs
{
    public class BL_Stock
    {
        public static List<Purchase> GetMedicines(int ClientID)
        {
            List<Purchase> purchases = new List<Purchase>();
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("ClientID",ClientID),
                //new SqlParameter("MedicineName",mname),
                new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_Stock", prm);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Purchase purchase = new Purchase();
                    //purchase.PurchaseID = Convert.ToInt32(dr["PurchaseID"]);
                    //purchase.CompanyID = Convert.ToInt32(dr["CompanyID"]);
                    purchase.MedicineID = Convert.ToInt32(dr["MedicineID"]);
                    purchase.MedicineName = Convert.ToString(dr["MedicineName"]);
                    purchase.CompanyID = Convert.ToInt32(dr["CompanyID"]);
                    purchase.CompanyName = Convert.ToString(dr["CompanyName"]);
                    purchase.Quantity = Convert.ToInt32(dr["Quantity"]);
                    purchase.SalePrice = Convert.ToDecimal(dr["SalePrice"]);
                    //purchase.DateOfManufacturing = Convert.ToDateTime(dr["DateOfManufacturing"]);
                    //purchase.DateOfExpiry = Convert.ToDateTime(dr["DateOfExpiry"]);
                    //purchase.OrderID = Convert.ToInt32(dr["OrderID"]);
                    purchase.ImageName = Convert.ToString(dr["ImageName"]);
                    purchases.Add(purchase);
                }
            }
            return purchases;
        }
        public static Purchase GetMedicine(int? ID, int? MedicineID)
        {

            SqlParameter[] prm = new SqlParameter[]
            {
                //new SqlParameter("PurchaseID",ID),
                new SqlParameter("MedicineID",MedicineID),
                new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_Stock", prm);
            Purchase purchase = new Purchase();
            if (dt.Rows.Count > 0)
            { 
                //purchase.CompanyID = Convert.ToInt32(dt.Rows[0]["CompanyID"]);
                purchase.MedicineID = Convert.ToInt32(dt.Rows[0]["MedicineID"]);
                purchase.MedicineName = Convert.ToString(dt.Rows[0]["MedicineName"]);
                purchase.CompanyName = Convert.ToString(dt.Rows[0]["CompanyName"]);
                purchase.CompanyID = Convert.ToInt32(dt.Rows[0]["CompanyID"]);
                purchase.Quantity = Convert.ToInt32(dt.Rows[0]["Quantity"]);
                purchase.SalePrice = Convert.ToDecimal(dt.Rows[0]["SalePrice"]);
                purchase.ImageName = Convert.ToString(dt.Rows[0]["ImageName"]);

            }
            return purchase;
        }

    }
}