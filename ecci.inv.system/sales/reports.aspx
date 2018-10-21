<%@ Page Title="" Language="C#" MasterPageFile="~/sales/Sales.Master" AutoEventWireup="true" CodeBehind="reports.aspx.cs" Inherits="ecci.inv.system.sales.report" %>
<asp:Content ID="TS1" ContentPlaceHolderID="title" runat="server">
    Reports
</asp:Content>
<asp:Content ID="HS1" ContentPlaceHolderID="heading" runat="server">
     <script type="text/javascript">  
         $(document).ready(function () {
             var datatableVariable;
             //data-target="#updateModal" data-toggle="modal"<i class="fa fa-clipboard-edit"></i> data-target='#updateModal' data-toggle='modal'
         $.ajax({  
             type: "POST",  
             dataType: "json",
             url: "WebService/ProductOrderService.asmx/GetPurchaseOrder",
             success: function (data) {
                 datatableVariable = $('#manageTable').DataTable({
                     data: data,
                     columns: [
                         { 'data': 'orderid' },
                         { 'data': 'client' },
                         { 'data': 'product' },
                         { 'data': 'price' },
                         { 'data': 'quantity'},
                         { 'data': 'amount' },
                         { 'data': 'date' },
                         { 'data': 'status' }
                     ],
                     language: {
                         emptyTable: "No data found!"
                     }
                 });
             },
             bServerSide: true,
             error: function (err) {
                 alert(err);
             }
         });
         $("#reportNav").addClass('active');

         });
 </script>  
</asp:Content>

 <asp:Content ID="CS1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
  <div class="content-wrapper">
  <!-- Content Header (Page header) -->
  <section class="content-header">
    <h1>
      Manage
      <small>Delivery</small>
    </h1>
    <ol class="breadcrumb">
      <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
      <li class="active">Delivery</li>
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
              <strong>Succesfully</strong> Update Received Delivery
          </div>
          <div class="alert alert-error alert-dismissible" role="alert">
            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
              <strong>Failed in Processing</strong> Delivery Update
          </div>
          <div class="alert alert-warning alert-dismissible" role="alert">
              <button class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
              <strong>Incorrect Input! </strong><asp:Label ID="lbWarning" Text="" runat="server"></asp:Label><strong> Try Again!</strong>
          </div>

      <%--  <?php if(in_array('createProduct', $user_permission)): ?>
          <a href="<%--<?php echo base_url('products/create') ?>AddProducts.aspx" class="btn btn-primary">Add Product</a>
          <br /> <br />--%>
<%--        <?php endif; ?>--%>

        <div class="box">
          <div class="box-header">
            <h3 class="box-title">View Ordered History</h3>
          </div>
          <!-- /.box-header -->
          <div class="box-body">
            <table id="manageTable" class="table table-bordered table-striped" style=" width: 100%">
              <thead>
              <tr>
                <th>SO</th>
                <th>Client</th>
                <th>Product</th>
                <th>Price</th>
                <th>Quantity</th>
                <th>Amount</th>
                <th>Date</th>
                <th>Status</th>
              </tr>
              </thead>
            </table>
          </div>
            <input type="hidden" id="hiddenStockId"  name="hiddenStockId" value="" />
          <!-- /.box-body -->
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
<!-- Update raw stock modal -->
<script type="text/javascript">  

            
         //var UpdateDelivery = function()
         //{
         //    var sid = $('#hiddenStockId').val();
         //    $('#updateModal').modal('hide');
         //    $.ajax({
         //        url: "WebService/OrderDeliveryService.asmx/UpdateById",
         //        data: { upid: sid },
         //        type: "POST",
         //        dataType: "xml",
         //        success: function (data) {
         //            datatableVariable.ajax.reload(null, false);
         //            if (data == 1) {
         //               $("#messages").html('<div class="alert alert-success alert-dismissible" role="alert">' +
         //     '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
         //    '<strong> <span class="glyphicon glyphicon-ok-sign"></span> </strong>' + data + '</div>');
         //            }
         //        },
         //        error: function (err) {
         //            $('.alert-success').hide(); $('.alert-error').show();
         //        }
         //    });
         //}
 </script>
</asp:Content>   

