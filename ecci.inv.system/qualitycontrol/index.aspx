<%@ Page Title="" Language="C#" MasterPageFile="~/qualitycontrol/QualityControl.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="ecci.inv.system.qualitycontrol.index" %>
<asp:Content ID="TS1" ContentPlaceHolderID="title" runat="server">
    Dashboard
</asp:Content>
<asp:Content ID="HS1" ContentPlaceHolderID="heading" runat="server">
     <script type="text/javascript">  
         $(document).ready(function () {
             //data-target="#updateModal" data-toggle="modal"<i class="fa fa-clipboard-edit"></i> data-target='#updateModal' data-toggle='modal'
         $.ajax({  
             type: "POST",  
             dataType: "json",
             url: "WebService/OrderDeliveryService.asmx/GetDeliveredOrder",
             success: function (data) {
                 var datatableVariable = $('#manageTable').DataTable({
                     data: data,
                     columns: [
                         {
                             'data': 'stockId', 'render': function (data, type, row) {
                                 return "<a  class='btn btn-primary btn-sm' onClick='ConfirmUpdate(" + data + ")'><i class='fa fa-edit'></i> Accept</a>";
                             },
                             orderable: false
                         },
                         { 'data': 'purchaseOrder' },
                         { 'data': 'suppName' },
                         { 'data': 'brandName' },
                         { 'data': 'quantity'},
                         { 'data': 'purchaseDate' },
                         { 'data': 'deliverDate' },
                         { 'data': 'poStatus' }
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
         $("#dashboardMainMenu").addClass('active');

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
            <h3 class="box-title">View Delivered Goods</h3>
          </div>
          <!-- /.box-header -->
          <div class="box-body">
            <table id="manageTable" class="table table-bordered table-striped">
              <thead>
              <tr>
                <th>Action</th>
                <th>PO#</th>
                <th>Supplier Name</th>
                <th>Brand Name</th>
                <th>Quantity</th>
                <th>Purchased Date</th>
                <th>Delivery Date</th>
                <th>Staus</th>
              </tr>
              </thead>
            </table>
          </div>
            <input type="hidden" id="hiddenStockId" />
            
            <asp:Label ID="lbError" runat="server" Text="Label"></asp:Label>
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
<!-- remove brand modal -->
<div class="modal fade" tabindex="-1"  role="dialog" id="updateModal">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title">Accept Items</h4>
      </div>

        <div class="modal-body">
            <div class="form-group">
                <label for="PO">Purchase Order Number</label>
                <input type="text" class="form-control" id="po" name="po" autocomplete="off"/>
            </div>
            <div class="form-group">
                <label for="supplier">Supplier</label>
                <input type="text" class="form-control" id="supplier" name="supplier" autocomplete="off" />
             </div>
             <div class="form-group">
                 <label for="brand">Brand</label>
                 <input type="text" class="form-control" id="brand" name="brand" autocomplete="off" />
             </div>
            <div class="form-group">
                <label for="qty">Quantity</label>
                <input type="text" class="form-control" id="qty" name="qty" autocomplete="off" />
            </div>
            <div class="form-group">
                <label for="pdate">Purchased Date</label>
                <input type="text" class="form-control" id="pdate" name="pdate" autocomplete="off" />
            </div>
            <div class="form-group">
                <label for="ddate">Delivery Date</label>
                <input type="text" class="form-control" id="ddate" name="ddate" autocomplete="off" />
            </div>

                
        </div>
        
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
           <%-- <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn btn-primary" OnClick="btnSave_Click"/>--%>
          <button type="submit" class="btn btn-primary" onclick="UpdateDelivery()">Receive Delivery</button>
        </div>


    </div><!-- /.modal-content -->
  </div><!-- /.modal-dialog -->
</div><!-- /.modal -->
         <script type="text/javascript">  

         function ConfirmUpdate(stockId)
         {
           $('#hiddenStockId').val(stockId);
          var sid = $('#hiddenStockId').val();
          $.ajax({
              type: "POST",
              dataType: "json",
              url: "WebService/OrderDeliveryService.asmx/ShowDelivered",
              data: { id: sid },
              success: function (data) {
                  $("#po").text(data.purchaseOrder);
                  $("#supplier").text(data.suppName);
                  $("#brand").text(data.brandName);
                  $("#qty").text(data.quantity);
                  $("#pdate").text(data.purchaseDate);
                  $("#ddate").text(data.deliverDate);
              }
          });
          $('#updateModal').modal('show');
         }
         var UpdateDelivery = function()
         {
             $('#updateModal').modal('hide');
         }
 </script>
</asp:Content>   
