using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Ticket.Models;

namespace Ticket.DAL
{
    public class DepartmentDAL
    {
        private readonly string _connectionString;

        public DepartmentDAL()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["YourDbConnection"].ConnectionString;
        }

        public List<Department> GetAllDepartments()
        {
            var departments = new List<Department>();
            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand("GetAllDepartments", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                departments.Add(new Department
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    ProductId = Convert.ToInt32(reader["ProductId"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception("Error fetching all departments", ex);
            }
            return departments;
        }

        public int CreateDepartment(Department department)
        {
            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand("CreateDepartment", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", department.Name);
                        cmd.Parameters.AddWithValue("@Description", department.Description);
                        cmd.Parameters.AddWithValue("@ProductId", department.ProductId);

                        con.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception("Error creating department", ex);
            }
        }


        public Department GetDepartmentById(int id)
        {
            try
            {
                Department department = null;
                using (var con = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand("GetDepartmentById", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);

                        con.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                department = new Department
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    ProductId = Convert.ToInt32(reader["ProductId"])
                                };
                            }
                        }
                    }
                }
                return department;
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception("Error fetching department by ID", ex);
            }
        }

        public int UpdateDepartment(Department department)
        {
            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand("UpdateDepartment", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", department.Id);
                        cmd.Parameters.AddWithValue("@Name", department.Name);
                        cmd.Parameters.AddWithValue("@Description", department.Description);
                        cmd.Parameters.AddWithValue("@ProductId", department.ProductId);

                        con.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception("Error updating department", ex);
            }
        }

        public int DeleteDepartment(int id)
        {
            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand("DeleteDepartment", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);

                        con.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception
                throw new Exception("Error deleting department", ex);
            }
        }
    }
}
