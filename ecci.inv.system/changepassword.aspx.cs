﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Security.Cryptography;

namespace ecci.inv.system
{
    public partial class changepassword : System.Web.UI.Page
    {
        private string sessionempno { get; set; }
        DBConnection con;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["empnumber"] != null)
            {
                sessionempno = Session["empnumber"].ToString();
                con = new DBConnection();
            }
            else
            {
                Session.Clear();
                Response.Redirect("~/default.aspx");
            }
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                con.OpenConection();
                con.ExecSqlQuery("SELECT password FROM users WHERE empno=@empno");
                con.Cmd.Parameters.Add("@empno", SqlDbType.VarChar).Value = sessionempno;
                con._dr = con.Cmd.ExecuteReader();
                if (con._dr.Read())
                {
                    string oldpass = con._dr["password"].ToString();
                    if (oldpass == GetHashedText(tboldPassword.Text))
                    {
                        //Test if the new password matched.
                        if (tbNewPassword.Text == tbConfNewPassword.Text)
                        {
                            con.OpenConection();
                            con.ExecSqlQuery("UPDATE users SET password=@newpass,reset=@resetno WHERE empno=@empno");
                            con.Cmd.Parameters.Add("@newpass", SqlDbType.VarChar).Value = GetHashedText(tbNewPassword.Text);
                            con.Cmd.Parameters.Add("@resetno", SqlDbType.VarChar).Value = "N";
                            con.Cmd.Parameters.Add("@empno", SqlDbType.VarChar).Value = sessionempno;
                            con.Cmd.ExecuteNonQuery();
                            ScriptManager.RegisterStartupScript(this, GetType(), "err_msg", "alert('Password changed successfully');window.location='default.aspx';", true);

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "err_msg", "alert('Password do not match!');", true);
                        }
                    }
                    else
                    {
                        //Warning message: Old password is invalid.
                        ScriptManager.RegisterStartupScript(this, GetType(), "err_msg", "alert('Old password is invalid!');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err_msg", "alert('" + ex.ToString() + "');", true);
            }
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