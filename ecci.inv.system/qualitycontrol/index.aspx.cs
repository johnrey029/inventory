using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecci.inv.system.qualitycontrol
{
    public partial class index : System.Web.UI.Page
    {
        private string sessionempno { get; set; }
        DBConnection con;
        int totalquantity;
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
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); $('.alert-warning').hide();});</script>");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int sid = Convert.ToInt32(Request.Form.Get("hiddenStockId").ToString());
            totalquantity = Convert.ToInt32(Request.Form.Get("hiddenquantity").ToString());
            if (totalquantity >= Convert.ToInt32(qty.Text) && Convert.ToInt32(qty.Text) != 0)
            {
                DeliveryService.OrderDeliveryServiceSoapClient client = new DeliveryService.OrderDeliveryServiceSoapClient("OrderDeliveryServiceSoap");
                int result1 = InsertById();
                int result = client.UpdateById(sid, Convert.ToInt32(qty.Text));
                if (result == 1 && result1 == 1)
                {
                    //Session["sucess"] = "Tama";
                    //  UpdatePanel2.ValidateRequestMode = ValidateRequestMode.Disabled;
                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "$('#updateModal').modal('hide');", true);
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "<script>$(document).ready(function(){ $('.alert-error').hide(); $('.alert-success').show(); $('.alert-warning').hide(); });</script>");
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "<script>$(document).ready(function(){ $('.alert-success').hide(); $('.alert-error').show(); $('.alert-warning').hide(); });</script>");
                    //Session["sucess"] = "Mali";
                }
            }
            else
            {
                if (Convert.ToInt32(qty.Text) == 0)
                {
                    string message = "Input Must Be Greater Than 0.";
                    lbWarning.Text = message;
                    //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    //sb.Append("<script type = 'text/javascript'>");
                    //sb.Append("window.onload=function(){");
                    //sb.Append("$('.alert-success').hide(); $('.alert-error').hide(); $('.alert-warning').show();");
                    //sb.Append("$('.alert-warning').html('<strong>");
                    //sb.Append(strong);
                    //sb.Append("</strong> ");
                    //sb.Append(message);
                    //sb.Append("<strong> Try Again!</strong>');");
                    //sb.Append("setTimeout(function(){ $('.alert-warning').hide('fade');},60000);");
                    //sb.Append("};");
                    //sb.Append("</script>");
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                       "<script type = 'text/javascript'>window.onload=function(){ $('.alert-success').hide(); $('.alert-error').hide(); $('.alert-warning').show(); setTimeout(function(){ $('.alert-warning').hide('fade');},30000); };</script>");
                    //qty.BorderColor = System.Drawing.Color.Red;
                    //lbError.Visible = true;
                    //lbError.ForeColor = System.Drawing.Color.Red;
                    //lbError.Text = "Incorrect input! Value must be greater than 0.";
                    // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "$('#updateModal').modal('show');", true);
                }
                else if(Convert.ToInt32(qty.Text)>totalquantity)
                {
                    string message = qty.Text + " Is Greater Than The Total Quantity " + totalquantity.ToString();
                    lbWarning.Text = message;
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                       "<script type = 'text/javascript'>window.onload=function(){ $('.alert-success').hide(); $('.alert-error').hide(); $('.alert-warning').show(); setTimeout(function(){ $('.alert-warning').hide('fade');},30000); };</script>");
                    
                    //System.Text.StringBuilder sb = new System.Text.StringBuilder(); $('.alert-warning').innerHTML('Wrong Input! Input Must Not Be Greater Than The Total Quantity');
                    //sb.Append("<script type = 'text/javascript'>");
                    //sb.Append("window.onload=function(){");
                    //sb.Append("$('.alert-success').hide(); $('.alert-error').hide(); $('.alert-warning').show();");
                    //sb.Append("$('.alert-warning').html('<strong>");
                    //sb.Append(strong);
                    //sb.Append("</strong> ");
                    //sb.Append(message);
                    //sb.Append("<strong> Try Again!</strong>');");
                    //sb.Append("setTimeout(function(){ $('.alert-warning').hide('fade');},10000);");
                    //sb.Append("};");
                    //sb.Append("</script>");
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                }
            }
        }
        public int InsertById()
        {
            //ToString("MMMM dd yyyy hh:mm tt")
            //int item = itemsid(); itemsid, quantity, purchasedate, deliverydate, postatus,receivedate, @item, @quan, @pdate, @ddate, @stat,@rdate
            string date = DateTime.Now.ToShortDateString();
            //DateTime pdate = DateTime.Parse(Request.Form.Get("pdate").ToString());
            //DateTime ddate = DateTime.Parse(Request.Form.Get("ddate").ToString());
            string time = DateTime.Now.ToLongTimeString();
            con.OpenConection();
            con.ExecSqlQuery("INSERT INTO activity_stock_raw(purchaseorder,empno,activity,date,time,remarks)VALUES(@po,@en,@act,@rdate,@t,@rem)");
            con.Cmd.Parameters.AddWithValue("@po", Request.Form.Get("po").ToString());
            //con.Cmd.Parameters.AddWithValue("@item", item);
            //con.Cmd.Parameters.AddWithValue("@quan", Request.Form.Get("qty").ToString());
            //con.Cmd.Parameters.AddWithValue("@pdate", pdate.ToShortDateString());
            //con.Cmd.Parameters.AddWithValue("@ddate", ddate.ToShortDateString());
            con.Cmd.Parameters.AddWithValue("@rdate", DateTime.Parse(date));
            //con.Cmd.Parameters.AddWithValue("@stat", "Received");
            con.Cmd.Parameters.AddWithValue("@en", sessionempno);
            con.Cmd.Parameters.AddWithValue("@act", "Received");
            con.Cmd.Parameters.AddWithValue("@t", DateTime.Parse(time));
            con.Cmd.Parameters.AddWithValue("@rem", "Update");
            int a = con.Cmd.ExecuteNonQuery();
            con.CloseConnection();
            if (a == 0)
            {
                //Page.ClientScript.RegisterClientScriptBlock(GetType(), "alert",
                //"<script>$(document).ready(function(){ $('.alert-success').hide(); $('.alert-error').show(); });</script>");
            }
            else
            {
                //Page.ClientScript.RegisterClientScriptBlock(GetType(), "alert",
                //"<script>$(document).ready(function(){ $('.alert-error').hide(); $('.alert-success').show(); });</script>");
            }
            return a;
        }

        protected void qty_TextChanged(object sender, EventArgs e)
        {
            qty.ForeColor = System.Drawing.Color.Black;
            totalquantity = Convert.ToInt32(Request.Form.Get("hiddenquantity").ToString());
            if (Convert.ToInt32(qty.Text) == 0)
            {
                qty.Text = 0.ToString();
                qty.BorderColor = System.Drawing.Color.Red;
                lbError.Visible = true;
                lbError.ForeColor = System.Drawing.Color.Red;
                lbError.Text = "Incorrect Input! Value Must Be Greater Than 0.";
            }
            else
            {
                int diff = totalquantity - Convert.ToInt32(qty.Text);
                if (totalquantity > Convert.ToInt32(qty.Text))
                {
                    qty.BorderColor = System.Drawing.Color.Blue;
                    lbError.Visible = true;
                    lbError.ForeColor = System.Drawing.Color.Blue;
                    lbError.Text = "Receiving: " + qty.Text + "  Out of Total Quantity: " + totalquantity.ToString()
                        + "  Remaining Quantity Receivable: " + diff;
                }
                else if (totalquantity == Convert.ToInt32(qty.Text))
                {
                    qty.BorderColor = System.Drawing.Color.Green;
                    lbError.Visible = true;
                    lbError.ForeColor = System.Drawing.Color.Green;
                    lbError.Text = "Receiving Total Quantity";

                }
                else if (totalquantity < Convert.ToInt32(qty.Text))
                {
                    lbError.Visible = true;
                    lbError.ForeColor = System.Drawing.Color.Red;
                    lbError.Text = "Input Quantity: " + Convert.ToInt32(qty.Text)
                        + " Is Greater Than Expected Total Quantity " + totalquantity;
                    qty.BorderColor = System.Drawing.Color.Red;
                }
            }

        }
    }
}