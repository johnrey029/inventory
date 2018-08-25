using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Data;
using ecci.inv.system.admin.CS;


namespace ecci.inv.system.admin.WebService
{
    /// <summary>
    /// Summary description for AdminRaw
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AdminRaw : System.Web.Services.WebService
    {
        DBConnection con;
        [WebMethod(enableSession: true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetRawMaterials()
        {
            con = new DBConnection();
            var orders = new List<RawMaterials>();
            con.OpenConection();
            con._dr = con.DataReader(
            @"SELECT w.purchaseorder,w.quantity,w.receivedate,w.id, w.status, 
            i.brandname,u.suppname FROM stock_warehouse w
            INNER JOIN stock_raw s ON w.purchaseorder = s.purchaseorder
            INNER JOIN items i ON s.itemsid = i.itemsid
            INNER JOIN suppliers u ON i.suppcode = u.suppcode  
            WHERE w.receivedate is not null and w.status='In Stock' and w.quantity>=0
            ORDER BY w.id ASC");
            while (con._dr.Read())
            {
                //
                //DateTime dt = DateTime.Parse(con._dr["purchasedate"].ToString());
                DateTime dt1 = DateTime.Parse(con._dr["receivedate"].ToString());
                var order = new RawMaterials
                {
                    id = Convert.ToInt32(con._dr["id"].ToString()),
                    purchaseOrder = con._dr["purchaseorder"].ToString(),
                    suppName = con._dr["suppname"].ToString(),
                    brandName = con._dr["brandname"].ToString(),
                    quantity = Convert.ToInt32(con._dr["quantity"].ToString()),
                    // purchaseDate = dt.ToShortDateString(),
                    receivedDate = dt1.ToShortDateString(),
                    status = con._dr["status"].ToString()
                };
                orders.Add(order);
            }
            con._dr.Close();
            con.CloseConnection();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(orders));
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ShowDeliveredById(int id)
        {
            con = new DBConnection();
            RawMaterials od = new RawMaterials();
            con.OpenConection();
            con._dr = con.DataReader(
            @"SELECT w.purchaseorder,w.quantity,w.receivedate,w.id, w.status, 
            i.brandname,u.suppname FROM stock_warehouse w
            INNER JOIN stock_raw s ON w.purchaseorder = s.purchaseorder
            INNER JOIN items i ON s.itemsid = i.itemsid
            INNER JOIN suppliers u ON i.suppcode = u.suppcode  
            WHERE w.id = '" + id + "';");
            while (con._dr.Read())
            {
                DateTime dt1 = DateTime.Parse(con._dr["receivedate"].ToString());
                od.id = Convert.ToInt32(con._dr["id"].ToString());
                od.purchaseOrder = con._dr["purchaseorder"].ToString();
                od.suppName = con._dr["suppname"].ToString();
                od.brandName = con._dr["brandname"].ToString();
                od.receivedDate = dt1.ToShortDateString();
                od.quantity = Convert.ToInt32(con._dr["quantity"].ToString());
                od.status = con._dr["status"].ToString();
            }
            con._dr.Close();
            con.CloseConnection();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(od));
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int UpdateDispatch(int upid)
        {
            int a = 0;
            con = new DBConnection();
            con.OpenConection();
            con.ExecSqlQuery("UPDATE stock_warehouse SET code = @c WHERE id = @sid");
            con.Cmd.Parameters.AddWithValue("@c", 2);
            con.Cmd.Parameters.AddWithValue("@sid", upid);
            a = con.Cmd.ExecuteNonQuery();
            con.CloseConnection();
            return a;
        }
    }
}
