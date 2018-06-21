using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecci.inv.system.purchasing
{
    public partial class items : System.Web.UI.Page
    {
        DBConnection con;
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            con = new DBConnection();
            if (!IsPostBack)
            {
                dropdown();
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
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
        private void load()
        {
            try
            {
                con.OpenConection();
                con.ExecSqlQuery("insert into items(suppcode, brandname, description)values(@scode, @bname, @des)");
                con.Cmd.Parameters.AddWithValue("@scode", ddSupplier.SelectedValue);
                con.Cmd.Parameters.AddWithValue("@bname", tbBrand.Text);
                con.Cmd.Parameters.AddWithValue("@des", tbDescription.Text);
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
            catch//(Exception ex)
            {
                //lbError.ForeColor = System.Drawing.Color.Red;
                //lbError.Text = "Error: " + ex.Message;
                //lbError.Visible = true;

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
            }
        }
        private void clear()
        {
            tbBrand.Text = "";
            tbDescription.Text = "";
            ddSupplier.SelectedIndex = -1;
        }

        protected void btnSave_Click1(object sender, EventArgs e)
        {
            load();
        }

        protected void btnBack_Click1(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
           "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
            Response.Redirect("index.aspx");
        }
    }
    
}