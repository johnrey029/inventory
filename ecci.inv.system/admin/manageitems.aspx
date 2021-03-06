﻿<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Superadmin.Master" AutoEventWireup="true" CodeBehind="manageitems.aspx.cs" Inherits="ecci.inv.system.admin.manageitems" %>

<asp:Content ID="TS1" ContentPlaceHolderID="title" runat="server">
    Administrator-Manage Items
</asp:Content>
<asp:Content ID="HS1" ContentPlaceHolderID="heading" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "WebService/ManageItemService.asmx/GetItem",
                success: function (data) {
                    var datatableVariable = $('#manageTable').DataTable({
                        data: data,
                        columns: [
                            {
                                'data': 'itemsId', 'render': function (data, type, row) {
                                    return "<a  class='btn btn-primary btn-sm' onClick='ConfirmUpdate(" + data + ")'><i class='fa fa-bars'></i>  Update</a>";
                                },
                                orderable: false
                            },
                            { 'data': 'suppName' },
                            { 'data': 'brandName' },
                            { 'data': 'description' },
                           { 'data': 'unitPrice' }
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
            $("#mainItemNav").addClass('active');
            $("#manageItemsNav").addClass('active');
        });
    </script>
</asp:Content>
<asp:Content ID="CS1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Manage Items
      <small>Administrator</small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Manage Items</li>
            </ol>
        </section>

        <!-- Main content -->
        <section class="content">
            <!-- Small boxes (Stat box) -->
            <div class="row">
                <div class="col-md-12 col-xs-12">
                    <a href="items.aspx" class="btn btn-primary">Add Items</a>
                    <br />
                    <br />
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">List of Items</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">

                            <table id="manageTable" class="table table-bordered table-striped" style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>Action</th>
                                        <th>Supplier Code</th>
                                        <th>Brand Name</th>
                                        <th>Description</th>
                                        <th>Unit Price</th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                        <input type="hidden" id="hiddenItemsId"  name="hiddenItemsId" value="" />
                        <asp:Label ID="lbError" runat="server" Text="Receiving Total Quantity" Visible="true" ForeColor="Green"></asp:Label>
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

    <div class="modal fade" tabindex="-1" role="dialog" id="itemModal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">

                <div class="modal-header bg-aqua-active">
                    <button type="button" class="close active" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Update Items</h4>
                </div>

                <div class="modal-body">
                    <div class="form-group">
                        <label for="PO">Supplier Name</label>
                        <input type="text" class="form-control" id="suppname" name="suppname" readonly="true" />
                    </div>
                    <div class="form-group">
                        <label for="supplier">Brand Name</label>
                        <input type="text" class="form-control" id="brandname" name="brandname" readonly="true" />
                    </div>
                    <div class="form-group">
                        <label for="brand">Description</label>
                        <input type="text" class="form-control" id="description" name="description" readonly="true" />
                    </div>
                    <div class="form-group">
                        <label for="qty">Current Unit Price</label>
                        <input type="text" class="form-control" id="unitprice" name="unitprice" readonly="true" />
                    </div>

                    <div class="form-group">
                        <label for="qty">New Unit Price</label>
                        <asp:TextBox ID="tbNewUnitPrice" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    </div>

                    <input type="hidden" id="hiddenitems" name="hiddenitems" value="" />

                </div>
                <div class="modal-footer bg-aqua-active">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbNewUnitPrice" ErrorMessage="Please input a Quantity" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:Button ID="btnUpdate" runat="server" Text="Update Items" CssClass="btn btn-success" OnClick="btnUpdate_Click"
                        UseSubmitBehavior="false" OnClientClick="if ( Page_ClientValidate() ) { this.value='Updating...'; this.disabled='false'; }" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <script type="text/javascript">
        function ConfirmUpdate(itemsId) {
            $('#hiddenItemsId').val(itemsId);
            var iid = $('#hiddenItemsId').val();
            $.ajax({
                url: "WebService/ManageItemService.asmx/GetItemById",
                data: { id: iid },
                type: "POST",
                dataType: "json",
                success: function (data) {
                    $('#suppname').val(data.suppName);
                    $('#brandname').val(data.brandName);
                    $('#description').val(data.description);
                    $('#unitprice').val(data.unitPrice);
                },
                error: function (err) {
                    alert(err);
                }
            });
            $('#itemModal').modal('show');
        }
        function myFunction() {
             document.getElementById("<%=btnUpdate.ClientID%>").disabled = false;
        }
    </script>
</asp:Content>
