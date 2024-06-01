<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ClinicManagementSystem.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <!-- Style specific for this form -->
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
                        <h5 class="mb-2 card-title">Create a new account</h5>
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
                        <div class="col" id="BirthDateContainer" runat="server" visible="true">
                            <asp:Label runat="server" AssociatedControlID="BirthDate" CssClass="form-label">Date of birth<span class="text-danger">*</span></asp:Label>
                            <asp:TextBox runat="server" ID="BirthDate" CssClass="form-control" TextMode="Date" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="BirthDate"
                                CssClass="text-danger" ErrorMessage="Date of birth is required." Display="Dynamic" />
                        </div>
                        <div class="col">
                            <asp:Label AssociatedControlID="SexAtBirth" CssClass="form-label d-block" runat="server">Sex assigned at birth<span class="text-danger">*</span></asp:Label>
                            <asp:RadioButtonList ID="SexAtBirth" runat="server" ClientIDMode="Static" RepeatLayout="Flow" CssClass="btn-group w-100">
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="SexAtBirth" CssClass="text-danger" Display="Dynamic" ErrorMessage="Sex assigned at birth is required." />
                        </div>
                    </div>
                    <div id="DepartmentAndClinicRoleContainer" class="mt-4" visible="false" runat="server">
                        <hr />
                        <h6 class="card-subtitle mb-2 text-muted mt-3">Clinic assignment details</h6>
                        <div class="row mb-3">
                            <div class="col">
                                <asp:Label AssociatedControlID="DepartmentDropDownList" runat="server" CssClass="form-label">Department<span class="text-danger">*</span></asp:Label>
                                <asp:DropDownList runat="server" CssClass="form-select" ID="DepartmentDropDownList"></asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="DepartmentDropDownList" CssClass="text-danger" Display="Dynamic" ErrorMessage="Department is required." InitialValue="Select department" />
                            </div>
                            <div class="col">
                                <asp:Label AssociatedControlID="ClinicRoleDropDownList" runat="server" CssClass="form-label">Clinic role<span class="text-danger">*</span></asp:Label>
                                <asp:DropDownList runat="server" CssClass="form-select" ID="ClinicRoleDropDownList"></asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ClinicRoleDropDownList" CssClass="text-danger" Display="Dynamic" ErrorMessage="Clinic role is required." InitialValue="Select clinic role" />
                            </div>
                        </div>
                    </div>
                    <hr class="mt-4" />
                    <h6 class="card-subtitle mb-2 text-muted mt-3">Account details</h6>
                    <div class="mb-3 row" runat="server" id="UserNameField" visible="false">
                        <div class="col">
                            <asp:Label runat="server" AssociatedControlID="UserName" CssClass="form-label">Username<span class="text-danger">*</span></asp:Label>
                            <asp:TextBox runat="server" ID="UserName" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                                CssClass="text-danger" ErrorMessage="Username is required." Display="Dynamic" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col">
                            <div runat="server" id="EmailWithInputGroupContainer" visible="false">
                                <asp:Label runat="server" AssociatedControlID="EmailWithInputGroup" CssClass="form-label">Email<span class="text-danger">*</span></asp:Label>
                                <div class="input-group">
                                    <asp:TextBox runat="server" ID="EmailWithInputGroup" CssClass="form-control" />
                                    <span class="input-group-text">@cms.com</span>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="EmailWithInputGroup"
                                    CssClass="text-danger" ErrorMessage="Email is required." Display="Dynamic" />
                            </div>
                            <div runat="server" id="EmailContainer" visible="true">
                                <asp:Label runat="server" AssociatedControlID="Email" CssClass="form-label">Email</asp:Label>
                                <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                                <asp:RegularExpressionValidator runat="server" ControlToValidate="Email"
                                    CssClass="text-danger" ErrorMessage="Email must be a valid." Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                            </div>
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
                    <div id="PasswordContainer" runat="server" visible="false">
                        <div class="row mb-3">
                            <div class="col">
                                <asp:Label runat="server" AssociatedControlID="Password" CssClass="form-label">Password<span class="text-danger">*</span></asp:Label>
                                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                                    CssClass="text-danger" ErrorMessage="Password is required." Display="Dynamic" />
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col">
                                <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="form-label">Confirm password<span class="text-danger">*</span></asp:Label>
                                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                                    CssClass="text-danger" Display="Dynamic" ErrorMessage="Confirm password is required." />
                                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col">
                            <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="btn btn-primary" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../Scripts/Account_Scripts/Register.js"></script>
</asp:Content>
