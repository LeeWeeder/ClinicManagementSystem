using ClinicManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ClinicManagementSystem.DBClass
{
    public static class AppointmentDB
    {
        public static bool CheckAppointmentAvailability(Appointment appointment)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string checkQuery = "SELECT COUNT(*) as CountWithinRange FROM Appointment WHERE AppointmentStartTime BETWEEN CAST(DATEADD(minute, -20, @StartTime) AS time) AND CAST(DATEADD(minute, 20, @StartTime) AS time) AND AppointmentDate != @AppointmentDate AND AppointmentAttendingStaffId != @AppointmentAttendingStaffId";

                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@StartTime", appointment.AppointmentStartTime);
                    checkCmd.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
                    checkCmd.Parameters.AddWithValue("@AppointmentAttendingStaffId", appointment.AppointmentAttendingStaffId);

                    conn.Open();
                    int existingAppointments = (int)checkCmd.ExecuteScalar();

                    if (existingAppointments > 0)
                    {
                        return false;
                    }

                    return true;
                }
            }
        }

        public static void InsertAppointment(Appointment appointment)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "INSERT INTO Appointment (AppointmentId, AppointmentPatientCaseId, AppointmentAttendingStaffId, AppointmentDate, AppointmentStartTime, AppointmentEndTime, AppointmentReasonForVisit) VALUES (@AppointmentId, @AppointmentPatientCaseId, @AppointmentAttendingStaffId, @AppointmentDate, @AppointmentStartTime, @AppointmentEndTime, @AppointmentReasonForVisit)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AppointmentId", appointment.AppointmentId);
                    cmd.Parameters.AddWithValue("@AppointmentPatientCaseId", appointment.AppointmentPatientCaseId);
                    cmd.Parameters.AddWithValue("@AppointmentAttendingStaffId", appointment.AppointmentAttendingStaffId);
                    cmd.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
                    cmd.Parameters.AddWithValue("@AppointmentStartTime", appointment.AppointmentStartTime);
                    cmd.Parameters.AddWithValue("@AppointmentEndTime", (object)appointment.AppointmentEndTime ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AppointmentReasonForVisit", appointment.AppointmentReasonForVisit);

                    conn.Open();
                    cmd.ExecuteNonQuery();
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
                                AppointmentReasonForVisit = reader["AppointmentReasonForVisit"].ToString()
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
                string query = "SELECT * FROM Appointment";

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
                                AppointmentPatientCaseId = Convert.ToInt32(reader["AppointmentPatientCaseId"]),
                                AppointmentAttendingStaffId = Convert.ToInt32(reader["AppointmentAttendingStaffId"]),
                                AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                                AppointmentStartTime = Convert.ToDateTime(reader["AppointmentStartTime"]),
                                AppointmentEndTime = reader["AppointmentEndTime"] as DateTime?,
                                AppointmentReasonForVisit = reader["AppointmentReasonForVisit"].ToString()
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
                    cmd.Parameters.AddWithValue("@AppointmentReasonForVisit", newAppointment.AppointmentReasonForVisit);

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
