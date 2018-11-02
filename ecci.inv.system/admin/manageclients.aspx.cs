using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecci.inv.system.admin
{
    public partial class manageclients : System.Web.UI.Page
    {
        private string sessionempno { get; set; }
        public int stat { get; set; }
        DBConnection con;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["empnumber"] != null)
            {
                sessionempno = Session["empnumber"].ToString();
            }
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (!IsPostBack)
            {
                lbError.Visible = false;
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int cid = Convert.ToInt32(Request.Form.Get("hiddenClientId").ToString());
            ManageSupplierService.ManageSupplierServiceSoapClient supplierSC = new ManageSupplierService.ManageSupplierServiceSoapClient("ManageSupplierServiceSoap");
            ManageClientService.ManageClientServiceSoapClient clientSC = new ManageClientService.ManageClientServiceSoapClient("ManageClientServiceSoap");

            if (rbActive.Checked && !rbDeactivate.Checked)
            {
                stat = 1;

            }
            else if (!rbActive.Checked && rbDeactivate.Checked)
            {
                stat = 2;
            }
            int result1 = activityClient();
            int result2 = clientSC.updateClientById(cid, tbClientAdd.Text, tbClientCon.Text, tbClientCon2.Text, stat);
            if (result1 == 1 && result2 == 1)
            {
                //Session["sucess"] = "Tama";
                //  UpdatePanel2.ValidateRequestMode = ValidateRequestMode.Disabled;
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "$('#updateModal').modal('hide');", true);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-error').hide(); $('.alert-success').show(); $('.alert-warning').hide(); });</script>");
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide(); $('.alert-error').show(); $('.alert-warning').hide(); });</script>");
                //Session["sucess"] = "Mali";
            }
        }
        public int activityClient()
        {
            int a = 0;
            //try
            //{
            int cid = Convert.ToInt32(Request.Form.Get("hiddenClientId").ToString());
            DateTime dati = DateTime.Now;
            con = new DBConnection();
            con.OpenConection();
            con.ExecSqlQuery("INSERT INTO activity_client(empno, client, datetime,remarks)VALUES(@empno, @client, @datetime, @remarks)");
            con.Cmd.Parameters.AddWithValue("@empno", sessionempno);
            con.Cmd.Parameters.AddWithValue("@client", cid.ToString());
            con.Cmd.Parameters.AddWithValue("@datetime", dati);
            con.Cmd.Parameters.AddWithValue("@remarks", "Update");

            a = con.Cmd.ExecuteNonQuery();
            con.CloseConnection();

            //clear();

            //}
            //catch (Exception ex)
            //{
            //    lbError.ForeColor = System.Drawing.Color.Red;
            //    lbError.Text = "Error: " + ex.Message;
            //    lbError.Visible = true;
            //}
            return a;
        }
    }
}