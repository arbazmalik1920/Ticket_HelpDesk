using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Ticket.Models;

namespace Ticket.DAL
{
    public class SubCategoryDAL
    {
        private readonly string _connectionString;

        public SubCategoryDAL()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["YourDbConnection"].ConnectionString;
        }

        // Get all SubCategories
        public List<SubCategory> GetAllSubCategories()
        {
            var subCategories = new List<SubCategory>();

            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand("GetAllSubCategories", con)) // Ensure this stored procedure exists
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                subCategories.Add(new SubCategory
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["SubCategoryName"].ToString(),                                  
                                    CategoryName = reader["CategoryName"].ToString(),
                                    DepartmentName = reader["DepartmentName"].ToString(),
                                    Status = reader["Status"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching subcategories", ex);
            }

            return subCategories;
        }

        // Get SubCategory by ID
        public SubCategory GetSubCategoryById(int id)
        {
            SubCategory subCategory = null;

            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand("GetSubCategoryById", con)) 
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);
                        con.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                subCategory = new SubCategory
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["SubCategoryName"].ToString(),                                  
                                    CategoryName = reader["CategoryName"].ToString(),                                   
                                    DepartmentName = reader["DepartmentName"].ToString(),
                                    Status = reader["Status"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching subcategory by ID", ex);
            }

            return subCategory;
        }

        // Create SubCategory
        public bool CreateSubCategory(SubCategory subCategory)
        {
            if (subCategory == null) throw new ArgumentNullException(nameof(subCategory));

            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand("CreateSubCategory", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", subCategory.Name);
                        cmd.Parameters.AddWithValue("@CategoryId", subCategory.CategoryId);
                        cmd.Parameters.AddWithValue("@DepartmentId", subCategory.DepartmentId);
                        cmd.Parameters.AddWithValue("@Status", subCategory.Status);
                        con.Open();
                        return cmd.ExecuteNonQuery() > 0; 
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error inserting subcategory", ex);
            }
        }


        // Update SubCategory
        public bool UpdateSubCategory(SubCategory subCategory)
        {
            if (subCategory == null || subCategory.Id <= 0) throw new ArgumentException("Invalid subcategory data!");

            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand("UpdateSubCategory", con)) 
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", subCategory.Id);
                        cmd.Parameters.AddWithValue("@Name", subCategory.Name);
                        cmd.Parameters.AddWithValue("@CategoryId", subCategory.CategoryId);
                        cmd.Parameters.AddWithValue("@DepartmentId", subCategory.DepartmentId);
                        cmd.Parameters.AddWithValue("@Status", subCategory.Status);
                        con.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating subcategory", ex);
            }
        }

        // Delete SubCategory
        public bool DeleteSubCategory(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid subcategory ID!");

            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand("DeleteSubCategory", con)) 
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);
                        con.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting subcategory", ex);
            }
        }
    }
}
