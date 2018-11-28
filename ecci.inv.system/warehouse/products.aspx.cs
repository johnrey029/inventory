using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecci.inv.system.warehouse
{
    public partial class products : System.Web.UI.Page
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
            //con.ExecSqlQuery("Select * from purchasedorder");,u.pname, c.name

           // INNER JOIN product u ON sp.productid = u.productid
           /// INNER JOIN purchaseorder p ON sp.orderid = p.orderid
           // INNER JOIN client c ON p.clientid = c.clientid
            con.ExecSqlQuery(@"SELECT * FROM stock_product
            where status = '" + "Ready" + "' ");
            GridView1.DataSource = con.DataQueryExec();
            GridView1.DataBind();
            if (GridView1.Rows.Count > 0)
            {
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            con.CloseConnection();
        }

        public string MyNewRow(object unqid)
        {
            return String.Format(@"</td></tr><tr id='tr{0}' class='collapsed-row'>
            <td></td> <td colspan='100' style='padding:0px; margin:0px;'>", unqid);
            //<button class='btn btn-primary glyphicon-envelope' style='height: 50px; width: 50px;'></button>
        }

    }
}