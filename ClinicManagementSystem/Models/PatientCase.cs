namespace ClinicManagementSystem.Models
{
    using System;

    public partial class PatientCase
    {
        public int PatientCaseId { get; set; }

        public DateTime PatientCaseStartDate { get; set; }

        public DateTime? PatientCaseEndDate { get; set; }

        public string PatientCaseStatus { get; set; }

        public string PatientCaseDescription { get; set; }

        public int? PatientCasePatientId { get; set; }
    }
}
