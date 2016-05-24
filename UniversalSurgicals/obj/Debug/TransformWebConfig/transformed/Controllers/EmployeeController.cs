using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversalSurgicals.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpGet]
        public ActionResult ExpenseList()
        {
            return View();
        }
    }
}
