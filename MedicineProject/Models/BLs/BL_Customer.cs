using MedicineProject.Models.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MedicineProject.Models.BLs
{
    public class BL_Customer
    {
        public static void Save(Customer customer)
        {
       
            SqlParameter[] prm = new SqlParameter[]
            {
                
                new SqlParameter("CustomerName",customer.CustomerName),
                new SqlParameter("Address",customer.Address),
                new SqlParameter("Contact",customer.Contact),
                new SqlParameter("ClientID",1),
                new SqlParameter("type",Actions.Insert)
            };
            Helper.sp_ExecuteQuery("sp_Customer", prm);
        }
        public static void Update(Customer customer)
        {
            SqlParameter[] prm = new SqlParameter[]
           {
                new SqlParameter("CustomerID",customer.CustomerID),
                new SqlParameter("CustomerName",customer.CustomerName),
                new SqlParameter("Address",customer.Address),
                   new SqlParameter("Contact",customer.Contact),
                new SqlParameter("ClientID",customer.ClientID),
                new SqlParameter("type",Actions.Update)
           };
            Helper.sp_ExecuteQuery("sp_Customer", prm);
        }
        public static void Delete(int id)
        {
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("CustomerID", id);
            para[1] = new SqlParameter("type", Actions.Delete);
            Helper.sp_ExecuteQuery("sp_Customer", para);
        }
        public static Customer GetCustomer(int ID)
        {

            SqlParameter[] prm = new SqlParameter[]
           {
                new SqlParameter("CustomerID",ID),
                new SqlParameter("type",Actions.Select)
           };
            DataTable dt = Helper.sp_Execute_Table("sp_Customer", prm);
            Customer customer = new Customer();
            if (dt.Rows.Count>0)
            {
                customer.CustomerID = Convert.ToInt32(dt.Rows[0]["CustomerID"]);
                customer.CustomerName = Convert.ToString(dt.Rows[0]["CustomerName"]);
                customer.Address = Convert.ToString(dt.Rows[0]["Address"]);
                customer.Contact = Convert.ToString(dt.Rows[0]["Contact"]);
                customer.ClientID = Convert.ToInt32(dt.Rows[0]["ClientID"]);
            }
            return customer;
        }
        public static List<Customer> GetCustomers(int ClientID)
        {
            List<Customer> customers = new List<Customer>();
            SqlParameter[] prm = new SqlParameter[]
          {
                new SqlParameter("ClientID",ClientID),
                new SqlParameter("type",Actions.Select)
          };
            DataTable dt = Helper.sp_Execute_Table("sp_Customer", prm);

            foreach (DataRow dr in dt.Rows)
            {
                Customer customer = new Customer();
                customer.CustomerID = Convert.ToInt32(dr["CustomerID"]);
                customer.Contact = Convert.ToString(dr["Contact"]);
                customer.CustomerName = Convert.ToString(dr["CustomerName"]);
                customer.Address = Convert.ToString(dr["Address"]);
                customer.ClientID = Convert.ToInt32(dr["ClientID"]);
                customers.Add(customer);
            }
            return customers;
        }
    }
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public int ClientID { get; set; }
        public int IsDelete { get; set; }
    }
}