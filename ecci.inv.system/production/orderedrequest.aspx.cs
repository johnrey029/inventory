using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
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
          //  BindPopUp();
        }
        private void BindPopUp()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("Brandname"), new DataColumn("Price"), new DataColumn("Total Quantity") });
            GridView3.DataSource = dt;
            GridView3.DataBind();
        }
        private void BindGridView()
        {
            con.OpenConection();
            //con.ExecSqlQuery("Select * from purchasedorder");
            con.ExecSqlQuery(@"SELECT s.status,s.date,c.name, s.orderid, i.productid,Convert(VARCHAR(19),i.productid) + 'c' + Convert(VARCHAR(50),s.orderid) AS uniqueid,
            i.price,i.quantityordered, i.amount, u.pname FROM purchaseorder s
            INNER JOIN oderdetails i ON s.orderid = i.orderid
            INNER JOIN client c ON s.clientid = c.clientid
            INNER JOIN product u ON i.productid = u.productid");
            GridView1.DataSource = con.DataQueryExec();
            GridView1.DataBind();
            con.CloseConnection();
        }

        //[WebMethod(enableSession: true)]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ConfirmUpdate()
        { 
           // BindPopUp();
            try
            {
                string id = Convert.ToString(Request.Form.Get("hiddenStockId").ToString());
                int pos = 0;
                string pid = "";
                string oid = "";
                pos = id.IndexOf("c");
                pid = id.Substring(0, pos);
                oid = id.Substring(pos + 1, id.Length - (pos + 1));
                con.OpenConection();
                con.ExecSqlQuery(
                @"SELECT  i.quantityordered,
                u.price,t.brandname FROM oderdetails i
                INNER JOIN productitems u ON i.productid = u.productid
                INNER JOIN items t ON u.itemsid = t.itemsid
                where i.productid = '" + Convert.ToInt32(pid) + "' and i.orderid ='" + Convert.ToInt64(oid) + "' ");
                GridView3.DataSource = con.DataQueryExec();
                GridView3.DataBind();
                con.CloseConnection();
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Pop", "openModal();",true);
            }catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
        public string MyNewRow(object unqid)
        {
            return String.Format(@"</td></tr><tr id='tr{0}' class='collapsed-row'>
            <td></td> <td colspan='100' style='padding:0px; margin:0px;'>", unqid);
            //<button class='btn btn-primary glyphicon-envelope' style='height: 50px; width: 50px;'></button>
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int pos = 0;
            string pid = "";
            string oid = "";
            // int ito = 9;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string requestedid = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
                if (requestedid.Length > 0)
                {
                    pos = requestedid.IndexOf("c");
                    //ito = requestedid.IndexOf("r");
                    pid = requestedid.Substring(0, pos);
                    oid = requestedid.Substring(pos+1, requestedid.Length-(pos+1));
                    //Convert(VARCHAR(19), i.productid) + 'j' + Convert(VARCHAR(50), i.orderid) AS idunique,
                    con.OpenConection();
                    GridView gv = (GridView)e.Row.FindControl("GridView2");
                    con.ExecSqlQuery(@"SELECT  i.quantityordered,
                    u.price,t.brandname FROM oderdetails i
                    INNER JOIN productitems u ON i.productid = u.productid
                    INNER JOIN items t ON u.itemsid = t.itemsid
                    where i.productid = '" + Convert.ToInt32(pid) + "' and i.orderid ='" + Convert.ToInt64(oid) + "' ");
                    gv.DataSource = con.DataQueryExec();
                    gv.DataBind();
                    con.CloseConnection();
                }
            }
        }

        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void button_Click(object sender, EventArgs e)
        {
            string id = Convert.ToString(Request.Form.Get("hiddenStockId").ToString());
        }
    }
}