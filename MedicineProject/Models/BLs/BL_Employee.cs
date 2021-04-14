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
    public class BL_Employee
    {
        public static void Save(Employee emp)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("EmployeeName",emp.EmployeeName),
                 new SqlParameter("Designation",emp.Designation),
                new SqlParameter("CNIC",emp.CNIC),
                new SqlParameter("Contact",emp.Contact),
                new SqlParameter("Address",emp.Address),
                 new SqlParameter("DateOfResign",emp.DateOfResign),
                 new SqlParameter("DateOfJoining",emp.DateOfJoining),
                  new SqlParameter("Salary",emp.Salary),
                new SqlParameter("ClientID",1),
                new SqlParameter("type",Actions.Insert)
            };
            Helper.sp_ExecuteQuery("sp_Employee", prm);
        }
        public static void Update(Employee emp)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("EmployeeID",emp.EmployeeID),
                new SqlParameter("EmployeeName",emp.EmployeeName),
                 new SqlParameter("Designation",emp.Designation),
                new SqlParameter("CNIC",emp.CNIC),
                new SqlParameter("Contact",emp.Contact),
                new SqlParameter("Address",emp.Address),
                 new SqlParameter("DateOfResign",emp.DateOfResign),
                 new SqlParameter("DateOfJoining",emp.DateOfJoining),
                  new SqlParameter("Salary",emp.Salary),
              
                new SqlParameter("type",Actions.Update)
            };
            Helper.sp_ExecuteQuery("sp_Employee", prm);
        }
        public static void Delete(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                 new SqlParameter("EmployeeID",ID),
                  new SqlParameter("type",Actions.Delete),
            };
            Helper.sp_ExecuteQuery("sp_Employee", prm);
        }
        public static Employee GetEmployee(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
             {
                  new SqlParameter("EmployeeID",ID),
                  new SqlParameter("type",Actions.Select),
             };
            DataTable dt = Helper.sp_Execute_Table("sp_Employee", prm);
            Employee emp = new Employee();
            if (dt.Rows.Count>0)
            {
                emp.EmployeeID = Convert.ToInt32(dt.Rows[0]["EmployeeID"]);
                emp.EmployeeName = Convert.ToString(dt.Rows[0]["EmployeeName"]);
                emp.Designation = Convert.ToString(dt.Rows[0]["Designation"]);
                emp.CNIC = Convert.ToString(dt.Rows[0]["CNIC"]);
                emp.Contact = Convert.ToString(dt.Rows[0]["Contact"]);
                emp.Address = Convert.ToString(dt.Rows[0]["Address"]);
                emp.DateOfJoining = Convert.ToDateTime(dt.Rows[0]["DateOfJoining"]);
                emp.DateOfResign = dt.Rows[0]["DateOfResign"] ==DBNull.Value ? (DateTime?)null : (DateTime?)dt.Rows[0]["DateOfResign"];
                emp.Salary = Convert.ToDecimal(dt.Rows[0]["Salary"]);
                emp.ClientID = Convert.ToInt32(dt.Rows[0]["ClientID"]);
            }
            return emp;
        }
        public static List<Employee> GetEmployees(int ID)
        {
            List<Employee> emps = new List<Employee>();
            SqlParameter[] prm = new SqlParameter[]
            {
                  new SqlParameter("ClientID",ID),
                  new SqlParameter("type",Actions.Select),
            };
            DataTable dt = Helper.sp_Execute_Table("sp_Employee", prm);

            foreach (DataRow dr in dt.Rows)
            {
                Employee emp = new Employee();
                emp.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);
                emp.EmployeeName = Convert.ToString(dr["EmployeeName"]);
                emp.Designation = Convert.ToString(dr["Designation"]);
                emp.CNIC = Convert.ToString(dr["CNIC"]);
                emp.Contact = Convert.ToString(dr["Contact"]);
                emp.Address = Convert.ToString(dr["Address"]);
                emp.DateOfJoining = Convert.ToDateTime(dr["DateOfJoining"]);
               //emp.DateOfResign = Convert.ToDateTime(dr["DateOfResign"]);
                emp.DateOfResign = dr.IsNull("DateOfResign") ? (DateTime?)null : (DateTime?)dr["DateOfResign"];
               //dueDate = dr.IsNull("DueDate") ? (DateTime?)null : (DateTime?)dr["DueDate"]
                emp.Salary = Convert.ToDecimal(dr["Salary"]);
                emp.ClientID = Convert.ToInt32(dr["ClientID"]);
                emps.Add(emp);
            }
            return emps;
        }
    }
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string CNIC { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateOfJoining { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DateOfResign { get; set; }
        public decimal Salary { get; set; }
        public int ClientID { get; set; }
        public int IsDelete { get; set; }
    }
}