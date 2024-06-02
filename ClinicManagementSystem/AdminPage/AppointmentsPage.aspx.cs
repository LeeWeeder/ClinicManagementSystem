using ClinicManagementSystem.DBClass;
using ClinicManagementSystem.Models;
using System;

namespace ClinicManagementSystem.AdminPage
{
    public partial class AppoinmentsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Appointments.DataSource = AppointmentDB.GetAppointments();
                Appointments.DataBind();
            }
        }

        protected void RejectButton_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(Id.Value);
            var appointment = AppointmentDB.GetAppointmentById(id);
            appointment.AppointmentStatus = "Rejected";

            AppointmentDB.UpdateAppointment(appointment);
            Response.Redirect("AppointmentsPage.aspx");
        }
    }
}