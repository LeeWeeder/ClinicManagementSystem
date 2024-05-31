﻿<%@ Page Title="Book Appointment" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="BookAppointment.aspx.cs" Inherits="ClinicManagementSystem.App_Pages.BookAppointment" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="modal" tabindex="-1" id="IsFirstTimeUserPrompt">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body p-3">
                    <p>Welcome! Is this your first time here?</p>
                    <button type="button" class="btn btn-outline-secondary mb-2 w-100" data-bs-dismiss="modal" aria-label="Close" data-bs-toggle="tooltip" title="First time user and haven't yet completed a visit to the clinic.">Yes, it's my first time, continue booking</button>
                    <a href="../../Account/Login.aspx?IsFirstTime=True" class="btn btn-outline-secondary mb-2 w-100" data-bs-toggle="tooltip" title="Login with your new username and password and view your last record.">Yes, but I already visited the clinic, proceed to login</a>
                    <a href="../../Account/Login.aspx?IsFirstTime=False" class="btn btn-outline-secondary mb-2 w-100" data-bs-toggle="tooltip" title="Login to restore existing records and book appointments for an existing case.">No, it's not my first time, proceed to login</a>
                </div>
            </div>
        </div>
    </div>
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
                    <asp:Button CssClass="btn btn-primary" Text="Check availability" runat="server" ID="CheckAvailabilityButton" OnClick="CheckAvailabilityButton_Click" />
                </div>
            </div>
        </div>
    </div>

    <script src="../../Scripts/App_Pages_Scripts/BookAppointment.js"></script>
    <script src="../../Scripts/ButtonGroup.js"></script>
</asp:Content>