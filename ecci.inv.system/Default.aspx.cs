using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Web.UI.WebControls;

namespace ecci.inv.system
{
    public partial class Default : System.Web.UI.Page
    {
        DBConnection con;
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            con = new DBConnection();
        }
        private bool validatelogin(string user, string pass)
        {
            con.OpenConection();
            con.ExecSqlQuery("Select * From users where empno=@user COLLATE SQL_Latin1_General_CP1_CS_AS and password=@pass COLLATE SQL_Latin1_General_CP1_CS_AS");
            con.Cmd.Parameters.AddWithValue("@user", user);
            con.Cmd.Parameters.AddWithValue("@pass", pass);
            con._dr = con.Cmd.ExecuteReader();
            if (con._dr.Read())
            {
                con.CloseConnection();
                return true;
            }
            else
            {
                con.CloseConnection();
                return false;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (tbEmpNo.Text != "" && tbPassword.Text != "")
            {
                con.OpenConection();
                string user = tbEmpNo.Text.Trim();
                string pass = tbPassword.Text.Trim();
                if (user == "" || pass == "")
                {
                    tbPassword.Text = "";
                    tbEmpNo.Focus();
                    return;
                }
                bool r = validatelogin(user, pass);
                if (r == false)
                {
                    //Response.Write("Username and Password did not match");
                    lbError.Visible = true;
                    lbError.Text = "Incorrect Employee Number/Password";
                    lbError.ForeColor = Color.Red;
                }
                else
                {
                    //Response.Redirect("~/purchasing/index.aspx");
                    con.OpenConection();
                    con.ExecSqlQuery("Select dept_id From users where empno=@user COLLATE SQL_Latin1_General_CP1_CS_AS and password=@pass COLLATE SQL_Latin1_General_CP1_CS_AS");
                    con.Cmd.Parameters.AddWithValue("@user", user);
                    con.Cmd.Parameters.AddWithValue("@pass", pass);
                    con._dr = con.Cmd.ExecuteReader();
                    if (con._dr.Read())
                    {
                        string did = con._dr["dept_id"].ToString();

                        if (did == "Purchasing")
                        {
                            Response.Redirect("~/purchasing/index.aspx");
                        }
                        else if (did == "Super Admin")
                        {
                            Response.Redirect("~/superadmin/index.aspx");
                        }
                        //con.CloseConnection();
                    }
                    else
                    {
                        lbError.Visible = true;
                        lbError.Text = "User does not exist!";
                        lbError.ForeColor = Color.Red;
                    }
                }
                tbEmpNo.Focus();
                tbEmpNo.Text = "";
                tbPassword.Text = "";

            }
            else { }
        }
    }
}