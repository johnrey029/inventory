using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using ecci.inv.system.sales.CS;

namespace ecci.inv.system.sales.WebService
{
    /// <summary>
    /// Summary description for ProductOrderService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ProductOrderService : System.Web.Services.WebService
    {

        DBConnection con;
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public void GetPurchaseOrder()
        {
            con = new DBConnection();
            var orders = new List<OrderDelivery>();
            con.OpenConection();
            con._dr = con.DataReader(
            @"SELECT s.status,s.date,c.name, s.orderid,
            i.price,i.quantityordered, u.pname FROM purchaseorder s
            INNER JOIN oderdetails i ON s.orderid = i.orderid
            INNER JOIN client c ON s.clientid = c.clientid
            INNER JOIN product u ON i.productid = u.productid
            ORDER BY s.orderid ASC");
            while (con._dr.Read())
            {
                DateTime dt = DateTime.Parse(con._dr["date"].ToString());
                // DateTime dt1 = DateTime.Parse(con._dr["deliverydate"].ToString());
                var order = new OrderDelivery
                {
                    orderid = Convert.ToInt32(con._dr["orderid"].ToString()),
                    client = con._dr["name"].ToString(),
                    product = con._dr["pname"].ToString(),
                    price = con._dr["price"].ToString(),
                    quantity = Convert.ToInt32(con._dr["quantityordered"].ToString()),
                    date = dt.ToShortDateString(),
                    amount = (Convert.ToDecimal(con._dr["quantityordered"]) * Convert.ToDecimal(con._dr["price"])).ToString(),
                    status = con._dr["status"].ToString()
                };
                orders.Add(order);
            }
            con._dr.Close();
            con.CloseConnection();
            var js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(orders));
        }
    }
}
