using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ecci.inv.system.sales
{
    public partial class index : System.Web.UI.Page
    {
        private string sessionempno { get; set; }
        DBConnection con;
        TableRow tr;
        TableCell tc;
        bool disp = true;
        int SumTotal = 0;
        protected int rows
        {
            get { return (int)ViewState["Rows"]; }
            set { ViewState["Rows"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            con = new DBConnection();
            if (!IsPostBack)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                     "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
                if (Session["empnumber"] != null)
                {
                    sessionempno = Session["empnumber"].ToString();
                }
                this.rows = 1;
                panelTableRow.Controls.Clear();
                createRow();

                lbDate.Text = DateTime.Now.ToString("MMMM d, yyyy");
                //lbTime.Text = DateTime.Now.ToString("hh:mm tt");
                btnAddrow.Font.Size = FontUnit.Smaller;
                dropdown();
                //dropProd();
            }
            else
            {
                if (disp == true)
                {
                    createTable();
                }
            }


        }
        private void createRow()
        {
            tr = new TableRow();
            tr.ID = "Row" + rows.ToString();
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
                switch (x) {
                    case 0: 
                        ddProduct.ID = rows.ToString();
                        try
                        {
                            con.OpenConection();
                            con.ExecSqlQuery("Select * from product");
                            ddProduct.DataTextField = "pname";
                            ddProduct.DataValueField = "productid";
                            ddProduct.DataSource = con.DataQueryExec();
                            ddProduct.DataBind();
                            ddProduct.Items.Insert(0, new ListItem("Select Product", "-1"));
                            con.CloseConnection();
                        }
                        catch { }
                        ddProduct.Attributes.Add("style", "width:100%");
                        ddProduct.AutoPostBack = true;
                        ddProduct.Attributes.Add("runat", "server");
                        ddProduct.Attributes.Add("class", "js-example-placeholder-single");
                        ddProduct.SelectedIndexChanged += new EventHandler(this.ddProductSelect);
                        tc.Controls.Add(ddProduct);
                        RequiredFieldValidator rfv = new RequiredFieldValidator();
                        rfv.Attributes.Add("runat", "server");
                        rfv.ID = "rfv" + rows.ToString();
                        rfv.ControlToValidate = rows.ToString();
                        rfv.ErrorMessage = "This field is required";
                        rfv.InitialValue = "-1";
                        rfv.ForeColor = System.Drawing.Color.Red;
                        rfv.Display = ValidatorDisplay.Dynamic;
                        tc.Controls.Add(rfv);
                        break;
                    case 1:
                        tbQty.ID = "Qty" + rows.ToString();
                        tbQty.Text = "1";// "Qty" + rows;
                        tbQty.Font.Size = FontUnit.Medium;
                        tbQty.TextChanged += new EventHandler(this.TbQty_TextChanged);
                        tbQty.AutoCompleteType = AutoCompleteType.Disabled;
                        tbQty.AutoPostBack = true;
                        tbQty.Attributes.Add("runat", "server");
                        tbQty.Attributes.Add("onkeydown", "return (!(event.keyCode>=65) && event.keyCode!=32);");
                        tc.Controls.Add(tbQty);
                        break;
                    case 2:
                        tbRate.ID = "Rate" + rows.ToString();
                        tbRate.Text = "0.00";//"Rate" + rows;
                        tbRate.Attributes.Add("ReadOnly", "true");
                        tbRate.Attributes.Add("BackColor", "White");
                        tbRate.Attributes.Add("runat", "server");
                        tbRate.Font.Size = FontUnit.Medium;
                        //tc.Controls.Add(tbRate);
                        UpdatePanel up1 = new UpdatePanel();
                        up1.ID = "Update" + rows.ToString();
                        up1.UpdateMode = UpdatePanelUpdateMode.Always;
                        up1.Attributes.Add("runat", "server");
                        up1.ChildrenAsTriggers = true;
                        up1.RenderMode = UpdatePanelRenderMode.Block;
                        up1.ContentTemplateContainer.Controls.Add(tbRate);
                        AsyncPostBackTrigger trigger1 = new AsyncPostBackTrigger();
                        trigger1.ControlID = "" + rows;
                        trigger1.EventName = "SelectedIndexChanged";
                        AsyncPostBackTrigger trigger2 = new AsyncPostBackTrigger();
                        trigger2.ControlID = "Qty" + rows;
                        trigger2.EventName = "TextChanged";
                        up1.Triggers.Add(trigger1);
                        up1.Triggers.Add(trigger2);
                        tc.Controls.Add(up1);
                        break;
                    case 3:
                        tbAmount.ID = "Amount" + rows.ToString();
                        tbAmount.Text = "0.00";// "Amount" + i;
                        tbAmount.Font.Size = FontUnit.Medium;
                        tbAmount.Attributes.Add("ReadOnly", "true");
                        tbAmount.Attributes.Add("BackColor", "White");
                        tbAmount.Attributes.Add("runat", "server");
                        //tc.Controls.Add(tbAmount);
                        UpdatePanel up = new UpdatePanel();
                        up.ID = "Updt" + rows.ToString();
                        up.UpdateMode = UpdatePanelUpdateMode.Always;
                        up.Attributes.Add("runat", "server");
                        up.ChildrenAsTriggers = true;
                        up.RenderMode = UpdatePanelRenderMode.Block;
                        up.ContentTemplateContainer.Controls.Add(tbAmount);
                        AsyncPostBackTrigger trigger3 = new AsyncPostBackTrigger();
                        trigger3.ControlID = "" + rows;
                        trigger3.EventName = "SelectedIndexChanged";
                        AsyncPostBackTrigger trigger4 = new AsyncPostBackTrigger();
                        trigger4.ControlID = "Qty" + rows;
                        trigger4.EventName = "TextChanged";
                        up.Triggers.Add(trigger3);
                        up.Triggers.Add(trigger4);
                        tc.Controls.Add(up);
                        break;
                    case 4:
                        btnRemove.ID = "Remove" + rows.ToString();
                        btnRemove.Text = "Remove Row";
                        btnRemove.Font.Size = FontUnit.Smaller;
                        btnRemove.Attributes.Add("class", "btn btn-warning");
                        btnRemove.Attributes.Add("runat", "server");
                        btnRemove.Click += BtnRemove_Click;
                        tc.Controls.Add(btnRemove);
                        break;
                    default: break;
                }

                tr.Cells.Add(tc);
            }
            panelTableRow.Controls.Add(tr);
            AsyncPostBackTrigger trigger5 = new AsyncPostBackTrigger();
            trigger5.ControlID = "" + rows;
            trigger5.EventName = "SelectedIndexChanged";
            AsyncPostBackTrigger trigger6 = new AsyncPostBackTrigger();
            trigger6.ControlID = "Qty" + rows;
            trigger6.EventName = "TextChanged";
            UpdatePanel5.Triggers.Add(trigger5);
            UpdatePanel5.Triggers.Add(trigger6);

        }

        protected void TbQty_TextChanged(object sender, EventArgs e)
        {
            TextBox tbQ = (TextBox)sender;
            string id = tbQ.ID.ToString();
            int len = id.Length - 3;
            TextBox tbR = (TextBox)panelTableRow.FindControl("Rate" + Convert.ToInt32(id.Substring(3, len)));
            TextBox tbA = (TextBox)panelTableRow.FindControl("Amount" + Convert.ToInt32(id.Substring(3, len)));
            decimal uprice = Convert.ToDecimal(tbR.Text);
            decimal quant = Convert.ToDecimal(tbQ.Text);
            decimal total = uprice * quant;
            tbA.Text = total.ToString();
            tbTotalAmount.Text = getTAmount();
        }
        protected void ddProductSelect(object sender, EventArgs e)
        {
            DropDownList dl = (DropDownList)sender;
            string row = dl.ID.ToString();
            TextBox tbR = (TextBox)panelTableRow.FindControl("Rate" + Convert.ToInt32(row));
            TextBox tbA = (TextBox)panelTableRow.FindControl("Amount" + Convert.ToInt32(row));
            TextBox tbQ = (TextBox)panelTableRow.FindControl("Qty" + Convert.ToInt32(row));
            //TextBox tb = (TextBox)table.Rows[Convert.ToInt32(row)].Cells[3].FindControl("Rate" + Convert.ToInt32(row));
            try
            {
                con.OpenConection();
                con._dr = con.DataReader("SELECT * FROM product WHERE productid='" + dl.SelectedValue.ToString() + "'");
                while (con._dr.Read())
                {
                    tbR.Text = con._dr["price"].ToString();
                }
                con.CloseConnection();
                decimal uprice = Convert.ToDecimal(tbR.Text);
                decimal quant = Convert.ToDecimal(tbQ.Text);
                decimal total = uprice * quant;
                tbA.Text = total.ToString();
                tbTotalAmount.Text = getTAmount();
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').show(); });</script>");
            }
        }
        private string getTAmount()
        {
            decimal total = 0;
            int rws = 0;
            int count = 0;
            string[,] array = new string[this.rows, 4];
            foreach (TableRow tbRow in panelTableRow.Controls.OfType<TableRow>())
            {
                foreach (TableCell tbCell in tbRow.Controls.OfType<TableCell>())
                {
                    switch (count)
                    {
                        case 0:
                            count++;
                            break;
                        case 1:
                            count++;
                            break;
                        case 2:
                            count++;
                            break;
                        case 3:
                            foreach (UpdatePanel up in tbCell.Controls.OfType<UpdatePanel>())
                            {
                                foreach (TextBox Text in up.ContentTemplateContainer.Controls.OfType<TextBox>())
                                {
                                    //message += Text.Text.ToString() + "\\n";
                                    array[rws, 3] = Text.Text.ToString();
                                }
                            }
                            count++;
                            break;
                        default:
                            count = 0;
                            break;
                    }
                }
                rws++;
            }
            rws = 0;
            for (int x = 0; x < this.rows; x++)
            {
                total += Convert.ToDecimal(array[x, 3].ToString());
            }
            return total.ToString();
        }
        private void createTable()
        {
            int num = this.rows;
            UpdatePanel5.Triggers.Clear();
            for (int i = 1;i<=num; i++)
            {
                this.rows = i;
                createRow();
            }
        }
        
        private void BtnRemove_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddrow_Click(object sender, EventArgs e)
        {
            this.rows++;
            this.createRow();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            //string message = "";
            int rws = 0;
            int count = 0;
            int checkdb = 0;
            string[,]array = new string[this.rows,4];
            foreach (TableRow tbRow in panelTableRow.Controls.OfType<TableRow>())
            {
                foreach (TableCell tbCell in tbRow.Controls.OfType<TableCell>())
                {
                    switch (count)
                    {
                        case 0:
                            foreach (DropDownList ddProd in tbCell.Controls.OfType<DropDownList>())
                            {
                                //message += ddProd.SelectedValue.ToString() + "\\n";
                                array[rws,0] = ddProd.SelectedValue.ToString();
                            }
                            count++;
                            break;
                        case 1:
                            foreach (TextBox Text in tbCell.Controls.OfType<TextBox>())
                            {
                                //message += Text.Text.ToString() + "\\n";
                                array[rws, 1] = Text.Text.ToString();
                            }
                            count++;
                            break;
                        case 2:
                            foreach (UpdatePanel up in tbCell.Controls.OfType<UpdatePanel>())
                            {
                                foreach (TextBox Text in up.ContentTemplateContainer.Controls.OfType<TextBox>())
                                {
                                    //message += Text.Text.ToString() + "\\n";
                                    array[rws, 2] = Text.Text.ToString();
                                }
                            }
                            count++;
                            break;
                        case 3:
                            foreach (UpdatePanel up in tbCell.Controls.OfType<UpdatePanel>())
                            {
                                foreach (TextBox Text in up.ContentTemplateContainer.Controls.OfType<TextBox>())
                                {
                                    //message += Text.Text.ToString() + "\\n";
                                    array[rws, 3] = Text.Text.ToString();
                                }
                            }
                            count++;
                            break;
                        default:
                            count = 0;
                            break;
                    }
                }
                rws++;
            }
            rws = 0;

            disp = false;
            //for (int i = 0; i < this.rows; i++)
            //{
            //    for (int j = 0; j < this.rows - 1; j++)
            //    {
            //        if (array[i, 0] == array[j + 1, 0])
            //        {

            //        }
            //    }
            //}
            //for (int x = 0; x < this.rows; x++)
            //{
            //    for (int y = 0; y < array.Length; y++)
            //    {

            //    }
            //}
            try
            {
                string date = DateTime.Now.ToShortTimeString();
                con.OpenConection();
                con.ExecSqlQuery("INSERT INTO purchaseorder(clientid,usersid,date,status,totalamount)VALUES(@ci,@ui,@date,@stat,@tamount)");//output inserted.orderid SET @ID = @@IDENTITY RETURN @ID
                con.Cmd.Parameters.AddWithValue("@ci", ddCustomer.SelectedValue.ToString());
                con.Cmd.Parameters.AddWithValue("@ui", 4008);
                con.Cmd.Parameters.AddWithValue("@date", DateTime.Parse(date));
                con.Cmd.Parameters.AddWithValue("@stat", "Order");
                con.Cmd.Parameters.AddWithValue("@tamount", tbTotalAmount.Text);
                //con.Cmd.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                //int olid = Convert.ToInt32(con.Cmd.Parameters["@ID"].Value.ToString());
                con.Cmd.ExecuteNonQuery();
                con.CloseConnection();

                con.OpenConection();
                con._dr = con.DataReader("Select Top 1 orderid From purchaseorder Order by orderid desc");// "Select * from purchaseorder"
                while (con._dr.Read())
                {
                    count = Convert.ToInt32(con._dr["orderid"].ToString());
                }
                con.CloseConnection();
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').show(); });</script>");
            }
            // ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('" + message + "');", true);
            int qty = 0;
            for (int i = 0; i < this.rows; i++)
            {
                if (Convert.ToInt32(array[i, 1]) <= 0)
                {
                    lbError.Visible = true;
                }
                else
                {
                    lbError.Visible = false;
                    con.OpenConection();
                    con.ExecSqlQuery("Select count(*) from oderdetails where orderid=@oid and productid=@pid");
                    con.Cmd.Parameters.AddWithValue("@oid", count);
                    con.Cmd.Parameters.AddWithValue("@pid", array[i, 0]);
                    checkdb = Convert.ToInt32(con.Cmd.ExecuteScalar());
                    con.CloseConnection();
                    try
                    {
                        if (checkdb  > 0)
                        {
                            con.OpenConection();
                            con._dr = con.DataReader("Select * from oderdetails where orderid='" + count + "' and productid='" + array[i, 0] + "'");// "Select * from purchaseorder"
                           // con.Cmd.Parameters.AddWithValue("@orid", count);
                           // con.Cmd.Parameters.AddWithValue("@pdid", array[i, 0]);
                            while (con._dr.Read())
                            {
                                qty = Convert.ToInt32(con._dr["quantityordered"].ToString());
                            }
                            con.CloseConnection();
                            decimal uprice = Convert.ToDecimal(array[i, 2]);
                            decimal quant = Convert.ToDecimal(qty) + Convert.ToDecimal(array[i, 1]);
                            decimal total = uprice * quant;
                            con.OpenConection();
                            con.ExecSqlQuery("UPDATE oderdetails SET quantityordered=@qo,price=@p,amount=@amt, status =@stat where orderid=@oi and productid=@pi");
                            con.Cmd.Parameters.AddWithValue("@oi", count);
                            con.Cmd.Parameters.AddWithValue("@pi", array[i, 0]);
                            con.Cmd.Parameters.AddWithValue("@qo", quant);
                            con.Cmd.Parameters.AddWithValue("@p", array[i, 2]);
                            con.Cmd.Parameters.AddWithValue("@amt", total);
                            con.Cmd.Parameters.AddWithValue("@stat", "Order");
                            rws = con.Cmd.ExecuteNonQuery();
                            con.CloseConnection();
                        }
                        else
                        {
                            con.OpenConection();
                            con.ExecSqlQuery("INSERT INTO oderdetails(orderid,productid,quantityordered,price,amount,status)VALUES(@oi,@pi,@qo,@p,@amt,@stat)");
                            con.Cmd.Parameters.AddWithValue("@oi", count);
                            con.Cmd.Parameters.AddWithValue("@pi", array[i, 0]);
                            con.Cmd.Parameters.AddWithValue("@qo", array[i, 1]);
                            con.Cmd.Parameters.AddWithValue("@p", array[i, 2]);
                            con.Cmd.Parameters.AddWithValue("@amt", array[i, 3]);
                            con.Cmd.Parameters.AddWithValue("@stat", "Order");
                            rws = con.Cmd.ExecuteNonQuery();
                            con.CloseConnection();
                        }
                    }
                    catch
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                        "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').show(); });</script>");
                    }
                }
            }
            panelTableRow.Controls.Clear();
            clear();
            if (rws == 1 && count > 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').show(); $('.alert-error').hide(); });</script>");
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide(); $('.alert-error').show(); });</script>");
            }

        }
        private void clear()
        {
            this.rows = 1;
            ddCustomer.SelectedIndex = -1;
           // dropdown();
            tbAddress.Text = "";
            tbContact.Text = "";
            lbError.Visible = false;
            tbTotalAmount.Text = "0.00";
            createRow();

        }

        protected void ddCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con.OpenConection();
                con._dr = con.DataReader("SELECT * FROM client WHERE clientid='" + ddCustomer.SelectedValue.ToString() + "'");
                while (con._dr.Read())
                {
                    tbAddress.Text = con._dr["address"].ToString();
                    tbContact.Text = con._dr["contact1"].ToString();
                }
                con.CloseConnection();
            }
            catch
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
            }
        }
        private void dropdown()
        {
            try
            {
                con.OpenConection();
                con.ExecSqlQuery("Select * from client");
                ddCustomer.DataTextField = "name";
                ddCustomer.DataValueField = "clientid";
                ddCustomer.DataSource = con.DataQueryExec();
                ddCustomer.DataBind();
                ddCustomer.Items.Insert(0, new ListItem("Select Client", "-1"));
                con.CloseConnection();
            }
            catch//(Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                "<script>$(document).ready(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>");
            }
        }
    }
}