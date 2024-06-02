using ClinicManagementSystem.DBClass;
using ClinicManagementSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;

namespace ClinicManagementSystem.AdminPage
{
    public partial class EditStaffDetails : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id;
                if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                {
                    id = Convert.ToInt32(Request.QueryString["Id"]);

                    if (StaffDB.GetStaffById(id) == null)
                    {
                        Response.Redirect("~/AdminPage/ManageStaff.aspx" + id);
                        return;
                    }
                }
                else
                {
                    Response.Redirect("~/AdminPage/ManageStaff.aspx");
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

            var staff = manager.FindById(aspNetUsersId);

            staff.UserName = email;
            staff.Email = email;
            staff.FirstName = firstName;
            staff.LastName = lastName;
            staff.MiddleName = middleName;
            staff.PhoneNumber = phoneNumber;
            staff.SexAtBirth = sexAtBirth;

            IdentityResult result = manager.Update(staff);

            if (result.Succeeded)
            {
                StaffDB.UpdateStaff(new Staff
                {
                    StaffId = id,
                    StaffDepartmentId = Convert.ToInt32(DepartmentDropDownList.SelectedValue),
                    StaffClinicRoleId = Convert.ToInt32(ClinicRoleDropDownList.SelectedValue),
                    StaffIsActive = status
                });

                IdentityHelper.RedirectToReturnUrl("/AdminPage/ManageStaff.aspx", Response);
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