using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Ticket.Models;

namespace Ticket.DAL
{
    public class TrackTicketDAL
    {
        private readonly string _connectionString;

        public TrackTicketDAL()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["YourDbConnection"].ConnectionString;
        }



        // Get All Tickets

        //public List<TrackTicket> GetAllTickets()
        //{
        //    var tickets = new List<TrackTicket>();

        //    using (SqlConnection conn = new SqlConnection(_connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("GetAllTickets", conn)
        //        {
        //            CommandType = CommandType.StoredProcedure
        //        };

        //        conn.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            tickets.Add(new TrackTicket
        //            {
        //                Id = Convert.ToInt32(reader["Id"]),
        //                TicketNo = reader["TicketNo"].ToString(),
        //                DepartmentName = reader["DepartmentName"].ToString(),
        //                CategoryName = reader["CategoryName"].ToString(),
        //                SubCategoryName = reader["SubCategoryName"].ToString(),
        //                Date = Convert.ToDateTime(reader["Date"]),
        //                Status = reader["Status"].ToString()
        //            });
        //        }
        //    }

        //    return tickets;
        //}



        public List<TrackTicket> GetAllTickets()
        {
            var tickets = new List<TrackTicket>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetAllTickets", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tickets.Add(new TrackTicket
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        TicketNo = reader["TicketNo"].ToString(),
                        DepartmentName = reader["DepartmentName"].ToString(),
                        CategoryName = reader["CategoryName"].ToString(),
                        SubCategoryName = reader["SubCategoryName"].ToString(),
                        Date = Convert.ToDateTime(reader["Date"]),
                        ClosingDate = reader["ClosingDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ClosingDate"]),
                        TAT = reader["TAT"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["TAT"]),
                        Status = reader["Status"].ToString()
                    });
                }
            }

            return tickets;
        }

        // Get Ticket By ID
        public TrackTicket GetTicketById(int id)
        {
            TrackTicket ticket = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetTicketById", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    ticket = new TrackTicket
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        TicketNo = reader["TicketNo"].ToString(),
                        DepartmentId = Convert.ToInt32(reader["DepartmentId"]),
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        SubCategoryId = Convert.ToInt32(reader["SubCategoryId"]),
                        Date = Convert.ToDateTime(reader["Date"]),
                        Status = reader["Status"].ToString()
                    };
                }
            }

            return ticket;
        }

        // Create Ticket
        public bool CreateTicket(TrackTicket ticket)
        {
            if (string.IsNullOrEmpty(ticket.TicketNo))
            {
                throw new ArgumentException("TicketNo cannot be null or empty");
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("CreateTicket", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@TicketNo", ticket.TicketNo);
                cmd.Parameters.AddWithValue("@DepartmentId", ticket.DepartmentId);
                cmd.Parameters.AddWithValue("@CategoryId", ticket.CategoryId);
                cmd.Parameters.AddWithValue("@SubCategoryId", ticket.SubCategoryId);
                cmd.Parameters.AddWithValue("@Date", ticket.Date ?? DateTime.Now); // Handle null
                cmd.Parameters.AddWithValue("@Status", ticket.Status);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }


        // Update Ticket
        public bool UpdateTicket(TrackTicket ticket)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateTicket", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", ticket.Id);
                cmd.Parameters.AddWithValue("@TicketNo", ticket.TicketNo);
                cmd.Parameters.AddWithValue("@DepartmentId", ticket.DepartmentId);
                cmd.Parameters.AddWithValue("@CategoryId", ticket.CategoryId);
                cmd.Parameters.AddWithValue("@SubCategoryId", ticket.SubCategoryId);
                //cmd.Parameters.AddWithValue("@Date", ticket.Date ?? DateTime.Now); // Handle null
                cmd.Parameters.AddWithValue("@Status", ticket.Status);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }




        // Delete Ticket
        public bool DeleteTicket(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteTicket", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}











































//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using Ticket.Models;

//namespace Ticket.DAL
//{
//    public class TrackTicketDAL
//    {
//        private readonly string _connectionString;

//        public TrackTicketDAL()
//        {
//            _connectionString = ConfigurationManager.ConnectionStrings["YourDbConnection"].ConnectionString;
//        }



//        // Get All Tickets

//        //public List<TrackTicket> GetAllTickets()
//        //{
//        //    var tickets = new List<TrackTicket>();

//        //    using (SqlConnection conn = new SqlConnection(_connectionString))
//        //    {
//        //        SqlCommand cmd = new SqlCommand("GetAllTickets", conn)
//        //        {
//        //            CommandType = CommandType.StoredProcedure
//        //        };

//        //        conn.Open();
//        //        SqlDataReader reader = cmd.ExecuteReader();

//        //        while (reader.Read())
//        //        {
//        //            tickets.Add(new TrackTicket
//        //            {
//        //                Id = Convert.ToInt32(reader["Id"]),
//        //                TicketNo = reader["TicketNo"].ToString(),
//        //                DepartmentName = reader["DepartmentName"].ToString(),
//        //                CategoryName = reader["CategoryName"].ToString(),
//        //                SubCategoryName = reader["SubCategoryName"].ToString(),
//        //                Date = Convert.ToDateTime(reader["Date"]),
//        //                Status = reader["Status"].ToString()
//        //            });
//        //        }
//        //    }

//        //    return tickets;
//        //}



//        public List<TrackTicket> GetAllTickets()
//        {
//            var tickets = new List<TrackTicket>();

//            try
//            {
//                using (SqlConnection conn = new SqlConnection(_connectionString))
//                {
//                    SqlCommand cmd = new SqlCommand("GetAllTickets", conn)
//                    {
//                        CommandType = CommandType.StoredProcedure
//                    };

//                    conn.Open();
//                    SqlDataReader reader = cmd.ExecuteReader();

//                    while (reader.Read())
//                    {
//                        tickets.Add(new TrackTicket
//                        {
//                            Id = Convert.ToInt32(reader["Id"]),
//                            TicketNo = reader["TicketNo"].ToString(),
//                            DepartmentName = reader["DepartmentName"].ToString(),
//                            CategoryName = reader["CategoryName"].ToString(),
//                            SubCategoryName = reader["SubCategoryName"].ToString(),
//                            Date = Convert.ToDateTime(reader["Date"]),
//                            ClosingDate = reader["ClosingDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ClosingDate"]),
//                            TAT = reader["TAT"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["TAT"]),
//                            //TAT = reader["TATHours"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["TATHours"]),
//                            Status = reader["Status"].ToString()
//                        });
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw new Exception("Error retrieving tickets", ex);
//            }

//            return tickets;
//        }

//        // Get Ticket By ID
//        public TrackTicket GetTicketById(int id)
//        {
//            TrackTicket ticket = null;

//            try
//            {
//                using (SqlConnection conn = new SqlConnection(_connectionString))
//                {
//                    SqlCommand cmd = new SqlCommand("GetTicketById", conn)
//                    {
//                        CommandType = CommandType.StoredProcedure
//                    };
//                    cmd.Parameters.AddWithValue("@Id", id);

//                    conn.Open();
//                    SqlDataReader reader = cmd.ExecuteReader();

//                    if (reader.Read())
//                    {
//                        ticket = new TrackTicket
//                        {
//                            Id = Convert.ToInt32(reader["Id"]),
//                            TicketNo = reader["TicketNo"].ToString(),
//                            DepartmentId = Convert.ToInt32(reader["DepartmentId"]),
//                            CategoryId = Convert.ToInt32(reader["CategoryId"]),
//                            SubCategoryId = Convert.ToInt32(reader["SubCategoryId"]),
//                            Date = Convert.ToDateTime(reader["Date"]),
//                            Status = reader["Status"].ToString()
//                        };
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw new Exception($"Error retrieving ticket with ID {id}", ex);
//            }

//            return ticket;
//        }

//        // Create Ticket
//        public bool CreateTicket(TrackTicket ticket)
//        {
//            try
//            {
//                using (SqlConnection conn = new SqlConnection(_connectionString))
//                {
//                    SqlCommand cmd = new SqlCommand("CreateTicket", conn)
//                    {
//                        CommandType = CommandType.StoredProcedure
//                    };
//                    cmd.Parameters.AddWithValue("@TicketNo", ticket.TicketNo);
//                    cmd.Parameters.AddWithValue("@DepartmentId", ticket.DepartmentId);
//                    cmd.Parameters.AddWithValue("@CategoryId", ticket.CategoryId);
//                    cmd.Parameters.AddWithValue("@SubCategoryId", ticket.SubCategoryId);
//                    cmd.Parameters.AddWithValue("@Date", ticket.Date ?? DateTime.Now);
//                    cmd.Parameters.AddWithValue("@Status", ticket.Status);

//                    conn.Open();
//                    return cmd.ExecuteNonQuery() > 0;
//                }
//            }
//            catch (Exception ex)
//            {
//                throw new Exception("Error creating ticket", ex);
//            }
//        }

//        public bool UpdateTicket(TrackTicket ticket)
//        {
//            try
//            {
//                using (SqlConnection conn = new SqlConnection(_connectionString))
//                {
//                    SqlCommand cmd = new SqlCommand("UpdateTicket", conn)
//                    {
//                        CommandType = CommandType.StoredProcedure
//                    };
//                    cmd.Parameters.AddWithValue("@Id", ticket.Id);
//                    cmd.Parameters.AddWithValue("@TicketNo", ticket.TicketNo);
//                    cmd.Parameters.AddWithValue("@DepartmentId", ticket.DepartmentId);
//                    cmd.Parameters.AddWithValue("@CategoryId", ticket.CategoryId);
//                    cmd.Parameters.AddWithValue("@SubCategoryId", ticket.SubCategoryId);
//                    cmd.Parameters.AddWithValue("@Date", ticket.Date ?? DateTime.Now);
//                    cmd.Parameters.AddWithValue("@Status", ticket.Status);

//                    conn.Open();
//                    return cmd.ExecuteNonQuery() > 0;
//                }
//            }
//            catch (Exception ex)
//            {
//                throw new Exception($"Error updating ticket with ID {ticket.Id}", ex);
//            }
//        }

//        public bool DeleteTicket(int id)
//        {
//            try
//            {
//                using (SqlConnection conn = new SqlConnection(_connectionString))
//                {
//                    SqlCommand cmd = new SqlCommand("DeleteTicket", conn)
//                    {
//                        CommandType = CommandType.StoredProcedure
//                    };
//                    cmd.Parameters.AddWithValue("@Id", id);

//                    conn.Open();
//                    return cmd.ExecuteNonQuery() > 0;
//                }
//            }
//            catch (Exception ex)
//            {
//                throw new Exception($"Error deleting ticket with ID {id}", ex);
//            }
//        }
//    }
//}
