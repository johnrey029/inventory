﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecci.inv.system.qualitycontrol
{
    public partial class dispatching : System.Web.UI.Page
    {
        private string sessionempno { get; set; }
        DBConnection con;
        string date = DateTime.Now.ToShortDateString();
        string time = DateTime.Now.ToLongTimeString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["empnumber"] != null)
            {
                sessionempno = Session["empnumber"].ToString();

            }
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            con = new DBConnection();
            if (!IsPostBack)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int sid = Convert.ToInt32(Request.Form.Get("hiddenStockId").ToString());
            int quantity = Convert.ToInt32(Request.Form.Get("qty").ToString());
            int sum = Convert.ToInt32(tbFquan.Text) + Convert.ToInt32(tbPquan.Text);
            if (quantity == sum)
            {
                if (Convert.ToInt32(tbPquan.Text) >= 0 && Convert.ToInt32(tbFquan.Text) >= 0)
                {
                    DispatchService.DispatchingDeliveryServiceSoapClient client = new DispatchService.DispatchingDeliveryServiceSoapClient("DispatchingDeliveryServiceSoap");
                    int result = client.UpdateDispatch(sid);
                    int result1 = Insert();
                    int result2 = warehouse();
                    if (result == 1 && result1 == 1 && result2 == 1)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alert",
                        "<script>$(document).ready(function(){ $('.alert-error').hide(); $('.alert-success').show(); });</script>");
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alert",
                        "<script>$(document).ready(function(){ $('.alert-success').hide(); $('.alert-error').show(); });</script>");
                    }
                }
            }
            else if(quantity > sum)
            {

            }
            else if (quantity < sum)
            {

            }

        }
        public int Insert()
        {
            int a = 0;
            con.OpenConection();
            con.ExecSqlQuery("INSERT INTO activity_stock_raw(purchaseorder,empno,activity,date,time)VALUES(@po,@en,@act,@rdate,@t)");
            con.Cmd.Parameters.AddWithValue("@po", Request.Form.Get("po").ToString());
            con.Cmd.Parameters.AddWithValue("@rdate", DateTime.Parse(date));
            con.Cmd.Parameters.AddWithValue("@en", sessionempno);
            con.Cmd.Parameters.AddWithValue("@act", "Dispatch Raw Materials");
            con.Cmd.Parameters.AddWithValue("@t", DateTime.Parse(time));
            a = con.Cmd.ExecuteNonQuery();
            con.CloseConnection();
            return a;
        }
        public int fail()
        {
            int a = 0;
            int comp = Convert.ToInt32(tbFquan.Text);
            if (comp >= 0)
            {
                con.OpenConection();
                con.ExecSqlQuery("INSERT INTO stock_raw_fail(purchaseorder,quantity,date,status)VALUES(@po,@quan,@date,@st)");
                con.Cmd.Parameters.AddWithValue("@po", Request.Form.Get("po").ToString());
                con.Cmd.Parameters.AddWithValue("@quan", tbFquan.Text);
                con.Cmd.Parameters.AddWithValue("@date", DateTime.Parse(date));
                con.Cmd.Parameters.AddWithValue("@st", "Hold");
                a = con.Cmd.ExecuteNonQuery();
                con.CloseConnection();
            }

            return a;
        }
        public int warehouse()
        {
            int a = 0;
            int quantity = Convert.ToInt32(Request.Form.Get("qty").ToString());
            int comp = Convert.ToInt32(tbPquan.Text);
            if (comp >= 0 && comp == quantity)
            {
                int res = 0;
                con.OpenConection();
                con.ExecSqlQuery("INSERT INTO stock_warehouse(purchaseorder,quantity,receivedate,status)VALUES(@po,@quan,@rdate,@st)");
                con.Cmd.Parameters.AddWithValue("@po", Request.Form.Get("po").ToString());
                con.Cmd.Parameters.AddWithValue("@quan", tbPquan.Text);
                con.Cmd.Parameters.AddWithValue("@rdate", DateTime.Parse(date));
                con.Cmd.Parameters.AddWithValue("@st", "In Stock");
                a = con.Cmd.ExecuteNonQuery();
                con.CloseConnection();
                if(a > 0)
                {
                    res = fail();
                }
                a = res;
            }
            return a;
        }

        protected void tbFquan_TextChanged(object sender, EventArgs e)
        {
            //char key = e.KeyChar;
            //if (!Char.IsDigit(key) && key != 8)
            //{
            //    e.Handled = true;
            //}
            int compute = Convert.ToInt32(Request.Form.Get("qty").ToString()) - Convert.ToInt32(tbFquan.Text);
            if (compute >= 0)
            {
                tbPquan.Text = compute.ToString();
            }
        }

        protected void tbPquan_TextChanged(object sender, EventArgs e)
        {
            int compute = Convert.ToInt32(Request.Form.Get("qty").ToString()) - Convert.ToInt32(tbPquan.Text);
            if (compute >= 0)
            {
                tbFquan.Text = compute.ToString();
            }
        }
    }
}