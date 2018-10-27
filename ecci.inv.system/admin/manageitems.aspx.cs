using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecci.inv.system.admin
{
    public partial class manageitems : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                lbError.Visible = false;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int iid = Convert.ToInt32(Request.Form.Get("hiddenItemsId").ToString());
            //Label1.Text = iid.ToString();
            ManageItemService.ManageItemServiceSoapClient itemsSC = new ManageItemService.ManageItemServiceSoapClient("ManageItemServiceSoap");
            int result1 = activityItem();
            int result2 = itemsSC.updateItemById(iid, Convert.ToDecimal(tbNewUnitPrice.Text));
            if (result1 == 1 && result2 == 1)
            {
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
        public int activityItem()
        {
            int iid = Convert.ToInt32(Request.Form.Get("hiddenItemsId").ToString());
            int a = 0;

            con = new DBConnection();
            con.OpenConection();
            con.ExecSqlQuery("INSERT INTO activity_items(act_empno, act_itemsid, act_brandname, act_unitprice, act_remarks)VALUES(@empno, @itemsid, @brandname, @unitprice, @remarks)");
            con.Cmd.Parameters.AddWithValue("@itemsid", iid);
            con.Cmd.Parameters.AddWithValue("@empno", sessionempno);
            con.Cmd.Parameters.AddWithValue("@brandname", Request.Form.Get("brandname").ToString());
            con.Cmd.Parameters.AddWithValue("@unitprice", tbNewUnitPrice.Text);
            con.Cmd.Parameters.AddWithValue("@remarks", "Changed Price");
            a = con.Cmd.ExecuteNonQuery();
            con.CloseConnection();

            return a;
        }
    }
}