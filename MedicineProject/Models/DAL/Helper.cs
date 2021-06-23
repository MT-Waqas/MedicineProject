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
        public static DataTable sp_Execute_Table(string Query)
        {
            SqlCommand cmd =new SqlCommand(Query, con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public static bool OrderID_Check()
        {
           DataTable dataTable = sp_Execute_Table("Select * from tbl_Purchase where  OrderID = (Select top 1 OrderID from tbl_Purchase_Order where ClientID=" + 1 + " order by orderID desc)");
           DataTable dataTable1= sp_Execute_Table("Select * from tbl_Purchase_Order where ClientID = " + 1);
            if (dataTable.Rows.Count>0)
            {
                return true;
            }
            else if (dataTable1.Rows.Count==0)
            {
                return true;
            }
            else
            {
                return false;
            }
          
        }
    }
}