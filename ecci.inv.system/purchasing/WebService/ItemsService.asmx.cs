using ecci.inv.system.purchasing.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace ecci.inv.system.purchasing.WebService
{
    /// <summary>
    /// Summary description for ItemsService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ItemsService : System.Web.Services.WebService
    {
        DBConnection con;
        [WebMethod]
        public void GetItem()
        {
            con = new DBConnection();
            var items = new List<Item>();
            con.OpenConection();
            con._dr = con.DataReader("SELECT brandname,description FROM items ORDER BY itemsid ASC");
            while (con._dr.Read())
            {
                var item = new Item
                {
                    brandName = con._dr["brandname"].ToString(),
                    description = con._dr["description"].ToString()
                };
                items.Add(item);
            }
            con._dr.Close();
            con.CloseConnection();
            var js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(items));
        }
    }
}
