using ClinicManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ClinicManagementSystem.DBClass
{
    public static class DepartmentDB
    {
        public static void InsertDepartment(Department department)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "INSERT INTO Department (DepartmentId, DepartmentName) VALUES (@DepartmentId, @DepartmentName)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StaffAspNetUsersId", department.DepartmentId);
                    cmd.Parameters.AddWithValue("@StaffClinicRoleId", department.DepartmentName);

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

        public static List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM Department";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Department department = new Department
                                {
                                    DepartmentId = Convert.ToInt32(reader["DepartmentId"]),
                                    DepartmentName = reader["DepartmentName"].ToString()
                                };

                                departments.Add(department);
                            }
                        }
                    }
                    catch (SqlException e)
                    {
                        throw e;
                    }

                    return departments;
                }
            }
        }

        public static Department GetDepartmentById(int id)
        {
            Department department = null;

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM Department WHERE DepartmentId = @DepartmentId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DepartmentId", id);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            department = new Department
                            {
                                DepartmentId = Convert.ToInt32(reader["DepartmentId"]),
                                DepartmentName = reader["DepartmentName"].ToString()
                            };
                        }
                    }
                }
            }

            return department;
        }

        public static void UpdateDepartment(Department newDepartment)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "UPDATE Department SET DepartmentName = @DepartmentName WHERE DepartmentId = @DepartmentId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DepartmentId", newDepartment.DepartmentId);
                    cmd.Parameters.AddWithValue("@DepartmentName", newDepartment.DepartmentName);

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

        public static void DeleteDepartment(int id)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "DELETE FROM Department WHERE DepartmentId = @DepartmentId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DepartmentId", id);

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