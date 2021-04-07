using MedicineProject.Models.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MedicineProject.Models.BLs
{
    public class BL_Client_Expense
    {
        public static void Save(ClientExpense clientExpense)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                  new SqlParameter("ClientID",clientExpense.ClientID),
                new SqlParameter("ExpenseHeadID",clientExpense.ExpenseHeadID),
                new SqlParameter("ExpenseAmount",clientExpense.ExpenseAmount),
                 new SqlParameter("type",Actions.Insert)
            };
            Helper.sp_ExecuteQuery("sp_ClientExpense", prm);
        }
        public static void Update(ClientExpense clientExpense)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("ClientExpenseID",clientExpense.ClientExpenseID),
                new SqlParameter("ExpenseHeadID",clientExpense.ExpenseHeadID),
                new SqlParameter("ExpenseAmount",clientExpense.ExpenseAmount),
                 new SqlParameter("type",Actions.Update)
            };
            Helper.sp_ExecuteQuery("sp_ClientExpense", prm);
        }
        public static void Delete(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("ClientExpenseID",ID),
                new SqlParameter("type",Actions.Delete)
            };
            Helper.sp_ExecuteQuery("sp_ClientExpense", prm);
        }
        public static ClientExpense GetClientExpense(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("ClientExpenseID",ID),
                new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_ClientExpense", prm);
            ClientExpense clientExpense = new ClientExpense();
            if (dt.Rows.Count>0)
            {
                clientExpense.ClientExpenseID = Convert.ToInt32(dt.Rows[0]["ClientExpenseID"]);
                clientExpense.ExpenseHeadID = Convert.ToInt32(dt.Rows[0]["ExpenseHeadID"]);
                clientExpense.ClientID = Convert.ToInt32(dt.Rows[0]["ClientID"]);
                clientExpense.ExpenseAmount = Convert.ToDecimal(dt.Rows[0]["ExpenseAmount"]);
            }
            return clientExpense;
        }
        public static List<ClientExpense> GetClientExpenses(int ClientID)
        {
            List<ClientExpense> clientExpenses = new List<ClientExpense>();
            SqlParameter[] prm = new SqlParameter[]
           {
                new SqlParameter("ClientID",ClientID),
                new SqlParameter("type",Actions.Select)
           };
            DataTable dt = Helper.sp_Execute_Table("sp_ClientExpense", prm);

            foreach (DataRow dr in dt.Rows)
            {
                ClientExpense clientExpense = new ClientExpense();
                clientExpense.ClientExpenseID = Convert.ToInt32(dr["ClientExpenseID"]);
                clientExpense.ExpenseHeadID = Convert.ToInt32(dr["ExpenseHeadID"]);
                clientExpense.ClientID = Convert.ToInt32(dr["ClientID"]);
                clientExpense.ExpenseAmount = Convert.ToDecimal(dr["ExpenseAmount"]);
                clientExpenses.Add(clientExpense);
            }
            return clientExpenses;
        }
    }
    public class ClientExpense
    {
        public int ClientExpenseID { get; set; }
        public int ExpenseHeadID { get; set; }
        public int ClientID { get; set; }
        public decimal ExpenseAmount { get; set; }
        public int IsDeleted { get; set; }

    }




}