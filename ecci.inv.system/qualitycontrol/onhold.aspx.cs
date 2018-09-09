using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecci.inv.system.qualitycontrol
{
    public partial class onhold : System.Web.UI.Page
    {
        private string sessionempno { get; set; }
        DBConnection con;
        bool s = false, r = false, f = false;
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
            else
            {

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int sid = Convert.ToInt32(Request.Form.Get("hiddenStockId").ToString());
            int quantity = Convert.ToInt32(Request.Form.Get("qty").ToString());
            int dp = Convert.ToInt32(Request.Form.Get("hiddenquantity").ToString());
            int sum = Convert.ToInt32(tbReturn.Text) + Convert.ToInt32(tbRework.Text) + Convert.ToInt32(tbScrap.Text);
            if (quantity == sum)
            {
                if (Convert.ToInt32(tbReturn.Text) >= 0 && Convert.ToInt32(tbRework.Text) >= 0)
                {
                    OnHoldService.OnHoldServiceSoapClient client = new OnHoldService.OnHoldServiceSoapClient("OnHoldServiceSoap");
                    int result = client.UpdateDispatch(sid, Convert.ToInt32(tbRework.Text),Convert.ToInt32(tbReturn.Text), Convert.ToInt32(tbScrap.Text));
                    if (result == 1 )//&& result1 == 1 && result2 == 1)
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
            else if (quantity > sum)
            {
                //lbError.Visible = true;
                //lbError.Text = "Sum of Rework, Return and Scrap Are Less Than The Total Quantity";
                lbWarning.Text = "Sum of Rework, Return and Scrap Are Less Than The Total Quantity";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                      "<script type = 'text/javascript'>window.onload=function(){ $('.alert-success').hide(); $('.alert-error').hide(); $('.alert-warning').show(); setTimeout(function(){ $('.alert-warning').hide('fade');},30000); };</script>");

            }
            else if (quantity < sum)
            {
                //lbError.Visible = true;
                //lbError.Text = "Sum of Rework, Return and Scrap Are Greater Than The Total Quantity";
                lbWarning.Text = "Sum of Rework, Return and Scrap Are Greater Than The Total Quantity";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "<script type = 'text/javascript'>window.onload=function(){ $('.alert-success').hide(); $('.alert-error').hide(); $('.alert-warning').show(); setTimeout(function(){ $('.alert-warning').hide('fade');},30000); };</script>");
            }
        }

        protected void tbRework_TextChanged(object sender, EventArgs e)
        {
            if (tbReturn.Text.Trim().Length>0 && tbScrap.Text.Trim().Length > 0 && tbRework.Text.Trim().Length > 0)
            {
                lbError.Visible = true;
                int total = Convert.ToInt32(Request.Form.Get("qty").ToString());
                int sum = Convert.ToInt32(tbReturn.Text.Trim()) + Convert.ToInt32(tbRework.Text.Trim()) + Convert.ToInt32(tbScrap.Text.Trim());
                int computed = total - sum;
                if (total == sum)
                {
                    lbError.Text = "Total Quantity Equal Confirmed.";
                    lbError.ForeColor = System.Drawing.Color.DarkGreen;
                    btnSave.Focus();
                }
                else if (total > sum)
                {
                    lbError.Text = "Insufficient Sum Required. Balance = " + computed;
                    lbError.ForeColor = System.Drawing.Color.OrangeRed;
                    tbReturn.Focus();
                }
                else if (total < sum)
                {
                    lbError.Text = "Exceeded Sum Required. Excess = " + computed;
                    lbError.ForeColor = System.Drawing.Color.Red;
                    tbRework.Focus();
                }
                //  tbRework.EnableViewState = false;
            }else
            {
                lbError.ForeColor = System.Drawing.Color.DarkGreen;
                lbError.Text = "Please Fill The Quantities Needed";
                tbReturn.ReadOnly = false;
                tbScrap.ReadOnly = true;
                tbReturn.Focus();
            }
        }

        protected void tbReturn_TextChanged(object sender, EventArgs e)
        {
            if (tbRework.Text.Trim().Length > 0 && tbScrap.Text.Trim().Length > 0 && tbReturn.Text.Trim().Length > 0)
            {
                lbError.Visible = true;
                int total = Convert.ToInt32(Request.Form.Get("qty").ToString());
                int sum = Convert.ToInt32(tbReturn.Text.Trim()) + Convert.ToInt32(tbRework.Text.Trim()) + Convert.ToInt32(tbScrap.Text.Trim());
                int computed = total - sum;
                if (total == sum)
                {
                    lbError.Text = "Total Quantity Equal Confirmed.";
                    lbError.ForeColor = System.Drawing.Color.DarkGreen;
                    btnSave.Focus();
                }
                else if (total > sum)
                {
                    lbError.Text = "Insuffient Sum Required. Balance = " + computed;
                    lbError.ForeColor = System.Drawing.Color.OrangeRed;
                    tbScrap.Focus();
                }
                else if (total < sum)
                {
                    lbError.Text = "Exceeded Sum Required. Excess = " + computed;
                    lbError.ForeColor = System.Drawing.Color.Red;
                    tbReturn.Focus();
                }
                //  tbReturn.EnableViewState = false;
            }
            else
            {
                lbError.ForeColor = System.Drawing.Color.DarkGreen;
                lbError.Text = "Please Fill The Quantities Needed";
                tbScrap.ReadOnly = false;
                tbScrap.Focus();
            }
        }

        protected void tbScrap_TextChanged(object sender, EventArgs e)
        {
            //if (tbRework.Text == string.Empty)
            //{
            //    tbRework.Text = "0";
            //}
            //if (tbReturn.Text == string.Empty)
            //{
            //    tbReturn.Text = "0";
            //}
            if (tbReturn.Text.Trim().Length > 0 && tbRework.Text.Trim().Length > 0 && tbScrap.Text.Trim().Length > 0)
            {
                lbError.Visible = true;
                int total = Convert.ToInt32(Request.Form.Get("qty").ToString());
                int sum = Convert.ToInt32(tbReturn.Text.Trim()) + Convert.ToInt32(tbRework.Text.Trim()) + Convert.ToInt32(tbScrap.Text.Trim());
                int computed = total - sum;
                //if (tbRework.Text == "0")
                //{
                //    tbRework.Text = null;
                //}
                //if (tbReturn.Text == "0")
                //{
                //    tbReturn.Text = null;
                //}
                if (total == sum)
                {
                    lbError.Text = "Total Quantity Equal Confirmed.";
                    lbError.ForeColor = System.Drawing.Color.DarkGreen;
                    btnSave.Focus();
                }
                else if (total > sum)
                {
                    lbError.Text = "Insuffient Sum Required. Balance = " + computed;
                    lbError.ForeColor = System.Drawing.Color.OrangeRed;
                    tbRework.Focus();
                }
                else if (total < sum)
                {
                    lbError.Text = "Exceeded Sum Required. Excess = " + computed;
                    lbError.ForeColor = System.Drawing.Color.Red;
                    tbScrap.Focus();
                }
               //   tbScrap.EnableViewState = false;
            }
            else
            {
                lbError.ForeColor = System.Drawing.Color.DarkGreen;
                lbError.Text = "Please Fill The Quantities Needed";
                btnSave.Focus();
            }
        }
    }
}