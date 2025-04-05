using System.Web;
using System.Web.Mvc;
using Ticket.Filters;

namespace Ticket
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomAuthorizeAttribute()); // Apply to all controllers for autherization
            filters.Add(new CustomExceptionFilterAttribute()); //For Exception
        }
    }
}
