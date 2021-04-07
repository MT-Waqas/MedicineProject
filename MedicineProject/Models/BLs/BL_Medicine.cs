using MedicineProject.Models.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MedicineProject.Models.BLs
{
    public class BL_Medicine
    {
        public static void Save(Medicine medicine)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                 new SqlParameter("CustomerID",medicine.MedicineName),
                new SqlParameter("DateOfOrder",medicine.CompanyID),
                new SqlParameter("ClientID",medicine.ClientID),
                new SqlParameter("type",Actions.Insert)
            };
            Helper.sp_ExecuteQuery("sp_Medicine", prm);
        }
        public static void Update(Medicine medicine)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("MedicineID",medicine.MedicineID),
                new SqlParameter("MedicineName",medicine.MedicineName),
                new SqlParameter("DateOfOrder",medicine.CompanyID),
                new SqlParameter("ClientID",medicine.ClientID),
                new SqlParameter("type",Actions.Update)
            };
            Helper.sp_ExecuteQuery("sp_Medicine", prm);
        }
        public static void Delete(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("MedicineID",ID),
                new SqlParameter("type",Actions.Delete)
            };
            Helper.sp_ExecuteQuery("sp_Medicine", prm);
        }
        public static Medicine GetMedicine(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("MedicineID",ID),
                new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_Medicine", prm);
            Medicine medicine = new Medicine();
            if (dt.Rows.Count > 0)
            {
                medicine.MedicineID = Convert.ToInt32(dt.Rows[0]["MedicineID"]);
                medicine.MedicineName = Convert.ToString(dt.Rows[0]["MedicineName"]);
                medicine.CompanyID = Convert.ToInt32(dt.Rows[0]["CompanyID"]);
                medicine.CompanyName = Convert.ToString(dt.Rows[0]["CompanyName"]);
            }
            return medicine;
        }
        public static List<Medicine> GetMedicines(int ClientID)
        {
            List<Medicine> medicines = new List<Medicine>();
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("ClientID",ClientID),
                new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_Medicine", prm);
            foreach (DataRow dr in dt.Rows)
            {
                Medicine medicine = new Medicine();
                medicine.MedicineID = Convert.ToInt32(dr["MedicineID"]);
                medicine.MedicineName = Convert.ToString(dr["MedicineName"]);
                medicine.CompanyID = Convert.ToInt32(dr["CompanyID"]);
                medicine.CompanyName = Convert.ToString(dr["CompanyName"]);
                medicines.Add(medicine);
            }
            return medicines;
        }
    }
    public class Medicine
    {
        public int MedicineID { get; set; }
        public string MedicineName { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public int IsDelete { get; set; }
        public int ClientID { get; set; }
    }
}