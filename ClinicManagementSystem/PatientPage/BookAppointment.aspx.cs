using ClinicManagementSystem.DBClass;
using ClinicManagementSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClinicManagementSystem.App_Pages
{
    public partial class BookAppointment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPhysicians();
                LoadPatientCases();
                AppointmentDate.Attributes["min"] = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            }
        }

        protected void RequestBookingButton_Click(object sender, EventArgs e)
        {
            int? selectedPhysicianId;
            if (PhysiciansDropDownList.SelectedValue == "None")
            {
                selectedPhysicianId = null;
            }
            else
            {
                selectedPhysicianId = Convert.ToInt32(PhysiciansDropDownList.SelectedValue);
            }

            int? patientCaseId;
            if (PatientCaseDropDownList.SelectedValue == "Create new patient case")
            {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var patientId = PatientDB.GetPatientByAspNetUsersId(manager.FindByName(HttpContext.Current.User.Identity.Name).Id);

                patientCaseId = PatientCaseDB.InsertPatientCase(new PatientCase(Convert.ToDateTime(AppointmentDate.Text), "Pending", AppointmentType.Text, (int)patientId));
            }
            else
            {
                patientCaseId = Convert.ToInt32(PatientCaseDropDownList.SelectedValue);
            }

            var appointment = new Appointment
            {
                AppointmentDate = Convert.ToDateTime(AppointmentDate.Text),
                AppointmentAttendingStaffId = selectedPhysicianId,
                AppointmentStartTime = Convert.ToDateTime(AppointmentStartTime.Text),
                AppointmentType = AppointmentType.SelectedValue,
                AppointmentPatientCaseId = patientCaseId
            };

            AppointmentDB.InsertAppointment(appointment);

            Response.Redirect("PatientDashboard.aspx");
        }

        private void LoadPhysicians()
        {
            var physicians = StaffDB.GetPhysiciansWithFullNameAndDepartment();

            PhysiciansDropDownList.DataSource = physicians;
            PhysiciansDropDownList.DataValueField = "StaffId";
            PhysiciansDropDownList.DataTextField = "DisplayValue";

            PhysiciansDropDownList.DataBind();

            var item = new ListItem("None");
            item.Selected = true;

            PhysiciansDropDownList.Items.Insert(0, item);
        }

        private void LoadPatientCases()
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var patientId = PatientDB.GetPatientByAspNetUsersId(manager.FindByName(HttpContext.Current.User.Identity.Name).Id);
            var patientCases = PatientCaseDB.GetPatientCasesByPatientId((int)patientId);

            PatientCaseDropDownList.DataSource = patientCases;
            PatientCaseDropDownList.DataValueField = "PatientCaseId";
            PatientCaseDropDownList.DataTextField = "PatientCaseDescription";

            PatientCaseDropDownList.DataBind();

            var item = new ListItem("Create new patient case");
            item.Selected = true;

            PatientCaseDropDownList.Items.Insert(0, item);
        }

        private void DisableControls(Control parent, Type excludedType)
        {
            foreach (Control c in parent.Controls)
            {
                if (c.GetType() != excludedType)
                {
                    if (c is WebControl)
                    {
                        ((WebControl)c).Enabled = false;
                    }
                }

                if (c.Controls.Count > 0)
                {
                    DisableControls(c, excludedType);
                }
            }
        }
    }
}