using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecci.inv.system.purchasing
{
    public partial class stock : System.Web.UI.Page
    {
        private string sessionempno { get; set; }
        DBConnection con;
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            con = new DBConnection();
            if (Session["empnumber"] != null)
            {
                sessionempno = Session["empnumber"].ToString();
            }
            if (!IsPostBack)
            {
                tbCalendar.Visible = false;
                ddBrand.Items.Insert(0, new ListItem("Select Brand", "-1"));
                dropdown();
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
                ddBrand.Enabled = false;
                tbDescription.Enabled = false;
            }

        }
        private void dropdown()
        {
            try
            {
                con.OpenConection();
                con.ExecSqlQuery("Select * from suppliers");
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
        private void clear()
        {
            tbPO.Text = ""; ;
            tbDescription.Text = "";
            ddSupplier.SelectedIndex = -1;
            tbPO.Text = "";
            ddBrand.SelectedIndex = -1;
            tbQuantity.Text = "";
            tbCalendar.Visible = false;
            tbEdate.Text = "";
        }

        protected void ddSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddSupplier.SelectedIndex == 0)
            {
                ddBrand.SelectedIndex = 0;
                ddBrand.Enabled = false;
                ddSupplier.SelectedIndex = 0;
                tbDescription.Text = "";
                tbDescription.Enabled = false;
            }
            else
            {
                ddBrand.Enabled = true;
                try
                {
                    con.OpenConection();
                    con.ExecSqlQuery("Select * from items where suppcode='" + ddSupplier.SelectedValue + "'");
                    ddBrand.DataTextField = "brandname";
                    ddBrand.DataValueField = "itemsid";
                    ddBrand.DataSource = con.DataQueryExec();
                    ddBrand.DataBind();
                    ddBrand.Items.Insert(0, new ListItem("Select Brand", "-1"));
                    con.CloseConnection();
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
                }
            }
        }

        protected void ddBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
            "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
            if (ddBrand.SelectedIndex == 0)
            {
                tbDescription.Text = "";
                tbDescription.Enabled = false;
            }
            else
            {
                tbDescription.Enabled = true;
                try
                {
                    con.OpenConection();
                    con._dr = con.DataReader("Select * from items where itemsid='" + ddBrand.SelectedValue + "'");
                    while(con._dr.Read())
                    {
                        tbDescription.Text = con._dr["description"].ToString();
                    }
                    con.CloseConnection();
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool tama = load();
            bool mali = activity();
            if(tama == true && mali == true)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-error').hide(); $('.alert-success').show(); });</script>");
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide(); $('.alert-error').show(); });</script>");
            }
            clear();
        }
        private Boolean load()
        {
            string date = DateTime.Now.ToString("MMMM dd yyyy");
            bool check = false;
            lbError.Visible = false;
            try
            {
                con.OpenConection();
                con.ExecSqlQuery("insert into stock(purchaseorder, itemsid, quantity, purchasedate, deliverydate, postatus)values(@po, @item, @quan, @pdate, @ddate, @stat)");
                con.Cmd.Parameters.AddWithValue("@po", tbPO.Text);
                //con.Cmd.Parameters.AddWithValue("@scode", ddSupplier.SelectedValue);
                con.Cmd.Parameters.AddWithValue("@item", ddBrand.SelectedValue);
                con.Cmd.Parameters.AddWithValue("@quan", tbQuantity.Text);
                con.Cmd.Parameters.AddWithValue("@pdate", date);
                con.Cmd.Parameters.AddWithValue("@ddate", tbEdate.Text);
                con.Cmd.Parameters.AddWithValue("@stat", "For delivery");
                int a = con.Cmd.ExecuteNonQuery();
                con.CloseConnection();
                if (a == 0)
                {
                    check = false;
                }
                else
                {
                    check = true;
                }
            }
            catch (Exception ex)
            {
                check = false;
                lbError.ForeColor = System.Drawing.Color.Red;
                lbError.Text = "Error: " + ex.Message;
                lbError.Visible = true;
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide(); $('.alert-error').show(); });</script>");
            }
            return check;
        }
        private Boolean activity()
        {
            //string date = DateTime.Now.ToString("MMMM dd, yyyy hh:mm tt");
            string date = DateTime.Now.ToString("MMMM dd, yyyy hh:mm tt");
            bool check = false;
            lbError.Visible = false;
            try
            {
                con.OpenConection();
                con.ExecSqlQuery("insert into activity_transaction(purchaseorder, itemsid, quantity, purchasedate, deliverydate, postatus,empno,activity)values(@po, @item, @quan, @pdate, @ddate, @stat,@en,@act)");
                con.Cmd.Parameters.AddWithValue("@po", tbPO.Text);
                con.Cmd.Parameters.AddWithValue("@item", ddBrand.SelectedValue);
                con.Cmd.Parameters.AddWithValue("@quan", tbQuantity.Text);
                con.Cmd.Parameters.AddWithValue("@pdate", date);
                con.Cmd.Parameters.AddWithValue("@ddate", tbCalendar.SelectedDate.ToString());
                con.Cmd.Parameters.AddWithValue("@stat", "For delivery");
                con.Cmd.Parameters.AddWithValue("@en", sessionempno);
                con.Cmd.Parameters.AddWithValue("@act", "Ordered");
                int a = con.Cmd.ExecuteNonQuery();
                con.CloseConnection();
                if (a == 0)
                {
                    check = false;
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    //"<script>$(document).ready(function(){ $('.alert-success').hide(); $('.alert-error').show(); });</script>");
                }
                else
                {
                    check = true;
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    //"<script>$(document).ready(function(){ $('.alert-error').hide(); $('.alert-success').show(); });</script>");
                }
            }
            catch (Exception ex)
            {
                lbError.ForeColor = System.Drawing.Color.Red;
                lbError.Text = "Error: " + ex.Message;
                lbError.Visible = true;
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide(); $('.alert-error').show(); });</script>");
            }
            return check;
        }

        protected void tbCalendar_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date.CompareTo(DateTime.Now.Date) < 0)
            {
                e.Cell.Text = e.Day.DayNumberText;
                e.Day.IsSelectable = false;
            }
        }
        
        protected void tbEdate_TextChanged(object sender, EventArgs e)
        {
            tbCalendar.Visible = true;
        }

        protected void tbCalendar_SelectionChanged(object sender, EventArgs e)
        {
            tbEdate.Text = tbCalendar.SelectedDate.ToShortDateString();
            tbCalendar.Visible = false;
        }
    }
}