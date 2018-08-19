using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecci.inv.system.superadmin
{
    public partial class supplier : Page
    {
        private string sessionempno { get; set; }
        DBConnection con;
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
            try
            {

                con.OpenConection();
                con.ExecSqlQuery("INSERT INTO suppliers(suppcode, suppname, suppadd, suppcontact)values(@scode, @sname, @sadd, @scontact)");
                con.Cmd.Parameters.AddWithValue("@scode", tbSuppCode.Text);
                con.Cmd.Parameters.AddWithValue("@sname", tbSuppName.Text);
                con.Cmd.Parameters.AddWithValue("@sadd", tbSuppAddress.Text);
                con.Cmd.Parameters.AddWithValue("@scontact", tbSuppContact.Text);
                int a = con.Cmd.ExecuteNonQuery();
                con.CloseConnection();
                if (a == 0)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "<script>$(document).ready(function(){ $('.alert-success').hide(); $('.alert-error').show(); });</script>");
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "<script>$(document).ready(function(){ $('.alert-error').hide(); $('.alert-success').show(); });</script>");
                }
                clear();
            }
            catch (Exception ex)
            {
                lbError.ForeColor = System.Drawing.Color.Red;
                lbError.Text = "Error: " + ex.Message;
                lbError.Visible = true;

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
            }
        }

        private void clear()
        {
            tbSuppCode.Text = "";
            tbSuppName.Text = "";
            tbSuppAddress.Text = "";
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
            "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
            Response.Redirect("index.aspx");
        }
    }
}