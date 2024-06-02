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

        public int PatientCasePatientId { get; set; }

        public DateTime PatientCaseDateCreated { get; set; }

        public PatientCase() { }

        public PatientCase(int PatientCaseId, DateTime PatientCaseStartDate, DateTime? PatientCaseEndDate, string PatientCaseStatus, string PatientCaseDescription, int PatientCasePatientId, DateTime PatientCaseDateCreated)
        {
            this.PatientCaseId = PatientCaseId;
            this.PatientCaseStartDate = PatientCaseStartDate;
            this.PatientCaseEndDate = PatientCaseEndDate;
            this.PatientCaseStatus = PatientCaseStatus;
            this.PatientCaseDescription = PatientCaseDescription;
            this.PatientCasePatientId = PatientCasePatientId;
            this.PatientCaseDateCreated = PatientCaseDateCreated;
        }

        public PatientCase(DateTime PatientCaseStartDate, string PatientCaseStatus, string PatientCaseDescription, int PatientCasePatientId)
        {
            this.PatientCaseStartDate = PatientCaseStartDate;
            this.PatientCaseStatus = PatientCaseStatus;
            this.PatientCaseDescription = PatientCaseDescription;
            this.PatientCasePatientId = PatientCasePatientId;
        }
    }
}
