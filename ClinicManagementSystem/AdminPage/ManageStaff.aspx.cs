using ClinicManagementSystem.DBClass;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Web;
using System.Web.UI.WebControls;

namespace ClinicManagementSystem.AdminPage
{
    public partial class ManageStaff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StaffList.DataSource = StaffDB.GetStaff();
                StaffList.DataBind();
            }
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(Id.Value);
            var staff = StaffDB.GetStaffById(id);
            StaffDB.DeleteStaff(id);

            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = manager.FindByEmail(staff.Email);
            manager.Delete(user);
            Response.Redirect("ManageStaff.aspx");
        }
    }
}