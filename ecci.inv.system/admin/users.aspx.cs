using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Security.Cryptography;

namespace ecci.inv.system.admin
{
    public partial class users : System.Web.UI.Page
    {
        private string sessionempno { get; set; }
        DBConnection con;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["empnumber"] != null)
            {
                sessionempno = Session["empnumber"].ToString();
                ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
                con = new DBConnection();
                if (!IsPostBack)
                {
                    Label1.Visible = false;
                    dropdown();
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
                }
            }
            else
            {
                Session.Clear();
                Response.Redirect("~/default.aspx");
            }


        }

        private void dropdown()
        {
            try
            {
                con.OpenConection();
                con.ExecSqlQuery("SELECT * FROM department ORDER BY dept_name ASC");
                ddDept.DataTextField = "dept_name";
                ddDept.DataValueField = "dept_name";
                ddDept.DataSource = con.DataQueryExec();
                ddDept.DataBind();
                ddDept.Items.Insert(0, new ListItem("SELECT Department", "-1"));
                con.CloseConnection();
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //try
            //{
            Label1.Text = "";
            con.OpenConection();
            con.ExecSqlQuery("SELECT * FROM users WHERE empno = @empno");
            con.Cmd.Parameters.Add("@empno", SqlDbType.VarChar).Value = tbEmpNo.Text;
            con._dr = con.Cmd.ExecuteReader();
            if (con._dr.Read())
            {
                string empNumber = con._dr["empno"].ToString();
                if (empNumber == tbEmpNo.Text)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').show(); });</script>");
                    ScriptManager.RegisterStartupScript(this, GetType(), "err_msg", "alert('Nasa database na siya.');", true);
                }
            }
            else
            {
                int result1 = addUser();
                int result2 = activityUser();

                if (result1 == 1 && result2 == 1)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "<script>$(document).ready(function(){ $('.alert-error').hide();$('.alert-success').hide(); });</script>");
                    //ScriptManager.RegisterStartupScript(this, GetType(), "err_msg", "alert('Wala pa siya sa database.');", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
                }

                
            }
            cleartext();
            //}

            //catch (Exception ex)
            //{
            //    Label1.Text = "Error: " + ex.Message + "";

            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
            //    "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').show(); });</script>");
            //}
        }
        private int addUser()
        {
            int a = 0;
            try
            {
                Label1.Text = "";
                con.OpenConection();
                con.ExecSqlQuery("INSERT INTO users (empno,password,firstname,lastname,position,gender,dept_id,reset) VALUES (@empid,@pass,@fname,@lname,@post,@gen,@dept,@reset)");
                con.Cmd.Parameters.Add("@empid", SqlDbType.VarChar).Value = tbEmpNo.Text;
                con.Cmd.Parameters.Add("@pass", SqlDbType.VarChar).Value = GetHashedText(tbPassword.Text);
                con.Cmd.Parameters.Add("@fname", SqlDbType.VarChar).Value = tbFname.Text;
                con.Cmd.Parameters.Add("@lname", SqlDbType.VarChar).Value = tbLname.Text;
                con.Cmd.Parameters.Add("@post", SqlDbType.VarChar).Value = tbPosition.Text;
                con.Cmd.Parameters.Add("@dept", SqlDbType.VarChar).Value = ddDept.Text;
                con.Cmd.Parameters.Add("@reset", SqlDbType.VarChar).Value = "Y";
                if (rbMale.Checked && !rbFemale.Checked)
                {
                    con.Cmd.Parameters.Add("@gen", SqlDbType.Int).Value = 1;
                }
                else if (!rbMale.Checked && rbFemale.Checked)
                {
                    con.Cmd.Parameters.Add("@gen", SqlDbType.Int).Value = 2;
                }
                a = con.Cmd.ExecuteNonQuery();

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-error').hide();$('.alert-success').show(); });</script>");
            }

            catch (Exception ex)
            {
                Label1.Text = "Error: " + ex.Message + "";

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').show(); });</script>");
            }
            return a;
        }
        private int activityUser()
        {
            int a = 0;
            try
            {
                Label1.Text = "";
                con.OpenConection();
                con.ExecSqlQuery("INSERT INTO activity_users (act_empno,act_fname,act_lname,act_remarks) VALUES (@empid,@fname,@lname,@remarks)");
                con.Cmd.Parameters.Add("@empid", SqlDbType.VarChar).Value = tbEmpNo.Text;
                con.Cmd.Parameters.Add("@fname", SqlDbType.VarChar).Value = tbFname.Text;
                con.Cmd.Parameters.Add("@lname", SqlDbType.VarChar).Value = tbLname.Text;
                con.Cmd.Parameters.Add("@remarks", SqlDbType.VarChar).Value = "Add";

                a = con.Cmd.ExecuteNonQuery();

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-error').hide();$('.alert-success').show(); });</script>");
            }

            catch (Exception ex)
            {
                Label1.Text = "Error: " + ex.Message + "";

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').show(); });</script>");
            }
            return a;
        }
        private string GetHashedText(string inputData)
        {
            byte[] tmpSource;
            byte[] tmpData;
            tmpSource = ASCIIEncoding.ASCII.GetBytes(inputData);
            tmpData = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            return Convert.ToBase64String(tmpData);
        }
        private void cleartext()
        {
            ddDept.SelectedIndex = -1;
            tbConfPassword.Text = "";
            tbEmpNo.Text = "";
            tbFname.Text = "";
            tbLname.Text = "";
            tbPassword.Text = "";
            tbPosition.Text = "";
            rbMale.Checked = false;
            rbFemale.Checked = false;
        }
    }
}