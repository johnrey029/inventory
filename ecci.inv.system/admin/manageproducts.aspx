﻿<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Superadmin.Master" AutoEventWireup="true" CodeBehind="manageproducts.aspx.cs" Inherits="ecci.inv.system.admin.manageproducts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Administrator-Manage Products
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="heading" runat="server">
     <script type="text/javascript">
        $(document).ready(function () {
            $("#mainProductNav").addClass('active');
            $("#manageProductsNav").addClass('active');
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
