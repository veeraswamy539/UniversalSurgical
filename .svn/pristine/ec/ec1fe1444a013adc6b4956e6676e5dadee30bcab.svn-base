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
using System.Text;
using System.Web.Script.Serialization;

namespace UniversalSurgicals.Controllers
{

    [OutputCacheAttribute(VaryByParam = "*", Duration = 1)] // will be applied to all actions in MyController, unless those actions override with their own decoration
    public class EmployeeController : EmpSessionCheckingController
    {
        UserBL userBL = new UserBL();

        [HttpGet]
        public ActionResult DashBoard(string id)
        {
            UserBL userBL = new UserBL();
            Employees emp = new Employees();
            emp = userBL.GetEmployeeByID(Session["Employee"].ToString(), null);
            return View(emp);
        }



        public ActionResult Expenditure(int? ExistingID)
        {
            Session["url"] = "/Employee/Expenditure";

            #region object craetion

            Employees emp = new Employees();
            DataTable dtExpenditureType = new DataTable();
            DataTable dtExpenditureTypeDetails = new DataTable();
            DataTable dtClientManagerNames = new DataTable();

            List<SelectListItem> expenseTyep = new List<SelectListItem>();
            List<SelectListItem> expensetypeDetails = new List<SelectListItem>();

            List<SelectListItem> ManagersName = new List<SelectListItem>();

            #endregion

            string EmployeeName = Session["Employee"].ToString();

            //get employee details
            emp = userBL.GetEmployeeByID(Session["Employee"].ToString(), null);

            //get expenditure type
            dtExpenditureType = userBL.GetExpenditureTypes();

            //get expenditure type details
            dtExpenditureTypeDetails = userBL.GetExpenditureTypeDetails();

            //get client manager names
            dtClientManagerNames = userBL.GetClientManagerNames();

            string defExpenditureType = "";
            string defExpenditureTypeDetails = "";
            string defManagerNames = "";

            #region expenditure type

            for (int i = 0; i < dtExpenditureType.Rows.Count; i++)
            {
                if (dtExpenditureType.Rows[i]["ExpanseName"].ToString().Contains("Mode of Transportation") || dtExpenditureType.Rows[i]["ExpanseName"].ToString().Contains("Business Conveyance") || dtExpenditureType.Rows[i]["ExpanseName"].ToString().Contains("Accommodation") || dtExpenditureType.Rows[i]["ExpanseName"].ToString().Contains("Food Allowances") || dtExpenditureType.Rows[i]["ExpanseName"].ToString().Contains("Miscellaneous"))
                {
                    expenseTyep.Add(new SelectListItem()
                    {
                        Text = dtExpenditureType.Rows[i]["ExpanseName"].ToString(),
                        Value = dtExpenditureType.Rows[i]["Id"].ToString(),
                        Selected = (dtExpenditureType.Rows[i]["ExpanseName"].ToString() == defExpenditureType ? true : false)
                    });
                }

            }

            ViewBag.ExpenseTypeList = expenseTyep;

            #endregion

            #region expenditure type details

            expensetypeDetails.Add(new SelectListItem()
            {
                Text = "Select",
                Value = "0",
                Selected = ("Select" == defExpenditureTypeDetails ? true : false)
            });

            for (int i = 0; i < dtExpenditureTypeDetails.Rows.Count; i++)
            {
                if (emp.VehicleType == "TwoWheeler")
                {
                    if (dtExpenditureTypeDetails.Rows[i]["Description"].ToString().Contains("Bike") || dtExpenditureTypeDetails.Rows[i]["Description"].ToString().Contains("Other"))
                    {
                        expensetypeDetails.Add(new SelectListItem()
                        {
                            Text = dtExpenditureTypeDetails.Rows[i]["Description"].ToString(),
                            Value = dtExpenditureTypeDetails.Rows[i]["Id"].ToString(),
                            Selected = (dtExpenditureTypeDetails.Rows[i]["Description"].ToString() == defExpenditureTypeDetails ? true : false)
                        });
                    }
                }
                else if (emp.VehicleType == "FourWheeler")
                {
                    if (dtExpenditureTypeDetails.Rows[i]["Description"].ToString().Contains("Car") || dtExpenditureTypeDetails.Rows[i]["Description"].ToString().Contains("Other"))
                    {
                        expensetypeDetails.Add(new SelectListItem()
                        {
                            Text = dtExpenditureTypeDetails.Rows[i]["Description"].ToString(),
                            Value = dtExpenditureTypeDetails.Rows[i]["Id"].ToString(),
                            Selected = (dtExpenditureTypeDetails.Rows[i]["Description"].ToString() == defExpenditureTypeDetails ? true : false)
                        });
                    }
                }
            }

            ViewBag.ExpenseTypeDetailsList = expensetypeDetails;

            #endregion

            #region mobile
            for (int i = 0; i < dtExpenditureTypeDetails.Rows.Count; i++)
            {
                if (dtExpenditureTypeDetails.Rows[i]["Description"].ToString().Contains("Mobile Bill"))
                {
                    string Value = dtExpenditureTypeDetails.Rows[i]["Id"].ToString();
                    ViewBag.MobileValue = Value;
                }
            }
            #endregion

            #region Internet
            for (int i = 0; i < dtExpenditureTypeDetails.Rows.Count; i++)
            {
                if (dtExpenditureTypeDetails.Rows[i]["Description"].ToString().Contains("Internet Bill"))
                {
                    string Value = dtExpenditureTypeDetails.Rows[i]["Id"].ToString();
                    ViewBag.InternetBill = Value;
                }
            }
            #endregion

            #region ClientManagers

            ManagersName.Add(new SelectListItem()
            {
                Text = "Select Client Manager",
                Value = "0",
                Selected = ("Select" == defManagerNames ? true : false)
            });

            for (int i = 0; i < dtClientManagerNames.Rows.Count; i++)
            {
                ManagersName.Add(new SelectListItem()
                    {
                        Text = dtClientManagerNames.Rows[i]["ClientManagerName"].ToString(),
                        Value = dtClientManagerNames.Rows[i]["Id"].ToString(),
                        Selected = (dtClientManagerNames.Rows[i]["ClientManagerName"].ToString() == defManagerNames ? true : false)

                    });
            }

            ViewBag.ClientManagers = ManagersName;

            #endregion

            if (ExistingID > 0)
            {
                ViewBag.ExpenseHeaderID = ExistingID;
            }

            return View(emp);
        }

        public ActionResult Training(int? ExistingID)
        {
            Session["url"] = "/Employee/Training";

            #region object craetion

            Employees emp = new Employees();
            DataTable dtExpenditureType = new DataTable();
            DataTable dtExpenditureTypeDetails = new DataTable();

            List<SelectListItem> expenseTyep = new List<SelectListItem>();
            List<SelectListItem> expensetypeDetails = new List<SelectListItem>();

            #endregion

            string EmployeeName = Session["Employee"].ToString();

            //get employee details
            emp = userBL.GetEmployeeByID(Session["Employee"].ToString(), null);

            //get expenditure type
            dtExpenditureType = userBL.GetExpenditureTypes();
            //get expenditure type details
            dtExpenditureTypeDetails = userBL.GetExpenditureTypeDetails();

            string defExpenditureType = "";
            string defExpenditureTypeDetails = "";

            #region expenditure type

            for (int i = 0; i < dtExpenditureType.Rows.Count; i++)
            {
                if (dtExpenditureType.Rows[i]["ExpanseName"].ToString().Contains("Mode of Transportation") || dtExpenditureType.Rows[i]["ExpanseName"].ToString().Contains("Business Conveyance") || dtExpenditureType.Rows[i]["ExpanseName"].ToString().Contains("Accommodation") || dtExpenditureType.Rows[i]["ExpanseName"].ToString().Contains("Food Allowances") || dtExpenditureType.Rows[i]["ExpanseName"].ToString().Contains("Miscellaneous"))
                {
                    expenseTyep.Add(new SelectListItem()
                    {
                        Text = dtExpenditureType.Rows[i]["ExpanseName"].ToString(),
                        Value = dtExpenditureType.Rows[i]["Id"].ToString(),
                        Selected = (dtExpenditureType.Rows[i]["ExpanseName"].ToString() == defExpenditureType ? true : false)
                    });
                }

            }

            ViewBag.ExpenseTypeList = expenseTyep;

            #endregion

            #region expenditure type details

            expensetypeDetails.Add(new SelectListItem()
            {
                Text = "Select",
                Value = "0",
                Selected = ("Select" == defExpenditureTypeDetails ? true : false)
            });

            for (int i = 0; i < dtExpenditureTypeDetails.Rows.Count; i++)
            {
                if (emp.VehicleType == "TwoWheeler")
                {
                    if (dtExpenditureTypeDetails.Rows[i]["Description"].ToString().Contains("Bike") || dtExpenditureTypeDetails.Rows[i]["Description"].ToString().Contains("Other"))
                    {
                        expensetypeDetails.Add(new SelectListItem()
                        {
                            Text = dtExpenditureTypeDetails.Rows[i]["Description"].ToString(),
                            Value = dtExpenditureTypeDetails.Rows[i]["Id"].ToString(),
                            Selected = (dtExpenditureTypeDetails.Rows[i]["Description"].ToString() == defExpenditureTypeDetails ? true : false)
                        });
                    }
                }
                else if (emp.VehicleType == "FourWheeler")
                {
                    if (dtExpenditureTypeDetails.Rows[i]["Description"].ToString().Contains("Car") || dtExpenditureTypeDetails.Rows[i]["Description"].ToString().Contains("Other"))
                    {
                        expensetypeDetails.Add(new SelectListItem()
                        {
                            Text = dtExpenditureTypeDetails.Rows[i]["Description"].ToString(),
                            Value = dtExpenditureTypeDetails.Rows[i]["Id"].ToString(),
                            Selected = (dtExpenditureTypeDetails.Rows[i]["Description"].ToString() == defExpenditureTypeDetails ? true : false)
                        });
                    }
                }
            }

            ViewBag.ExpenseTypeDetailsList = expensetypeDetails;

            #endregion

            if (ExistingID > 0)
            {
                ViewBag.ExpenseHeaderID = ExistingID;
            }

            return View(emp);
        }

        [HttpGet]
        public ActionResult CreateLeave()
        {

            //    #region leavetype

            //List<SelectListItem> leavetype = new List<SelectListItem>();

            //SelectListItem _mList = new SelectListItem();
            //_mList = new SelectListItem() { Text = "Select Leave Type", Value = "0" };
            //leavetype.Insert(0, _mList);
            //_mList = new SelectListItem() { Text = "Annual Leave", Value = "Annual Leave" };
            //leavetype.Insert(1, _mList);
            //_mList = new SelectListItem() { Text = "Casual Leave", Value = "Casual Leave" };
            //leavetype.Insert(2, _mList);
            //_mList = new SelectListItem() { Text = "Sick Leave", Value = "Sick Leave" };
            //leavetype.Insert(3, _mList);
            //_mList = new SelectListItem() { Text = "Hospitalized (Admit)", Value = "Hospitalized (Admit)" };
            //leavetype.Insert(4, _mList);
            //_mList = new SelectListItem() { Text = "Funeral Leave", Value = "Funeral Leave" };
            //leavetype.Insert(5, _mList);





            //ViewBag.LeaveType = leavetype;

            //    #endregion

            return PartialView("CreateLeave");
        }

        [HttpPost]
        public ActionResult CreateLeave(Leave Leave)
        {

            Employees emp = new Employees();

            UserBL userBL = new UserBL();
            string UserSession = (HttpContext.Session["Employee"] != null) ? HttpContext.Session["Employee"].ToString() : "Employee Session Null";

            emp = userBL.GetEmployeeByID(Session["Employee"].ToString(), null);

            int? Employee_ID;

            emp = userBL.GetEmployeeByID(Session["Employee"].ToString(), null);

            Employee_ID = emp.Id;

            Universal.BusinessEntities.LeaveList empLeavesList = new Universal.BusinessEntities.LeaveList();

            empLeavesList = userBL.GetLeaves(Employee_ID, null);

            int? countleave = 0;

            for (DateTime? date = Leave.LeaveFrom; date <= Leave.LeaveTo; date = date.Value.AddDays(1))
            {

                countleave = (from cuntlv in empLeavesList where date >= cuntlv.LeaveFrom && date <= cuntlv.LeaveTo && (cuntlv.LeaveStatus == true || cuntlv.LeaveStatus == null) select cuntlv).Count() + countleave;


            }



            if (countleave == 0 && Leave.LeaveTo >= Leave.LeaveFrom)
            {

                Leave.Employee_ID = emp.Id;

                int? err = userBL.CreateLeave(Leave);


                if (err == null)
                {
                    Emails mail = new Emails();

                    StringBuilder sb = new StringBuilder();

                    sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>Universal Surgicals</title></head><body style=\"background-color:#e7e8ea;\"> <div style=\"max-width:800px; width:100%; margin:0px auto;\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr bgcolor=\"#333645\" style=\"padding: 30px;\"><td height=\"150px;\" style=\"padding-left:30px;\"> <img src=\"http://universal.globaldatasolutions.com/Content/Images/login-logo.png\"></td></tr><tr bgcolor=\"#FFFFFF\"><td  style=\"font-family:'Open Sans';padding:40px;\"><div style=\"width:100%; font-size:18px; font-weight: bold; font-weight:800px; color:#3dbeba;\">  Dear Manager,</div><div style=\"color:#868686; font-size:13px; margin-top:8px;\">Leave request submitted by Mr/Ms." + emp.FirstName + " " + emp.LastName + "(" + emp.Employee_ID + ") with the following details;</div><div style=\"border-radius: 5px; border:  solid 1px #e7e8ea; padding:20px;  font-size:14px;  font-weight: bold; color:#868686; margin-top:20px;\"><table width=\"100%\"><tr><td style=\"font-size:13px;  font-weight: normal; color:#868686;\"><div style=\"width:100%; text-align:center\">From Date:</div><div style=\"width:100%; text-align:center\"><span style=\"color:#3dbeba;\"> " + Leave.LeaveFrom.Value.ToShortDateString() + "	</span></div> </td><td style=\"font-size:13px;  font-weight: normal; color:#868686;\"><div style=\"width:100%; text-align:center\">To Date: </div><div style=\"width:100%; text-align:center\"><span style=\"color:#3dbeba;\">" + Leave.LeaveTo.Value.ToShortDateString() + "</span></div></td><td style=\"font-size:13px;  font-weight: normal; color:#868686;\"><div style=\"width:100%; text-align:center\">Type of Leave:</div><div style=\"width:100%; text-align:center\"> <span style=\"color:#3dbeba;\">" + Leave.LeaveType + " </span></div></td><td style=\"font-size:13px;  font-weight: normal; color:#868686;\"><div style=\"width:100%; text-align:center\">Reason (Message/comments):</div> <div style=\"width:100%; text-align:center\"><span style=\"color:#3dbeba;text-transform: capitalize;\">" + Leave.LeaveComment + " </span></div></td></tr></table></div>");
                    sb.Append("<div style=\"color:#868686; font-size:13px; margin-top:15px; color:#3dbeba; float: right;  font-weight: 700; text-align: center;\"><b>Thanks,</b><br/><b>Expense Management Support !</b><br/><b>Universal Surgicals</b></div>");
                    sb.Append("</td></tr></table></div></body></html>");

                    if (emp.FK_UserGroup == 3)
                    {
                        mail.GDSSendBulkeMail(System.Configuration.ConfigurationManager.AppSettings["webmail"].ToString(), emp.ManagerEmailID, "", emp.AccountManagerEmailID, sb.ToString(), " Leave request submitted by " + emp.FirstName + " " + emp.LastName + " (" + emp.Employee_ID + ") ", "", "Iwjil");
                    }
                    else if (emp.FK_UserGroup == 2)
                    {
                        mail.GDSSendBulkeMail(System.Configuration.ConfigurationManager.AppSettings["webmail"].ToString(), emp.AccountManagerEmailID, "", "", sb.ToString(), " Leave request submitted by " + emp.FirstName + " " + emp.LastName + " (" + emp.Employee_ID + ") ", "", "Iwjil");
                    }
                    else if (emp.FK_UserGroup == 4)
                    {
                        mail.GDSSendBulkeMail(System.Configuration.ConfigurationManager.AppSettings["webmail"].ToString(), emp.AccountManagerEmailID, "", "", sb.ToString(), " Leave request submitted by " + emp.FirstName + " " + emp.LastName + " (" + emp.Employee_ID + ") ", "", "Iwjil");
                    }
                    else
                    {

                        mail.GDSSendBulkeMail(System.Configuration.ConfigurationManager.AppSettings["webmail"].ToString(), emp.CompanyMailID, "", "", sb.ToString(), " Leave request submitted by " + emp.FirstName + " " + emp.LastName + " (" + emp.Employee_ID + ") ", "", "Iwjil");

                    }

                    StringBuilder Spend = new StringBuilder();

                    Spend.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>Universal Surgicals</title></head><body style=\"background-color:#e7e8ea;\"> <div style=\"max-width:800px; width:100%; margin:0px auto;\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr bgcolor=\"#333645\" style=\"padding: 30px;\"><td height=\"150px;\" style=\"padding-left:30px;\"> <img src=\"http://universal.globaldatasolutions.com/Content/Images/login-logo.png\"></td></tr><tr bgcolor=\"#FFFFFF\"><td  style=\"font-family:'Open Sans';padding:40px;\"><div style=\"width:100%; font-size:18px; font-weight: bold; font-weight:800px; color:#3dbeba;\">  Dear " + emp.FirstName + " " + emp.LastName + ",</div><div style=\"color:#868686; font-size:13px; margin-top:8px;\">Your Leave request with the following details has been successfully submitted to your Manager; you will get an update once any action taken by your Manager;</div><div style=\"border-radius: 5px; border:  solid 1px #e7e8ea; padding:20px;  font-size:14px;  font-weight: bold; color:#868686; margin-top:20px;\"><table width=\"100%\"><tr><td style=\"font-size:13px;  font-weight: normal; color:#868686;\">From Date: <span style=\"color:#3dbeba;\"> " + Leave.LeaveFrom.Value.ToShortDateString() + "	</span> </td><td style=\"font-size:13px;  font-weight: normal; color:#868686;\">To Date: <span style=\"color:#3dbeba;\">" + Leave.LeaveTo.Value.ToShortDateString() + "</span></td><td style=\"font-size:13px;  font-weight: normal; color:#868686;\">Type of Leave: <span style=\"color:#3dbeba;\">" + Leave.LeaveType + " </span></td><td style=\"font-size:13px;  font-weight: normal; color:#868686;\">Reason (Message/comments): <span style=\"color:#3dbeba;text-transform: capitalize;\">" + Leave.LeaveComment + " </span></td></tr></table></div>");
                    Spend.Append("<div style=\"color:#868686; font-size:13px; margin-top:15px; color:#3dbeba; float: right;  font-weight: 700; text-align: center;\"><b>Thanks,</b><br/><b>Expense Management Support !</b><br/><b>Universal Surgicals</b></div>");
                    Spend.Append("</td></tr></table></div></body></html>");

                    mail.GDSSendBulkeMail(System.Configuration.ConfigurationManager.AppSettings["webmail"].ToString(), emp.CompanyMailID, "", "", Spend.ToString(), "Leave request submission acknowledgement !!", "", "Iwjil");
                    LogFiles logfiles = new LogFiles();
                    logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "You have successfully submitted Leave Request");
                    return Json(new { Available = true, Text = "You have successfully submitted your Leave Request!!!" });
                }
                else
                {
                    LogFiles logfiles = new LogFiles();
                    logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "not submitted Leave Request");
                    return Json(new { Available = false, Text = "Your request not submitted" });
                }

            }

            else

                return Json(new { Available = false, Text = "Your request not submitted" });
        }

        public ActionResult CheckExpenditure(string ExpenditureDate, string MobileCheck, string InternetCheck, string FromPage)
        {
            object expenditureDateInLeave = new object();
            object ExpeseDate = new object();
            object Result = new object();
            object expenditureDateInTraining = new object();
            object ExpendituresInBlockedList = new object();
            object MobileCheckInExpenditure = new object();
            object InternetCheckInExpenditure = new object();
            object ExpendituresInTravelDesk = new object();

            int SpanOneBlockedDates = 0;
            int SpanTwoBlockedDates = 0;
            int SpanThreeBlockedDates = 0;

            ExpenseBlockedDatesList objBlockedList = new ExpenseBlockedDatesList();
            objBlockedList = userBL.ExpenseBlockedDatesList();
            var objEmployeeBlockedList = objBlockedList.ToList();

            DateTime ExpenditureDate1 = DateTime.Parse(ExpenditureDate);

            int NoofDaysInPreviousMonth = DateTime.DaysInMonth(ExpenditureDate1.Year, ExpenditureDate1.Month);

            Employees emp = new Employees();
            emp = userBL.GetEmployeeByID(Session["Employee"].ToString(), null);

            objEmployeeBlockedList = (from blocList in objEmployeeBlockedList.Where(s => s.EmpId == emp.Id && s.BlockingFromDate.Month == ExpenditureDate1.Month && s.BlockingFromDate.Year == ExpenditureDate1.Year && s.Status == "Unblocked") select blocList).ToList();

            for (int i = 0; i < objEmployeeBlockedList.Count; i++)
            {
                if (objEmployeeBlockedList[i].BlockingFromDate.Day > 0 && objEmployeeBlockedList[i].BlockingToDate.Day < 11)
                {
                    SpanOneBlockedDates = 1;
                }
                else if (objEmployeeBlockedList[i].BlockingFromDate.Day > 10 && objEmployeeBlockedList[i].BlockingToDate.Day < 21)
                {
                    SpanTwoBlockedDates = 1;
                }
                else if (objEmployeeBlockedList[i].BlockingFromDate.Day > 20 && objEmployeeBlockedList[i].BlockingToDate.Day == NoofDaysInPreviousMonth)
                {
                    SpanThreeBlockedDates = 1;
                }
            }

            expenditureDateInLeave = userBL.CheckExpenditureDateInLeave(ExpenditureDate1, emp.Id);
            ExpeseDate = userBL.CheckExpenditureDate(ExpenditureDate1, emp.Id);
            expenditureDateInTraining = userBL.CheckExpenditureDateInTraining(ExpenditureDate1, emp.Id);
            ExpendituresInBlockedList = userBL.CheckExpenditureDateInBlockedList(ExpenditureDate1, emp.Id);
            ExpendituresInTravelDesk = userBL.CheckExpenditureDateInTravelDesk(ExpenditureDate1, emp.Id);

            if (FromPage == "Expense" && Convert.ToInt32(expenditureDateInTraining) > 0)
            {
                expenditureDateInTraining = 1;
            }
            else if (FromPage == "Expense" && Convert.ToInt32(ExpendituresInTravelDesk) > 0)
            {
                expenditureDateInTraining = 0;
            }

            if (MobileCheck != null && MobileCheck.Length > 0)
            {
                MobileCheckInExpenditure = userBL.CheckMobileExpenditure(ExpenditureDate1, emp.Id);

                if (Convert.ToInt32(MobileCheckInExpenditure) > 0)
                {
                    MobileCheckInExpenditure = 1;
                }

            }

            if (InternetCheck != null && InternetCheck.Length > 0)
            {
                InternetCheckInExpenditure = userBL.CheckInternetExpenditure(ExpenditureDate1, emp.Id);

                if (Convert.ToInt32(InternetCheckInExpenditure) > 0)
                {
                    InternetCheckInExpenditure = 1;
                }
            }

            if (object.Equals(expenditureDateInLeave, 0))
            {
                expenditureDateInLeave = 0;
            }

            if (object.Equals(ExpeseDate, 0))
            {
                ExpeseDate = 0;
            }

            if (object.Equals(expenditureDateInTraining, 0))
            {
                expenditureDateInTraining = 0;
            }

            if (object.Equals(ExpendituresInBlockedList, 0))
            {
                ExpendituresInBlockedList = 0;
            }
            else
            {
                ExpendituresInBlockedList = 1;
            }

            if (object.Equals(InternetCheckInExpenditure, 0))
            {
                InternetCheckInExpenditure = 0;
            }

            if (object.Equals(MobileCheckInExpenditure, 0))
            {
                MobileCheckInExpenditure = 0;
            }

            return Json(Result = new { expenditureDateInLeave, ExpeseDate, expenditureDateInTraining, ExpendituresInBlockedList, MobileCheckInExpenditure, InternetCheckInExpenditure, SpanOneBlockedDates, SpanTwoBlockedDates, SpanThreeBlockedDates }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LocalExpenditure(string ExpenseDate, string ClientName, string ServiceOrderNumber, string OrderConfirmationNo, string From, string To, string ExpenditureType, string kms, string Conveyance, string amount, string SOExplanation, string ExpenditureTypeDetail, string Localization, string ExpenditureHeaderID, string ClientManagerName, string ReasonForOther, string Comments, string Mobile, string Internet, string InternetID, string MobileTypeID, string SerialNoInstrument, string OrderConfirmationDate, string MobileNo, string MobileComment, string InternetNo, string InternetComment)
        {
            UserExpenses_Header ObjUserExpenseHeader = new UserExpenses_Header();
            UserExpense_Details ObjeUserExpenseDetails = new UserExpense_Details();

            UserExpenseDetailsList dtExpenseDetails = new UserExpenseDetailsList();

            DataTable dtExpenditureType = new DataTable();
            DataTable dtExpenditureTypeDetails = new DataTable();
            DataTable dtExpenditureDetailsByID = new DataTable();

            Employees emp = new Employees();
            emp = userBL.GetEmployeeByID(Session["Employee"].ToString(), null);

            int? ExpenseHeaderID = 0;

            int? detailID = 0;

            if (ExpenditureHeaderID == string.Empty)
            {
                ExpenseHeaderID = 0;
            }
            else
            {
                ExpenseHeaderID = Convert.ToInt32(ExpenditureHeaderID);
            }

            dtExpenditureType = userBL.GetExpenditureTypes();
            dtExpenditureTypeDetails = userBL.GetExpenditureTypeDetails();

            for (int i = 0; i < dtExpenditureTypeDetails.Rows.Count; i++)
            {
                if (dtExpenditureTypeDetails.Rows[i]["Description"].ToString().Contains(ExpenditureType))
                {
                    ObjeUserExpenseDetails.FK_ExpenseTypeDetails = Convert.ToInt32(dtExpenditureTypeDetails.Rows[i]["ID"].ToString());
                    ObjeUserExpenseDetails.FK_ExpenseType = Convert.ToInt32(dtExpenditureTypeDetails.Rows[i]["FK_ExpenseType"].ToString());
                }
            }

            #region assign values to properties for header
            //header
            ObjUserExpenseHeader.FK_EmployeeId = emp.Id;
            if (Localization == "Domestic" || Localization == "International")
            {
                ObjUserExpenseHeader.UserExpenseType = "Training";
            }
            else
            {
                ObjUserExpenseHeader.UserExpenseType = "Expenditure";
            }
            ObjUserExpenseHeader.Localization = Localization;
            ObjUserExpenseHeader.FromDate = Convert.ToDateTime(ExpenseDate);
            ObjUserExpenseHeader.ToDate = Convert.ToDateTime(ExpenseDate);
            ObjUserExpenseHeader.ExpenseSheetName = ExpenseDate + "_" + ObjUserExpenseHeader.Localization;
            ObjUserExpenseHeader.ClientName = ClientName.ToString().Trim();
            ObjUserExpenseHeader.ClientManagerName = ClientManagerName.ToString().Trim();
            ObjUserExpenseHeader.ServiceOrderNo = ServiceOrderNumber;
            ObjUserExpenseHeader.ServiceOrderConfirmationNo = OrderConfirmationNo;
            ObjUserExpenseHeader.ExplanationforSONo = SOExplanation;
            ObjeUserExpenseDetails.CommentsforExpenseType = Comments;

            if (OrderConfirmationDate != null)
            {
                ObjUserExpenseHeader.ServiceOrderConfirmationDate = Convert.ToDateTime(OrderConfirmationDate);
            }
            else
            {
                ObjUserExpenseHeader.ServiceOrderConfirmationDate = DateTime.Now;
            }


            ObjUserExpenseHeader.SerialNoforInstrument = SerialNoInstrument;
            #endregion

            #region assign values to properties for details

            if (From != null)
            {
                ObjeUserExpenseDetails.FromLocation = From.ToString().Trim();
            }

            if (To != null)
            {
                ObjeUserExpenseDetails.ToLocation = To.ToString().Trim();
            }
            if (kms != string.Empty)
            {
                ObjeUserExpenseDetails.TraveledKms = Convert.ToDecimal(kms);
            }
            if (Conveyance != string.Empty)
            {
                ObjeUserExpenseDetails.Conveyance = Convert.ToDecimal(Conveyance);
            }
            if (ReasonForOther != string.Empty)
            {
                ObjeUserExpenseDetails.ReasonForOther = ReasonForOther;
            }
            if (amount != string.Empty)
            {
                ObjeUserExpenseDetails.Amount = Convert.ToDecimal(amount);
            }

            if (detailID == 0)
            {
                ObjeUserExpenseDetails.InvoiceNo = "INV_" + emp.Employee_ID + "_" + DateTime.Now.ToString().Replace("/", "") + "_" + ExpenditureType;
            }
            else
            {
                ObjeUserExpenseDetails.InvoiceNo = "INV_" + emp.Employee_ID + "_" + DateTime.Now.ToString().Replace("/", "") + "_" + ExpenditureType;
            }
            #endregion

            if (ExpenseHeaderID == 0)
            {
                if (ObjeUserExpenseDetails.FK_ExpenseTypeDetails > 0)
                {
                    ExpenseHeaderID = userBL.CreateExpenditure(ObjUserExpenseHeader, ObjeUserExpenseDetails, emp, out detailID);
                }
                else
                {
                    if (Mobile.Length > 0)
                    {
                        #region assign values to properties for details when from Mobile
                        if (Mobile.Length > 0)
                        {
                            UserExpense_Details objMobileExpense = new UserExpense_Details();
                            ObjUserExpenseHeader.Id = Convert.ToInt32(ExpenseHeaderID);
                            for (int i = 0; i < dtExpenditureTypeDetails.Rows.Count; i++)
                            {
                                if (dtExpenditureTypeDetails.Rows[i]["Description"].ToString().Contains(MobileTypeID))
                                {
                                    objMobileExpense.FK_ExpenseTypeDetails = Convert.ToInt32(dtExpenditureTypeDetails.Rows[i]["ID"].ToString());
                                    objMobileExpense.FK_ExpenseType = Convert.ToInt32(dtExpenditureTypeDetails.Rows[i]["FK_ExpenseType"].ToString());
                                }
                            }
                            if (Mobile != null)
                            {
                                objMobileExpense.Amount = Convert.ToDecimal(Mobile);
                                objMobileExpense.MobileNo = MobileNo;
                                objMobileExpense.CommentsforExpenseType = MobileComment;
                                objMobileExpense.InvoiceNo = "INV_" + emp.Employee_ID + "_" + DateTime.Now.ToString().Replace("/", "") + "_" + MobileTypeID;
                            }
                            ExpenseHeaderID = userBL.CreateExpenditure(ObjUserExpenseHeader, objMobileExpense, emp, out detailID);
                            Mobile = "";
                        }

                        #endregion
                    }
                    else if (Internet.Length > 0)
                    {
                        #region assign values to properties for details when from Internet
                        if (Internet.Length > 0)
                        {
                            UserExpense_Details objInternetBill = new UserExpense_Details();
                            ObjUserExpenseHeader.Id = Convert.ToInt32(ExpenseHeaderID);
                            for (int i = 0; i < dtExpenditureTypeDetails.Rows.Count; i++)
                            {
                                if (dtExpenditureTypeDetails.Rows[i]["Description"].ToString().Contains(InternetID))
                                {
                                    objInternetBill.FK_ExpenseTypeDetails = Convert.ToInt32(dtExpenditureTypeDetails.Rows[i]["ID"].ToString());
                                    objInternetBill.FK_ExpenseType = Convert.ToInt32(dtExpenditureTypeDetails.Rows[i]["FK_ExpenseType"].ToString());
                                }
                            }
                            if (Internet != null)
                            {
                                objInternetBill.Amount = Convert.ToDecimal(Internet);
                                objInternetBill.MobileNo = InternetNo;
                                objInternetBill.CommentsforExpenseType = InternetComment;
                                objInternetBill.InvoiceNo = "INV_" + emp.Employee_ID + "_" + DateTime.Now.ToString().Replace("/", "") + "_" + InternetID;
                            }

                            if (ExpenseHeaderID > 0)
                            {
                                int? res = userBL.CreateExpenditureDetails(ObjUserExpenseHeader, objInternetBill, emp, out detailID);
                                Internet = "";
                            }
                            else
                            {
                                ExpenseHeaderID = userBL.CreateExpenditure(ObjUserExpenseHeader, objInternetBill, emp, out detailID);
                                Internet = "";
                            }
                        }
                        #endregion
                    }
                }

                if (ExpenseHeaderID > 0)
                {
                    #region assign values to properties for details when from Mobile
                    if (Mobile != null)
                    {
                        if (Mobile.Length > 0)
                        {
                            UserExpense_Details objMobileExpense = new UserExpense_Details();
                            ObjUserExpenseHeader.Id = Convert.ToInt32(ExpenseHeaderID);
                            for (int i = 0; i < dtExpenditureTypeDetails.Rows.Count; i++)
                            {
                                if (dtExpenditureTypeDetails.Rows[i]["Description"].ToString().Contains(MobileTypeID))
                                {
                                    objMobileExpense.FK_ExpenseTypeDetails = Convert.ToInt32(dtExpenditureTypeDetails.Rows[i]["ID"].ToString());
                                    objMobileExpense.FK_ExpenseType = Convert.ToInt32(dtExpenditureTypeDetails.Rows[i]["FK_ExpenseType"].ToString());
                                }
                            }
                            if (Mobile != null)
                            {
                                objMobileExpense.Amount = Convert.ToDecimal(Mobile);
                                objMobileExpense.MobileNo = MobileNo;
                                objMobileExpense.CommentsforExpenseType = MobileComment;
                                objMobileExpense.InvoiceNo = "INV_" + emp.Employee_ID + "_" + DateTime.Now.ToString().Replace("/", "") + "_" + MobileTypeID;
                            }
                            int? res = userBL.CreateExpenditureDetails(ObjUserExpenseHeader, objMobileExpense, emp, out detailID);
                        }
                    }

                    #endregion

                    #region assign values to properties for details when from Internet

                    if (Internet != null)
                    {
                        if (Internet.Length > 0)
                        {
                            UserExpense_Details objInternetBill = new UserExpense_Details();
                            ObjUserExpenseHeader.Id = Convert.ToInt32(ExpenseHeaderID);
                            for (int i = 0; i < dtExpenditureTypeDetails.Rows.Count; i++)
                            {
                                if (dtExpenditureTypeDetails.Rows[i]["Description"].ToString().Contains(InternetID))
                                {
                                    objInternetBill.FK_ExpenseTypeDetails = Convert.ToInt32(dtExpenditureTypeDetails.Rows[i]["ID"].ToString());
                                    objInternetBill.FK_ExpenseType = Convert.ToInt32(dtExpenditureTypeDetails.Rows[i]["FK_ExpenseType"].ToString());
                                }
                            }
                            if (Internet != null)
                            {
                                objInternetBill.Amount = Convert.ToDecimal(Internet);
                                objInternetBill.MobileNo = InternetNo;
                                objInternetBill.CommentsforExpenseType = InternetComment;
                                objInternetBill.InvoiceNo = "INV_" + emp.Employee_ID + "_" + DateTime.Now.ToString().Replace("/", "") + "_" + InternetID;
                            }

                            int? res = userBL.CreateExpenditureDetails(ObjUserExpenseHeader, objInternetBill, emp, out detailID);
                        }
                    }
                    #endregion
                }
            }
            else
            {
                if (ObjeUserExpenseDetails.FK_ExpenseTypeDetails > 0)
                {
                    ObjUserExpenseHeader.Id = Convert.ToInt32(ExpenseHeaderID);
                    int? res = userBL.CreateExpenditureDetails(ObjUserExpenseHeader, ObjeUserExpenseDetails, emp, out detailID);
                }

                #region assign values to properties for details when from Mobile
                if (!(string.IsNullOrEmpty(Mobile)))
                {
                    if (Mobile.Length > 0)
                    {
                        UserExpense_Details objMobileExpense = new UserExpense_Details();
                        ObjUserExpenseHeader.Id = Convert.ToInt32(ExpenseHeaderID);
                        for (int i = 0; i < dtExpenditureTypeDetails.Rows.Count; i++)
                        {
                            if (dtExpenditureTypeDetails.Rows[i]["Description"].ToString().Contains(MobileTypeID))
                            {
                                objMobileExpense.FK_ExpenseTypeDetails = Convert.ToInt32(dtExpenditureTypeDetails.Rows[i]["ID"].ToString());
                                objMobileExpense.FK_ExpenseType = Convert.ToInt32(dtExpenditureTypeDetails.Rows[i]["FK_ExpenseType"].ToString());
                            }
                        }
                        if (Mobile != null)
                        {
                            objMobileExpense.Amount = Convert.ToDecimal(Mobile);
                            objMobileExpense.MobileNo = MobileNo;
                            objMobileExpense.CommentsforExpenseType = MobileComment;
                            objMobileExpense.InvoiceNo = "INV_" + emp.Employee_ID + "_" + DateTime.Now.ToString().Replace("/", "") + "_" + MobileTypeID;
                        }
                        int? res1 = userBL.CreateExpenditureDetails(ObjUserExpenseHeader, objMobileExpense, emp, out detailID);
                    }
                }
                #endregion

                #region assign values to properties for details when from Internet
                if (!(string.IsNullOrEmpty(Internet)))
                {
                    if (Internet.Length > 0)
                    {
                        UserExpense_Details objInternetBill = new UserExpense_Details();
                        ObjUserExpenseHeader.Id = Convert.ToInt32(ExpenseHeaderID);
                        for (int i = 0; i < dtExpenditureTypeDetails.Rows.Count; i++)
                        {
                            if (dtExpenditureTypeDetails.Rows[i]["Description"].ToString().Contains(InternetID))
                            {
                                objInternetBill.FK_ExpenseTypeDetails = Convert.ToInt32(dtExpenditureTypeDetails.Rows[i]["ID"].ToString());
                                objInternetBill.FK_ExpenseType = Convert.ToInt32(dtExpenditureTypeDetails.Rows[i]["FK_ExpenseType"].ToString());
                            }
                        }
                        if (Internet != null)
                        {
                            objInternetBill.Amount = Convert.ToDecimal(Internet);
                            objInternetBill.MobileNo = InternetNo;
                            objInternetBill.CommentsforExpenseType = InternetComment;
                            objInternetBill.InvoiceNo = "INV_" + emp.Employee_ID + "_" + DateTime.Now.ToString().Replace("/", "") + "_" + InternetID;
                        }

                        int? res2 = userBL.CreateExpenditureDetails(ObjUserExpenseHeader, objInternetBill, emp, out detailID);
                    }
                }
                #endregion
            }

            return Json(ExpenseHeaderID, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ExpenseListByID(string Data)
        {
            UserExpenseDetailsList useExpenseDetailsList = new UserExpenseDetailsList();
            UserExpenses_Header userexheader = new UserExpenses_Header();
            if (Data != string.Empty)
            {
                useExpenseDetailsList = userBL.GetExpenditureDetailsByID(Convert.ToInt32(Data));
                userexheader.Id = Convert.ToInt32(Data);

                userexheader.UserExpenseDetailsList = useExpenseDetailsList;

                return PartialView("ExpenseListByID", userexheader);
            }
            else
            {
                return PartialView("ExpenseListByID", useExpenseDetailsList);
            }
        }

        public ActionResult ExpenditureListClose(string HeaderID, string SONo, string SOExplanation, string FromStatus, string Comments, string SerialNoInstrument, string OrderConfirmationNo, string OrderConfirmationDate)
        {
            Boolean status;

            if (FromStatus == "false")
            {
                status = false;
            }
            else
            {
                status = true;
            }

            int ExpenseHeaderID = Convert.ToInt32(HeaderID);

            int? res = userBL.ExpenditureSubmit(status, ExpenseHeaderID, SONo, SOExplanation, SerialNoInstrument, OrderConfirmationNo, OrderConfirmationDate);

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DraftedExpenditure(int? Page, int? FromHeaderID)
        {
            Session["url"] = "/Employee/DraftedExpenditure";
            int pageSize = 7;
            int pageNumber = (Page ?? 1);
            string EmployeeID = Session["Employee"].ToString();

            Employees emp = new Employees();
            emp = userBL.GetEmployeeByID(Session["Employee"].ToString(), null);

            UserExpensesHeaderList userExpenditureHeaderList = new UserExpensesHeaderList();

            userExpenditureHeaderList = userBL.GetDraftedExpenditure(emp.Id);

            if (FromHeaderID > 0)
            {
                var userFromHeaderIDList = (from userFromHeaderList in userExpenditureHeaderList where userFromHeaderList.Id == FromHeaderID select userFromHeaderList).ToList();

                return Json(userFromHeaderIDList, JsonRequestBehavior.AllowGet);
            }
            else
            {

                var userExpenseHeader = (from userExpenseHeader1 in userExpenditureHeaderList select userExpenseHeader1).ToList();

                return View("DraftedExpenditure", userExpenseHeader.ToPagedList(pageNumber, pageSize));
            }
        }

        public ActionResult ExpenditureTypeDetailsByID(int ExpenseTypeHeaderID)
        {
            ExpenseTypeDetailsList expensetypeList = new ExpenseTypeDetailsList();

            expensetypeList = userBL.GetExpenditureTypeDetailsByID(ExpenseTypeHeaderID);

            var ExpenseTypeDetailsByID = expensetypeList;

            return Json(expensetypeList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult UpdateOrderConfirm(int? Page, int? FormHeaderID)
        {
            Session["url"] = "/Employee/UpdateOrderConfirm";
            int pageSize = 7;
            int pageNumber = (Page ?? 1);
            string EmployeeID = Session["Employee"].ToString();

            Employees emp = new Employees();
            emp = userBL.GetEmployeeByID(Session["Employee"].ToString(), null);

            UserExpensesHeaderList userExpenditureHeaderList = new UserExpensesHeaderList();

            userExpenditureHeaderList = userBL.GetInstrumentNoExpenditures(emp.Id);

            var userExpenseHeader = (from userExpenseHeader1 in userExpenditureHeaderList select userExpenseHeader1).ToList();

            return View("UpdateOrderConfirm", userExpenseHeader.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult OrderConfirmationNo(string ExpenseHeaderID, string ServiceOrderNo, string OrderConfirmationNo, string OrderConfirmatoinDate, string FK_HeaderID)
        {
            UserExpenses_Header objUserExpenseHeader = new UserExpenses_Header();
            ServiceOrderNos objserviceOrderNos = new ServiceOrderNos();

            if (Convert.ToInt32(FK_HeaderID) > 0)
            {
                #region from multiple SoNos
                objserviceOrderNos.Id = Convert.ToInt32(ExpenseHeaderID);
                objserviceOrderNos.FK_UserExpense_Header_Id = Convert.ToInt32(FK_HeaderID);
                objserviceOrderNos.ServiceOrderNo = ServiceOrderNo;
                objserviceOrderNos.ServiceOrderConfirmationNo = OrderConfirmationNo;
                objserviceOrderNos.ServiceOrderConfirmationDate = OrderConfirmatoinDate != null && OrderConfirmatoinDate.Length > 0 ? Convert.ToDateTime(OrderConfirmatoinDate) : DateTime.Now;

                int? updateRes = userBL.UpdateMultipleSONOs(objserviceOrderNos);

                return Json(new { Available = "Update Successfully", Text = "Update Successfully" });
                #endregion
            }
            else
            {
                #region from single SONo
                objUserExpenseHeader.Id = Convert.ToInt32(ExpenseHeaderID);
                objUserExpenseHeader.ServiceOrderNo = ServiceOrderNo;
                objUserExpenseHeader.ServiceOrderConfirmationNo = OrderConfirmationNo;
                objUserExpenseHeader.ServiceOrderConfirmationDate = Convert.ToDateTime(OrderConfirmatoinDate);

                int? Res = userBL.UpdateOrderConfirmationNo(objUserExpenseHeader);

                return Json(new { Available = "Update Successfully", Text = "Update Successfully" });
                #endregion
            }
        }

        [HttpPost]
        public ActionResult MultipleSONos(string Data, string SONOID, string SOType, string SONO, string SOCNO, string SOCDate, string Explanation, string InstrumentNo)
        {
            ServiceOrderNos_List objSONosList = new ServiceOrderNos_List();

            if (Data != string.Empty)
            {
                if (SOType == "DELETE")
                {
                    int? res = userBL.DeleteMultipleSONOs(SONOID);

                    objSONosList = userBL.GetMultipleSONOsbyExpenseHeaderID(Convert.ToInt32(Data));
                    return PartialView("MultipleSONos", objSONosList);
                }
                else if (SOType == "EDIT")
                {
                    ServiceOrderNos objserviceOrderNos = new ServiceOrderNos();

                    objserviceOrderNos.Id = Convert.ToInt32(SONOID);
                    objserviceOrderNos.FK_UserExpense_Header_Id = Convert.ToInt32(Data);
                    objserviceOrderNos.ServiceOrderNo = SONO;
                    objserviceOrderNos.ServiceOrderConfirmationNo = SOCNO;
                    objserviceOrderNos.ServiceOrderConfirmationDate = SOCDate != null && SOCDate.Length > 0 ? Convert.ToDateTime(SOCDate) : DateTime.Now;
                    objserviceOrderNos.ExplanationforSONo = Explanation;
                    objserviceOrderNos.SerialNoforInstrument = InstrumentNo;

                    int updateRes = userBL.UpdateMultipleSONOs(objserviceOrderNos);

                    objSONosList = userBL.GetMultipleSONOsbyExpenseHeaderID(Convert.ToInt32(Data));
                    return PartialView("MultipleSONos", objSONosList);
                }
                else
                {
                    objSONosList = userBL.GetMultipleSONOsbyExpenseHeaderID(Convert.ToInt32(Data));
                    return PartialView("MultipleSONos", objSONosList);
                }
            }
            else
            {
                return PartialView("MultipleSONos", objSONosList);
            }
        }

        public ActionResult MultipleServiceOrders(string FormData, string ExpenseDate, string ClientName, string ClientManagerName, string From, string To, string ExpenditureType, string kms, string amount, string ExpenditureTypeDetail, string Conveyance, string Localization, string ExpenditureHeaderID, string ReasonForOther, string Comments)
        {
            #region ObjectCreationsForEntities
            ServiceOrderNos objServiceOrderNos = new ServiceOrderNos();
            List<ServiceOrderNos> objServiceOrderNosList = new List<ServiceOrderNos>();
            UserExpenses_Header ObjUserExpenseHeader = new UserExpenses_Header();
            UserExpense_Details ObjeUserExpenseDetails = new UserExpense_Details();
            UserExpenseDetailsList dtExpenseDetails = new UserExpenseDetailsList();
            DataTable dtExpenditureType = new DataTable();
            DataTable dtExpenditureTypeDetails = new DataTable();
            DataTable dtExpenditureDetailsByID = new DataTable();
            Employees emp = new Employees();
            #endregion

            emp = userBL.GetEmployeeByID(Session["Employee"].ToString(), null);

            int? ExpenseHeaderID = 0;
            int? detailID = 0;

            if (ExpenditureHeaderID == string.Empty)
            {
                ExpenseHeaderID = 0;
            }
            else
            {
                ExpenseHeaderID = Convert.ToInt32(ExpenditureHeaderID);
            }

            #region AssignvaluestoExpenditureTypes
            dtExpenditureType = userBL.GetExpenditureTypes();
            dtExpenditureTypeDetails = userBL.GetExpenditureTypeDetails();
            for (int i = 0; i < dtExpenditureTypeDetails.Rows.Count; i++)
            {
                if (dtExpenditureTypeDetails.Rows[i]["Description"].ToString().Contains(ExpenditureType))
                {
                    ObjeUserExpenseDetails.FK_ExpenseTypeDetails = Convert.ToInt32(dtExpenditureTypeDetails.Rows[i]["ID"].ToString());
                    ObjeUserExpenseDetails.FK_ExpenseType = Convert.ToInt32(dtExpenditureTypeDetails.Rows[i]["FK_ExpenseType"].ToString());
                }
            }
            #endregion

            #region assign values to properties for header
            //header
            ObjUserExpenseHeader.FK_EmployeeId = emp.Id;
            if (Localization == "Domestic" || Localization == "International")
            {
                ObjUserExpenseHeader.UserExpenseType = "Training";
            }
            else
            {
                ObjUserExpenseHeader.UserExpenseType = "Expenditure";
            }
            ObjUserExpenseHeader.Localization = Localization;
            ObjUserExpenseHeader.FromDate = Convert.ToDateTime(ExpenseDate);
            ObjUserExpenseHeader.ToDate = Convert.ToDateTime(ExpenseDate);
            ObjUserExpenseHeader.ExpenseSheetName = ExpenseDate + "_" + ObjUserExpenseHeader.Localization;
            ObjUserExpenseHeader.ClientName = ClientName.ToString().Trim();
            ObjUserExpenseHeader.ClientManagerName = ClientManagerName.ToString().Trim();
            ObjeUserExpenseDetails.CommentsforExpenseType = Comments;
            #endregion

            #region assign values to properties for details

            if (From != null)
            {
                ObjeUserExpenseDetails.FromLocation = From.ToString().Trim();
            }

            if (To != null)
            {
                ObjeUserExpenseDetails.ToLocation = To.ToString().Trim();
            }
            if (kms != string.Empty)
            {
                ObjeUserExpenseDetails.TraveledKms = Convert.ToDecimal(kms);
            }
            if (Conveyance != string.Empty)
            {
                ObjeUserExpenseDetails.Conveyance = Convert.ToDecimal(Conveyance);
            }
            if (ReasonForOther != string.Empty)
            {
                ObjeUserExpenseDetails.ReasonForOther = ReasonForOther;
            }
            if (amount != string.Empty)
            {
                ObjeUserExpenseDetails.Amount = Convert.ToDecimal(amount);
            }

            if (detailID == 0)
            {
                ObjeUserExpenseDetails.InvoiceNo = "INV_" + emp.Employee_ID + "_" + ExpenseDate.ToString().Replace("/", "") + "_" + ExpenditureType;
            }
            else
            {
                ObjeUserExpenseDetails.InvoiceNo = "INV_" + emp.Employee_ID + "_" + ExpenseDate.ToString().Replace("/", "") + "_" + ExpenditureType;
            }
            #endregion

            if (ExpenseHeaderID == 0)
            {
                if (ObjeUserExpenseDetails.FK_ExpenseTypeDetails > 0)
                {
                    ExpenseHeaderID = userBL.CreateExpenditure(ObjUserExpenseHeader, ObjeUserExpenseDetails, emp, out detailID);

                    if (ExpenseHeaderID > 0)
                    {
                        #region JSON DATA Split
                        var JsonData = FormData;
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        dynamic objArrayList = serializer.Deserialize<dynamic>(JsonData);

                        string st = "";
                        string s = "";
                        string ItemValue = "";

                        foreach (var values in objArrayList)
                        {
                            foreach (var item in values)
                            {
                                int count = 1;
                                foreach (var item1 in item)
                                {
                                    if (count == 1)
                                    {
                                        st = item1.Key;
                                        s = item1.Value;
                                    }
                                    else
                                    {
                                        ItemValue = item1.Value;
                                    }
                                    count++;
                                }

                                #region SoNos

                                if (s == "txtSONo1")
                                {
                                    objServiceOrderNos.ServiceOrderNo = ItemValue;
                                }

                                if (s == "txtSONo2")
                                {
                                    objServiceOrderNos.ServiceOrderNo = ItemValue;
                                }

                                if (s == "txtSONo3")
                                {
                                    objServiceOrderNos.ServiceOrderNo = ItemValue;
                                }

                                if (s == "txtSONo4")
                                {
                                    objServiceOrderNos.ServiceOrderNo = ItemValue;
                                }

                                if (s == "txtSONo5")
                                {
                                    objServiceOrderNos.ServiceOrderNo = ItemValue;
                                }

                                if (s == "txtSONo6")
                                {
                                    objServiceOrderNos.ServiceOrderNo = ItemValue;
                                }

                                if (s == "txtSONo7")
                                {
                                    objServiceOrderNos.ServiceOrderNo = ItemValue;
                                }

                                if (s == "txtSONo8")
                                {
                                    objServiceOrderNos.ServiceOrderNo = ItemValue;
                                }

                                if (s == "txtSONo9")
                                {
                                    objServiceOrderNos.ServiceOrderNo = ItemValue;
                                }

                                if (s == "txtSONo10")
                                {
                                    objServiceOrderNos.ServiceOrderNo = ItemValue;
                                }

                                #endregion

                                #region ServiceorderConfirmationNos

                                if (s == "SOConfirmationNo1")
                                {
                                    objServiceOrderNos.ServiceOrderConfirmationNo = ItemValue;
                                }

                                if (s == "SOConfirmationNo2")
                                {
                                    objServiceOrderNos.ServiceOrderConfirmationNo = ItemValue;
                                }

                                if (s == "SOConfirmationNo3")
                                {
                                    objServiceOrderNos.ServiceOrderConfirmationNo = ItemValue;
                                }

                                if (s == "SOConfirmationNo4")
                                {
                                    objServiceOrderNos.ServiceOrderConfirmationNo = ItemValue;
                                }

                                if (s == "SOConfirmationNo5")
                                {
                                    objServiceOrderNos.ServiceOrderConfirmationNo = ItemValue;
                                }

                                if (s == "SOConfirmationNo6")
                                {
                                    objServiceOrderNos.ServiceOrderConfirmationNo = ItemValue;
                                }

                                if (s == "SOConfirmationNo7")
                                {
                                    objServiceOrderNos.ServiceOrderConfirmationNo = ItemValue;
                                }

                                if (s == "SOConfirmationNo8")
                                {
                                    objServiceOrderNos.ServiceOrderConfirmationNo = ItemValue;
                                }

                                if (s == "SOConfirmationNo9")
                                {
                                    objServiceOrderNos.ServiceOrderConfirmationNo = ItemValue;
                                }

                                if (s == "SOConfirmationNo10")
                                {
                                    objServiceOrderNos.ServiceOrderConfirmationNo = ItemValue;
                                }

                                #endregion

                                #region ServiceConfirmationDate

                                if (s == "txtOCDate1")
                                {
                                    objServiceOrderNos.ServiceOrderConfirmationDate = ItemValue != null && ItemValue.Length > 0 ? Convert.ToDateTime(ItemValue) : DateTime.Now;
                                }

                                if (s == "txtOCDate2")
                                {
                                    objServiceOrderNos.ServiceOrderConfirmationDate = ItemValue != null && ItemValue.Length > 0 ? Convert.ToDateTime(ItemValue) : DateTime.Now;
                                }

                                if (s == "txtOCDate3")
                                {
                                    objServiceOrderNos.ServiceOrderConfirmationDate = ItemValue != null && ItemValue.Length > 0 ? Convert.ToDateTime(ItemValue) : DateTime.Now;
                                }

                                if (s == "txtOCDate4")
                                {
                                    objServiceOrderNos.ServiceOrderConfirmationDate = ItemValue != null && ItemValue.Length > 0 ? Convert.ToDateTime(ItemValue) : DateTime.Now;
                                }

                                if (s == "txtOCDate5")
                                {
                                    objServiceOrderNos.ServiceOrderConfirmationDate = ItemValue != null && ItemValue.Length > 0 ? Convert.ToDateTime(ItemValue) : DateTime.Now;
                                }

                                if (s == "txtOCDate6")
                                {
                                    objServiceOrderNos.ServiceOrderConfirmationDate = ItemValue != null && ItemValue.Length > 0 ? Convert.ToDateTime(ItemValue) : DateTime.Now;
                                }

                                if (s == "txtOCDate7")
                                {
                                    objServiceOrderNos.ServiceOrderConfirmationDate = ItemValue != null && ItemValue.Length > 0 ? Convert.ToDateTime(ItemValue) : DateTime.Now;
                                }

                                if (s == "txtOCDate8")
                                {
                                    objServiceOrderNos.ServiceOrderConfirmationDate = ItemValue != null && ItemValue.Length > 0 ? Convert.ToDateTime(ItemValue) : DateTime.Now;
                                }

                                if (s == "txtOCDate9")
                                {
                                    objServiceOrderNos.ServiceOrderConfirmationDate = ItemValue != null && ItemValue.Length > 0 ? Convert.ToDateTime(ItemValue) : DateTime.Now;
                                }

                                if (s == "txtOCDate10")
                                {
                                    objServiceOrderNos.ServiceOrderConfirmationDate = ItemValue != null && ItemValue.Length > 0 ? Convert.ToDateTime(ItemValue) : DateTime.Now;
                                }

                                #endregion

                                #region ExplanationForSONo

                                if (s == "txtSOExplanation1")
                                {
                                    objServiceOrderNos.ExplanationforSONo = ItemValue;
                                }

                                if (s == "txtSOExplanation2")
                                {
                                    objServiceOrderNos.ExplanationforSONo = ItemValue;
                                }

                                if (s == "txtSOExplanation3")
                                {
                                    objServiceOrderNos.ExplanationforSONo = ItemValue;
                                }

                                if (s == "txtSOExplanation4")
                                {
                                    objServiceOrderNos.ExplanationforSONo = ItemValue;
                                }

                                if (s == "txtSOExplanation5")
                                {
                                    objServiceOrderNos.ExplanationforSONo = ItemValue;
                                }

                                if (s == "txtSOExplanation6")
                                {
                                    objServiceOrderNos.ExplanationforSONo = ItemValue;
                                }

                                if (s == "txtSOExplanation7")
                                {
                                    objServiceOrderNos.ExplanationforSONo = ItemValue;
                                }

                                if (s == "txtSOExplanation8")
                                {
                                    objServiceOrderNos.ExplanationforSONo = ItemValue;
                                }

                                if (s == "txtSOExplanation9")
                                {
                                    objServiceOrderNos.ExplanationforSONo = ItemValue;
                                }

                                if (s == "txtSOExplanation10")
                                {
                                    objServiceOrderNos.ExplanationforSONo = ItemValue;
                                }

                                #endregion

                                #region InstrumentNo

                                if (s == "txtSONoforInstrument1")
                                {
                                    objServiceOrderNos.SerialNoforInstrument = ItemValue;
                                }

                                if (s == "txtSONoforInstrument2")
                                {
                                    objServiceOrderNos.SerialNoforInstrument = ItemValue;
                                }

                                if (s == "txtSONoforInstrument3")
                                {
                                    objServiceOrderNos.SerialNoforInstrument = ItemValue;
                                }

                                if (s == "txtSONoforInstrument4")
                                {
                                    objServiceOrderNos.SerialNoforInstrument = ItemValue;
                                }

                                if (s == "txtSONoforInstrument5")
                                {
                                    objServiceOrderNos.SerialNoforInstrument = ItemValue;
                                }

                                if (s == "txtSONoforInstrument6")
                                {
                                    objServiceOrderNos.SerialNoforInstrument = ItemValue;
                                }

                                if (s == "txtSONoforInstrument7")
                                {
                                    objServiceOrderNos.SerialNoforInstrument = ItemValue;
                                }

                                if (s == "txtSONoforInstrument8")
                                {
                                    objServiceOrderNos.SerialNoforInstrument = ItemValue;
                                }

                                if (s == "txtSONoforInstrument9")
                                {
                                    objServiceOrderNos.SerialNoforInstrument = ItemValue;
                                }

                                if (s == "txtSONoforInstrument10")
                                {
                                    objServiceOrderNos.SerialNoforInstrument = ItemValue;
                                }

                                #endregion

                            }
                            int? resMulti = userBL.InsertMultipleServiceOrderNos(ExpenseHeaderID, objServiceOrderNos, emp);
                        }

                        #endregion
                    }
                }
            }
            else
            {
                if (ObjeUserExpenseDetails.FK_ExpenseTypeDetails > 0)
                {
                    ObjUserExpenseHeader.Id = Convert.ToInt32(ExpenseHeaderID);
                    int? res = userBL.CreateExpenditureDetails(ObjUserExpenseHeader, ObjeUserExpenseDetails, emp, out detailID);

                    #region unwantend

                    ServiceOrderNos_List objSONosList = new ServiceOrderNos_List();

                    objSONosList = userBL.GetMultipleSONOsbyExpenseHeaderID(ExpenseHeaderID);


                    #region JSON DATA Split
                    var JsonData = FormData;
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    dynamic objArrayList = serializer.Deserialize<dynamic>(JsonData);

                    string st = "";
                    string s = "";
                    string ItemValue = "";

                    foreach (var values in objArrayList)
                    {
                        #region arraySplit
                        foreach (var item in values)
                        {
                            #region objectSplit
                            int count = 1;
                            foreach (var item1 in item)
                            {
                                if (count == 1)
                                {
                                    st = item1.Key;
                                    s = item1.Value;
                                }
                                else
                                {
                                    ItemValue = item1.Value;
                                }
                                count++;
                            }
                            #endregion

                            #region SoNos

                            if (s == "txtSONo1")
                            {
                                objServiceOrderNos.ServiceOrderNo = ItemValue;
                            }

                            if (s == "txtSONo2")
                            {
                                objServiceOrderNos.ServiceOrderNo = ItemValue;
                            }

                            if (s == "txtSONo3")
                            {
                                objServiceOrderNos.ServiceOrderNo = ItemValue;
                            }

                            if (s == "txtSONo4")
                            {
                                objServiceOrderNos.ServiceOrderNo = ItemValue;
                            }

                            if (s == "txtSONo5")
                            {
                                objServiceOrderNos.ServiceOrderNo = ItemValue;
                            }

                            if (s == "txtSONo6")
                            {
                                objServiceOrderNos.ServiceOrderNo = ItemValue;
                            }

                            if (s == "txtSONo7")
                            {
                                objServiceOrderNos.ServiceOrderNo = ItemValue;
                            }

                            if (s == "txtSONo8")
                            {
                                objServiceOrderNos.ServiceOrderNo = ItemValue;
                            }

                            if (s == "txtSONo9")
                            {
                                objServiceOrderNos.ServiceOrderNo = ItemValue;
                            }

                            if (s == "txtSONo10")
                            {
                                objServiceOrderNos.ServiceOrderNo = ItemValue;
                            }

                            #endregion

                            #region ServiceorderConfirmationNos

                            if (s == "SOConfirmationNo1")
                            {
                                objServiceOrderNos.ServiceOrderConfirmationNo = ItemValue;
                            }

                            if (s == "SOConfirmationNo2")
                            {
                                objServiceOrderNos.ServiceOrderConfirmationNo = ItemValue;
                            }

                            if (s == "SOConfirmationNo3")
                            {
                                objServiceOrderNos.ServiceOrderConfirmationNo = ItemValue;
                            }

                            if (s == "SOConfirmationNo4")
                            {
                                objServiceOrderNos.ServiceOrderConfirmationNo = ItemValue;
                            }

                            if (s == "SOConfirmationNo5")
                            {
                                objServiceOrderNos.ServiceOrderConfirmationNo = ItemValue;
                            }

                            if (s == "SOConfirmationNo6")
                            {
                                objServiceOrderNos.ServiceOrderConfirmationNo = ItemValue;
                            }

                            if (s == "SOConfirmationNo7")
                            {
                                objServiceOrderNos.ServiceOrderConfirmationNo = ItemValue;
                            }

                            if (s == "SOConfirmationNo8")
                            {
                                objServiceOrderNos.ServiceOrderConfirmationNo = ItemValue;
                            }

                            if (s == "SOConfirmationNo9")
                            {
                                objServiceOrderNos.ServiceOrderConfirmationNo = ItemValue;
                            }

                            if (s == "SOConfirmationNo10")
                            {
                                objServiceOrderNos.ServiceOrderConfirmationNo = ItemValue;
                            }

                            #endregion

                            #region ServiceConfirmationDate

                            if (s == "txtOCDate1")
                            {
                                objServiceOrderNos.ServiceOrderConfirmationDate = ItemValue != null && ItemValue.Length > 0 ? Convert.ToDateTime(ItemValue) : DateTime.Now;
                            }

                            if (s == "txtOCDate2")
                            {
                                objServiceOrderNos.ServiceOrderConfirmationDate = ItemValue != null && ItemValue.Length > 0 ? Convert.ToDateTime(ItemValue) : DateTime.Now;
                            }

                            if (s == "txtOCDate3")
                            {
                                objServiceOrderNos.ServiceOrderConfirmationDate = ItemValue != null && ItemValue.Length > 0 ? Convert.ToDateTime(ItemValue) : DateTime.Now;
                            }

                            if (s == "txtOCDate4")
                            {
                                objServiceOrderNos.ServiceOrderConfirmationDate = ItemValue != null && ItemValue.Length > 0 ? Convert.ToDateTime(ItemValue) : DateTime.Now;
                            }

                            if (s == "txtOCDate5")
                            {
                                objServiceOrderNos.ServiceOrderConfirmationDate = ItemValue != null && ItemValue.Length > 0 ? Convert.ToDateTime(ItemValue) : DateTime.Now;
                            }

                            if (s == "txtOCDate6")
                            {
                                objServiceOrderNos.ServiceOrderConfirmationDate = ItemValue != null && ItemValue.Length > 0 ? Convert.ToDateTime(ItemValue) : DateTime.Now;
                            }

                            if (s == "txtOCDate7")
                            {
                                objServiceOrderNos.ServiceOrderConfirmationDate = ItemValue != null && ItemValue.Length > 0 ? Convert.ToDateTime(ItemValue) : DateTime.Now;
                            }

                            if (s == "txtOCDate8")
                            {
                                objServiceOrderNos.ServiceOrderConfirmationDate = ItemValue != null && ItemValue.Length > 0 ? Convert.ToDateTime(ItemValue) : DateTime.Now;
                            }

                            if (s == "txtOCDate9")
                            {
                                objServiceOrderNos.ServiceOrderConfirmationDate = ItemValue != null && ItemValue.Length > 0 ? Convert.ToDateTime(ItemValue) : DateTime.Now;
                            }

                            if (s == "txtOCDate10")
                            {
                                objServiceOrderNos.ServiceOrderConfirmationDate = ItemValue != null && ItemValue.Length > 0 ? Convert.ToDateTime(ItemValue) : DateTime.Now;
                            }

                            #endregion

                            #region ExplanationForSONo

                            if (s == "txtSOExplanation1")
                            {
                                objServiceOrderNos.ExplanationforSONo = ItemValue;
                            }

                            if (s == "txtSOExplanation2")
                            {
                                objServiceOrderNos.ExplanationforSONo = ItemValue;
                            }

                            if (s == "txtSOExplanation3")
                            {
                                objServiceOrderNos.ExplanationforSONo = ItemValue;
                            }

                            if (s == "txtSOExplanation4")
                            {
                                objServiceOrderNos.ExplanationforSONo = ItemValue;
                            }

                            if (s == "txtSOExplanation5")
                            {
                                objServiceOrderNos.ExplanationforSONo = ItemValue;
                            }

                            if (s == "txtSOExplanation6")
                            {
                                objServiceOrderNos.ExplanationforSONo = ItemValue;
                            }

                            if (s == "txtSOExplanation7")
                            {
                                objServiceOrderNos.ExplanationforSONo = ItemValue;
                            }

                            if (s == "txtSOExplanation8")
                            {
                                objServiceOrderNos.ExplanationforSONo = ItemValue;
                            }

                            if (s == "txtSOExplanation9")
                            {
                                objServiceOrderNos.ExplanationforSONo = ItemValue;
                            }

                            if (s == "txtSOExplanation10")
                            {
                                objServiceOrderNos.ExplanationforSONo = ItemValue;
                            }

                            #endregion

                            #region InstrumentNo

                            if (s == "txtSONoforInstrument1")
                            {
                                objServiceOrderNos.SerialNoforInstrument = ItemValue;
                            }

                            if (s == "txtSONoforInstrument2")
                            {
                                objServiceOrderNos.SerialNoforInstrument = ItemValue;
                            }

                            if (s == "txtSONoforInstrument3")
                            {
                                objServiceOrderNos.SerialNoforInstrument = ItemValue;
                            }

                            if (s == "txtSONoforInstrument4")
                            {
                                objServiceOrderNos.SerialNoforInstrument = ItemValue;
                            }

                            if (s == "txtSONoforInstrument5")
                            {
                                objServiceOrderNos.SerialNoforInstrument = ItemValue;
                            }

                            if (s == "txtSONoforInstrument6")
                            {
                                objServiceOrderNos.SerialNoforInstrument = ItemValue;
                            }

                            if (s == "txtSONoforInstrument7")
                            {
                                objServiceOrderNos.SerialNoforInstrument = ItemValue;
                            }

                            if (s == "txtSONoforInstrument8")
                            {
                                objServiceOrderNos.SerialNoforInstrument = ItemValue;
                            }

                            if (s == "txtSONoforInstrument9")
                            {
                                objServiceOrderNos.SerialNoforInstrument = ItemValue;
                            }

                            if (s == "txtSONoforInstrument10")
                            {
                                objServiceOrderNos.SerialNoforInstrument = ItemValue;
                            }

                            #endregion

                        }
                        int? resMulti = userBL.InsertMultipleServiceOrderNos(ExpenseHeaderID, objServiceOrderNos, emp);
                        #endregion
                    }

                    #endregion
                    #endregion
                }
            }

            return Json(ExpenseHeaderID, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ExpenditureReport(int? Page, string SDate, string EDate)
        {
            #region objectCreations
            UserExpensesHeaderList userExpenseHeaderList = new UserExpensesHeaderList();
            UserExpensesHeaderList objUserExpenseList = new UserExpensesHeaderList();
            UserExpenseDetailsList useExpenseDetailsList = new UserExpenseDetailsList();
            ServiceOrderNos_List serviceorderNosList = new ServiceOrderNos_List();
            Employees emp = new Employees();
            #endregion

            int pageNumber;
            pageNumber = (Page ?? 1);
            DateTime ExpenditureSDate = Convert.ToDateTime(SDate);
            DateTime ExpenditureEDate = Convert.ToDateTime(EDate);
            var userExpenseHeader = objUserExpenseList;
            emp = userBL.GetEmployeeByID(Session["Employee"].ToString(), null);

            if (SDate != null && EDate != null)
            {
                userExpenseHeaderList = userBL.GetExpenditureReport(ExpenditureSDate, ExpenditureEDate, emp.Id);
            }

            for (int i = 0; i < userExpenseHeaderList.Count; i++)
            {
                useExpenseDetailsList = userBL.GetExpenditureDetailsByID(userExpenseHeaderList[i].Id);

                if (userExpenseHeaderList[i].ServiceOrderNo == null || userExpenseHeaderList[i].SerialNoforInstrument == null && (useExpenseDetailsList[0].ExpenseTypeDetails != "Mobile Bill" || useExpenseDetailsList[0].ExpenseTypeDetails != "Internet Bill"))
                {
                    serviceorderNosList = userBL.GetMultipleSONOsbyExpenseHeaderID(userExpenseHeaderList[i].Id);
                    userExpenseHeaderList[i].objServiceOrderNosList = serviceorderNosList;
                }

                userExpenseHeaderList[i].Id = userExpenseHeaderList[i].Id;
                userExpenseHeaderList[i].UserExpenseDetailsList = useExpenseDetailsList;
                objUserExpenseList.Add(userExpenseHeaderList[i]);

                ViewBag.profilepath = useExpenseDetailsList[0].ProfilePicturePath != null ? "../../Content/ProfileImg/" + useExpenseDetailsList[0].ProfilePicturePath : "../../Content/Images/prf-no-img.png";
                ViewBag.Fullname = useExpenseDetailsList[0].FullName.ToUpperInvariant();
                ViewBag.FromDate = SDate;
                ViewBag.ToDate = EDate;
                ViewBag.Zone = useExpenseDetailsList[0].Zone.ToString();
                userExpenseHeader = objUserExpenseList;
                ViewBag.TotalReportAmount = userExpenseHeaderList[0].TotalReportAmount;
                ViewBag.LumpsumAmount = userExpenseHeaderList[0].LumpsumAmount;
                ViewBag.AdvanceAmount = userExpenseHeaderList[0].TotalAdvanceAmount;

                ViewBag.CloseBalance = userExpenseHeaderList[0].TotalReportAmount + userExpenseHeaderList[0].LumpsumAmount - userExpenseHeaderList[0].TotalAdvanceAmount;

            }

            return View(userExpenseHeader.ToPagedList(pageNumber, userExpenseHeader.Count != 0 ? userExpenseHeader.Count : 1));
        }
        [HttpGet]
        public ActionResult SkillsMetrics(string Employeeid)
        {
            if (Employeeid == null && Session["Employee"] != null)
                Employeeid = Session["Employee"].ToString();
            Session["url"] = "/Employee/SkillsMetrics/" + Employeeid;
            UserBL userbl = new UserBL();
            SkillsMetrics skillsMetrics = new SkillsMetrics();
            skillsMetrics = userbl.GetSkillMetrics(Employeeid);
            return View(skillsMetrics);
        }

        public ActionResult GetSalaryDetails(string EmployeeId, string Month)
        {
            if ((EmployeeId == null || EmployeeId == "") && Session["Employee"] != null)
                EmployeeId = Session["Employee"].ToString();
            Session["url"] = "/Employee/GetSalaryDetails";
            UserBL userBL = new UserBL();
            PayStub paystub = new PayStub();

            var mon = 0;
            if (!(string.IsNullOrWhiteSpace(Month)))
            {
                mon = int.Parse(Month);
                ViewBag.Month = mon;
            }
            else
            {
                mon = DateTime.Now.Month-1;
                ViewBag.Month = mon;
            }

            if (mon < 4)
            {
                paystub.Flag = false;
                SalaryDetails salaryDetailsList = new SalaryDetails();
                salaryDetailsList = userBL.EmpGetSalaryDetailsForMonth(EmployeeId, mon);
                salaryDetailsList.TL = salaryDetailsList.CL + salaryDetailsList.SL;
                paystub.salaryDetails = salaryDetailsList;
            }
            else
            {
                paystub.Flag = true;
                SalaryDetails_NEW salaryDetails_NEW = new SalaryDetails_NEW();
                salaryDetails_NEW = userBL.EmpGetSalaryDetailsForMonthNewFormat(EmployeeId, mon);
                salaryDetails_NEW.TL = salaryDetails_NEW.CL + salaryDetails_NEW.SL;
                
                paystub.salaryDetails_NEW = salaryDetails_NEW;
            }

            return View("SalaryDetails", paystub);
        }
    }
}