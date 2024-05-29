using ClinicManagementSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;

namespace ClinicManagementSystem.Logic
{
    internal class RoleActions
    {
        internal void AddUserAndRole()
        {
            Models.ApplicationDbContext context = new ApplicationDbContext();
            IdentityResult IdRoleResult;
            IdentityResult IdUserResult;

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
            var applicationUser = new ApplicationUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com"
            };
            IdUserResult = userManager.Create(applicationUser, ConfigurationManager.AppSettings["AppUserPasswordKey"]);

            if (!userManager.IsInRole(userManager.FindByEmail("admin@admin.com").Id, "admin"))
            {
                IdUserResult = userManager.AddToRole(userManager.FindByEmail("admin@admin.com").Id, "admin");
            }
        }
    }
}