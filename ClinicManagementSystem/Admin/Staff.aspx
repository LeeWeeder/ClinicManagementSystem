<%@ Page Title="Staff" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Staff.aspx.cs" Inherits="ClinicManagementSystem.Admin.Staff" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <h1 class="col">Staff</h1>
            <div class="col position-relative">
                <a href="/Account/Register.aspx" class="btn btn-primary position-absolute end-0">Create staff</a>
            </div>
        </div>
    </div>
    <div class="table-responsive">
        <asp:GridView ID="StaffList" runat="server" CssClass="table" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" />
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <span class="badge rounded-pill text-bg-<%# Convert.ToBoolean(Eval("IsActive")) ? "success" : "danger" %>"><%# Convert.ToBoolean(Eval("IsActive")) ? "Active" : "Inactive" %></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="FullName" HeaderText="Name" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="ContactNumber" HeaderText="Contact&nbsp;number" />
                <asp:BoundField DataField="Username" HeaderText="Username" />
                <asp:BoundField DataField="ClinicRole" HeaderText="Clinic&nbsp;role" />
                <asp:BoundField DataField="Department" HeaderText="Department" />
                <asp:BoundField DataField="SexAtBirth" HeaderText="Assigned sex&nbsp;at&nbsp;birth" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="EditButton_Click" ID="EditButton" CssClass="btn btn-primary" Text="Edit" />
                        &nbsp;
                                <asp:Button runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="DeleteButton_Click" ID="DeleteButton" CssClass="btn btn-danger" Text="Delete" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
