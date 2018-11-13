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
            @"SELECT  i.quantityordered, u.itemsid,
                u.price,t.brandname FROM oderdetails i
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
            Int64 total = 0;
            Int64 addedtotal = 0;
            int count = 1;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string requestedid = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
                if (requestedid.Length > 0)
                {
                    do
                    {
                        //   for (int i = 1; i<= GridView1; i++)
                        //  {
                        //for (int i = 0; i < gv.Columns.Count; i++)
                        // {
                        //String header = gv.Columns[i].HeaderText;
                        //    total = Convert.ToInt64(row.Cells[2].Text);
                        // }
                        //}
                      //  total = Convert.ToInt64(GridView1.Rows[index].Cells[2].Text.ToString());
                        //total = Convert.ToInt64(Request.Form.Get(requestedid).ToString());
                        con.OpenConection();
                        GridView gv = (GridView)e.Row.FindControl("GridView2");
                        con.ExecSqlQuery("SELECT TOP " + count.ToString() + " " + 
                        "sw.purchaseorder, sw.quantity,"+
                        "sw.receivedate,t.brandname FROM stock_warehouse sw " +
                        "INNER JOIN items t ON sw.itemsid = t.itemsid " +
                        "where sw.itemsid ='" + Convert.ToInt64(requestedid) + "' "+
                        "order by sw.receivedate Desc");
                        gv.DataSource = con.DataQueryExec();
                        gv.DataBind();
                        con.CloseConnection();
                        foreach (GridViewRow row in gv.Rows)
                        {
                            //for (int i = 0; i < gv.Columns.Count; i++)
                            // {
                            //String header = gv.Columns[i].HeaderText;
                            addedtotal += Convert.ToInt64(row.Cells[2].Text);
                            // }
                        }
                        if (addedtotal == total)
                        {
                            contin = false;
                        }
                    } while (contin != false);
                }
            }
        }
    }
}