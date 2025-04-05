using System;
using System.Web.Mvc;
using Ticket.Models;
using Ticket.DAL;

namespace Ticket.Controllers
{
    public class RoleMasterController : Controller
    {
        private readonly RoleMasterDAL _roleMasterDAL;

        public RoleMasterController()
        {
            _roleMasterDAL = new RoleMasterDAL();
        }

        // GET: 
        public ActionResult Index()
        {
            try
            {
                var roles = _roleMasterDAL.GetAllRoleMaster();
                return View(roles);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                return View("Error");
            }
        }


        // GET: 
        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                return View("Error");
            }
        }

        // POST:  
        [HttpPost]
        public JsonResult Create(RoleMaster roleMaster)
        {
            bool success = false;
            string message = "An error occurred.";

            try
            {
                if (ModelState.IsValid)
                {
                    success = _roleMasterDAL.CreateRoleMaster(roleMaster);
                    message = success ? "Role created successfully." : "Failed to create role.";
                }
                else
                {
                    message = "Invalid data.";
                }
            }
            catch (Exception ex)
            {
                message = "An error occurred: " + ex.Message;
            }

            return Json(new { success, message });
        }

        // GET: 
        public ActionResult Edit(int id)
        {
            try
            {
                var roleMaster = _roleMasterDAL.GetRoleMasterById(id);
                if (roleMaster == null)
                {
                    return HttpNotFound();
                }

                return View(roleMaster);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                return View("Error");
            }
        }

        // POST: 
        [HttpPost]
        public JsonResult Edit(RoleMaster roleMaster)
        {
            bool success = false;
            string message = "An error occurred.";

            try
            {
                if (ModelState.IsValid)
                {
                    success = _roleMasterDAL.UpdateRoleMaster(roleMaster);
                    message = success ? "Role updated successfully." : "Failed to update role.";
                }
                else
                {
                    message = "Invalid data.";
                }
            }
            catch (Exception ex)
            {
                message = "An error occurred: " + ex.Message;
            }

            return Json(new { success, message });
        }

        // GET: 
        public ActionResult Delete(int id)
        {
            try
            {
                var roleMaster = _roleMasterDAL.GetRoleMasterById(id);
                if (roleMaster == null)
                {
                    return HttpNotFound();
                }

                return View(roleMaster);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                return View("Error");
            }
        }

        // POST: 

        [HttpPost]
        public JsonResult DeleteConfirmed(int roleId)
        {
            try
            {
                bool success = _roleMasterDAL.DeleteRoleMaster(roleId);
                string message = success ? "Role deleted successfully." : "Failed to delete role.";
                return Json(new { success, message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


    }
}
