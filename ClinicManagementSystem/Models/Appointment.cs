namespace ClinicManagementSystem.Models
{
    using System;

    public class Appointment
    {
        public int AppointmentId { get; set; }

        public int AppointmentPatientCaseId { get; set; }

        public int AppointmentAttendingStaffId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public DateTime AppointmentStartTime { get; set; }

        public DateTime? AppointmentEndTime { get; set; }

        public string AppointmentReasonForVisit { get; set; }

        public Appointment() { }

        // With ID
        public Appointment(
            int AppointmentId,
            int AppointmentPatientCaseId,
            int AppointmentAttendingStaffId,
            DateTime AppointmentDate,
            DateTime AppointmentStartTime,
            DateTime? AppointmentEndTime,
            string AppointmentReasonForVisit
        )
        {
            this.AppointmentId = AppointmentId;
            this.AppointmentPatientCaseId = AppointmentPatientCaseId;
            this.AppointmentAttendingStaffId = AppointmentAttendingStaffId;
            this.AppointmentDate = AppointmentDate;
            this.AppointmentStartTime = AppointmentStartTime;
            this.AppointmentEndTime = AppointmentEndTime;
            this.AppointmentReasonForVisit = AppointmentReasonForVisit;
        }

        // Without ID
        public Appointment(
            int AppointmentPatientCaseId,
            int AppointmentAttendingStaffId,
            DateTime AppointmentDate,
            DateTime AppointmentStartTime,
            DateTime? AppointmentEndTime,
            string AppointmentReasonForVisit
        )
        {
            this.AppointmentPatientCaseId = AppointmentPatientCaseId;
            this.AppointmentAttendingStaffId = AppointmentAttendingStaffId;
            this.AppointmentDate = AppointmentDate;
            this.AppointmentStartTime = AppointmentStartTime;
            this.AppointmentEndTime = AppointmentEndTime;
            this.AppointmentReasonForVisit = AppointmentReasonForVisit;
        }
    }
}
