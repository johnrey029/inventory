
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

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ShowDeliveredById(int id)
        {
            con = new DBConnection();
            Hold od = new Hold();
            con.OpenConection();
            con._dr = con.DataReader(
            @"SELECT s.purchaseorder,s.quantity,s.date,
            s.id, s.status, i.brandname, u.suppname FROM stock_raw_fail s
            INNER JOIN stock_raw r ON s.purchaseorder = r.purchaseorder
            INNER JOIN items i ON r.itemsid = i.itemsid
            INNER JOIN suppliers u ON i.suppcode = u.suppcode
            WHERE s.id = '" + id + "';");
            while (con._dr.Read())
            {
                //var order = new OrderDelivery
                //{

                DateTime dt = DateTime.Parse(con._dr["date"].ToString());
                od.id = Convert.ToInt32(con._dr["id"].ToString());
                od.purchaseOrder = con._dr["purchaseorder"].ToString();
                od.suppName = con._dr["suppname"].ToString();
                od.brandName = con._dr["brandname"].ToString();
                od.quantity = Convert.ToInt32(con._dr["quantity"].ToString());
                od.holdDate = dt.ToShortDateString();
                //   deliverDate = dt1.ToShortDateString(),
                od.status = con._dr["status"].ToString();
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
        public int UpdateDispatch(int upid, int rework, int returns, int scrap)
        {
            con = new DBConnection();
            int rq = 0, oq = 0,sq = 0,quantity=0,dispatch=0,warehouse=0, a = 0;
            int po = 0;
            con.OpenConection();
            con.ExecSqlQuery("Select * from stock_raw_fail where id = @sid");
            con.Cmd.Parameters.AddWithValue("@sid", upid);
            con._dr = con.Cmd.ExecuteReader();
            while (con._dr.Read())
            {
                rq = Convert.ToInt32(con._dr["fixquantity"].ToString());
                oq = Convert.ToInt32(con._dr["quantity"].ToString());
                sq = Convert.ToInt32(con._dr["scrapquantity"].ToString());
                po = Convert.ToInt32(con._dr["purchaseorder"].ToString());
            }
            con.CloseConnection();
            con.OpenConection();
            con.ExecSqlQuery("Select * from stock_raw where purchaseorder = @sid");
            con.Cmd.Parameters.AddWithValue("@sid", po);
            con._dr = con.Cmd.ExecuteReader();
            while (con._dr.Read())
            {
                quantity = Convert.ToInt32(con._dr["quantity"].ToString());
                dispatch = Convert.ToInt32(con._dr["dispatchquantity"].ToString());

            }
            con.CloseConnection();
            con.OpenConection();
            con.ExecSqlQuery("Select * from stock_warehouse where purchaseorder = @sid");
            con.Cmd.Parameters.AddWithValue("@sid", po);
            con._dr = con.Cmd.ExecuteReader();
            while (con._dr.Read())
            {
                warehouse = Convert.ToInt32(con._dr["quantity"].ToString());
            }
            con.CloseConnection();
            int sum = rework + returns + scrap;
            int decrease = oq - sum;
            int receiving = returns + quantity;
            int dispatching = dispatch - returns;
            int stock = warehouse + rework;
            if (oq == sum)
            {
                if (returns > 0)
                {
                    con.OpenConection();
                    con.ExecSqlQuery("UPDATE stock_raw_fail SET status = @stat,quantity=@rq, fixquantity=@dq, date = @rdate WHERE id = @sid");
                    con.Cmd.Parameters.AddWithValue("@stat", "Evaluated");
                    con.Cmd.Parameters.AddWithValue("@rq", decrease);
                    con.Cmd.Parameters.AddWithValue("@dq", rework);
                    con.Cmd.Parameters.Add("@rdate", SqlDbType.Date).Value = DateTime.Now;
                    con.Cmd.Parameters.AddWithValue("@sid", upid);
                    a = con.Cmd.ExecuteNonQuery();
                    con.CloseConnection();

                    con.OpenConection();
                    con.ExecSqlQuery("UPDATE stock_warehouse SET quantity=@rq, receivedate = @rdate WHERE purchaseorder = @sid");
                    con.Cmd.Parameters.AddWithValue("@rq", stock);
                    con.Cmd.Parameters.Add("@rdate", SqlDbType.Date).Value = DateTime.Now;
                    con.Cmd.Parameters.AddWithValue("@sid", po);
                    a = con.Cmd.ExecuteNonQuery();
                    con.CloseConnection();
                }
                if(rework > 0)
                {
                    con.OpenConection();
                    con.ExecSqlQuery("UPDATE stock_raw SET postatus = @stat, quantity=@rq,dispatchquantity=@dq WHERE purchaseorder = @sid");
                    con.Cmd.Parameters.AddWithValue("@stat", "Partial Delivery");
                    con.Cmd.Parameters.AddWithValue("@rq", receiving);
                    con.Cmd.Parameters.AddWithValue("@dq", dispatching);
                    con.Cmd.Parameters.AddWithValue("@sid", po);
                    a = con.Cmd.ExecuteNonQuery();
                    con.CloseConnection();
                }
                if(scrap > 0)
                {
                    con.OpenConection();
                    con.ExecSqlQuery("UPDATE stock_raw_fail SET status = @stat, scrapquantity=@sq, date = @rdate WHERE id = @sid");
                    con.Cmd.Parameters.AddWithValue("@stat", "Evaluated");
                    con.Cmd.Parameters.AddWithValue("@sq", scrap);
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
