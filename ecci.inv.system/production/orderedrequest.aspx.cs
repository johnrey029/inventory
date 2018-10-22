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
                BindGridView();
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); $('.alert-warning').hide();});</script>");
            }

        }
        private void BindGridView()
        {
            con.OpenConection();
            //con.ExecSqlQuery("Select * from purchasedorder");
            con.ExecSqlQuery(@"SELECT s.status,s.date,c.name, s.orderid, i.productid,Convert(VARCHAR(19),i.productid) + 'c' + Convert(VARCHAR(19),s.orderid) AS uniqueid,
            i.price,i.quantityordered, i.amount, u.pname FROM purchaseorder s
            INNER JOIN oderdetails i ON s.orderid = i.orderid
            INNER JOIN client c ON s.clientid = c.clientid
            INNER JOIN product u ON i.productid = u.productid");
            GridView1.DataSource = con.DataQueryExec();
            GridView1.DataBind();
            con.CloseConnection();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int pos = 2;
            int ito = 9;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string requestedid = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
                //pos = requestedid.IndexOf("j");
                //ito = requestedid.IndexOf("r");
                //string pid = requestedid.Substring(0, pos-1);
                //string oid = requestedid.Substring(pos, ito-1);
                con.OpenConection();
                GridView gv = (GridView)e.Row.FindControl("GridView2");
                con.ExecSqlQuery(@"SELECT  i.quantityordered,Convert(VARCHAR(19),i.productid) +'j' + Convert(VARCHAR(19),i.orderid) +'r' AS idunique,
                u.price,t.brandname FROM oderdetails i
                INNER JOIN productitems u ON i.productid = u.productid
                INNER JOIN items t ON u.itemsid = t.itemsid");
                //where i.productid = '" + pid + "' and i.orderid ='" + oid + "' 
                gv.DataSource = con.DataQueryExec();
                gv.DataBind();
                con.CloseConnection();
            }
        }
    }
}