using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Security.Cryptography;

namespace ecci.inv.system.superadmin
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
                con.ExecSqlQuery("SELECT * FROM department");
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
            bool user = addUser();
            if (user == true)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-error').hide(); $('.alert-success').show(); });</script>");
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
               "<script>$(document).ready(function(){ $('.alert-success').hide(); $('.alert-error').show(); });</script>");
            }
            //try
            //{
            //    con.OpenConection();
            //    con.ExecSqlQuery("INSERT INTO users (empno,password,firstname,lastname,position,gender,dept_id,reset) VALUES (@empid,@pass,@fname,@lname,@post,@gen,@dept,@reset)");
            //    con.Cmd.Parameters.Add("@empid", SqlDbType.VarChar).Value = tbEmpNo.Text;
            //    con.Cmd.Parameters.Add("@pass", SqlDbType.VarChar).Value = GetHashedText(tbPassword.Text);
            //    con.Cmd.Parameters.Add("@fname", SqlDbType.VarChar).Value = tbFname.Text;
            //    con.Cmd.Parameters.Add("@lname", SqlDbType.VarChar).Value = tbLname.Text;
            //    con.Cmd.Parameters.Add("@post", SqlDbType.VarChar).Value = tbPosition.Text;
            //    con.Cmd.Parameters.Add("@dept", SqlDbType.VarChar).Value = ddDept.Text;
            //    con.Cmd.Parameters.Add("@reset", SqlDbType.VarChar).Value = "Y";
            //    if (rbMale.Checked && !rbFemale.Checked)
            //    {
            //        con.Cmd.Parameters.Add("@gen", SqlDbType.Int).Value = 1;
            //    }
            //    else if (!rbMale.Checked && rbFemale.Checked)
            //    {
            //        con.Cmd.Parameters.Add("@gen", SqlDbType.Int).Value = 2;
            //    }
            //        con.Cmd.ExecuteNonQuery();
            //}

            //catch (Exception ex)
            //{
            //    Label1.Text = "Error: " + ex.Message + "";

            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
            //    "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
            //}
        }
        private Boolean addUser()
        {
            bool check = false;
            try
            {
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
                con.Cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                Label1.Text = "Error: " + ex.Message + "";

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').show(); });</script>");
            }
            return check;
        }
        private string GetHashedText(string inputData)
        {
            byte[] tmpSource;
            byte[] tmpData;
            tmpSource = ASCIIEncoding.ASCII.GetBytes(inputData);
            tmpData = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            return Convert.ToBase64String(tmpData);
        }
    }
}