using System;
using System.Web.Mvc;
using Ticket.Models;
using Ticket.DAL;

public class CategoryController : Controller
{
    private readonly DepartmentDAL _departmentDal = new DepartmentDAL();
    private readonly CategoryDAL _categoryDal = new CategoryDAL();

    // Index Action to show all categories
    public ActionResult Index()
    {
        try
        {
            var categories = _categoryDal.GetAllCategories();
            ViewBag.Departments = _departmentDal.GetAllDepartments();
            return View(categories);
        }
        catch (Exception ex)
        {
            return View("Error", new HandleErrorInfo(ex, "Category", "Index"));
        }
    }

    // GET: Create
    public ActionResult Create()
    {
        try
        {
            ViewBag.Departments = _departmentDal.GetAllDepartments();
            return View();
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
            return View("Error");
        }
    }


    // POST: Create
    [HttpPost]
    
    public JsonResult Create(Category category)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Invalid category data!" });
        }

        try
        {
            var success = _categoryDal.CreateCategory(category);

            if (success)
            {
                return Json(new { success = true, message = "Category created successfully!" });
            }
            else
            {
                return Json(new { success = false, message = "Failed to create category!" });
            }
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = $"Error: {ex.Message}" });
        }
    }

    public ActionResult Edit(int id)
    {
        try
        {
            var category = _categoryDal.GetCategoryById(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            ViewBag.Departments = _departmentDal.GetAllDepartments();
            return View(category);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
            return View("Error");
        }
    }


    // POST: Category/Edit/{id}
    [HttpPost]
    public JsonResult Edit(int id, Category category)
    {
        if (id != category.Id)
        {
            return Json(new { success = false, message = "Invalid category ID." });
        }

        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Invalid category data!" });
        }

        try
        {
            var success = _categoryDal.UpdateCategory(category);
            return Json(new { success = success, message = success ? "Category updated successfully!" : "Failed to update category!" });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = $"Error: {ex.Message}" });
        }
    }





    // GET: Delete
    public ActionResult Delete(int id)
    {
        try
        {
            var category = _categoryDal.GetCategoryById(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
            return View("Error");
        }
    }


    // POST: Delete
    [HttpPost]
    public JsonResult DeleteConfirmed(int id)
    {
        if (id <= 0)
        {
            return Json(new { success = false, message = "Invalid category ID!" });
        }

        try
        {
            var success = _categoryDal.DeleteCategory(id);
            return Json(new { success = success, message = success ? "Category deleted successfully!" : "Failed to delete category!" });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = $"Error: {ex.Message}" });
        }
    }



    // Get Category by ID
    //public JsonResult GetById(int id)
    //{
    //    var category = _categoryDal.GetCategoryById(id);
    //    return Json(category, JsonRequestBehavior.AllowGet);
    //}
}
