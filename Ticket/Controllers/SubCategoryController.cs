using System;
using System.Web.Mvc;
using Ticket.Models;
using Ticket.DAL;

public class SubCategoryController : Controller
{
    private readonly SubCategoryDAL _subCategoryDal = new SubCategoryDAL();
    private readonly CategoryDAL _categoryDal = new CategoryDAL();
    private readonly DepartmentDAL _departmentDal = new DepartmentDAL();

    // GET: SubCategory/Index
    public ActionResult Index()
    {
        try
        {
            var subCategories = _subCategoryDal.GetAllSubCategories();
            return View(subCategories);
        }
        catch (Exception ex)
        {
            return View("Error", new HandleErrorInfo(ex, "SubCategory", "Index"));
        }
    }

    // GET: SubCategory/Create
    public ActionResult Create()
    {
        try
        {
            ViewBag.Categories = _categoryDal.GetAllCategories();
            ViewBag.Departments = _departmentDal.GetAllDepartments();
            return View();
        }
        catch (Exception ex)
        {
            return View("Error", new HandleErrorInfo(ex, "SubCategory", "Create"));
        }
    }

    // POST: SubCategory/Create
    [HttpPost]
    public JsonResult Create(SubCategory subCategory)
    {
        if (subCategory == null)
        {
            return Json(new { success = false, message = "SubCategory data is invalid." });
        }

        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Please fill in all fields!" });
        }

        try
        {
            var success = _subCategoryDal.CreateSubCategory(subCategory);
            if (success)
            {
                return Json(new { success = true, message = "SubCategory created successfully!" });
            }
            else
            {
                return Json(new { success = false, message = "Failed to create SubCategory!" });
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = $"Error: {ex.Message}" });
        }
    }


    // GET: SubCategory/Edit/{id}
    public ActionResult Edit(int id)
    {
        try
        {
            var subCategory = _subCategoryDal.GetSubCategoryById(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }

            ViewBag.Categories = _categoryDal.GetAllCategories();
            ViewBag.Departments = _departmentDal.GetAllDepartments();
            return View(subCategory);
        }
        catch (Exception ex)
        {
            return View("Error", new HandleErrorInfo(ex, "SubCategory", "Edit"));
        }
    }

    // POST: SubCategory/Edit
    [HttpPost]
    public JsonResult Edit(SubCategory subCategory)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Invalid data!" });
        }

        try
        {
            var success = _subCategoryDal.UpdateSubCategory(subCategory);
            return Json(new { success, message = success ? "SubCategory updated successfully!" : "Failed to update SubCategory." });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = $"Error: {ex.Message}" });
        }
    }

    // GET: SubCategory/Delete/{id}
    public ActionResult Delete(int id)
    {
        try
        {
            var subCategory = _subCategoryDal.GetSubCategoryById(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategory);
        }
        catch (Exception ex)
        {
            return View("Error", new HandleErrorInfo(ex, "SubCategory", "Delete"));
        }
    }

    // POST: SubCategory/Delete
    [HttpPost]
    public JsonResult DeleteConfirmed(int id)
    {
        if (id <= 0)
        {
            return Json(new { success = false, message = "Invalid SubCategory ID!" });
        }

        try
        {
            var success = _subCategoryDal.DeleteSubCategory(id);
            return Json(new { success, message = success ? "SubCategory deleted successfully!" : "Failed to delete SubCategory." });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = $"Error: {ex.Message}" });
        }
    }
}
