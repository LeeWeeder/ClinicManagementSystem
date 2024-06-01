namespace ClinicManagementSystem.Models
{
    public class Staff
    {
        public int StaffId { get; set; }

        public string StaffAspNetUsersId { get; set; }

        public int StaffClinicRoleId { get; set; }

        public int? StaffDepartmentId { get; set; }

        public bool StaffIsActive { get; set; }

        public Staff() { }

        // With ID
        public Staff(
            int StaffId,
            string StaffAspNetUsersId,
            int StaffClinicRoleId,
            bool StaffIsActive,
            int? StaffDepartmentId = null
        )
        {
            this.StaffId = StaffId;
            this.StaffAspNetUsersId = StaffAspNetUsersId;
            this.StaffClinicRoleId = StaffClinicRoleId;
            this.StaffDepartmentId = StaffDepartmentId;
            this.StaffIsActive = StaffIsActive;
        }

        // Without ID
        public Staff(
            string StaffAspNetUsersId,
            int StaffClinicRoleId,
            bool StaffIsActive,
            int? StaffDepartmentId = null
        )
        {
            this.StaffAspNetUsersId = StaffAspNetUsersId;
            this.StaffClinicRoleId = StaffClinicRoleId;
            this.StaffDepartmentId = StaffDepartmentId;
            this.StaffIsActive = StaffIsActive;
        }

        // Without AspNetUsers ID
        public Staff(
            int StaffClinicRoleId,
            bool StaffIsActive,
            int? StaffDepartmentId = null
        )
        {
            this.StaffClinicRoleId = StaffClinicRoleId;
            this.StaffDepartmentId = StaffDepartmentId;
            this.StaffIsActive = StaffIsActive;
        }
    }
}
