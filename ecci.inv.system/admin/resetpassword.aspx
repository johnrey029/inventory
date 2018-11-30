<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Superadmin.Master" AutoEventWireup="true" CodeBehind="resetpassword.aspx.cs" Inherits="ecci.inv.system.admin.resetpassword" %>
<asp:Content ID="ts1" ContentPlaceHolderID="title" runat="server">
    Administrator-Reset Password
</asp:Content>
<asp:Content ID="hs1" ContentPlaceHolderID="heading" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#resetPassNav").addClass('active');
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <!-- Content Wrapper. Contains page content -->
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        Password Management
        <small>Administrator</small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Reset Password</li>
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
            <h3 class="box-title">Reset Password</h3>
          </div>

              <div class="box-body">

                  <div class="form-group">
                  <label for="brand">Employee Number</label> <span style="display:inline-block; width: 20px;"></span>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbEmpNo" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                  <asp:TextBox ID="tbEmpNo" runat="server" CssClass="form-control" placeholder="Employee Number" CausesValidation="false"></asp:TextBox>
                     </div>

                  <div class="form-group">
                  <label for="brand">New Password</label> <span style="display:inline-block; width: 20px;"></span>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbPassword" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                  <asp:TextBox ID="tbPassword" runat="server" CssClass="form-control" placeholder="Password" TextMode="Password" CausesValidation="false"></asp:TextBox>
                     </div>

                   <div class="form-group">
                  <label for="brand">Confirm New Password</label> <span style="display:inline-block; width: 20px;"></span>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tbConfPassword" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                  <asp:TextBox ID="tbConfPassword" runat="server" CssClass="form-control" placeholder="Confirm Password" TextMode="Password" CausesValidation="false"></asp:TextBox>
                     </div>

              </div>
                
              <!-- /.box-body -->

              <div class="box-footer">
                  <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn btn-primary" OnClick="btnSave_Click"/>
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
