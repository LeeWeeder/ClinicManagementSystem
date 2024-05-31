namespace ClinicManagementSystem.Models
{
    public class ClinicRole
    {
        public int ClinicRoleId { get; set; }

        public string ClinicRoleName { get; set; }

        public ClinicRole() { }

        public ClinicRole(int ClinicRoleId, string ClinicRoleName)
        {
            this.ClinicRoleId = ClinicRoleId;
            this.ClinicRoleName = ClinicRoleName;
        }

        public ClinicRole(string ClinicRoleName)
        {
            this.ClinicRoleName = ClinicRoleName;
        }
    }
}
