using ClinicManagementSystem.DBClass;
using ClinicManagementSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClinicManagementSystem.App_Pages.Appointment
{
    public partial class BookAppointment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.User.IsInRole("patient"))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "IsFirstTimeUserPrompt.hide();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "IsFirstTimeUserPrompt.show();", true);
                    PatientCaseRow.Visible = false;
                }
            }
        }

        protected void CheckAvailabilityButton_Click(object sender, EventArgs e)
        {
            var PatientCaseId = PatientCaseDropDownList.SelectedValue;
        }
    }
}