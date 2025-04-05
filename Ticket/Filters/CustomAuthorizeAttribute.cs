using System.Web;
using System.Web.Mvc;

namespace Ticket.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Check if the user session is valid
            return httpContext.Session["User"] != null;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Redirect to login page if unauthorized
            filterContext.Result = new RedirectResult("~/Account/Login");
        }
    }
}
