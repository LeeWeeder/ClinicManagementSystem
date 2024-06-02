using ClinicManagementSystem.DBClass;
using System;

namespace ClinicManagementSystem.AdminPage
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                NumberOfActivePhysicians.Text = StaffDB.GetStaffByClinicRoleName("Physician", true).Count.ToString();
                NumberOfActivePatients.Text = PatientDB.GetPatients().Count.ToString() ?? "No active patients";
                NumberOfAssistivePersonnel.Text = StaffDB.GetStaffByClinicRoleName("Assistive Personnel", true).Count.ToString() ?? "No active assistive personnel";
                NumberOfLaboratoryPersonnel.Text = StaffDB.GetStaffByClinicRoleName("Laboratory Personnel", true).Count.ToString() ?? "No active laboratory personnel";
                NumberOfNurses.Text = StaffDB.GetStaffByClinicRoleName("Nurse", true).Count.ToString() ?? "No active nurse";
                NumberOfPharmacists.Text = StaffDB.GetStaffByClinicRoleName("Pharmacists", true).Count.ToString() ?? "No active pharmacist";
            }
        }
    }
}