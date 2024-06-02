using ClinicManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ClinicManagementSystem.DBClass
{
    public static class AppointmentDB
    {
        public static int InsertAppointment(Appointment appointment)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "INSERT INTO Appointment (AppointmentPatientCaseId, AppointmentAttendingStaffId, AppointmentDate, AppointmentStartTime, AppointmentEndTime, AppointmentType) VALUES (@AppointmentPatientCaseId, @AppointmentAttendingStaffId, @AppointmentDate, @AppointmentStartTime, @AppointmentEndTime, @AppointmentType)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AppointmentPatientCaseId", (object)appointment.AppointmentPatientCaseId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AppointmentAttendingStaffId", (object)appointment.AppointmentAttendingStaffId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
                    cmd.Parameters.AddWithValue("@AppointmentStartTime", appointment.AppointmentStartTime);
                    cmd.Parameters.AddWithValue("@AppointmentEndTime", (object)appointment.AppointmentEndTime ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AppointmentType", appointment.AppointmentType);

                    conn.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static Appointment GetAppointmentById(int id)
        {
            Appointment appointment = null;

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
                            appointment = new Appointment
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

            return appointment;
        }

        public static List<Appointment> GetAppointments()
        {
            List<Appointment> appointments = new List<Appointment>();

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "SELECT Appointment.AppointmentId, Appointment.AppointmentPatientCaseId, Appointment.AppointmentAttendingStaffId, Appointment.AppointmentDate, Appointment.AppointmentType, Appointment.AppointmentStatus, CAST(Appointment.AppointmentStartTime AS DATETIME) AS 'AppointmentStartTime', CAST(Appointment.AppointmentEndTime AS DATETIME) AS 'AppointmentEndTime', CONCAT(AspNetUsers.FirstName, ' ', AspNetUsers.LastName) AS 'FullName' FROM Appointment LEFT JOIN Staff ON Appointment.AppointmentAttendingStaffId = Staff.StaffId JOIN AspNetUsers ON Staff.StaffAspNetUsersId = AspNetUsers.Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Appointment appointment = new Appointment
                            {
                                AppointmentId = Convert.ToInt32(reader["AppointmentId"]),
                                AppointmentPatientCaseId = reader["AppointmentPatientCaseId"] as int?,
                                AppointmentAttendingStaffId = reader["AppointmentAttendingStaffId"] as int?,
                                AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                                AppointmentStartTime = Convert.ToDateTime(reader["AppointmentStartTime"]),
                                AppointmentEndTime = reader["AppointmentEndTime"] as DateTime?,
                                AppointmentType = reader["AppointmentType"].ToString(),
                                AppointmentStatus = reader["AppointmentStatus"].ToString(),
                                AppointmentAttendingStaffName = reader["FullName"].ToString()
                            };

                            appointments.Add(appointment);
                        }
                    }
                }
            }

            return appointments;
        }

        public static List<Appointment> GetAppointmentsByPatientId(int patientId)
        {
            List<Appointment> appointments = new List<Appointment>();

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "SELECT Appointment.AppointmentId, Appointment.AppointmentPatientCaseId, Appointment.AppointmentAttendingStaffId, Appointment.AppointmentDate, Appointment.AppointmentType, Appointment.AppointmentStatus, CAST(Appointment.AppointmentStartTime AS DATETIME) AS 'AppointmentStartTime', CAST(Appointment.AppointmentEndTime AS DATETIME) AS 'AppointmentEndTime', CONCAT(AspNetUsers.FirstName, ' ', AspNetUsers.LastName) AS 'FullName' FROM Appointment LEFT JOIN Staff ON Appointment.AppointmentAttendingStaffId = Staff.StaffId JOIN AspNetUsers ON Staff.StaffAspNetUsersId = AspNetUsers.Id JOIN PatientCase ON Appointment.AppointmentPatientCaseId = PatientCase.PatientCaseId WHERE PatientCase.PatientCasePatientId = @PatientId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PatientId", patientId);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Appointment appointment = new Appointment
                            {
                                AppointmentId = Convert.ToInt32(reader["AppointmentId"]),
                                AppointmentPatientCaseId = reader["AppointmentPatientCaseId"] as int?,
                                AppointmentAttendingStaffId = reader["AppointmentAttendingStaffId"] as int?,
                                AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                                AppointmentStartTime = Convert.ToDateTime(reader["AppointmentStartTime"]),
                                AppointmentEndTime = reader["AppointmentEndTime"] as DateTime?,
                                AppointmentType = reader["AppointmentType"].ToString(),
                                AppointmentStatus = reader["AppointmentStatus"].ToString(),
                                AppointmentAttendingStaffName = reader["FullName"].ToString()
                            };

                            appointments.Add(appointment);
                        }
                    }
                }
            }

            return appointments;
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
