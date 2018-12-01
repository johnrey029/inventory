<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefaultXXX.aspx.cs" Inherits="ecci.inv.system.Default" %>

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
    <div class="login-box">
  <div class="login-logo">
    <b>Triton Industrial Plastic Manufacturing</b>
  </div>
  <!-- /.login-logo -->
  <div class="auto-style1">
    <p class="login-box-msg">INVENTORY SYSTEM
      </p>
      <asp:Label ID="lbError" runat="server" Text="Label" Visible="False" Width="100%"></asp:Label>
<%--
    <form action="<?php echo base_url('auth/login') ?>" method="post">--%>
      <div class="form-group has-feedback">
        <%--<input type="email" class="form-control" name="email" id="email" placeholder="Email" autocomplete="off">--%>
          <asp:TextBox ID="tbEmpNo" runat="server" CssClass="form-control" placeholder="Employee Number" autocomplete="off" ></asp:TextBox>
        <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
          
      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbEmpNo" ErrorMessage="*The Employee Number field is required" ForeColor="Red"></asp:RequiredFieldValidator>
      </div>
      <div class="form-group has-feedback">
        <%--<input type="password" class="form-control" name="password" id="password" placeholder="Password" autocomplete="off">--%>
        <asp:TextBox ID="tbPassword" runat="server" CssClass="form-control" placeholder="Password" TextMode="Password" autocomplete="off"></asp:TextBox>
          <span class="glyphicon glyphicon-lock form-control-feedback"></span>
          
      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  ControlToValidate="tbPassword" ErrorMessage="*The Password field is required" ForeColor="Red"></asp:RequiredFieldValidator>
      </div>
      <div class="row">
        <!-- /.col -->
        <div class="col-xs-12">
          <%--<button type="submit" class="btn btn-primary btn-block btn-flat">Sign In</button>--%>
            <asp:Button ID="btnSubmit" runat="server" Text="Sign In" OnClick="btnSubmit_Click" CssClass="btn btn-primary btn-block btn-flat" />
        </div>
        <!-- /.col -->
      </div>
<%--    </form>--%>

  </div>
  <!-- /.login-box-body -->
</div>
<!-- /.login-box -->
    </form>
    <script type="text/javascript" src="../assets/bower_components/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap 3.3.7 -->
<script type="text/javascript" src="../assets/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
<!-- iCheck -->
<script type="text/javascript" src="../assets/plugins/iCheck/icheck.min.js"></script>
<script>
  $(function () {
    $('input').iCheck({
      checkboxClass: 'icheckbox_square-blue',
      radioClass: 'iradio_square-blue',
      increaseArea: '20%' // optional
    });
  });
</script>
</body>
</html>
