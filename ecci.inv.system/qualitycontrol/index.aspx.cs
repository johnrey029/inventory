using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecci.inv.system.qualitycontrol
{
    public partial class index : System.Web.UI.Page
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
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int sid = Convert.ToInt32(Request.Form.Get("hiddenStockId").ToString());
            DeliveryService.OrderDeliveryServiceSoapClient client = new DeliveryService.OrderDeliveryServiceSoapClient("OrderDeliveryServiceSoap");
            int result = client.UpdateById(sid);
            int result1 = InsertById();
            if (result == 1 && result1 == 1)
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-error').hide(); $('.alert-success').show(); });</script>");
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide(); $('.alert-error').show(); });</script>");
            }
        }
        public int itemsid()
        {
            int a = 0,sid = 0;
            con = new DBConnection();
            con.OpenConection();
            con._dr = con.DataReader("Select * from suppliers where suppname = @sname");
            con.Cmd.Parameters.AddWithValue("@sname", Request.Form.Get("supplier").ToString());
            while(con._dr.Read())
            {
                sid = Convert.ToInt32(con._dr["stockid"].ToString());
            }
            con.CloseConnection();

            con.OpenConection();
            con._dr = con.DataReader("Select * from items where brandname = @bn and suppcode=@sc");
            con.Cmd.Parameters.AddWithValue("@bn", Request.Form.Get("brand").ToString());
            con.Cmd.Parameters.AddWithValue("@sc", sid); 
            while(con._dr.Read())
            {
                a = Convert.ToInt32(con._dr["stockid"].ToString());
            }
            con.CloseConnection();

            return a;
        }
        public int InsertById()
        {
            string date = DateTime.Now.ToString("MMMM dd, yyyy hh:mm tt");
            DateTime pdate = DateTime.Parse(Request.Form.Get("pdate").ToString());
            con = new DBConnection();
            con.OpenConection();
            con.ExecSqlQuery("insert into activity_transaction(purchaseorder, itemsid, quantity, purchasedate, deliverydate, postatus,receivedate,empno,activity)values(@po, @item, @quan, @pdate, @ddate, @stat,@rdate,@en,@act)");
            con.Cmd.Parameters.AddWithValue("@po", Request.Form.Get("po").ToString());
            con.Cmd.Parameters.AddWithValue("@item", itemsid());
            con.Cmd.Parameters.AddWithValue("@quan", Request.Form.Get("qty").ToString());
            con.Cmd.Parameters.AddWithValue("@pdate", pdate.ToString("MMMM dd, yyyy hh:mm tt"));
            con.Cmd.Parameters.AddWithValue("@ddate", Request.Form.Get("ddate").ToString());
            con.Cmd.Parameters.AddWithValue("@rdate", date);
            con.Cmd.Parameters.AddWithValue("@stat", "Received");
            con.Cmd.Parameters.AddWithValue("@en", sessionempno);
            con.Cmd.Parameters.AddWithValue("@act", "Received Delivery");
            int a = con.Cmd.ExecuteNonQuery();
            con.CloseConnection();
            if (a == 0)
            {
                //Page.ClientScript.RegisterClientScriptBlock(GetType(), "alert",
                //"<script>$(document).ready(function(){ $('.alert-success').hide(); $('.alert-error').show(); });</script>");
            }
            else
            {
                //Page.ClientScript.RegisterClientScriptBlock(GetType(), "alert",
                //"<script>$(document).ready(function(){ $('.alert-error').hide(); $('.alert-success').show(); });</script>");
            }
            return a;
        }
    }
}