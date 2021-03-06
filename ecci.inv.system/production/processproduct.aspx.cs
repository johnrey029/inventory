﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecci.inv.system.production
{
    public partial class processproduct : System.Web.UI.Page
    {
        private string sessionempno { get; set; }
        DBConnection con;
        private bool contin = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["empnumber"] != null)
            {
                sessionempno = Session["empnumber"].ToString();
            }
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            con = new DBConnection();
            if (Request.Cookies["CookieName"] != null)
            {
                Label1.Text = Server.HtmlEncode(Request.Cookies["CookieName"].Value);
            }
            if (!IsPostBack)
            {
                BindGridView();
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
               "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); $('.alert-warning').hide();});</script>");
            }
        }
        private void BindGridView()
        {
            string id = Label1.Text;
            Label1.Visible = false;
            Label3.Visible = false;
            int pos = 0;
            string pid = "";
            string oid = "";
            pos = id.IndexOf("c");
            pid = id.Substring(0, pos);
            oid = id.Substring(pos + 1, id.Length - (pos + 1));
            con.OpenConection();
            con.ExecSqlQuery(
            @"SELECT  i.quantityordered * u.quantity as required,
                u.itemsid,Convert(VARCHAR(19),i.quantityordered * u.quantity) + 'c' + Convert(VARCHAR(50),u.itemsid) AS uniqueid,
                u.price,t.brandname, i.quantityordered FROM oderdetails i
                INNER JOIN productitems u ON i.productid = u.productid
                INNER JOIN items t ON u.itemsid = t.itemsid
                where i.productid = '" + Convert.ToInt32(pid) + "' and i.orderid ='" + Convert.ToInt64(oid) + "' ");
            GridView1.DataSource = con.DataQueryExec();
            GridView1.DataBind();
            con.CloseConnection();
        }
        public string MyNewRow(object unqid)
        {
            return String.Format(@"</td></tr><tr id='tr{0}' class='collapsed-row'>
            <td>LIST OF AVAILABLE PO #</td> <td colspan='100' style='padding:0px; margin:0px;'>", unqid);
            //<button class='btn btn-primary glyphicon-envelope' style='height: 50px; width: 50px;'></button>
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int total = 0;
            int sum = 0;
            int count = 1;
            int number = 0;
            int pos = 0;
            string qty = "";
            string itemsid = "";
            int read = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string requestedid = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();

                if (requestedid.Length > 0)
                {
                    GridView gv = (GridView)e.Row.FindControl("GridView2");
                    pos = requestedid.IndexOf("c");
                    qty = requestedid.Substring(0, pos);
                    itemsid = requestedid.Substring(pos + 1, requestedid.Length - (pos + 1));
                    do
                    {

                        con.OpenConection();
                        con._dr = con.DataReader("SELECT TOP " + count.ToString() + " " +
                        "sw.purchaseorder, sw.quantity," +
                        "sw.receivedate,t.brandname FROM stock_warehouse sw " +
                        "INNER JOIN items t ON sw.itemsid = t.itemsid " +
                        "where sw.itemsid ='" + Convert.ToInt64(itemsid) + "' " +
                        "order by sw.receivedate ASC");
                        //read = Convert.ToInt32(con._dr.Read());
                        while (con._dr.Read()) 
                        {
                            total += Convert.ToInt32(con._dr["quantity"].ToString());
                            read++;
                        }
                        con._dr.Close();
                        con.CloseConnection();
                        if (total < Convert.ToInt32(qty))
                        {
                            if (count == read)
                            {
                                contin = false;
                               // btnRequest.Enabled = true;
                            }
                            else
                            {
                                e.Row.BackColor = System.Drawing.Color.Orange;
                                contin = true;
                                number = read;
                                sum = total;
                                btnRequest.Enabled = false;
                            }
                            count++;
                        }
                        else
                        {
                            e.Row.BackColor = System.Drawing.Color.White;
                            contin = true;
                            number = read;
                            sum = total;
                        }
                        read = 0;
                        total = 0;
                    } while (contin == false);
                    con.OpenConection();
                    con.ExecSqlQuery("SELECT TOP " + number.ToString() + " " +
                    "sw.purchaseorder, sw.quantity," +
                    "sw.receivedate,t.brandname FROM stock_warehouse sw " +
                    "INNER JOIN items t ON sw.itemsid = t.itemsid " +
                    "where sw.itemsid ='" + Convert.ToInt64(itemsid) + "' and sw.status='" + "Reserved" + "' " +
                    "order by sw.receivedate ASC");
                    gv.DataSource = con.DataQueryExec();
                    gv.DataBind();
                    con.CloseConnection();
                    Label lb = new Label();
                    lb.Text = "Total Amount";
                    Label lb1 = new Label();
                    lb1.Text = sum + "/" + qty;
                    if (gv.Rows.Count > 0)
                    {
                        gv.FooterRow.Cells[1].Controls.Add(lb);
                        gv.FooterRow.Cells[2].Controls.Add(lb1);
                    }
                    else
                    {
                        Label lab = (Label)e.Row.FindControl("Label2");
                        lab.Text = "No Raw Materials Available";
                        lab.Visible = true;
                        btnRequest.Enabled = false;
                    }
                    count = 1;
                    sum = 0;
                }
            }
        }

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            int qty = 0;
            int sum = 0;
            int com = 0;
            string ponumber = "";
            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                qty = Convert.ToInt32(gvrow.Cells[2].Text);
                GridView gvInner = gvrow.FindControl("GridView2") as GridView;
                foreach (GridViewRow row in gvInner.Rows)
                {
                    ponumber = row.Cells[0].Text;
                    com = Convert.ToInt32(row.Cells[2].Text);
                    sum += Convert.ToInt32(row.Cells[2].Text);
                    if(qty > sum)
                    {
                        insertupdate(com, 0, ponumber,qty);
                    }
                    else
                    {
                        int diff = sum - com;
                        int notdiff = qty - diff;
                        int finaldiff = com - notdiff;
                        insertupdate(notdiff, finaldiff, ponumber,qty);
                    }
                }
                sum = 0;
            }

            Session["saved"] = "ok";
            Response.Redirect("/production/approvedrequest.aspx");
        }
        private void insertupdate(int com,int update, string ponumber,int qty)
        {
            string status = "";
            string id = Label1.Text;
            int pos = 0;
            string pid = "";
            string oid = "";
            pos = id.IndexOf("c");
            pid = id.Substring(0, pos);
            oid = id.Substring(pos + 1, id.Length - (pos + 1));
            try
            {
                if(com<qty)
                {
                    status = "In Stock";
                }
                else
                {
                    status = "Used";
                }
                con.OpenConection();
                con.ExecSqlQuery("update stock_warehouse set quantity = @qty, status = @fstat  where purchaseorder = @sid and status = @stat");
                con.Cmd.Parameters.AddWithValue("@qty", update);
                con.Cmd.Parameters.AddWithValue("@fstat", status);
                // con.Cmd.Parameters.AddWithValue("@uqty", com);usedqty =@uqty
                con.Cmd.Parameters.AddWithValue("@stat", "Reserved");
                con.Cmd.Parameters.AddWithValue("@sid", ponumber);
                con.Cmd.ExecuteNonQuery();
                con.CloseConnection();

                con.OpenConection();
                con.ExecSqlQuery("Insert into requestitems(quantity,po,productid,orderid)values(@qty,@sid,@pid,@oid)");
                con.Cmd.Parameters.AddWithValue("@qty", com);
                con.Cmd.Parameters.AddWithValue("@sid", ponumber);
                con.Cmd.Parameters.AddWithValue("@pid", pid);
                con.Cmd.Parameters.AddWithValue("@oid", oid);
                con.Cmd.ExecuteNonQuery();
                con.CloseConnection();

                con.OpenConection();
                con.ExecSqlQuery("UPDATE oderdetails SET status = @stat WHERE orderid=@oid and productid=@pid");
                con.Cmd.Parameters.AddWithValue("@stat", "In Production");
                con.Cmd.Parameters.AddWithValue("@oid", oid);
                con.Cmd.Parameters.AddWithValue("@pid", pid);
                con.Cmd.ExecuteNonQuery();
                con.CloseConnection();

                con.OpenConection();
                con.ExecSqlQuery("UPDATE purchaseorder SET status = @stat WHERE orderid=@oid");
                con.Cmd.Parameters.AddWithValue("@stat", "Processing");
                con.Cmd.Parameters.AddWithValue("@oid", oid);
                con.Cmd.ExecuteNonQuery();
                con.CloseConnection();

            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
    }
}