using ClinicManagementSystem.DBClass;
using ClinicManagementSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;

namespace ClinicManagementSystem.Admin
{
    public partial class AdminPage : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id;
                if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                {
                    id = Convert.ToInt32(Request.QueryString["Id"]);
                }
                else
                {
                    Response.Redirect("~/Admin/Staff.aspx");
                    return;
                }

                StaffWithDetails staff = StaffDB.GetStaffById(id);
                var departments = DepartmentDB.GetDepartments();
                var selectedDepartment = departments.FindIndex(department => department.DepartmentName == staff.Department);
                LoadDropDownListItems(departments, DepartmentDropDownList, selectedDepartment, "DepartmentName", "DepartmentId");

                var clinicRoles = ClinicRoleDB.GetClinicRoles();
                var selectedClinicRole = clinicRoles.FindIndex(clinicRole => clinicRole.ClinicRoleName == staff.ClinicRole);
                LoadDropDownListItems(clinicRoles, ClinicRoleDropDownList, selectedClinicRole, "ClinicRoleName", "ClinicRoleId");

                EmailWithInputGroup.Text = staff.Email.Split('@')[0];
                Username.Text = staff.Username;
                FirstName.Text = staff.FirstName;
                MiddleName.Text = staff.MiddleName;
                LastName.Text = staff.LastName;
                ContactNumber.Text = staff.ContactNumber;
                SexAtBirth.SelectedValue = staff.SexAtBirth;
                Status.SelectedValue = staff.IsActive.ToString().ToLower();
            }
        }

        // Creates user
        protected void UpdateStaff_Click(object sender, EventArgs e)
        {
            var email = EmailWithInputGroup.Text.Split('@')[0].Trim() + "@cms.com";
            var username = Username.Text.Trim();
            var firstName = FirstName.Text.Trim();
            var lastName = LastName.Text.Trim();
            var department = DepartmentDropDownList.SelectedItem.Text;
            var middleName = MiddleName.Text.Trim();
            var phoneNumber = ContactNumber.Text.Trim();
            var sexAtBirth = SexAtBirth.SelectedValue;
            var status = Convert.ToBoolean(Status.SelectedValue);
            var id = Convert.ToInt32(Request.QueryString["Id"]);
            var aspNetUsersId = StaffDB.GetStaffById(id).AspNetUsersId;

            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();

            if (string.IsNullOrEmpty(middleName))
            {
                middleName = null;
            }

            var user = new ApplicationUser() { UserName = username, Email = email, FirstName = firstName, LastName = lastName, MiddleName = middleName, PhoneNumber = phoneNumber, SexAtBirth = sexAtBirth, Id = aspNetUsersId };

            IdentityResult result = manager.Update(user);

            var userId = manager.FindByName(username).Id;

            if (result.Succeeded)
            {
                StaffDB.UpdateStaff(new Models.Staff
                {
                    StaffId = id,
                    StaffDepartmentId = Convert.ToInt32(DepartmentDropDownList.SelectedValue),
                    StaffClinicRoleId = Convert.ToInt32(ClinicRoleDropDownList.SelectedValue),
                    StaffIsActive = status
                });

                IdentityHelper.RedirectToReturnUrl("/Admin/Staff.aspx", Response);
            }
            else
            {
                ErrorMessage.Text = "Error updating staff details";
            }
        }

        private void LoadDropDownListItems<T>(List<T> dataSource, DropDownList dropDownControl, int selectedIndex, string textField, string valueField)
        {
            dropDownControl.DataSource = dataSource;
            dropDownControl.DataTextField = textField;
            dropDownControl.DataValueField = valueField;
            dropDownControl.DataBind();
            dropDownControl.SelectedIndex = selectedIndex;
        }
    }
}