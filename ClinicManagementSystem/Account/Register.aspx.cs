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
                var HeadingText = Page.Title + " ";

                if (isAdmin)
                {
                    HeadingText += "staff";
                    EmailWithInputGroupContainer.Visible = true;
                    EmailContainer.Visible = false;
                    DepartmentAndClinicRoleContainer.Visible = true;
                    BirthDateContainer.Visible = false;

                    LoadDropDownListItems(DepartmentDB.GetDepartments(), DepartmentDropDownList, "Select department", "DepartmentName", "DepartmentId");
                    LoadDropDownListItems(ClinicRoleDB.GetClinicRoles(), ClinicRoleDropDownList, "Select clinic role", "ClinicRoleName", "ClinicRoleId");
                }
                else if (currentUser.IsInRole("staff"))
                {
                    HeadingText += "patient";
                }
                else
                {
                    UserNameField.Visible = true;
                    PasswordContainer.Visible = true;
                }

                Heading.Text = HeadingText;
            }
        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var userRole = "patient";
            
            var returnUrl = Request.QueryString["ReturnUrl"];

            string email = Email.Text.Trim();
            string username = UserName.Text.Trim();
            string password = Password.Text;

            if (isAdmin)
            {
                userRole = "staff";
                email = EmailWithInputGroup.Text.Split('@')[0].Trim() + "@cms.com";
                username = email;
                returnUrl = "/Admin/AdminPage.aspx";
                password = username;
            }

            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();

            var firstName = FirstName.Text.Trim();
            var lastName = LastName.Text.Trim();
            var department = DepartmentDropDownList.SelectedItem.Text;
            var middleName = MiddleName.Text.Trim();

            if (string.IsNullOrEmpty(middleName))
            {
                middleName = null;
            }

            var user = new ApplicationUser() { UserName = username, Email = email, FirstName = FirstName.Text, LastName = LastName.Text, MiddleName = middleName, PhoneNumber = ContactNumber.Text };
            IdentityResult result = manager.Create(user, password);

            if (!result.Succeeded)
            {
                throw new Exception(isAdmin.ToString());
            }

            var userId = manager.FindByEmail(email).Id;
            result = manager.AddToRole(userId, userRole);

            if (result.Succeeded)
            {
                StaffDB.InsertStaff(new Staff
                {
                    StaffAspNetUsersId = userId,
                    StaffDepartmentId = Convert.ToInt32(DepartmentDropDownList.SelectedValue),
                    StaffClinicRoleId = Convert.ToInt32(ClinicRoleDropDownList.SelectedValue)
                });

                if (!isAdmin)
                {
                    signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                }

                IdentityHelper.RedirectToReturnUrl(returnUrl, Response);
            }
            else
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
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