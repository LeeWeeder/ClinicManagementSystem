using ClinicManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ClinicManagementSystem.DBClass
{
    public static class StaffDB
    {
        public static void InsertStaff(Staff staff)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "INSERT INTO Staff (StaffAspNetUsersId, StaffClinicRoleId, StaffDepartmentId) VALUES (@StaffAspNetUsersId, @StaffClinicRoleId, @StaffDepartmentId)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StaffAspNetUsersId", staff.StaffAspNetUsersId);
                    cmd.Parameters.AddWithValue("@StaffClinicRoleId", staff.StaffClinicRoleId);
                    cmd.Parameters.AddWithValue("@StaffDepartmentId", (object)staff.StaffDepartmentId ?? DBNull.Value);

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

        public static List<StaffWithDetails> GetStaff()
        {
            List<StaffWithDetails> staffs = new List<StaffWithDetails>();

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "SELECT Staff.StaffId AS 'ID', Staff.StaffAspNetUsersId AS 'AspNetUsersId', Staff.StaffIsActive AS 'Active', AspNetUsers.LastName AS 'LastName', AspNetUsers.FirstName AS 'FirstName', AspNetUsers.MiddleName AS 'MiddleName', AspNetUsers.SexAtBirth AS 'SexAtBirth', AspNetUsers.Email AS 'Email', AspNetUsers.PhoneNumber AS 'ContactNumber', ClinicRole.ClinicRoleName AS 'ClinicRole', Department.DepartmentName AS 'Department' FROM Staff JOIN AspNetUsers ON AspNetUsers.Id = Staff.StaffAspNetUsersId JOIN ClinicRole ON ClinicRole.ClinicRoleId = Staff.StaffClinicRoleId LEFT JOIN Department ON Department.DepartmentId = Staff.StaffDepartmentId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StaffWithDetails staff = new StaffWithDetails(
                                    Convert.ToInt32(reader["ID"]),
                                    Convert.ToBoolean(reader["Active"]),
                                    reader["SexAtBirth"].ToString(),
                                    reader["ClinicRole"].ToString(),
                                    reader["Department"].ToString(),
                                    reader["Email"].ToString(),
                                    reader["ContactNumber"].ToString(),
                                    reader["FirstName"].ToString(),
                                    reader["MiddleName"].ToString(),
                                    reader["LastName"].ToString(),
                                    reader["AspNetUsersId"].ToString()
                                );

                                staffs.Add(staff);
                            }
                        }
                    }
                    catch (SqlException e)
                    {
                        throw e;
                    }

                    return staffs;
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

        public static StaffWithDetails GetStaffById(int id)
        {
            StaffWithDetails staff = null;

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "SELECT Staff.StaffIsActive AS 'Active', Staff.StaffAspNetUsersId AS 'AspNetUsersId', AspNetUsers.LastName AS 'LastName', AspNetUsers.FirstName AS 'FirstName', AspNetUsers.MiddleName AS MiddleName, AspNetUsers.SexAtBirth AS 'SexAtBirth', AspNetUsers.Email AS 'Email', AspNetUsers.PhoneNumber AS 'ContactNumber', ClinicRole.ClinicRoleId, ClinicRole.ClinicRoleName AS 'ClinicRole', Department.DepartmentName AS 'Department' FROM Staff JOIN AspNetUsers ON AspNetUsers.Id = Staff.StaffAspNetUsersId JOIN ClinicRole ON ClinicRole.ClinicRoleId = Staff.StaffClinicRoleId LEFT JOIN Department ON Department.DepartmentId = Staff.StaffDepartmentId WHERE Staff.StaffId = @StaffId";
                ;

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StaffId", id);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            staff = new StaffWithDetails
                            {
                                Id = id,
                                FirstName = reader["FirstName"].ToString(),
                                MiddleName = reader["MiddleName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                ClinicRole = reader["ClinicRole"].ToString(),
                                Department = reader["Department"].ToString(),
                                IsActive = reader.GetBoolean(reader.GetOrdinal("Active")),
                                SexAtBirth = reader["SexAtBirth"].ToString(),
                                Email = reader["Email"].ToString(),
                                ContactNumber = reader["ContactNumber"].ToString(),
                                AspNetUsersId = reader["AspNetUsersId"].ToString()
                            };
                        }
                    }
                }
            }

            return staff;
        }

        public static void UpdateStaff(Staff newStaff)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "UPDATE Staff SET StaffClinicRoleId = @StaffClinicRoleId, StaffDepartmentId = @StaffDepartmentId, StaffIsActive = @StaffIsActive WHERE StaffId = @StaffId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StaffId", newStaff.StaffId);
                    cmd.Parameters.AddWithValue("@StaffClinicRoleId", newStaff.StaffClinicRoleId);
                    cmd.Parameters.AddWithValue("@StaffDepartmentId", (object)newStaff.StaffDepartmentId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@StaffIsActive", newStaff.StaffIsActive);

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

        public static List<Staff> GetStaffByClinicRoleName(string clinicRoleName, bool active)
        {

            List<Staff> staffList = new List<Staff>();

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "SELECT Staff.*, ClinicRole.ClinicRoleName FROM Staff JOIN ClinicRole ON Staff.StaffClinicRoleId = ClinicRole.ClinicRoleId WHERE ClinicRole.ClinicRoleName = @StaffClinicRoleName AND StaffIsActive = @StaffIsActive";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StaffClinicRoleName", clinicRoleName);
                    cmd.Parameters.AddWithValue("@StaffIsActive", active);
                    conn.Open();
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Staff staff = new Staff
                                {
                                    StaffId = Convert.ToInt32(reader["StaffId"]),
                                    StaffClinicRoleId = Convert.ToInt32(reader["StaffClinicRoleId"]),
                                    StaffDepartmentId = reader["StaffDepartmentId"] as int?,
                                    StaffIsActive = reader.GetBoolean(reader.GetOrdinal("StaffIsActive")),
                                    StaffAspNetUsersId = reader["StaffAspNetUsersId"].ToString()
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
    }
}