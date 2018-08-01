<%--<%@ Page Title="" Language="C#" MasterPageFile="~/purchasing/Purchasing.Master" AutoEventWireup="true" CodeBehind="supplier.aspx.cs" Inherits="ecci.inv.system.purchasing.supplier" %>--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="supplier.aspx.cs" Inherits="ecci.inv.system.purchasing.supplier" MasterPageFile="~/purchasing/Purchasing.Master"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Purchasing-Supplier
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="heading" runat="server">
     <script type="text/javascript">  
     $(document).ready(function () {  
         $("#mainSupNav").addClass('active');
         $("#addSupNav").addClass('active');
  });  
 </script>  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <!-- Content Wrapper. Contains page content -->
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<%--    <script type="text/javascript">$(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>--%>
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        Supplier Management
        <small>Purchasing</small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Add Supplier</li>
      </ol>
    </section>
 <!-- Main content -->
  <section class="content">
    <!-- Small boxes (Stat box) -->
    <div class="row">
      <div class="col-md-12 col-xs-12">

        <div class="alert alert-success alert-dismissible" role="alert">
            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <strong>Succesfully</strong> Saved Supplier
        </div>
          
          <div class="alert alert-error alert-dismissible" role="alert">
            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
              <strong>Failed</strong> To Save Supplier
          </div>

        <%--<?php if($this->session->flashdata('success')): ?>
          <?php echo $this->session->flashdata('success'); ?>
          </div>
        <?php elseif($this->session->flashdata('error')): ?>
            <?php echo $this->session->flashdata('error'); ?
          </div>
        <?php endif; ?>--%>


        <div class="box">
          <div class="box-header">
            <h3 class="box-title">Add Supplier</h3>
          </div>
          <!-- /.box-header -->
          <%--<form role="form" action="#" method="post" enctype="multipart/form-data">--%>
              <div class="box-body">
                  
               
<%--                <?php echo validation_errors(); ?>--%>
                  <div class="form-group">
                  <label for="suppcode">Supplier Code</label><span style="display:inline-block; width: 20px;"></span>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbSuppCode" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="tbSuppCode" runat="server" CssClass="form-control" placeholder="Supplier Code" autocomplete="off"></asp:TextBox>
                </div>

                <div class="form-group">
                  <label for="suppname">Supplier Name</label><span style="display:inline-block; width: 20px;"></span>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbSuppName" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                  <asp:TextBox ID="tbSuppName" runat="server" CssClass="form-control" placeholder="Supplier Name" autocomplete="off"></asp:TextBox>
                </div>

                <div class="form-group">
                  <label for="suppaddress">Supplier Address</label><span style="display:inline-block; width: 20px;"></span>
                    
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbSuppAddress" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                  <asp:TextBox ID="tbSuppAddress" runat="server" CssClass="form-control" placeholder="Supplier Address" autocomplete="off"></asp:TextBox>
                </div>
              </div>
              <!-- /.box-body -->

              <div class="box-footer">
                  <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn btn-primary" OnClick="btnSave_Click"/>
                  <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-warning" CausesValidation="false" OnClick="btnBack_Click"  />
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
