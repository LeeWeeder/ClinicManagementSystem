namespace ClinicManagementSystem.DBClass
{
    public class StaffWithDetails
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string FullName { get; private set; }
        public string SexAtBirth { get; set; }
        public string ClinicRole { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string AspNetUsersId { get; set; }

        public StaffWithDetails() { }

        public StaffWithDetails(int Id, bool IsActive, string SexAtBirth, string ClinicRole, string Department, string Email, string ContactNumber, string Username, string FirstName, string MiddleName, string LastName, string AspNetUsersId)
        {
            this.Id = Id;
            this.IsActive = IsActive;
            this.Department = Department;
            this.ClinicRole = ClinicRole;
            this.SexAtBirth = SexAtBirth;
            this.Email = Email;
            this.ContactNumber = ContactNumber;
            this.Username = Username;
            this.FirstName = FirstName;
            this.MiddleName = MiddleName;
            this.LastName = LastName;
            this.AspNetUsersId = AspNetUsersId;
            FullName = LastName + ", " + FirstName + (!string.IsNullOrEmpty(MiddleName) ? (" " + MiddleName[0] + ".") : "");
        }
    }
}