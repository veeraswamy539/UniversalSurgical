using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Universal.BusinessEntities;
using Universal.BusinessLogicLayer;
using System.Web.Security;
using PagedList;
using PagedList.Mvc;
using System.Data;


namespace UniversalSurgicals.Controllers
{
    public class ManagerController : ManagerSessionCheckingController
    {
        [HttpGet]
        public ActionResult ManagerDashBoard(string id)
        {
            UserBL userBL = new UserBL();
            Employees emp = new Employees();
            emp = userBL.GetEmployeeByID(Session["Manager"].ToString(), null);
            return View(emp);
        }

        [HttpGet]
        public ActionResult EmployeeExpenditure()
        {
            return View();
        }



    }
}
