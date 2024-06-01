using ClinicManagementSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Configuration;

namespace ClinicManagementSystem.Logic
{
    internal class RoleActions
    {
        internal void AddUserAndRole()
        {
            Models.ApplicationDbContext context = new ApplicationDbContext();
            IdentityResult IdRoleResult;
            IdentityResult IdAdminResult;

            var roleStore = new RoleStore<IdentityRole>(context);

            var roleManager = new RoleManager<IdentityRole>(roleStore);

            if (!roleManager.RoleExists("admin"))
            {
                IdRoleResult = roleManager.Create(new IdentityRole { Name = "admin" });
            }

            if (!roleManager.RoleExists("patient"))
            {
                IdRoleResult = roleManager.Create(new IdentityRole { Name = "patient" });
            }

            if (!roleManager.RoleExists("staff"))
            {
                IdRoleResult = roleManager.Create(new IdentityRole { Name = "staff" });
            }


            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 5
            };

            var admin = new ApplicationUser
            {
                UserName = "admin"
            };
            IdAdminResult = userManager.Create(admin, ConfigurationManager.AppSettings["AdminPassword"]);

            if (!userManager.IsInRole(userManager.FindByName("admin").Id, "admin"))
            {
                IdAdminResult = userManager.AddToRole(userManager.FindByName("admin").Id, "admin");
            }
        }
    }
}