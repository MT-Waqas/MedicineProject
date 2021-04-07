using MedicineProject.Models.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MedicineProject.Models.BLs
{
    public class BL_Company
    {
        public static void Save(Company cmp)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("CompanyName",cmp.CompanyName),
                new SqlParameter("Address",cmp.Address),
                new SqlParameter("Contact",cmp.Contact),
                new SqlParameter("ClientID",cmp.ClientID),
                new SqlParameter("type",Actions.Insert)
            };
            Helper.sp_ExecuteQuery("sp_Company", prm);
        }
        public static void Update(Company cmp)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("CompanyID",cmp.CompanyID),
                new SqlParameter("CompanyName",cmp.CompanyName),
                new SqlParameter("Address",cmp.Address),
                new SqlParameter("Contact",cmp.Contact),
           
                new SqlParameter("type",Actions.Update)
            };
            Helper.sp_ExecuteQuery("sp_Company", prm);
        }
        public static void Delete(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
           {
                new SqlParameter("CompanyID",ID),
                new SqlParameter("type",Actions.Delete)
           };
            Helper.sp_ExecuteQuery("sp_Company", prm);
        }
        public static Company GetCompany(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
         {
                new SqlParameter("CompanyID",ID),
                new SqlParameter("type",Actions.Select)
         };
            DataTable dt = Helper.sp_Execute_Table("sp_Company", prm);
            Company cmp = new Company();
            if (dt.Rows.Count > 0)
            {
                cmp.CompanyID = Convert.ToInt32(dt.Rows[0]["CompanyID"]);
                cmp.CompanyName = Convert.ToString(dt.Rows[0]["CompanyName"]);
                cmp.Address = Convert.ToString(dt.Rows[0]["Address"]);
                cmp.Contact = Convert.ToString(dt.Rows[0]["Contact"]);
                cmp.ClientID = Convert.ToInt32(dt.Rows[0]["ClientID"]);
            }
            return cmp;
        }
        public static List<Company> GetCompanies(int ID)
        {
            List<Company> cmps = new List<Company>();
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("ClientID",ID),
                new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_Company", prm);
            foreach (DataRow dr in dt.Rows)
            {
                Company cmp = new Company();
                cmp.CompanyID = Convert.ToInt32(dr["CompanyID"]);
                cmp.CompanyName = Convert.ToString(dr["CompanyName"]);
                cmp.Address = Convert.ToString(dr["Address"]);
                cmp.Contact = Convert.ToString(dr["Contact"]);
                cmp.ClientID = Convert.ToInt32(dr["ClientID"]);
                cmps.Add(cmp);
            }
            return cmps;
        }

    }
    public class Company
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public int ClientID { get; set; }
        public int IsDelete { get; set; }
    }
}