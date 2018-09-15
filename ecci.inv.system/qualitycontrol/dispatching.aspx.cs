using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecci.inv.system.qualitycontrol
{
    public partial class dispatching : System.Web.UI.Page
    {
        private string sessionempno { get; set; }
        DBConnection con;
        string date = DateTime.Now.ToShortDateString();
        string time = DateTime.Now.ToLongTimeString();
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
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); $('.alert-warning').hide(); });</script>");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int sid = Convert.ToInt32(Request.Form.Get("hiddenStockId").ToString());
            int quantity = Convert.ToInt32(Request.Form.Get("qty").ToString());
            int dp = Convert.ToInt32(Request.Form.Get("dispatchquantity").ToString());
            int sum = Convert.ToInt32(tbFquan.Text) + Convert.ToInt32(tbPquan.Text);
            if (quantity == sum)
            {
                if (Convert.ToInt32(tbPquan.Text) >= 0 && Convert.ToInt32(tbFquan.Text) >= 0)
                {
                    DispatchService.DispatchingDeliveryServiceSoapClient client = new DispatchService.DispatchingDeliveryServiceSoapClient("DispatchingDeliveryServiceSoap");
                    int result = client.UpdateDispatch(sid, dp+sum);
                    int result1 = Insert();
                    int result2 = warehouse();
                    int result3 = 0;
                    if(Convert.ToInt32(tbFquan.Text) > 0)
                    {
                        result3 = fail();
                    }
                    if (result == 1 && result1 == 1 && result2 == 1)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alert",
                        "<script>$(document).ready(function(){ $('.alert-error').hide(); $('.alert-success').show(); $('.alert-warning').hide(); });</script>");
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(GetType(), "alert",
                        "<script>$(document).ready(function(){ $('.alert-success').hide(); $('.alert-error').show(); $('.alert-warning').hide(); });</script>");
                    }
                }
            }
            else if(quantity > sum)
            {
                lbError.Visible = true;
                lbError.Text = "Sum Of Passed&Failed Quantity Is Less Than The Total Quantity";
                lbWarning.Text = "Sum Of Passed&Failed Quantity Is Less Than The Total Quantity";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                      "<script type = 'text/javascript'>window.onload=function(){ $('.alert-success').hide(); $('.alert-error').hide(); $('.alert-warning').show(); setTimeout(function(){ $('.alert-warning').hide('fade');},30000); };</script>");
                
            }
            else if (quantity < sum)
            {
                lbError.Visible = true;
                lbError.Text = "Sum Of Passed&Failed Quantity Is Greater Than The Total Quantity";
                lbWarning.Text = "Sum Of Passed&Failed Quantity Is Greater Than The Total Quantity";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "<script type = 'text/javascript'>window.onload=function(){ $('.alert-success').hide(); $('.alert-error').hide(); $('.alert-warning').show(); setTimeout(function(){ $('.alert-warning').hide('fade');},30000); };</script>");
            }

        }
        public int Insert()
        {
            int a = 0;
            con.OpenConection();
            con.ExecSqlQuery("INSERT INTO activity_stock_raw(purchaseorder,empno,activity,date,time)VALUES(@po,@en,@act,@rdate,@t)");
            con.Cmd.Parameters.AddWithValue("@po", Request.Form.Get("po").ToString());
            con.Cmd.Parameters.AddWithValue("@rdate", DateTime.Parse(date));
            con.Cmd.Parameters.AddWithValue("@en", sessionempno);
            con.Cmd.Parameters.AddWithValue("@act", "Dispatch Raw Materials");
            con.Cmd.Parameters.AddWithValue("@t", DateTime.Parse(time));
            a = con.Cmd.ExecuteNonQuery();
            con.CloseConnection();
            return a;
        }
        public int fail()
        {
            int a = 0,q=0;
            int quantity = Convert.ToInt32(Request.Form.Get("qty").ToString());
            int comp = Convert.ToInt32(tbFquan.Text);
            con.OpenConection();
            con.ExecSqlQuery("Select count(*) from stock_raw_fail where purchaseorder = @sid");
            con.Cmd.Parameters.AddWithValue("@sid", Request.Form.Get("po").ToString().Trim());
            int b = Convert.ToInt32(con.Cmd.ExecuteScalar());
            con.CloseConnection();
            if (comp > 0 && comp <= quantity)
            {
                con.OpenConection();
                con.ExecSqlQuery("Select * from stock_raw_fail where purchaseorder = @sid");
                con.Cmd.Parameters.AddWithValue("@sid", Request.Form.Get("po").ToString());
                con._dr = con.Cmd.ExecuteReader();
                while (con._dr.Read())
                {
                    q = Convert.ToInt32(con._dr["quantity"].ToString());
                }
                con.CloseConnection();
                int add = comp + q;
                if (b == 1)
                {
                    con.OpenConection();
                    con.ExecSqlQuery("UPDATE stock_raw_fail set quantity=@quan,date=@rdate where purchaseorder=@po");
                    con.Cmd.Parameters.AddWithValue("@quan", add);
                    con.Cmd.Parameters.AddWithValue("@rdate", DateTime.Parse(date));
                    con.Cmd.Parameters.AddWithValue("@po", Request.Form.Get("po").ToString());
                    a = con.Cmd.ExecuteNonQuery();
                    con.CloseConnection();
                }
                else
                {
                    con.OpenConection();
                    con.ExecSqlQuery("INSERT INTO stock_raw_fail(purchaseorder,quantity,fixquantity,scrapquantity,returnquantity,date,status)VALUES(@po,@quan,@fix,@scrap,@return,@date,@st)");
                    con.Cmd.Parameters.AddWithValue("@po", Request.Form.Get("po").ToString());
                    con.Cmd.Parameters.AddWithValue("@quan", tbFquan.Text);
                    con.Cmd.Parameters.AddWithValue("@fix", 0);
                    con.Cmd.Parameters.AddWithValue("@scrap", 0); 
                    con.Cmd.Parameters.AddWithValue("@return", 0);
                    con.Cmd.Parameters.AddWithValue("@date", DateTime.Parse(date));
                    con.Cmd.Parameters.AddWithValue("@st", "Hold");
                    a = con.Cmd.ExecuteNonQuery();
                    con.CloseConnection();
                }
            }
            return a;
        }
        public int warehouse()
        {
            int q = 0, a = 0, b = 0;
            int quantity = Convert.ToInt32(Request.Form.Get("qty").ToString());
            int comp = Convert.ToInt32(tbPquan.Text);
            con.OpenConection();
            con.ExecSqlQuery("Select count(*) from stock_warehouse where purchaseorder = @sid");
            con.Cmd.Parameters.AddWithValue("@sid", Request.Form.Get("po").ToString().Trim());
            b = Convert.ToInt32(con.Cmd.ExecuteScalar());
            con.CloseConnection();
            if (b == 1)
            {
                con.OpenConection();
                con.ExecSqlQuery("Select * from stock_warehouse where purchaseorder = @sid");
                con.Cmd.Parameters.AddWithValue("@sid", Request.Form.Get("po").ToString());
                con._dr = con.Cmd.ExecuteReader();
                while (con._dr.Read())
                {
                    q = Convert.ToInt32(con._dr["quantity"].ToString());
                }
                con.CloseConnection();
                int add = comp + q;
                if (comp >= 0 && comp <= quantity)
                {
                    con.OpenConection();
                    con.ExecSqlQuery("UPDATE stock_warehouse set quantity=@quan,receivedate=@rdate where purchaseorder=@po");
                    con.Cmd.Parameters.AddWithValue("@quan", add);
                    con.Cmd.Parameters.AddWithValue("@rdate", DateTime.Parse(date));
                    con.Cmd.Parameters.AddWithValue("@po", Request.Form.Get("po").ToString());
                    a = con.Cmd.ExecuteNonQuery();
                    con.CloseConnection();
                }
            }
            else
            {
                if (comp >= 0 && comp <= quantity)
                {
                    con.OpenConection();
                    con.ExecSqlQuery("INSERT INTO stock_warehouse(purchaseorder,quantity,receivedate,status,code)VALUES(@po,@quan,@rdate,@st,@c)");
                    con.Cmd.Parameters.AddWithValue("@po", Request.Form.Get("po").ToString());
                    con.Cmd.Parameters.AddWithValue("@quan", comp);
                    con.Cmd.Parameters.AddWithValue("@rdate", DateTime.Parse(date));
                    con.Cmd.Parameters.AddWithValue("@st", "In Stock");
                    con.Cmd.Parameters.AddWithValue("@c", 0);
                    a = con.Cmd.ExecuteNonQuery();
                    con.CloseConnection();
                }
            }
            return a;
        }

        protected void tbFquan_TextChanged(object sender, EventArgs e)
        {
            int total = Convert.ToInt32(Request.Form.Get("qty").ToString());
            int compute = Convert.ToInt32(Request.Form.Get("qty").ToString()) - Convert.ToInt32(tbFquan.Text);
            if (total >= Convert.ToInt32(tbFquan.Text) && Convert.ToInt32(tbFquan.Text) >= 0)
            {
                tbFquan.BorderColor = System.Drawing.Color.Green;
                tbPquan.BorderColor = System.Drawing.Color.Green;
                //lbError.ForeColor = System.Drawing.Color.Red;
                lbError.Visible = false;
                tbPquan.Text = compute.ToString();
            }
            else if (total < Convert.ToInt32(tbFquan.Text))
            {
                tbFquan.BorderColor = System.Drawing.Color.Red;
                tbPquan.BorderColor = System.Drawing.Color.Red;
                lbError.ForeColor = System.Drawing.Color.Red;
                tbPquan.Text = "0";
                lbError.Visible = true;
                lbError.Text = "Failed Quantity Is Greater Than The Total Quantity";
            }
        }

        protected void tbPquan_TextChanged(object sender, EventArgs e)
        {
            int total = Convert.ToInt32(Request.Form.Get("qty").ToString());
            int compute = Convert.ToInt32(Request.Form.Get("qty").ToString()) - Convert.ToInt32(tbPquan.Text);
            if (total >= Convert.ToInt32(tbPquan.Text) && Convert.ToInt32(tbPquan.Text) >= 0)
            {
                tbPquan.BorderColor = System.Drawing.Color.Green;
                tbFquan.BorderColor = System.Drawing.Color.Green;
                lbError.Visible = false;
                //lbError.ForeColor = System.Drawing.Color.Red;
                tbFquan.Text = compute.ToString();
            }
            else if (total < Convert.ToInt32(tbPquan.Text))
            {
                tbFquan.Text = "0";
                tbPquan.BorderColor = System.Drawing.Color.Red;
                tbFquan.BorderColor = System.Drawing.Color.Red;
                //tbFquan.ForeColor = System.Drawing.Color.Black;
                //tbPquan.ForeColor = System.Drawing.Color.Black;
                lbError.ForeColor = System.Drawing.Color.Red;
                lbError.Visible = true;
                lbError.Text = "Passed Quantity Is Greater Than The Total Quantity";
            }
        }
    }
}