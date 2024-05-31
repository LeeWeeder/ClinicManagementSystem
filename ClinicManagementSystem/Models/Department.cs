namespace ClinicManagementSystem.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public Department() { }

        public Department(int DepartmentId, string DepartmentName)
        {
            this.DepartmentId = DepartmentId;
            this.DepartmentName = DepartmentName;
        }

        public Department(string DepartmentName)
        {
            this.DepartmentName = DepartmentName;
        }
    }
}
