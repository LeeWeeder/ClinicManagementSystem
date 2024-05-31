using ClinicManagementSystem.DBClass;
using ClinicManagementSystem.Models;
using System;
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
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "IsFirstTimeUserPrompt.hide();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "IsFirstTimeUserPrompt.show();", true);
                    PatientCaseRow.Visible = false;
                }
                LoadPhysicians();
                AppointmentDate.Attributes["min"] = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            }
        }

        protected void CheckAvailabilityButton_Click(object sender, EventArgs e)
        {
            CheckAvailabilityButton.Attributes.Add("disabled", "disabled");
            int? selectedPhysicianId;
            if (PhysiciansDropDownList.SelectedValue == "None")
            {
                selectedPhysicianId = null;
            }
            else
            {
                selectedPhysicianId = Convert.ToInt32(PhysiciansDropDownList.SelectedValue);
            }

            var appointment = new Appointment
            {
                AppointmentDate = Convert.ToDateTime(AppointmentDate.Text),
                AppointmentAttendingStaffId = selectedPhysicianId,
                AppointmentStartTime = Convert.ToDateTime(AppointmentStartTime.Text),
                AppointmentType = AppointmentType.SelectedValue
            };

            var isAvailable = AppointmentDB.IsAppointmentAvailable(appointment);
            int? appointmentId = null;

            if (isAvailable)
            {
                appointmentId = AppointmentDB.InsertAppointment(appointment);
            }

            if (appointmentId != null)
            {
                Response.Redirect("~/Account/Register.aspx?AppointmentId=" + appointmentId);
            }
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
    }
}