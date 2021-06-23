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
                new SqlParameter("ClientID",1),
                new SqlParameter("Credit",0),
                new SqlParameter("Debit",0),
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
                new SqlParameter("Credit",cmp.Credit),
                new SqlParameter("Debit",cmp.Debit),
                new SqlParameter("type",Actions.Update)
            };
            Helper.sp_ExecuteQuery("sp_Company", prm);
        }
        public static void UpdateCredit_Debit(int cid,decimal? credit)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("CompanyID",cid),
                new SqlParameter("Credit",credit),
                new SqlParameter("type",Actions.Update_Credit)
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
                cmp.Credit = Convert.ToDecimal(dt.Rows[0]["Credit"]);
                cmp.Debit = Convert.ToDecimal(dt.Rows[0]["Debit"]);
            }
            return cmp;
        }
        public static List<Company> GetCompanies(int ID, int? cid)
        {
            List<Company> cmps = new List<Company>();
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("ClientID",ID),
                new SqlParameter("CompanyID",cid),
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
                cmp.Credit = Convert.ToDecimal(dr["Credit"]);
                cmp.Debit = Convert.ToDecimal(dr["Debit"]);
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
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public int IsDelete { get; set; }
    }
}