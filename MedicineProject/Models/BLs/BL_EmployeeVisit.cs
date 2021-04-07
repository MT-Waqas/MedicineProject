using MedicineProject.Models.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MedicineProject.Models.BLs
{
    public class BL_EmployeeVisit
    {
        public static void Save(EmployeeVisit visit)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("EmployeeID",visit.EmployeeID),
                new SqlParameter("Distance",visit.Distance),
                new SqlParameter("RS",visit.RS),
                new SqlParameter("EmployeeVisiteDate",visit.EmployeeVisiteDate),
                new SqlParameter("ClientID",visit.ClientID),
                new SqlParameter("EmployeeID",Actions.Insert)
            };
            Helper.sp_ExecuteQuery("sp_EmployeeVisit", prm);
        }
        public static void Update(EmployeeVisit visit)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("EmployeeVisitID",visit.EmployeeVisitID),
                new SqlParameter("EmployeeID",visit.EmployeeID),
                new SqlParameter("Distance",visit.Distance),
                new SqlParameter("RS",visit.RS),
                new SqlParameter("EmployeeVisiteDate",visit.EmployeeVisiteDate),
                new SqlParameter("type",Actions.Update)
            };
            Helper.sp_ExecuteQuery("sp_EmployeeVisit", prm);
        }
        public static void Delete(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
           {
                new SqlParameter("EmployeeVisitID",ID),
                new SqlParameter("type",Actions.Update)
           };
            Helper.sp_ExecuteQuery("sp_EmployeeVisit", prm);
        }
        public static EmployeeVisit GetVisit(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("EmployeeVisitID",ID),
                new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_EmployeeVisit", prm);
            EmployeeVisit visit = new EmployeeVisit();
            if (dt.Rows.Count > 0)
            {
                visit.EmployeeVisitID = Convert.ToInt32(dt.Rows[0]["EmployeeVisitID"]);
                visit.EmployeeID = Convert.ToInt32(dt.Rows[0]["EmployeeID"]);
                visit.Distance = Convert.ToInt32(dt.Rows[0]["Distance"]);
                visit.RS = Convert.ToInt32(dt.Rows[0]["RS"]);
                visit.EmployeeVisiteDate = Convert.ToInt32(dt.Rows[0]["EmployeeVisiteDate"]);
            }
            return visit;
        }
        public static List<EmployeeVisit> GetVisits(int ClientID)
        {
            List<EmployeeVisit> visits = new List<EmployeeVisit>();
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("ClientID",ClientID),
                new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_EmployeeVisit", prm);
            foreach (DataRow dr in dt.Rows)
            {
                EmployeeVisit visit = new EmployeeVisit();
                visit.EmployeeVisitID = Convert.ToInt32(dr["EmployeeVisitID"]);
                visit.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);
                visit.Distance = Convert.ToInt32(dr["Distance"]);
                visit.RS = Convert.ToInt32(dr["RS"]);
                visit.EmployeeVisiteDate = Convert.ToInt32(dr["EmployeeVisiteDate"]);
                visits.Add(visit);
            }
            return visits;
        }
}
public class EmployeeVisit
{
    public int EmployeeVisitID { get; set; }
    public int EmployeeID { get; set; }
    public int Distance { get; set; }
    public decimal RS { get; set; }
    public int EmployeeVisiteDate { get; set; }
    public int IsDelete { get; set; }
    public int ClientID { get; set; }
}

}