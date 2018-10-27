using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ecci.inv.system.admin
{
    public partial class department : System.Web.UI.Page
    {
        private string sessionempno { get; set; }
        DBConnection con;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["empnumber"] != null)
            {
                sessionempno = Session["empnumber"].ToString();
                if (!IsPostBack)
                {
                    //dropdown();
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
                }
            }

            else
            {
                Session.Clear();
                Response.Redirect("~/default.aspx");
            }
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            con = new DBConnection();
            if (!IsPostBack)
            {
                //dropdown();
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                con.OpenConection();
                con.ExecSqlQuery("INSERT INTO department (dept_name,dept_head) VALUES (@deptname,@depthead)");
                con.Cmd.Parameters.Add("@deptname", SqlDbType.VarChar).Value = tbDeptName.Text;
                con.Cmd.Parameters.Add("@depthead", SqlDbType.VarChar).Value = tbDeptHead.Text;
                con.Cmd.ExecuteNonQuery();
            }
            catch 
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
            }
        }
    }
}