<%@ Page Title="" Language="C#" MasterPageFile="~/warehouse/WareHouse.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="ecci.inv.system.warehouse.index" %>
<asp:Content ID="TS1" ContentPlaceHolderID="title" runat="server">
    Warehouse
</asp:Content>
<asp:Content ID="HS1" ContentPlaceHolderID="heading" runat="server">
     <script type="text/javascript">  
         $(document).ready(function () {
             var datatableVariable;
             //data-target="#updateModal" data-toggle="modal"<i class="fa fa-clipboard-edit"></i> data-target='#updateModal' data-toggle='modal'
         $.ajax({  
             type: "POST",  
             dataType: "json",
             url: "WebService/WarehouseRaw.asmx/GetRawMaterials",
             success: function (data) {
                 datatableVariable = $('#manageTable').DataTable({
                     data: data,
                     columns: [
                         { 'data': 'purchaseOrder' },
                         { 'data': 'suppName' },
                         { 'data': 'brandName' },
                         { 'data': 'quantity'},
                         { 'data': 'receivedDate' },
                         { 'data': 'status' }
                     ],
                     language: {
                         emptyTable: "No Available Stock!"
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
            <h3 class="box-title">View Delivered Goods</h3>
          </div>
          <!-- /.box-header -->
          <div class="box-body">
            <table id="manageTable" class="table table-bordered table-striped" style=" width: 100%">
              <thead>
              <tr>
                <th>PO#</th>
                <th>Supplier Name</th>
                <th>Brand Name</th>
                <th>Stock Quantity</th>
                <th>Received Date</th>
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
<div class="modal fade" tabindex="-1"  role="dialog" id="updateModal">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header bg-aqua-active">
        <button type="button" class="close active" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title">Accept Delivery Items</h4>
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
            <%--<div class="form-group">
                <label for="pdate">Purchased Date</label>
                <input type="text" class="form-control" id="pdate" name="pdate" readonly="true"/>
            </div>--%>
            <div class="form-group">
                <label for="rdate">Received Date</label>
                <input type="text" class="form-control" id="ddate" name="ddate"readonly="true"/>
            </div>
            <div class="form-group">
                <label for="qty">Stock Quantity</label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="qty" ErrorMessage="Please input a Quantity" ForeColor="Red"></asp:RequiredFieldValidator>
                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                    <ContentTemplate>--%>
                <asp:TextBox ID="qty" CssClass="form-control"  runat="server" placeholder="Input Quantity Receive" autocomplete="off" min="0" AutoPostBack="True"  onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);"></asp:TextBox>
            <%--</ContentTemplate> 
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="qty" EventName="TextChanged" />
                        </Triggers>OnTextChanged="qty_TextChanged" 
                    </asp:UpdatePanel>--%>
            </div>
            <div class="form-group">
              <%--  <asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Inline">
                    <ContentTemplate>--%>
             <asp:Label ID="lbError" runat="server" Text="Receiving Total Quantity" Visible="true" ForeColor="Green"></asp:Label>
               <%--         </ContentTemplate> 
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="qty" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
                </div>
            <input type="hidden" id="hiddenquantity"  name="hiddenquantity" value="" />
                
        </div>
        
        <div class="modal-footer bg-aqua-active">
          <asp:Button ID="btnSave" runat="server" Text="Receive Order" CssClass="btn btn-success"/>
          <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="myFunction()">Cancel</button>
           <%-- OnClick="btnSave_Click"  UseSubmitBehavior="false" OnClientClick="if ( Page_ClientValidate() ) { this.value='Receiving...'; this.disabled='false'; }"--%>
          <%--<button type="submit" class="btn btn-primary" onclick="UpdateDelivery()">Receive Delivery</button>--%>
             <%-- <input type="text" class="form-control" id="qty" name="qty" readonly="false"/>--%> 
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
              url: "WebService/WarehouseRaw.asmx/ShowDeliveredById",
              data: { id: sid },
              type: "POST",
              dataType: "json",
              success: function (data) {
                      $('#po').val(data.purchaseOrder);
                      $('#supplier').val(data.suppName);
                      $('#brand').val(data.brandName);
                      //$('#qty').val(data.quantity);
                     // $('#pdate').val(data.purchaseDate);
                      $('#ddate').val(data.receiveDate);
                      document.getElementById('<%=qty.ClientID %>').value = data.quantity;
                  $('#hiddenquantity').val(data.quantity);
                  
              },
              error: function (err) {
                  alert(err);
              }
          });
             $('#updateModal').modal('show');
             document.getElementById("<%=qty.ClientID%>").style.borderColor = 'Green';
             document.getElementById("<%=qty.ClientID%>").style.color = 'Black';
             document.getElementById("<%=lbError.ClientID%>").style.color = 'Green';
             document.getElementById("<%=lbError.ClientID%>").innerHTML = 'Receiving Total Quantity';
         }
         function myFunction() {
             document.getElementById("<%=lbError.ClientID%>").innerHTML = 'Receiving Total Quantity';
             document.getElementById("<%=btnSave.ClientID%>").disabled = false;
         }
 </script>
</asp:Content>
