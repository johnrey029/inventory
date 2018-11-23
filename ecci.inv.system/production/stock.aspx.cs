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

namespace ecci.inv.system.production
{
    public partial class stock : System.Web.UI.Page
    {
        private string sessionempno { get; set; }
        private string orderid { get; set; }
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
                    ddProduct.Items.Insert(0, new ListItem("Select Brand", "-1"));
                    dropdown();
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
                    tbExpected.Enabled = false;
                    ddProduct.Enabled = false;
                }
            }


        }

        
        private void dropdown()
        {
            try
            {
                con.OpenConection();
                con.ExecSqlQuery("Select * from oderdetails where status=@stat");
                con.Cmd.Parameters.Add("@stat", SqlDbType.NVarChar).Value = "In Production";
                ddOrder.DataTextField = "orderid";
                ddOrder.DataValueField = "orderid";
                ddOrder.DataSource = con.DataQueryExec();
                ddOrder.DataBind();
                ddOrder.Items.Insert(0, new ListItem("Select Order ID", "-1"));
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
            tbExpected.Text = "";
            ddOrder.SelectedIndex = -1;
            ddProduct.SelectedIndex = -1;
            tbQuantity.Text = "";
            ddProduct.Enabled = false;
        }
        
        private string unitPrice { get; set; }
      
        protected void btnSave_Click(object sender, EventArgs e)
        {
           // bool tama = load();
          //  bool mali = activity();
            //if (tama == true && mali == true)

           // {
                //print();
                //modalPO.Show();
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-error').hide(); $('.alert-success').show(); });</script>");
          //  }
         //   else
         //   {

                //lbError.Text = "Dito may mali.";
                lbError.Visible = true;
                lbError.ForeColor = System.Drawing.Color.Red;
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide(); $('.alert-error').show(); });</script>");
         //   }
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
                //ponumber2 = tbPO.Text;
                con.OpenConection();
                con.ExecSqlQuery("INSERT INTO stock_raw(purchaseorder, itemsid, quantity, receivedquantity, dispatchquantity,totalquantity, purchasedate, deliverydate, postatus, price, unit)VALUES(@po, @item, @quan, @rq,@dq,@tq, @pdate, @ddate, @stat, @price, @unit)");
                con.Cmd.Parameters.Add("@item", SqlDbType.Int).Value = ddProduct.SelectedValue;
                con.Cmd.Parameters.Add("@quan", SqlDbType.Int).Value = tbQuantity.Text;
                con.Cmd.Parameters.Add("@rq", SqlDbType.Int).Value = 0;
                con.Cmd.Parameters.Add("@dq", SqlDbType.Int).Value = 0;
                con.Cmd.Parameters.Add("@tq", SqlDbType.Int).Value = tbQuantity.Text;
                con.Cmd.Parameters.AddWithValue("@pdate", DateTime.Parse(date));
                con.Cmd.Parameters.Add("@stat", SqlDbType.Char).Value = "For delivery";
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
                con.ExecSqlQuery("INSERT INTO activity_stock_raw(purchaseorder,empno,activity,date,time,remarks)VALUES(@po,@en,@act,@ddate,@t,@rem)");
                //con.Cmd.Parameters.AddWithValue("@po", tbPO.Text);
                //con.Cmd.Parameters.AddWithValue("@item", ddBrand.SelectedValue);
                //con.Cmd.Parameters.AddWithValue("@quan", tbQuantity.Text);
                //con.Cmd.Parameters.AddWithValue("@pdate", date);
                con.Cmd.Parameters.AddWithValue("@en", sessionempno);
                con.Cmd.Parameters.AddWithValue("@act", "For Delivery");
                con.Cmd.Parameters.AddWithValue("@ddate", DateTime.Parse(date));
                //con.Cmd.Parameters.AddWithValue("@stat", "For delivery");
                con.Cmd.Parameters.AddWithValue("@t", DateTime.Parse(time));
                con.Cmd.Parameters.AddWithValue("@rem", "Add");
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
        }

        protected void ddOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddOrder.SelectedIndex == 0)
            {
                ddProduct.SelectedIndex = 0;
                ddProduct.Enabled = false;
                tbExpected.Text = "";
                tbExpected.Enabled = false;
            }
            else
            {
                ddProduct.Enabled = true;
                try
                {
                    con.OpenConection();
                    con.ExecSqlQuery(@"SELECT od.productid,  p.pname FROM oderdetails od 
                    INNER JOIN product p ON od.productid = p.productid
                    WHERE od.orderid=@oid and od.status=@stat");
                    con.Cmd.Parameters.Add("@oid", SqlDbType.NVarChar).Value = ddOrder.SelectedValue;
                    con.Cmd.Parameters.Add("@stat", SqlDbType.NVarChar).Value = "In Production";
                    ddProduct.DataTextField = "pname";
                    ddProduct.DataValueField = "productid";
                    ddProduct.DataSource = con.DataQueryExec();
                    ddProduct.DataBind();
                    ddProduct.Items.Insert(0, new ListItem("Select Product", "-1"));
                    con.CloseConnection();
                    orderid = ddOrder.SelectedValue;
                }
                catch
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
                }
            }
        }

        protected void ddProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddProduct.SelectedIndex == 0)
            {
                tbExpected.Text = "";
                tbExpected.Enabled = false; 
            }
            else
            {
                try
                {

                    con.OpenConection();
                    con._dr = con.DataReader(@"SELECT od.quantityordered,c.name FROM oderdetails od 
                    INNER JOIN product p ON od.productid = p.productid
                    INNER JOIN purchaseorder po ON od.orderid = po.orderid
                    INNER JOIN client c ON po.clientid = c.clientid
                    WHERE od.productid='" + ddProduct.SelectedValue +"' and od.orderid='" + orderid + "' and od.status='" + "In Production" + "'");
                    if (con._dr.Read())
                    {
                        tbExpected.Text = con._dr["quantityordered"].ToString();
                        tbClient.Text = con._dr["name"].ToString();
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

        //private void print()
        //{
        //    rvPurchaseOrder.ProcessingMode = ProcessingMode.Local;
        //    rvPurchaseOrder.LocalReport.ReportPath = Server.MapPath("~/purchasing/purchOrder.rdlc");
        //    purchaseorder dsCustomers = GetData(@"SELECT stock_raw.purchaseorder, stock_raw.purchasedate, stock_raw.deliverydate, stock_raw.quantity, stock_raw.itemsid, items.itemsid AS Expr1, items.description, items.unitprice, stock_raw.price, items.suppcode, 
        //                    suppliers.suppcode AS Expr2, suppliers.suppname, suppliers.suppcontact, stock_raw.unit, activity_stock_raw.purchaseorder AS Expr3, activity_stock_raw.empno, users.empno AS Expr4, users.firstname, users.lastname
        //                    FROM stock_raw INNER JOIN
        //                    items ON stock_raw.itemsid = items.itemsid INNER JOIN
        //                    suppliers ON items.suppcode = suppliers.suppcode INNER JOIN
        //                    activity_stock_raw ON stock_raw.purchaseorder = activity_stock_raw.purchaseorder INNER JOIN
        //                    users ON activity_stock_raw.empno = users.empno WHERE stock_raw.purchaseorder=" + ponumber2 + "");
        //    ReportDataSource purchaseorder = new ReportDataSource("poDataSet", dsCustomers.Tables[0]);
        //    rvPurchaseOrder.LocalReport.DataSources.Clear();
        //    rvPurchaseOrder.LocalReport.DataSources.Add(purchaseorder);
        //    lbError.Text = ponumber;
        //    lbError.Visible = true;
        //}
        //private purchaseorder GetData(string query)
        //{
        //    string cs = WebConfigurationManager.ConnectionStrings["getdatabase"].ConnectionString;
        //    SqlCommand cmd = new SqlCommand(query);

        //    using (SqlConnection con = new SqlConnection(cs))
        //    {
        //        using (SqlDataAdapter sda = new SqlDataAdapter())
        //        {
        //            cmd.Connection = con;

        //            sda.SelectCommand = cmd;
        //            using (purchaseorder dsCustomers = new purchaseorder())
        //            {
        //                sda.Fill(dsCustomers, "poDT");
        //                return dsCustomers;
        //            }
        //        }
        //    }
        //}
    }
}