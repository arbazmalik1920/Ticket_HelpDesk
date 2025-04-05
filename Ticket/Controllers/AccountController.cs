
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Ticket.Controllers
{
    public class AccountController : Controller
    {
        private readonly string _connectionString;

        public AccountController()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["YourDbConnection"].ConnectionString;
        }

        [AllowAnonymous]
        // Render the Login Page
        public ActionResult Login()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                return View("Error");
            }
        }


        [AllowAnonymous]
        // Validate Login
        [HttpPost]
        public JsonResult ValidateLogin(string username, string password)
        {
            try
            {
                bool isValid = false;

                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ValidateUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        con.Open();
                        var result = cmd.ExecuteScalar();
                        isValid = Convert.ToInt32(result) > 0;
                    }
                }

                if (isValid)
                {
                    Session["User"] = username;
                    Session.Timeout = 20;
                }

                return Json(new { isValid }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { isValid = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        // Logout Action
        public ActionResult Logout()
        {
            try
            {
                Session.Clear();
                Session.Abandon();
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                return View("Error");
            }
        }
    }
}

