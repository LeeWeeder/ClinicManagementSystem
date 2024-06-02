<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AppointmentsPage.aspx.cs" Inherits="ClinicManagementSystem.AdminPage.AppoinmentsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="modal fade" id="RejectConfirmationModal" tabindex="-1" aria-labelledby="DeleteConfirmationModal" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <asp:HiddenField runat="server" ID="Id" />
                        Reject booking appointment with id: <span id="id"></span>?
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                    <asp:Button runat="server" OnClick="RejectButton_Click" ID="RejectButton" CssClass="btn btn-danger" Text="Reject" />
                </div>
            </div>
        </div>
    </div>
    <h1>Appointments</h1>
    <div class="container-fluid">
        <div class="table-responsive">
            <asp:GridView CssClass="table" runat="server" ID="Appointments" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="AppointmentId" HeaderText="Id" />
                    <asp:BoundField DataField="AppointmentPatientCaseId" HeaderText="Case id" />
                    <asp:TemplateField HeaderText="Attending staff">
                        <ItemTemplate>
                            <asp:Label runat="server"><%# Eval("AppointmentAttendingStaffName") != null ? Eval("AppointmentAttendingStaffName") : "Unassigned" %></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Appointment date">
                        <ItemTemplate>
                            <asp:Label runat="server"><%# Eval("AppointmentDate", "{0:d}") %></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start time">
                        <ItemTemplate>
                            <asp:Label runat="server"><%# Eval("AppointmentStartTime", "{0:t}") %></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="End time">
                        <ItemTemplate>
                            <asp:Label runat="server"><%# Eval("AppointmentEndTime") != null ? Eval("AppointmentEndTime", "{0:t}") : "Pending" %></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="AppointmentType" HeaderText="Appointment type" />
                    <asp:BoundField DataField="AppointmentStatus" HeaderText="Status" />
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <a class="btn btn-primary" href='<%# "ManageAppointment.aspx?Id=" + Eval("AppointmentId") %>'>Manage</a>
                            <button runat="server" class="btn btn-danger" data-bs-toggle="modal" data-bs-target="#RejectConfirmationModal" data-bs-id='<%# Eval("AppointmentId") %>' type="button">Reject</button>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
     <script>
        $(document).ready(function () {
            $('#RejectConfirmationModal').on('show.bs.modal', event => {
                // Button that triggered the modal
                const button = event.relatedTarget
                // Extract info from data-bs-* attributes
                const id = button.getAttribute('data-bs-id');
                // Update the modal's content.
                const appointmentId = $('#RejectConfirmationModal #id');
                $('#RejectButton').attr('CommandArgument', id);

                appointmentId.html(id);

                $('#<%= Id.ClientID %>').val(id);
            })
        })
    </script>
</asp:Content>
