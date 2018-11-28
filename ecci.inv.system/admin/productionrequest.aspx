<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Superadmin.Master" AutoEventWireup="true" CodeBehind="productionrequest.aspx.cs" Inherits="ecci.inv.system.admin.productionrequest" %>
<asp:Content ID="TS1" ContentPlaceHolderID="title" runat="server">
    Production
</asp:Content>
<asp:Content ID="HS1" ContentPlaceHolderID="heading" runat="server">
    <style type="text/css">
        .collapsed-row{
            display:none;
            padding:0px;
            margin:0px;

        }
    </style>  
<script type="text/javascript">  
    $(document).ready(function () {
        $("#requestNav").addClass('active');
        $(".display").DataTable();
       // document.getElementById("<%=GridView1.ClientID %>").prepend($("<thead></thead>").append($("#GridView1").find("tbody tr:first"))).dataTable();
        //$("#GridView1").prepend($("<thead></thead>").append($("#GridView1").find("tbody tr:first"))).dataTable();
         });
         //function expandcollapse(name) {
         //    var div = document.getElementById(name);
         //    var img = document.getElementById('img' + name);
         //    if (div.style.display=="none") {
         //        div.style.display = "inline";
         //        img.src = "../Images/minus.png";
         //    }
         //    else {
         //        div.style.display = "none";
         //        img.src = "../Images/plus.png";
         //    }
         //} 
         function ToggleGridPanel(btn, row) {
             var current = $('#' + row).css('display');
             if (current == 'none') {
                 $('#' + row).show();
                 $(btn).removeClass('btn-primary glyphicon-plus')
                 $(btn).addClass('btn-danger glyphicon-minus')
             } else {
                 $('#' + row).hide();
                 $(btn).addClass('btn-primary glyphicon-plus')
                 $(btn).removeClass('btn-danger glyphicon-minus')
             }
             return false;
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
            <h3 class="box-title">Request By Production</h3>
          </div>
          <!-- /.box-header --><%--OnPreRender="GridView1_PreRender" OnRowDataBound="GridView1_RowDataBound" 
                  DataKeyNames="uniqueid">--%>
          <div class="box-body">
              <asp:GridView ID="GridView1" CssClass="display table table-bordered table-striped" ShowHeaderWhenEmpty="true" Width="100%" runat="server" 
                  AutoGenerateColumns="false" >
                  <Columns>
                     <asp:TemplateField>
                          <ItemTemplate>
                                <button type="button" class="btn btn-success glyphicon-envelope" style="height: 30px; display:inline-block; width: 75%;" onclick="openModal('<%# Eval("uniqueid") %>');" > Request</button>
                               <input type="hidden" id="<%# Eval("uniqueid") %>"  name="hiddenStockId" value=""/>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="orderid" HeaderText="Order ID" />
                      <asp:BoundField DataField="name" HeaderText="Customer Name" />
                      <asp:BoundField DataField="pname" HeaderText="Product Name" />
                      <asp:BoundField DataField="quantityordered" HeaderText="Qty" />
                      <asp:BoundField DataField="price" HeaderText="Price" />
                      <asp:BoundField DataField="amount" HeaderText="Total Amount" />
                      <asp:BoundField DataField="date" HeaderText="Date" DataFormatString="{0:MMMM-dd-yyyy}" />
                      <asp:BoundField DataField="status" HeaderText="Status"/> 
                     <%-- <asp:TemplateField HeaderText="Status">
                          <ItemTemplate>
                              <%#Eval("status") %>
                              <%#MyNewRow(Eval("uniqueid")) %>
                              <asp:GridView ID="GridView2" CssClass="table table-bordered table-striped" runat="server" ShowHeaderWhenEmpty="true" Width="100%" AutoGenerateColumns="false">
                                                 <Columns>
                                                     <asp:BoundField DataField="brandname" HeaderText="Material Name" />
                                                     <asp:BoundField DataField="required" HeaderText="Qty" />
                                                     <asp:BoundField DataField="price" HeaderText="Price"/>
                                                 </Columns>
                              </asp:GridView>
                                <div style="width:100%; text-align:center;">
                                <asp:Button ID="button" CssClass="btn btn-success" Height="30px" Width="75%"  runat="server" Text="Request Raw Materials" OnClick="button_Click" OnClientClick="return openModal('<%#Eval("uniqueid")%>')" />
                                <button type="button" class="btn btn-success" style="height: 30px; display:inline-block; width: 75%;" onclick="openModal('<%# Eval("uniqueid") %>');" >Request Raw Materials</button>
                                </div>
                                  
                               </br>
                            <%--         <tr>onclick='<%#Request_RawMats(this.ClientID) %>'onserverclick="ConfirmUpdate(<%# Eval("uniqueid") %>);"
                                  <td> 
                                      <div id='<%#Eval("uniqueid") %>' style="display:none">--%>
                                          
                                     <%-- </div>
                                  </td>
                              </tr>
                          </ItemTemplate>
                      </asp:TemplateField>--%>
                  </Columns>
                  
              </asp:GridView>
          </div>
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
    <script type="text/javascript">
        //$(document).ready(function () {
        //    $("#GridView1").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        //});
            function openModal(stockId) {
                $('#' + stockId).val(stockId);
                var sid = $('#' + stockId).val();
                var now = new Date();
                var minutes = 30;
                now.setTime(now.getTime() + (minutes * 60 * 1000));
                document.cookie = "CookieName=" + sid + ";expires=" + now.toUTCString();
                window.location = "/admin/approvematerials.aspx";
                //var datatableVariable;
                //var dtVariable;
                //$.ajax({
                //    url: "WebService/ReservedRawMats.asmx/GetReservedRaw",
                //    data: { id: sid },
                //    type: "POST",
                //    dataType: "json",
                //    success: function (data) {
                //        datatableVariable = $('#manageTable').DataTable({
                //            data: data,
                //            columns: [
                //                { 'data': 'brand' },
                //                { 'data': 'price' },
                //                { 'data': 'qty' },
                //                {
                //                    'data': 'itemsid', 'render': function (data, type, row) {
                //                        return '<table id="dtTable" class="table table-bordered table-striped" style="width: 100%"><thead>' +
                //                            '<tr><th>PO#</th><th>Brand</th><th>Quantity</th><th>Stored Date</th></tr>';
                //                    }
                //                }
                //            ],
                //            destroy: true,
                //            language: {
                //                emptyTable: "No Request Available!"
                //            }
                //        });
                //    },
                //    error: function (err) {
                //        alert(err);
                //    }
                //});
                //$.ajax({
                //    url: "orderedrequest.aspx/ConfirmUpdate",
                //    data: { },
                //    type: "POST",
                //    dataType: "json",
                //    success: function (data) {
                //       // alert(data);
                //                //$("#GridView3").find("tr:gt(0)").remove();
                //                //for (var i = 0; i < data.length; i++) {
                //                //    $("#GridView3").append("<tr><td>" + data.brandname +
                //                //                                    "</td><td>" + data.price + "</td><td>" + data.quantityordered + "</td></tr>");
                //                //}
                //    },
                //    error: function (err) {
                //        alert(err);
                //    }
                //});
                //complete: function () {
                //    $.ajax({
                //        url: "WebService/ReservedRawMats.asmx/GetRawList",
                //        data: { id: sid },
                //        type: "POST",
                //        dataType: "json",
                //        success: function (data) {
                //            dtVariable = $('#dtTable').DataTable({
                //                data: data,
                //                columns: [
                //                    { 'data': 'brand' },
                //                    { 'data': 'price' },
                //                    { 'data': 'qty' },
                //                ],
                //                destroy: true,
                //                language: {
                //                    emptyTable: "No Request Available!"
                //                }

                //                /*do some thing in second function*/
                //            });
                //        }
                //    });
                //}
                //$('#popUpModal').modal('show');
            }
</script>
</asp:Content>
