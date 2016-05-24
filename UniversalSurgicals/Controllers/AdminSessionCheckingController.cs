using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversalSurgicals.Controllers
{

    [OutputCacheAttribute(VaryByParam = "*", Duration = 1)] // will be applied to all actions in MyController, unless those actions override with their own decoration
    public class AdminSessionCheckingController : Controller, IActionFilter
    {
        //
        // GET: /AdminSessionChecking/

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            HttpSessionStateBase session = filterContext.HttpContext.Session;
            var activeSession = session["AccountMgr"];

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
