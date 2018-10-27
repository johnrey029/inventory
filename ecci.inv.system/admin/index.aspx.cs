using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecci.inv.system.admin
{
    public partial class index : System.Web.UI.Page
    {
        private string sessionempno { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["empnumber"] != null)
            {
                sessionempno = Session["empnumber"].ToString();
            }
            else
            {
                Session.Clear();
                Response.Redirect("~/default.aspx");
            }
        }
    }
}