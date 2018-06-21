using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;

namespace ecci.inv.system.purchasing.WebService
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
            con._dr = con.DataReader(
            @"SELECT suppcode, brandname, description FROM items ORDER BY itemsid ASC");
            while (con._dr.Read())
            {

                var order = new ManageItem
                {
                    suppCode = con._dr["suppcode"].ToString(),
                    brandName = con._dr["brandname"].ToString(),
                    description = con._dr["description"].ToString()
                };

                orders.Add(order);
            }
            con._dr.Close();
            con.CloseConnection();
            var js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(orders));
        }
        public class ManageItem
        {
            public string suppCode { get; set; }
            public string brandName { get; set; }
            public string description { get; set; }
        }
    }
}
