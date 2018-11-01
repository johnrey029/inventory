using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecci.inv.system.admin
{
    public partial class clients : System.Web.UI.Page
    {
        private string sessionempno { get; set; }
        DBConnection con;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["empnumber"] != null)
            {
                sessionempno = Session["empnumber"].ToString();
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
            con.OpenConection();
            con.ExecSqlQuery("SELECT * FROM client WHERE name = @clientname");
            con.Cmd.Parameters.Add("@clientname", SqlDbType.VarChar).Value = tbClientName.Text;
            con._dr = con.Cmd.ExecuteReader();
            if (con._dr.Read())
            {
                string suppCode = con._dr["name"].ToString();
                if (suppCode == tbClientName.Text)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').show(); });</script>");
                    ScriptManager.RegisterStartupScript(this, GetType(), "err_msg", "alert('Client exists in database.');", true);
                }
            }
            else
            {
                int result1 = addClient();
                //int result2 = activityClient();
                if (result1 == 1) //&& result2 == 1)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "<script>$(document).ready(function(){ $('.alert-error').hide(); $('.alert-success').show(); $('.alert-warning').hide(); });</script>");
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "<script>$(document).ready(function(){ $('.alert-success').hide(); $('.alert-error').show(); $('.alert-warning').hide(); });</script>");
                }
            }
        }
        private int addClient()
        {
            int a = 0;
            try
            {
                con.OpenConection();
                con.ExecSqlQuery("INSERT INTO client(name, address, contact1, contact2, contactstatus) VALUES(@name, @address, @contact1, @contact2, @contactstat)");
                con.Cmd.Parameters.AddWithValue("@name", tbClientName.Text);
                con.Cmd.Parameters.AddWithValue("@address", tbClientAdd.Text);
                con.Cmd.Parameters.AddWithValue("@contact1", tbClientCon.Text);
                con.Cmd.Parameters.AddWithValue("@contact2", tbClientCon2.Text);
                if (rbActive.Checked && !rbDeactivate.Checked)
                {
                    con.Cmd.Parameters.Add("@contactstat", SqlDbType.Int).Value = 1;
                }
                else if (!rbActive.Checked && rbDeactivate.Checked)
                {
                    con.Cmd.Parameters.Add("@contactstat", SqlDbType.Int).Value = 2;
                }

                a = con.Cmd.ExecuteNonQuery();
                con.CloseConnection();

                //clear();
            }
            catch (Exception ex)
            {
                lbError.ForeColor = System.Drawing.Color.Red;
                lbError.Text = "Error: " + ex.Message;
                lbError.Visible = true;
            }
            return a;
        }

        //private int activitySupplier()
        //{
        //    int a = 0;
        //    try
        //    {
        //        con.OpenConection();
        //        con.ExecSqlQuery("INSERT INTO activity_suppliersaddupdate(act_empno, act_suppcode, act_remarks)VALUES(@empno, @scode, @remarks)");
        //        con.Cmd.Parameters.AddWithValue("@empno", sessionempno);
        //        con.Cmd.Parameters.AddWithValue("@scode", tbSuppCode.Text);
        //        con.Cmd.Parameters.AddWithValue("@remarks", "Add");

        //        a = con.Cmd.ExecuteNonQuery();
        //        con.CloseConnection();

        //        clear();
        //    }
        //    catch (Exception ex)
        //    {
        //        lbError.ForeColor = System.Drawing.Color.Red;
        //        lbError.Text = "Error: " + ex.Message;
        //        lbError.Visible = true;
        //    }
        //    return a;
        //}
    }
}