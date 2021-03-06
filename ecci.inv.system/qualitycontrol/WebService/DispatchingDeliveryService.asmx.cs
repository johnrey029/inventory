﻿using ecci.inv.system.qualitycontrol.CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Data;

namespace ecci.inv.system.qualitycontrol.WebService
{
    /// <summary>
    /// Summary description for DispatchingDeliveryService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
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
            @"SELECT s.purchaseorder,s.receivedquantity,s.purchasedate,s.receivedate,s.itemsid,
            s.stockid, s.postatus, i.brandname, u.suppname FROM stock_raw s
            INNER JOIN items i ON s.itemsid = i.itemsid
            INNER JOIN suppliers u ON i.suppcode = u.suppcode
            WHERE s.receivedate is not null and s.receivedquantity>0 and s.quantity>=0
            ORDER BY s.stockid ASC");
            while (con._dr.Read())
            {
                DateTime dt = DateTime.Parse(con._dr["purchasedate"].ToString());
                DateTime dt1 = DateTime.Parse(con._dr["receivedate"].ToString());
                var order = new DispatchDelivery
                {
                    stockId = Convert.ToInt32(con._dr["stockid"].ToString()),
                    purchaseOrder = con._dr["purchaseorder"].ToString(),
                    suppName = con._dr["suppname"].ToString(),
                    brandName = con._dr["brandname"].ToString(),
                    quantity = Convert.ToInt32(con._dr["receivedquantity"].ToString()),
                    purchaseDate = dt.ToShortDateString(),
                    receivedDate = dt1.ToShortDateString(),
                    poStatus = con._dr["postatus"].ToString(),
                    itemsid = Convert.ToInt32(con._dr["itemsid"].ToString())
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
            DispatchDelivery od = new DispatchDelivery();
            con.OpenConection();
            con._dr = con.DataReader(
            @"SELECT s.purchaseorder,s.receivedquantity,s.dispatchquantity,s.purchasedate,s.receivedate,
            s.stockid, s.itemsid,s.postatus, i.brandname, u.suppname FROM stock_raw s
            INNER JOIN items i ON s.itemsid = i.itemsid
            INNER JOIN suppliers u ON i.suppcode = u.suppcode
            WHERE s.stockid = '" + id + "';");
            while (con._dr.Read())
            {
                //var order = new OrderDelivery
                //{

                DateTime dt = DateTime.Parse(con._dr["purchasedate"].ToString());
                DateTime dt1 = DateTime.Parse(con._dr["receivedate"].ToString());
                od.stockId = Convert.ToInt32(con._dr["stockid"].ToString());
                od.purchaseOrder = con._dr["purchaseorder"].ToString();
                od.suppName = con._dr["suppname"].ToString();
                od.brandName = con._dr["brandname"].ToString();
                od.quantity = Convert.ToInt32(con._dr["receivedquantity"].ToString());
                od.purchaseDate = dt.ToShortDateString();
                od.receivedDate = dt1.ToShortDateString();
                od.poStatus = con._dr["postatus"].ToString();
                od.dispatch = Convert.ToInt32(con._dr["dispatchquantity"].ToString());
                od.itemsid = Convert.ToInt32(con._dr["itemsid"].ToString());
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
        public int UpdateDispatch(int upid,int dq)
        {
            con = new DBConnection();
            int rq = 0, oq = 0, a = 0;
            con.OpenConection();
            con.ExecSqlQuery("Select * from stock_raw where stockid = @sid");
            con.Cmd.Parameters.AddWithValue("@sid", upid);
            con._dr = con.Cmd.ExecuteReader();
            while (con._dr.Read())
            {
                rq = Convert.ToInt32(con._dr["receivedquantity"].ToString());
                oq = Convert.ToInt32(con._dr["quantity"].ToString());
            }
            con.CloseConnection();
            if (rq == 0 && oq == 0)
            {
                con.OpenConection();
                con.ExecSqlQuery("UPDATE stock_raw SET postatus = @stat,receivedquantity=@rq, dispatchquantity=@dq, receivedate = @rdate WHERE stockid = @sid");
                con.Cmd.Parameters.AddWithValue("@stat", "Dispatch");
                con.Cmd.Parameters.AddWithValue("@rq", 0);
                con.Cmd.Parameters.AddWithValue("@dq", dq);
                con.Cmd.Parameters.Add("@rdate", SqlDbType.Date).Value = DateTime.Now;
                con.Cmd.Parameters.AddWithValue("@sid", upid);
                a = con.Cmd.ExecuteNonQuery();
                con.CloseConnection();
            }
            else if (rq > 0 || oq > 0)
            {
                con.OpenConection();
                con.ExecSqlQuery("UPDATE stock_raw SET postatus = @stat,receivedquantity=@rq, dispatchquantity=@dq, receivedate = @rdate WHERE stockid = @sid");
                con.Cmd.Parameters.AddWithValue("@stat", "Partial Delivery");
                con.Cmd.Parameters.AddWithValue("@rq", 0);
                con.Cmd.Parameters.AddWithValue("@dq", dq);
                con.Cmd.Parameters.Add("@rdate", SqlDbType.Date).Value = DateTime.Now;
                con.Cmd.Parameters.AddWithValue("@sid", upid);
                a = con.Cmd.ExecuteNonQuery();
                con.CloseConnection();
            }
            return a;
        }
    }
}
