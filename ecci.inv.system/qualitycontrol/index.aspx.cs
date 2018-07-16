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
            int result1 = InsertById();
            int result = client.UpdateById(sid);
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
        //public int itemsid()
        //{
        //    int a = 0,sid = 0;
        //    con.OpenConection();
        //    con.ExecSqlQuery("Select * from suppliers where suppname = @sname");
        //    con.Cmd.Parameters.AddWithValue("@sname", Request.Form.Get("supplier").ToString());
        //    con._dr = con.Cmd.ExecuteReader();
        //    while(con._dr.Read())
        //    {
        //        sid = Convert.ToInt32(con._dr["suppcode"].ToString());
        //    }
        //    con.CloseConnection();

        //    con.OpenConection();
        //    con.ExecSqlQuery("Select * from items where brandname = @bn and suppcode=@sc");
        //    con.Cmd.Parameters.AddWithValue("@bn", Request.Form.Get("brand").ToString());
        //    con.Cmd.Parameters.AddWithValue("@sc", sid);
        //    con._dr = con.Cmd.ExecuteReader();
        //    while (con._dr.Read())
        //    {
        //        a = Convert.ToInt32(con._dr["itemsid"].ToString());
        //    }
        //    con.CloseConnection();

        //    return a;
        //}
        public int InsertById()
        {
            //ToString("MMMM dd yyyy hh:mm tt")
            //int item = itemsid(); itemsid, quantity, purchasedate, deliverydate, postatus,receivedate, @item, @quan, @pdate, @ddate, @stat,@rdate

            string date = DateTime.Now.ToShortDateString();
            //DateTime pdate = DateTime.Parse(Request.Form.Get("pdate").ToString());
            //DateTime ddate = DateTime.Parse(Request.Form.Get("ddate").ToString());
            string time = DateTime.Now.ToLongTimeString();
            con.OpenConection();
            con.ExecSqlQuery("INSERT INTO activity_stock_raw(purchaseorder,empno,activity,date,time)VALUES(@po,@en,@act,@rdate,@t)");
            con.Cmd.Parameters.AddWithValue("@po", Request.Form.Get("po").ToString());
            //con.Cmd.Parameters.AddWithValue("@item", item);
            //con.Cmd.Parameters.AddWithValue("@quan", Request.Form.Get("qty").ToString());
            //con.Cmd.Parameters.AddWithValue("@pdate", pdate.ToShortDateString());
            //con.Cmd.Parameters.AddWithValue("@ddate", ddate.ToShortDateString());
            con.Cmd.Parameters.AddWithValue("@rdate", DateTime.Parse(date));
            //con.Cmd.Parameters.AddWithValue("@stat", "Received");
            con.Cmd.Parameters.AddWithValue("@en", sessionempno);
            con.Cmd.Parameters.AddWithValue("@act", "Received");
            con.Cmd.Parameters.AddWithValue("@t", DateTime.Parse(time));
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