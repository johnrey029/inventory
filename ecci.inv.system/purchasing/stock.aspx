<%@ Page Title="" Language="C#" MasterPageFile="~/purchasing/Purchasing.Master" AutoEventWireup="true" CodeBehind="stock.aspx.cs" Inherits="ecci.inv.system.purchasing.stock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Purchasing-Stock
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="heading" runat="server">
     <script type="text/javascript">  
     $(document).ready(function () {  
         $("#stockNav").addClass('active');
     });
 </script>  
    <script type="text/javascript">
        $(document).ready(function () {
            $('.alert-success').hide();
            $('.alert-error').hide();
        });
    </script>
 
<%--<script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/js/bootstrap-datepicker.js"></script>--%>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Wrapper. Contains page content -->
    
<%--                <div class="form-group">
                  <label for="batch">Batch Code</label>
                  <asp:TextBox ID="tbBatch" runat="server" CssClass="form-control" placeholder="Batch Code"></asp:TextBox>
                </div>--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        Stock Management
        <small>Purchasing</small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Stock Input</li>
      </ol>
    </section>
 <!-- Main content -->
  <section class="content">
    <!-- Small boxes (Stat box) -->
    <div class="row">
      <div class="col-md-12 col-xs-12">

        <div id="messages"></div>
          
          <div class="alert alert-success alert-dismissible" role="alert">
            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <strong>Succesfully</strong> Saved Order
        </div>
          
          <div class="alert alert-error alert-dismissible" role="alert">
            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
              <strong>Failed</strong> To Save Order
          </div>

        <div class="box">
          <div class="box-header">
            <h3 class="box-title">Stock Input</h3>
          </div>
          <!-- /.box-header -->
            <%-- <asp:TextBox ID="tbEdate" runat="server" CssClass="form-control" placeholder="Expected Delivery Date"></asp:TextBox>CausesValidation="false"--%>
              <div class="box-body">

<%--                <?php echo validation_errors(); ?>--%>
                <div class="form-group">
                  <label for="purchaseorder">Purchase Order Number</label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbPO" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="tbPO" runat="server" CssClass="form-control" placeholder="Purchase Order Number" autocomplete="off" ReadOnly="True" BackColor="White"></asp:TextBox>
                </div>
                <div class="form-group">
                  <label for="supplier">Supplier Name</label><span style="display:inline-block; width: 20px;"></span>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddSupplier" ErrorMessage="This field is required" ForeColor="Red" InitialValue="-1"></asp:RequiredFieldValidator>
                  <asp:DropDownList ID="ddSupplier" CssClass="form-control js-example-placeholder-single"  CausesValidation="false" runat="server" OnSelectedIndexChanged="ddSupplier_SelectedIndexChanged" AutoPostBack="True" Width="100%"></asp:DropDownList>
                </div>

                <div class="form-group">
                  <label for="brand">Brand Name</label><span style="display:inline-block; width: 20px;"></span>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddBrand" ErrorMessage="This field is required" ForeColor="Red" InitialValue="-1"></asp:RequiredFieldValidator>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                  <asp:DropDownList ID="ddBrand"  CssClass="form-control js-example-placeholder-single"  CausesValidation="false" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddBrand_SelectedIndexChanged" Width="100%" BackColor="White"></asp:DropDownList>
               </ContentTemplate> 
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddSupplier" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
               </div>

                <div class="form-group">
                  <label for="description">Description</label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbDescription" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                  <asp:TextBox ID="tbDescription" runat="server" CssClass="form-control" placeholder="Description" autocomplete="off" ReadOnly="true" BackColor="White"></asp:TextBox>
                         </ContentTemplate> 
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddBrand" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

                <div class="form-group">
                  <label for="quantity">Quantity</label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbQuantity" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                  <asp:TextBox ID="tbQuantity" runat="server" CssClass="form-control" placeholder="Quantity" autocomplete="off"></asp:TextBox>
                </div>
                  <div class="form-group">
                  <label for="price">Price</label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tbPrice" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                  <asp:TextBox ID="tbPrice" runat="server" CssClass="form-control" placeholder="Price" autocomplete="off"></asp:TextBox>
                </div>

<%--                </asp:TextBox> runat="server"--%>
                 <%-- <div class="form-group"><input type="text" id="tbEdate" class="form-control" placeholder="Expected Delivery Date (MM/DD/YYYY)" autocomplete="off"/>
                  <label for="deliverydate">Expected Delivery Date</label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tbEdate" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                  <input type="text" id="tbEdate" class="form-control" placeholder="Expected Delivery Date" autocomplete="off"/>
                  <asp:Calendar ID="tbCalendar" CssClass="form-control" placeholder="Expected Delivery Date" runat="server" OnDayRender="tbCalendar_DayRender" OnSelectionChanged="tbCalendar_SelectionChanged"></asp:Calendar>
                </div>--%>
                  <div class="form-group">
                  <label for="deliverydate">Expected Delivery Date</label>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tbEdate" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                <div class="input-group date" id="datetimepicker">
                    <asp:TextBox ID="tbEdate"  runat="server" CssClass="form-control" placeholder="Expected Delivery Date (MM/DD/YYYY)" autocomplete="off"></asp:TextBox>
                    <span class="input-group-addon">
                        <span class="glyphicon glyphicon-calendar"></span>
                    </span>
                </div>
            </div>

              </div>
              <!-- /.box-body -->

              <div class="box-footer">
                  <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn btn-primary" OnClick="btnSave_Click"/>
                  <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" CssClass="btn btn-warning" />
                  <asp:Label ID="lbError" runat="server" Text="Label" Visible="False"></asp:Label>
              </div>
        </div>
        <!-- /.box -->
      </div>
      <!-- col-md-12 -->
    </div>
    <!-- /.row -->
  </section>     
  </div>
    <script type="text/javascript">  
        $(document).ready(function () {
            $("#datetimepicker").datepicker({
                format: "mm/dd/yyyy",
                chageMonth: true,
                changeYear: true,
                startDate: "+0d"
            });
            
        });
 </script>  
</asp:Content>
