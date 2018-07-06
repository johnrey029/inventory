using ecci.inv.system.qualitycontrol.CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace ecci.inv.system.qualitycontrol.WebService
{
    /// <summary>
    /// Summary description for DispatchingDeliveryService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DispatchingDeliveryService : System.Web.Services.WebService
    {
        DBConnection con;
        [WebMethod(enableSession:true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DispatchDelivery()
        {
            con = new DBConnection();
            var orders = new List<DispatchDelivery>();
            con.OpenConection();
            con._dr = con.DataReader(
            @"SELECT s.purchaseorder,s.quantity,s.purchasedate,s.deliverydate,
            s.stockid, s.postatus, i.brandname, u.suppname FROM stock s
            INNER JOIN items i ON s.itemsid = i.itemsid
            INNER JOIN suppliers u ON i.suppcode = u.suppcode
            WHERE s.postatus='Received'
            ORDER BY s.stockid ASC");
            while (con._dr.Read())
            {
                var order = new DispatchDelivery
                {
                    stockId = Convert.ToInt32(con._dr["stockid"].ToString()),
                    purchaseOrder = con._dr["purchaseorder"].ToString(),
                    suppName = con._dr["suppname"].ToString(),
                    brandName = con._dr["brandname"].ToString(),
                    quantity = Convert.ToInt32(con._dr["quantity"].ToString()),
                    purchaseDate = con._dr["purchasedate"].ToString(),
                    deliverDate = con._dr["deliverydate"].ToString(),
                    poStatus = con._dr["postatus"].ToString()
                };
                orders.Add(order);
            }
            con._dr.Close();
            con.CloseConnection();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(orders));
        }

    }
}
