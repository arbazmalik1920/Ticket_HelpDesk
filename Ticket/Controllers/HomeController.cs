using System;
using System.Web.Mvc;
using Ticket.DAL;

namespace Ticket.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeDAL _homeDal = new HomeDAL();

        public ActionResult Index()
        {
            // Get department-wise ticket count
            var departmentWiseData = _homeDal.GetDepartmentWiseTicketData();

            // Get ticket status count
            var ticketStatusData = _homeDal.GetTicketStatusData();

            ViewBag.DepartmentData = departmentWiseData;
            ViewBag.TicketStatusData = ticketStatusData;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpGet]
        public JsonResult GetDashboardStats()
        {
            try
            {
                var stats = _homeDal.GetDashboardStatistics();
                return Json(stats, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}





































//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using Ticket.DAL;

//namespace Ticket.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly DepartmentDAL _departmentDaL = new DepartmentDAL();
//        private readonly UserDAL _userDal = new UserDAL();
//        private readonly TrackTicketDAL _trackTicketDal = new TrackTicketDAL();
//        public ActionResult Index()
//        {
//            // Get department-wise ticket count
//            var tickets = _trackTicketDal.GetAllTickets();
//            var departmentWiseData = tickets
//                .GroupBy(t => t.DepartmentName)
//                .Select(g => new { Department = g.Key, Count = g.Count() })
//                .ToList();

//            // Get ticket status count
//            var ticketStatusData = tickets
//                .GroupBy(t => t.Status)
//                .Select(g => new { Status = g.Key, Count = g.Count() })
//                .ToList();

//            ViewBag.DepartmentData = departmentWiseData;
//            ViewBag.TicketStatusData = ticketStatusData;

//            return View();
//        }

//        public ActionResult About()
//        {
//            ViewBag.Message = "Your application description page.";

//            return View();
//        }

//        public ActionResult Contact()
//        {
//            ViewBag.Message = "Your contact page.";

//            return View();
//        }

//        [HttpGet]
//        public JsonResult GetDashboardStats()
//        {
//            try
//            {
//                var stats = new
//                {
//                    ActiveUsers = _userDal.GetAllUsers().Count(u => u.Status == "Active"),
//                    InactiveUsers = _userDal.GetAllUsers().Count(u => u.Status == "Inactive"),
//                    TotalUsers = _userDal.GetAllUsers().Count(),
//                    OpenTickets = _trackTicketDal.GetAllTickets().Count(t => t.Status == "Open"),
//                    ClosedTickets = _trackTicketDal.GetAllTickets().Count(t => t.Status == "Closed")
//                };

//                return Json(stats, JsonRequestBehavior.AllowGet);
//            }
//            catch (Exception ex)
//            {
//                return Json(new { success = false, message = $"Error: {ex.Message}" }, JsonRequestBehavior.AllowGet);
//            }
//        }

//    }
//}













































//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using Ticket.DAL;

//namespace Ticket.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly DepartmentDAL _departmentDaL = new DepartmentDAL();
//        private readonly UserDAL _userDal = new UserDAL();
//        private readonly TrackTicketDAL _trackTicketDal = new TrackTicketDAL();
//        public ActionResult Index()
//        {
//            try
//            {
//                // Get department-wise ticket count
//                var tickets = _trackTicketDal.GetAllTickets();

//                var departmentWiseData = tickets
//                    .GroupBy(t => t.DepartmentName)
//                    .Select(g => new { Department = g.Key, Count = g.Count() })
//                    .ToList();

//                // Get ticket status count
//                var ticketStatusData = tickets
//                    .GroupBy(t => t.Status)
//                    .Select(g => new { Status = g.Key, Count = g.Count() })
//                    .ToList();

//                ViewBag.DepartmentData = departmentWiseData;
//                ViewBag.TicketStatusData = ticketStatusData;

//                return View();
//            }
//            catch (Exception ex)
//            {
//                ViewBag.ErrorMessage = "An error occurred: " + ex.Message;
//                return View("Error");
//            }
//        }

//        public ActionResult About()
//        {
//            ViewBag.Message = "Your application description page.";

//            return View();
//        }

//        public ActionResult Contact()
//        {
//            ViewBag.Message = "Your contact page.";

//            return View();
//        }

//        [HttpGet]
//        public JsonResult GetDashboardStats()
//        {
//            try
//            {
//                var stats = new
//                {
//                    ActiveUsers = _userDal.GetAllUsers().Count(u => u.Status == "Active"),
//                    InactiveUsers = _userDal.GetAllUsers().Count(u => u.Status == "Inactive"),
//                    TotalUsers = _userDal.GetAllUsers().Count(),
//                    OpenTickets = _trackTicketDal.GetAllTickets().Count(t => t.Status == "Open"),
//                    ClosedTickets = _trackTicketDal.GetAllTickets().Count(t => t.Status == "Closed")
//                };

//                return Json(stats, JsonRequestBehavior.AllowGet);
//            }
//            catch (Exception ex)
//            {
//                return Json(new { success = false, message = $"Error: {ex.Message}" }, JsonRequestBehavior.AllowGet);
//            }
//        }

//    }
//}





























