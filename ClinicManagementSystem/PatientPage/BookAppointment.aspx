<%@ Page Title="Book Appointment" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="BookAppointment.aspx.cs" Inherits="ClinicManagementSystem.App_Pages.BookAppointment" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Book appointment</h1>
        <div class="card p-4 mt-3">
            <h6 class="mb-3">Appointment details</h6>
            <div class="row mb-3" runat="server" id="PatientCaseRow">
                <div class="col">
                    <asp:Label AssociatedControlID="PatientCaseDropDownList" Text="Case" CssClass="form-label" runat="server"></asp:Label>
                    <asp:DropDownList runat="server" CssClass="form-select" ID="PatientCaseDropDownList"></asp:DropDownList>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col">
                    <asp:Label AssociatedControlID="AppointmentDate" CssClass="form-label" runat="server">Date<span class="text-danger">*</span></asp:Label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="AppointmentDate" TextMode="Date" ></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="AppointmentDate" Display="Dynamic" ErrorMessage="Appointment date is required." CssClass="text-danger" />
                    <asp:CustomValidator runat="server" ErrorMessage="Clinic is closed on Saturdays and Sundays." ControlToValidate="AppointmentDate" ClientValidationFunction="validateDate" CssClass="text-danger" Display="Dynamic" ></asp:CustomValidator>
                </div>
                <div class="col">
                    <asp:Label AssociatedControlID="AppointmentStartTime" CssClass="form-label" runat="server">Start time<span class="text-danger">*</span></asp:Label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="AppointmentStartTime" TextMode="Time"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="AppointmentStartTime" Display="Dynamic" ErrorMessage="Appointment start time is required." CssClass="text-danger" />
                    <asp:CustomValidator runat="server" ErrorMessage="Clinic is open on 9:00 AM to 5:00 PM." ControlToValidate="AppointmentStartTime" ClientValidationFunction="validateTime" CssClass="text-danger" Display="Dynamic" ></asp:CustomValidator>
                </div>
            </div>
            <div class="row mb-3">
                <asp:Label AssociatedControlID="AppointmentType" Text="Appointment type" CssClass="form-label" runat="server"></asp:Label>
                <asp:RadioButtonList ID="AppointmentType" runat="server" ClientIDMode="Static" RepeatLayout="Flow" CssClass="btn-group">
                    <asp:ListItem Selected="True">Doctor appointment</asp:ListItem>
                    <asp:ListItem>Clinical Test</asp:ListItem>
                    <asp:ListItem>Health and Wellness Services</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <!-- Show this row only if the type is doctor appointment. Add another option for None -->
            <div class="row mb-3" id="PhysiciansDropDownListContainer">
                <div class="col">
                    <asp:Label AssociatedControlID="PhysiciansDropDownList" Text="Preferred doctor" CssClass="form-label" runat="server"></asp:Label>
                    <asp:DropDownList runat="server" CssClass="form-select" ID="PhysiciansDropDownList" ClientIDMode="Static"></asp:DropDownList>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col">
                    <asp:Button CssClass="btn btn-primary" Text="Request booking" runat="server" ID="RequestBookingButton" OnClick="RequestBookingButton_Click"/>
                </div>
            </div>
        </div>
    </div>

    <script src="../../Scripts/App_Pages_Scripts/BookAppointment.js"></script>
</asp:Content>
