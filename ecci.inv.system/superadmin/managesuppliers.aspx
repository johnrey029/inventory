<%@ Page Title="" Language="C#" MasterPageFile="~/superadmin/Superadmin.Master" AutoEventWireup="true" CodeBehind="managesuppliers.aspx.cs" Inherits="ecci.inv.system.superadmin.managesuppliers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Super Admin-Manage Suppliers
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="heading" runat="server">
     <script type="text/javascript">  
     $(document).ready(function () {  
         $.ajax({  
             type: "POST",  
             dataType: "json",  
             url: "WebService/ManageSupplierService.asmx/GetSupplier",
             success: function (data) {
                 var datatableVariable = $('#manageTable').DataTable({
                     data: data,
                     columns: [
                         { 'data': 'suppCode' },
                         { 'data': 'suppName' },
                         { 'data': 'suppAdd' },
                         { 'data': 'suppContact' }
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
         $("#mainSupNav").addClass('active');
         $("#manageSupNav").addClass('active');
  });  
 </script>  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">
  <!-- Content Header (Page header) -->
  <section class="content-header">
    <h1>
      Manage
      <small>Suppliers</small>
    </h1>
    <ol class="breadcrumb">
      <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
      <li class="active">Suppliers</li>
    </ol>
  </section>

  <!-- Main content -->
  <section class="content">
    <!-- Small boxes (Stat box) -->
    <div class="row">
      <div class="col-md-12 col-xs-12">
          <a href="supplier.aspx" class="btn btn-primary">Add Suppliers</a>
            <br /> <br />
        <div class="box">
          <div class="box-header">
            <h3 class="box-title">List of Suppliers</h3>
          </div>
          <!-- /.box-header -->
          <div class="box-body">
              
            <table id="manageTable" class="table table-bordered table-striped" style=" width: 100%">
              <thead>
              <tr>
                <%--<th>PO#</th>--%>
                <th>Supplier Code</th>
                <th>Company</th>
                <th>Address</th>
                  <th>Contact Person</th>
                <%--<th>Purchased Date</th>
                <th>Delivery Date</th>
                <th>Staus</th>--%>
              </tr>
              </thead>
<%--                <tbody>
              <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
               </tbody>--%>
            </table>
          </div>
            
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
<%--
<?php if(in_array('deleteProduct', $user_permission)): ?>--%>
<!-- remove brand modal -->
<%--<div class="modal fade" tabindex="-1" role="dialog" id="removeModal">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title">Remove Product</h4>
      </div>

      <form role="form" action="#" method="post" id="removeForm">
        <div class="modal-body">
          <p>Do you really want to remove?</p>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
          <button type="submit" class="btn btn-primary">Save changes</button>
        </div>
      </form>


    </div><!-- /.modal-content -->
  </div><!-- /.modal-dialog -->
</div><!-- /.modal -->--%>
</asp:Content>
