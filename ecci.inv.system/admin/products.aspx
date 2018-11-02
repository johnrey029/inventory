<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Superadmin.Master" AutoEventWireup="true" CodeBehind="products.aspx.cs" Inherits="ecci.inv.system.admin.products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Administrator-Products
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Wrapper. Contains page content -->
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <%--    <script type="text/javascript">$(function(){ $('.alert-success').hide();$('.alert-error').hide(); });</script>--%>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Add Products
        <small>Administrator</small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Add Products</li>
            </ol>
        </section>
        <!-- Main content -->
        <section class="content">
            <!-- Small boxes (Stat box) -->
            <div class="row">
                <div class="col-md-12 col-xs-12">
                    <div class="alert alert-success alert-dismissible" role="alert">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <strong>Succesfully</strong> Saved Item
                    </div>

                    <div class="alert alert-error alert-dismissible" role="alert">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <strong>Failed</strong> To Save Item
                    </div>


                    <div class="box">
                        <div class="box-header">
                            <h3 class="box-title">Add Items</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">


                            <div class="form-group">
                                <label for="brand">Product Name</label>
                                <span style="display: inline-block; width: 20px;"></span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbProduct" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="tbProduct" runat="server" CssClass="form-control" placeholder="Product Name" CausesValidation="false" autocomplete="off"></asp:TextBox>

                            </div>

                            <div class="form-group">
                                <label for="description">Description</label><span style="display: inline-block; width: 20px;"></span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbDescription" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="tbDescription" runat="server" CssClass="form-control" placeholder="Description" CausesValidation="false" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="description">Unit Price</label><span style="display: inline-block; width: 20px;"></span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbUnitPrice" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="tbUnitPrice" runat="server" CssClass="form-control" placeholder="Unit Price  " CausesValidation="false" autocomplete="off"></asp:TextBox>
                            </div>
                            <br />
                            <br />
                            <table class="table table-bordered" id="product_info_table">
                                <thead>
                                    <tr>
                                        <th style="width: 25%">Item</th>
                                        <th style="width: 20%">Quantity</th>
                                        <th style="width: 10%">Price</th>
                                        <th style="width: 10%">Amount</th>
                                        <th style="width: 10%">
                                            <asp:Button ID="btnAddrow" runat="server" CssClass="btn btn-primary" Text="Add New Row" OnClick="btnAddrow_Click" UseSubmitBehavior="False" CausesValidation="False"></asp:Button></th>
                                    </tr>
                                </thead>

                                <tbody>
                                    <asp:Panel ID="panelTableRow" runat="server" Width="100%"></asp:Panel>
                                    
                                </tbody>
                            </table>
                            <br />
                            <br />

                            <div class="col-md-6 col-xs-12 pull pull-right">
                                <%--<asp:Panel ID="panelTotal" runat="server"></asp:Panel>--%>
                                <div class="form-group">
                                    <label for="gross_amount" style="text-align: right; font: bold; font-size: large" class="col-sm-4 control-label">Total Amount</label>
                                    <div class="col-sm-8">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" RenderMode="Inline">
                                            <ContentTemplate>
                                                <asp:TextBox ID="tbTotalAmount" runat="server" CssClass="form-control" placeholder="0.00" ReadOnly="true" autocomplete="off" BackColor="White" Font-Bold="True" Font-Size="Large"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                            </div>
                        </div>

                        <!-- /.box-body -->

                        <div class="box-footer">
                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnCreate_Click" />
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-warning" CausesValidation="false" />
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
<asp:Content ID="Content3" ContentPlaceHolderID="heading" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mainProductNav").addClass('active');
            $("#addProductsNav").addClass('active');
        });
    </script>
</asp:Content>
