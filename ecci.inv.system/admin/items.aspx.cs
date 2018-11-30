using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecci.inv.system.admin
{
    public partial class items : System.Web.UI.Page
    {
        private string sessionempno { get; set; }
        private int count { get; set; }
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
                con.ExecSqlQuery("SELECT * FROM suppliers ORDER BY suppname");
                ddSupplier.DataTextField = "suppname";
                ddSupplier.DataValueField = "suppcode";
                ddSupplier.DataSource = con.DataQueryExec();
                ddSupplier.DataBind();
                ddSupplier.Items.Insert(0, new ListItem("Select Supplier", "-1"));
                con.CloseConnection();
            }
            catch//(Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
            }
        }

        protected void btnSave_Click1(object sender, EventArgs e)
        {
            int result1 = addItems();
            int result2 = activityItems();

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

            clear();
        }

        private int addItems()
        {
            int a = 0;
            try
            {
                con.OpenConection();
                con.ExecSqlQuery("INSERT INTO items(suppcode, brandname, description,unitprice)VALUES(@scode, @bname, @des, @price)");
                con.Cmd.Parameters.AddWithValue("@scode", ddSupplier.SelectedValue);
                con.Cmd.Parameters.AddWithValue("@bname", tbBrand.Text);
                con.Cmd.Parameters.AddWithValue("@des", tbDescription.Text);
                con.Cmd.Parameters.Add("@price", SqlDbType.Money).Value = tbUnitPrice.Text;
                a = con.Cmd.ExecuteNonQuery();
                con.CloseConnection();

                con.OpenConection();
                con._dr = con.DataReader("SELECT TOP 1 itemsid FROM items ORDER BY itemsid DESC");
                while (con._dr.Read())
                {
                    count = Convert.ToInt32(con._dr["itemsid"].ToString());
                }
                con.CloseConnection();

            }
            catch
            {

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
            }
            return a;
        }

        private int activityItems()
        {
            int a = 0;
            DateTime dati = DateTime.Now;
            try
            {

                con.OpenConection();
                con.ExecSqlQuery("INSERT INTO activity_items(act_empno, act_suppcode, act_unitprice, act_remarks,act_itemsid,act_datetime)VALUES(@empno, @suppcode, @unitprice, @remarks, @id,@dt)");
                con.Cmd.Parameters.AddWithValue("@suppcode", ddSupplier.SelectedValue);
                con.Cmd.Parameters.AddWithValue("@empno", sessionempno);
                con.Cmd.Parameters.AddWithValue("@unitprice", tbUnitPrice.Text);
                con.Cmd.Parameters.AddWithValue("@remarks", "Add");
                con.Cmd.Parameters.AddWithValue("@id", count.ToString());
                con.Cmd.Parameters.AddWithValue("@dt", dati);
                a = con.Cmd.ExecuteNonQuery();
                con.CloseConnection();

            }
            catch
            {

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
            }
            return a;
        }
        private void clear()
        {
            tbBrand.Text = "";
            tbDescription.Text = "";
            ddSupplier.SelectedIndex = -1;
            tbUnitPrice.Text = "";
        }

        protected void btnBack_Click1(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
           "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
            Response.Redirect("index.aspx");
        }
    }

}