
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
                var order = new Hold
                {
                    id = Convert.ToInt32(con._dr["id"].ToString()),
                    purchaseOrder = con._dr["purchaseorder"].ToString(),
                    suppName = con._dr["suppname"].ToString(),
                    brandName = con._dr["brandname"].ToString(),
                    quantity = Convert.ToInt32(con._dr["quantity"].ToString()),
                    holdDate = dt.ToShortDateString(),
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
                DateTime dt = DateTime.Parse(con._dr["date"].ToString());
                od.id = Convert.ToInt32(con._dr["id"].ToString());
                od.purchaseOrder = con._dr["purchaseorder"].ToString();
                od.suppName = con._dr["suppname"].ToString();
                od.brandName = con._dr["brandname"].ToString();
                od.quantity = Convert.ToInt32(con._dr["quantity"].ToString());
                od.holdDate = dt.ToShortDateString();
                od.status = con._dr["status"].ToString();
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
            int rq = 0, oq = 0,sq = 0,warehouse=0, a = 0, po = 0,fq=0;
            con.OpenConection();
            con.ExecSqlQuery("Select * from stock_raw_fail where id = @sid");
            con.Cmd.Parameters.AddWithValue("@sid", upid);
            con._dr = con.Cmd.ExecuteReader();
            while (con._dr.Read())
            {
                fq = Convert.ToInt32(con._dr["fixquantity"].ToString());
                oq = Convert.ToInt32(con._dr["quantity"].ToString());
                sq = Convert.ToInt32(con._dr["scrapquantity"].ToString());
                rq = Convert.ToInt32(con._dr["returnquantity"].ToString());
                po = Convert.ToInt32(con._dr["purchaseorder"].ToString());
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
            int stock = warehouse + rework;
            if (oq == sum)
            {
                if (returns > 0)
                {
                    con.OpenConection();
                    con.ExecSqlQuery("UPDATE stock_raw_fail SET status = @stat,quantity=@q, fixquantity=@fq, scrapquantity=@sq,returnquantity=@rq, date = @rdate WHERE id = @sid and status = @stats");
                    con.Cmd.Parameters.AddWithValue("@stat", "Evaluated");
                    con.Cmd.Parameters.AddWithValue("@q", decrease);
                    con.Cmd.Parameters.AddWithValue("@fq", rework+fq);
                    con.Cmd.Parameters.AddWithValue("@sq", scrap+sq);
                    con.Cmd.Parameters.AddWithValue("@rq", returns+rq);
                    con.Cmd.Parameters.Add("@rdate", SqlDbType.Date).Value = DateTime.Now;
                    con.Cmd.Parameters.AddWithValue("@sid", upid);
                    con.Cmd.Parameters.AddWithValue("@stat", "Hold");
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
                int quantity = 0, dispatch = 0,itemsid = 0, received = 0;
                int dispatching = 0, total = 0, unitprice = 0;
                double price = 0.00, pminus = 0.00, padd = 0.00;
                string pdate = string.Empty, units = string.Empty, assumedate = string.Empty;
                if (rework > 0)
                {
                    con.OpenConection();
                    con.ExecSqlQuery("SELECT * FROM stock_raw where purchaseorder = @sid and postatus != @rstat");
                    con.Cmd.Parameters.AddWithValue("@sid", po);
                    // con.Cmd.Parameters.AddWithValue("@rdate", string.Empty); s.receivedate != @rdate and
                    //s.purchasedate,s.itemsid,s.quantity,s.receivedquantity,s.dispatchquantity,s.unit, i.unitprice 
                    con.Cmd.Parameters.AddWithValue("@rstat", "Returned");
                    con._dr = con.Cmd.ExecuteReader();
                    if (con._dr.Read())
                    {
                        itemsid = Convert.ToInt32(con._dr["itemsid"].ToString());
                        pdate = con._dr["purchasedate"].ToString();
                        units = con._dr["unit"].ToString();
                        quantity = Convert.ToInt32(con._dr["quantity"].ToString());
                        dispatch = Convert.ToInt32(con._dr["dispatchquantity"].ToString());
                        received = Convert.ToInt32(con._dr["receivedquantity"].ToString());
                        price = Convert.ToDouble(con._dr["price"].ToString());
                    }
                    con.CloseConnection();

                    dispatching = dispatch - returns;
                    total = dispatch + quantity + received;
                    unitprice = Convert.ToInt32(price)/total;
                    pminus = Convert.ToDouble(unitprice * returns);
                    padd = Convert.ToDouble(unitprice * (total - returns));
                    assumedate = DateTime.Now.AddMonths(1).ToShortDateString();
                    con.OpenConection();
                    con.ExecSqlQuery("UPDATE stock_raw SET dispatchquantity=@dq, price = @p,returndate=@rdate WHERE purchaseorder = @sid and postatus != @stat");
                    con.Cmd.Parameters.AddWithValue("@dq", dispatching);
                    con.Cmd.Parameters.AddWithValue("@p", padd);
                    con.Cmd.Parameters.AddWithValue("@rdate", SqlDbType.Date).Value = DateTime.Now;
                    con.Cmd.Parameters.AddWithValue("@sid", po);
                    con.Cmd.Parameters.AddWithValue("@stat", "Returned");
                    a = con.Cmd.ExecuteNonQuery();
                    con.CloseConnection();

                    con.OpenConection();
                    con.ExecSqlQuery("Select count(*) from stock_raw where purchaseorder = @sid and postatus = @stat and receivedate=@rdate");
                    con.Cmd.Parameters.AddWithValue("@sid", po);
                    con.Cmd.Parameters.AddWithValue("@stat", "Returned");
                    con.Cmd.Parameters.AddWithValue("@rdate", string.Empty);
                    int count = Convert.ToInt32(con.Cmd.ExecuteScalar());
                    con.CloseConnection();
                    if (count == 1)
                    {
                        int quan = 0;
                        con.OpenConection();
                        con.ExecSqlQuery("Select * from stock_raw where purchaseorder = @sid and postatus = @stat");
                        con.Cmd.Parameters.AddWithValue("@sid", po);
                        con.Cmd.Parameters.AddWithValue("@stat", "Returned");
                        con._dr = con.Cmd.ExecuteReader();
                        while (con._dr.Read())
                        {
                            quan = Convert.ToInt32(con._dr["quantity"].ToString());
                        }
                        int upquan = quan + returns;
                        int dispatching2 = dispatch - upquan;
                        double padd2 = price * (total - upquan);
                        con.OpenConection();
                        con.ExecSqlQuery("UPDATE stock_raw SET quantity=@rq,dispatchquantity=@dq,price = @p,deliverydate=@ddate WHERE purchaseorder = @sid and postatus =@stat");
                        con.Cmd.Parameters.AddWithValue("@dq", dispatching2);
                        con.Cmd.Parameters.AddWithValue("@p", padd2);
                        con.Cmd.Parameters.AddWithValue("@ddate", DateTime.Parse(assumedate));
                        con.Cmd.Parameters.AddWithValue("@sid", po);
                        con.Cmd.Parameters.AddWithValue("@stat", "Returned");
                        a = con.Cmd.ExecuteNonQuery();
                        con.CloseConnection();
                    }
                    else
                    {
                        con.OpenConection();
                        con.ExecSqlQuery("INSERT INTO stock_raw(purchaseorder, itemsid, quantity, receivedquantity, dispatchquantity, purchasedate, deliverydate, postatus, price, unit)VALUES(@po, @item, @quan, @rq,@dq, @pdate, @ddate, @stat, @price, @unit)");
                        con.Cmd.Parameters.Add("@po", SqlDbType.NVarChar).Value = po;
                        con.Cmd.Parameters.Add("@item", SqlDbType.Int).Value = itemsid;
                        con.Cmd.Parameters.Add("@quan", SqlDbType.Int).Value = returns;
                        con.Cmd.Parameters.Add("@rq", SqlDbType.Int).Value = 0;
                        con.Cmd.Parameters.Add("@dq", SqlDbType.Int).Value = 0;
                        con.Cmd.Parameters.AddWithValue("@pdate", pdate);
                        con.Cmd.Parameters.AddWithValue("@ddate", DateTime.Parse(assumedate));
                        con.Cmd.Parameters.Add("@stat", SqlDbType.Char).Value = "Returned";
                        con.Cmd.Parameters.Add("@price", SqlDbType.Money).Value = pminus;
                        con.Cmd.Parameters.Add("@unit", SqlDbType.VarChar).Value = units;
                        a = con.Cmd.ExecuteNonQuery();
                        con.CloseConnection();
                    }
                }
            }
            return a;
        }
    }
}
