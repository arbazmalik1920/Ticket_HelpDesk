using System.Linq;
using Ticket.DAL;

namespace Ticket.DAL
{
    public class HomeDAL
    {
        private readonly UserDAL _userDal = new UserDAL();
        private readonly TrackTicketDAL _trackTicketDal = new TrackTicketDAL();

        public object GetDepartmentWiseTicketData()
        {
            var tickets = _trackTicketDal.GetAllTickets();
            return tickets
                .GroupBy(t => t.DepartmentName)
                .Select(g => new { Department = g.Key, Count = g.Count() })
                .ToList();
        }

        public object GetTicketStatusData()
        {
            var tickets = _trackTicketDal.GetAllTickets();
            return tickets
                .GroupBy(t => t.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToList();
        }

        public object GetDashboardStatistics()
        {
            var users = _userDal.GetAllUsers();
            var tickets = _trackTicketDal.GetAllTickets();

            return new
            {
                ActiveUsers = users.Count(u => u.Status == "Active"),
                InactiveUsers = users.Count(u => u.Status == "Inactive"),
                TotalUsers = users.Count(),
                OpenTickets = tickets.Count(t => t.Status == "Open"),
                ClosedTickets = tickets.Count(t => t.Status == "Closed")
            };
        }
    }
}
