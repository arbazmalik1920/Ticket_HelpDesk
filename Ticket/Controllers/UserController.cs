using System;
using System.Web.Mvc;
using Ticket.Models;
using Ticket.DAL;
using System.Linq;

namespace Ticket.Controllers
{
    [Ticket.Filters.CustomExceptionFilter]       //Exception Filter
    public class UserController : Controller
    {
        private readonly UserDAL _userDal = new UserDAL();
        private readonly DepartmentDAL _departmentDal = new DepartmentDAL();

        // GET: User/Index - List All Users
        public ActionResult Index()
        {

            try
            {
                var users = _userDal.GetAllUsers();
                ViewBag.Departments = _departmentDal.GetAllDepartments();
                return View(users);
            }
            catch (Exception ex)
            {

                return View("Error", new HandleErrorInfo(ex, "User", "Index"));
            }
        }


        //Exception filter - log
        //public ActionResult Index()
        //{
        //    try
        //    {
        //        throw new Exception("Test exception");
        //    }
        //    catch (Exception ex)
        //    {
        //        return View("Error", new HandleErrorInfo(ex, "User", "Index"));
        //    }
        //}



        // GET: User/Create
        public ActionResult Create()
        {
            
            try
            {
                
                ViewBag.Departments = _departmentDal.GetAllDepartments();
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "User", "Create"));
            }
        }

        // POST: User/Create
        [HttpPost]
        public JsonResult Create(User user)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid user data!" });
            }

            try
            {
                var success = _userDal.CreateUser(user); 
                return Json(new { success, message = success ? "User created successfully!" : "Failed to create user!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }


        // GET: User/Edit/{id}
        public ActionResult Edit(int id)
        {
            try
            {
                var user = _userDal.GetUserById(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
               
                ViewBag.Departments = _departmentDal.GetAllDepartments();
                return View(user);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "User", "Edit"));
            }

        }

       

        // POST: User/Edit/{id}
        [HttpPost]
        public JsonResult Edit(int id, User user)
        {
            if (id != user.UserId)
            {
                return Json(new { success = false, message = "Invalid user ID." });
            }

            //if (!ModelState.IsValid)
            //{
            //    return Json(new { success = false, message = "Invalid user data!" });
            //}

            try
            {
                var success = _userDal.UpdateUser(user);
                return Json(new { success, message = success ? "User updated successfully!" : "Failed to update user!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }


        // GET: User/Delete/{id}
        public ActionResult Delete(int id)
        {
            var user = _userDal.GetUserById(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: User/DeleteConfirmed
        [HttpPost]
        public JsonResult DeleteConfirmed(int id)
        {
            if (id <= 0)
            {
                return Json(new { success = false, message = "Invalid user ID!" });
            }

            try
            {
                var success = _userDal.DeleteUser(id);
                return Json(new { success, message = success ? "User deleted successfully!" : "Failed to delete user!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }


        // GET: User/ChangePassword
        //public ActionResult ChangePassword()
        //{
        //    ViewBag.Title = "Change Password";


        //    var userId = User.Identity.GetUserId<int>(); // Replace this with your authentication mechanism.
        //    //var userId = _userDal.GetUserById(id);

        //    var model = new ChangePassword
        //    {
        //        UserId = userId
        //    };

        //    return View(model);
        //}

        //// POST: Change Password
        //[HttpPost]
        //public JsonResult ChangePassword(ChangePassword model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Json(new { success = false, message = "Invalid data!" });
        //    }

        //    try
        //    {

        //        var loggedInUserId = User.Identity.GetUserId<int>();
        //        //var loggedInUserId = _userDal.GetUserById(id);
        //        if (model.UserId != loggedInUserId)
        //        {
        //            return Json(new { success = false, message = "Unauthorized operation!" });
        //        }

        //        var user = _userDal.GetUserById(loggedInUserId);
        //        if (user == null)
        //        {
        //            return Json(new { success = false, message = "User not found!" });
        //        }

        //        if (!string.Equals(user.Password, model.CurrentPassword))
        //        {
        //            return Json(new { success = false, message = "Invalid current password." });
        //        }

        //        if (!string.Equals(model.NewPassword, model.ConfirmPassword))
        //        {
        //            return Json(new { success = false, message = "Passwords do not match!" });
        //        }

        //        var success = _userDal.ChangePassword(loggedInUserId, model.NewPassword);
        //        return Json(new { success, message = success ? "Password changed successfully!" : "Failed to change password." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = $"Error: {ex.Message}" });
        //    }
        //}


    }
}
