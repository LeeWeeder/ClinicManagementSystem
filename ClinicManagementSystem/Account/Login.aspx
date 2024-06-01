<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ClinicManagementSystem.Account.Login" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="row justify-content-center">
        <div class="col-12 col-lg-6">
            <div class="card">
                <div class="card-body p-4">
                    <div class="card-title">
                        <asp:Label runat="server" CssClass="h4">Login</asp:Label>
                    </div>
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="row mb-3 mt-4">
                        <div class="col">
                            <div class="form-floating">
                                <asp:TextBox runat="server" ID="Username" CssClass="form-control" placeholder="username" />
                                <asp:Label runat="server" AssociatedControlID="Username" CssClass="form-label">Username</asp:Label>
                            </div>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Username"
                                CssClass="text-danger" ErrorMessage="Email or username is required." Display="Dynamic" />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col">
                            <div class="form-floating">
                                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" placeholder="Password" />
                                <asp:Label runat="server" AssociatedControlID="Password" CssClass="form-label">Password</asp:Label>
                            </div>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="Password is required." Display="Dynamic" />
                        </div>
                    </div>
                    <div class="row mb-3 ps-2">
                        <asp:CheckBox runat="server" ID="RememberMe" CssClass="form-check" Text="Remember me?" />
                    </div>
                    <div class="row mb-3">
                        <div class="col">
                            <asp:Button runat="server" OnClick="LogIn" Text="Log in" CssClass="btn btn-primary" />
                        </div>
                    </div>
                    <p>
                        <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Register as a new user</asp:HyperLink>
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
