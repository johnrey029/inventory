using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecci.inv.system.admin
{
    public partial class supplier : Page
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
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            con.OpenConection();
            con.ExecSqlQuery("SELECT * FROM suppliers WHERE suppcode = @suppcode");
            con.Cmd.Parameters.Add("@suppcode", SqlDbType.VarChar).Value = tbSuppCode.Text;
            con._dr = con.Cmd.ExecuteReader();
            if (con._dr.Read())
            {
                string suppCode = con._dr["suppcode"].ToString();
                if (suppCode == tbSuppCode.Text)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').show(); });</script>");
                    ScriptManager.RegisterStartupScript(this, GetType(), "err_msg", "alert('Nasa database na siya.');", true);
                }
            }
            else
            { 
                int result1 = addSupplier();
                int result2 = activitySupplier();
                if (result1 == 1 && result2 == 1)
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
        private int addSupplier()
        {
            int a = 0;
            try
            {
                con.OpenConection();
                con.ExecSqlQuery("INSERT INTO suppliers(suppcode, suppname, suppadd, suppcontact)values(@scode, @sname, @sadd, @scontact)");
                con.Cmd.Parameters.AddWithValue("@scode", tbSuppCode.Text);
                con.Cmd.Parameters.AddWithValue("@sname", tbSuppName.Text);
                con.Cmd.Parameters.AddWithValue("@sadd", tbSuppAddress.Text);
                con.Cmd.Parameters.AddWithValue("@scontact", tbSuppContact.Text);

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

        private int activitySupplier()
        {
            int a = 0;
            try
            {
                DateTime dati = DateTime.Now;
                con.OpenConection();
                con.ExecSqlQuery("INSERT INTO activity_suppliersaddupdate(act_empno, act_suppcode, act_remarks, act_datetime)VALUES(@empno, @scode, @remarks, @dt)");
                con.Cmd.Parameters.AddWithValue("@empno", sessionempno);
                con.Cmd.Parameters.AddWithValue("@scode", tbSuppCode.Text);
                con.Cmd.Parameters.AddWithValue("@remarks", "Add");
                con.Cmd.Parameters.AddWithValue("@dt", dati);

                a = con.Cmd.ExecuteNonQuery();
                con.CloseConnection();

                clear();
            }
            catch (Exception ex)
            {
                lbError.ForeColor = System.Drawing.Color.Red;
                lbError.Text = "Error: " + ex.Message;
                lbError.Visible = true;
            }
            return a;
        }

        private void clear()
        {
            tbSuppCode.Text = "";
            tbSuppName.Text = "";
            tbSuppAddress.Text = "";
            tbSuppContact.Text = "";
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
            "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
            Response.Redirect("index.aspx");
        }
    }
}