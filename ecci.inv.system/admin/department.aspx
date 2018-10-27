<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Superadmin.Master" AutoEventWireup="true" CodeBehind="department.aspx.cs" Inherits="ecci.inv.system.superadmin.department" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
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
        Add Department
        <small>Admin</small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Add Department</li>
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
            <h3 class="box-title">Add Department</h3>
          </div>

              <div class="box-body">
                  
                <div class="form-group">
                  <label for="brand">Department Name</label> <span style="display:inline-block; width: 20px;"></span>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbDeptName" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                    
                    <asp:TextBox ID="tbDeptName" runat="server" CssClass="form-control" placeholder="Department Name" CausesValidation="false"></asp:TextBox>
                     
                        </div>
                
                <div class="form-group">
                  <label for="description">Department Head</label><span style="display:inline-block; width: 20px;"></span>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbDeptHead" ErrorMessage="This field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                  <asp:TextBox ID="tbDeptHead" runat="server" CssClass="form-control" placeholder="Description" CausesValidation="false"></asp:TextBox>
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
