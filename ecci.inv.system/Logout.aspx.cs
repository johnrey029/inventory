using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecci.inv.system
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            Session.Abandon();
            Session.Clear();
            System.Web.Security.FormsAuthentication.SignOut();
            if (Session["empnumber"] == null)
            {
                Response.Redirect("~/default.aspx");
            }

        }
    }
}