<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="ClinicManagementSystem.Admin.Dashboard" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h1 class="mb-2">Dashboard</h1>
        <hr />
        <div class="row">
            <div class="col">
                <a class="btn btn-primary d-flex p-4 flex-column">
                    <h5>Active&nbsp;physicians</h5>
                    <asp:Label runat="server" ID="NumberOfActivePhysicians" CssClass="h1"></asp:Label>
                </a>
            </div>
            <div class="col">
                <a class="btn btn-primary d-flex p-4 flex-column">
                    <h5>Active&nbsp;patients</h5>
                    <asp:Label runat="server" ID="NumberOfActivePatients" CssClass="h1"></asp:Label>
                </a>
            </div>
            <div class="col">
                <a class="btn btn-primary d-flex p-4 flex-column">
                    <h5>Active&nbsp;nurses</h5>
                    <asp:Label runat="server" ID="NumberOfNurses" CssClass="h1"></asp:Label>
                </a>
            </div>
        </div>
        <div class="row mt-4">
            <div class="col">
                <a class="btn btn-primary d-flex p-4 flex-column">
                    <h5>Laboratory&nbsp;personnel</h5>
                    <asp:Label runat="server" ID="NumberOfLaboratoryPersonnel" CssClass="h1"></asp:Label>
                </a>
            </div>
            <div class="col">
                <a class="btn btn-primary d-flex p-4 flex-column">
                    <h5>Pharmacists</h5>
                    <asp:Label runat="server" ID="NumberOfPharmacists" CssClass="h1"></asp:Label>
                </a>
            </div>
            <div class="col">
                <a class="btn btn-primary d-flex p-4 flex-column">
                    <h5>Assistive personnel</h5>
                    <asp:Label runat="server" ID="NumberOfAssistivePersonnel" CssClass="h1"></asp:Label>
                </a>
            </div>
        </div>
    </div>
</asp:Content>
