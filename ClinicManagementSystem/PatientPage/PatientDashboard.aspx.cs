using ClinicManagementSystem.DBClass;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClinicManagementSystem.PatientPage
{
    public partial class PatientDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var patientId = PatientDB.GetPatientByAspNetUsersId(manager.FindByName(HttpContext.Current.User.Identity.Name).Id);
                Appointments.DataSource = AppointmentDB.GetAppointmentsByPatientId((int)patientId);
                Appointments.DataBind();
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(Id.Value);
             AppointmentDB.DeleteAppointment(id);
            Response.Redirect("PatientDashboard.aspx");
        }
    }
}