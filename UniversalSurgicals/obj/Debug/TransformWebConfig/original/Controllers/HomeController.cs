using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Universal.BusinessEntities;
using Universal.BusinessLogicLayer;


namespace UniversalSurgicals.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Signin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signin(Employees emp)
        {
            UserBL userBL = new UserBL();
            Employees employees = new Employees();
            employees = userBL.ValidateUser(emp);
            if (employees.Employee_ID != null)
            {
                if (employees.Status == true)
                {
                    if (employees.FK_UserGroup == 1)
                    {
                        Session["EmpID"] = employees.Employee_ID.Trim();
                        return Json(new { redirectTo = Url.Action("ExpenseList", "Employee"), Text = "success", }, JsonRequestBehavior.AllowGet);
                    }
                    else if (employees.FK_UserGroup == 2)
                    {
                        Session["EmpID"] = employees.Employee_ID.Trim();
                        return Json(new { redirectTo = Url.Action("ExpenseList", "Employee"), Text = "success", }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Session["EmpID"] = employees.Employee_ID.Trim();
                        return Json(new { redirectTo = Url.Action("ExpenseList", "Employee"), Text = "success", }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { Available = "Not a valid user", Text = "Please activate your account and login" });
                }
            }
            else
            {
                return Json(new { Available = "Not a valid user", Text = "Incorrect EmployeeId or Password" });
            }
          

        }
    }
}
