<%@ Page Title="" Language="C#" MasterPageFile="~/purchasing/Purchasing.Master" AutoEventWireup="true" CodeBehind="stock.aspx.cs" Inherits="ecci.inv.system.purchasing.stock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="heading" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Wrapper. Contains page content -->
    
<%--    <script type="text/javascript">$(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>--%>
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
          <%--<form role="form" action="#" method="post" enctype="multipart/form-data">--%>
              <div class="box-body">

<%--                <?php echo validation_errors(); ?>--%>
                <div class="form-group">
                  <label for="purchaseorder">Purchase Order Number</label>
                    <asp:TextBox ID="tbPO" runat="server" CssClass="form-control" placeholder="Purchase Order Number"></asp:TextBox>
                </div>

                <div class="form-group">
                  <label for="supplier">Supplier Name</label><span style="display:inline-block; width: 20px;"></span>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddSupplier" ErrorMessage="This field is required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                  <asp:DropDownList ID="ddSupplier"  CssClass="form-control js-example-placeholder-single" CausesValidation="false" runat="server" OnSelectedIndexChanged="ddSupplier_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                </div>

                <div class="form-group">
                  <label for="brand">Brand Name</label><span style="display:inline-block; width: 20px;"></span>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddBrand" ErrorMessage="This field is required" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                  <asp:DropDownList ID="ddBrand"  CssClass="form-control js-example-placeholder-single" CausesValidation="false" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddBrand_SelectedIndexChanged"></asp:DropDownList>
               </ContentTemplate> 
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddSupplier" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
               </div>

                <div class="form-group">
                  <label for="description">Description</label>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                  <asp:TextBox ID="tbDescription" runat="server" CssClass="form-control" placeholder="Description"></asp:TextBox>
                         </ContentTemplate> 
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddBrand" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

                <div class="form-group">
                  <label for="quantity">Quantity</label>
                  <asp:TextBox ID="tbQuantity" runat="server" CssClass="form-control" placeholder="Quantity"></asp:TextBox>
                </div>
<%--                <div class="form-group">
                  <label for="batch">Batch Code</label>
                  <asp:TextBox ID="tbBatch" runat="server" CssClass="form-control" placeholder="Batch Code"></asp:TextBox>
                </div>--%>
                  <div class="form-group">
                  <label for="deliverydate">Expected Delivery Date</label>
                  <asp:TextBox ID="tbEdate" runat="server" CssClass="form-control" placeholder="Expected Delivery Date"></asp:TextBox>
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
</asp:Content>
