using ecci.inv.system.qualitycontrol.CS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;


namespace ecci.inv.system.qualitycontrol.WebService
{
    /// <summary>
    /// Summary description for PurchaseOrderService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class OrderDeliveryService : System.Web.Services.WebService
    {
        DBConnection con;
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetDeliveredOrder()
        {
            con = new DBConnection();
            var orders = new List<OrderDelivery>();
            con.OpenConection();
            con._dr = con.DataReader(
            @"SELECT s.purchaseorder,s.quantity,s.purchasedate,s.deliverydate,
            s.stockid, s.postatus, i.brandname, u.suppname FROM stock_raw s
            INNER JOIN items i ON s.itemsid = i.itemsid
            INNER JOIN suppliers u ON i.suppcode = u.suppcode
            WHERE s.quantity>0
            ORDER BY s.stockid ASC");
            while (con._dr.Read())
            {
                DateTime dt = DateTime.Parse(con._dr["purchasedate"].ToString());
                DateTime dt1 = DateTime.Parse(con._dr["deliverydate"].ToString());
                var order = new OrderDelivery
                {
                    stockId = Convert.ToInt32(con._dr["stockid"].ToString()),
                    purchaseOrder = con._dr["purchaseorder"].ToString(),
                    suppName = con._dr["suppname"].ToString(),
                    brandName = con._dr["brandname"].ToString(),
                    quantity = Convert.ToInt32(con._dr["quantity"].ToString()),
                    purchaseDate = dt.ToShortDateString(),
                    deliverDate = dt1.ToShortDateString(),
                    poStatus = con._dr["postatus"].ToString()
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
            OrderDelivery od = new OrderDelivery();
            con.OpenConection();
            con._dr = con.DataReader(
            @"SELECT s.purchaseorder,s.quantity,s.purchasedate,s.deliverydate,
            s.stockid, s.postatus, i.brandname, u.suppname FROM stock_raw s
            INNER JOIN items i ON s.itemsid = i.itemsid
            INNER JOIN suppliers u ON i.suppcode = u.suppcode
            WHERE s.stockid = '" + id + "';");
            while (con._dr.Read())
            {
                //var order = new OrderDelivery
                //{

                DateTime dt = DateTime.Parse(con._dr["purchasedate"].ToString());
                DateTime dt1 = DateTime.Parse(con._dr["deliverydate"].ToString());
                od.stockId = Convert.ToInt32(con._dr["stockid"].ToString());
                od.purchaseOrder = con._dr["purchaseorder"].ToString();
                od.suppName = con._dr["suppname"].ToString();
                od.brandName = con._dr["brandname"].ToString();
                od.quantity = Convert.ToInt32(con._dr["quantity"].ToString());
                od.purchaseDate = dt.ToShortDateString();
                od.deliverDate = dt1.ToShortDateString();
                od.poStatus = con._dr["postatus"].ToString();
                //};
                //orders.Add(order);
            }
            con._dr.Close();
            con.CloseConnection();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(od));
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int UpdateById(int upid,int total)
        {
            con = new DBConnection();
            int rq = 0, oq = 0, a = 0, po = 0,b =0,c=0;
            int decrease = 0, increase = 0;
            con.OpenConection();
            con.ExecSqlQuery("SELECT * FROM stock_raw WHERE stockid = @sid");
            con.Cmd.Parameters.AddWithValue("@sid", upid);
            con._dr = con.Cmd.ExecuteReader();
            while (con._dr.Read())
            {
                po = Convert.ToInt32(con._dr["purchaseorder"].ToString());
                rq = Convert.ToInt32(con._dr["receivedquantity"].ToString());
                oq = Convert.ToInt32(con._dr["quantity"].ToString());
            }
            con.CloseConnection();

            con.OpenConection();
            con.ExecSqlQuery("SELECT count(*) FROM stock_raw WHERE purchaseorder = @po and postatus != @stat and stockid = @sid");
            con.Cmd.Parameters.AddWithValue("@po", po);
            con.Cmd.Parameters.AddWithValue("@stat", "Returned");
            con.Cmd.Parameters.AddWithValue("@sid", upid);
            c = Convert.ToInt32(con.Cmd.ExecuteScalar());
            con.CloseConnection();
            if (c == 1)
            {
                decrease = oq - total;
                increase = rq + total;

                if (decrease > 0 && increase >= 0)
                {
                    con.OpenConection();
                    con.ExecSqlQuery("UPDATE stock_raw SET postatus = @stat,quantity=@quan, receivedquantity=@rq, receivedate = @rdate WHERE stockid = @sid");
                    con.Cmd.Parameters.AddWithValue("@stat", "Partial Delivery");
                    con.Cmd.Parameters.AddWithValue("@quan", decrease);
                    con.Cmd.Parameters.AddWithValue("@rq", increase);
                    con.Cmd.Parameters.AddWithValue("@sid", upid);
                    con.Cmd.Parameters.Add("@rdate", SqlDbType.Date).Value = DateTime.Now;
                    a = con.Cmd.ExecuteNonQuery();
                    con.CloseConnection();
                }
                else if (decrease == 0)
                {
                    con.OpenConection();
                    con.ExecSqlQuery("UPDATE stock_raw SET postatus = @stat,quantity=@quan, receivedquantity=@rq, receivedate = @rdate WHERE stockid = @sid");
                    con.Cmd.Parameters.AddWithValue("@stat", "Received");
                    con.Cmd.Parameters.AddWithValue("@quan", decrease);
                    con.Cmd.Parameters.AddWithValue("@rq", increase);
                    con.Cmd.Parameters.AddWithValue("@sid", upid);
                    con.Cmd.Parameters.Add("@rdate", SqlDbType.Date).Value = DateTime.Now;
                    a = con.Cmd.ExecuteNonQuery();
                    con.CloseConnection();
                }
            }


            con.OpenConection();
            con.ExecSqlQuery("SELECT count(*) FROM stock_raw WHERE purchaseorder = @po and postatus = @stat and stockid = @sid");
            con.Cmd.Parameters.AddWithValue("@po", po);
            con.Cmd.Parameters.AddWithValue("@stat", "Returned");
            con.Cmd.Parameters.AddWithValue("@sid", upid);
            b = Convert.ToInt32(con.Cmd.ExecuteScalar());
            con.CloseConnection();
            if (b == 1)
            {
                con.OpenConection();
                con.ExecSqlQuery("SELECT * FROM stock_raw WHERE purchaseorder = @po and postatus !=@stat");
                con.Cmd.Parameters.AddWithValue("@po", po);
                con.Cmd.Parameters.AddWithValue("@stat", "Returned");
                con._dr = con.Cmd.ExecuteReader();
                while (con._dr.Read())
                {
                    rq = Convert.ToInt32(con._dr["receivedquantity"].ToString());
                }
                con.CloseConnection();
                decrease = oq - total;
                increase = rq + total;
                if (increase >= 0)
                {
                    con.OpenConection();
                    con.ExecSqlQuery("UPDATE stock_raw SET postatus = @stat, receivedquantity=@rq, receivedate = @rdate WHERE purchaseorder = @sid and postatus !=@stt");
                    con.Cmd.Parameters.AddWithValue("@stat", "Partial Delivery");
                    con.Cmd.Parameters.AddWithValue("@rq", increase);
                    con.Cmd.Parameters.Add("@rdate", SqlDbType.Date).Value = DateTime.Now;
                    con.Cmd.Parameters.AddWithValue("@sid", po);
                    con.Cmd.Parameters.AddWithValue("@stt", "Returned");
                    a = con.Cmd.ExecuteNonQuery();
                    con.CloseConnection();
                }
                if (decrease >= 0)
                {
                    con.OpenConection();
                    con.ExecSqlQuery("UPDATE stock_raw SET quantity=@quan, receivedate = @rdate WHERE stockid = @sid");
                    con.Cmd.Parameters.AddWithValue("@quan", decrease);
                    con.Cmd.Parameters.Add("@rdate", SqlDbType.Date).Value = DateTime.Now;
                    con.Cmd.Parameters.AddWithValue("@sid", upid);
                    a = con.Cmd.ExecuteNonQuery();
                    con.CloseConnection();
                }
            }
            return a;
        }
    }
}
