using ClinicManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ClinicManagementSystem.DBClass
{
    public static class ClinicRoleDB
    {
        public static void InsertClinicRole(ClinicRole clinicRole)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "INSERT INTO ClinicRole (ClinicRoleId, ClinicRoleName) VALUES (@ClinicRoleId, @ClinicRoleName)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClinicRoleId", clinicRole.ClinicRoleId);
                    cmd.Parameters.AddWithValue("@ClinicRoleName", clinicRole.ClinicRoleName);

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

        public static List<ClinicRole> GetClinicRoles()
        {
            List<ClinicRole> clinicRoles = new List<ClinicRole>();

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM ClinicRole";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClinicRole clinicRole = new ClinicRole
                                {
                                    ClinicRoleId = Convert.ToInt32(reader["ClinicRoleId"]),
                                    ClinicRoleName = reader["ClinicRoleName"].ToString()
                                };

                                clinicRoles.Add(clinicRole);
                            }
                        }
                    }
                    catch (SqlException e)
                    {
                        throw e;
                    }

                    return clinicRoles;
                }
            }
        }

        public static ClinicRole GetClinicRoleById(int id)
        {
            ClinicRole clinicRole = null;

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "SELECT * FROM ClinicRole WHERE ClinicRoleId = @ClinicRoleId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClinicRoleId", id);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            clinicRole = new ClinicRole
                            {
                                ClinicRoleId = Convert.ToInt32(reader["ClinicRoleId"]),
                                ClinicRoleName = reader["ClinicRoleName"].ToString()
                            };
                        }
                    }
                }
            }

            return clinicRole;
        }

        public static void UpdateClinicRole(ClinicRole newClinicRole)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                string query = "UPDATE ClinicRole SET ClinicRoleName = @ClinicRoleName WHERE ClinicRoleId = @ClinicRoleId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClinicRoleId", newClinicRole.ClinicRoleId);
                    cmd.Parameters.AddWithValue("@ClinicRoleName", newClinicRole.ClinicRoleName);

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
                string query = "DELETE FROM ClinicRole WHERE ClinicRoleId = @ClinicRoleId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClinicRoleId", id);

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