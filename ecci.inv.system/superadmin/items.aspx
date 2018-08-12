<%@ Page Title="" Language="C#" MasterPageFile="~/superadmin/Superadmin.Master" AutoEventWireup="true" CodeBehind="items.aspx.cs" Inherits="ecci.inv.system.superadmin.items" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Super Admin-Items
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Wrapper. Contains page content -->
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<%--    <script type="text/javascript">$(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>--%>
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        Items Management
        <small>Super Admin</small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Add Items</li>pu
      </ol>
    </section>
 <!-- Main content -->
  <section class="content">
    <!-- Small boxes (Stat box) -->
    <div class="row">
      <div class="col-md-12 col-xs-12">
          <div class="alert alert-success alert-dismissible" role="alert">
            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <strong>Succesfully</strong> Saved Item
        </div>
          
          <div class="alert alert-error alert-dismissible" role="alert">
            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
              <strong>Failed</strong> To Save Item
          </div>


        <div class="box">
          <div class="box-header">
            <h3 class="box-title">Add Items</h3>
          </div>
          <!-- /.box-header -->
          <%--<form role="form" action="#" method="post" enctype="multipart/form-data">--%>
              <div class="box-body">
                  
                  <%--<div class="form-group">
                  <label for="supplier">Supplier Name</label>
                  <select id="select1" class="form-control" role="list" runat="server">
                    <option value="">Select Supplier</option>
                  </select>
                </div>--%>
                  <div class="form-group">
                  <label for="supplier">Supplier Name</label><span style="display:inline-block; width: 20px;"></span>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddSupplier" ErrorMessage="This field is required" ForeColor="Red" InitialValue="-1"></asp:RequiredFieldValidator>
                  <asp:DropDownList ID="ddSupplier"  CssClass="form-control js-example-placeholder-single" CausesValidation="false" runat="server"></asp:DropDownList>
                 </div>
                  
                <div class="form-group">
                  <label for="brand">Brand Name</label> <span style="display:inline-block; width: 20px;"></span>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbBrand" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                    
                    <asp:TextBox ID="tbBrand" runat="server" CssClass="form-control" placeholder="Brand Name" CausesValidation="false" autocomplete="off"></asp:TextBox>
                     
                        </div>
                
                <div class="form-group">
                  <label for="description">Description</label><span style="display:inline-block; width: 20px;"></span>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbDescription" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                  <asp:TextBox ID="tbDescription" runat="server" CssClass="form-control" placeholder="Description" CausesValidation="false" autocomplete="off"></asp:TextBox>
                </div>
                  <div class="form-group">
                  <label for="description">Unit Price</label><span style="display:inline-block; width: 20px;"></span>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbUnitPrice" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                  <asp:TextBox ID="tbUnitPrice" runat="server" CssClass="form-control" placeholder="Unit Price  " CausesValidation="false" autocomplete="off"></asp:TextBox>
                </div>
              </div>
                
              <!-- /.box-body -->

              <div class="box-footer">
                  <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn btn-primary" OnClick="btnSave_Click1"/>
                  <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-warning" CausesValidation="false" OnClick="btnBack_Click1"/>
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

<asp:Content ID="Content3" ContentPlaceHolderID="heading" runat="server">
<script type="text/javascript">  
     $(document).ready(function () {  
         $("#mainItemNav").addClass('active');
         $("#addItemsNav").addClass('active');
  });  
 </script>  
</asp:Content>