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
    public partial class resetpassword : System.Web.UI.Page
    {
        private string sessionempno { get; set;}
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            con.OpenConection();
            con.ExecSqlQuery("SELECT * FROM users WHERE empno=@empno");
            con.Cmd.Parameters.Add("@empno", SqlDbType.VarChar).Value = tbEmpNo.Text;
            con._dr = con.Cmd.ExecuteReader();
            if (con._dr.Read())
            {
                string employeenum = con._dr["empno"].ToString();
                string resetstat = con._dr["reset"].ToString();

                if (employeenum != tbEmpNo.Text)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "err_msg", "alert('User does not exist.');", true);
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
                }
                else
                {
                    if (tbPassword.Text == tbConfPassword.Text)
                    {
                        if (resetstat == "Y" || resetstat == "N")
                        {
                            con.OpenConection();
                            con.ExecSqlQuery("UPDATE users SET password=@newpass, reset=@reset WHERE empno=@empno");
                            con.Cmd.Parameters.Add("@empno", SqlDbType.VarChar).Value = tbEmpNo.Text;
                            con.Cmd.Parameters.Add("@newpass", SqlDbType.VarChar).Value = GetHashedText(tbConfPassword.Text);
                            con.Cmd.Parameters.Add("@reset", SqlDbType.VarChar).Value = "Y";
                            con.Cmd.ExecuteNonQuery();

                            activityReset();

                        }
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                        "<script>$(document).ready(function(){ $('.alert-error').hide();$('.alert-success').show(); });</script>");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "err_msg", "alert('Password do not match!');", true);
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
                    }
                }
            }
        }

        private void activityReset()
        {
            DateTime dati = DateTime.Now;
            con.OpenConection();
            con.ExecSqlQuery("INSERT INTO activity_reset(empno, rst_empno, datetime)VALUES(@empno, @rst, @dt)");
            con.Cmd.Parameters.AddWithValue("@empno", sessionempno);
            con.Cmd.Parameters.AddWithValue("@rst", tbEmpNo.Text);
            con.Cmd.Parameters.AddWithValue("@dt", dati);

            con.Cmd.ExecuteNonQuery();
            con.CloseConnection();
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