using ClinicManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ClinicManagementSystem.DBClass
{
    public static class PatientCaseDB
    {
        public static int InsertPatientCase(PatientCase patientCase)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "INSERT INTO PatientCase (PatientCaseStartDate, PatientCaseEndDate, PatientCaseStatus, PatientCaseDescription, PatientCasePatientId) VALUES (@PatientCaseStartDate, @PatientCaseEndDate, @PatientCaseStatus, @PatientCaseDescription, @PatientCasePatientId)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PatientCaseStartDate", (object)patientCase.PatientCaseStartDate);
                    cmd.Parameters.AddWithValue("@PatientCaseEndDate", (object)patientCase.PatientCaseEndDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PatientCaseStatus", patientCase.PatientCaseStatus);
                    cmd.Parameters.AddWithValue("@PatientCaseDescription", patientCase.PatientCaseDescription);
                    cmd.Parameters.AddWithValue("@PatientCasePatientId", patientCase.PatientCasePatientId);

                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        /*
        public static PatientCase GetPatientCaseByPatient(int patientId)
        {
            PatientCase patientCase = null;

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM Appointment WHERE AppointmentId = @AppointmentId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AppointmentId", id);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            patientCase = new Appointment
                            {
                                AppointmentId = Convert.ToInt32(reader["AppointmentId"]),
                                AppointmentPatientCaseId = Convert.ToInt32(reader["AppointmentPatientCaseId"]),
                                AppointmentAttendingStaffId = Convert.ToInt32(reader["AppointmentAttendingStaffId"]),
                                AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                                AppointmentStartTime = Convert.ToDateTime(reader["AppointmentStartTime"]),
                                AppointmentEndTime = reader["AppointmentEndTime"] as DateTime?,
                                AppointmentType = reader["AppointmentType"].ToString()
                            };
                        }
                    }
                }
            }

            return patientCase;
        }
        */
        public static List<PatientCase> GetPatientCasesByPatientId(int patientId)
        {
            List<PatientCase> patientCases = new List<PatientCase>();

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM PatientCase WHERE PatientCasePatientId = @PatientCasePatientId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PatientCasePatientId", patientId);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PatientCase patientCase = new PatientCase(Convert.ToInt32(reader["PatientCaseId"]), Convert.ToDateTime(reader["PatientCaseStartDate"]), reader["PatientCaseEndDate"] as DateTime?, reader["PatientCaseStatus"].ToString(), reader["PatientCaseDescription"].ToString(), patientId, Convert.ToDateTime(reader["PatientCaseDateCreated"]));

                            patientCases.Add(patientCase);
                        }
                    }
                }
            }

            return patientCases;
        }

        public static void UpdateAppointment(Appointment newAppointment)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "UPDATE Appointment SET AppointmentPatientCaseId = @AppointmentPatientCaseId, AppointmentAttendingStaffId = @AppointmentAttendingStaffId, AppointmentDate = @AppointmentDate, AppointmentStartTime = @AppointmentStartTime, AppointmentEndTime = @AppointmentEndTime, AppointmentReasonForVisit = @AppointmentReasonForVisit WHERE AppointmentId = @AppointmentId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AppointmentId", newAppointment.AppointmentId);
                    cmd.Parameters.AddWithValue("@AppointmentPatientCaseId", newAppointment.AppointmentPatientCaseId);
                    cmd.Parameters.AddWithValue("@AppointmentAttendingStaffId", newAppointment.AppointmentAttendingStaffId);
                    cmd.Parameters.AddWithValue("@AppointmentDate", newAppointment.AppointmentDate);
                    cmd.Parameters.AddWithValue("@AppointmentStartTime", newAppointment.AppointmentStartTime);
                    cmd.Parameters.AddWithValue("@AppointmentEndTime", (object)newAppointment.AppointmentEndTime ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AppointmentType", newAppointment.AppointmentType);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteAppointment(int id)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "DELETE FROM Appointment WHERE AppointmentId = @AppointmentId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AppointmentId", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
