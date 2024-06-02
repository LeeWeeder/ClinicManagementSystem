<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ClinicManagementSystem._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <section id="hero" style="height: var(100vh - 50px);">
         <div class="bg-light rounded-3 p-5">
        <div class="container">
            <h1 class="display-5 fw-bold">Clinic Management System</h1>
            <h1>for patients and doctors</h1>
            <p class="fs-5 mt-5">Welcome to the future of healthcare management! Our <strong>Clinic Management System</strong> is designed to streamline your clinic’s operations, ensuring a seamless experience for both patients and staff. With our intuitive dashboard, you can easily schedule appointments, manage patient records, and access medical histories with just a few clicks. Experience the power of technology in healthcare today!</p>

            <div class="d-flex mt-5">
                <p class="me-4"><a href="Account/Login.aspx" class="btn btn-outline-secondary btn-lg">Log in</a></p>
                <p><a href="Account/Register.aspx" class="btn btn-primary btn-lg">Sign up</a></p>
            </div>
        </div>
    </div>
    </section>
</asp:Content>
