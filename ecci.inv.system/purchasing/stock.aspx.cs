﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ecci.inv.system.purchasing
{
    public partial class stock : System.Web.UI.Page
    {
        private string sessionempno { get; set; }
        DBConnection con;
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            con = new DBConnection();
            if (Session["empnumber"] != null)
            {
                sessionempno = Session["empnumber"].ToString();
                if (!IsPostBack)
                {
                    //tbCalendar.Visible = false;
                    ddBrand.Items.Insert(0, new ListItem("Select Brand", "-1"));
                    dropdown();
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
                    ddBrand.Enabled = false;
                    tbDescription.Enabled = false;
                }
                tbPO.Text = DateTime.Now.ToString("Myyssff");
            }
            

        }
      
        private void dropdown()
        {
            try
            {
                con.OpenConection();
                con.ExecSqlQuery("Select * from suppliers");
                ddSupplier.DataTextField = "suppname";
                ddSupplier.DataValueField = "suppcode";
                ddSupplier.DataSource = con.DataQueryExec();
                ddSupplier.DataBind();
                ddSupplier.Items.Insert(0, new ListItem("Select Supplier", "-1"));
                con.CloseConnection();
            }
            catch//(Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
            }
        }
        private void clear()
        {
            tbPO.Text = ""; ;
            tbDescription.Text = "";
            ddSupplier.SelectedIndex = -1;
            tbPO.Text = "";
            ddBrand.SelectedIndex = -1;
            tbQuantity.Text = "";
            ddBrand.Enabled = false;
            //tbCalendar.Visible = false;
            tbEdate.Text = "";
        }

        protected void ddSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddSupplier.SelectedIndex == 0)
            {
                ddBrand.SelectedIndex = 0;
                ddBrand.Enabled = false;
                ddSupplier.SelectedIndex = 0;
                tbDescription.Text = "";
                tbDescription.Enabled = false;
            }
            else
            {
                ddBrand.Enabled = true;
                try
                {
                    con.OpenConection();
                    con.ExecSqlQuery("Select * from items where suppcode='" + ddSupplier.SelectedValue + "'");
                    ddBrand.DataTextField = "brandname";
                    ddBrand.DataValueField = "itemsid";
                    ddBrand.DataSource = con.DataQueryExec();
                    ddBrand.DataBind();
                    ddBrand.Items.Insert(0, new ListItem("Select Brand", "-1"));
                    con.CloseConnection();
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
                }
            }
        }

        protected void ddBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
            "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
            if (ddBrand.SelectedIndex == 0)
            {
                tbDescription.Text = "";
                tbDescription.Enabled = false;
            }
            else
            {
                tbDescription.Enabled = true;
                try
                {
                    con.OpenConection();
                    con._dr = con.DataReader("Select * from items where itemsid='" + ddBrand.SelectedValue + "'");
                    while(con._dr.Read())
                    {
                        tbDescription.Text = con._dr["description"].ToString();
                    }
                    con.CloseConnection();
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool tama = load();
            bool mali = activity();
            if(tama == true && mali == true)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-error').hide(); $('.alert-success').show(); });</script>");
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide(); $('.alert-error').show(); });</script>");
            }
            //clear();
        }
        private Boolean load()
        {
            //string date = DateTime.Now.ToString("MMMM dd yyyy");
            string date = DateTime.Now.ToShortDateString();
            bool check = false;
            lbError.Visible = false;
            try
            {
                con.OpenConection();
                con.ExecSqlQuery("insert into stock_raw(purchaseorder, itemsid, quantity, purchasedate, deliverydate, postatus, price)values(@po, @item, @quan, @pdate, @ddate, @stat, @price)");
                con.Cmd.Parameters.AddWithValue("@po", tbPO.Text);
                con.Cmd.Parameters.AddWithValue("@item", ddBrand.SelectedValue);
                con.Cmd.Parameters.AddWithValue("@quan", tbQuantity.Text);
                con.Cmd.Parameters.AddWithValue("@pdate", DateTime.Parse(date));
                con.Cmd.Parameters.AddWithValue("@ddate", DateTime.Parse(tbEdate.Text));
                con.Cmd.Parameters.AddWithValue("@stat", "For delivery");
                con.Cmd.Parameters.AddWithValue("@price", tbPrice.Text);
                int a = con.Cmd.ExecuteNonQuery();
                con.CloseConnection();
                if (a == 0)
                {
                    check = false;
                }
                else
                {
                    check = true;
                }
            }
            catch (Exception ex)
            {
                check = false;
                lbError.ForeColor = System.Drawing.Color.Red;
                lbError.Text = "Error: " + ex.Message;
                lbError.Visible = true;
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide(); $('.alert-error').show(); });</script>");
            }
            return check;
        }
        private Boolean activity()
        {
            //string date = DateTime.Now.ToString("MMMM dd, yyyy hh:mm tt");
            string date = DateTime.Now.ToShortDateString();
            string time = DateTime.Now.ToLongTimeString();
            bool check = false;
            lbError.Visible = false;
            try
            {
                //itemsid, quantity, purchasedate, deliverydate, postatus,@item, @quan, @pdate, @ddate, @stat,
                con.OpenConection();
                con.ExecSqlQuery("INSERT INTO activity_stock_raw(purchaseorder,empno,activity,date,time)VALUES(@po,@en,@act,@ddate,@t)");
                con.Cmd.Parameters.AddWithValue("@po", tbPO.Text);
                //con.Cmd.Parameters.AddWithValue("@item", ddBrand.SelectedValue);
                //con.Cmd.Parameters.AddWithValue("@quan", tbQuantity.Text);
                //con.Cmd.Parameters.AddWithValue("@pdate", date);
                con.Cmd.Parameters.AddWithValue("@en", sessionempno);
                con.Cmd.Parameters.AddWithValue("@act", "For Delivery");
                con.Cmd.Parameters.AddWithValue("@ddate", DateTime.Parse(date));
                //con.Cmd.Parameters.AddWithValue("@stat", "For delivery");
                con.Cmd.Parameters.AddWithValue("@t", DateTime.Parse(time));
                int a = con.Cmd.ExecuteNonQuery();
                con.CloseConnection();
                if (a == 0)
                {
                    check = false;
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    //"<script>$(document).ready(function(){ $('.alert-success').hide(); $('.alert-error').show(); });</script>");
                }
                else
                {
                    check = true;
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    //"<script>$(document).ready(function(){ $('.alert-error').hide(); $('.alert-success').show(); });</script>");
                }
            }
            catch (Exception ex)
            {
                lbError.ForeColor = System.Drawing.Color.Red;
                lbError.Text = "Error: " + ex.Message;
                lbError.Visible = true;
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide(); $('.alert-error').show(); });</script>");
            }
            return check;
        }
    }
}