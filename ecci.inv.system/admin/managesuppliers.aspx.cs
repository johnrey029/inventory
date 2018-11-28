using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecci.inv.system.admin
{
    public partial class managesuppliers : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                lbError.Visible = false;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int sid = Convert.ToInt32(Request.Form.Get("hiddenSupplierId").ToString());
            ManageSupplierService.ManageSupplierServiceSoapClient supplierSC = new ManageSupplierService.ManageSupplierServiceSoapClient("ManageSupplierServiceSoap");

            int result1 = activitySupplier();
            int result2 = supplierSC.updateSupplierById(sid, tbSuppAdd.Text, tbSuppContact.Text);
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

        public int activitySupplier()
        {
            DateTime dati = DateTime.Now;
            con = new DBConnection();
            con.OpenConection();
            con.ExecSqlQuery("INSERT INTO activity_suppliers (act_empno,act_suppid,act_remarks,act_datetime) VALUES (@empno,@suppid,@remarks,@dt) ");
            con.Cmd.Parameters.AddWithValue("@empno", sessionempno);
            con.Cmd.Parameters.AddWithValue("@suppid", tbSuppCode.Text);
            con.Cmd.Parameters.AddWithValue("@remarks", "Update");
            con.Cmd.Parameters.AddWithValue("@dt",dati);

            int a = con.Cmd.ExecuteNonQuery();
            con.CloseConnection();

            return a;
        }
    }
}