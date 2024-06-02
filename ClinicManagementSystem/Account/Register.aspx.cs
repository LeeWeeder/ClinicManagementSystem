using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Security.Principal;
using Microsoft.AspNet.Identity.Owin;
using ClinicManagementSystem.Models;
using Microsoft.AspNet.Identity;
using ClinicManagementSystem.DBClass;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Net;

namespace ClinicManagementSystem.Account
{
    public partial class Register : Page
    {
        private IPrincipal currentUser = HttpContext.Current.User;
        private bool isAdmin;

        protected void Page_Load(object sender, EventArgs e)
        {
            isAdmin = currentUser.IsInRole("admin");
            if (!IsPostBack)
            {
                BirthDate.Attributes["max"] = DateTime.Now.ToString("yyyy-MM-dd");
                // var HeadingText = Page.Title + " ";

                if (isAdmin)
                {
                    // HeadingText += "staff";
                    EmailWithInputGroupContainer.Visible = true;
                    EmailContainer.Visible = false;
                    DepartmentAndClinicRoleContainer.Visible = true;
                    BirthDateContainer.Visible = false;
                    PasswordContainer.Visible = false;

                    LoadDropDownListItems(DepartmentDB.GetDepartments(), DepartmentDropDownList, "Select department", "DepartmentName", "DepartmentId");
                    LoadDropDownListItems(ClinicRoleDB.GetClinicRoles(), ClinicRoleDropDownList, "Select clinic role", "ClinicRoleName", "ClinicRoleId");
                }
                else if (currentUser.IsInRole("staff"))
                {
                    // HeadingText += "patient";
                }

                // Heading.Text = HeadingText;
            }
        }

        // Creates user
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            // Set the default role of the user that is to be registered to patient as out of the three possible insertion of users, two of which is patient is the role
            var userRole = "patient";

            // Set the default returnUrl to the value passed in ReturnUrl argument if the user directly access a restricted page. 
            var returnUrl = Request.QueryString["ReturnUrl"];

            var email = Email.Text.Trim();
            var password = Password.Text;
            var firstName = FirstName.Text.Trim();
            var lastName = LastName.Text.Trim();
            int? department=  null;
            var middleName = MiddleName.Text.Trim();

            // Check if the current user that is registering is anonymous
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                // If anonynous, check if there is a value to the AppointmentId passed as navigational arguments
                if (!string.IsNullOrEmpty(Request.QueryString["AppointmentId"]))
                {
                    // If there is value, which means, not null or empty then get the AppointmentId
                    var AppointmentId = int.Parse(Request.QueryString["AppointmentId"]);

                    // To avoid conflict in PatientCase of an Appointment, check if the set AppointmentId already have a PatientCase associated with it.
                    // Get the appointment with the AppointmentId
                    var appointment = AppointmentDB.GetAppointmentById(AppointmentId);

                    // Check if patientCaseId is already set
                    if (appointment.AppointmentPatientCaseId != null)
                    {
                        HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    }
                    else
                    {
                        returnUrl = "/Default.aspx";
                        password = email;
                    }
                }
            }
            else if (isAdmin)
            {
                userRole = "staff";
                email = EmailWithInputGroup.Text.Split('@')[0].Trim() + "@cms.com";
                returnUrl = "/AdminPage/ManageStaff.aspx";
                password = email;
                if (DepartmentDropDownList.SelectedValue == "Select department")
                {
                    department = null;
                }
                else
                {
                    department = Convert.ToInt32(DepartmentDropDownList.SelectedValue);
                }
            }

            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();

            if (string.IsNullOrEmpty(middleName))
            {
                middleName = null;
            }

            var user = new ApplicationUser() { UserName = email, Email = email, FirstName = FirstName.Text, LastName = LastName.Text, MiddleName = middleName, PhoneNumber = ContactNumber.Text, SexAtBirth = SexAtBirth.SelectedValue };
            IdentityResult result = manager.Create(user, password);

            var userId = manager.FindByEmail(email).Id;
            result = manager.AddToRole(userId, userRole);

            if (result.Succeeded)
            {
                if (isAdmin)
                {
                    StaffDB.InsertStaff(new Staff
                    {
                        StaffAspNetUsersId = userId,
                        StaffDepartmentId = department,
                        StaffClinicRoleId = Convert.ToInt32(ClinicRoleDropDownList.SelectedValue)
                    });
                }
                else if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var patientId = PatientDB.InsertPatient(new Models.Patient
                    {
                        PatientAspNetUsersId = userId,
                        PatientBirthDate = Convert.ToDateTime(BirthDate.Text)
                    });



                    if (string.IsNullOrEmpty(Request.QueryString["AppointmentId"]))
                    {
                        signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                    }
                    // Register user within booking process
                    else
                    {
                        var AppointmentId = int.Parse(Request.QueryString["AppointmentId"]);

                        var appointment = AppointmentDB.GetAppointmentById(AppointmentId);
                        // Create new patient case based on the patient id
                        var patientCaseId = PatientCaseDB.InsertPatientCase(new PatientCase(appointment.AppointmentDate, "Pending", appointment.AppointmentType, patientId));
                        // Set the appointment to this patient case
                        AppointmentDB.UpdateAppointment(new Appointment(appointment.AppointmentId, patientCaseId, appointment.AppointmentAttendingStaffId, appointment.AppointmentDate, appointment.AppointmentStartTime, appointment.AppointmentEndTime, appointment.AppointmentType, appointment.AppointmentStatus, appointment.AppointmentAttendingStaffName));


                    }
                }

                IdentityHelper.RedirectToReturnUrl(returnUrl, Response);
            }
            else
            {
                ErrorMessage.Text =result.Errors.FirstOrDefault();
            }
        }

        private void LoadDropDownListItems<T>(List<T> dataSource, DropDownList dropDownControl, string defaultItemName, string textField, string valueField)
        {
            dropDownControl.DataSource = dataSource;
            dropDownControl.DataTextField = textField;
            dropDownControl.DataValueField = valueField;
            dropDownControl.DataBind();

            var defaultItem = new ListItem(defaultItemName);
            defaultItem.Selected = true;
            defaultItem.Attributes.Add("disabled", "disabled");
            dropDownControl.Items.Insert(0, defaultItem);
        }

        protected void ClinicRoleDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var clinicRole = (DropDownList)sender;
            if (clinicRole.SelectedValue == "1")
            {
                DepartmentContainer.Visible = true;
            }
            else
            {
                DepartmentContainer.Visible = false;
            }
        }
    }
}