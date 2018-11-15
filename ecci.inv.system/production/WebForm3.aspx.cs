using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecci.inv.system.production
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        private string sessionempno { get; set; }
        DBConnection con;
        private bool contin = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["empnumber"] != null)
            {
                sessionempno = Session["empnumber"].ToString();
            }
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            con = new DBConnection();
            if (Request.Cookies["CookieName"] != null)
            {
                Label1.Text = Server.HtmlEncode(Request.Cookies["CookieName"].Value);
            }
            BindGridView();
        }
        private void BindGridView()
        {
            string id = Label1.Text;
            int pos = 0;
            string pid = "";
            string oid = "";
            pos = id.IndexOf("c");
            pid = id.Substring(0, pos);
            oid = id.Substring(pos + 1, id.Length - (pos + 1));
            con.OpenConection();
            con.ExecSqlQuery(
            @"SELECT  i.quantityordered * u.quantity as required, u.itemsid,Convert(VARCHAR(19),i.quantityordered) + 'c' + Convert(VARCHAR(50),u.itemsid) AS uniqueid,
                u.price,t.brandname, i.quantityordered FROM oderdetails i
                INNER JOIN productitems u ON i.productid = u.productid
                INNER JOIN items t ON u.itemsid = t.itemsid
                where i.productid = '" + Convert.ToInt32(pid) + "' and i.orderid ='" + Convert.ToInt64(oid) + "' ");
            GridView1.DataSource = con.DataQueryExec();
            GridView1.DataBind();
            con.CloseConnection();
        }
        public string MyNewRow(object unqid)
        {
            return String.Format(@"</td></tr><tr id='tr{0}' class='collapsed-row'>
            <td>LIST OF AVAILABLE PO #</td> <td colspan='100' style='padding:0px; margin:0px;'>", unqid);
            //<button class='btn btn-primary glyphicon-envelope' style='height: 50px; width: 50px;'></button>
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int total = 0;
            int count = 1;
            int pos = 0;
            string qty = "";
            string itemsid = "";
            int status = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string requestedid = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();

                if (requestedid.Length > 0)
                {
                    GridView gv = (GridView)e.Row.FindControl("GridView2");
                    pos = requestedid.IndexOf("c");
                    qty = requestedid.Substring(0, pos);
                    itemsid = requestedid.Substring(pos + 1, requestedid.Length - (pos + 1));
                    do
                    {

                        con.OpenConection();
                        con._dr = con.DataReader("SELECT TOP " + count.ToString() + " " +
                        "sw.purchaseorder, sw.quantity," +
                        "sw.receivedate,t.brandname FROM stock_warehouse sw " +
                        "INNER JOIN items t ON sw.itemsid = t.itemsid " +
                        "where sw.itemsid ='" + Convert.ToInt64(itemsid) + "' " +
                        "order by sw.receivedate Desc");
                        while (con._dr.Read()) 
                        {
                            total += Convert.ToInt32(con._dr["quantity"].ToString());
                        }
                        con._dr.Close();
                        con.CloseConnection();

                        if (total > Convert.ToInt32(qty) || total == Convert.ToInt32(qty))
                        {
                            count++;
                            contin = false;
                        }
                    
                        else
                        {
                            contin = true;
                           // gv.BorderColor = System.Drawing.Color.Orange;
                        }
                    } while (contin != false);
                    con.OpenConection();
                    con.ExecSqlQuery("SELECT TOP " + count.ToString() + " " +
                    "sw.purchaseorder, sw.quantity," +
                    "sw.receivedate,t.brandname FROM stock_warehouse sw " +
                    "INNER JOIN items t ON sw.itemsid = t.itemsid " +
                    "where sw.itemsid ='" + Convert.ToInt64(itemsid) + "' " +
                    "order by sw.receivedate ASC");
                    gv.DataSource = con.DataQueryExec();
                    gv.DataBind();
                    con.CloseConnection();
                    
                }
            }
        }

    }
}