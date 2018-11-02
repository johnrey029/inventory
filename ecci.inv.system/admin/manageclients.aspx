<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Superadmin.Master" AutoEventWireup="true" CodeBehind="manageclients.aspx.cs" Inherits="ecci.inv.system.admin.manageclients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Administrator-Manage Cients
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="heading" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "WebService/ManageClientService.asmx/GetClient",
                success: function (data) {
                    var datatableVariable = $('#manageTable').DataTable({
                        data: data,
                        columns: [
                            {
                                'data': 'clientId', 'render': function (data, type, row) {
                                    return "<a  class='btn btn-primary btn-sm' onClick='ConfirmUpdate(" + data + ")'><i class='fa fa-bars'></i>  Update</a>";
                                },
                                orderable: false
                            },
                            { 'data': 'clientName' },
                            { 'data': 'clientAdd' },
                            { 'data': 'clientCon' },
                            { 'data': 'clientStatus' }
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
            $("#mainClientNav").addClass('active');
            $("#manageClientNav").addClass('active');
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Manage Clients
      <small>Administrator</small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Manage Clients</li>
            </ol>
        </section>

        <!-- Main content -->
        <section class="content">
            <!-- Small boxes (Stat box) -->
            <div class="row">
                <div class="col-md-12 col-xs-12">
                    <a href="clients.aspx" class="btn btn-primary">Add Clients</a>
                    <br />
                    <br />
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">List of Clients</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">

                            <table id="manageTable" class="table table-bordered table-striped" style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>Action</th>
                                        <th>Client Name</th>
                                        <th>Client Address</th>
                                        <th>Contact Number</th>
                                        <th>Status</th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                        <input type="hidden" id="hiddenClientId" name="hiddenClientId" value="" />
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

    <div class="modal fade" tabindex="-1" role="dialog" id="clientModal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">

                <div class="modal-header bg-aqua-active">
                    <button type="button" class="close active" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Update Items</h4>
                </div>

                <div class="modal-body">
                    <div class="form-group">
                        <label for="PO">Client Name</label>
                        <%--<input type="text" class="form-control" id="suppcode" name="suppcode" readonly="true" />--%>
                        <asp:TextBox ID="tbClientName" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="supplier">Client Address</label>
                        <%--<input type="text" class="form-control" id="suppname" name="suppname" readonly="true" />--%>
                        <asp:TextBox ID="tbClientAdd" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="brand">Client Contact 1</label>
                        <%--<input type="text" class="form-control" id="suppadd" name="suppadd" readonly="true" />--%>
                        <asp:TextBox ID="tbClientCon" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="qty">Client Contact 2</label>
                        <%--<input type="text" class="form-control" id="suppcontact" name="suppcontact" readonly="true" />--%>
                        <asp:TextBox ID="tbClientCon2" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                                <label for="stat">Status</label>
                                <div class="radio">
                                    <label>
                                        <asp:RadioButton ID="rbActive" runat="server" GroupName="stat"></asp:RadioButton>Activated</label>
                                    <label>
                                        <asp:RadioButton ID="rbDeactivate" runat="server" GroupName="stat"></asp:RadioButton>Deactivated</label>
                                </div>
                            </div>

                    <input type="hidden" id="hiddenitems" name="hiddenitems" value="" />

                </div>
                <div class="modal-footer bg-aqua-active">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbClientCon" ErrorMessage="Please input a contact number" ForeColor="Red"></asp:RequiredFieldValidator>
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
        function ConfirmUpdate(clieId) {
            $('#hiddenClientId').val(clieId);
            var cid = $('#hiddenClientId').val();
            $.ajax({
                url: "WebService/ManageClientService.asmx/GetClientById",
                data: { id: cid },
                type: "POST",
                dataType: "json",
                success: function (data) {

                    $('#<%= tbClientName.ClientID%>').val(data.clientName);
                    $('#<%= tbClientAdd.ClientID%>').val(data.clientAdd);
                    $('#<%= tbClientCon.ClientID%>').val(data.clientCon);
                    $('#<%= tbClientCon2.ClientID%>').val(data.clientCon2);
                    
                },
                error: function (err) {
                    alert(err);
                }
            });
            $('#clientModal').modal('show');
        }
        function myFunction() {
            document.getElementById("<%=btnUpdate.ClientID%>").disabled = false;
        }
    </script>
</asp:Content>
