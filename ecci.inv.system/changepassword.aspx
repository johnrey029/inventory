<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="changepassword.aspx.cs" Inherits="ecci.inv.system.changepassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h4>Password expired. Please change your password</h4>
        <div>
            <asp:Label ID="Label1" runat="server" Text="Old Password"></asp:Label><asp:TextBox ID="tboldPassword" runat="server"></asp:TextBox></div>
        <div>
            <asp:Label ID="Label3" runat="server" Text="New Password"></asp:Label><asp:TextBox ID="tbNewPassword" runat="server"></asp:TextBox></div>
        <div>
            <asp:Label ID="Label2" runat="server" Text="Confirm New Password"></asp:Label><asp:TextBox ID="tbConfNewPassword" runat="server"></asp:TextBox></div>
        <div>
            <asp:Button ID="btnChange" runat="server" Text="Button" OnClick="btnChange_Click" /></div>
    </div>
    </form>
</body>
</html>
