using System;
using System.Web;

namespace ClinicManagementSystem
{
    public partial class Forbidden : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpUtility.UrlDecode(Request.QueryString["ReturnUrl"]) == "/default.aspx")
            {
                if (HttpContext.Current.User.IsInRole("admin"))
                {
                    Response.Redirect("~/AdminPage/AdminDashboard.aspx");
                }
                else if (HttpContext.Current.User.IsInRole("patient"))
                {
                    Response.Redirect("~/PatientPage/PatientDashboard.aspx");
                }
                else if (HttpContext.Current.User.IsInRole("staff"))
                {
                    Response.Redirect("~/StaffPage/StaffDashboard.aspx");
                }
            }
        }
    }
}