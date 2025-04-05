using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Ticket.Models;

public class CategoryDAL
{
    private readonly string _connectionString;

    public CategoryDAL()
    {
        _connectionString = ConfigurationManager.ConnectionStrings["YourDbConnection"].ConnectionString;
    }

    public List<Category> GetAllCategories()
    {
        var categories = new List<Category>();

        try
        {
            using (var con = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("GetAllCategories", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(new Category
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["CategoryName"].ToString(),
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
            
            throw new Exception("Error fetching categories", ex);
        }

        return categories;
    }

    public Category GetCategoryById(int id)
    {
        Category category = null;

        using (var con = new SqlConnection(_connectionString))
        {
            var cmd = new SqlCommand("GetCategoryById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    category = new Category
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["CategoryName"].ToString(),
                        DepartmentName = reader["DepartmentName"].ToString(),
                        Status = reader["Status"].ToString()
                    };
                }
            }
        }

        return category;
    }





    public bool CreateCategory(Category category)
    {
        if (category == null) throw new ArgumentNullException(nameof(category));

        try
        {
            using (var con = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("CreateCategory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", category.Name);
                    cmd.Parameters.AddWithValue("@DepartmentId", category.DepartmentId);
                    cmd.Parameters.AddWithValue("@Status", category.Status);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();  
                    return result > 0;
                }
            }
        }
        catch (Exception ex)
        {
            
            throw new Exception("Error creating category", ex);
        }
    }


    public bool UpdateCategory(Category category)
    {
        if (category == null || category.Id <= 0) throw new ArgumentException("Invalid category data!");

        try
        {
            using (var con = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("UpdateCategory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", category.Id);
                    cmd.Parameters.AddWithValue("@Name", category.Name);
                    cmd.Parameters.AddWithValue("@DepartmentId", category.DepartmentId);
                    cmd.Parameters.AddWithValue("@Status", category.Status);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error updating category", ex);
        }
    }





    // Delete a category
    public bool DeleteCategory(int id)
    {
        if (id <= 0) throw new ArgumentException("Invalid category ID!");

        try
        {
            using (var con = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("DeleteCategory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    var result = cmd.ExecuteNonQuery(); 

                    return result > 0; 
                }
            }
        }
        catch (Exception ex)
        {
            
            throw new Exception("Error deleting category", ex);
        }
    }
}
