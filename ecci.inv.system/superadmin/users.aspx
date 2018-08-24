<%@ Page Title="" Language="C#" MasterPageFile="~/superadmin/Superadmin.Master" AutoEventWireup="true" CodeBehind="users.aspx.cs" Inherits="ecci.inv.system.superadmin.users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    SuperAdmin-Users
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="heading" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <!-- Content Wrapper. Contains page content -->
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        Add Users
        <small>Super Admin</small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Add Users</li>
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
            <h3 class="box-title">Add Users</h3>
          </div>

              <div class="box-body">
                  
                  <div class="form-group">
                  <label for="supplier">Department Name</label><span style="display:inline-block; width: 20px;"></span>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddDept" ErrorMessage="This field is required" ForeColor="Red" InitialValue="-1"></asp:RequiredFieldValidator>
                  <asp:DropDownList ID="ddDept"  CssClass="form-control js-example-placeholder-single" CausesValidation="false" runat="server"></asp:DropDownList>
                 </div>
                  
                <div class="form-group">
                  <label for="brand">Position</label> <span style="display:inline-block; width: 20px;"></span>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbPosition" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                  <asp:TextBox ID="tbPosition" runat="server" CssClass="form-control" placeholder="Position" CausesValidation="false"></asp:TextBox>
                     </div>

                  <div class="form-group">
                  <label for="brand">Employee Number</label> <span style="display:inline-block; width: 20px;"></span>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbEmpNo" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                      <asp:TextBox ID="tbEmpNo" runat="server" CssClass="form-control" placeholder="Employee Number" CausesValidation="false"></asp:TextBox>               
                     </div>

                  <div class="form-group">
                  <label for="brand">Password</label> <span style="display:inline-block; width: 20px;"></span>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbPassword" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                  <asp:TextBox ID="tbPassword" runat="server" CssClass="form-control" placeholder="Password" CausesValidation="false" TextMode="Password"></asp:TextBox>
                     </div>

                   <div class="form-group">
                  <label for="brand">Confirm Password</label> <span style="display:inline-block; width: 20px;"></span>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tbConfPassword" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                  <asp:TextBox ID="tbConfPassword" runat="server" CssClass="form-control" placeholder="Confirm Password" CausesValidation="false" TextMode="Password"></asp:TextBox>
                     </div>
                
                  <div class="form-group">
                  <label for="brand">First Name</label> <span style="display:inline-block; width: 20px;"></span>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tbFname" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                  <asp:TextBox ID="tbFname" runat="server" CssClass="form-control" placeholder="First Name" CausesValidation="false"></asp:TextBox>
                     </div>

                  <div class="form-group">
                  <label for="brand">Last Name</label> <span style="display:inline-block; width: 20px;"></span>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="tbLname" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                  <asp:TextBox ID="tbLname" runat="server" CssClass="form-control" placeholder="Last Name" CausesValidation="false"></asp:TextBox>
                     </div>

                  <div class="form-group">
                  <label for="gender">Gender</label>
                  <div class="radio">
                      <label>
                      <asp:radiobutton ID="rbMale" runat="server" GroupName="gender"></asp:radiobutton>Male</label>
                      <label>
                      <asp:radiobutton ID="rbFemale" runat="server" GroupName="gender"></asp:radiobutton>Female</label>
                  </div>
                      </div>

              </div>
                
              <!-- /.box-body -->

              <div class="box-footer">
                  <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn btn-primary" OnClick="btnSave_Click"/>
                  <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-warning" CausesValidation="false" />
                  <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
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
