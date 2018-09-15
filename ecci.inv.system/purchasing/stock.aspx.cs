using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace ecci.inv.system.purchasing
{
    public partial class stock : System.Web.UI.Page
    {
        private string sessionempno { get; set; }
        private string ponumber { get; set; }
        private string ponumber2 { get; set; }
        DBConnection con;
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            con = new DBConnection();
            if (Session["empnumber"] != null)
            {
                sessionempno = Session["empnumber"].ToString();
                if (!IsPostBack)
                {
                    //tbCalendar.Visible = false;
                    ddBrand.Items.Insert(0, new ListItem("Select Brand", "-1"));
                    dropdown();
                    ddUnits();
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
                    tbDescription.Enabled = false;
                    ddBrand.Enabled = false;

                    ponumber = DateTime.Now.ToString("Myyddssff");
                    tbPO.Text = ponumber;
                }

                //ponumber = DateTime.Now.ToString("Myyssff");
                //tbPO.Text = ponumber;
            }


        }


        private void ddUnits()
        {
            try
            {
                con.OpenConection();
                con.ExecSqlQuery("SELECT * FROM unit ORDER BY unit ASC");
                ddlUnit.DataTextField = "unit";
                ddlUnit.DataValueField = "unit";
                ddlUnit.DataSource = con.DataQueryExec();
                ddlUnit.DataBind();
                ddlUnit.Items.Insert(0, new ListItem("Select Unit", "-1"));
                con.CloseConnection();
            }
            catch//(Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
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
            // tbPO.Text = "";
            tbDescription.Text = "";
            ddSupplier.SelectedIndex = -1;
            tbPO.Text = DateTime.Now.ToString("Myyddssff");
            ddBrand.SelectedIndex = -1;
            tbQuantity.Text = "";
            ddBrand.Enabled = false;
            tbUnitPrice.Text = "";
            tbTotalPrice.Text = "";
            ddlUnit.SelectedIndex = -1;
            //tbCalendar.Visible = false;
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
                tbUnitPrice.Text = "";
                tbUnitPrice.Enabled = false;
            }
            else
            {
                ddBrand.Enabled = true;
                try
                {
                    con.OpenConection();
                    con.ExecSqlQuery("SELECT * FROM items WHERE suppcode='" + ddSupplier.SelectedValue + "'");
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
        private string unitPrice { get; set; }
        protected void ddBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
            "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
            if (ddBrand.SelectedIndex == 0)
            {
                tbDescription.Text = "";
                tbDescription.Enabled = false;
                tbUnitPrice.Text = "";
                tbUnitPrice.Enabled = false;
            }
            else
            {
                tbDescription.Enabled = true;
                tbUnitPrice.Enabled = true;
                try
                {
                    con.OpenConection();
                    con._dr = con.DataReader("SELECT * FROM items WHERE itemsid='" + ddBrand.SelectedValue + "'");
                    while (con._dr.Read())
                    {
                        tbDescription.Text = con._dr["description"].ToString();
                        unitPrice = con._dr["unitprice"].ToString();
                        tbUnitPrice.Text = unitPrice;
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
            if (tama == true && mali == true)

            {
                print();
                modalPO.Show();
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-error').hide(); $('.alert-success').show(); });</script>");
            }
            else
            {

                //lbError.Text = "Dito may mali.";
                lbError.Visible = true;
                lbError.ForeColor = System.Drawing.Color.Red;
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide(); $('.alert-error').show(); });</script>");
            }
            clear();
        }
        private Boolean load()
        {
            //string date = DateTime.Now.ToString("MMMM dd yyyy");
            string date = DateTime.Now.ToShortDateString();
            bool check = false;
            lbError.Visible = false;
            try
            {
                ponumber2 = tbPO.Text;
                con.OpenConection();
                con.ExecSqlQuery("INSERT INTO stock_raw(purchaseorder, itemsid, quantity, receivedquantity, dispatchquantity, purchasedate, deliverydate, postatus, price, unit)VALUES(@po, @item, @quan, @rq,@dq, @pdate, @ddate, @stat, @price, @unit)");
                con.Cmd.Parameters.Add("@po", SqlDbType.NVarChar).Value = tbPO.Text;
                con.Cmd.Parameters.Add("@item", SqlDbType.Int).Value = ddBrand.SelectedValue;
                con.Cmd.Parameters.Add("@quan", SqlDbType.Int).Value = tbQuantity.Text;
                con.Cmd.Parameters.Add("@rq", SqlDbType.Int).Value = 0;
                con.Cmd.Parameters.Add("@dq", SqlDbType.Int).Value = 0;
                con.Cmd.Parameters.AddWithValue("@pdate", DateTime.Parse(date));
                con.Cmd.Parameters.AddWithValue("@ddate", DateTime.Parse(tbEdate.Text));
                con.Cmd.Parameters.Add("@stat", SqlDbType.Char).Value = "For delivery";
                con.Cmd.Parameters.Add("@price", SqlDbType.Money).Value = tbTotalPrice.Text;
                con.Cmd.Parameters.Add("@unit", SqlDbType.VarChar).Value = ddlUnit.SelectedValue;
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
            string date = DateTime.Now.ToShortDateString();
            string time = DateTime.Now.ToLongTimeString();
            bool check = false;
            lbError.Visible = false;
            try
            {
                //itemsid, quantity, purchasedate, deliverydate, postatus,@item, @quan, @pdate, @ddate, @stat,
                con.OpenConection();
                con.ExecSqlQuery("INSERT INTO activity_stock_raw(purchaseorder,empno,activity,date,time)VALUES(@po,@en,@act,@ddate,@t)");
                con.Cmd.Parameters.AddWithValue("@po", tbPO.Text);
                //con.Cmd.Parameters.AddWithValue("@item", ddBrand.SelectedValue);
                //con.Cmd.Parameters.AddWithValue("@quan", tbQuantity.Text);
                //con.Cmd.Parameters.AddWithValue("@pdate", date);
                con.Cmd.Parameters.AddWithValue("@en", sessionempno);
                con.Cmd.Parameters.AddWithValue("@act", "For Delivery");
                con.Cmd.Parameters.AddWithValue("@ddate", DateTime.Parse(date));
                //con.Cmd.Parameters.AddWithValue("@stat", "For delivery");
                con.Cmd.Parameters.AddWithValue("@t", DateTime.Parse(time));
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

        protected void tbQuantity_TextChanged(object sender, EventArgs e)
        {
            decimal uprice;
            decimal quant;
            uprice = Convert.ToDecimal(tbUnitPrice.Text);
            quant = Convert.ToDecimal(tbQuantity.Text);

            decimal total = uprice * quant;

            tbTotalPrice.Text = total.ToString();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //rvPurchaseOrder.ProcessingMode = ProcessingMode.Local;
            //rvPurchaseOrder.LocalReport.ReportPath = Server.MapPath("~/purchasing/purchOrder.rdlc");
            //purchaseorder dsCustomers = GetData("SELECT stock_raw.purchaseorder, stock_raw.purchasedate, stock_raw.deliverydate, stock_raw.quantity, stock_raw.itemsid, items.itemsid AS Expr1, items.description, items.unitprice, stock_raw.price FROM stock_raw INNER JOIN items ON stock_raw.itemsid = items.itemsid WHERE stock_raw.purchaseorder='" + ponumber2 + "'");
            //ReportDataSource purchaseorder = new ReportDataSource("poDataSet", dsCustomers.Tables[0]);
            //rvPurchaseOrder.LocalReport.DataSources.Clear();
            //rvPurchaseOrder.LocalReport.DataSources.Add(purchaseorder);
            //lbError.Text = ponumber;
            //lbError.Visible = true;
            //}
        }

        private void print()
        {
            rvPurchaseOrder.ProcessingMode = ProcessingMode.Local;
            rvPurchaseOrder.LocalReport.ReportPath = Server.MapPath("~/purchasing/purchOrder.rdlc");
            purchaseorder dsCustomers = GetData(@"SELECT stock_raw.purchaseorder, stock_raw.purchasedate, stock_raw.deliverydate, stock_raw.quantity, stock_raw.itemsid, items.itemsid AS Expr1, items.description, items.unitprice, stock_raw.price, items.suppcode, 
                            suppliers.suppcode AS Expr2, suppliers.suppname, suppliers.suppcontact, stock_raw.unit, activity_stock_raw.purchaseorder AS Expr3, activity_stock_raw.empno, users.empno AS Expr4, users.firstname, users.lastname
                            FROM stock_raw INNER JOIN
                            items ON stock_raw.itemsid = items.itemsid INNER JOIN
                            suppliers ON items.suppcode = suppliers.suppcode INNER JOIN
                            activity_stock_raw ON stock_raw.purchaseorder = activity_stock_raw.purchaseorder INNER JOIN
                            users ON activity_stock_raw.empno = users.empno WHERE stock_raw.purchaseorder=" + ponumber2 + "");
            ReportDataSource purchaseorder = new ReportDataSource("poDataSet", dsCustomers.Tables[0]);
            rvPurchaseOrder.LocalReport.DataSources.Clear();
            rvPurchaseOrder.LocalReport.DataSources.Add(purchaseorder);
            lbError.Text = ponumber;
            lbError.Visible = true;
        }
        private purchaseorder GetData(string query)
        {
            string cs = WebConfigurationManager.ConnectionStrings["getdatabase"].ConnectionString;
            SqlCommand cmd = new SqlCommand(query);

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;

                    sda.SelectCommand = cmd;
                    using (purchaseorder dsCustomers = new purchaseorder())
                    {
                        sda.Fill(dsCustomers, "poDT");
                        return dsCustomers;
                    }
                }
            }
        }
    }
}