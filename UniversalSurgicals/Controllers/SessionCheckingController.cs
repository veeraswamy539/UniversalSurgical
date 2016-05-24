using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversalSurgicals.Controllers
{
    public class SessionCheckingController : Controller, IActionFilter
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            HttpSessionStateBase session = filterContext.HttpContext.Session;
            if ((Session["AccountMgr"] != null) || (Session["TravelManager"] != null) || (Session["Manager"] != null) || (Session["Employee"] != null))
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                filterContext.Result = new RedirectResult(Url.Action("Logout", "Home"));
            }

        }


    }
}
