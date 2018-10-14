using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecci.inv.system.sales
{
    public partial class report : System.Web.UI.Page
    {
        private string sessionempno { get; set; }
        DBConnection con;
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            con = new DBConnection();
            if (!IsPostBack)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); $('.alert-warning').hide();});</script>");
                if (Session["empnumber"] != null)
                {
                    sessionempno = Session["empnumber"].ToString();
                }

            }
        }
    }
}