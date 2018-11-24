using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecci.inv.system.sales
{
    public partial class Sales : System.Web.UI.MasterPage
    {
        private string sessionempno { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["empnumber"] != null)
            {
                lblEmpno.Text = Session["empnumber"].ToString();
            }
            else
            {
                Session.Clear();
                Response.Redirect("~/default.aspx");
            }
        }
    }
}