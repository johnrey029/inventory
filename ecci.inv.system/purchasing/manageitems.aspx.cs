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
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (!IsPostBack)
            {
                lbError.Visible = false;
                //load();
            }
        }

        //protected void btnAddItems_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("items.aspx");
        //}
    }
}