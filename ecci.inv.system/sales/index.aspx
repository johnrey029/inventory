<%@ Page Title="" Language="C#" MasterPageFile="~/sales/Sales.Master" AutoEventWireup="true"  CodeBehind="index.aspx.cs" Inherits="ecci.inv.system.sales.index" %>

<asp:Content ID="TS1" ContentPlaceHolderID="title" runat="server">
    Purchase Order
</asp:Content>
<asp:Content ID="HS1" ContentPlaceHolderID="heading" runat="server">
     <script type="text/javascript">  
        $(document).ready(function () {
         //    //var resourceAdress = '@Url.Content("~/WebService/PurchaseOrderService.asmx/GetPurchaseOrder")';
         //$.ajax({  
         //    type: "POST",  
         //    dataType: "json",
         //    url: "WebService/PurchaseOrderService.asmx/GetPurchaseOrder",
         //    //url: '@Url.Action("WebService", "PurchaseOrderService.asmx", "GetPurchaseOrder")',
         //    //url: resourceAdress,
         //    success: function (data) {
         //        var datatableVariable = $('#manageTable').DataTable({
         //            data: data,
         //            columns: [
         //                { 'data': 'purchaseOrder' },
         //                { 'data': 'suppName' },
         //                { 'data': 'brandName' },
         //                { 'data': 'quantity'},
         //                { 'data': 'purchaseDate' },
         //                { 'data': 'deliverDate' },
         //                { 'data': 'poStatus' }
         //            ]
         //        });
         //    },
         //    bServerSide: true,
         //    error: function (err) {
         //        alert(err);
         //    }
         //});
         
            $("#purchaseNav").addClass('active');
     });

 </script> 
</asp:Content>

 <asp:Content ID="CS1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
  <div class="content-wrapper">
  <!-- Content Header (Page header) -->
  <section class="content-header">
    <h1>
      Mange
      <small>Orders</small>
    </h1>
    <ol class="breadcrumb">
      <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
      <li class="active">Orders</li>
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
                        <strong>Failed</strong> Failed To Save Order
                    </div>
                    
          <div class="box">
          <div class="box-header">
            <h3 class="box-title">Add Order</h3>
          </div>

         <div class="box-body">
<%--                 <div class="form-group pull pull-right">
                  <label for="gross_amount" class="col-sm-12 control-label">Time: </label>
                    <asp:Label ID="lbTime" runat="server" Text="Label" CssClass="col-sm-12 control-label"></asp:Label>
                </div>--%>
                <div class="form-group pull pull-right">
                 <label for="gross_amount" class="col-sm-12 control-label">Date: </label>
                    <asp:Label ID="lbDate" runat="server" Text="Label" CssClass="col-sm-12 control-label"></asp:Label>
                </div>
                <div class="col-md-4 col-xs-12 pull pull-left">

                  <div class="form-group">
                    <label for="customer" <%--class="col-sm-5 control-label"--%> style="text-align:left;">Customer Name</label>
                   <%-- <div class="col-sm-7">--%>
                      <%--<input type="text" class="form-control" id="customer_name" name="customer_name" placeholder="Enter Customer Name" autocomplete="off" />--%>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddCustomer" ErrorMessage="This field is required" ForeColor="Red" InitialValue="-1"></asp:RequiredFieldValidator>
                      <asp:DropDownList ID="ddCustomer" CssClass="form-control js-example-placeholder-single" CausesValidation="false" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddCustomer_SelectedIndexChanged" Width="100%"></asp:DropDownList>
                                    
                       <%--</div>--%>
                  </div>
                  <div class="form-group">
                    <label for="address" <%--class="col-sm-5 control-label"--%> style="text-align:left;">Customer Address</label>
                    <%--<div class="col-sm-7">--%>
                      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                       <asp:TextBox ID="tbAddress" runat="server" CssClass="form-control" placeholder="Auto Fill Customer Address" autocomplete="off" ReadOnly="true" BackColor="White"></asp:TextBox>
                                        </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddCustomer" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                      <%--<input type="text" class="form-control" id="customer_address" name="customer_address" placeholder="Enter Customer Address" autocomplete="off"/>--%>
                    <%--</div>--%>
                  </div>
                  <div class="form-group">
                    <label for="contact" <%--class="col-sm-5 control-label"--%> style="text-align:left;">Customer Phone</label>
                   <%-- <div class="col-sm-7">--%>
                      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                      <asp:TextBox ID="tbContact" runat="server" CssClass="form-control" placeholder="Auto Fill Customer Phone" autocomplete="off" ReadOnly="true" BackColor="White"></asp:TextBox>
                      </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddCustomer" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                     <%-- <input type="text" class="form-control" id="customer_phone" name="customer_phone" placeholder="Enter Customer Phone" autocomplete="off"/>--%>
                   <%-- </div>--%>
                  </div>
                </div>
                
                
                <br /> <br/>
                <table class="table table-bordered" id="product_info_table">
                  <thead>
                    <tr>
                      <th style="width:50%">Product</th>
                      <th style="width:10%">Quantity</th>
                      <th style="width:10%">Rate</th>
                      <th style="width:20%">Amount</th>
                      <th style="width:10%"><asp:Button ID="btnAddrow" runat="server" CssClass="btn btn-primary" Text="Add New Row" OnClick="btnAddrow_Click" UseSubmitBehavior="False" CausesValidation="False"></asp:Button></th>
                    </tr>
                  </thead>
                    
                   <tbody>
                       <asp:Panel ID="panelTableRow" runat="server" Width="100%"></asp:Panel>
                    <%-- <tr id="row_1">
                       <td>
                           
                            <asp:DropDownList ID="ddProduct" CssClass="form-control js-example-placeholder-single" CausesValidation="false" runat="server" OnSelectedIndexChanged="ddProduct_SelectedIndexChanged1" AutoPostBack="True" Width="100%"></asp:DropDownList>
                        
                         </td>
                        <td>
                            <asp:TextBox ID="tbQuantity" runat="server" CssClass="form-control" autocomplete="off" OnTextChanged="tbQuantity_TextChanged" BackColor="White" Text="1" AutoPostBack="True"></asp:TextBox>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" RenderMode="Inline">
                                    <ContentTemplate>
                           <asp:TextBox ID="tbRate" runat="server" CssClass="form-control" autocomplete="off" ReadOnly="true" BackColor="White"></asp:TextBox>
                                        </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddProduct" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>=
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" RenderMode="Inline">
                                    <ContentTemplate>
                           <asp:TextBox ID="tbAmount" runat="server" CssClass="form-control" autocomplete="off" ReadOnly="true" BackColor="White"></asp:TextBox>
                            </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tbQuantity" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                        </td>
                     </tr>--%>
                   </tbody>
                </table>
                <br /> <br/>

                <div class="col-md-6 col-xs-12 pull pull-right">

                  <%--<div class="form-group">
                    <label for="gross_amount" class="col-sm-5 control-label">Gross Amount</label>
                    <div class="col-sm-7">
                      <input type="text" class="form-control" id="gross_amount" name="gross_amount"  autocomplete="off"/>
                      <input type="hidden" class="form-control" id="gross_amount_value" name="gross_amount_value" autocomplete="off"/>
                    </div>
                  </div>

                  <div class="form-group">
                    <label for="service_charge" class="col-sm-5 control-label">S-Charge </label>
                    <div class="col-sm-7">
                      <input type="text" class="form-control" id="service_charge" name="service_charge" autocomplete="off"/>
                      <input type="hidden" class="form-control" id="service_charge_value" name="service_charge_value" autocomplete="off"/>
                    </div>
                  </div>

                  <div class="form-group">
                    <label for="vat_charge" class="col-sm-5 control-label">Vat </label>
                    <div class="col-sm-7">
                      <input type="text" class="form-control" id="vat_charge" name="vat_charge"  autocomplete="off"/>
                      <input type="hidden" class="form-control" id="vat_charge_value" name="vat_charge_value" autocomplete="off"/>
                    </div>
                  </div>

                  <div class="form-group">
                    <label for="discount" class="col-sm-5 control-label">Discount</label>
                    <div class="col-sm-7">
                      <input type="text" class="form-control" id="discount" name="discount" placeholder="Discount" onkeyup="subAmount()" autocomplete="off"/>
                    </div>
                  </div>--%>

                 <%-- <div class="form-group">
                    <label for="net_amount" class="col-sm-5 control-label">Total Amount</label>
                    <div class="col-sm-7">
                      <input type="text" class="form-control" id="net_amount" name="net_amount"  autocomplete="off"/>
                      <input type="hidden" class="form-control" id="net_amount_value" name="net_amount_value" autocomplete="off"/>
                    </div>
                  </div>--%>

                </div>
              </div>
              <!-- /.box-body -->

              <div class="box-footer">
                <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server" RenderMode="Inline">
                                   <ContentTemplate>--%>
                  <asp:Label ID="lbError" runat="server" CssClass="form-control" autocomplete="off" Visible="false" Text="Quantity Is Not Valid" ForeColor="Red"></asp:Label>
                                  <%-- </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tbQuantity" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>--%>
                <asp:Button ID="btnCreate" runat="server" CssClass="btn btn-success" Text="Create Order" OnClick="btnCreate_Click" UseSubmitBehavior="false"/>
                <a <%--href="<?php echo base_url('orders/') ?>"--%> class="btn btn-warning">Back</a>
                  
              </div>
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
