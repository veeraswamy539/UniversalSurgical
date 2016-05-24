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
using System.Text.RegularExpressions;
using System.IO;


namespace UniversalSurgicals.Controllers
{
    public class TravelDeskController : TravelDeskSessionCheckingController
    {

        [HttpGet]
        public ActionResult TravelDeskManagerDashBoard(string id)
        {
            UserBL userBL = new UserBL();
            Employees emp = new Employees();
            emp = userBL.GetEmployeeByID(Session["TravelManager"].ToString(), null);
            return View(emp);
        }


        [HttpGet]
        public ActionResult CreateTravel()
        {
            UserBL userBL = new UserBL();

            Session["url"] = "/TravelDesk/CreateTravel";

            #region employee detailas
            UserBL userbl = new UserBL();
            Universal.BusinessEntities.EmployeesList empList = new Universal.BusinessEntities.EmployeesList();
            empList = userbl.GetEmployees();
            List<SelectListItem> employeeList = new List<SelectListItem>();
            foreach (var item in empList)
            {
                employeeList.Add(new SelectListItem()
                {
                    Text =  item.FirstName + " - " + item.Employee_ID,
                    Value = item.Id.ToString(),
                    Selected = (item.Employee_ID == "" ? true : false)
                });
            }
            //var employeeids = (from s in empList select s.Employee_ID);
            ViewBag.empList = employeeList;

            #endregion

            return PartialView("CreateTravel");
        }


        [HttpPost]
        public ActionResult CreateTravel(TravelDesk travelDesk)
        {
            UserBL userBL = new UserBL();
            string UserSession = (HttpContext.Session["TravelManager"] != null) ? HttpContext.Session["TravelManager"].ToString() : "TravelManager Session Null";
            if (travelDesk.Purpose == null)
            {
                travelDesk.Purpose = "TrainingExpenditure";
            }

            travelDesk.CreatedBy = Session["TravelManager"].ToString();
            travelDesk.ModifiedBy = Session["TravelManager"].ToString();
            int? err = userBL.CreateTravel(travelDesk,1);


            if (err != null)
            {
                Session["TravelId"] = err;
                userBL.sendmail(travelDesk);
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "Traveldesk Manager successfully send the Travel Desk information");
                return Json(new { Available = true, Text = "You have successfully send the Travel Desk Information" });
            }
            else
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs"), UserSession.ToUpper(), "not send traveldesk information");
                return Json(new { Available = false, Text = "Your request not submitted" });
            }
        }


        [HttpGet]
        public ActionResult CreateAdvanceAmount()
        {
            UserBL userBL = new UserBL();

            Session["url"] = "/TravelDesk/CreateAdvanceAmount";

            #region employee detailas
            UserBL userbl = new UserBL();
            Universal.BusinessEntities.EmployeesList empList = new Universal.BusinessEntities.EmployeesList();
            empList = userbl.GetEmployees();
            List<SelectListItem> employeeList = new List<SelectListItem>();
            foreach (var item in empList)
            {
                employeeList.Add(new SelectListItem()
                {
                    Text = item.Employee_ID + " [" + item.FirstName + "] ",
                    Value = item.Id.ToString(),
                    Selected = (item.Employee_ID == "" ? true : false)
                });
            }
            //var employeeids = (from s in empList select s.Employee_ID);
            ViewBag.empList = employeeList;

            #endregion

            return PartialView("CreateAdvanceAmount");
        }

        [HttpPost]
        public string UploadTickets()
        {
            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var uploadTicket = Request.Files[i];
                    if (uploadTicket != null && uploadTicket.ContentLength > 0)
                    {
                        var inputStream = uploadTicket.InputStream;
                        var fileName = RemoveSpecialCharactersSpaces(DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + i + "_" + Session["TravelId"] + "_") + uploadTicket.FileName;
                        TravelUploadTicket trvelUpload = new TravelUploadTicket();
                        trvelUpload.Fk_TravelId = int.Parse(Session["TravelId"].ToString());
                        trvelUpload.TicketImage = fileName;
                        UserBL userBL = new UserBL();
                        int? err = userBL.InsertTravelUploadTicket(trvelUpload);
                        var path = Path.Combine(Server.MapPath("~/Content/TravelImages"), fileName);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            inputStream.CopyTo(fileStream);
                        }

                    }

                }
            }


            catch (Exception)
            {

                throw;
            }


            return "";
        }

        public static string RemoveSpecialCharactersSpaces(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_]+", "");
        }
    }
}
