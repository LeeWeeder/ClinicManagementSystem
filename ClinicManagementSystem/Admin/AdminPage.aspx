<%@ Page Title="Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="ClinicManagementSystem.Admin.AdminPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:LinkButton ID="RegisterStaffButton" runat="server" PostBackUrl="~/Account/Register.aspx">Register staff</asp:LinkButton>
</asp:Content>
