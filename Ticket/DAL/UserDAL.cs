using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Ticket.Models;

namespace Ticket.DAL
{
    public class UserDAL
    {
        private readonly string _connectionString;

        public UserDAL()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["YourDbConnection"].ConnectionString;
        }

        // Retrieve All Users
        public List<User> GetAllUsers()
        {
            var users = new List<User>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetAllUsers", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                users.Add(new User
                                {
                                    UserId = Convert.ToInt32(dr["UserId"]),
                                    Username = dr["Username"].ToString(),
                                    Name = dr["Name"].ToString(),
                                    DepartmentName = dr["DepartmentName"].ToString(),
                                    MobileNo = dr["MobileNo"].ToString(),
                                    Email = dr["Email"].ToString(),
                                    Role = dr["Role"].ToString(),
                                    Status = dr["Status"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving users", ex);
            }

            return users;
        }


        // Retrieve User by ID
        public User GetUserById(int userId)
        {
            User user = null;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetUserById", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                user = new User
                                {
                                    UserId = Convert.ToInt32(dr["UserId"]),
                                    Username = dr["Username"].ToString(),
                                    Name = dr["Name"].ToString(),
                                    DepartmentName = dr["DepartmentName"].ToString(),
                                    MobileNo = dr["MobileNo"].ToString(),
                                    Email = dr["Email"].ToString(),
                                    Role = dr["Role"].ToString(),
                                    Status = dr["Status"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving user with ID {userId}", ex);
            }

            return user;
        }


        // Create a New User
        public bool CreateUser(User user)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("CreateUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        cmd.Parameters.AddWithValue("@Name", user.Name);
                        cmd.Parameters.AddWithValue("@DepartmentId", user.DepartmentId);
                        cmd.Parameters.AddWithValue("@MobileNo", user.MobileNo);
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@Role", user.Role);
                        cmd.Parameters.AddWithValue("@Status", user.Status);

                        con.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating user", ex);
            }
        }

        // Create or Update User
        public bool UpdateUser(User user)
        {
            if (user == null || user.UserId <= 0)
                throw new ArgumentException("Invalid user data!");

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UserId", user.UserId);
                        cmd.Parameters.AddWithValue("@Name", user.Name);
                        cmd.Parameters.AddWithValue("@DepartmentId", user.DepartmentId);
                        cmd.Parameters.AddWithValue("@MobileNo", user.MobileNo);
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@Role", user.Role);
                        cmd.Parameters.AddWithValue("@Status", user.Status);

                        con.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating user with ID {user.UserId}", ex);
            }
        }


        // Delete User
        public bool DeleteUser(int userId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("DeleteUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        con.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting user with ID {userId}", ex);
            }
        }


        //Change Password
        //public bool UpdatePassword(int userId, string newPassword)
        //{
        //    try
        //    {
        //        using (var con = new SqlConnection(_connectionString))
        //        {
        //            using (var cmd = new SqlCommand("UpdatePassword", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@UserId", userId);
        //                cmd.Parameters.AddWithValue("@Password", newPassword);

        //                con.Open();
        //                return cmd.ExecuteNonQuery() > 0;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error updating user", ex);
        //    }
        //}

        //public bool ChangePassword(int userId, string currentPassword, string newPassword)
        //{
        //    try
        //    {
        //        using (var con = new SqlConnection(_connectionString))
        //        {
        //            using (var cmd = new SqlCommand("UpdatePassword", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@UserId", userId);
        //                cmd.Parameters.AddWithValue("@CurrentPassword", currentPassword);
        //                cmd.Parameters.AddWithValue("@NewPassword", newPassword);

        //                con.Open();
        //                return cmd.ExecuteNonQuery() > 0;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error updating user", ex);
        //    }
        //}




    }
}
