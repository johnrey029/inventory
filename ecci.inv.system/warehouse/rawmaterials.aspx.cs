using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecci.inv.system.warehouse
{
    public partial class rawmaterials : System.Web.UI.Page
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int sid = Convert.ToInt32(Request.Form.Get("hiddenStockId").ToString());
            totalquantity = Convert.ToInt32(Request.Form.Get("hiddenquantity").ToString());
            if (totalquantity >= Convert.ToInt32(qty.Text) && Convert.ToInt32(qty.Text) != 0)
            {
                WarehouseRaw.WarehouseRawSoapClient client = new WarehouseRaw.WarehouseRawSoapClient("WarehouseRawSoap");
               // int result1 = InsertById();
                int result = client.UpdateDispatch(sid);
                if (result == 1) //&& result1 == 1)
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
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                       "<script type = 'text/javascript'>window.onload=function(){ $('.alert-success').hide(); $('.alert-error').hide(); $('.alert-warning').show(); setTimeout(function(){ $('.alert-warning').hide('fade');},30000); };</script>");
                }
                else if (Convert.ToInt32(qty.Text) > totalquantity)
                {
                    string message = qty.Text + " Is Greater Than The Total Quantity " + totalquantity.ToString();
                    lbWarning.Text = message;
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                       "<script type = 'text/javascript'>window.onload=function(){ $('.alert-success').hide(); $('.alert-error').hide(); $('.alert-warning').show(); setTimeout(function(){ $('.alert-warning').hide('fade');},30000); };</script>");
                }
            }
        }
    }
}