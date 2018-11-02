using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using ecci.inv.system.admin.CS;

namespace ecci.inv.system.admin.WebService
{
    /// <summary>
    /// Summary description for ManageClientService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ManageClientService : System.Web.Services.WebService
    {

        DBConnection con;
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetClient()
        {
            con = new DBConnection();
            var clients = new List<ManageClient>();
            con.OpenConection();
            con._dr = con.DataReader(@"SELECT * FROM client ORDER BY name ASC");
            while (con._dr.Read())
            {
                var cli = new ManageClient
                {
                    clientId = Convert.ToInt32(con._dr["clientid"].ToString()),
                    clientName = con._dr["name"].ToString(),
                    clientAdd = con._dr["address"].ToString(),
                    clientCon = con._dr["contact1"].ToString(),
                    clientCon2 = con._dr["contact2"].ToString(),
                    clientStatus = Convert.ToInt32(con._dr["contactstatus"].ToString())
                };
                clients.Add(cli);
            }
            con._dr.Close();
            con.CloseConnection();
            var js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(clients));
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetClientById(int id)
        {
            con = new DBConnection();
            ManageClient clie = new ManageClient();
            con.OpenConection();
            con._dr = con.DataReader(@"SELECT * FROM client WHERE clientid='" + id + "'");
            while (con._dr.Read())
            {
                clie.clientId = Convert.ToInt32(con._dr["clientid"].ToString());
                clie.clientName = con._dr["name"].ToString();
                clie.clientAdd = con._dr["address"].ToString();
                clie.clientCon = con._dr["contact1"].ToString();
                clie.clientCon2 = con._dr["contact2"].ToString();
                clie.clientStatus = Convert.ToInt32(con._dr["contactstatus"].ToString());
            }
            con._dr.Close();
            con.CloseConnection();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(clie));
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int updateClientById(int clid, string address, string contact, string contact2, int stat)
        {
            con = new DBConnection();
            int a = 0;
            con.OpenConection();
            con.ExecSqlQuery(@"UPDATE client SET address=@cadd, contact1=@con1, contact2=@con2, contactstatus=@stat WHERE clientid = @clid");
            con.Cmd.Parameters.AddWithValue("@clid", Convert.ToInt32(clid));
            con.Cmd.Parameters.AddWithValue("@cadd", address);
            con.Cmd.Parameters.AddWithValue("@con1", contact);
            con.Cmd.Parameters.AddWithValue("@con2", contact2);
            con.Cmd.Parameters.AddWithValue("@stat", stat);

            a = con.Cmd.ExecuteNonQuery();
            con.CloseConnection();

            return a;
        }
    }
}
