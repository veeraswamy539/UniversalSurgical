using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversalSurgicals.Controllers
{
    public class TravelDeskSessionCheckingController : Controller, IActionFilter
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            HttpSessionStateBase session = filterContext.HttpContext.Session;
            var activeSession = session["TravelManager"];

            if (activeSession != null)
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
