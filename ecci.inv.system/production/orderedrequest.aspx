<%@ Page Title="" Language="C#" MasterPageFile="~/production/Production.Master" AutoEventWireup="true" CodeBehind="orderedrequest.aspx.cs" Inherits="ecci.inv.system.production.orderedrequest" %>
<asp:Content ID="TS1" ContentPlaceHolderID="title" runat="server">
    Production
</asp:Content>
<asp:Content ID="HS1" ContentPlaceHolderID="heading" runat="server">
     <script type="text/javascript">  
         $(document).ready(function () {
         //    var datatableVariable;
         //    //data-target="#updateModal" data-toggle="modal"<i class="fa fa-clipboard-edit"></i> data-target='#updateModal' data-toggle='modal'
         //$.ajax({  
         //    type: "POST",  
         //    dataType: "json",
         //    url: "WebService/ProdRaw.asmx/GetProductRaw",
         //    success: function (data) {
         //        datatableVariable = $('#manageTable').DataTable({
         //            data: data,
         //            columns: [
         //                {
         //                    'data': 'id', 'render': function (data, type, row) {
         //                        return "<a  class='btn btn-primary btn-sm' onClick='ConfirmUpdate(" + data + ")'><i class='fa fa-truck'></i>  Request</a>";
         //                    },
         //                    orderable: false
         //                },
         //                { 'data': 'purchaseOrder' },
         //                { 'data': 'suppName' },
         //                { 'data': 'brandName' },
         //                { 'data': 'quantity'},
         //                { 'data': 'receivedDate' },
         //                { 'data': 'status' }
         //            ],
         //            language: {
         //                emptyTable: "No Request Available!"
         //            }
         //        });
         //    },
         //    bServerSide: true,
         //    error: function (err) {
         //        alert(err);
         //    }
         //});
         $("#requestNav").addClass('active');
         });
         function expandcollapse(name) {
             var div = document.getElementById(name);
             var img = document.getElementById('img' + name);
             if (div.style.display=="none") {
                 div.style.display = "inline";
                 img.src = "../Images/minus.png";
             }
             else {
                 div.style.display = "none";
                 img.src = "../Images/plus.png";
             }
         }
 </script>  
</asp:Content>
<asp:Content ID="CS1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
  <div class="content-wrapper">
  <!-- Content Header (Page header) -->
  <section class="content-header">
    <h1>
      Manage Raw Materials
      <small>Request</small>
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
            <h3 class="box-title">View Raw Materials</h3>
          </div>
          <!-- /.box-header --><%-- --%>
          <div class="box-body">
              <asp:GridView ID="GridView1" CssClass="table table-bordered table-striped" runat="server" 
                  AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound" 
                  DataKeyNames="uniqueid">
                  <Columns>
                      <asp:TemplateField>
                          <ItemTemplate>
                              <a href="JavaScript:expandcollapse('<%#Eval("uniqueid") %>');">
                                 <img src="../Images/plus.png" class="img-sm" border="0" id='img<%#Eval("uniqueid") %>' />
                              </a>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="name" HeaderText="Customer Name" />
                      <asp:BoundField DataField="pname" HeaderText="Product Name" />
                      <asp:BoundField DataField="quantityordered" HeaderText="Qty" />
                      <asp:BoundField DataField="price" HeaderText="Price" />
                      <asp:BoundField DataField="amount" HeaderText="Total Amount" />
                      <asp:BoundField DataField="date" HeaderText="Date"/>
                      <%--<asp:BoundField DataField="status"  />--%>
                      <asp:TemplateField HeaderText="Status">
                          <ItemTemplate>
                              <%#Eval("status") %>
                              <tr>
                                  <td>
                                      <div id='<%#Eval("uniqueid") %>' style="display:none">
                                          <asp:GridView ID="GridView2" CssClass="table table-bordered table-striped" runat="server" Width="100%" AutoGenerateColumns="false">
                                                 <Columns>
                                                     <asp:BoundField DataField="brandname" HeaderText="Material Name" />
                                                     <asp:BoundField DataField="quantityordered" HeaderText="Qty" />
                                                     <asp:BoundField DataField="price" HeaderText="Price"/>
                                                 </Columns>
                                          </asp:GridView>
                                      </div>

                                  </td>
                              </tr>
                          </ItemTemplate>
                      </asp:TemplateField>
                  </Columns>
                  
              </asp:GridView>
            <%--<table id="manageTable" class="table table-bordered table-striped" style=" width: 100%">
              <thead>
              <tr>
                <th>Action</th>
                <th>PO#</th>
                <th>Supplier Name</th>
                <th>Brand Name</th>
                <th>Stock Quantity</th>
                <th>Stock Date</th>
                <th>Status</th>
              </tr>
              </thead>
            </table>--%>
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
        <h4 class="modal-title">Releasing Of Raw Materials</h4>
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
                <label for="sdate">Stock Date</label>
                <input type="text" class="form-control" id="sdate" name="sdate" readonly="true"/>
            </div>
            <div class="form-group">
                <label for="qty">Stock Quantity</label>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="qty" ErrorMessage="Please input a Quantity" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                    <ContentTemplate>--%>
                <asp:TextBox ID="qty" CssClass="form-control"  runat="server" ReadOnly="true"></asp:TextBox>
            <%--</ContentTemplate> 
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="qty" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
            </div>
            <%--<div class="form-group">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Inline">
                    <ContentTemplate>
             <asp:Label ID="lbError" runat="server" Text="Receiving Total Quantity" Visible="true" ForeColor="Green"></asp:Label>
                        </ContentTemplate> 
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="qty" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>--%>
            <input type="hidden" id="hiddenquantity"  name="hiddenquantity" value="" />
                
        </div>
        
        <div class="modal-footer bg-aqua-active">
          <asp:Button ID="btnSave" runat="server" Text="Release Raw Materials" CssClass="btn btn-success"/>
          <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="myFunction()">Cancel</button>
           <%-- OnClick="btnSave_Click"  UseSubmitBehavior="false" OnClientClick="if ( Page_ClientValidate() ) { this.value='Receiving...'; this.disabled='false'; }"--%>
          <%--<button type="submit" class="btn btn-primary" onclick="UpdateDelivery()">Receive Delivery</button>--%>
             <%-- <input type="text" class="form-control" id="qty" name="qty" readonly="false"/>--%> 
        </div>


    </div><!-- /.modal-content -->
  </div><!-- /.modal-dialog -->
</div><!-- /.modal -->
    <script type="text/javascript">  
 </script>
</asp:Content>
