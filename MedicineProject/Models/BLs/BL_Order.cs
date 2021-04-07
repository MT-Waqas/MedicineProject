using MedicineProject.Models.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using MedicineProject.Models;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MedicineProject.Models.BLs
{
    public class BL_Order
    {
        public static void Save(Order order)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                 new SqlParameter("CustomerID",order.CustomerID),
                new SqlParameter("DateOfOrder",order.DateOfOrder),           
                new SqlParameter("type",Actions.Insert)
            };
            Helper.sp_ExecuteQuery("sp_Order", prm);
        }
        public static void Update(Order order)
        {
            SqlParameter[] prm = new SqlParameter[]
           {
                new SqlParameter("OrderID",order.OrderID),
              
                new SqlParameter("DateOfOrder",order.DateOfOrder),
                new SqlParameter("type",Actions.Insert)
           };
            Helper.sp_ExecuteQuery("sp_Order", prm);
        }
        public static void Delete(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
          {
                new SqlParameter("OrderID",ID),
                new SqlParameter("type",Actions.Delete)
            };
            Helper.sp_ExecuteQuery("sp_Order", prm);
        }
        public static Order GetOrder(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("OrderID",ID),
                new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_Order", prm);
            Order order = new Order();
            if (dt.Rows.Count > 0)
            {
                order.OrderID = Convert.ToInt32(dt.Rows[0]["OrderID"]);
                order.CustomerID = Convert.ToInt32(dt.Rows[0]["CustomerID"]);
                order.CustomerName = Convert.ToString(dt.Rows[0]["CustomeName"]);
                order.DateOfOrder = Convert.ToDateTime(dt.Rows[0]["DateOfOrder"]);

            }
            return order;
        }
        public static List<Order> GetItems(int ClientID)
        {
            List<Order> orders = new List<Order>();
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("ClientID",ClientID),
                new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_Order", prm);

            foreach (DataRow dr in dt.Rows)
            {
                Order order = new Order();
                order.OrderID = Convert.ToInt32(dr["OrderID"]);
                order.CustomerID = Convert.ToInt32(dr["CustomerID"]);
                order.CustomerName = Convert.ToString(dr["CustomeName"]);
                order.DateOfOrder = Convert.ToDateTime(dr["DateOfOrder"]);
                orders.Add(order);
            }
            return orders;
        }
    }
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public DateTime DateOfOrder { get; set; }
        public int IsDelete { get; set; }
        public int ClientID { get; set; }

    }
}