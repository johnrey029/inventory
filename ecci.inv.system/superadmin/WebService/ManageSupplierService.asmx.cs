using ecci.inv.system.superadmin.CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
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
        [WebMethod]
        public void GetSupplier()
        {
            con = new DBConnection();
            var supps = new List<ManageSupplier>();
            con.OpenConection();
            con._dr = con.DataReader(@"SELECT suppcode, suppname, suppadd, suppcontact FROM suppliers ORDER BY suppname ASC");
            while (con._dr.Read())
            {
                var sup = new ManageSupplier
                {
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
    }
}
