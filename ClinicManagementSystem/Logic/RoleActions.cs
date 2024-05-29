using ClinicManagementSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;

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
                if (!IdUserResult.Succeeded)
                {
                    Debug.WriteLine(IdUserResult.Errors);
                }
            }
        }
    }
}