using ClinicManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ClinicManagementSystem.DBClass
{
    public static class PatientDB
    {
        public static int InsertPatient(Models.Patient patient)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                int id;
                string query = "INSERT INTO Patient (PatientAspNetUsersId, PatientBirthDate) VALUES (@PatientAspNetUsersId, @PatientBirthDate)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PatientBirthDate", patient.PatientBirthDate);
                    cmd.Parameters.AddWithValue("@PatientAspNetUsersId", (object)patient.PatientAspNetUsersId ?? DBNull.Value);

                    conn.Open();
                    try
                    {
                        id = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch (SqlException e)
                    {
                        throw e;
                    }
                }
                return id;
            }
        }

        public static List<Models.Patient> GetPatients()
        {
            List<Models.Patient> patients = new List<Models.Patient>();

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM Patient";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Models.Patient patient = new Models.Patient
                                {
                                    PatientId = Convert.ToInt32(reader["PatientId"]),
                                    PatientAspNetUsersId = reader["PatientAspNetUsersId"].ToString(),
                                    PatientBirthDate = Convert.ToDateTime(reader["PatientBirthDate"])
                                };

                                patients.Add(patient);
                            }
                        }
                    }
                    catch (SqlException e)
                    {
                        throw e;
                    }

                    return patients;
                }
            }
        }

        public static List<PhysicianWithFullNameAndDepartment> GetPhysiciansWithFullNameAndDepartment()
        {
            List<PhysicianWithFullNameAndDepartment> physicians = new List<PhysicianWithFullNameAndDepartment>();

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "SELECT Staff.StaffId, CONCAT(AspNetUsers.FirstName, ' ', AspNetUsers.LastName) AS FullName, Department.DepartmentName FROM Staff JOIN AspNetUsers ON Staff.StaffAspNetUsersId = AspNetUsers.Id JOIN Department ON Staff.StaffDepartmentId = Department.DepartmentId JOIN ClinicRole ON Staff.StaffClinicRoleId = ClinicRole.ClinicRoleId WHERE ClinicRole.ClinicRoleName = 'Physician'";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PhysicianWithFullNameAndDepartment physician = new PhysicianWithFullNameAndDepartment(Convert.ToInt32(reader["StaffId"]), reader["FullName"].ToString(), reader["DepartmentName"].ToString());
                                physicians.Add(physician);
                            }
                        }
                    }
                    catch (SqlException e)
                    {
                        throw e;
                    }

                    return physicians;
                }
            }
        }

        public static int? GetPatientByAspNetUsersId(string aspNetUsersId)
        {
            int? patientId = null;

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM Patient WHERE PatientAspNetUsersId = @PatientAspNetUsersId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PatientAspNetUsersId", aspNetUsersId);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            patientId = reader["PatientId"] as int?;
                        }
                    }
                }
            }

            return patientId;
        }

        /*
        public static void UpdateStaff(Staff newStaff)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "UPDATE Staff SET StaffAspNetUsersId = @StaffAspNetUsersId, StaffClinicRoleId = @StaffClinicRoleId, StaffDepartmentId = @StaffDepartmentId WHERE StaffId = @StaffId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StaffId", newStaff.StaffId);
                    cmd.Parameters.AddWithValue("@StaffAspNetUsersId", newStaff.StaffId);
                    cmd.Parameters.AddWithValue("@StaffClinicRoleId", newStaff.StaffClinicRoleId);
                    cmd.Parameters.AddWithValue("@StaffDepartmentId", (object)newStaff.StaffDepartmentId ?? DBNull.Value);

                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        throw e;
                    }
                }
            }
        }

        public static void DeleteStaff(int id)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "DELETE FROM Staff WHERE StaffId = @StaffId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StaffId", id);

                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        throw e;
                    }
                }
            }
        }

        public static List<Staff> GetStaffByClinicRoleName(string clinicRoleName)
        {

            List<Staff> staffList = new List<Staff>();

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "SELECT Staff.*, ClinicRole.ClinicRoleName FROM Staff JOIN ClinicRole ON Staff.StaffClinicRoleId = ClinicRole.ClinicRoleId WHERE ClinicRoleName = @StaffClinicRoleName AND Staff.StaffIsActive = 1";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StaffClinicRoleName", clinicRoleName);
                    conn.Open();
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Staff staff = new Staff
                                {
                                    StaffId = Convert.ToInt32(reader["StaffId"].ToString()),
                                    StaffClinicRoleId = Convert.ToInt32(reader["StaffClinicRoleId"]),
                                    StaffDepartmentId = reader["StaffDepartmentId"] as int?
                                };

                                staffList.Add(staff);
                            }
                        }
                    }
                    catch (SqlException e)
                    {
                        throw e;
                    }

                    return staffList;
                }
            }
        }
        */
    }
}