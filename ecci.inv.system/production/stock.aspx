<%@ Page Title="" Language="C#" MasterPageFile="~/production/Production.Master" AutoEventWireup="true" CodeBehind="stock.aspx.cs" Inherits="ecci.inv.system.production.stock" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Purchasing-Stock
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="heading" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#stockNav").addClass('active');
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.alert-success').hide();
            $('.alert-error').hide();
        });
    </script>
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            height: 140px;
        }
    </style>

    <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/js/bootstrap-datepicker.js"></script>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Wrapper. Contains page content -->
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Stock Management
        <small>Purchasing</small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Stock Input</li>
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
                        <strong>Failed</strong> To Save Order
                    </div>

                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">Stock Input</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">

                            <div class="form-group">
                                <label for="order">Order Number</label><span style="display: inline-block; width: 20px;"></span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddOrder" ErrorMessage="This field is required" ForeColor="Red" InitialValue="-1"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddOrder" CssClass="form-control js-example-placeholder-single" CausesValidation="false" runat="server" OnSelectedIndexChanged="ddOrder_SelectedIndexChanged" AutoPostBack="True" Width="100%"></asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <label for="project">Product Names</label><span style="display: inline-block; width: 20px;"></span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddProduct" ErrorMessage="This field is required" ForeColor="Red" InitialValue="-1"></asp:RequiredFieldValidator>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddProduct" CssClass="form-control js-example-placeholder-single" CausesValidation="false" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddProduct_SelectedIndexChanged" Width="100%" BackColor="White"></asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddOrder" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                            
                            <div class="form-group">
                                <label for="client">Client Name</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbClient" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="tbClient" runat="server" CssClass="form-control" placeholder="Client Name" autocomplete="off" ReadOnly="true" BackColor="White"></asp:TextBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddProduct" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                            <div class="form-group">
                                <label for="price">Expected Quantity</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="tbExpected" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="tbExpected" runat="server" CssClass="form-control" placeholder="Expected" autocomplete="off" ReadOnly="true" BackColor="White"></asp:TextBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddProduct" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                            <div class="form-group">
                                <label for="quantity">Produce Quantity</label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbQuantity" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="tbQuantity" runat="server" CssClass="form-control" placeholder="Quantity" autocomplete="off" AutoPostBack="true" OnTextChanged="tbQuantity_TextChanged"></asp:TextBox>
                            </div>
                            

                        </div>
                        <!-- /.box-body -->

                        <div class="box-footer">
                            <asp:Button ID="btnSave" runat="server" Text="Save & Print" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                            <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" CssClass="btn btn-warning" />
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
    <script type="text/javascript">
    </script>
</asp:Content>
