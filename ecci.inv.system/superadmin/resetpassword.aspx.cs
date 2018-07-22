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
    public partial class resetpassword : System.Web.UI.Page
    {
        private string sessionempno { get; set;}
        DBConnection con;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["empnumber"] != null)
            {
                sessionempno = Session["empnumber"].ToString();
                con = new DBConnection();
                if (!IsPostBack)
                {
                    //dropdown();
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            con.OpenConection();
            con.ExecSqlQuery("SELECT * FROM users WHERE empno=@empno");
            con.Cmd.Parameters.Add("@empno", SqlDbType.VarChar).Value = sessionempno;
            con._dr = con.Cmd.ExecuteReader();
            if (con._dr.Read())
            {
                string employeenum = con._dr["empno"].ToString();
                string resetstat = con._dr["reset"].ToString();

                if (employeenum != tbEmpNo.Text)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "err_msg", "alert('User does not exist.');", true);
                }
                else
                {
                    if (tbPassword.Text == tbConfPassword.Text)
                    {
                        if(resetstat=="Y" || resetstat == "N")
                        {
                            con.OpenConection();
                            con.ExecSqlQuery("UPDATE users SET password=@newpass, reset=@reset WHERE empno=@empno");
                            con.Cmd.Parameters.Add("@empno", SqlDbType.VarChar).Value = sessionempno;
                            con.Cmd.Parameters.Add("@newpass", SqlDbType.VarChar).Value = GetHashedText(tbConfPassword.Text);
                            con.Cmd.Parameters.Add("@reset", SqlDbType.VarChar).Value = "Y";
                            con.Cmd.ExecuteNonQuery();
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert","<script>$(document).ready(function(){ $('.alert-success').show();$('.alert-error').hide(); });</script>");

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "err_msg", "alert('Password do not match!');", true);
                    }
                }
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