using System;
using MedicineProject.Models.Custom;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace MedicineProject.Models.BLs
{
    public class BL_EmployeeRecovery
    {

        public static void Save(EmoloyeeRecovery c)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
               new SqlParameter("ClientName",c.CustomerID),
               new SqlParameter("CNIC",c.RecoveryAmount),
               new SqlParameter("Contact",c.CustomerRemaningPaymentID),
               new SqlParameter("type",Actions.Insert)
            };
            Helper.sp_ExecuteQuery("sp_EmployeRecovery", prm);
        }
        public static void Update(EmoloyeeRecovery c)
        {
            SqlParameter[] prm = new SqlParameter[]
             {
               new SqlParameter("EmployeeRecID",c.EmployeeRecID),
             new SqlParameter("CustomerID",c.CustomerID),
               new SqlParameter("RecoveryAmount",c.RecoveryAmount),
               new SqlParameter("CustomerRemaningPaymentID",c.CustomerRemaningPaymentID),
               new SqlParameter("type",Actions.Update)
             };
            Helper.sp_ExecuteQuery("sp_EmployeRecovery", prm);
        }
        public static void Delete(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
               new SqlParameter("EmployeeRecID",ID),
               new SqlParameter("type",Actions.Delete)
            };
            Helper.sp_ExecuteQuery("sp_EmployeRecovery", prm);
        }
        public static EmoloyeeRecovery GetDetail(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
               new SqlParameter("EmployeeRecID",ID),
               new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_EmployeRecovery", prm);
            EmoloyeeRecovery client = new EmoloyeeRecovery();
            if (dt.Rows.Count > 0)
            {
                client.ClientID = Convert.ToInt32(dt.Rows[0]["ClientID"]);
                client.EmployeeRecID = Convert.ToInt32(dt.Rows[0]["EmployeeRecID"]);
                client.CustomerID = Convert.ToInt32(dt.Rows[0]["CustomerID"]);
                client.RecoveryAmount = Convert.ToDecimal(dt.Rows[0]["RecoveryAmount"]);
                client.CustomerRemaningPaymentID = Convert.ToInt32(dt.Rows[0]["CustomerRemaningPaymentID"]);
              
            }
            return client;
        }

        public static List<EmoloyeeRecovery> GetDetail()
        {
            List<EmoloyeeRecovery> clients = new List<EmoloyeeRecovery>();
            SqlParameter[] prm = new SqlParameter[]
            {
             
               new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_EmployeRecovery", prm);


            foreach (DataRow dr in dt.Rows)
            {
                EmoloyeeRecovery client = new EmoloyeeRecovery();
                client.ClientID = Convert.ToInt32(dr["ClientID"]);
                client.EmployeeRecID = Convert.ToInt32(dr["EmployeeRecID"]);
                client.RecoveryAmount = Convert.ToDecimal(dr["RecoveryAmount"]);
                client.CustomerRemaningPaymentID = Convert.ToInt32(dr["CustomerRemaningPaymentID"]);
                clients.Add(client);
            }
            return clients;
        }
    }
    public class EmoloyeeRecovery
    {
        public int EmployeeRecID { get; set; }
        public int CustomerID { get; set; }
        public decimal RecoveryAmount { get; set; }
        public int CustomerRemaningPaymentID { get; set; }
        public int IsDelete { get; set; }
        public int ClientID { get; set; }
       
    
    
    }
}