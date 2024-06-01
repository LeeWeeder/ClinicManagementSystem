namespace ClinicManagementSystem.Models
{
    using System;

    public class Patient
    {
        public int PatientId { get; set; }

        public DateTime PatientBirthDate { get; set; }

        public string PatientAspNetUsersId { get; set; }

        public Patient() { }

        public Patient(int PatientId, DateTime PatientBirthDate, string PatientAspNetUserId)
        {
            this.PatientId = PatientId;
            this.PatientBirthDate = PatientBirthDate;
            this.PatientAspNetUsersId = PatientAspNetUserId;
        }

        public Patient(DateTime PatientBirthDate, string PatientAspNetUserId)
        {
            this.PatientBirthDate = PatientBirthDate;
            this.PatientAspNetUsersId = PatientAspNetUserId;
        }

        public Patient(DateTime PatientBirthDate)
        {
            this.PatientBirthDate = PatientBirthDate;
        }
    }
}
