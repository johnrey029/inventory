<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="ecci.inv.system.SignIn" MasterPageFile="~/Site1.Master"%>

<asp:Content ID="TS1" ContentPlaceHolderID="title" runat="server">
    SignIn
</asp:Content>
<asp:Content ID="HS1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
 <asp:Content ID="CS1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container">
            <div class="form-horizontal">
                <h2>Login</h2>
                <hr />
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" CssClass="col-sm-3 control-label" Text="Username"></asp:Label>
                    <div class="col-sm-5">
                        <asp:TextBox ID="UserName" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorUsername" CssClass="text-danger" runat="server" ErrorMessage="The Username field is Required !" ControlToValidate="UserName"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label2" runat="server" CssClass="col-sm-3 control-label" Text="Password"></asp:Label>
                    <div class="col-sm-5">
                        <asp:TextBox ID="Password" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPass" CssClass="text-danger" runat="server" ErrorMessage="The Password field is Required !" ControlToValidate="Password"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-3"></div>
                    <div class="col-sm-5">
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                        <asp:Label ID="Label3" runat="server" CssClass="control-label" Text="Remember me ?"></asp:Label>
                        <asp:LinkButton ID="lbForgotPass" runat="server" CssClass="control-label" PostBackUrl="~/ForgotPassword.aspx">Forgot Password !</asp:LinkButton>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-3"></div>
                    <div class="col-sm-5">
                        <asp:Button ID="Button1" runat="server" Text="Login" CssClass="form-control btn btn-success"></asp:Button>
<%--                        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/SignUp.aspx">Sign Up</asp:LinkButton>--%>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-3"></div>
                    <div class="col-sm-5">
                        <asp:Button ID="Button2" runat="server" Text="Sign Up" CssClass="form-control btn btn-primary"></asp:Button>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-3"></div>
                    <div class="col-sm-5">
                        <asp:Label ID="lblError" runat="server" CssClass="text-danger"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>       
