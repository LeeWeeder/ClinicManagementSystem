using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Account
{
    public partial class Register : Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var email = Email.Text;
            var user = new ApplicationUser() { UserName = email, Email = email };
            IdentityResult result = manager.Create(user, Password.Text);

            var userId = manager.FindByEmail(email).Id;
            var userRole = "patient";
            if (!manager.IsInRole(userId, userRole))
            {
                result = manager.AddToRole(userId, userRole);
            }

            if (result.Succeeded)
            {
                signInManager.SignIn( user, isPersistent: false, rememberBrowser: false);
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else 
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}