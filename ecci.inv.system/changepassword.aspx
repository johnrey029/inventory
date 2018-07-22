<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="changepassword.aspx.cs" Inherits="ecci.inv.system.changepassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
  <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
  <title>Log in</title>
  <!-- Tell the browser to be responsive to screen width -->
  <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport"/>
  <!-- Bootstrap 3.3.7 -->
  
    <!--commit #2-->
  
  <link rel="stylesheet" type="text/css" href="../assets/bower_components/bootstrap/dist/css/bootstrap.min.css"/>
  <!-- Font Awesome -->
  <link rel="stylesheet" type="text/css" href="../assets/bower_components/font-awesome/css/font-awesome.min.css"/>
  <!-- Ionicons -->
  <link rel="stylesheet" type="text/css" href="../assets/bower_components/Ionicons/css/ionicons.min.css"/>
  <!-- Theme style -->
  <link rel="stylesheet" type="text/css" href="../assets/dist/css/AdminLTE.min.css"/>
  <!-- iCheck -->
  <link rel="stylesheet" type="text/css" href="../assets/plugins/iCheck/square/blue.css"/>

  <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
  <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
  <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->

  <!-- Google Font -->
  <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic"/>
    <style type="text/css">
        .auto-style1 {
            background: #fff;
            padding: 20px;
            color: #666;
            text-align: center;
            border-top-style: none;
            border-top-color: inherit;
            border-top-width: 0;
        }
    </style>
</head>
<body class="hold-transition login-page">
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
  <div class="login-box">
  <div class="login-logo">
    <b>Triton Industrial Plastic Manufacturing</b>
  </div>
       <div class="auto-style1">
    <p class="login-box-msg">INVENTORY SYSTEM
      </p>
        <h4>Password expired. Please change your password</h4>

        <div  class="form-group has-feedback">
            <asp:TextBox ID="tboldPassword" runat="server" CssClass="form-control" placeholder="Old Password" autocomplete="off" TextMode="Password" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tboldPassword" ErrorMessage="*The Old Password field is required" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group has-feedback">
            <asp:TextBox ID="tbNewPassword" runat="server" OnTextChanged="tbNewPassword_TextChanged" CssClass="form-control" placeholder="New Password" autocomplete="off" AutoPostBack="True" TextMode="Password" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbNewPassword" ErrorMessage="*The New Password field is required" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group has-feedback">
            <asp:TextBox ID="tbConfNewPassword" runat="server" OnTextChanged="tbConfNewPassword_TextChanged" CssClass="form-control" placeholder="Confirm New Password" autocomplete="off" AutoPostBack="True" TextMode="Password" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbConfNewPassword" ErrorMessage="*The Confirm New Password field is required" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>
           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
               <ContentTemplate>
        <asp:Label ID="lbError" runat="server" Text="Label" Visible="False" Width="100%" ForeColor="Red"></asp:Label>
                   </ContentTemplate> 
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="tbConfNewPassword" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="tbNewPassword" EventName="TextChanged" />
                        </Triggers>
               </asp:UpdatePanel>
        <div class="row form-group">
            <div class="col-xs-12">
            <asp:Button ID="btnChange" runat="server" Text="Save" OnClick="btnChange_Click" CssClass="form-control btn btn-primary btn-block btn-flat"/>
            </div>
        </div>

           <div class="row form-group">
            <div class="col-xs-12">
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="form-control btn btn-danger btn-block btn-flat" CausesValidation="False"/>
            </div>
            </div>
        </div>
      </div>
    </form>
</body>
</html>
