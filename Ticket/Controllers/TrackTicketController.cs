using System;
using System.Web.Mvc;
using Ticket.Models;
using Ticket.DAL;
using System.Linq;   //for search the tickets on datebased



public class TrackTicketController : Controller
{
    private readonly TrackTicketDAL _trackTicketDal = new TrackTicketDAL();
    private readonly DepartmentDAL _departmentDal = new DepartmentDAL();
    private readonly CategoryDAL _categoryDal = new CategoryDAL();
    private readonly SubCategoryDAL _subCategoryDal = new SubCategoryDAL();

    // GET: TrackTicket/Index
    public ActionResult Index()
    {
        //try
        //{
        //    throw new Exception("Test exception");
        //}
        //catch (Exception ex)
        //{
        //    return View("Error", new HandleErrorInfo(ex, "TrackTicket", "Index"));
        //}

        return View();
    }


    //GET: TrackTicket/GetAllTickets
    //public JsonResult GetAllTickets()
    //{
    //    try
    //    {
    //        var tickets = _trackTicketDal.GetAllTickets();
    //        return Json(tickets, JsonRequestBehavior.AllowGet);
    //    }
    //    catch (Exception ex)
    //    {
    //        return Json(new { success = false, message = $"Error: {ex.Message}" }, JsonRequestBehavior.AllowGet);
    //    }
    //}


    // GET: TrackTicket/GetAllTickets
    public JsonResult GetAllTickets(string ticketNo = "")
    {
        try
        {
            var tickets = string.IsNullOrWhiteSpace(ticketNo)
                ? _trackTicketDal.GetAllTickets() // Get all tickets if no search term is provided
                : _trackTicketDal.GetAllTickets()
                                 .Where(t => t.TicketNo.ToLower().Contains(ticketNo.ToLower()))
                                 .ToList();

            // Sort tickets in ascending order by Ticket Number
            tickets = tickets.OrderBy(t => t.TicketNo).ToList();

            return Json(tickets, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = $"Error: {ex.Message}" }, JsonRequestBehavior.AllowGet);
        }
    }

    //GET : TATReport
    public ActionResult TATReport()
    {
        return View();
    }

    //GET : TATReport/GetTATReport
    public JsonResult GetTATReport()
    {
        try
        {
            var tickets = _trackTicketDal.GetAllTickets();

            // Sort tickets in ascending order by Ticket Number
            tickets = tickets.OrderBy(t => t.TicketNo).ToList();

            return Json(tickets, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = $"Error: {ex.Message}" }, JsonRequestBehavior.AllowGet);
        }
    }


    // GET: TrackTicket/Create
    public ActionResult Create()
    {
        try
        {
            ViewBag.Departments = _departmentDal.GetAllDepartments();
            ViewBag.Categories = _categoryDal.GetAllCategories();
            ViewBag.SubCategories = _subCategoryDal.GetAllSubCategories();
            return View();
        }
        catch (Exception ex)
        {
            return View("Error", new HandleErrorInfo(ex, "TrackTicket", "Create"));
        }
    }

    // POST: TrackTicket/Create
    [HttpPost]
    public JsonResult Create(TrackTicket ticket)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Invalid data submitted!" });
        }

        try
        {
            var success = _trackTicketDal.CreateTicket(ticket);
            return Json(new { success, message = success ? "Ticket created successfully!" : "Failed to create ticket." });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = $"Error: {ex.Message}" });
        }
    }


    // GET: TrackTicket/Edit/{id}
    public ActionResult Edit(int id)
    {
        try
        {
            var ticket = _trackTicketDal.GetTicketById(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }

            ViewBag.Departments = _departmentDal.GetAllDepartments();
            ViewBag.Categories = _categoryDal.GetAllCategories();
            ViewBag.SubCategories = _subCategoryDal.GetAllSubCategories();


            return PartialView("_Edit", ticket);
            //return View(ticket);

        }
        catch (Exception ex)
        {
            return View("Error", new HandleErrorInfo(ex, "TrackTicket", "Edit"));
        }
    }


    // POST: TrackTicket/Edit
    [HttpPost]
    public JsonResult Edit(TrackTicket trackTicket)
    {
        //if (!ModelState.IsValid)
        //{

        //    return Json(new { success = false, message = "Invalid ticket data!" });
        //}

        try
        {
            var success = _trackTicketDal.UpdateTicket(trackTicket);
            return Json(new { success, message = success ? "Ticket updated successfully!" : "Failed to update ticket." });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = $"Error: {ex.Message}" });
        }
    }






    // GET: TrackTicket/Delete/{id}
    public ActionResult Delete(int id)
    {
        try
        {
            var ticket = _trackTicketDal.GetTicketById(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            //return View(ticket);
            return PartialView("_Delete", ticket);
        }
        catch (Exception ex)
        {
            return View("Error", new HandleErrorInfo(ex, "TrackTicket", "Delete"));
        }
    }

    // POST: TrackTicket/Delete
    [HttpPost]
    public JsonResult DeleteConfirmed(int id)
    {
        if (id <= 0)
        {
            return Json(new { success = false, message = "Invalid ticket ID!" });
        }

        try
        {
            var success = _trackTicketDal.DeleteTicket(id);
            return Json(new { success, message = success ? "Ticket deleted successfully!" : "Failed to delete ticket." });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = $"Error: {ex.Message}" });
        }
    }
}





