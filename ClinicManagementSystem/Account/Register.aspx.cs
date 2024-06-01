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

                    LoadDropDownListItems(DepartmentDB.GetDepartments(), DepartmentDropDownList, "Select department", "DepartmentName", "DepartmentId");
                    LoadDropDownListItems(ClinicRoleDB.GetClinicRoles(), ClinicRoleDropDownList, "Select clinic role", "ClinicRoleName", "ClinicRoleId");
                }
                else if (currentUser.IsInRole("staff"))
                {
                    // HeadingText += "patient";
                }
                else
                {
                    UserNameField.Visible = true;
                    PasswordContainer.Visible = true;
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
            var username = UserName.Text.Trim();
            var password = Password.Text;
            var firstName = FirstName.Text.Trim();
            var lastName = LastName.Text.Trim();
            string department;
            var middleName = MiddleName.Text.Trim();

            // Check if the current user that is registering is anonymous
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                // TODO: Should listen for change in the url and cancel registration and booking appointment by deleting the initial appointment record.

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
                        // TODO: Implement ways to notify the user that they cannot edit this appointment anymore.
                        // Possible solutions: Throw page not found, throw forbidden access
                        HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    }
                }
            }
            else if (isAdmin)
            {
                userRole = "staff";
                email = EmailWithInputGroup.Text.Split('@')[0].Trim() + "@cms.com";
                username = email;
                returnUrl = "/Admin/Staff.aspx";
                password = username;
                department = DepartmentDropDownList.SelectedItem.Text;
            }

            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();

            if (string.IsNullOrEmpty(middleName))
            {
                middleName = null;
            }

            var user = new ApplicationUser() { UserName = username, Email = email, FirstName = FirstName.Text, LastName = LastName.Text, MiddleName = middleName, PhoneNumber = ContactNumber.Text, SexAtBirth = SexAtBirth.SelectedValue };
            IdentityResult result = manager.Create(user, password);

            var userId = manager.FindByName(username).Id;
            result = manager.AddToRole(userId, userRole);

            if (result.Succeeded)
            {
                if (isAdmin)
                {
                    StaffDB.InsertStaff(new Staff
                    {
                        StaffAspNetUsersId = userId,
                        StaffDepartmentId = Convert.ToInt32(DepartmentDropDownList.SelectedValue),
                        StaffClinicRoleId = Convert.ToInt32(ClinicRoleDropDownList.SelectedValue)
                    });
                }
                else
                {
                    PatientDB.InsertPatient(new Patient
                    {
                        PatientAspNetUsersId = userId,
                        PatientBirthDate = Convert.ToDateTime(BirthDate.Text)
                    });
                    signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                }

                IdentityHelper.RedirectToReturnUrl(returnUrl, Response);
            }
            else
            {
                ErrorMessage.Text = "Username or email already exists";
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
    }
}