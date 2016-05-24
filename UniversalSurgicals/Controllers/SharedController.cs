﻿using System;
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
using System.Globalization;
using System.Threading;
using System.IO;

namespace UniversalSurgicals.Controllers
{
    [OutputCacheAttribute(VaryByParam = "*", Duration = 1)] // will be applied to all actions in MyController, unless those actions override with their own decoration

    public class SharedController : SessionCheckingController
    {

        [HttpGet]
        public ActionResult RoleChange()
        {
            string sessionval = "";
            if (Session["AccountMgr"] != null)
                sessionval = Session["AccountMgr"].ToString();
            else if (Session["TravelManager"] != null)
                sessionval = Session["TravelManager"].ToString();
            else if (Session["Manager"] != null)
                sessionval = Session["Manager"].ToString();

            Session.Clear();
            Session.RemoveAll();
            
            Session["Employee"] = sessionval;
            return RedirectToAction("DashBoard", "Employee");
        }

        [HttpGet]
        public ActionResult RevertRole(int id)
        {
            string sessionval = "";
            if(Session["Employee"]!=null)
            sessionval = Session["Employee"].ToString();
            Session.Clear();
            Session.RemoveAll();
            if (id == 1)
            {
                Session["AccountMgr"] = sessionval;
                return Json(new { redirectTo = Url.Action("AdminDashBoard", "Admin"), Text = "success", }, JsonRequestBehavior.AllowGet);
            }
            else if (id == 2)
            {
                Session["Manager"] = sessionval;
                return Json(new { redirectTo = Url.Action("ManagerDashBoard", "Manager"), Text = "success", }, JsonRequestBehavior.AllowGet);
                          }
            else if (id == 4)
            {
                Session["TravelManager"] = sessionval;
                return Json(new { redirectTo = Url.Action("TravelDeskManagerDashBoard", "TravelDesk"), Text = "success", }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["Employee"] = sessionval;
                return Json(new { redirectTo = Url.Action("DashBoard", "Employee"), Text = "success", }, JsonRequestBehavior.AllowGet);
            }

            
        }

        [HttpGet]
        public ActionResult TravelDesk(string sortOrder, string currentFilter, string searchString, int? page)
        {
            Session["url"] = "/Shared/TravelDesk";

            UserBL userbl = new UserBL();
            TravelDeskList travelList = new TravelDeskList();
            travelList = userbl.GetTravelList();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";



            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (Session["Employee"] != null)
            {

                var tlist = (from s in travelList where s.EmployeeId == Session["Employee"].ToString() select s);


                if (!String.IsNullOrWhiteSpace(searchString))
                {
                    tlist = tlist.Where(s => s.EmployeeId.Trim().ToUpper().Contains(searchString.Trim().ToUpper()));
                }

                switch (sortOrder)
                {
                    case "name_desc":
                        tlist = tlist.OrderBy(s => s.TraveledFromDate);
                        break;

                    default:
                        tlist = tlist.OrderByDescending(s => s.TraveledFromDate);
                        break;
                }

                int pageSize = 7;
                int pageNumber = (page ?? 1);

                return PartialView("TravelDesk", tlist.ToPagedList(pageNumber, pageSize));
            }

            else if (Session["Manager"] != null)
            {

                Employees emp = new Employees();

                emp = userbl.GetEmployeeByID(Session["Manager"].ToString(), null);

                var tlist = (from s in travelList where s.ManagerId == emp.Id.ToString() select s);


                if (!String.IsNullOrWhiteSpace(searchString))
                {
                    tlist = tlist.Where(s => s.EmployeeId.Trim().ToUpper().Contains(searchString.Trim().ToUpper()));
                }

                switch (sortOrder)
                {
                    case "name_desc":
                        tlist = tlist.OrderBy(s => s.TraveledFromDate);
                        break;

                    default:
                        tlist = tlist.OrderByDescending(s => s.TraveledFromDate);
                        break;
                }

                int pageSize = 7;
                int pageNumber = (page ?? 1);

                return PartialView("TravelDesk", tlist.ToPagedList(pageNumber, pageSize));
            }


            else
            {

                var tlist = (from s in travelList select s);



                if (!String.IsNullOrWhiteSpace(searchString))
                {
                    tlist = tlist.Where(s => s.EmployeeId.Trim().ToUpper().Contains(searchString.Trim().ToUpper()));
                }

                switch (sortOrder)
                {
                    case "name_desc":
                        tlist = tlist.OrderBy(s => s.CreatedDate);
                        break;

                    default:
                        tlist = tlist.OrderByDescending(s => s.CreatedDate);
                        break;
                }

                int pageSize = 7;
                int pageNumber = (page ?? 1);

                return PartialView("TravelDesk", tlist.ToPagedList(pageNumber, pageSize));
            }

        }

        public ActionResult Leave(string sortOrder, string currentFilter, string searchString, int? page, string fromDate, string toDate)
        {
            Session["url"] = "/Shared/Leave";

            UserBL userbl = new UserBL();
            Universal.BusinessEntities.LeaveList empLeavesList = new Universal.BusinessEntities.LeaveList();


            UserBL userBL = new UserBL();
            Employees emp = new Employees();
            int? Employee_ID;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            int pageSize = 7;
            int pageNumber = (page ?? 1);

            if (Session["AccountMgr"] != null)
            {
                emp = userBL.GetEmployeeByID(Session["AccountMgr"].ToString(), null);

                Employee_ID = emp.Id;

                empLeavesList = userbl.GetLeaves(null, Employee_ID);


                var leaveslist = (from empleavelist1 in empLeavesList select empleavelist1);

                if (!String.IsNullOrWhiteSpace(searchString))
                {
                    leaveslist = leaveslist.Where(s => s.EmpUserID.Trim().ToUpper().Contains(searchString.Trim().ToUpper()));
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        leaveslist = leaveslist.OrderBy(s => s.LeaveFrom);
                        break;

                    default:
                        leaveslist = leaveslist.OrderByDescending(s => s.LeaveFrom);
                        break;
                }

                if (!String.IsNullOrEmpty(fromDate))
                {

                    DateTime Sdt = Convert.ToDateTime(fromDate);


                    leaveslist = (from empleavelist1 in leaveslist where empleavelist1.LeaveFrom >= Sdt select empleavelist1);

                    if (!String.IsNullOrEmpty(toDate))
                    {

                        DateTime Edt = Convert.ToDateTime(toDate);

                        leaveslist = (from empleavelist1 in leaveslist where empleavelist1.LeaveFrom >= Sdt where empleavelist1.LeaveTo <= Edt select empleavelist1);

                    }


                }

                return View(leaveslist.ToPagedList(pageNumber, pageSize));
            }

            else if (Session["TravelManager"] != null)
            {
                emp = userBL.GetEmployeeByID(Session["TravelManager"].ToString(), null);

                Employee_ID = emp.Id;
                empLeavesList = userbl.GetLeaves(null, Employee_ID);


                var leaveslist = (from empleavelist1 in empLeavesList select empleavelist1);
                if (!String.IsNullOrWhiteSpace(searchString))
                {
                    leaveslist = leaveslist.Where(s => s.EmpUserID.Trim().ToUpper().Contains(searchString.Trim().ToUpper()));
                }

                switch (sortOrder)
                {
                    case "name_desc":
                        leaveslist = leaveslist.OrderByDescending(s => s.LeaveFrom);
                        break;

                    default:
                        leaveslist = leaveslist.OrderBy(s => s.LeaveFrom);
                        break;
                }

                if (!String.IsNullOrEmpty(fromDate))
                {

                    DateTime Sdt = Convert.ToDateTime(fromDate);


                    leaveslist = (from empleavelist1 in leaveslist where empleavelist1.LeaveFrom >= Sdt select empleavelist1);

                    if (!String.IsNullOrEmpty(toDate))
                    {

                        DateTime Edt = Convert.ToDateTime(toDate);

                        leaveslist = (from empleavelist1 in leaveslist where empleavelist1.LeaveFrom >= Sdt where empleavelist1.LeaveTo <= Edt select empleavelist1);

                    }


                }

                return View(leaveslist.ToPagedList(pageNumber, pageSize));
            }
            else if (Session["Employee"] != null)
            {
                emp = userBL.GetEmployeeByID(Session["Employee"].ToString(), null);

                Employee_ID = emp.Id;
                empLeavesList = userbl.GetLeaves(Employee_ID, null);

                var leaveslist = (from empleavelist1 in empLeavesList select empleavelist1);

                if (!String.IsNullOrWhiteSpace(searchString))
                {
                    leaveslist = leaveslist.Where(s => s.EmpUserID.Trim().ToUpper().Contains(searchString.Trim().ToUpper()));

                }




                switch (sortOrder)
                {
                    case "name_desc":
                        leaveslist = leaveslist.OrderByDescending(s => s.LeaveFrom);
                        break;

                    default:
                        leaveslist = leaveslist.OrderBy(s => s.LeaveFrom);
                        break;
                }

                if (!String.IsNullOrEmpty(fromDate))
                {

                    DateTime Sdt = Convert.ToDateTime(fromDate);


                    leaveslist = (from empleavelist1 in leaveslist where empleavelist1.LeaveFrom >= Sdt select empleavelist1);

                    if (!String.IsNullOrEmpty(toDate))
                    {

                        DateTime Edt = Convert.ToDateTime(toDate);

                        leaveslist = (from empleavelist1 in leaveslist where empleavelist1.LeaveFrom >= Sdt where empleavelist1.LeaveTo <= Edt select empleavelist1);

                    }


                }

                return View(leaveslist.ToPagedList(pageNumber, pageSize));

            }

            else if (Session["Manager"] != null)
            {
                emp = userBL.GetEmployeeByID(Session["Manager"].ToString(), null);
                Employee_ID = emp.Id;
                empLeavesList = userbl.GetLeaves(null, Employee_ID);


                var leaveslist = (from empleavelist1 in empLeavesList where empleavelist1.ManagerUserId == Employee_ID select empleavelist1);

                if (!String.IsNullOrWhiteSpace(searchString))
                {
                    leaveslist = leaveslist.Where(s => s.EmpUserID.Trim().ToUpper().Contains(searchString.Trim().ToUpper()));
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        leaveslist = leaveslist.OrderByDescending(s => s.LeaveFrom);
                        break;

                    default:
                        leaveslist = leaveslist.OrderBy(s => s.LeaveFrom);
                        break;
                }

                if (!String.IsNullOrEmpty(fromDate))
                {

                    DateTime Sdt = Convert.ToDateTime(fromDate);


                    leaveslist = (from empleavelist1 in leaveslist where empleavelist1.LeaveFrom >= Sdt select empleavelist1);

                    if (!String.IsNullOrEmpty(toDate))
                    {

                        DateTime Edt = Convert.ToDateTime(toDate);

                        leaveslist = (from empleavelist1 in leaveslist where empleavelist1.LeaveFrom >= Sdt where empleavelist1.LeaveTo <= Edt select empleavelist1);

                    }


                }


                return View(leaveslist.ToPagedList(pageNumber, pageSize));

            }


            return View();

        }

        public ActionResult EmployeeList(string sortOrder, string currentFilter, string searchString, int? Page, int? id)
        {

            UserBL userbl = new UserBL();
            Universal.BusinessEntities.EmployeesList empList = new Universal.BusinessEntities.EmployeesList();
            empList = userbl.GetEmployees();

            
            if (id !=null)
            {
                int pageSize = 200;
                int pageNumber = (Page ?? 1);
                ViewBag.empgroup = id;
                var emps = (from empLst in empList where empLst.FK_UserGroup == id select empLst);
                return PartialView("EmployeeList", emps.ToPagedList(pageNumber, pageSize));
            }

            else
            {
                int pageSize = 7;
                int pageNumber = (Page ?? 1);
                Session["url"] = "/Shared/EmployeeList";
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
                if (searchString != null)
                {
                    Page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                ViewBag.CurrentFilter = searchString;
                if (Session["Manager"] != null)
                {
                    var emps = (from empLst in empList where empLst.ManagerUserId == Session["Manager"].ToString() select empLst);
                    if (!String.IsNullOrWhiteSpace(searchString))
                    {
                        emps = emps.Where(s => s.FullName.Trim().ToUpper().Contains(searchString.Trim().ToUpper()));
                    }
                    switch (sortOrder)
                    {
                        case "name_desc":
                            emps = emps.OrderByDescending(s => s.FullName);
                            break;

                        default:
                            emps = emps.OrderBy(s => s.FullName);
                            break;
                    }
                    return PartialView("EmployeeList", emps.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    var emps = (from empLst in empList select empLst);
                    if (!String.IsNullOrWhiteSpace(searchString))
                    {
                        emps = emps.Where(s => s.FullName.Trim().ToUpper().Contains(searchString.Trim().ToUpper()));
                    }

                    switch (sortOrder)
                    {
                        case "name_desc":
                            emps = emps.OrderByDescending(s => s.FullName);
                            break;

                        default:
                            emps = emps.OrderBy(s => s.FullName);
                            break;
                    }

                    return PartialView("EmployeeList", emps.ToPagedList(pageNumber, pageSize));
                }




            }

        }

        [HttpGet]
        public ActionResult ZoneEmployeeList(string sortOrder, string currentFilter, string searchString, int? Page, int? id)
        {

            UserBL userbl = new UserBL();
            Universal.BusinessEntities.EmployeesList empList = new Universal.BusinessEntities.EmployeesList();
            empList = userbl.GetEmployees();

            var emplist = from q in empList where q.FK_UserGroup == 3 && q.FK_LocationorZone == id select q;

            int pageSize = 7;
            int pageNumber = (Page ?? 1);

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";


            if (searchString != null)
            {
                Page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;



            return PartialView("ZoneEmployeeList", emplist.ToPagedList(pageNumber, pageSize));

        }

        public ActionResult SubmittedExpenditure(int? Page, string ExpType, string SZoneID, string SManagerID, string SEmployeeID, string SStatus, string SFromDate, string SToDate, string Search)
        {
            UserBL userBL = new UserBL();

            Session["url"] = "/Shared/SubmittedExpenditure";

            ViewBag.ExpType = ExpType;
            int pageSize;
            int pageNumber;
            if (ExpType == "Approved")
            {
                pageSize = 7;
                pageNumber = (Page ?? 1);
            }
            else
            {
                pageSize = 100;
                pageNumber = (Page ?? 1);
            }
            Employees emp = new Employees();
            Universal.BusinessEntities.EmployeesList empList = new Universal.BusinessEntities.EmployeesList();
            UserExpensesHeaderList userExpenditureHeaderList = new UserExpensesHeaderList();

            if (Session["AccountMgr"] != null)
            {
                #region Account Manager
                string EmployeeID = Session["AccountMgr"].ToString();
                userExpenditureHeaderList = userBL.GetUsersExpendituresforAccountManager(ExpType);
                var userExpenseHeader = userExpenditureHeaderList.ToList();

                #region zone
                ZonesList zones = new ZonesList();
                zones = userBL.GetZones();
                string defZones = "";
                List<SelectListItem> zonesList = new List<SelectListItem>();

                foreach (var item in zones)
                {
                    zonesList.Add(new SelectListItem()
                    {
                        Text = item.Zone,
                        Value = item.Id.ToString(),
                        Selected = (item.Zone == defZones ? true : false)
                    });
                }

                ViewBag.Zones = zonesList;
                #endregion

                if (Search != null)
                {
                    #region Search for Account Manager
                    userExpenditureHeaderList = userBL.GetAllEmployeesSubmittedExpenditureRecords();

                    var SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList select SearchExpenditures).ToList();

                    if (SZoneID != null && SZoneID.Length > 0)
                    {
                        if (SManagerID != null && SManagerID.Length > 0)
                        {
                            if (SEmployeeID != null && SEmployeeID.Length > 0)
                            {
                                #region fromEmployee
                                if (SFromDate != null && SFromDate.Length > 0 && SToDate != null && SToDate.Length > 0)
                                {
                                    if (SStatus == "Approved")
                                    {
                                        SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && x.ManagerID == Convert.ToInt32(SManagerID) && x.empID == Convert.ToInt32(SEmployeeID) && x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(SToDate) && x.ManagerStatus != null && x.AccountManagerStatus == SStatus && x.PaidPendingAmount > 0).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                    }
                                    else
                                    {
                                        SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && x.ManagerID == Convert.ToInt32(SManagerID) && x.empID == Convert.ToInt32(SEmployeeID) && x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(SToDate) && x.ManagerStatus != null && x.AccountManagerStatus == SStatus).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                    }
                                }
                                else if (SFromDate != null && SFromDate.Length > 0)
                                {
                                    if (SStatus == "Approved")
                                    {
                                        SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && x.ManagerID == Convert.ToInt32(SManagerID) && x.empID == Convert.ToInt32(SEmployeeID) && x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(DateTime.Now) && x.ManagerStatus != null && x.AccountManagerStatus == SStatus && x.PaidPendingAmount > 0).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                    }
                                    else
                                    {
                                        SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && x.ManagerID == Convert.ToInt32(SManagerID) && x.empID == Convert.ToInt32(SEmployeeID) && x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(DateTime.Now) && x.ManagerStatus != null && x.AccountManagerStatus == SStatus).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                    }
                                }
                                else
                                {
                                    if (SStatus == "Approved")
                                    {
                                        SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && x.ManagerID == Convert.ToInt32(SManagerID) && x.empID == Convert.ToInt32(SEmployeeID) && x.ManagerStatus != null && x.AccountManagerStatus == SStatus && x.PaidPendingAmount > 0).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                    }
                                    else
                                    {
                                        SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && x.ManagerID == Convert.ToInt32(SManagerID) && x.empID == Convert.ToInt32(SEmployeeID) && x.ManagerStatus != null && x.AccountManagerStatus == SStatus).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                #region fromManager
                                if (SFromDate != null && SFromDate.Length > 0 && SToDate != null && SToDate.Length > 0)
                                {
                                    if (SStatus == "Approved")
                                    {
                                        SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(SToDate) && x.ManagerID == Convert.ToInt32(SManagerID) && x.ManagerStatus != null && x.AccountManagerStatus == SStatus && x.PaidPendingAmount > 0).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                    }
                                    else
                                    {
                                        SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(SToDate) && x.ManagerID == Convert.ToInt32(SManagerID) && x.ManagerStatus != null && x.AccountManagerStatus == SStatus).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                    }
                                }
                                else if (SFromDate != null && SFromDate.Length > 0)
                                {
                                    if (SStatus == "Approved")
                                    {
                                        SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(DateTime.Now) && x.ManagerID == Convert.ToInt32(SManagerID) && x.ManagerStatus != null && x.AccountManagerStatus == SStatus && x.PaidPendingAmount > 0).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                    }
                                    else
                                    {
                                        SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(DateTime.Now) && x.ManagerID == Convert.ToInt32(SManagerID) && x.ManagerStatus != null && x.AccountManagerStatus == SStatus).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                    }
                                }
                                else
                                {
                                    if (SStatus == "Approved")
                                    {
                                        SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && x.ManagerID == Convert.ToInt32(SManagerID) && x.ManagerStatus != null && x.AccountManagerStatus == SStatus && x.PaidPendingAmount > 0).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                    }
                                    else
                                    {
                                        SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && x.ManagerID == Convert.ToInt32(SManagerID) && x.ManagerStatus != null && x.AccountManagerStatus == SStatus).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                    }
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            #region Zone and Zone with Dates
                            if (SFromDate != null && SFromDate.Length > 0 && SToDate != null && SToDate.Length > 0)
                            {
                                if (SStatus == "Approved")
                                {
                                    SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(SToDate) && (x.ROLE == "Employee" ? x.ManagerStatus != null : x.ManagerStatus == null) && x.AccountManagerStatus == SStatus && x.PaidPendingAmount > 0).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                }
                                else
                                {
                                    SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(SToDate) && (x.ROLE == "Employee" ? x.ManagerStatus != null : x.ManagerStatus == null) && x.AccountManagerStatus == SStatus).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                }
                            }
                            else if (SFromDate != null && SFromDate.Length > 0)
                            {
                                if (SStatus == "Approved")
                                {
                                    SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(DateTime.Now) && (x.ROLE == "Employee" ? x.ManagerStatus != null : x.ManagerStatus == null) && x.AccountManagerStatus == SStatus && x.PaidPendingAmount > 0).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                }
                                else
                                {
                                    SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(DateTime.Now) && (x.ROLE == "Employee" ? x.ManagerStatus != null : x.ManagerStatus == null) && x.AccountManagerStatus == SStatus).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                }
                            }
                            else
                            {
                                if (SStatus == "Approved")
                                {
                                    SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && (x.ROLE == "Employee" ? x.ManagerStatus != null : x.ManagerStatus == null) && x.AccountManagerStatus == SStatus && x.PaidPendingAmount > 0).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                }
                                else
                                {
                                    SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && (x.ROLE == "Employee" ? x.ManagerStatus != null : x.ManagerStatus == null) && x.AccountManagerStatus == SStatus).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                }
                            }
                            #endregion
                        }
                    }
                    else
                    {
                            #region onlyDate
                            if (SFromDate != null && SFromDate.Length > 0 && SToDate != null && SToDate.Length > 0)
                            {
                                if (SStatus == "Approved")
                                {
                                    SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(SToDate) && (x.ROLE == "Employee" ? x.ManagerStatus != null : x.ManagerStatus == null) && x.AccountManagerStatus == SStatus && x.PaidPendingAmount > 0).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                }
                                else
                                {
                                    SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(SToDate) && (x.ROLE == "Employee" ? x.ManagerStatus != null : x.ManagerStatus == null) && x.AccountManagerStatus == SStatus).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                }
                            }
                            else
                            {
                                if (SStatus == "Approved")
                                {
                                    SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(DateTime.Now) && (x.ROLE == "Employee" ? x.ManagerStatus != null : x.ManagerStatus == null) && x.AccountManagerStatus == SStatus && x.PaidPendingAmount > 0).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                }
                                else
                                {
                                    SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(DateTime.Now) && (x.ROLE == "Employee" ? x.ManagerStatus != null : x.ManagerStatus == null) && x.AccountManagerStatus == SStatus).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                }
                            }
                            #endregion
                    }
                    if (SearchExpenses.Count > 0)
                    {
                        var MaxDate = (from d in SearchExpenses select d.CreatedDate).Max();
                        var UserexpenseheaderforMaxId = (from userExpenseHeader1 in SearchExpenses where userExpenseHeader1.CreatedDate == Convert.ToDateTime(MaxDate) select userExpenseHeader1).ToList();
                        ViewBag.MaxId = UserexpenseheaderforMaxId[0].Id;
                        ViewBag.Search = "Search";
                        
                    }
                    ViewBag.ExpType = SStatus;
                    ViewBag.Status = SStatus;

                    if (ExpType == "Approved")
                    {
                        return View("SubmittedExpenditure", SearchExpenses.ToPagedList(pageNumber, pageSize));
                    }
                    else
                    {
                        return View("SubmittedExpenditure", SearchExpenses.ToPagedList(pageNumber, SearchExpenses.Count != 0 ? SearchExpenses.Count : 1));
                    }
                    
                    #endregion
                }
                else
                {
                    #region Expenditure list
                    if (ExpType != "SubExp" && ExpType != null)
                    {
                        if (ExpType == "Approved")
                        {
                            UserExpensesHeaderList objHeaderList = new UserExpensesHeaderList();
                            objHeaderList = userBL.GetEmployeeMonthlyExpenditures();
                            userExpenseHeader = objHeaderList.OrderByDescending(s=>s.FromDate).ToList();
                            ViewBag.Status = "Approved";
                        }
                        else
                        {
                            userExpenseHeader = (from userExpenseHeader1 in userExpenditureHeaderList.Where(x => x.AccountManagerStatus == ExpType).OrderByDescending(x => x.FromDate) select userExpenseHeader1).ToList();

                            if (userExpenseHeader.Count > 0)
                            {
                                var MaxDate = (from d in userExpenseHeader select d.FromDate).Max();

                                var UserexpenseheaderforMaxId = (from userExpenseHeader1 in userExpenseHeader where userExpenseHeader1.FromDate == Convert.ToDateTime(MaxDate) select userExpenseHeader1).ToList();

                                ViewBag.MaxId = UserexpenseheaderforMaxId[0].Id;
                            }
                            else
                                ViewBag.MaxId = 0;
                        }
                       
                    }

                    if (ExpType == "Approved")
                    {
                        return View("SubmittedExpenditure", userExpenseHeader.ToPagedList(pageNumber, pageSize));
                    }
                    else
                    {
                        return View("SubmittedExpenditure", userExpenseHeader.ToPagedList(pageNumber, userExpenseHeader.Count != 0 ? userExpenseHeader.Count : 1));
                    }
                    
                    #endregion
                }
                #endregion
            }
            else if (Session["Manager"] != null)
            {
                #region Manager
                string EmployeeID = Session["Manager"].ToString();

                emp = userBL.GetEmployeeByID(Session["Manager"].ToString(), null);

                #region empsinManager

                empList = userBL.GetEmployees();

                var emps = (from empLst in empList where empLst.ManagerId == emp.Id select empLst);

                string defEmployee = "";
                List<SelectListItem> empsinManager = new List<SelectListItem>();

                foreach (var item in emps)
                {
                    empsinManager.Add(new SelectListItem()
                    {
                        Text = item.Employee_ID + "[" + item.FirstName + " " + item.LastName + "]",
                        Value = item.Id.ToString(),
                        Selected = (item.Employee_ID + "[" + item.FathersName + "]" == defEmployee ? true : false)
                    });
                }

                ViewBag.EmployeesByManager = empsinManager;

                #endregion

                if (Search != null)
                {
                    #region Manager Search

                    userExpenditureHeaderList = userBL.GetAllEmployeesSubmittedExpenditureRecords();

                    var SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList select SearchExpenditures).ToList();

                    if (SEmployeeID != null && SEmployeeID.Length > 0)
                    {
                        if (SFromDate != null && SFromDate.Length > 0 && SToDate != null && SToDate.Length > 0)
                        {
                            SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(SToDate) && x.Status == true && x.ManagerStatus == SStatus && x.empID == Convert.ToInt32(SEmployeeID)).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                        }
                        else if (SFromDate != null && SFromDate.Length > 0)
                        {
                            SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(DateTime.Now) && x.Status == true && x.ManagerStatus == SStatus && x.empID == Convert.ToInt32(SEmployeeID)).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                        }
                        else
                        {
                            SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.Status == true && x.ManagerStatus == SStatus && x.empID == Convert.ToInt32(SEmployeeID)).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                        }
                    }
                    else if (SFromDate != null && SFromDate.Length > 0 && SToDate != null && SToDate.Length > 0)
                    {
                        SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(SToDate) && x.Status == true && x.ManagerStatus == SStatus).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                    }
                    else if (SFromDate != null && SFromDate.Length > 0)
                    {
                        SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(DateTime.Now) && x.Status == true && x.ManagerStatus == SStatus).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                    }

                    if (SearchExpenses.Count > 0)
                    {
                        var MaxDate = (from d in SearchExpenses select d.CreatedDate).Max();
                        var UserexpenseheaderforMaxId = (from userExpenseHeader1 in SearchExpenses where userExpenseHeader1.CreatedDate == Convert.ToDateTime(MaxDate) select userExpenseHeader1).ToList();
                        ViewBag.MaxId = UserexpenseheaderforMaxId[0].Id;
                        ViewBag.Search = "Search";
                    }
                    ViewBag.ExpType = SStatus;
                    return View("SubmittedExpenditure", SearchExpenses.ToPagedList(pageNumber, SearchExpenses.Count != 0 ? SearchExpenses.Count : 1));

                    #endregion
                }
                else
                {
                    #region manager expenditures
                    userExpenditureHeaderList = userBL.GetUsersExpendituresforManager(emp.Id, ExpType);

                    var userExpenseHeader = userExpenditureHeaderList.ToList();

                    if (ExpType != "SubExp" && ExpType != null)
                    {
                        userExpenseHeader = (from userExpenseHeader1 in userExpenditureHeaderList.Where(x => x.ManagerStatus == ExpType).OrderByDescending(x=>x.FromDate) select userExpenseHeader1).ToList();

                        if (userExpenseHeader.Count > 0)
                        {
                            var MaxDate = (from d in userExpenditureHeaderList.Where(x => x.ManagerStatus == ExpType) select d.FromDate).Max();

                            var UserexpenseheaderforMaxId = (from userExpenseHeader1 in userExpenseHeader where userExpenseHeader1.FromDate == Convert.ToDateTime(MaxDate) select userExpenseHeader1).ToList();

                            ViewBag.MaxId = UserexpenseheaderforMaxId[0].Id;
                        }
                        else
                            ViewBag.MaxId = 0;
                    }
                    return View("SubmittedExpenditure", userExpenseHeader.ToPagedList(pageNumber, userExpenseHeader.Count != 0 ? userExpenseHeader.Count : 1));
                    #endregion
                }
                #endregion
            }
            else if (Session["Employee"] != null)
            {
                #region Employee
                string EmployeeID = Session["Employee"].ToString();

                emp = userBL.GetEmployeeByID(Session["Employee"].ToString(), null);

                userExpenditureHeaderList = userBL.GetSubmittedExpenditure(emp.Id);

                var userExpenseHeader = userExpenditureHeaderList.ToList();

                if (Search != null && SStatus != null)
                {
                    #region search

                    if (SFromDate != null && SFromDate.Length > 0 && SToDate != null && SToDate.Length > 0)
                    {
                        if (SStatus == "SubExp")
                        {
                            userExpenseHeader = (from userExpenseHeader1 in userExpenditureHeaderList where userExpenseHeader1.AccountManagerStatus == null && userExpenseHeader1.FromDate >= Convert.ToDateTime(SFromDate) && userExpenseHeader1.FromDate <= Convert.ToDateTime(SToDate) select userExpenseHeader1).ToList();
                        }
                        else
                        {
                            userExpenseHeader = (from userExpenseHeader1 in userExpenditureHeaderList where userExpenseHeader1.AccountManagerStatus == SStatus && userExpenseHeader1.FromDate >= Convert.ToDateTime(SFromDate) && userExpenseHeader1.FromDate <= Convert.ToDateTime(SToDate) select userExpenseHeader1).ToList();
                        }
                    }
                    else if (SFromDate != null && SFromDate.Length > 0)
                    {
                        if (SStatus == "SubExp")
                        {
                            userExpenseHeader = (from userExpenseHeader1 in userExpenditureHeaderList where userExpenseHeader1.AccountManagerStatus == null && userExpenseHeader1.FromDate >= Convert.ToDateTime(SFromDate) && userExpenseHeader1.FromDate <= Convert.ToDateTime(DateTime.Now) select userExpenseHeader1).ToList();
                        }
                        else
                        {
                            userExpenseHeader = (from userExpenseHeader1 in userExpenditureHeaderList where userExpenseHeader1.AccountManagerStatus == SStatus && userExpenseHeader1.FromDate >= Convert.ToDateTime(SFromDate) && userExpenseHeader1.FromDate <= Convert.ToDateTime(DateTime.Now) select userExpenseHeader1).ToList();
                        }
                    }

                    if (userExpenseHeader.Count > 0)
                    {
                        var MaxDate = (from d in userExpenseHeader select d.CreatedDate).Max();

                        var UserexpenseheaderforMaxId = (from userExpenseHeader1 in userExpenseHeader where userExpenseHeader1.CreatedDate == Convert.ToDateTime(MaxDate) select userExpenseHeader1).ToList();

                        ViewBag.MaxId = UserexpenseheaderforMaxId[0].Id;
                    }

                    ViewBag.ExpType = SStatus;

                    return View("SubmittedExpenditure", userExpenseHeader.ToPagedList(pageNumber, userExpenseHeader.Count != 0 ? userExpenseHeader.Count : 1));
                    #endregion
                }
                else
                {
                    #region emp expenses
                    if (ExpType != "SubExp")
                    {
                        userExpenseHeader = (from userExpenseHeader1 in userExpenditureHeaderList.Where(x=>x.AccountManagerStatus == ExpType).OrderByDescending(x=>x.FromDate) select userExpenseHeader1).ToList();
                    }
                    else
                    {
                        userExpenseHeader = (from userExpenseHeader1 in userExpenditureHeaderList.Where(x=> x.AccountManagerStatus == null).OrderByDescending(x=>x.FromDate) select userExpenseHeader1).ToList();
                    }
                    if (userExpenseHeader.Count > 0)
                    {
                        var MaxDate = (from d in userExpenseHeader select d.FromDate).Max();

                        var UserexpenseheaderforMaxId = (from userExpenseHeader1 in userExpenseHeader where userExpenseHeader1.FromDate == Convert.ToDateTime(MaxDate) select userExpenseHeader1).ToList();

                        ViewBag.MaxId = UserexpenseheaderforMaxId[0].Id;
                    }
                    else
                        ViewBag.MaxId = 0;

                    return View("SubmittedExpenditure", userExpenseHeader.ToPagedList(pageNumber, userExpenseHeader.Count != 0 ? userExpenseHeader.Count : 1));
                    #endregion
                }
                #endregion
            }
            else
                return RedirectToAction("Signin", "Home");
        }

        public ActionResult ApprovedExpenditure(int? Page)
        {
            UserBL userBL = new UserBL();
            Session["url"] = "/Shared/ApprovedExpenditure";

            string EmployeeID = Session["Employee"].ToString();

            int pageSize = 7;
            int pageNumber = (Page ?? 1);

            UserExpensesHeaderList userExpenditureHeaderList = new UserExpensesHeaderList();

            Employees emp = new Employees();

            emp = userBL.GetEmployeeByID(Session["Employee"].ToString(), null);

            userExpenditureHeaderList = userBL.GetSubmittedExpenditure(emp.Id);

            var userExpenseHeader = (from userExpenseHeader1 in userExpenditureHeaderList where userExpenseHeader1.AccountManagerStatus == "Approved" select userExpenseHeader1).ToList();

            return View("ApprovedExpenditure", userExpenseHeader.ToPagedList(pageNumber, pageSize));

        }

        public ActionResult ExpenseListByID(string Data)
        {
            UserExpenseDetailsList useExpenseDetailsList = new UserExpenseDetailsList();
            UserBL userBL = new UserBL();

            UserExpenses_Header userexheader = new UserExpenses_Header();

            ServiceOrderNos_List serviceorderNosList = new ServiceOrderNos_List();

            if (Data != string.Empty)
            {

                useExpenseDetailsList = userBL.GetExpenditureDetailsByID(Convert.ToInt32(Data));


                UserExpense_Details expdetails = new UserExpense_Details();

                List<SelectListItem> DropDownlist = new List<SelectListItem>();

                SelectListItem Drplist = new SelectListItem();

                userexheader.Id = Convert.ToInt32(Data);

                if (Session["AccountMgr"] != null || Session["Manager"] != null)
                {

                    ViewBag.Authorized = "Authorized";

                    Drplist = new SelectListItem() { Text = "Approve", Value = "Approved" };

                    DropDownlist.Insert(0, Drplist);

                    Drplist = new SelectListItem() { Text = "Upheld", Value = "Upheld" };

                    DropDownlist.Insert(1, Drplist);

                    if (Session["AccountMgr"] != null)
                    {

                        Drplist = new SelectListItem() { Text = "Reject", Value = "Rejected" };

                        DropDownlist.Insert(2, Drplist);
                    }



                    ViewBag.DropDownlist = DropDownlist;


                    if (useExpenseDetailsList.Count > 0)
                    {
                        ViewBag.ManagerStatus = useExpenseDetailsList[0].ManagerStatus != null ? "ManagerApproved" : "ManagerNotApproved";

                        ViewBag.AccountMgr = useExpenseDetailsList[0].AccountManagerStatus != null ? "AccountMgrApproved" : "AccountMgrNotApproved";


                        ViewBag.profilepath = useExpenseDetailsList[0].ProfilePicturePath != null ? "../../Content/ProfileImg/" + useExpenseDetailsList[0].ProfilePicturePath : "../../Content/Images/prf-no-img.png";

                        ViewBag.Fullname = useExpenseDetailsList[0].FullName;

                    }
                }

                userexheader.UserExpenseDetailsList = useExpenseDetailsList;

                if (useExpenseDetailsList.Count > 0)
                {
                    if (useExpenseDetailsList[0].SON == null && (useExpenseDetailsList[0].ExpenseTypeDetails != "Mobile Bill" || useExpenseDetailsList[0].ExpenseTypeDetails != "Internet Bill"))
                    {
                        serviceorderNosList = userBL.GetMultipleSONOsbyExpenseHeaderID(Convert.ToInt32(Data));
                        userexheader.objServiceOrderNosList = serviceorderNosList;
                    }
                }

                return PartialView("ExpenseListByID", userexheader);
            }
            else
            {
                return PartialView("ExpenseListByID", userexheader);
            }
        }
        
        [HttpPost]
        public ActionResult UpdateExpenseSheetStatus(UserExpenses_Header userexpenseheader, string ExpenseHeaderID, string ExpenseComment, string StatusType)
        {
            userexpenseheader.Id = Convert.ToInt32(ExpenseHeaderID);
            userexpenseheader.TempComments = ExpenseComment;
            userexpenseheader.TempStatus = StatusType;
            UserBL userbl = new UserBL();

            string userType;

            if (Session["Manager"] != null)
            {

                userType = "manager";

                int? result = userbl.UpdateExpenditureStatus(userexpenseheader, userType);


            }
            if (Session["AccountMgr"] != null)
            {
                userType = "accountMgr";

                int? result = userbl.UpdateExpenditureStatus(userexpenseheader, userType);
                if (StatusType == "Rejected")
                {
                    userbl.mailSend(userexpenseheader,ExpenseHeaderID);
                }
                else if (StatusType == "Upheld")
                {
                    userbl.MailSend(userexpenseheader, ExpenseHeaderID);
                }
            }



            return Json(new { Available = "Updated Successfully", Text = "Updated Successfully" });
        }

        [HttpGet]
        public ActionResult UpdateTavelDesk(int? id)
        {
            UserBL userbl = new UserBL();
            Session["url"] = "/Shared/TravelDesk/" + id;
            TravelDesk tdesklist = new TravelDesk();
            tdesklist = userbl.GetTravelDesk(id);
            Universal.BusinessEntities.EmployeesList empList = new Universal.BusinessEntities.EmployeesList();
            empList = userbl.GetEmployees();
            List<SelectListItem> employeeList = new List<SelectListItem>();
            foreach (var item in empList)
            {
                employeeList.Add(new SelectListItem()
                {
                    Text = item.Employee_ID + " [" + item.FirstName + "] ",
                    Value = item.Id.ToString(),
                    Selected = (item.Id == tdesklist.FK_EmployeeId ? true : false)
                });



            }
            //var employeeids = (from s in empList select s.Employee_ID);
            ViewBag.empList = employeeList;
            return PartialView("_UpdateTavelDesk", tdesklist);
        }

        [HttpPost]
        public ActionResult UpdateTavelDesk(TravelDesk tdesk)
        {
            //Session["url"] = "/Shared/TravelDesk";

            tdesk.CreatedBy = Session["TravelManager"].ToString();
            tdesk.ModifiedBy = Session["TravelManager"].ToString();
            UserBL userBL = new UserBL();
            string UserSession = (HttpContext.Session["TravelManager"] != null) ? HttpContext.Session["TravelManager"].ToString() : "TravelManager Session Null";
            TravelUploadTicket travelUploadTicket = new TravelUploadTicket();
            var ticketsDeleted = Request["DeletedUrl"];
            if (ticketsDeleted != null)
            {
                if (ticketsDeleted.IndexOf(',') != -1)
                {
                    string[] allMails = ticketsDeleted.Split(',');

                    foreach (string ticketFile in allMails)
                    {
                        var pathString = Path.Combine(Server.MapPath("~/Content/TravelImages"), ticketFile);
                        FileInfo fi = new FileInfo(pathString);
                        if (fi.Exists)
                        {
                            fi.Delete();
                            travelUploadTicket.Fk_TravelId = tdesk.Id;
                            travelUploadTicket.TicketImage = ticketFile;
                            int? err1 = userBL.DeleteTravelTicket(travelUploadTicket);
                        }
                    }

                }
                else
                {
                    var pathString = Path.Combine(Server.MapPath("~/Content/TravelImages"), ticketsDeleted);
                    FileInfo fi = new FileInfo(pathString);
                    if (fi.Exists)
                    {
                        fi.Delete();
                        travelUploadTicket.Fk_TravelId = tdesk.Id;
                        travelUploadTicket.TicketImage = ticketsDeleted;
                        int? err1 = userBL.DeleteTravelTicket(travelUploadTicket);
                    }
                }
            }

            if (tdesk.Purpose == null)
            {
                tdesk.Purpose = "TrainingExpenditure";
            }


            int? err = userBL.CreateTravel(tdesk, 1);
            if (err == null)
            {
                Session["TravelId"] = tdesk.Id;
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs"), UserSession.ToUpper(), "TravelManager successfully updated the traveldesk information");
                return Json(new { Available = true, Text = "You have successfully updated the Travel Desk!!!" });
            }
            else
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs"), UserSession.ToUpper(), "not updated traveldesk information");
                return Json(new { Available = false, Text = "Your request not submitted" });
            }

        }

        [HttpGet]
        public ActionResult LeaveView(int id)
        {

            UserBL userBL = new UserBL();

            Universal.BusinessEntities.Leave Leavedetails = new Universal.BusinessEntities.Leave();

            Leavedetails = userBL.LeaveDetailsById(id);




            if (Session["Manager"] != null || Session["AccountMgr"] != null || Session["TravelManager"] != null)
            {

                Employees emp = new Employees();

                int? Employee_ID;

                Universal.BusinessEntities.LeaveList leavesList = new Universal.BusinessEntities.LeaveList();

                UserBL userbl = new UserBL();

                //emp = userBL.GetEmployeeByID(Session["Manager"].ToString(),null);

                //Employee_ID = emp.Id;
                if (Leavedetails.UserGroup == 2)
                    leavesList = userbl.GetLeaves(null, Leavedetails.AccountManagerId);
                else if (Leavedetails.UserGroup == 1 || Leavedetails.UserGroup == 4)
                    leavesList = userbl.GetLeaves(null,1);
                else
                leavesList = userbl.GetLeaves(null, Leavedetails.Managerid);

                var empleavelist = (from elvlst in leavesList.Where(x => x.Employee_ID == Leavedetails.Employee_ID) select elvlst).ToList();


                Universal.BusinessEntities.Leave lvlist = new Universal.BusinessEntities.Leave();


                if (Leavedetails.LeaveFrom.Value.Year == Leavedetails.LeaveTo.Value.Year)
                {




                    lvlist = userbl.GenerateLeaveReport(Leavedetails, leavesList, Leavedetails.LeaveFrom);


                    Leavedetails.TotalNumberofLeaves = lvlist.TotalNumberofLeaves;

                    Leavedetails.ApprovedLeaves = lvlist.ApprovedLeaves;

                    Leavedetails.RejectedLeaves = lvlist.RejectedLeaves;

                    Leavedetails.PendingLeaves = lvlist.PendingLeaves;

                    Leavedetails.HospitalizedLeave = lvlist.HospitalizedLeave;

                    Leavedetails.CasualLeave = lvlist.CasualLeave;

                    Leavedetails.SickLeave = lvlist.SickLeave;

                    Leavedetails.FuneralLeave = lvlist.FuneralLeave;



                }

                else
                {


                    lvlist = userbl.GenerateLeaveReport(Leavedetails, leavesList, Leavedetails.LeaveFrom);

                    Leavedetails.TotalNumberofLeaves = lvlist.TotalNumberofLeaves;

                    Leavedetails.ApprovedLeaves = lvlist.ApprovedLeaves;

                    Leavedetails.RejectedLeaves = lvlist.RejectedLeaves;
                        
                    Leavedetails.PendingLeaves = lvlist.PendingLeaves;

                    Leavedetails.HospitalizedLeave = lvlist.HospitalizedLeave;

                    Leavedetails.CasualLeave = lvlist.CasualLeave;

                    Leavedetails.SickLeave = lvlist.SickLeave;

                    Leavedetails.FuneralLeave = lvlist.FuneralLeave;


                    lvlist = userbl.GenerateLeaveReport(Leavedetails, leavesList, Leavedetails.LeaveTo);

                    Leavedetails.TotalNumberofLeavesother = lvlist.TotalNumberofLeaves;

                    Leavedetails.ApprovedLeavesother = lvlist.ApprovedLeaves;

                    Leavedetails.RejectedLeavesother = lvlist.RejectedLeaves;

                    Leavedetails.PendingLeavesother = lvlist.PendingLeaves;

                    Leavedetails.HospitalizedLeaveOther = lvlist.HospitalizedLeave;

                    Leavedetails.CasualLeaveOther = lvlist.CasualLeave;

                    Leavedetails.SickLeaveOther = lvlist.SickLeave;

                    Leavedetails.FuneralLeaveOther = lvlist.FuneralLeave;


                }

                for (DateTime? date = Leavedetails.LeaveFrom; date <= Leavedetails.LeaveTo; date = date.Value.AddDays(1))
                {

                    int? noofleaves = (from lv in leavesList where (date >= lv.LeaveFrom && date <= lv.LeaveTo && lv.Employee_ID!=Leavedetails.Employee_ID) select lv.NoofDays).Sum();

                    var list = new List<KeyValuePair<DateTime?, int?>>();

                    list.Add(new KeyValuePair<DateTime?, int?>(date, noofleaves));

                  



                }
              


            }

            return View(Leavedetails);
        }

        [HttpGet]
        public ActionResult LeaveStatusPopup(int? id, string status, int? empid, DateTime? lvfrom, DateTime? lvto)
        {
            UserBL userBL = new UserBL();
            Leave leave = new Leave();

            bool leavestatus;

            if (status == "true")
            {

                leavestatus = true;

            }
            else
            {

                leavestatus = false;

            }
            leave.Id = id;

            leave.LeaveStatus = leavestatus;

            leave.Employee_ID = empid;

            leave.LeaveFrom = lvfrom;

            leave.LeaveTo = lvto;

            //if (id != null && status != null && empid != null)
            //{
            //    leave.Employee_ID = empid;
            //    leave.LeaveStatus = Convert.ToBoolean(status);
            //    leave.Id = id;
            //    userBL.Mail(leave);

            //}

            return View(leave);
        }

        [HttpPost]
        public ActionResult LeaveStatusPopup(Leave leave)
        {
            UserBL userBL = new UserBL();
            //string UserSession = (HttpContext.Session["AccountMgr"] != null) ? HttpContext.Session["AccountMgr"].ToString() : "Admin Session Null";

            int? result = userBL.UpdateLeaveStatus(leave);


            if(leave.Employee_ID!=null && leave.Id!=null && leave.LeaveStatus!=null)
                userBL.Mail(leave);

            //LogFiles logfiles = new LogFiles();
            //logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs"),UserSession.ToUpper(),"Admin action performed for Leave request");
            return RedirectToAction("Leave", "Shared");



        }
        
        [HttpPost]
        public ActionResult ValidateLeaveDates(string sdate, string edate)
        {
            DateTime temp;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-IN");



            if ((DateTime.TryParse(sdate, out temp)) && (DateTime.TryParse(edate, out temp)))
            {

                Employees emp = new Employees();

                int? Employee_ID;

                UserBL userBL = new UserBL();

                emp = userBL.GetEmployeeByID(Session["Employee"].ToString(), null);

                Employee_ID = emp.Id;

                Universal.BusinessEntities.LeaveList empLeavesList = new Universal.BusinessEntities.LeaveList();

                empLeavesList = userBL.GetLeaves(Employee_ID, null);

                int? countleave=0;

            
                    for (DateTime date = Convert.ToDateTime(sdate); date <= Convert.ToDateTime(edate); date = date.AddDays(1))
                    {
                        countleave = (from cuntlv in empLeavesList where date >= cuntlv.LeaveFrom && date <= cuntlv.LeaveTo && (cuntlv.LeaveStatus == true || cuntlv.LeaveStatus == null) select cuntlv).Count() + countleave;
                    }


                

                if (countleave!=0)
                    return Json(false);
                else
                    return Json(true);


            }
            else
            {

                return Json(false);
            }

        }

        public ActionResult GetManagersList(int? ZoneId)
        {
            UserBL userbl = new UserBL();
            Universal.BusinessEntities.EmployeesList empList = new Universal.BusinessEntities.EmployeesList();
            empList = userbl.GetEmployees();

            var magrlist = from q in empList where q.FK_UserGroup == 2 && q.FK_LocationorZone == ZoneId select q;

            return Json(magrlist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEmployeesForManager(int? Id)
        {
            UserBL userbl = new UserBL();
            Universal.BusinessEntities.EmployeesList empList = new Universal.BusinessEntities.EmployeesList();
            empList = userbl.GetEmployees();

            var emps = (from a in empList where a.ManagerId == Id select new { a.Id, a.Employee_ID, a.FirstName, a.LastName });

            return Json(emps, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ViewProfile(int? id,string profile)
        {

            UserBL userbl = new UserBL();
            Employees emp = new Employees();
            if (profile != null)
            {
                if (profile == "yes")
                {
                    ViewBag.Profile = "yes";
                }
                else if (profile == "ug")
                {
                    ViewBag.Profile = "ug";
                }
            }

            emp = userbl.GetEmployeeByID(null, id);
            return PartialView("_ViewProfile", emp);
        }

        [HttpPost]
        public ActionResult ValidateTravelDeskDate(int? id, string fromdate, string todate)
        {



            DateTime temp;

            if ((DateTime.TryParse(fromdate, out temp)) || (DateTime.TryParse(todate, out temp)))
            {


                fromdate = (string.IsNullOrEmpty(fromdate) ? (String.Format("{0:yyyy-M-d HH:mm:ss}", Convert.ToDateTime(todate))) : (String.Format("{0:yyyy-M-d HH:mm:ss}", Convert.ToDateTime(fromdate))));

                todate = (string.IsNullOrEmpty(todate) ? (String.Format("{0:yyyy-M-d HH:mm:ss}", Convert.ToDateTime(fromdate))) : (String.Format("{0:yyyy-M-d HH:mm:ss}", Convert.ToDateTime(todate))));

                UserBL userBL = new UserBL();



                DataTable getResult = userBL.ValidateTravelDeskDates(Convert.ToDateTime(fromdate), Convert.ToDateTime(todate), id);



                if (getResult.Rows.Count > 0 && Convert.ToInt32(getResult.Rows[0]["resultcount"]) != 0)
                    return Json(false);
                else
                    return Json(true);
            }
            else
            {

                return Json(false);
            }
        }

        [HttpGet]
        public ActionResult EmployeeExpenditures(int? Page, string ExpType, string SZoneID, string SManagerID, string SEmployeeID, string SStatus, string SFromDate, string SToDate, string Search)
        {
            UserBL userBL = new UserBL();
            Session["url"] = "/Shared/EmployeeExpenditures";
            Page = 1;
            
            int pageNumber = (Page ?? 1);
            Employees emp = new Employees();
            Universal.BusinessEntities.EmployeesList empList = new Universal.BusinessEntities.EmployeesList();
            UserExpensesHeaderList userExpenditureHeaderList = new UserExpensesHeaderList();

            if (Session["AccountMgr"] != null)
            {
                #region Search
                if (Search != null)
                {
                    #region zone
                    ZonesList zones = new ZonesList();
                    zones = userBL.GetZones();
                    string defZones = "";
                    List<SelectListItem> zonesList = new List<SelectListItem>();

                    foreach (var item in zones)
                    {
                        zonesList.Add(new SelectListItem()
                        {
                            Text = item.Zone,
                            Value = item.Id.ToString(),
                            Selected = (item.Zone == defZones ? true : false)
                        });
                    }

                    ViewBag.Zones = zonesList;
                    #endregion

                    userExpenditureHeaderList = userBL.GetAllEmployeesSubmittedExpenditureRecords();

                    var SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList select SearchExpenditures).ToList();

                    if (SZoneID != null && SZoneID.Length > 0)
                    {
                        if (SManagerID != null && SManagerID.Length > 0)
                        {
                            if (SEmployeeID != null && SEmployeeID.Length > 0)
                            {
                                #region fromEmployee
                                if (SFromDate != null && SFromDate.Length > 0 && SToDate != null && SToDate.Length > 0)
                                {
                                    SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && x.ManagerID == Convert.ToInt32(SManagerID) && x.empID == Convert.ToInt32(SEmployeeID) && x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(SToDate) && x.ManagerStatus != null && x.AccountManagerStatus == null).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                }
                                else if (SFromDate != null && SFromDate.Length > 0)
                                {
                                    SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && x.ManagerID == Convert.ToInt32(SManagerID) && x.empID == Convert.ToInt32(SEmployeeID) && x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(DateTime.Now) && x.ManagerStatus != null && x.AccountManagerStatus == null).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                }
                                else
                                {
                                    SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && x.ManagerID == Convert.ToInt32(SManagerID) && x.empID == Convert.ToInt32(SEmployeeID) && x.ManagerStatus != null && x.AccountManagerStatus == null).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                }
                                #endregion
                            }
                            else
                            {
                                #region fromManager
                                if (SFromDate != null && SFromDate.Length > 0 && SToDate != null && SToDate.Length > 0)
                                {
                                    SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(SToDate) && x.ManagerID == Convert.ToInt32(SManagerID) && x.ManagerStatus != null && x.AccountManagerStatus == null).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                }
                                else if (SFromDate != null && SFromDate.Length > 0)
                                {
                                    SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(DateTime.Now) && x.ManagerID == Convert.ToInt32(SManagerID) && x.ManagerStatus != null && x.AccountManagerStatus == null).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                }
                                else
                                {
                                    SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && x.ManagerID == Convert.ToInt32(SManagerID) && x.ManagerStatus != null && x.AccountManagerStatus == null).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            #region Zone and Zone with Dates
                            if (SFromDate != null && SFromDate.Length > 0 && SToDate != null && SToDate.Length > 0)
                            {
                                SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(SToDate) && (x.ROLE == "Employee" ? x.ManagerStatus != null : x.ManagerStatus == null) && x.AccountManagerStatus == null).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                            }
                            else if (SFromDate != null && SFromDate.Length > 0)
                            {
                                SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(DateTime.Now) && (x.ROLE == "Employee" ? x.ManagerStatus != null : x.ManagerStatus == null) && x.AccountManagerStatus == null).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                            }
                            else
                            {
                                SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.ZoneID == Convert.ToInt32(SZoneID) && (x.ROLE == "Employee" ? x.ManagerStatus != null : x.ManagerStatus == null) && x.AccountManagerStatus == null).OrderByDescending(x=>x.CreatedDate) select SearchExpenditures).ToList();
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        if (SStatus != null && SStatus.Length > 0)
                        {
                            SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(SToDate) && (x.ROLE == "Employee" ? x.ManagerStatus != null : x.ManagerStatus == null) && x.AccountManagerStatus == SStatus).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                        }
                        else
                        {
                            #region onlyDate
                            if (SFromDate != null && SFromDate.Length > 0 && SToDate != null && SToDate.Length > 0)
                            {
                                SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(SToDate) && (x.ROLE == "Employee" ? x.ManagerStatus != null : x.ManagerStatus == null) && x.AccountManagerStatus == null).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                            }
                            else
                            {
                                SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(DateTime.Now) && (x.ROLE == "Employee" ? x.ManagerStatus != null : x.ManagerStatus == null) && x.AccountManagerStatus == null).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                            }
                            #endregion
                        }
                    }
                    if (SearchExpenses.Count > 0)
                    {
                        var MaxDate = (from d in SearchExpenses select d.CreatedDate).Max();
                        var UserexpenseheaderforMaxId = (from userExpenseHeader1 in SearchExpenses where userExpenseHeader1.CreatedDate == Convert.ToDateTime(MaxDate) select userExpenseHeader1).ToList();
                        ViewBag.MaxId = UserexpenseheaderforMaxId[0].Id;
                        ViewBag.Search = "Search";
                    }

                    return View("EmployeeExpenditures", SearchExpenses.ToPagedList(pageNumber, SearchExpenses.Count != 0 ? SearchExpenses.Count : 1));
                }
                

                #endregion

                #region account Manager

                string EmployeeID = Session["AccountMgr"].ToString();
                userExpenditureHeaderList = userBL.GetUsersExpendituresforAccountManager(ExpType);

                var userExpenseHeader = userExpenditureHeaderList.ToList();

                #region zone
                ZonesList zones1 = new ZonesList();
                zones1 = userBL.GetZones();
                string defZones1 = "";
                List<SelectListItem> zonesList1 = new List<SelectListItem>();

                foreach (var item in zones1)
                {
                    zonesList1.Add(new SelectListItem()
                    {
                        Text = item.Zone,
                        Value = item.Id.ToString(),
                        Selected = (item.Zone == defZones1 ? true : false)
                    });
                }

                ViewBag.Zones = zonesList1;
                #endregion

                if (userExpenseHeader.Count > 0)
                {
                    var MaxDate = (from d in userExpenditureHeaderList select d.ExpenseDate).Max();
                    var UserexpenseheaderforMaxId = (from userExpenseHeader1 in userExpenseHeader where userExpenseHeader1.ExpenseDate == Convert.ToDateTime(MaxDate) select userExpenseHeader1).ToList();

                    ViewBag.MaxEmpId = UserexpenseheaderforMaxId[0].empID;
                    ViewBag.MaxDate = MaxDate;
                }


                return View("EmployeeExpenditures", userExpenseHeader.ToPagedList(pageNumber, userExpenseHeader.Count != 0 ? userExpenseHeader.Count : 1));
                #endregion
            }
            else if (Session["Manager"] != null)
            {
                #region Manager Search
               
                if (Search != null)
                {
                    #region empsinManager
                    emp = userBL.GetEmployeeByID(Session["Manager"].ToString(), null);
                    empList = userBL.GetEmployees();

                    var emps = (from empLst in empList where empLst.ManagerId == emp.Id select empLst);

                    string defEmployee = "";
                    List<SelectListItem> empsinManager = new List<SelectListItem>();

                    foreach (var item in emps)
                    {
                        empsinManager.Add(new SelectListItem()
                        {
                            Text = item.Employee_ID + "[" + item.FirstName + " " + item.LastName + "]",
                            Value = item.Id.ToString(),
                            Selected = (item.Employee_ID + "[" + item.FirstName + "]" == defEmployee ? true : false)
                        });
                    }

                    ViewBag.EmployeesByManager = empsinManager;

                    #endregion


                    userExpenditureHeaderList = userBL.GetAllEmployeesSubmittedExpenditureRecords();

                    var SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList select SearchExpenditures).ToList();

                    if (SEmployeeID != null && SEmployeeID.Length > 0)
                    {
                        if (SFromDate != null && SFromDate.Length > 0 && SToDate != null && SToDate.Length > 0)
                        {
                            SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(SToDate) && x.Status == true && x.ManagerStatus == null && x.empID == Convert.ToInt32(SEmployeeID)).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                        }
                        else if (SFromDate != null && SFromDate.Length > 0)
                        {
                            SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(DateTime.Now) && x.Status == true && x.ManagerStatus == null && x.empID == Convert.ToInt32(SEmployeeID)).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                        }
                        else
                        {
                            SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.Status == true && x.ManagerStatus == null && x.empID == Convert.ToInt32(SEmployeeID)).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                        }
                    }
                    else if(SFromDate != null && SFromDate.Length > 0 && SToDate != null && SToDate.Length > 0)
                    {
                        SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(SToDate) && x.Status == true && x.ManagerStatus == null && x.ManagerID == emp.Id).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                    }
                    else if (SFromDate != null && SFromDate.Length > 0)
                    {
                        SearchExpenses = (from SearchExpenditures in userExpenditureHeaderList.Where(x => x.FromDate >= Convert.ToDateTime(SFromDate) && x.FromDate <= Convert.ToDateTime(DateTime.Now) && x.Status == true && x.ManagerStatus == null && x.ManagerID == emp.Id).OrderByDescending(x => x.CreatedDate) select SearchExpenditures).ToList();
                    }


                    SearchExpenses = (from SearchExpenditureslist in SearchExpenses.Where(x => x.ROLE == "Employee") select SearchExpenditureslist).ToList();

                    if (SearchExpenses.Count > 0)
                    {
                        var MaxDate = (from d in SearchExpenses select d.CreatedDate).Max();
                        var UserexpenseheaderforMaxId = (from userExpenseHeader1 in SearchExpenses where userExpenseHeader1.CreatedDate == Convert.ToDateTime(MaxDate) select userExpenseHeader1).ToList();
                        ViewBag.MaxId = UserexpenseheaderforMaxId[0].Id;
                        ViewBag.Search = "Search";
                    }

                  


                    return View("EmployeeExpenditures", SearchExpenses.ToPagedList(pageNumber, SearchExpenses.Count != 0 ? SearchExpenses.Count : 1));

                }

                #endregion

                #region manager
                string EmployeeID = Session["Manager"].ToString();
                emp = userBL.GetEmployeeByID(Session["Manager"].ToString(), null);

                #region empsinManager

                empList = userBL.GetEmployees();

                var emps1 = (from empLst in empList where empLst.ManagerId == emp.Id select empLst);

                string defEmployee1 = "";
                List<SelectListItem> empsinManager1 = new List<SelectListItem>();

                foreach (var item in emps1)
                {
                    empsinManager1.Add(new SelectListItem()
                    {
                        Text = item.Employee_ID + "[" + item.FirstName + " " + item.LastName + "]",
                        Value = item.Id.ToString(),
                        Selected = (item.Employee_ID + "[" + item.FirstName + "]" == defEmployee1 ? true : false)
                    });
                }

                ViewBag.EmployeesByManager = empsinManager1;

                #endregion

                userExpenditureHeaderList = userBL.GetUsersExpendituresforManager(emp.Id, ExpType);
                var userExpenseHeader = userExpenditureHeaderList.ToList();

                if (userExpenseHeader.Count > 0)
                {
                    var MaxDate = (from d in userExpenditureHeaderList select d.ExpenseDate).Max();
                    var UserexpenseheaderforMaxId = (from userExpenseHeader1 in userExpenseHeader where userExpenseHeader1.ExpenseDate == Convert.ToDateTime(MaxDate) select userExpenseHeader1).ToList();

                    ViewBag.MaxId = UserexpenseheaderforMaxId[0].Id;
                    ViewBag.MaxEmpId = UserexpenseheaderforMaxId[0].empID;
                    ViewBag.MaxDate = MaxDate;
                }
                return View("EmployeeExpenditures", userExpenseHeader.ToPagedList(pageNumber, userExpenseHeader.Count != 0 ? userExpenseHeader.Count :1));
                #endregion
            }
            else
                return RedirectToAction("Signin", "Home");

        }

        public ActionResult GroupExpenses(DateTime? MaxDate, string MaxEmpId)
        {
            UserBL userBL = new UserBL();
            UserExpensesHeaderList userExpenseHeaderList = new UserExpensesHeaderList();
            UserExpenseDetailsList useExpenseDetailsList = new UserExpenseDetailsList();
            UserExpensesHeaderList objUserExpenseList = new UserExpensesHeaderList();
            ServiceOrderNos_List serviceorderNosList = new ServiceOrderNos_List();

            if (Session["Manager"] != null)
            {
                if (MaxDate.ToString().Length > 0 && MaxEmpId.Length > 0)
                {
                    #region Group of Records
                    userExpenseHeaderList = userBL.GetUserExpenses(Convert.ToInt32(MaxEmpId), Convert.ToDateTime(MaxDate));
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
                        userExpenseHeaderList[i].empID = Convert.ToInt32(MaxEmpId);
                        userExpenseHeaderList[i].ExpenseDate = Convert.ToDateTime(MaxDate);
                        objUserExpenseList.Add(userExpenseHeaderList[i]);

                        ViewBag.profilepath = useExpenseDetailsList[0].ProfilePicturePath != null ? "../../Content/ProfileImg/" + useExpenseDetailsList[0].ProfilePicturePath : "../../Content/Images/prf-no-img.png";
                        ViewBag.Fullname = useExpenseDetailsList[0].FullName.ToUpperInvariant();
                    }
                    #endregion

                    if (objUserExpenseList.Count > 0)
                    {
                        return PartialView("GroupExpenses", objUserExpenseList);
                    }
                    else
                    {
                        return PartialView("GroupExpenses", objUserExpenseList);
                    }
                }
                else
                {
                    return View("GroupExpenses", objUserExpenseList);
                }
            }
            else if (Session["AccountMgr"] != null)
            {
                if (MaxDate.ToString().Length > 0 && MaxEmpId.Length > 0)
                {
                    #region Group of Records
                    userExpenseHeaderList = userBL.GetManagerActionExpenses(Convert.ToInt32(MaxEmpId), Convert.ToDateTime(MaxDate));
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
                        userExpenseHeaderList[i].empID = Convert.ToInt32(MaxEmpId);
                        userExpenseHeaderList[i].ExpenseDate = Convert.ToDateTime(MaxDate);
                        objUserExpenseList.Add(userExpenseHeaderList[i]);

                        ViewBag.profilepath = useExpenseDetailsList[0].ProfilePicturePath != null ? "../../Content/ProfileImg/" + useExpenseDetailsList[0].ProfilePicturePath : "../../Content/Images/profiles/avatar_small.jpg";
                        ViewBag.Fullname = useExpenseDetailsList[0].FullName;
                        ViewBag.UpheldAmount = useExpenseDetailsList[0].UpheldAmount;
                        ViewBag.AdvanceAmount = useExpenseDetailsList[0].AdvanceAmount;
                        ViewBag.TravelDeskAmount = useExpenseDetailsList[0].TravelDeskAmount;
                    }
                    #endregion

                    if (objUserExpenseList.Count > 0)
                    {
                        return PartialView("GroupExpenses", objUserExpenseList);
                    }
                    else
                    {
                        return PartialView("GroupExpenses", objUserExpenseList);
                    }

                }
                else
                {
                    return View("GroupExpenses", objUserExpenseList);
                }
            }
            else
                return RedirectToAction("Signin", "Home");
        }

        [HttpPost]
        public ActionResult Payments(string EmpID, string PaidAmount, string SDate, string EDate, string HeaderIDFromSearch)
        {
            UserBL userbl = new UserBL();
            UserExpensesHeaderList objUserExpenseHeaderList = new UserExpensesHeaderList();
            DateTime ExpenditureSDate = Convert.ToDateTime(SDate);
            DateTime ExpenditureEDate = Convert.ToDateTime(EDate);

            Single PaidSpanAmount = 0;

            Single PendingAmount = 0;

            if (Session["AccountMgr"] != null)
            {
                if (!(string.IsNullOrEmpty(HeaderIDFromSearch)))
                {
                    int? result = userbl.UpdatePaidAmount(HeaderIDFromSearch, PaidAmount);
                }
                else
                {
                    #region PaymentProcess For Multiple

                    string HeaderID = "";
                    objUserExpenseHeaderList = userbl.GetEmployeeSpanRecords(ExpenditureSDate, ExpenditureEDate, EmpID);

                    for (int i = 0; i < objUserExpenseHeaderList.Count; i++)
                    {
                        HeaderID = objUserExpenseHeaderList[i].Id.ToString();

                        PendingAmount = Convert.ToSingle(objUserExpenseHeaderList[i].PaidPendingAmount);

                        if (Convert.ToSingle(PaidAmount) <= PendingAmount)
                        {
                            int? result = userbl.UpdatePaidAmount(HeaderID, PaidAmount);
                            PaidAmount = "0";
                        }
                        else
                        {
                            int? result = userbl.UpdatePaidAmount(HeaderID, PendingAmount.ToString());
                            PaidAmount = (Convert.ToSingle(PaidAmount) - PendingAmount).ToString();
                        }
                    }

                    #endregion
                }

            }

            return Json(new { Available = "Payment Success", Text = "Payment Success" });
        }

        public ActionResult ChangePassword(int? id)
        {
            ViewBag.id = id;
            return View();
        }
        
        [HttpPost]
        public ActionResult changepassword(string password,int? id)
        {
            UserBL userBL = new UserBL();
        
            object newpassword = new object();

            newpassword = userBL.updatepassword(password,id);
            if (newpassword == null)
            {
                userBL.changepassword(password, id);
                return Json(new { Available = true, Text = "You have successfully changed your Password!!!" });
            }
            else
            {
                return Json(new { Available = false, Text = "Your request not submitted" });
            }

           
        }
        
        [HttpGet]
        public ActionResult VerifyOldPassword(string password,int? id)
        {
            UserBL userBL = new UserBL();
            object oldpassword = new object();

            oldpassword = userBL.VerifyOldPassword(password,id);

            return Json(oldpassword, JsonRequestBehavior.AllowGet);
        }

        public ActionResult HR()
        {
            Session["url"] = "/Shared/HR";
            return View();
        }

        public ActionResult EmployeeAssetsList(string sortOrder, string currentFilter, string searchString, int? Page, int? id)
        {
            UserBL userbl = new UserBL();
            EmployeeAssetsList objEmployeeAssetsList = new EmployeeAssetsList();

            objEmployeeAssetsList = userbl.GetEmployeeAssets();

            int pageSize = 7;
            int pageNumber = (Page ?? 1);
            Session["url"] = "/Shared/EmployeeAssetsList";
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                Page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var empAsset = (from empAssetsLst in objEmployeeAssetsList select empAssetsLst);

            if (Session["AccountMgr"] != null && Session["Employee"] == null)
            {
                if (!String.IsNullOrWhiteSpace(searchString))
                {
                    empAsset = empAsset.Where(s => s.Employee_ID.Contains(searchString.Trim().ToString()));
                }

                switch (sortOrder)
                {
                    case "name_desc":
                        empAsset = empAsset.OrderByDescending(s => s.Employee_ID);
                        break;

                    default:
                        empAsset = empAsset.OrderBy(s => s.Employee_ID);
                        break;
                }
            }
            else
            {

                if (id != null || id != 0)
                {
                    Employees emp = new Employees();
                    emp = userbl.GetEmployeeByID(Session["Employee"].ToString(), null);

                    empAsset = empAsset.Where(s => s.FK_EmployeeID == emp.Id);
                }
            }

            return PartialView("EmployeeAssetsList", empAsset.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult AssetAssigned(int? flag)
        {
            EmployeeAssets objEmployeeAssets = new EmployeeAssets();

            objEmployeeAssets.flag = flag;

            Session["url"] = "/Shared/AssetAssigned";
            #region All Employees List
            Universal.BusinessEntities.EmployeesList empList = new Universal.BusinessEntities.EmployeesList();
            UserBL userBL = new UserBL();
            empList = userBL.GetEmployees();
            List<SelectListItem> employeeList = new List<SelectListItem>();
            int? defEmp = null;
            foreach (var item in empList)
            {
                employeeList.Add(new SelectListItem()
                {
                    Text = item.Employee_ID + " [" + item.FirstName + "] ",
                    Value = item.Id.ToString(),
                    Selected = (item.Id == defEmp ? true : false)
                });
            }
            ViewBag.EmployeesList = employeeList;
            #endregion

            return View(objEmployeeAssets);
        }

        [HttpPost]
        public ActionResult AssetAssigned(EmployeeAssets objEmployeeAssets)
        {
            UserBL userBL = new UserBL();
            string UserSession = (HttpContext.Session["AccountMgr"] != null) ? HttpContext.Session["AccountMgr"].ToString() : "Admin Session Null";
            objEmployeeAssets.CreatedBy = Session["AccountMgr"].ToString();

            EmployeeAssetsList objEmployeeAssetsList = new EmployeeAssetsList();
            objEmployeeAssetsList = userBL.GetEmployeeAssets();

            if (objEmployeeAssets.Id == 0 || objEmployeeAssets.Id == null)
            {
                for (int i = 0; i < objEmployeeAssetsList.Count; i++)
                {
                    if (objEmployeeAssetsList[i].FK_EmployeeID == objEmployeeAssets.FK_EmployeeID)
                    {
                        return Json(new { Available = true, Text = "Employee assets already assigned!!!" });
                    }
                }
            }

            int? err = null;
            err = userBL.CreateEmployeeAssets(objEmployeeAssets);

            if (err == null)
            {
                if (objEmployeeAssets.Id > 0)
                {
                    LogFiles logfiles = new LogFiles();
                    logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs"), UserSession.ToUpper(), "Admin Updated successfully assigned assets info");
                    return Json(new { Available = true, Text = "Updated successfully assigned assets info!!!" });
                }
                else
                {
                    LogFiles logfiles = new LogFiles();
                    logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs"), UserSession.ToUpper(), "Admin Assets assigned successfully");
                    return Json(new { Available = true, Text = "Assets assigned successfully!!!" });
                }
            }
            else
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs"), UserSession.ToUpper(), "Your request not submitted");
                return Json(new { Available = false, Text = "Your request not submitted" });
            }
        }

        public ActionResult UpdateAssetsAssigned(int? Id)
        {
            UserBL userbl = new UserBL();
            EmployeeAssetsList objEmployeeAssetsList = new EmployeeAssetsList();
            EmployeeAssets objEmployeeAssets = new EmployeeAssets();

            #region All Employees List
            Universal.BusinessEntities.EmployeesList empList = new Universal.BusinessEntities.EmployeesList();
            UserBL userBL = new UserBL();
            empList = userBL.GetEmployees();
            List<SelectListItem> employeeList = new List<SelectListItem>();
            int? defEmp = null;
            foreach (var item in empList)
            {
                employeeList.Add(new SelectListItem()
                {
                    Text = item.Employee_ID + " [" + item.FirstName + "] ",
                    Value = item.Id.ToString(),
                    Selected = (item.Id == defEmp ? true : false)
                });
            }
            ViewBag.EmployeesList = employeeList;
            #endregion

            DataTable dtEmployeeAssets = new DataTable();
            DataTable empAssets = new DataTable();

            dtEmployeeAssets = userbl.dtGetEmployeeAssets();

            empAssets = dtEmployeeAssets.Select("Id=" + Id).CopyToDataTable();

            Universal.BusinessEntities.Core.ObjectBuilder.Fill<EmployeeAssets>(empAssets, objEmployeeAssets);

            return PartialView("AssetAssigned", objEmployeeAssets);
        }


        //
        // GET: Shared/NewsList
        public ActionResult NewsList(string sortOrder, string currentFilter, string searchString, int? page)
        {
            Session["url"] = "/Shared/NewsList";
            UserBL userbl = new UserBL();
            NewsCollection newsCollection = new NewsCollection();
            newsCollection = userbl.GetNewsList();
           
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
           
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            int pageSize = 7;
            int pageNumber = (page ?? 1);
            if (Session["AccountMgr"] != null)
            {
                var AllNews = (from s in newsCollection select s);
                if (!String.IsNullOrWhiteSpace(searchString))
                {
                    AllNews = AllNews.Where(s => s.Title.Trim().ToUpper().Contains(searchString.Trim().ToUpper()));
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        AllNews = AllNews.OrderBy(s => s.ModifiedDate);
                        break;
                    default:
                        AllNews = AllNews.OrderByDescending(s => s.ModifiedDate);
                        break;
                }
                return PartialView("NewsList", AllNews.ToPagedList(pageNumber, pageSize));

            }
            else
            {
                var AllNews = (from s in newsCollection where s.Status==true select s);
                if (!String.IsNullOrWhiteSpace(searchString))
                {
                    AllNews = AllNews.Where(s => s.Title.Trim().ToUpper().Contains(searchString.Trim().ToUpper()));
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        AllNews = AllNews.OrderBy(s => s.ModifiedDate);
                        break;
                    default:
                        AllNews = AllNews.OrderByDescending(s => s.ModifiedDate);
                        break;
                }
                return PartialView("NewsList", AllNews.ToPagedList(pageNumber, pageSize));

            }
        
        }


        //
        // GET: Shared/JobView
        public ActionResult NewsView(int? id)
        {
            Session["url"] = "/Shared/NewsView/" + id;
            UserBL userbl = new UserBL();
            News news = new News();
            news = userbl.NewsDetailsById(id);
            return View(news);
        }

        public ActionResult SpanAmount(string SDate, string EDate, string FK_EmpID)
        {
            decimal? SpanAmount = 0;
            DateTime ExpenditureSDate = Convert.ToDateTime(SDate);
            DateTime ExpenditureEDate = Convert.ToDateTime(EDate);
            object result = new object();
            UserBL objUserBl = new UserBL();

            result = objUserBl.GetSpanAmountForEmmployee(ExpenditureSDate, ExpenditureEDate, Convert.ToInt32(FK_EmpID));

            if (!object.Equals(result,0) && !string.IsNullOrEmpty(result.ToString()))
            {
                //if (Convert.ToInt32(result) > 0)
                //{
                    SpanAmount = Convert.ToDecimal(result);    
                //}
            }

            return Json(SpanAmount, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewTravelTicketInformation(int? id)
        {
            UserBL userBL = new UserBL();
            TravelUplaodTickets travelUpload = new TravelUplaodTickets();
            travelUpload = userBL.GetTravelTicketsListById(id);
            TravelDesk travelDesk = new Universal.BusinessEntities.TravelDesk();
            //TravelDesk tdesklist = new TravelDesk();
            travelDesk = userBL.GetTravelDesk(id);
            Universal.BusinessEntities.EmployeesList empList = new Universal.BusinessEntities.EmployeesList();
            empList = userBL.GetEmployees();
            List<SelectListItem> employeeList = new List<SelectListItem>();
            foreach (var item in empList)
            {
                employeeList.Add(new SelectListItem()
                {
                    Text = item.Employee_ID + " [" + item.FirstName + "] ",
                    Value = item.Id.ToString(),
                    Selected = (item.Id == travelDesk.FK_EmployeeId ? true : false)
                });



            }
            //var employeeids = (from s in empList select s.Employee_ID);
            ViewBag.empList = employeeList;
            TravelDeskList objtravl = new TravelDeskList();
            //TravelDesk travelDesk = new Universal.BusinessEntities.TravelDesk();
            // objtravl[1].Id = id;
            travelDesk.travelTicketsCollection = travelUpload;

            return View(travelDesk);
        }
        //public ActionResult Download(TravelUploadTicket travelUploadticket)
        //{
        //    UserBL userBL = new UserBL();



        //    return View();
        //}
    }
}
