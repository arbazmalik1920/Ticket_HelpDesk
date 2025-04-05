

using System;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;

namespace Ticket.Filters
{
    public class CustomExceptionFilterAttribute : FilterAttribute, IExceptionFilter
    {
        private readonly string _connectionString;

        public CustomExceptionFilterAttribute()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["YourDbConnection"].ConnectionString;
        }

        public void OnException(ExceptionContext filterContext)
        {
            // Prevent further processing of the exception
            filterContext.ExceptionHandled = true;

            // Determine the user who caused the exception
            string createdBy = HttpContext.Current.User?.Identity?.IsAuthenticated == true
               ? HttpContext.Current.User.Identity.Name
               : "admin";

            // Log exception to the database
            LogExceptionToDatabase(filterContext.Exception,
                                   filterContext.RouteData.Values["controller"].ToString(),
                                   filterContext.RouteData.Values["action"].ToString(),
                                   createdBy);

            // Redirect to an error page
            filterContext.Result = new RedirectResult("~/Error/General");
        }

        private void LogExceptionToDatabase(Exception exception, string controller, string action, string createdBy)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("LogException", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Message", exception.Message);
                    cmd.Parameters.AddWithValue("@StackTrace", exception.StackTrace);
                    cmd.Parameters.AddWithValue("@Controller", controller);
                    cmd.Parameters.AddWithValue("@Action", action);
                    cmd.Parameters.AddWithValue("@LogTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@CreatedBy", createdBy);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

