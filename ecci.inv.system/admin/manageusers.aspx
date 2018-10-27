<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Superadmin.Master" AutoEventWireup="true" CodeBehind="manageusers.aspx.cs" Inherits="ecci.inv.system.admin.manageusers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
        Super Admin-Manage Users
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="heading" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "WebService/ManageSupplierService.asmx/GetSupplier", //Change Service
                success: function (data) {
                    var datatableVariable = $('#manageTable').DataTable({
                        data: data,
                        columns: [
                            {
                                'data': 'userId', 'render': function (data, type, row) {
                                    return "<a  class='btn btn-primary btn-sm' onClick='ConfirmUpdate(" + data + ")'><i class='fa fa-bars'></i>  Update</a>";
                                },
                                orderable: false
                            },
                            { 'data': 'suppCode' },     //Change database
                            { 'data': 'suppName' },     //Change database
                            { 'data': 'suppAdd' },      //Change database
                            { 'data': 'suppContact' }   //Change database
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
            $("#mainUserNav").addClass('active');
            $("#manageUserNav").addClass('active');
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Manage
      <small>Suppliers</small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Suppliers</li>
            </ol>
        </section>

        <!-- Main content -->
        <section class="content">
            <!-- Small boxes (Stat box) -->
            <div class="row">
                <div class="col-md-12 col-xs-12">
                    <a href="supplier.aspx" class="btn btn-primary">Add Suppliers</a>
                    <br />
                    <br />
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">List of Suppliers</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">

                            <table id="manageTable" class="table table-bordered table-striped" style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>Action</th>
                                        <th>Supplier Code</th>
                                        <th>Company</th>
                                        <th>Address</th>
                                        <th>Contact Person</th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                        <input type="hidden" id="hiddenSupplierId" name="hiddenSupplierId" value="" />
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

    <div class="modal fade" tabindex="-1" role="dialog" id="supplierModal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">

                <div class="modal-header bg-aqua-active">
                    <button type="button" class="close active" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Update Items</h4>
                </div>

                <div class="modal-body">
                    <div class="form-group">
                        <label for="PO">Supplier Code</label>
                        <%--<input type="text" class="form-control" id="suppcode" name="suppcode" readonly="true" />--%>
                        <asp:TextBox ID="tbSuppCode" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="supplier">Supplier Name</label>
                        <%--<input type="text" class="form-control" id="suppname" name="suppname" readonly="true" />--%>
                        <asp:TextBox ID="tbSuppName" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="brand">Supplier Address</label>
                        <%--<input type="text" class="form-control" id="suppadd" name="suppadd" readonly="true" />--%>
                        <asp:TextBox ID="tbSuppAdd" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="qty">Supplier Point of Contact</label>
                        <%--<input type="text" class="form-control" id="suppcontact" name="suppcontact" readonly="true" />--%>
                        <asp:TextBox ID="tbSuppContact" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <input type="hidden" id="hiddenitems" name="hiddenitems" value="" />

                </div>
                <div class="modal-footer bg-aqua-active">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbSuppContact" ErrorMessage="Please input a Quantity" ForeColor="Red"></asp:RequiredFieldValidator>
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
        function ConfirmUpdate(suppId) {
            $('#hiddenSupplierId').val(suppId);
            var spid = $('#hiddenSupplierId').val();
            $.ajax({
                url: "WebService/ManageSupplierService.asmx/GetSupplierById",
                data: { id: spid },
                type: "POST",
                dataType: "json",
                success: function (data) {

                    $('#<%= tbSuppCode.ClientID%>').val(data.suppCode);
                    $('#<%= tbSuppName.ClientID%>').val(data.suppName);
                    $('#<%= tbSuppAdd.ClientID%>').val(data.suppAdd);
                    $('#<%= tbSuppContact.ClientID%>').val(data.suppContact);
                },
                error: function (err) {
                    alert(err);
                }
            });
            $('#supplierModal').modal('show');
        }
        function myFunction() {
            document.getElementById("<%=btnUpdate.ClientID%>").disabled = false;
        }
    </script>
</asp:Content>
