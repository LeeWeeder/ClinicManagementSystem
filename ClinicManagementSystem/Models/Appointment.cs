namespace ClinicManagementSystem.Models
{
    using System;

    public class Appointment
    {
        public int AppointmentId { get; set; }

        public int? AppointmentPatientCaseId { get; set; }

        public int? AppointmentAttendingStaffId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public DateTime AppointmentStartTime { get; set; }

        public DateTime? AppointmentEndTime { get; set; }

        public string AppointmentType { get; set; }

        public Appointment() { }

        // With ID
        public Appointment(
            int AppointmentId,
            int AppointmentPatientCaseId,
            int AppointmentAttendingStaffId,
            DateTime AppointmentDate,
            DateTime AppointmentStartTime,
            DateTime? AppointmentEndTime,
            string AppointmentType
        )
        {
            this.AppointmentId = AppointmentId;
            this.AppointmentPatientCaseId = AppointmentPatientCaseId;
            this.AppointmentAttendingStaffId = AppointmentAttendingStaffId;
            this.AppointmentDate = AppointmentDate;
            this.AppointmentStartTime = AppointmentStartTime;
            this.AppointmentEndTime = AppointmentEndTime;
            this.AppointmentType = AppointmentType;
        }

        // Without ID
        public Appointment(
            int AppointmentPatientCaseId,
            int AppointmentAttendingStaffId,
            DateTime AppointmentDate,
            DateTime AppointmentStartTime,
            DateTime? AppointmentEndTime,
            string AppointmentType
        )
        {
            this.AppointmentPatientCaseId = AppointmentPatientCaseId;
            this.AppointmentAttendingStaffId = AppointmentAttendingStaffId;
            this.AppointmentDate = AppointmentDate;
            this.AppointmentStartTime = AppointmentStartTime;
            this.AppointmentEndTime = AppointmentEndTime;
            this.AppointmentType = AppointmentType;
        }

        // For checking
        public Appointment(
            int? AppointmentAttendingStaffId,
            DateTime AppointmentDate,
            DateTime AppointmentStartTime
        )
        {
            this.AppointmentAttendingStaffId = AppointmentAttendingStaffId;
            this.AppointmentDate = AppointmentDate;
            this.AppointmentStartTime = AppointmentStartTime;
        }
    }
}
