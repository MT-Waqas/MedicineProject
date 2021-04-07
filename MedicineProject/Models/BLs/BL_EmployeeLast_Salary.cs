using MedicineProject.Models.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MedicineProject.Models.BLs
{
    public class BL_EmployeeLast_Salary
    {
        public static void Save(EmployeeLastSalary lastSalary)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("EmployeeID",lastSalary.EmployeeID),
                new SqlParameter("LastSalary", lastSalary.LastSalary),
                new SqlParameter("DateOfLastSalary",lastSalary.DateOfLastSalary),
                new SqlParameter("ClientID",lastSalary.ClientID),
                new SqlParameter("type",Actions.Insert)
            };
            Helper.sp_ExecuteQuery("sp_EmployeeLastSalary", prm);
        }
        public static void Update(EmployeeLastSalary lastSalary)
        {

            SqlParameter[] prm = new SqlParameter[]
            {
                 new SqlParameter("EmpLastSalaryID",lastSalary.EmpLastSalaryID),
                new SqlParameter("LastSalary", lastSalary.LastSalary),
                new SqlParameter("DateOfLastSalary",lastSalary.DateOfLastSalary),
               
                new SqlParameter("type",Actions.Update)
            };
            Helper.sp_ExecuteQuery("sp_EmployeeLastSalary", prm);
        }
        public static void Delete(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                 new SqlParameter("EmpLastSalaryID",ID),
                new SqlParameter("type",Actions.Delete)
            };
            Helper.sp_ExecuteQuery("sp_EmployeeLastSalary", prm);
        }
        public static List<EmployeeLastSalary> GetEmployeeLastSalary(int ClientID)
        {
            List<EmployeeLastSalary> lastSalaries = new List<EmployeeLastSalary>();
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("ClientID",ClientID),
                new SqlParameter("type",Actions.Select),
            };
            DataTable dt = Helper.sp_Execute_Table("sp_EmployeeLastSalary", prm);
            
            foreach (DataRow dr in dt.Rows)
            {
                EmployeeLastSalary lastSalary = new EmployeeLastSalary();
                lastSalary.EmployeeName = Convert.ToString(dr["EmployeeName"]);
                lastSalary.Designation = Convert.ToString(dr["Designation"]);
                lastSalary.LastSalary = Convert.ToDecimal(dr["LastSalary"]);    
                lastSalary.DateOfLastSalary = Convert.ToDateTime(dr["DateOfLastSalary"]);
                lastSalary.ClientID = Convert.ToInt32(dr["Client"]);
                lastSalaries.Add(lastSalary);
            }
            return lastSalaries;
        }
    }
    public class EmployeeLastSalary
    {
        public int EmpLastSalaryID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public decimal LastSalary { get; set; }
        public DateTime DateOfLastSalary { get; set; }
        public int IsDelete { get; set; }
        public int ClientID { get; set; }
    }
}