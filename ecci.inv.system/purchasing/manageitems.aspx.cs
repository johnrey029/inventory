using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecci.inv.system.purchasing
{
    public partial class manageitems : System.Web.UI.Page
    {
        private string sessionempno { get; set; }
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

        //protected void btnAddItems_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("items.aspx");
        //}
    }
}