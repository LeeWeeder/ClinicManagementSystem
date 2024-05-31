namespace ClinicManagementSystem.Models
{

    // Full name does not include middle name
    public class PhysicianWithFullNameAndDepartment
    {
        public int StaffId { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string DisplayValue { get; private set; }

        public PhysicianWithFullNameAndDepartment(int StaffId, string FullName, string Department)
        {
            this.StaffId = StaffId;
            this.FullName = FullName;
            this.Department = Department;
            this.DisplayValue = this.FullName + ", " + this.Department;
        }
    }
}