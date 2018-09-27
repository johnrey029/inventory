using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecci.inv.system.sales
{
    public partial class index : System.Web.UI.Page
    {
        private string sessionempno { get; set; }
        private string ponumber { get; set; }
        private string ponumber2 { get; set; }
        DBConnection con;
        TableRow tr;
        TableCell tc;
        int i = 1, count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            con = new DBConnection();
            if (!IsPostBack)
            {
                if (Session["empnumber"] != null)
                {
                    sessionempno = Session["empnumber"].ToString();
                }
                panelTableRow.Controls.Clear();
                lbDate.Text = DateTime.Now.ToString("MMMM d, yyyy");

                lbTime.Text = DateTime.Now.ToString("hh:mm tt");
                btnAddrow.Font.Size = FontUnit.Smaller;

                load(i);
            }
            else
            {
                List<string> keyprod = Request.Form.AllKeys.Where(key => key.Contains("Product")).ToList();
                //List<string> keyqty = Request.Form.AllKeys.Where(key => key.Contains("Qty")).ToList();
                //List<string> keyrate = Request.Form.AllKeys.Where(key => key.Contains("Rate")).ToList();

                //List<string> keyamt = Request.Form.AllKeys.Where(key => key.Contains("Amount")).ToList();

                //List<string> keyrmv = Request.Form.AllKeys.Where(key => key.Contains("Remove")).ToList();
                int y = 1;
                foreach(string key in keyprod)
                {
                    this.load(y);
                    y++;
                }
            }
        }

        private void load(int id)
        {
            StringBuilder sb = new StringBuilder();
            i = id;
           // StringBuilder table = new StringBuilder();
            tr = new TableRow();
            tr.ID = "Row" + i;
            tr.Attributes.Add("style", "width:100%");
            for (int x = 0; x < 5; x++)
            {
                tc = new TableCell();
                tc.Attributes.Add("style", "width:100%");
                DropDownList ddProduct = new DropDownList();
                TextBox tbQty = new TextBox();
                TextBox tbRate = new TextBox();
                TextBox tbAmount = new TextBox();
                Button btnRemove = new Button();
                sb.Append("<div class='form-group'>");
                
                //table.Append("<td style='width: 100px'>");
                switch (x) {
                    case 0: 
                        ddProduct.ID = "Product" + i;
                        // ddProduct.Text = "Product" + i;
                        try
                        {
                            con.OpenConection();
                            con.ExecSqlQuery("Select * from suppliers");
                            ddProduct.DataTextField = "suppname";
                            ddProduct.DataValueField = "suppcode";
                            ddProduct.DataSource = con.DataQueryExec();
                            ddProduct.DataBind();
                            ddProduct.Items.Insert(0, new ListItem("Select Supplier", "-1"));
                            con.CloseConnection();
                        }
                        catch { }
                        //ddProduct.Items.Add("Hello world1");
                        //ddProduct.Items.Add("Hello world2");
                        //ddProduct.Items.Add("Hello world3");
                        //ddProduct.Items.Add("Hello world4");
                        ddProduct.Attributes.Add("style", "width:100%");
                        ddProduct.Attributes.Add("class", "js-example-placeholder-single");
                        tc.Controls.Add(ddProduct);
                        break;
                    case 1:
                        tbQty.ID = "Qty" + i;
                        tbQty.Text = "Qty" + i;

                        tbQty.Font.Size = FontUnit.Medium;
                        //tbQty.Attributes.Add("class", "form-control");
                        tc.Controls.Add(tbQty);
                        break;
                    case 2:
                        tbRate.ID = "Rate" + i;
                        tbRate.Text = "Rate" + i;

                        tbRate.Font.Size = FontUnit.Medium;
                        //tbRate.Attributes.Add("class", "form-control");
                        tc.Controls.Add(tbRate);
                        break;
                    case 3:
                        tbAmount.ID = "Amount" + i;
                        tbAmount.Text = "Amount" + i;
                        tbAmount.Font.Size = FontUnit.Medium;
                        //tbAmount.Attributes.Add("class", "form-control");
                        tc.Controls.Add(tbAmount);
                        break;
                    case 4:
                        btnRemove.ID = "Remove" + i;
                        btnRemove.Text = "Remove Row";
                        btnRemove.Font.Size = FontUnit.Smaller;
                        ddProduct.Attributes.Add("style", "width:50%");
                        btnRemove.Attributes.Add("class", "btn btn-warning");
                        btnRemove.Click += BtnRemove_Click;
                        tc.Controls.Add(btnRemove);
                        break;
                    default: break;
                }
                tr.Cells.Add(tc);
                sb.Append("</div>");
            }
            //  table.Append("</td>");
            i++;
            panelTableRow.Controls.Add(tr);

        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {

        }

        //private void dropdown()
        //{
        //    try
        //    {
        //        con.OpenConection();
        //        con.ExecSqlQuery("Select * from suppliers");
        //        ddSupplier.DataTextField = "suppname";
        //        ddSupplier.DataValueField = "suppcode";
        //        ddSupplier.DataSource = con.DataQueryExec();
        //        ddSupplier.DataBind();
        //        ddSupplier.Items.Insert(0, new ListItem("Select Supplier", "-1"));
        //        con.CloseConnection();
        //    }
        //    catch//(Exception ex)
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
        //        "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
        //    }
        //}

        protected void btnAddrow_Click(object sender, EventArgs e)
        {
            int id = panelTableRow.Controls.OfType<TableRow>().ToList().Count + 1;
            this.load(id);
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            string message = "";
            foreach (TableRow tbRow in panelTableRow.Controls.OfType<TableRow>())
            {
                foreach (TableCell tbCell in tbRow.Controls.OfType<TableCell>())
                {
                    switch (count)
                    {
                        case 0:
                            foreach (DropDownList ddProd in tbCell.Controls.OfType<DropDownList>())
                            {
                                message += ddProd.SelectedValue.ToString() + "\\n";
                            }
                            count++;
                            break;
                        case 1:
                        case 2:
                        case 3:
                            foreach (TextBox Text in tbCell.Controls.OfType<TextBox>())
                            {
                                message += Text.ID.ToString() + "\\n";
                            }
                            count++;
                            break;
                        default:
                            count = 0;
                            break;
                    }
                }
            }
            
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('" + message + "');", true);
        }
    }
}