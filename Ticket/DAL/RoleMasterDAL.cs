using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Ticket.Models;

namespace Ticket.DAL
{
    public class RoleMasterDAL
    {
        private readonly string _connectionString;

        public RoleMasterDAL()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["YourDbConnection"].ConnectionString;
        }

        // Fetch all roles
        public List<RoleMaster> GetAllRoleMaster()
        {
            var roles = new List<RoleMaster>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetAllRoleMaster", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                roles.Add(new RoleMaster
                                {
                                    RoleId = Convert.ToInt32(reader["RoleId"]),
                                    RoleName = reader["RoleName"].ToString(),
                                    Status = reader["Status"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception("Error fetching all roles", ex);
            }
            return roles;
        }


        // Fetch a role by ID
        public RoleMaster GetRoleMasterById(int id)
        {
            RoleMaster roleMaster = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetRoleMasterById", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleId", id);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            roleMaster = new RoleMaster
                            {
                                RoleId = Convert.ToInt32(reader["RoleId"]),
                                RoleName = reader["RoleName"].ToString(),
                                
                                Status = reader["Status"].ToString()
                            };
                        }
                    }
                }
            }

            return roleMaster;
        }

        // Create a new role
        public bool CreateRoleMaster(RoleMaster roleMaster)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("CreateRoleMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleName", roleMaster.RoleName);
                   
                    cmd.Parameters.AddWithValue("@Status", roleMaster.Status);

                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Update an existing role
        public bool UpdateRoleMaster(RoleMaster roleMaster)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateRoleMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleId", roleMaster.RoleId);
                    cmd.Parameters.AddWithValue("@RoleName", roleMaster.RoleName);
                    
                    cmd.Parameters.AddWithValue("@Status", roleMaster.Status);

                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Delete a role by ID
        public bool DeleteRoleMaster(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteRoleMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleId", id);

                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
