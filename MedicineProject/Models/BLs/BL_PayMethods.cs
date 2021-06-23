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
    public class BL_PayMethods
    {
        public static void Save(PayMethods pay)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("PaymentMethodName",pay.PaymentMethodName),
                new SqlParameter("Status",pay.Status),
                new SqlParameter("type",Actions.Insert)
            };
            Helper.sp_ExecuteQuery("sp_PaymentMethod", prm);
        }
        public static void Update(PayMethods pay)
        {
            SqlParameter[] prm = new SqlParameter[]
           {
               new SqlParameter("PaymentMethodID",pay.PaymentMethodID),
                new SqlParameter("PaymentMethodName",pay.PaymentMethodName),
                new SqlParameter("Status",pay.Status),
                new SqlParameter("type",Actions.Update)
           };
            Helper.sp_ExecuteQuery("sp_PaymentMethod", prm);
        }
        public static void Delete(int PaymentMethodID)
        {
            SqlParameter[] prm = new SqlParameter[]
          {
                new SqlParameter("PaymentMethodID",PaymentMethodID),
                new SqlParameter("type",Actions.Delete)
          };
            Helper.sp_ExecuteQuery("sp_PaymentMethod", prm);
        }
        public static List<PayMethods> GetPaymentMethods()
        {
            List<PayMethods> paymentMethods = new List<PayMethods>();
            SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_PaymentMethod", prm);
            foreach (DataRow dr in dt.Rows)
            {
                PayMethods methods = new PayMethods();
                methods.PaymentMethodID = Convert.ToInt32(dr["PaymentMethodID"]);
                methods.PaymentMethodName = Convert.ToString(dr["PaymentMethodName"]);
                methods.Status = Convert.ToInt32(dr["Status"]);
                paymentMethods.Add(methods);
            }
            return paymentMethods;
        }
        public static PayMethods GetPaymentMethod(int PaymentMethodID)
        {
            SqlParameter[] prm = new SqlParameter[]
            { 
                new SqlParameter("PaymentMethodID",PaymentMethodID),
                new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_PaymentMethod", prm);
            PayMethods methods = new PayMethods();
            if (dt.Rows.Count>0)
            { 
                methods.PaymentMethodID = Convert.ToInt32(dt.Rows[0]["PaymentMethodID"]);
                methods.PaymentMethodName = Convert.ToString(dt.Rows[0]["PaymentMethodName"]);
                methods.Status = Convert.ToInt32(dt.Rows[0]["Status"]);
            }
            return methods;
        }
    }
    public class PayMethods
    {
        public int? PaymentMethodID { get; set; }
        [Required]
        public string PaymentMethodName { get; set; }
        public int Status { get; set; }
    }
}