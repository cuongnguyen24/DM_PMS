using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using PMS.Common;

namespace PMS.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Session[CommonConstants.ACCOUNT_SESSION] = "anhpd";
            //GetInfo(null, null);
            var session = HttpContext.Session.GetString(CommonConstants.UserId);
            session = "X";
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "LOGIN", action = "Index" }));
            }
            base.OnActionExecuting(filterContext);
        }

        protected void SetAlert(string type, string message)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "error";
            }
            else
            {
                TempData["AlertType"] = "info";
            }
        }
    }
}
