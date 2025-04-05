
using System;
using System.Web.Mvc;
using Ticket.Models;
using Ticket.DAL; 

namespace Ticket.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductDAL _productDAL;

        public ProductController()
        {
            _productDAL = new ProductDAL();
        }

        // GET: Product/Index
        public ActionResult Index()
        {
            try
            {
                var products = _productDAL.GetAllProducts();
                return View(products);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                return View("Error");
            }
        }

        // GET: Product/Create
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

        // POST: Product/Create (Handle product creation)
        [HttpPost]
        public JsonResult Create(Product product)
        {
            bool success = false;
            string message = "An error occurred.";

            try
            {
                if (ModelState.IsValid)
                {
                    success = _productDAL.CreateProduct(product);
                    message = success ? "Product created successfully." : "Failed to create product.";
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

        // GET: Product/Edit/Id
        public ActionResult Edit(int id)
        {
            try
            {
                var product = _productDAL.GetProductById(id);
                if (product == null)
                {
                    return HttpNotFound();
                }

                return View(product);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                return View("Error");
            }
        }


        // POST: Product/Edit/Id (Handle product update)
        [HttpPost]
        public JsonResult Edit(Product product)
        {
            bool success = false;
            string message = "An error occurred.";

            try
            {
                if (ModelState.IsValid)
                {
                    success = _productDAL.UpdateProduct(product);
                    message = success ? "Product updated successfully." : "Failed to update product.";
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

        // GET: Product/Delete/Id
        public ActionResult Delete(int id)
        {
            try
            {
                var product = _productDAL.GetProductById(id);
                if (product == null)
                {
                    return HttpNotFound();
                }

                return View(product);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
                return View("Error");
            }
        }

        // POST: Product/Delete/Id (Handle product deletion)
        [HttpPost]
        public JsonResult DeleteConfirmed(int id)
        {
            bool success = false;
            string message = "An error occurred.";

            try
            {
                success = _productDAL.DeleteProduct(id);
                message = success ? "Product deleted successfully." : "Failed to delete product.";
            }
            catch (Exception ex)
            {
                message = "An error occurred: " + ex.Message;
            }

            return Json(new { success, message });
        }
    }
}

