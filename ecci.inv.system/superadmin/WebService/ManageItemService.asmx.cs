using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using ecci.inv.system.superadmin.CS;
using System.Web.Script.Services;

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
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetItem()
        {
            con = new DBConnection();
            var item = new List<ManageItem>();
            con.OpenConection();
            con._dr = con.DataReader(@"SELECT s.suppname, s.suppcode, i.brandname, i.description, i.unitprice, i.itemsid 
			FROM items i INNER JOIN suppliers s ON s.suppcode=i.suppcode ORDER BY s.suppname ASC");
            while (con._dr.Read())
            {

                var items = new ManageItem
                {
                    itemsId=Convert.ToInt32(con._dr["itemsid"].ToString()),
                    suppName = con._dr["suppname"].ToString(),
                    brandName = con._dr["brandname"].ToString(),
                    description = con._dr["description"].ToString(),
                    unitPrice = con._dr["unitprice"].ToString()
                };

                item.Add(items);
            }
            con._dr.Close();
            con.CloseConnection();
            var js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(item));
            con.CloseConnection();
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetItemById(int id)
        {
            con = new DBConnection();
            ManageItem itm = new ManageItem();
            con.OpenConection();
            con._dr = con.DataReader(@"SELECT s.suppname, s.suppcode, i.brandname, i.description, i.unitprice, i.itemsid 
			FROM items i INNER JOIN suppliers s ON s.suppcode=i.suppcode WHERE i.itemsid ='" + id + "'");
            while (con._dr.Read())
            {
                itm.itemsId = Convert.ToInt32(con._dr["itemsid"].ToString());
                itm.suppName = con._dr["suppname"].ToString();
                itm.brandName = con._dr["brandname"].ToString();
                itm.description = con._dr["description"].ToString();
                itm.unitPrice = con._dr["unitprice"].ToString();
            }
            con._dr.Close();
            con.CloseConnection();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(itm));
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int updateItemById(int iid, decimal iprice)
        {
            con = new DBConnection();
            int a = 0;
            con.OpenConection();
            con.ExecSqlQuery(@"UPDATE items SET unitprice = @iprice WHERE itemsid = @iid");
            con.Cmd.Parameters.AddWithValue("@iprice", Convert.ToDecimal(iprice));
            con.Cmd.Parameters.AddWithValue("@iid", Convert.ToInt32(iid));
            a = con.Cmd.ExecuteNonQuery();
            con.CloseConnection();

            return a;
        }
    }
}
