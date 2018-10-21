using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecci.inv.system.production
{
    public partial class orderedrequest : System.Web.UI.Page
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
            con = new DBConnection();
            if (!IsPostBack)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); $('.alert-warning').hide();});</script>");
            }
            BindGridView();

        }
        private void BindGridView()
        {
            con.OpenConection();
            //con.ExecSqlQuery("Select * from purchasedorder");
            con.ExecSqlQuery(@"SELECT s.status,s.date,c.name, s.orderid, i.productid,
            i.price,i.quantityordered, i.amount, u.pname FROM purchaseorder s
            INNER JOIN oderdetails i ON s.orderid = i.orderid
            INNER JOIN client c ON s.clientid = c.clientid
            INNER JOIN product u ON i.productid = u.productid");
            GridView1.DataSource = con.DataQueryExec();
            GridView1.DataBind();
            con.CloseConnection();
        }
    }
}