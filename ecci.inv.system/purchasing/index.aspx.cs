using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace ecci.inv.system.purchasing
{
    public partial class index : Page
    {

        private string sessionempno { get; set; }
        DBConnection con;
        StringBuilder table = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["empnumber"] != null)
            {
                sessionempno = Session["empnumber"].ToString();
                ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
                con = new DBConnection();
                if (!IsPostBack)
                {
                    lbError.Visible = false;
                    //load();
                }
            }
            else
            {
                Session.Clear();
                Response.Redirect("~/default.aspx");
            }
        }

        private void load()
        {
            try
            {
                con.OpenConection();
                con._dr = con.DataReader(
                @"SELECT s.purchaseorder,s.quantity,s.purchasedate,s.deliverydate,
                s.postatus, i.brandname, u.suppname FROM stock_raw s
                INNER JOIN items i ON s.itemsid = i.itemsid
                INNER JOIN suppliers u ON i.suppcode = u.suppcode
                ORDER BY s.stockid ASC");
                //con.ExecNonQuery();
                //con._dr = con.Cmd.ExecuteReader();
                //table.Append("<table border = '1'>");
                //table.Append("<tr>");
                //table.Append("< th > PO#</th>< th > Supplier Name </ th >< th > Brand Name </ th >< th > Description </ th > < th > Quantity </ th > < th > Purchased Date </ th >< th > Delivery Date </ th >< th > Staus </ th >");
                //table.Append("</tr>");
                // table.Append("<tbody>");
                while (con._dr.Read())
                {
                    table.Append("<tr>");
                    table.Append("<td>" + con._dr["purchaseorder"].ToString() + "</td>");
                    table.Append("<td>" + con._dr["suppname"].ToString() + "</td>");
                    table.Append("<td>" + con._dr["brandname"].ToString() + "</td>");
                    table.Append("<td>" + con._dr["quantity"].ToString() + "</td>");
                    table.Append("<td>" + con._dr["purchasedate"].ToString() + "</td>");
                    table.Append("<td>" + con._dr["deliverydate"].ToString() + "</td>");
                    table.Append("<td>" + con._dr["postatus"].ToString() + "</td>");
                    table.Append("<tr>");
                }
              //  table.Append("</tbody>");
                //table.Append("</table>");
               // PlaceHolder1.Controls.Add(new Literal { Text = table.ToString() });
                con._dr.Close();
                con.CloseConnection();
            }
            catch (Exception ex)
            {
                lbError.Visible = true;
                lbError.Text = ex.Message;
            }
        }
    }
}