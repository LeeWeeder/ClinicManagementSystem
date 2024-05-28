<%@ Page Title="Book Appointment" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="BookAppointment.aspx.cs" Inherits="ClinicManagementSystem.App_Pages.Appointment.BookAppointment" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="modal" tabindex="-1" id="IsFirstTimeUserPrompt" data-bs-backdrop="static" data-bs-keyboard="false">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body p-3">
                    <p>Welcome! Is this your first time here?</p>
                    <button type="button" class="btn btn-outline-secondary mb-2 w-100" data-bs-dismiss="modal" aria-label="Close" data-bs-toggle="tooltip" title="First time user and haven't yet completed a visit to the clinic.">Yes, it's my first time, continue booking</button>
                    <a href="../../Account/Login.aspx?IsFirstTime=True" class="btn btn-outline-secondary mb-2 w-100" data-bs-toggle="tooltip" title="Login with your new username and password and view your last record.">Yes, but I already visited the clinic, proceed to login</a>
                    <a href="../../Account/Login.aspx?IsFirstTime=False" class="btn btn-outline-secondary mb-2 w-100" data-bs-toggle="tooltip" title="Login to restore booked appointments and book appointments for an existing case.">No, it's not my first time, proceed to login</a>
                </div>
            </div>
        </div>
    </div>
    <script src="../../Scripts/App_Pages_Scripts/Appointment/BookAppointment.js"></script>
</asp:Content>
