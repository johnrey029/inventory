using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Data;
using ecci.inv.system.production.CS;

namespace ecci.inv.system.production.WebService
{
    /// <summary>
    /// Summary description for ReservedRawMats
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ReservedRawMats : System.Web.Services.WebService
    {
        DBConnection con;
        [WebMethod(enableSession: true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetReservedRaw(string id)
        {
            int pos = 0;
            string pid = "";
            string oid = "";
            pos = id.IndexOf("c");
            pid = id.Substring(0, pos);
            oid = id.Substring(pos + 1, id.Length - (pos + 1));
            con = new DBConnection();
            var orders = new List<ProductRaw>();
            con.OpenConection();
            con._dr = con.DataReader(
            @"SELECT  i.quantityordered, u.itemsid,
            u.price,t.brandname FROM oderdetails i
            INNER JOIN productitems u ON i.productid = u.productid
            INNER JOIN items t ON u.itemsid = t.itemsid
            where i.productid = '" + Convert.ToInt32(pid) + "' and i.orderid ='" + Convert.ToInt64(oid) + "' ");
            while (con._dr.Read())
            {
                var order = new ProductRaw
                {
                    price = con._dr["price"].ToString(),
                    brand = con._dr["brandname"].ToString(),
                    qty = con._dr["quantityordered"].ToString(),
                    itemsid = con._dr["itemsid"].ToString()
                };
                orders.Add(order);
            }
            con._dr.Close();
            con.CloseConnection();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(orders));
        }
        [WebMethod(enableSession: true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetRawList(string id)
        {
            int pos = 0;
            string pid = "";
            string oid = "";
            pos = id.IndexOf("c");
            pid = id.Substring(0, pos);
            oid = id.Substring(pos + 1, id.Length - (pos + 1));
            con = new DBConnection();
            var orders = new List<RawProduct>();
            con.OpenConection();
            con._dr = con.DataReader(@"SELECT sw.purchaseorder, sw.quantity,
            sw.receivedate,t.brandname FROM store_warehouse sw
            INNER JOIN items t ON sw.itemsid = t.itemsid
            where sw.itemsid ='" + Convert.ToInt64(oid) + "' ");
            while (con._dr.Read())
            {
                DateTime dt1 = DateTime.Parse(con._dr["receivedate"].ToString());
                var order = new RawProduct
                {
                    po = con._dr["purchaseorder"].ToString(),
                    brand = con._dr["brandname"].ToString(),
                    qty = con._dr["quantity"].ToString(),
                    date = dt1.ToString()
                };
                orders.Add(order);
            }
            con._dr.Close();
            con.CloseConnection();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(orders));
            //@"SELECT  sw.purchaseorder, sw.quantity,
            //sw.receivedate,t.brandname FROM oderdetails i
            //INNER JOIN productitems u ON i.productid = u.productid
            //INNER JOIN items t ON u.itemsid = t.itemsid
            //INNER JOIN stock_warehouse sw ON u.itemsid = sw.itemsid
            //where i.productid = '" + Convert.ToInt32(pid) + "' and i.orderid = '" + Convert.ToInt64(oid) + "' ")
        }
    }
}
