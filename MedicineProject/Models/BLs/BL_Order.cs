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
                new SqlParameter("EmployeeID",order.EmployeeID),
                new SqlParameter("TotalAmount",order.TotalAmount),
                new SqlParameter("Quantity",order.Quantity),
                new SqlParameter("SaleDate",order.SaleDate),
                 new SqlParameter("IsPaid",order.IsPaid),
                new SqlParameter("type",Actions.Insert)
            };
            Helper.sp_ExecuteQuery("sp_CustomerInvoice", prm);
        }
        public static void Update(Order order)
        {
            SqlParameter[] prm = new SqlParameter[]
           {
                new SqlParameter("OrderID",order.OrderID),
                new SqlParameter("CustomerID",order.CustomerID),
                new SqlParameter("EmployeeID",order.EmployeeID),
                new SqlParameter("TotalAmount",order.TotalAmount),
                new SqlParameter("Quantity",order.Quantity),
                new SqlParameter("SaleDate",order.SaleDate),
                new SqlParameter("IsPaid",order.IsPaid),
                new SqlParameter("type",Actions.Insert)
           };
            Helper.sp_ExecuteQuery("sp_CustomerInvoice", prm);
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
        public static Order GetOrderInvoice_(int? OrderID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("OrderID",OrderID),
                new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_CustomerInvoice", prm);
            Order order = new Order();
            if (dt.Rows.Count > 0)
            {
                order.OrderID = Convert.ToInt32(dt.Rows[0]["OrderID"]);
                order.CustomerID = Convert.ToInt32(dt.Rows[0]["CustomerID"]);
                order.CustomerName = Convert.ToString(dt.Rows[0]["CustomerName"]);
                order.EmployeeID = Convert.ToInt32(dt.Rows[0]["EmployeeID"]);
                order.EmployeeName = Convert.ToString(dt.Rows[0]["EmployeeName"]);
                order.TotalAmount = Convert.ToDecimal(dt.Rows[0]["TotalAmount"]);
                order.Quantity = Convert.ToInt32(dt.Rows[0]["Quantity"]);
                order.IsPaid = Convert.ToInt32(dt.Rows[0]["IsPaid"]);
                order.SaleDate = Convert.ToDateTime(dt.Rows[0]["SaleDate"]);
            }
            return order;
        }
        public static Order GetOrderID_Only(int? CustomerID, decimal? TotalAmount)
        {
            SqlParameter[] prm = new SqlParameter[]
           {
                new SqlParameter("CustomerID",CustomerID),
                new SqlParameter("TotalAmount",TotalAmount),
                new SqlParameter("type",Actions.Select)
           };
            DataTable dt = Helper.sp_Execute_Table("sp_CustomerInvoice", prm);
            Order order = new Order();
            if (dt.Rows.Count > 0)
            {
                order.OrderID = Convert.ToInt32(dt.Rows[0]["OrderID"]);
                order.TotalAmount = Convert.ToDecimal(dt.Rows[0]["TotalAmount"]);
            }
            return order;
        }
        public static List<Order> GetOrdersInvoice_s(int? CustomerID)
        {
            List<Order> orders = new List<Order>();
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("CustomerID",CustomerID),
                new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_CustomerInvoice", prm);

            foreach (DataRow dr in dt.Rows)
            {
                Order order = new Order();
                order.OrderID = Convert.ToInt32(dr["OrderID"]);
                order.CustomerID = Convert.ToInt32(dr["CustomerID"]);
                order.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);
                order.CustomerName = Convert.ToString(dr["CustomerName"]);
                order.EmployeeName = Convert.ToString(dr["EmployeeName"]);
                order.TotalAmount = Convert.ToDecimal(dr["TotalAmount"]);
                order.Quantity = Convert.ToInt32(dr["Quantity"]);
                order.SaleDate = Convert.ToDateTime(dr["SaleDate"]);
                order.IsPaid = Convert.ToInt32(dr["IsPaid"]);
                orders.Add(order);
            }
            return orders;
        }
    }
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public string CustomerName { get; set; }
        public string EmployeeName { get; set; }
        public decimal TotalAmount { get; set; }
        public int Quantity { get; set; }
        public DateTime SaleDate { get; set; }
        public int IsPaid { get; set; }
        public int IsDelete { get; set; }
        public int ClientID { get; set; }

    }
}