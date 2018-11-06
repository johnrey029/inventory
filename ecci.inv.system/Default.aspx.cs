using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Security.Cryptography;

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
            con.ExecSqlQuery("SELECT * FROM users WHERE empno=@user COLLATE SQL_Latin1_General_CP1_CS_AS AND password=@pass COLLATE SQL_Latin1_General_CP1_CS_AS");
            con.Cmd.Parameters.AddWithValue("@user", user);
            con.Cmd.Parameters.AddWithValue("@pass", GetHashedText(pass));
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
        private string log_user { get; set; }
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
                    log_user = tbEmpNo.Text;
                    //user_activity();
                    // Response.Redirect("~/qualitycontrol/index.aspx");
                    con.OpenConection();
                    con.ExecSqlQuery("SELECT * FROM users WHERE empno=@user COLLATE SQL_Latin1_General_CP1_CS_AS AND password=@pass");
                    con.Cmd.Parameters.AddWithValue("@user", user);
                    con.Cmd.Parameters.AddWithValue("@pass", GetHashedText(pass));
                    con._dr = con.Cmd.ExecuteReader();
                    while (con._dr.Read())
                    {
                        string did = con._dr["dept_id"].ToString();
                        string passreset = con._dr["reset"].ToString();
                        if (passreset == "Y")
                        {
                            Session["empnumber"] = con._dr["empno"].ToString();
                            Response.Redirect("~/changepassword.aspx");
                        }
                        else
                        {
                            switch (did)
                            {
                                case "Purchasing":
                                    Session["empnumber"] = con._dr["empno"].ToString();
                                    Response.Redirect("~/purchasing/index.aspx");
                                    break;

                                case "Admin":
                                    Session["empnumber"] = con._dr["empno"].ToString();
                                    Response.Redirect("~/admin/index.aspx");
                                    break;

                                case "Quality Control":
                                    Session["empnumber"] = con._dr["empno"].ToString();
                                    Response.Redirect("~/qualitycontrol/index.aspx");
                                    break;

                                case "Warehouse":
                                    Session["empnumber"] = con._dr["empno"].ToString();
                                    Response.Redirect("~/warehouse/index.aspx");
                                    break;

                                case "Super Admin":
                                    Session["empnumber"] = con._dr["empno"].ToString();
                                    Response.Redirect("~/superadmin/rawmaterials.aspx");
                                    break;
                                case "Production":
                                    Session["empnumber"] = con._dr["empno"].ToString();
                                    Response.Redirect("~/production/orderedrequest.aspx");
                                    break;

                                case "Sales":
                                    Session["empnumber"] = con._dr["empno"].ToString();
                                    Response.Redirect("~/sales/index.aspx");
                                    break;

                                default:
                                    lbError.Visible = true;
                                    lbError.Text = "User does not exist!";
                                    lbError.ForeColor = Color.Red;
                                    break;
                            }
                        }
                        
                    }
                }
                tbEmpNo.Focus();
                tbEmpNo.Text = "";
                tbPassword.Text = "";

            }
        }
        private void user_activity()
        {
            con.OpenConection();
            con.ExecSqlQuery("INSERT INTO activity_login (act_empno,act_activity,act_datetime) VALUES (@empno,@activity,@datetime)");
            con.Cmd.Parameters.Add("@empno", SqlDbType.Char).Value = log_user;
            con.Cmd.Parameters.Add("@activity", SqlDbType.Char).Value = "Login";
            con.Cmd.Parameters.Add("@datetime", SqlDbType.DateTime).Value = DateTime.Now;
            con._dr = con.Cmd.ExecuteReader();
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