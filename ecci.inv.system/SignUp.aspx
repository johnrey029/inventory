<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="ecci.inv.system.SignUp" MasterPageFile="~/Site1.Master"%>

<asp:Content ID="TSu1" ContentPlaceHolderID="title" runat="server">
    SignUp
</asp:Content>
<asp:Content ID="HSu1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="CSu1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container">
        <div class="form-horizontal">
            <h2>Sign Up</h2>
            <hr />
            <div class="form-group">
            <label class="col-sm-8 col-sm-offset-3">Username</label>
            <div class="col-sm-6 col-sm-offset-3">
                <asp:TextBox ID="tbUname" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox>
            </div>
            </div>
                
            <div class="form-group">
            <label class="col-sm-8 col-sm-offset-3">Password</label>
            <div class="col-sm-6 col-sm-offset-3">
                <asp:TextBox ID="tbPass" runat="server" CssClass="form-control" placeholder="Password" TextMode="Password"></asp:TextBox>
            </div>
                </div>
                
            <div class="form-group">
            <label class="col-sm-8 col-sm-offset-3">Confirm Password</label>
            <div class="col-sm-6 col-sm-offset-3">
                <asp:TextBox ID="tbCPass" runat="server" CssClass="form-control" placeholder="Confirm Password" TextMode="Password"></asp:TextBox>
            </div>
                </div>
                
            <div class="form-group">
            <label class="col-sm-8 col-sm-offset-3">Name</label>
            <div class="col-sm-6 col-sm-offset-3">
                <asp:TextBox ID="tbName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
            </div>
                </div>
                
            <div class="form-group">
            <label class="col-sm-8 col-sm-offset-3">Email</label>
            <div class="col-sm-6 col-sm-offset-3">
                <asp:TextBox ID="tbEmail" runat="server" CssClass="form-control" placeholder="Email" TextMode="Email"></asp:TextBox>
            </div>
                </div>
                
            <div class="form-group">
            <div class="col-sm-6 col-sm-offset-3 space-vert">
                <asp:Button ID="btSignup" runat="server"  CssClass="col-xs-12 btn btn-success" Text="Sign Up"/>
               <%-- <asp:Label ID="lblMsg" runat="server"></asp:Label>--%>
            </div>
            </div>
            <div class="form-group">
            <div class="col-sm-6 col-sm-offset-3 space-vert">
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </div>
          </div>
    </div>
</asp:Content>    
