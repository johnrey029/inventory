<%@ Page Title="" Language="C#" MasterPageFile="~/purchasing/Purchasing.Master" AutoEventWireup="true" CodeBehind="manageitems.aspx.cs" Inherits="ecci.inv.system.purchasing.manageitems" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="heading" runat="server">
<script type="text/javascript">  
     $(document).ready(function () {  
         $.ajax({  
             type: "POST",  
             dataType: "json",  
             url: "WebService/ItemsService.asmx/GetItem",
             success: function (data) {
                 var datatableVariable = $('#mgTable').DataTable({
                     data: data,
                     columns: [
                         { 'data': 'brandName' },
                         { 'data': 'description' }
                     ]
                 });
             }
         });

         $("#mainItemNav").addClass('active');
         $("#manageItemsNav").addClass('active');
  });  
</script>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         
  <div class="content-wrapper">
  <!-- Content Header (Page header) -->
  <section class="content-header">
    <h1>
      Manage
      <small>Items</small>
    </h1>
    <ol class="breadcrumb">
      <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
      <li class="active">Items</li>
    </ol>
  </section>

  <!-- Main content -->
  <section class="content">
    <!-- Small boxes (Stat box) -->
    <div class="row">
      <div class="col-md-12 col-xs-12">
  <%--       <div id="messages"></div>
       <?php if($this->session->flashdata('success')): ?>
          <div class="alert alert-success alert-dismissible" role="alert">
            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <?php echo $this->session->flashdata('success'); ?>
          </div>
        <?php elseif($this->session->flashdata('error')): ?>
          <div class="alert alert-error alert-dismissible" role="alert">
            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <?php echo $this->session->flashdata('error'); ?>
          </div>
        <?php endif; ?>--%>

      <%--  <?php if(in_array('createProduct', $user_permission)): ?>
          <a href="<%--<?php echo base_url('products/create') ?>AddProducts.aspx" class="btn btn-primary">Add Product</a>
          <br /> <br />--%>
<%--        <?php endif; ?>--%>

        <div class="box">
          <div class="box-header">
            <h3 class="box-title">View Item List</h3>
          </div>
          <!-- /.box-header -->
          <div class="box-body">
              
            <table id="mgTable" class="table table-bordered table-striped">
              <thead>
              <tr>
                <th>Brand Name</th>
                <th>Description</th>
              </tr>
              </thead>
<%--                <tbody>
              <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
               </tbody>--%>
            </table>
          </div>
            
           <!--  <asp:Label ID="lbError" runat="server" Text="Label"></asp:Label>
          /.box-body -->
        </div>
        <!-- /.box -->
      </div>
      <!-- col-md-12 -->
    </div>

    <!-- /.row -->
  </section>
  <!-- /.content -->
</div>
<!-- /.content-wrapper -->
</asp:Content>
