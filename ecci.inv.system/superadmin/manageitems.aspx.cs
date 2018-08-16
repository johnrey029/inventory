using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecci.inv.system.superadmin
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
            Label1.Text = iid.ToString();
            ManageItemService.ManageItemServiceSoapClient itemsSC = new ManageItemService.ManageItemServiceSoapClient("ManageItemServiceSoap");
            //int result1 = insertItemActivity();
            int result2 = itemsSC.updateItemById(iid, Convert.ToDecimal(tbNewUnitPrice.Text));
            if (result2 == 1)
                //&& result2 == 1)
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
        public int insertItemActivity()
        { 
            con = new DBConnection();
            con.OpenConection();
            con.ExecSqlQuery("UPDATE ");
            con.Cmd.Parameters.AddWithValue("@po", Request.Form.Get("po").ToString());


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
    }
}