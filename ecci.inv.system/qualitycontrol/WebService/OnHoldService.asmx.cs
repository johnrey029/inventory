
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using ecci.inv.system.qualitycontrol.CS;

namespace ecci.inv.system.qualitycontrol.WebService
{
    /// <summary>
    /// Summary description for OnHoldService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class OnHoldService : System.Web.Services.WebService
    {
        DBConnection con;
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetOnHoldItems()
        {
            con = new DBConnection();
            var orders = new List<Hold>();
            con.OpenConection();
            con._dr = con.DataReader(
            @"SELECT s.purchaseorder,s.quantity,s.date,
            s.id, s.status, i.brandname, u.suppname FROM stock_raw_fail s
            INNER JOIN stock_raw r ON s.purchaseorder = r.purchaseorder
            INNER JOIN items i ON r.itemsid = i.itemsid
            INNER JOIN suppliers u ON i.suppcode = u.suppcode
            WHERE s.quantity>0
            ORDER BY s.id ASC");
            while (con._dr.Read())
            {
                DateTime dt = DateTime.Parse(con._dr["date"].ToString());
               // DateTime dt1 = DateTime.Parse(con._dr["deliverydate"].ToString());
                var order = new Hold
                {
                    id = Convert.ToInt32(con._dr["id"].ToString()),
                    purchaseOrder = con._dr["purchaseorder"].ToString(),
                    suppName = con._dr["suppname"].ToString(),
                    brandName = con._dr["brandname"].ToString(),
                    quantity = Convert.ToInt32(con._dr["quantity"].ToString()),
                    holdDate = dt.ToShortDateString(),
                 //   deliverDate = dt1.ToShortDateString(),
                    status = con._dr["status"].ToString()
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
