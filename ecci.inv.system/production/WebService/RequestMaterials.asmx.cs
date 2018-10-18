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
    /// Summary description for RequestMaterials
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RequestMaterials : System.Web.Services.WebService
    {
        DBConnection con;
        [WebMethod(enableSession: true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetProductRaw()
        {
            con = new DBConnection();
            var orders = new List<RequestMaterial>();
            con.OpenConection();
            con._dr = con.DataReader(
            @"SELECT w.orderid,w.date,w.status,w.totalamount,
            c.name,u.firstname + ' ' + u.lastname AS empname FROM purchaseorder w
            INNER JOIN client c ON w.clientid = c.clientid
            INNER JOIN users u ON w.usersid = u.id
            ORDER BY w.orderid ASC");
            while (con._dr.Read())
            {
                DateTime dt1 = DateTime.Parse(con._dr["date"].ToString());
                var order = new RequestMaterial
                {
                    so = con._dr["orderid"].ToString(),
                    client = con._dr["name"].ToString(),
                    user = con._dr["empname"].ToString(),
                    amount = con._dr["totalamount"].ToString(),
                    date = dt1.ToShortDateString(),
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
