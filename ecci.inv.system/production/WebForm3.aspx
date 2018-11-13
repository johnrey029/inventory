<%@ Page Title="" Language="C#" MasterPageFile="~/production/Production.Master" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="ecci.inv.system.production.WebForm3" %>

<asp:Content ID="TS1" ContentPlaceHolderID="title" runat="server">
    Dashboard
</asp:Content>
<asp:Content ID="HS1" ContentPlaceHolderID="heading" runat="server">
     <script type="text/javascript">  
         $(document).ready(function () {
             $("#requestNav").addClass('active');
         });
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
          
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
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
            <%--<table id="manageTable" class="table table-bordered table-striped" style=" width: 100%">
              <thead>
              <tr>
                <th>Action</th>
                <th>PO#</th>
                <th>Supplier Name</th>
                <th>Brand Name</th>
                <th>Ordered Quantity</th>
                <th>Purchased Date</th>
                <th>Delivery Date</th>
                <th>Status</th>
              </tr>
              </thead>
            </table>--%>
              <asp:GridView ID="GridView1" CssClass="table table-bordered table-striped" runat="server" 
                  AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound" 
                  DataKeyNames="itemsid">
                  <Columns>
                      <asp:TemplateField HeaderStyle-Width="20px" ItemStyle-Width="20px">
                          <ItemTemplate>
                              <%--<a href="JavaScript:expandcollapse('<%#Eval("uniqueid") %>');">
                                 <img src="../Images/plus.png" class="img-sm" border="0" id='img<%#Eval("uniqueid") %>' />
                              </a>--%>
                              <button class="btn btn-primary glyphicon-plus" onclick="return ToggleGridPanel(this, 'tr<%# Eval("itemsid") %>')"></button>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:BoundField DataField="brandname" HeaderText="Material Name" />
                      <asp:BoundField DataField="quantityordered" HeaderText="Qty" />
                      <asp:TemplateField HeaderText="Price">
                          <ItemTemplate>
                              <%#Eval("price") %>
                              <%#MyNewRow(Eval("itemsid")) %>
                              
                                   <input type="hidden" id="<%# Eval("itemsid") %>"  name="hiddenStockId" value="<%# Eval("quantityordered") %>" />
                              <asp:GridView ID="GridView2" CssClass="table table-bordered table-striped" runat="server" ShowHeaderWhenEmpty="true" Width="100%" AutoGenerateColumns="false">
                                                 <Columns>
                                                     <asp:BoundField DataField="purchaseorder" HeaderText="PO#" />
                                                     <asp:BoundField DataField="brandname" HeaderText="Material Name" />
                                                     <asp:BoundField DataField="quantity" HeaderText="Qty"/>
                                                     <asp:BoundField DataField="receivedate" HeaderText="Date"/>
                                                 </Columns>
                              </asp:GridView>
                          </ItemTemplate>
                      </asp:TemplateField>
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
<!-- /.content-wrapper -->
<!-- Update raw stock modal -->
<script type="text/javascript">  
        
 </script>
</asp:Content>   