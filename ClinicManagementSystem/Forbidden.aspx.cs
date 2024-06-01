using System;
using System.Web;

namespace ClinicManagementSystem
{
    public partial class Forbidden : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.IsInRole("admin"))
            {
                if (HttpUtility.UrlDecode(Request.QueryString["ReturnUrl"]) == "/default.aspx")
                {
                    Response.Redirect("~/Admin/Dashboard.aspx");
                }
            }
        }
    }
}