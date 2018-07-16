﻿<%@ Page Title="" Language="C#" MasterPageFile="~/qualitycontrol/QualityControl.Master" AutoEventWireup="true" CodeBehind="dispatching.aspx.cs" Inherits="ecci.inv.system.qualitycontrol.dispatching" %>
<asp:Content ID="TS1" ContentPlaceHolderID="title" runat="server">
    Dispatching
</asp:Content>
<asp:Content ID="HS1" ContentPlaceHolderID="heading" runat="server">
     <script type="text/javascript">  
         $(document).ready(function () {
             var datatableVariable;
             //data-target="#updateModal" data-toggle="modal"<i class="fa fa-clipboard-edit"></i> data-target='#updateModal' data-toggle='modal'
         $.ajax({  
             type: "POST",  
             dataType: "json",
             url: "WebService/DispatchingDeliveryService.asmx/DispatchDelivery",
             success: function (data) {
                     datatableVariable = $('#manageTable').DataTable({
                     data: data,
                     columns: [
                         {
                             'data': 'stockId', 'render': function (data, type, row) {
                                 return "<a  class='btn btn-primary btn-sm' onClick='ConfirmUpdate(" + data + ")'><i class='fa fa-paper-plane'></i>  Dispatch</a>";
                             },
                             orderable: false
                         },
                         { 'data': 'purchaseOrder' },
                         { 'data': 'suppName' },
                         { 'data': 'brandName' },
                         { 'data': 'quantity'},
                         { 'data': 'purchaseDate' },
                         { 'data': 'receivedDate' },
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
         $("#dispatchNav").addClass('active');

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
            <table id="manageTable" class="table table-bordered table-striped" style=" width: 100%">
              <thead>
              <tr>
                <th>Action</th>
                <th>PO#</th>
                <th>Supplier Name</th>
                <th>Brand Name</th>
                <th>Quantity</th>
                <th>Purchased Date</th>
                <th>Received Date</th>
                <th>Staus</th>
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
</div>
<div class="modal fade" tabindex="-1"  role="dialog" id="dispatchModal">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header bg-aqua-active">
        <button type="button" class="close active" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title">Dispatch Raw Materials</h4>
      </div>

        <div class="modal-body">
            <div class="form-group">
                <label for="PO">Purchase Order Number</label>
                <input type="text" class="form-control" id="po" name="po" readonly="true"/>
            </div>
            <div class="form-group">
                <label for="supplier">Supplier</label>
                <input type="text" class="form-control" id="supplier" name="supplier" readonly="true"/>
             </div>
             <div class="form-group">
                 <label for="brand">Brand</label>
                 <input type="text" class="form-control" id="brand" name="brand" readonly="true"/>
             </div>
            <div class="form-group">
                <label for="qty">Total Quantity</label>
                <input type="text" class="form-control" id="qty" name="qty" readonly="true"/>
            </div>
            <div class="form-group">
                <label for="pdate">Purchased Date</label>
                <input type="text" class="form-control" id="pdate" name="pdate" readonly="true"/>
            </div>
            <div class="form-group">
                <label for="ddate">Delivery Date</label>
                <input type="text" class="form-control" id="ddate" name="ddate"readonly="true"/>
            </div>
            <div class="form-group">
                <label for="pquan">Quantity Passed</label>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbPquan" ErrorMessage="Input a quantity" ForeColor="Red"></asp:RequiredFieldValidator>
                 <asp:TextBox ID="tbPquan" CssClass="form-control"  runat="server" placeholder="Quantity Number Passed" autocomplete="off"></asp:TextBox>
                
            </div>
            <div class="form-group">
                <label for="fquan">Quantity Failed</label>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbFquan" ErrorMessage="Input a quantity" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:TextBox ID="tbFquan" CssClass="form-control"  runat="server" placeholder="Quantity Number Failed" autocomplete="off"></asp:TextBox>
                <%--<input type="text" class="form-control" id="fquan" name="ddate"/>
                <input type="text" class="form-control" id="pquan" name="pdate"/>--%>
            </div>
            <div class="alert alert-warning alert-dismissible" role="alert">
            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
              <strong>Not Equal</strong>
          </div>
        </div>
        <div class="modal-footer bg-aqua-active">
            
          <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
          <asp:Button ID="btnSave" runat="server" Text="Dispatch Raw Materials" CssClass="btn btn-success" Onclick="btnSave_Click"/>
          <%--<button type="submit" class="btn btn-primary" onclick="UpdateDelivery()">Receive Delivery</button>--%>
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
              url: "WebService/OrderDeliveryService.asmx/ShowDeliveredById",
              data: { id: sid },
              type: "POST",
              dataType: "json",
              success: function (data) {
                      $('#po').val(data.purchaseOrder);
                      $('#supplier').val(data.suppName);
                      $('#brand').val(data.brandName);
                      $('#qty').val(data.quantity);
                      $('#pdate').val(data.purchaseDate);
                      $('#ddate').val(data.deliverDate);
              },
              error: function (err) {
                  alert(err);
              }
          });
          $('#dispatchModal').modal('show');
         }
 </script>
</asp:Content>
