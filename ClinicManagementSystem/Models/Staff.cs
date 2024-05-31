namespace ClinicManagementSystem.Models
{
    public class Staff
    {
        public int StaffId { get; set; }

        public string StaffAspNetUsersId { get; set; }

        public int StaffClinicRoleId { get; set; }

        public int? StaffDepartmentId { get; set; }

        public Staff() { }

        // With ID
        public Staff(
            int StaffId, 
            string StaffAspNetUsersId,
            int StaffRoleId,
            int? StaffDepartmentId = null
        )
        {
            this.StaffId = StaffId;
            this.StaffAspNetUsersId = StaffAspNetUsersId;
            this.StaffClinicRoleId = StaffRoleId;
            this.StaffDepartmentId = StaffDepartmentId;
        }

        // Without ID
        public Staff(
            string StaffAddress,
            string StaffAspNetUsersId,
            int StaffRoleId,
            int? StaffDepartmentId = null
        )
        {
            this.StaffAspNetUsersId = StaffAspNetUsersId;
            this.StaffClinicRoleId = StaffRoleId;
            this.StaffDepartmentId = StaffDepartmentId;
        }
    }
}
