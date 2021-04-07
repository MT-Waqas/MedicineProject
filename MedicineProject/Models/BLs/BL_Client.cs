using MedicineProject.Models.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace MedicineProject.Models.BLs
{
    public class BL_Client
    {
        public static void Save(Client client)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
               new SqlParameter("ClientName",client.ClientName),
               new SqlParameter("CNIC",client.CNIC),
               new SqlParameter("Contact",client.Contact),
                new SqlParameter("Address",client.Address),
                new SqlParameter("ClientEmail",client.ClientEmail),
                new SqlParameter("Password",client.Password),
               new SqlParameter("type",Actions.Insert)
            };
            Helper.sp_ExecuteQuery("sp_Client", prm);
        }
        public static void Update(Client client)
        {
            SqlParameter[] prm = new SqlParameter[]
             {
               new SqlParameter("ClientID",client.ClientID),
              new SqlParameter("ClientName",client.ClientName),
               new SqlParameter("CNIC",client.CNIC),
               new SqlParameter("Contact",client.Contact),
                new SqlParameter("Address",client.Address),
                new SqlParameter("ClientEmail",client.ClientEmail),
                new SqlParameter("Password",client.Password),
               new SqlParameter("type",Actions.Update)
             };
            Helper.sp_ExecuteQuery("sp_Client", prm);
        }
        public static void Delete(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
               new SqlParameter("ClientID",ID),
               new SqlParameter("type",Actions.Delete)
            };
            Helper.sp_ExecuteQuery("sp_Client", prm);
        }
        public static Client GetClient(int ID)
        {
            SqlParameter[] prm = new SqlParameter[]
            {
               new SqlParameter("ClientID",ID),
               new SqlParameter("type",Actions.Select)
            };
            DataTable dt = Helper.sp_Execute_Table("sp_Client", prm);
            Client client = new Client();
            if (dt.Rows.Count>0)
            {
                client.ClientID = Convert.ToInt32(dt.Rows[0]["ClientID"]);
                client.ClientName = Convert.ToString(dt.Rows[0]["ClientName"]);
                client.CNIC = Convert.ToString(dt.Rows[0]["CNIC"]);
                client.Contact = Convert.ToString(dt.Rows[0]["Contact"]);
                client.Address = Convert.ToString(dt.Rows[0]["Address"]);
                client.ClientEmail = Convert.ToString(dt.Rows[0]["ClientEmail"]);
                client.Password = Convert.ToString(dt.Rows[0]["Password"]);
            }
            return client;
        }
        public static List<Client>  GetClients()
        {
            List<Client> clients = new List<Client>();
            SqlParameter[] prm = new SqlParameter[1];
            prm[0] = new SqlParameter("@type", Actions.Select);
            DataTable dt = Helper.sp_Execute_Table("sp_Client", prm);


            foreach (DataRow dr in dt.Rows)
            {
                Client client = new Client();
                client.ClientID = Convert.ToInt32(dr["ClientID"]);
                client.ClientName = Convert.ToString(dr["ClientName"]);
                client.CNIC = Convert.ToString(dr["CNIC"]);
                client.Contact = Convert.ToString(dr["Contact"]);
                client.Address = Convert.ToString(dr["Address"]);
                client.ClientEmail = Convert.ToString(dr["ClientEmail"]);
                client.Password = Convert.ToString(dr["Password"]);
                clients.Add(client);
            }
            return clients;
        }

    }
    public class Client
    {
        public int ClientID { get; set; }
        public string ClientName {get; set;}
        public string CNIC { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string ClientEmail { get; set; }
        public string Password { get; set; }
        public int IsDelete { get; set; }
    }
}