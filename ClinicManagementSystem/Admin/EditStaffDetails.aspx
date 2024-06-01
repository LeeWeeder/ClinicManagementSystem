<%@ Page Title="Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditStaffDetails.aspx.cs" Inherits="ClinicManagementSystem.Admin.AdminPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .form-check {
            margin: 0;
        }
    </style>
    <div class="row justify-content-center">
        <div class="col-12 col-lg-8">
            <p class="text-danger">
                <asp:Literal runat="server" ID="ErrorMessage" />
            </p>
            <div class="card">
                <div class="card-body p-4">
                    <div class="row">
                        <h5 class="mb-2 card-title">Edit staff details with id: <%: Request.QueryString["Id"] %></h5>
                    </div>
                    <div class="row mb-3">
                        <div class="col">
                            <asp:Label AssociatedControlID="Status" CssClass="form-label d-block" runat="server">Status<span class="text-danger">*</span></asp:Label>
                            <asp:RadioButtonList ID="Status" ClientIDMode="Static" runat="server" RepeatLayout="Flow" CssClass="btn-group w-100">
                                <asp:ListItem Value="true">Active</asp:ListItem>
                                <asp:ListItem Value="false">Inactive</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <h6 class="mb-3 card-subtitle text-muted">Personal details</h6>
                    <div class="row mb-3">
                        <div class="col">
                            <asp:Label runat="server" AssociatedControlID="LastName" CssClass="form-label">Last name<span class="text-danger">*</span></asp:Label>
                            <asp:TextBox runat="server" ID="LastName" CssClass="form-control" placeholder="Doe" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="LastName"
                                CssClass="text-danger" ErrorMessage="Last name is required." Display="Dynamic" />
                            <asp:RegularExpressionValidator runat="server" ControlToValidate="LastName"
                                CssClass="text-danger" ErrorMessage="Last name must only contain letters." Display="Dynamic" ValidationExpression="^[a-zA-Z\s\-\']+$" />
                        </div>
                        <div class="col">
                            <asp:Label runat="server" AssociatedControlID="FirstName" CssClass="form-label">First name<span class="text-danger">*</span></asp:Label>
                            <asp:TextBox runat="server" ID="FirstName" CssClass="form-control" placeholder="John" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="FirstName"
                                CssClass="text-danger" ErrorMessage="First name is required." Display="Dynamic" />
                            <asp:RegularExpressionValidator runat="server" ControlToValidate="FirstName"
                                CssClass="text-danger" ErrorMessage="First name must only contain letters." Display="Dynamic" ValidationExpression="^[a-zA-Z\s\-\']+$" />
                        </div>
                        <div class="col">
                            <asp:Label runat="server" AssociatedControlID="MiddleName" CssClass="form-label">Middle name</asp:Label>
                            <asp:TextBox runat="server" ID="MiddleName" CssClass="form-control" ClientIDMode="Static" placeholder="Cena" />
                            <asp:RegularExpressionValidator runat="server" ControlToValidate="MiddleName"
                                CssClass="text-danger" ErrorMessage="Middle name must only contain letters." Display="Dynamic" ValidationExpression="^[a-zA-Z\s\-\']+$" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col">
                            <asp:Label AssociatedControlID="SexAtBirth" CssClass="form-label d-block" runat="server">Sex assigned at birth<span class="text-danger">*</span></asp:Label>
                            <asp:RadioButtonList ID="SexAtBirth" runat="server" ClientIDMode="Static" RepeatLayout="Flow" CssClass="btn-group w-100">
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <hr class="mt-4" />
                    <h6 class="card-subtitle mb-2 text-muted mt-3">Clinic assignment details</h6>
                    <div class="row mb-3">
                        <div class="col">
                            <asp:Label AssociatedControlID="DepartmentDropDownList" runat="server" CssClass="form-label">Department<span class="text-danger">*</span></asp:Label>
                            <asp:DropDownList runat="server" CssClass="form-select" ID="DepartmentDropDownList"></asp:DropDownList>
                        </div>
                        <div class="col">
                            <asp:Label AssociatedControlID="ClinicRoleDropDownList" runat="server" CssClass="form-label">Clinic role<span class="text-danger">*</span></asp:Label>
                            <asp:DropDownList runat="server" CssClass="form-select" ID="ClinicRoleDropDownList"></asp:DropDownList>
                        </div>
                    </div>
                    <hr class="mt-4" />
                    <h6 class="card-subtitle mb-2 text-muted mt-3">Account details</h6>
                    <div class="mb-3 row">
                        <div class="col">
                            <asp:Label runat="server" AssociatedControlID="Username" CssClass="form-label">Username<span class="text-danger">*</span></asp:Label>
                            <asp:TextBox runat="server" ID="Username" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Username"
                                CssClass="text-danger" ErrorMessage="Username is required." Display="Dynamic" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col">
                            <asp:Label runat="server" AssociatedControlID="EmailWithInputGroup" CssClass="form-label">Email<span class="text-danger">*</span></asp:Label>
                            <div class="input-group">
                                <asp:TextBox runat="server" ID="EmailWithInputGroup" CssClass="form-control" />
                                <span class="input-group-text">@cms.com</span>
                            </div>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="EmailWithInputGroup"
                                CssClass="text-danger" ErrorMessage="Email is required." Display="Dynamic" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col">
                            <asp:Label runat="server" AssociatedControlID="ContactNumber" CssClass="form-label">Contact number<span class="text-danger">*</span></asp:Label>
                            <asp:TextBox runat="server" ID="ContactNumber" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ContactNumber"
                                CssClass="text-danger" ErrorMessage="Contact number is required." Display="Dynamic" />
                            <asp:RegularExpressionValidator runat="server" ControlToValidate="ContactNumber"
                                CssClass="text-danger" ErrorMessage="Contact number must be a valid." Display="Dynamic" ValidationExpression="^\+?(\d{1,3})?[-. ]?\(?(\d{1,4})\)?[-. ]?(\d{1,4})[-. ]?(\d{1,9})$" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col">
                            <asp:Button runat="server" OnClick="UpdateStaff_Click" Text="Update" CssClass="btn btn-primary" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            $('#Status input').addClass("btn-check");
            $('#Status label').each(function (index, value) {
                if (index == 0) {
                    $(this).addClass("btn btn-outline-success");
                } else {
                    $(this).addClass("btn btn-outline-danger");
                }
            });
        })
    </script>

</asp:Content>
