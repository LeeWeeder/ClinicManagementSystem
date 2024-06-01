using ClinicManagementSystem.DBClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClinicManagementSystem.Admin
{
    public partial class Staff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StaffList.DataSource = StaffDB.GetStaff();
                StaffList.DataBind();
            }
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            Response.Redirect("EditStaffDetails.aspx?Id=" + button.CommandArgument);
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {

        }
    }
}