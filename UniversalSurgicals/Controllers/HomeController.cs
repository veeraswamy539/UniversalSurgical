using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Universal.BusinessEntities;
using Universal.BusinessLogicLayer;
using System.Web.Security;
using System.Text;


namespace UniversalSurgicals.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult test()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Signin()
        {
            //if (Session["AccountMgr"] != null)
            //{
            //    return RedirectToAction("AdminDashBoard", "Admin");
            //}
            //else if (Session["TravelManager"] != null)
            //{
            //    return RedirectToAction("TravelDeskManagerDashBoard", "TravelDesk");
            //}
            //else if (Session["Manager"] != null)
            //{
            //    return RedirectToAction("ManagerDashBoard", "Manager");
            //}
            //else if (Session["Employee"] != null)
            //{
            //    return RedirectToAction("DashBoard", "Employee");
            //}
            //else
            //{
            //    return View();
            //}
                        
           
            return View();
        }

        
        [HttpPost]
        public ActionResult Signin(Employees emp)
        {
            UserBL userBL = new UserBL();
            Employees employees = new Employees();
            LogFiles logfiles = new LogFiles();
            if (!string.IsNullOrWhiteSpace(emp.Employee_ID) && !string.IsNullOrEmpty(emp.Password))
            {
                employees = userBL.ValidateUser(emp);
                if (employees.Employee_ID != null)
                {
                    if (employees.Status == true)
                    {
                        Session.Clear();
                        Session.RemoveAll();
                        if (employees.FK_UserGroup == 3)
                        {
                            Session["Employee"] = employees.Employee_ID.Trim();
                            
                            logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"),employees.Employee_ID.ToUpper() , "Employee is successfully loggged in");

                            return Json(new { redirectTo = Url.Action("DashBoard", "Employee"), Text = "success", }, JsonRequestBehavior.AllowGet);
                        }
                        else if (employees.FK_UserGroup == 1)
                        {
                            Session["AccountMgr"] = employees.Employee_ID.Trim();
                            logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), employees.Employee_ID.ToUpper(), "Admin is successfully loggged in");
                            return Json(new { redirectTo = Url.Action("AdminDashBoard", "Admin"), Text = "success", }, JsonRequestBehavior.AllowGet);
                        }
                        else if (employees.FK_UserGroup == 2)
                        {
                            Session["Manager"] = employees.Employee_ID.Trim();
                            logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), employees.Employee_ID.ToUpper(), "Manager is successfully loggged in");
                            return Json(new { redirectTo = Url.Action("ManagerDashBoard", "Manager"), Text = "success", }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            Session["TravelManager"] = employees.Employee_ID.Trim();
                            logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), employees.Employee_ID.ToUpper(), "Traveldesk Manager is successfully loggged in");
                            return Json(new { redirectTo = Url.Action("TravelDeskManagerDashBoard", "TravelDesk"), Text = "success", }, JsonRequestBehavior.AllowGet);

                        }
                    }
                    else
                    {
                        return Json(new { Available = "Not a valid user", Text = "Your account has been deactivated! Please contact Account Manager" });
                    }
                }
                else
                {
                    return Json(new { Available = "Not a valid user", Text = "Incorrect Employee ID or Password" });
                }
            }
            else
            {
                return Json(new { Available = "Not a valid user", Text = "Please Enter Employee ID and Password" });
            }



        }
        public ActionResult SessionOut()
        {
            if ((Session["AccountMgr"] != null) || (Session["TravelManager"] != null) || (Session["Manager"] != null) || (Session["Employee"] != null))
                return Json(new { Text = "session" });
            else
                return Json(new { Text = "nosession" });
        }
        public ActionResult Relogin()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            return PartialView("_Relogin");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Signin", "Home");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(Employees emp)
        {
            UserBL userBL = new UserBL();
            Employees employees = new Employees();
            employees = userBL.ValidateUserId(emp);

            if (employees.Employee_ID != null)
            {

                Emails mail = new Emails();

                StringBuilder sb = new StringBuilder();

                sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>Universal Surgicals</title></head><body style=\"background-color:#e7e8ea;\"> <div style=\"max-width:800px; width:100%; margin:0px auto;\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr bgcolor=\"#333645\" style=\"padding: 30px;\"><td height=\"150px;\" style=\"padding-left:30px;\"> <img src=\"http://universal.globaldatasolutions.com/Content/Images/login-logo.png\"></td></tr><tr bgcolor=\"#FFFFFF\"><td  style=\"font-family:'Open Sans';padding:40px;\"><div style=\"width:100%; font-size:18px; font-weight: bold; font-weight:800px; color:#3dbeba;\">Dear " + employees.FirstName + " " + employees.LastName + ",</div><div style=\"color:#868686; font-size:13px; margin-top:8px;\">We received a request to reset the password associated with this email address. If you made this request, please use the following security password to login your Universal Surgicals account;</div><div style=\"border-radius: 5px; border:  solid 1px #e7e8ea; padding:20px;  font-size:14px;  font-weight: bold; color:#868686; margin-top:20px;\"><table width=\"100%\"><tr><td style=\"font-size:13px;  font-weight: normal; color:#868686;\">Password: <span style=\"color:#3dbeba;\"> " + employees.Password.ToString() + " </span> </td> </tr></table></div>");
                sb.Append("<div style=\"color:#868686; font-size:13px; margin-top:15px;\">If you did not request to have your password reset, you can safely ignore this email. We assure you that your account is safe.<br/><br/>Thank you for contacting Universal surgical's support !<br/><br/></div>");
                sb.Append("<div style=\"color:#868686; font-size:13px; margin-top:15px; color:#3dbeba; float: right;  font-weight: 700; text-align: center;\"><b>Expense Management Support !</b><br/><b>Universal Surgicals</b></div>");
                sb.Append("</td></tr></table></div></body></html>");

                mail.GDSSendBulkeMail(System.Configuration.ConfigurationManager.AppSettings["webmail"].ToString(), employees.CompanyMailID, "", "", sb.ToString(), " Universal Surgicals:- Password recovery assistance ", "", "Iwjil");



                return Json(new { Available = "valid user", Text = "To get back into your account, follow the instructions we've sent to your webmail address." });
            }
            else
            {
                return Json(new { Available = "Not a valid user", Text = "Incorrect Employee Id" });
            }





        }

    }
}
