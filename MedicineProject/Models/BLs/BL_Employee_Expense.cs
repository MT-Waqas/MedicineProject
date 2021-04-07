using MedicineProject.Models.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MedicineProject.Models.BLs
{
    public class BL_Employee_Expense
    {
        public static void Save(EmployeeExpense expense)
        {
            SqlParameter[] prm = new SqlParameter[]
                {
                   new SqlParameter("EmployeeID",expense.EmployeeID),
                   new SqlParameter("ExpenseHeadID",expense.ExpenseHeadID),
                   new SqlParameter("ExpenseAmount",expense.ExpenseAmount),
                   new SqlParameter("ClientID",expense.ClientID),
                   new SqlParameter("type",Actions.Insert)
                };
            Helper.sp_ExecuteQuery("sp_EmployeeExpense", prm);
        }
        public static void Update(EmployeeExpense expense)
        {
            SqlParameter[] prm = new SqlParameter[]
                {
                   new SqlParameter("EmployeeExpenseID",expense.EmployeeExpenseID),
                   new SqlParameter("EmployeeID",expense.EmployeeID),
                   new SqlParameter("ExpenseHeadID",expense.ExpenseHeadID),
                   new SqlParameter("ExpenseAmount",expense.ExpenseAmount),
             
                   new SqlParameter("type",Actions.Update)
                };
            Helper.sp_ExecuteQuery("sp_EmployeeExpense", prm);
        }
        public static void Delete (int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
                {
                  new SqlParameter("EmployeeExpenseID",ID),
                   new SqlParameter("type",Actions.Delete)
                };
            Helper.sp_ExecuteQuery("sp_EmployeeExpense", prm);
        }
        public static EmployeeExpense GetEExpense(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
               {
                  new SqlParameter("EmployeeExpenseID",ID),
                   new SqlParameter("type",Actions.Select)
               };
            DataTable dt = Helper.sp_Execute_Table("sp_EmployeeExpense", prm);
            EmployeeExpense expense = new EmployeeExpense();
            if (dt.Rows.Count > 0)
            {
                expense.EmployeeExpenseID = Convert.ToInt32(dt.Rows[0]["EmployeeExpenseID"]);
                expense.EmployeeID = Convert.ToInt32(dt.Rows[0]["EmployeeID"]);
                expense.ExpenseHeadID = Convert.ToInt32(dt.Rows[0]["ExpenseHeadID"]);
                expense.ExpenseAmount = Convert.ToDecimal(dt.Rows[0]["ExpenseAmount"]);
                expense.ClientID = Convert.ToInt32(dt.Rows[0]["ClientID"]);
            }
            return expense;
        }
        public static List<EmployeeExpense> GetEExpenses(int ClientID)
        {
            List<EmployeeExpense> expenses = new List<EmployeeExpense>();
            SqlParameter[] prm = new SqlParameter[]
               {
                  new SqlParameter("ClientID",ClientID),
                   new SqlParameter("type",Actions.Select)
               };
            DataTable dt = Helper.sp_Execute_Table("sp_EmployeeExpense", prm);
            foreach (DataRow dr in dt.Rows )
            {
                EmployeeExpense expense = new EmployeeExpense();
                expense.EmployeeExpenseID = Convert.ToInt32(dr["EmployeeExpenseID"]);
                expense.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);
                expense.ExpenseHeadID = Convert.ToInt32(dr["ExpenseHeadID"]);
                expense.ExpenseAmount = Convert.ToDecimal(dr["ExpenseAmount"]);
                expense.ClientID = Convert.ToInt32(dr["ClientID"]);
                expenses.Add(expense);
            }
            return expenses;
        }
    }
    public class EmployeeExpense
    {
        public int EmployeeExpenseID { get; set; }
        public int EmployeeID { get; set; }
        public int ExpenseHeadID { get; set; }
        public decimal ExpenseAmount { get; set; }
        public int IsDeleted { get; set; }
        public int ClientID { get; set; }
    }
}