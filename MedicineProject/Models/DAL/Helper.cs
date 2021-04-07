using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MedicineProject.Models
{
    public class Helper
    {

        public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ToString());

      //static  SqlConnection con =new SqlConnection("Data Source=DESKTOP-T;  Initial Catalog=Medicin_Management_System; integrated Security=true;");
         public static bool sp_ExecuteQuery(string storeprocedure,SqlParameter[] prm)
         {
            SqlCommand cmd = new SqlCommand(storeprocedure,con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(prm);
            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception e)
            {
                con.Close();
                return false;
            }
         }
        public static DataTable sp_Execute_Table(string storeprocedure,SqlParameter[] prm)
        {
            SqlCommand cmd = new SqlCommand(storeprocedure,con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(prm);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

                 
    }
}