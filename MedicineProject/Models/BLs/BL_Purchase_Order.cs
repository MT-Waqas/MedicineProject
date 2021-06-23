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
    public class BL_Purchase_Order
    {
        public static void Save(Purchase_Order purchase_Order)
        {

          
            if (Helper.OrderID_Check())
            {
                SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("ClientID",1),
                new SqlParameter("type",Actions.Insert)
            };
                Helper.sp_ExecuteQuery("sp_Purchase_Order", prm);
            }

        }
        public static void Delete(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("ClientID",ID),
                new SqlParameter("type",Actions.Delete)
            };
            Helper.sp_ExecuteQuery("sp_Purchase_Order", prm);
        }
        public static Purchase_Order GetPurchaseOrder(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
                        {
                new SqlParameter("ClientID",ID),
                new SqlParameter("type",Actions.Select)
                        };
            DataTable dt = Helper.sp_Execute_Table("sp_Purchase_Order", prm);
            Purchase_Order purchase_Order = new Purchase_Order();
            if (dt.Rows.Count > 0)
            {
                purchase_Order.OrderID = Convert.ToInt32(dt.Rows[0]["OrderID"]);
                purchase_Order.ClientID = Convert.ToInt32(dt.Rows[0]["ClientID"]);
                //purchase_Order.OrderDate = Convert.ToDateTime(dt.Rows[0]["OrderDate"]);

            }
            return purchase_Order;
        }
    }
    public class Purchase_Order
    {
        public int? OrderID { get; set; }
        public int ClientID { get; set; }
        //public DateTime OrderDate { get; set; }
    }
}