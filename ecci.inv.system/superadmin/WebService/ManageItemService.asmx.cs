using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using ecci.inv.system.superadmin.CS;

namespace ecci.inv.system.superadmin.WebService
{
    /// <summary>
    /// Summary description for ManageItemService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ManageItemService : System.Web.Services.WebService
    {
        DBConnection con;
        [WebMethod]
        public void GetItem()
        {
            con = new DBConnection();
            var orders = new List<ManageItem>();
            con.OpenConection();
            con._dr = con.DataReader(@"SELECT suppliers.suppname AS supplierName, items.brandname AS brandname, items.description AS description, items.unitprice AS price FROM suppliers INNER JOIN items ON suppliers.suppcode = items.suppcode ORDER BY suppliers.suppname ASC");
            while (con._dr.Read())
            {

                var order = new ManageItem
                {
                    suppName = con._dr["supplierName"].ToString(),
                    brandName = con._dr["brandname"].ToString(),
                    description = con._dr["description"].ToString(),
                    unitPrice = con._dr["price"].ToString()
                };

                orders.Add(order);
            }
            con._dr.Close();
            //con.CloseConnection();
            var js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(orders));
            con.CloseConnection();
        }
    }
}
