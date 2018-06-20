using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ecci.inv.system.superadmin
{
    public partial class users : System.Web.UI.Page
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
                con.ExecSqlQuery("SELECT * FROM department");
                ddDept.DataTextField = "dept_name";
                ddDept.DataValueField = "dept_name";
                ddDept.DataSource = con.DataQueryExec();
                ddDept.DataBind();
                ddDept.Items.Insert(0, new ListItem("Select Department", "-1"));
                con.CloseConnection();
            }
            catch (Exception ex)
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
                con.ExecSqlQuery("INSERT INTO users (empno,password,firstname,lastname,position,gender,dept_id) VALUES (@empid,@pass,@fname,@lname,@post,@gen,@dept)");
                con.Cmd.Parameters.Add("@empid", SqlDbType.VarChar).Value = tbEmpNo.Text;
                con.Cmd.Parameters.Add("@pass", SqlDbType.VarChar).Value = tbPassword.Text;
                con.Cmd.Parameters.Add("@fname", SqlDbType.VarChar).Value = tbFname.Text;
                con.Cmd.Parameters.Add("@lname", SqlDbType.VarChar).Value = tbLname.Text;
                con.Cmd.Parameters.Add("@post", SqlDbType.VarChar).Value = tbPosition.Text;
                con.Cmd.Parameters.Add("@dept", SqlDbType.VarChar).Value = ddDept.Text;
                if (rbMale.Checked && !rbFemale.Checked)
                {
                    con.Cmd.Parameters.Add("@gen", SqlDbType.Int).Value = 1;
                }
                else if (!rbMale.Checked && rbFemale.Checked)
                {
                    con.Cmd.Parameters.Add("@gen", SqlDbType.Int).Value = 2;
                }
                    con.Cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                Label1.Text = "Error: " + ex.Message + "";

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
            }
        }
    }
}