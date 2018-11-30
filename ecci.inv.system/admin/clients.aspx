<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Superadmin.Master" AutoEventWireup="true" CodeBehind="clients.aspx.cs" Inherits="ecci.inv.system.admin.clients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Administrator-Clients
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="heading" runat="server">
    <script type="text/javascript">  
     $(document).ready(function () {  
         $("#mainClientNav").addClass('active');
         $("#addClientNav").addClass('active');
  });  
 </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Wrapper. Contains page content -->
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <%--    <script type="text/javascript">$(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>--%>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Clients Management
        <small>Administrator</small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Add Client</li>
            </ol>
        </section>
        <!-- Main content -->
        <section class="content">
            <!-- Small boxes (Stat box) -->
            <div class="row">
                <div class="col-md-12 col-xs-12">

                    <div class="alert alert-success alert-dismissible" role="alert">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <strong>Succesfully</strong> Saved Client
                    </div>

                    <div class="alert alert-error alert-dismissible" role="alert">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <strong>Failed</strong> To Save Client
                    </div>
                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">Add Client</h3>
                        </div>
                        <div class="box-body">

                            <div class="form-group">
                                <label for="suppcode">Client Name</label><span style="display: inline-block; width: 20px;"></span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbClientName" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="tbClientName" runat="server" CssClass="form-control" placeholder="Client Name" autocomplete="off"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label for="suppname">Client Address</label><span style="display: inline-block; width: 20px;"></span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbClientAdd" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="tbClientAdd" runat="server" CssClass="form-control" placeholder="Client Address" autocomplete="off"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label for="suppaddress">Client Contact</label><span style="display: inline-block; width: 20px;"></span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbClientCon" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="tbClientCon" runat="server" CssClass="form-control" placeholder="Client Contact 1" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="suppaddress">Client Contact 2</label><span style="display: inline-block; width: 20px;"></span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbClientCon2" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="tbClientCon2" runat="server" CssClass="form-control" placeholder="Client Contact 2" autocomplete="off"></asp:TextBox>
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
                        </div>
                        <!-- /.box-body -->

                        <div class="box-footer">
                            <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-warning" CausesValidation="false" />
                            <asp:Label ID="lbError" runat="server" Text="Label" Visible="False"></asp:Label>
                        </div>
                    </div>
                    <!-- /.box -->
                </div>
                <!-- col-md-12 -->
            </div>
            <!-- /.row -->
        </section>
    </div>
</asp:Content>
