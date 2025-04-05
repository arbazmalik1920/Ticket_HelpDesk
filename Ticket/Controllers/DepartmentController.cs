using System;
using System.Web.Mvc;
using Ticket.DAL;
using Ticket.Models;

namespace Ticket.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly DepartmentDAL _departmentDal;
        private readonly ProductDAL _productDal; // Declare ProductDAL

        public DepartmentController()
        {
            _departmentDal = new DepartmentDAL();
            _productDal = new ProductDAL(); // Initialize ProductDAL
        }

        public ActionResult Index()
        {
            try
            {
                var departments = _departmentDal.GetAllDepartments();
                return View(departments);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                return View("Error");
            }
        }

        public ActionResult Create()
        {
            try
            {
                ViewBag.Products = new SelectList(_productDal.GetAllProducts(), "Id", "Name");
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                return View("Error");
            }
        }

        [HttpPost]
        public JsonResult Create(Department department)
        {
            try
            {
                var rowsAffected = _departmentDal.CreateDepartment(department);
                return Json(new { success = rowsAffected > 0 });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var department = _departmentDal.GetDepartmentById(id);
                ViewBag.Products = new SelectList(_productDal.GetAllProducts(), "Id", "Name", department.ProductId);
                return View(department);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                return View("Error");
            }
        }


        [HttpPost]
        public JsonResult Edit(Department department)
        {
            try
            {
                var rowsAffected = _departmentDal.UpdateDepartment(department);
                return Json(new { success = rowsAffected > 0 });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var department = _departmentDal.GetDepartmentById(id);
                return View(department);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                return View("Error");
            }
        }

        [HttpPost]
        public JsonResult DeleteConfirmed(int id)
        {
            try
            {
                var rowsAffected = _departmentDal.DeleteDepartment(id);
                return Json(new { success = rowsAffected > 0 });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }




    }
}
