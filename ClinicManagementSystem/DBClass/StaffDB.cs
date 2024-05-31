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

        public static List<Staff> GetStaffs()
        {
            List<Staff> staffs = new List<Staff>();

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM Staff";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
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
                                    StaffAspNetUsersId = reader["StaffAspNetUsersId"].ToString(),
                                    StaffClinicRoleId = Convert.ToInt32(reader["StaffClinicRoleId"]),
                                    StaffDepartmentId = reader["StaffDepartmentId"] as int?
                                };

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

        public static Staff GetStaffById(int id)
        {
            Staff staff = null;

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM Staff WHERE StaffId = @StaffId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StaffId", id);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            staff = new Staff
                            {
                                StaffId = Convert.ToInt32(reader["StaffId"]),
                                StaffAspNetUsersId = reader["StaffAspNetUsersId"].ToString(),
                                StaffClinicRoleId = Convert.ToInt32(reader["StaffClinicRoleId"]),
                                StaffDepartmentId = reader["StaffDepartmentId"] as int?
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
                string query = "UPDATE Staff SET StaffAspNetUsersId = @StaffAspNetUsersId, StaffClinicRoleId = @StaffClinicRoleId, StaffDepartmentId = @StaffDepartmentId WHERE StaffId = @StaffId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StaffId", newStaff.StaffId);
                    cmd.Parameters.AddWithValue("@StaffAspNetUsersId", newStaff.StaffAspNetUsersId);
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
    }
}