using MedicineProject.Models.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MedicineProject.Models.BLs
{
    public class BL_Expense_Head
    {
        public static void Save(ExpenseHead head)
        {
            SqlParameter[] prm = new SqlParameter[]
                {

                   new SqlParameter("ExpenseHeadName",head.ExpenseHeadName),
                   new SqlParameter("Status",head.Status),
                   new SqlParameter("ClientID",head.ClientID),
                   new SqlParameter("type",Actions.Insert)
                };
            Helper.sp_ExecuteQuery("sp_ExpenseHead", prm);
        }
        public static void Update(ExpenseHead head)
        {
            SqlParameter[] prm = new SqlParameter[]
                {
                   new SqlParameter("ExpenseHeadID",head.ExpenseHeadID),
                   new SqlParameter("ExpenseHeadName",head.ExpenseHeadName),
                   new SqlParameter("Status",head.Status),
               
                   new SqlParameter("type",Actions.Update)
                };
            Helper.sp_ExecuteQuery("sp_ExpenseHead", prm);
        }
        public static void Delete(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
               {
                   new SqlParameter("ExpenseHeadID",ID),
                   new SqlParameter("type",Actions.Delete)
               };
            Helper.sp_ExecuteQuery("sp_ExpenseHead", prm);
        }
        public static ExpenseHead GetHead(int headID,int ClientID)
        {
            SqlParameter[] prm = new SqlParameter[]
               {
                   new SqlParameter("ExpenseHeadID",headID),
                   new SqlParameter("ClientID",ClientID),
                   new SqlParameter("type",Actions.Select)
               };
            DataTable dt= Helper.sp_Execute_Table("sp_ExpenseHead", prm);
            ExpenseHead head = new ExpenseHead();
            if (dt.Rows.Count>0)
            {
                head.ExpenseHeadID = Convert.ToInt32(dt.Rows[0]["ExpenseHeadID"]); 
                head.ExpenseHeadName = Convert.ToString(dt.Rows[0]["ExpenseHeadName"]);
                head.Status = Convert.ToBoolean(dt.Rows[0]["Status"]);
                head.ClientID = Convert.ToInt32(dt.Rows[0]["ClientID"]);
            }
            return head;
        }
        public static List<ExpenseHead> GetHeads(int ClientID)
        {
            List<ExpenseHead> heads = new List<ExpenseHead>();
            SqlParameter[] prm = new SqlParameter[]
               {
                   //new SqlParameter("ExpenseHeadID",headID),
                   new SqlParameter("ClientID",ClientID),
                   new SqlParameter("type",Actions.Select)
               };
            DataTable dt = Helper.sp_Execute_Table("sp_ExpenseHead", prm);
            foreach (DataRow dr in dt.Rows )
            {
                ExpenseHead head = new ExpenseHead();
                head.ExpenseHeadID = Convert.ToInt32(dt.Rows[0]["ExpenseHeadID"]);
                head.ExpenseHeadName = Convert.ToString(dt.Rows[0]["ExpenseHeadName"]);
                head.Status = Convert.ToBoolean(dt.Rows[0]["Status"]);
                head.ClientID = Convert.ToInt32(dt.Rows[0]["ClientID"]);
                heads.Add(head);
            }
            return heads;
        }
    }
    public class ExpenseHead
    {
        public int ExpenseHeadID { get; set; }
        public string ExpenseHeadName { get; set; }
        public bool Status { get; set; }
        public int ClientID { get; set; }
        public int IsDeleted { get; set; }
    }
}