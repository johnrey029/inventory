using ecci.inv.system.superadmin.CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace ecci.inv.system.superadmin.WebService
{
    /// <summary>
    /// Summary description for ManageSupplierService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ManageSupplierService : System.Web.Services.WebService
    {
        DBConnection con;
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetSupplier()
        {
            con = new DBConnection();
            var supps = new List<ManageSupplier>();
            con.OpenConection();
            con._dr = con.DataReader(@"SELECT * FROM suppliers ORDER BY suppname ASC");
            while (con._dr.Read())
            {
                var sup = new ManageSupplier
                {
                    suppId = Convert.ToInt32(con._dr["suppid"].ToString()),
                    suppCode = con._dr["suppcode"].ToString(),
                    suppName = con._dr["suppname"].ToString(),
                    suppAdd = con._dr["suppadd"].ToString(),
                    suppContact = con._dr["suppcontact"].ToString()
                };
                supps.Add(sup);
            }
            con._dr.Close();
            con.CloseConnection();
            var js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(supps));
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetSupplierById(int id)
        {
            con = new DBConnection();
            ManageSupplier suppl = new ManageSupplier();
            con.OpenConection();
            con._dr = con.DataReader(@"SELECT * FROM suppliers WHERE suppid='" + id + "'");
            while (con._dr.Read())
            {
                suppl.suppId = Convert.ToInt32(con._dr["suppid"].ToString());
                suppl.suppName = con._dr["suppname"].ToString();
                suppl.suppAdd = con._dr["suppadd"].ToString();
                suppl.suppCode = con._dr["suppcode"].ToString();
                suppl.suppContact = con._dr["suppcontact"].ToString();
            }
            con._dr.Close();
            con.CloseConnection();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(suppl));
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int updateSupplierById(int spid, string address, string contact)
        {
            con = new DBConnection();
            int a = 0;
            con.OpenConection();
            con.ExecSqlQuery(@"UPDATE supplier SET suppadd = @sadd, suppcontact = @scontact WHERE itemsid = @spid");
            con.Cmd.Parameters.AddWithValue("@spid", Convert.ToInt32(spid));
            //con.Cmd.Parameters.AddWithValue("@sadd", tbSuppAddress.Text);
            //con.Cmd.Parameters.AddWithValue("@scontact", tbSuppContact.Text);
            a = con.Cmd.ExecuteNonQuery();
            con.CloseConnection();

            return a;
        }
    }
}
