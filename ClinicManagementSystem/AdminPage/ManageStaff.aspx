<%@ Page Title="Staff" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageStaff.aspx.cs" Inherits="ClinicManagementSystem.AdminPage.ManageStaff" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="modal fade" id="DeleteConfirmationModal" tabindex="-1" aria-labelledby="DeleteConfirmationModal" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <asp:HiddenField runat="server" ID="Id" />
                        Delete staff with id: <span id="id"></span>?
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                    <asp:Button runat="server" OnClick="DeleteButton_Click" ID="DeleteButton" CssClass="btn btn-danger" Text="Delete" />
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid">
        <div class="d-flex justify-content-between">
            <h1>Staff</h1>
            <div>
                <a href="/Account/Register.aspx" class="btn btn-primary">Add staff</a>
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
                <asp:BoundField DataField="ClinicRole" HeaderText="Clinic&nbsp;role" />
                <asp:TemplateField HeaderText="Department">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("Department").ToString() == string.Empty ? "N/A" : Eval("Department")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SexAtBirth" HeaderText="Assigned sex&nbsp;at&nbsp;birth" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <a class="btn btn-primary" href='<%# "EditStaffDetails.aspx?Id=" + Eval("Id") %>'>Edit</a>
                        <button runat="server" class="btn btn-danger" data-bs-toggle="modal" data-bs-target="#DeleteConfirmationModal" data-bs-id='<%# Eval("Id") %>' type="button">Delete</button>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <script>
        $(document).ready(function () {
            $('#DeleteConfirmationModal').on('show.bs.modal', event => {
                // Button that triggered the modal
                const button = event.relatedTarget
                // Extract info from data-bs-* attributes
                const id = button.getAttribute('data-bs-id');
                // Update the modal's content.
                const staffId = $('#DeleteConfirmationModal #id');
                $('#DeleteButton').attr('CommandArgument', id);

                staffId.html(id);

                $('#<%= Id.ClientID %>').val(id);
            })
        })
    </script>
</asp:Content>
