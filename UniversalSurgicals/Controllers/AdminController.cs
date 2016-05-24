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
using System.IO;
using System.Net;
using System.Data.OleDb;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;


namespace UniversalSurgicals.Controllers
{
    [OutputCacheAttribute(VaryByParam = "*", Duration = 1)] // will be applied to all actions in MyController, unless those actions override with their own decoration
    public class AdminController : AdminSessionCheckingController
    {
        UserBL userBL = new UserBL();

        [HttpGet]
        public ActionResult AdminDashBoard()
        {
            UserBL userBL = new UserBL();
            Employees emp = new Employees();
            emp = userBL.GetEmployeeByID(Session["AccountMgr"].ToString(), null);

            return View(emp);
        }

        [HttpGet]
        public ActionResult CreateEmployee()
        {
            Session["url"] = "/Admin/CreateEmployee";

            #region gender

            List<SelectListItem> gendr = new List<SelectListItem>();

            SelectListItem _mList = new SelectListItem();
            //_mList = new SelectListItem() { Text = "Select Gender", Value = "0" };
            //gendr.Insert(0, _mList);
            _mList = new SelectListItem() { Text = "Male", Value = "M" };
            gendr.Insert(0, _mList);
            _mList = new SelectListItem() { Text = "Female", Value = "F" };
            gendr.Insert(1, _mList);

            ViewBag.Genders = gendr;

            #endregion

            #region zones list
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

            #region states list

            StatesList states = new StatesList();
            states = userBL.GetStates();

            string defStates = "";

            List<SelectListItem> statesList = new List<SelectListItem>();

            foreach (var item in states)
            {
                statesList.Add(new SelectListItem()
                {
                    Text = item.StateName,
                    Value = item.Id.ToString(),
                    Selected = (item.StateName == defStates ? true : false)

                });

            }

            ViewBag.States = statesList;

            #endregion

            #region usergroups

            UserGroupsList users = new UserGroupsList();

            users = userBL.GetUserGroups();

            string defUser = "";

            List<SelectListItem> usersList = new List<SelectListItem>();

            foreach (var item in users)
            {
                usersList.Add(new SelectListItem()
                {
                    Text = item.UserGroup_ID,
                    Value = item.Id.ToString(),
                    Selected = (item.UserGroup_ID == defUser ? true : false)
                });
            }

            ViewBag.UserGroups = usersList;

            #endregion

            #region manager and account manager
            Universal.BusinessEntities.EmployeesList empList = new Universal.BusinessEntities.EmployeesList();
            empList = userBL.GetEmployees();


            var aclist = from q in empList where q.FK_UserGroup == 1 select q;
            List<SelectListItem> accountmgrs = new List<SelectListItem>();
            foreach (var item in aclist)
            {
                accountmgrs.Add(new SelectListItem()
                {
                    Text = item.Employee_ID + " [" + item.FirstName + "] ",
                    Value = item.Id.ToString(),
                    Selected = (item.Employee_ID == "" ? true : false)
                });
            }
            //var employeeids = (from s in empList select s.Employee_ID);
            ViewBag.AccountManagersList = accountmgrs;



            //var employeeids = (from s in empList select s.Employee_ID);
            ViewBag.AccountManagersList = accountmgrs;

            #endregion




            var magrlist = from q in empList where q.FK_UserGroup == 2 select q;

            List<SelectListItem> mgrs = new List<SelectListItem>();
            foreach (var item in magrlist)
            {
                mgrs.Add(new SelectListItem()
                {
                    Text = item.Employee_ID + " [" + item.FirstName + "] ",
                    Value = item.Id.ToString(),
                    Selected = (item.Employee_ID == "" ? true : false)
                });
            }


            ViewBag.ManagersList = mgrs;
            return View();
        }

        public ActionResult FillCities(int StateId)
        {
            CitiesList cities = new CitiesList();
            cities = userBL.GetCities();
            var city = cities.Where(m => m.FK_StateId == StateId).Select(m => new { Text = m.CityName, Value = m.Id });
            return Json(city, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateEmployee(Employees emp)
        {
            UserBL userBL = new UserBL();
            string UserSession = (HttpContext.Session["AccountMgr"] != null) ? HttpContext.Session["AccountMgr"].ToString() : "Admin Session Null";
            emp.CreatedBy = Session["AccountMgr"].ToString();
            if (emp.Temimagepath != "" && emp.Temimagepath != null)
            {
                emp.Temimagepath = RemoveSpecialCharactersSpaces(DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + "_" + emp.Employee_ID + "_") + emp.Temimagepath;

                Session["ProfileImage"] = emp.Temimagepath;
            }

            if (emp.SameAddress == true)
            {
                emp.TempAddress = emp.PermanentAddress;
                emp.FK_TempState = emp.FK_PermanentState;
                emp.FK_TempCity = emp.FK_PermanentCity;
                emp.TempPincode = emp.PermanentPincode;
                emp.CurrentCityname = emp.PermanentCityname;
            }

            #region
            if (Request.Browser.Type.ToUpper().Contains("IE"))
            {
                if (Request.Browser.MajorVersion < 10)
                {
                    Session["ProfileImage"] = emp.Employee_ID;
                }
            }
            #endregion

            #region
            if (!Request.Browser.Type.ToUpper().Contains("IE") && (emp.Temimagepath == "" || emp.Temimagepath == null))
            {

                Session["ProfileImage"] = null;
            }
            #endregion



            int? err = userBL.CreateEmployee(emp, 1);
            if (err == null)
            {
                userBL.RegistrationMail(emp);
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "Admin successfully created a new employee");
                return Json(new { Available = true, Text = "You have successfully created a new Employee!!!" });
            }
            else
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "Admin fail for creating a new employee ");
                return Json(new { Available = false, Text = "Your request not submitted" });
            }
        }

        public ActionResult EmployeeExpenditure()
        {
            Session["url"] = "/Admin/EmployeeExpenditure";

            return View();
        }

        [HttpGet]
        public ActionResult CreateUserGroup()
        {

            return PartialView("CreateUserGroup");

        }

        [HttpPost]
        public ActionResult CreateUserGroup(UserGroups userGroup)
        {

            UserBL userBL = new UserBL();
            string UserSession = (HttpContext.Session["AccountMgr"] != null) ? HttpContext.Session["AccountMgr"].ToString() : "Admin Session Null";
            userGroup.CreatedBy = Session["AccountMgr"].ToString();
            int? err = userBL.CreateUserGroup(userGroup);
            if (err == null)
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "Admin successfully created a new user group");
                return Json(new { Available = true, Text = "You have successfully created a new user group!!!" });
            }
            else
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "not created a new user group");
                return Json(new { Available = false, Text = "Your request not submitted" });
            }
        }

        [HttpGet]
        public ActionResult CreateExpenseType()
        {

            return PartialView("CreateExpenseType");

        }

        [HttpPost]
        public ActionResult CreateExpenseType(ExpenseType expensetype)
        {

            UserBL userBL = new UserBL();
            string UserSession = (HttpContext.Session["AccountMgr"] != null) ? HttpContext.Session["AccountMgr"].ToString() : "Admin Session Null";
            expensetype.CreatedBy = Session["AccountMgr"].ToString();
            int? err = null;
            err = userBL.CreateExpenseType(expensetype);
            if (err == null)
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "Admin successfully created a new Expence Type");
                return Json(new { Available = true, Text = "You have successfully created a new Expense Type!!!" });
            }
            else
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "not created a new Expence Type");
                return Json(new { Available = false, Text = "Your request not submitted" });
            }
        }


        [HttpGet]
        public ActionResult CreateNewZone()
        {

            return PartialView("CreateNewZone");
        }

        [HttpPost]
        public ActionResult CreateNewZone(Zones zones)
        {

            UserBL userBL = new UserBL();
            string UserSession = (HttpContext.Session["AccountMgr"] != null) ? HttpContext.Session["AccountMgr"].ToString() : "Admin Session Null";
            zones.CreatedBy = Session["AccountMgr"].ToString();
            int? err = userBL.CreateNewZone(zones);
            if (err == null)
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "Admin successfully created a new Zone");
                return Json(new { Available = true, Text = "You have successfully created a new zone!!!" });
            }
            else
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "not created a new Zone");
                return Json(new { Available = false, Text = "Your request not submitted" });
            }
        }


        public ActionResult ZonesList(string sortOrder, string currentFilter, string searchString, int? page)
        {
            Session["url"] = "/Admin/ZonesList";
            UserBL userbl = new UserBL();
            Universal.BusinessEntities.ZonesList zoneslist = new Universal.BusinessEntities.ZonesList();
            zoneslist = userbl.GetZonesList();
            var zones = (from s in zoneslist select s);


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


            if (!String.IsNullOrWhiteSpace(searchString))
            {
                zones = zones.Where(s => s.Zone.Trim().ToUpper().Contains(searchString.Trim().ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    zones = zones.OrderByDescending(s => s.Zone);
                    break;

                default:
                    zones = zones.OrderBy(s => s.Zone);
                    break;
            }

            int pageSize = 7;
            int pageNumber = (page ?? 1);

            return PartialView("ZonesList", zones.ToPagedList(pageNumber, pageSize));


        }

        [HttpGet]
        public ActionResult UserGroupsList(string sortOrder, string currentFilter, string searchString, int? page)
        {
            Session["url"] = "/Admin/UserGroupsList";

            UserBL userbl = new UserBL();
            Universal.BusinessEntities.UserGroupsList UserGroupList = new Universal.BusinessEntities.UserGroupsList();
            UserGroupList = userbl.GetUserGroupList();
            var UserGroups = (from s in UserGroupList select s);



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


            if (!String.IsNullOrWhiteSpace(searchString))
            {
                UserGroups = UserGroups.Where(s => s.UserGroup_ID.Trim().ToUpper().Contains(searchString.Trim().ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    UserGroups = UserGroups.OrderByDescending(s => s.UserGroup_ID);
                    break;

                default:
                    UserGroups = UserGroups.OrderBy(s => s.UserGroup_ID);
                    break;
            }

            int pageSize = UserGroupList.Count;
            int pageNumber = (page ?? 1);

            return PartialView("UserGroupsList", UserGroups.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult UpdateZone(int? id)
        {
            Session["url"] = "/Admin/ZonesList";
            Zones zones = new Zones();
            zones = userBL.GetZone(id);
            return PartialView("UpdateZone", zones);

        }

        [HttpPost]
        public ActionResult UpdateZone(Zones zones)
        {
            Session["url"] = "/Admin/ZonesList";
            UserBL userBL = new UserBL();
            string UserSession = (HttpContext.Session["AccountMgr"] != null) ? HttpContext.Session["AccountMgr"].ToString() : "Admin Session Null";
            zones.ModifiedBy = Session["AccountMgr"].ToString();
            int? err = userBL.CreateNewZone(zones);
            if (err == null)
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "Admin successfully updated the zone");
                return Json(new { Available = true, Text = "You have successfully updated the zone!!!" });
            }
            else
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "not updated the zone");
                return Json(new { Available = false, Text = "Your request not submitted" });
            }

        }


        [HttpGet]
        public ActionResult UpdateExpenseType(int? id)
        {
            Session["url"] = "/Admin/ExpenseType";
            ExpenseType expensetype = new ExpenseType();
            expensetype = userBL.GetExpense(id);
            return PartialView("UpdateExpenseType", expensetype);
        }

        [HttpPost]
        public ActionResult UpdateExpenseType(ExpenseType expense)
        {
            Session["url"] = "/Admin/ExpenseType";

            UserBL userBL = new UserBL();
            string UserSession = (HttpContext.Session["AccountMgr"] != null) ? HttpContext.Session["AccountMgr"].ToString() : "Admin Session Null";
            expense.ModifiedBy = Session["AccountMgr"].ToString();
            int? err = null;
            err = userBL.CreateExpenseType(expense);
            if (err == null)
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "Admin successfully updated the ExpenseType");
                return Json(new { Available = true, Text = "You have successfully updated the ExpenseType!!!" });
            }
            else
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "not updated the ExpenseType");
                return Json(new { Available = false, Text = "Your request not submitted" });
            }
        }


        [HttpGet]
        public ActionResult UpdateUserGroup(int? id)
        {
            Session["url"] = "/Admin/UserGroupsList";
            UserGroups usergroups = new UserGroups();
            usergroups = userBL.GetUserGroup(id);
            return PartialView("_UpdateUserGroup", usergroups);
        }

        [HttpPost]
        public ActionResult UpdateUserGroup(UserGroups usergroups)
        {
            Session["url"] = "/Admin/UserGroupsList";

            UserBL userBL = new UserBL();
            string UserSession = (HttpContext.Session["AccountMgr"] != null) ? HttpContext.Session["AccountMgr"].ToString() : "Admin Session Null";
            usergroups.ModifiedBy = Session["AccountMgr"].ToString();
            int? err = userBL.CreateUserGroup(usergroups);
            if (err == null)
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "Admin successfully updated the user group");
                return Json(new { Available = true, Text = "You have successfully updated the user group!!!" });
            }
            else
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "not updated the user group");
                return Json(new { Available = false, Text = "Your request not submitted" });
            }
        }


        [HttpGet]
        public ActionResult UpdateEmployee(int? Id)
        {
            Session["url"] = "/Shared/EmployeeList";

            Employees emp = new Employees();

            emp = userBL.GetEmployeeByID(null, Id);

            //return View(emp);
            #region Permanent states list

            StatesList states = new StatesList();
            states = userBL.GetStates();

            string defStates = "";

            List<SelectListItem> statesList = new List<SelectListItem>();

            foreach (var item in states)
            {
                statesList.Add(new SelectListItem()
                {
                    Text = item.StateName,
                    Value = item.Id.ToString(),
                    Selected = (item.StateName == defStates ? true : false)

                });

            }

            ViewBag.States = statesList;

            #endregion
            #region Temprory states list

            StatesList sts = new StatesList();
            sts = userBL.GetStates();

            string defsts = "";

            List<SelectListItem> stsList = new List<SelectListItem>();

            foreach (var item in sts)
            {
                stsList.Add(new SelectListItem()
                {
                    Text = item.StateName,
                    Value = item.Id.ToString(),
                    Selected = (item.StateName == defsts ? true : false)

                });

            }

            ViewBag.States1 = stsList;

            #endregion
            #region gender

            List<SelectListItem> gendr = new List<SelectListItem>();

            SelectListItem _mList = new SelectListItem();
            //_mList = new SelectListItem() { Text = "Select Gender", Value = "0" };
            //gendr.Insert(0, _mList);
            _mList = new SelectListItem() { Text = "Male", Value = "M" };
            gendr.Insert(0, _mList);
            _mList = new SelectListItem() { Text = "Female", Value = "F" };
            gendr.Insert(1, _mList);

            ViewBag.Genders = gendr;

            #endregion

            #region zones list
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



            #region Permanent Cities list
            UserBL userbl = new UserBL();
            CitiesList cities = new CitiesList();
            //string defCities = "";
            cities = userbl.GetCities();
            //var citiesforState = (from s in cities where s.Id==emp.FK_PermanentCity select s);
            Universal.BusinessEntities.CitiesList citieslist = new Universal.BusinessEntities.CitiesList();

            List<SelectListItem> CitiesList = new List<SelectListItem>();
            foreach (var item in cities)
            {
                CitiesList.Add(new SelectListItem()
                {
                    Text = item.CityName,
                    Value = item.Id.ToString(),
                    Selected = (item.Id == emp.FK_PermanentCity ? true : false)
                });
            }

            ViewBag.CitiesList = CitiesList;
            //return PartialView("UpdateEmployee", cities);
            #endregion

            #region Temporary Cities list
            UserBL userbl1 = new UserBL();
            //CitiesList tempcities = new CitiesList();
            CitiesList cities1 = new CitiesList();
            //string defCities = "";
            cities1 = userbl.GetCities();
            //var citiesforState = (from s in cities where s.Id==emp.FK_PermanentCity select s);
            Universal.BusinessEntities.CitiesList citieslist1 = new Universal.BusinessEntities.CitiesList();

            List<SelectListItem> CitiesList1 = new List<SelectListItem>();
            foreach (var item in cities1)
            {
                CitiesList1.Add(new SelectListItem()
                {
                    Text = item.CityName,
                    Value = item.Id.ToString(),
                    Selected = (item.Id == emp.FK_TempCity ? true : false)
                });
            }

            ViewBag.CitiesList2 = CitiesList1;
            //return PartialView("UpdateEmployee", cities);
            #endregion
            #region usergroups

            UserGroupsList users = new UserGroupsList();

            users = userBL.GetUserGroups();

            string defUser = "";

            List<SelectListItem> usersList = new List<SelectListItem>();

            foreach (var item in users)
            {
                usersList.Add(new SelectListItem()
                {
                    Text = item.UserGroup_ID,
                    Value = item.Id.ToString(),
                    Selected = (item.UserGroup_ID == defUser ? true : false)
                });
            }

            ViewBag.UserGroups = usersList;

            #endregion

            #region manager and account manager
            Universal.BusinessEntities.EmployeesList empList = new Universal.BusinessEntities.EmployeesList();
            empList = userBL.GetEmployees();


            var aclist = from q in empList where q.FK_UserGroup == 1 select q;
            List<SelectListItem> accountmgrs = new List<SelectListItem>();
            foreach (var item in aclist)
            {
                accountmgrs.Add(new SelectListItem()
                {
                    Text = item.Employee_ID + " [" + item.FirstName + "] ",
                    Value = item.Id.ToString(),
                    Selected = (item.Employee_ID == "" ? true : false)
                });
            }
            //var employeeids = (from s in empList select s.Employee_ID);
            ViewBag.AccountManagersList = accountmgrs;



            var mgrslist = from q in empList where q.FK_UserGroup == 2 select q;
            List<SelectListItem> managers = new List<SelectListItem>();
            foreach (var item in mgrslist)
            {
                managers.Add(new SelectListItem()
                {
                    Text = item.Employee_ID + " [" + item.FirstName + "] ",
                    Value = item.Id.ToString(),
                    Selected = (item.Employee_ID == "" ? true : false)
                });
            }
            //var employeeids = (from s in empList select s.Employee_ID);
            ViewBag.ManagersList = managers;

            #endregion

            #region Profile path


            #endregion

            if (emp.PermanentAddress == emp.TempAddress && emp.FK_PermanentState == emp.FK_TempState && emp.TempPincode == emp.PermanentPincode && emp.FK_TempCity == emp.FK_PermanentCity && emp.PermanentCityname == emp.CurrentCityname)
            {
                emp.SameAddress = true;
            }
            else
            {
                emp.SameAddress = false;
            }

            return View(emp);
        }
        [HttpPost]
        public ActionResult UpdateEmployee(Employees employees)
        {

            UserBL userBL = new UserBL();
            employees.ModifiedBy = Session["AccountMgr"].ToString();
            string UserSession = (HttpContext.Session["AccountMgr"] != null) ? HttpContext.Session["AccountMgr"].ToString() : "Admin Session Null";
            if (employees.Temimagepath != "" && employees.Temimagepath != null)
            {
                employees.Temimagepath = RemoveSpecialCharactersSpaces(DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + "_" + employees.Employee_ID + "_") + employees.Temimagepath;

                Session["ProfileImage"] = employees.Temimagepath;
            }

            else if (Request.Form["actualpic"] != "" && Request.Form["actualpic"] != null)
            {
                employees.Temimagepath = Request.Form["actualpic"];
            }
            if (employees.SameAddress == true)
            {
                employees.TempAddress = employees.PermanentAddress;
                employees.FK_TempState = employees.FK_PermanentState;
                employees.FK_TempCity = employees.FK_PermanentCity;
                employees.TempPincode = employees.PermanentPincode;
                employees.CurrentCityname = employees.PermanentCityname;
            }


            #region
            if (Request.Browser.Type.ToUpper().Contains("IE"))
            {
                if (Request.Browser.MajorVersion < 10)
                {
                    Session["ProfileImage"] = employees.Employee_ID;
                }
            }

            #endregion

            #region
            if (!Request.Browser.Type.ToUpper().Contains("IE") && (employees.Temimagepath == "" || employees.Temimagepath == null))
            {
                Session["ProfileImage"] = null;
            }
            #endregion


            int? err = userBL.CreateEmployee(employees, 2);
            if (err == null)
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "Admin successfully updated profile");
                return Json(new { Available = true, Text = "You have successfully updated profile !!!" });
            }
            else
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "not updated profile");
                return Json(new { Available = false, Text = "Your request not submitted" });
            }

        }



        public ActionResult CheckEmployeeID(string EmpID)
        {
            object EmployeeID = new object();

            EmployeeID = userBL.CheckEmployeeID(EmpID);


            return Json(EmployeeID, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VerifyZoneName(string zoneName)
        {
            object zone = new object();

            zone = userBL.VerifyZoneName(zoneName);

            return Json(zone, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VerifyExpenseName(string expensename)
        {
            object expense = new object();
            expense = userBL.VerifyExpenseName(expensename);
            return Json(expense, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VerifyUserGroupID(string UserGroupID)
        {
            object usergroup = new object();
            usergroup = userBL.VerifyUserGroupID(UserGroupID);
            return Json(usergroup, JsonRequestBehavior.AllowGet);
        }



        public ActionResult VerifyPassportNumber(string passport)
        {
            object passportNum = new object();

            passportNum = userBL.VerifyPassportNumber(passport);

            return Json(passportNum, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckAadharID(string AadharID)
        {
            object Aadhar = new object();

            Aadhar = userBL.CheckAadharID(AadharID);

            return Json(Aadhar, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckPancard(string Pancard)
        {
            object pancardid = new object();

            pancardid = userBL.CheckPancard(Pancard);

            return Json(pancardid, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CheckDrivingLicenceNo(string DrivingLicence)
        {
            object drivinglicence = new object();

            drivinglicence = userBL.CheckDrivingLicenceNo(DrivingLicence);

            return Json(drivinglicence, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult ExpenseType(string sortOrder, string currentFilter, string searchString, int? page)
        {
            Session["url"] = "/Admin/ExpenseType";

            UserBL userbl = new UserBL();
            Universal.BusinessEntities.ExpenseTypeList expenseTypeList = new Universal.BusinessEntities.ExpenseTypeList();

            expenseTypeList = userbl.GetExpenseTypeList();
            var varexpenseTypeList = (from s in expenseTypeList select s);

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


            if (!String.IsNullOrWhiteSpace(searchString))
            {
                varexpenseTypeList = varexpenseTypeList.Where(s => s.ExpenseName.Trim().ToUpper().Contains(searchString.Trim().ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    varexpenseTypeList = varexpenseTypeList.OrderByDescending(s => s.ExpenseName);
                    break;

                default:
                    varexpenseTypeList = varexpenseTypeList.OrderBy(s => s.ExpenseName);
                    break;
            }



            int pageSize = 7;
            int pageNumber = (page ?? 1);
            return View("ExpenseType", varexpenseTypeList.ToPagedList(pageNumber, pageSize));

        }

        [HttpPost]
        public string UploadProfileImage(string id)
        {

            //if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            //{
            //    var pic = System.Web.HttpContext.Current.Request.Files["ProfileImage"];
            //}

            string UserSession = (HttpContext.Session["AccountMgr"] != null) ? HttpContext.Session["AccountMgr"].ToString() : "Admin Session Null";
            try
            {

                if (Session["ProfileImage"] != null)
                {

                    foreach (string file in Request.Files)
                    {

                        var fileContent = Request.Files[file];

                        if (fileContent != null && fileContent.ContentLength > 0)
                        {
                            var stream = fileContent.InputStream;
                            var fileName = Session["ProfileImage"].ToString();
                            var path = Path.Combine(Server.MapPath("~/Content/ProfileImg"), fileName);

                            using (var fileStream = System.IO.File.Create(path))
                            {
                                stream.CopyTo(fileStream);
                            }

                        }

                    }

                }



                else
                {
                    LogFiles logfiles = new LogFiles();
                    logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "Profile Image upload Failed");
                    return "Profile Image not uploaded";
                }


            }
            catch (Exception)
            {

                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "Profile Image upload Failed");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return "Profile Image upload Failed";


            }

            Session["ProfileImage"] = null;
            LogFiles logfile = new LogFiles();
            logfile.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "Profile Image uploaded successfully");
            return "Profile Image uploaded successfully";
        }



        public JsonResult AutoCompletezonlist(string term)
        {
            //var result = (from r in db.Customers
            //              where r.Country.ToLower().Contains(term.ToLower())
            //              select new { r.Country }).Distinct();


            UserBL userbl = new UserBL();
            Universal.BusinessEntities.ZonesList zoneslist = new Universal.BusinessEntities.ZonesList();
            zoneslist = userbl.GetZonesList();
            var zones = (from s in zoneslist where s.Zone.ToLower().Contains(term.ToLower()) select new { s.Zone }).Distinct();

            return Json(zones, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult PaymentConfirmation(int? Page)
        {
            int pageSize = 7;
            int pageNumber = (Page ?? 1);

            UserExpensesHeaderList objuserExpenseHeader = new UserExpensesHeaderList();

            objuserExpenseHeader = userBL.GetPaidExpenditures();

            var objPaidExpenses = (from userExpenseHeader1 in objuserExpenseHeader select userExpenseHeader1).ToList();

            return View(objPaidExpenses.ToPagedList(pageNumber, pageSize));
        }


        [HttpGet]
        public ActionResult ExpenseBlocking(string sortOrder, string currentFilter, string searchString, int? page, string status)
        {
            Session["url"] = "/Admin/ExpenseBlocking";
            UserBL userbl = new UserBL();
            ExpenseBlockedDatesList exdataesList = new ExpenseBlockedDatesList();
            exdataesList = userbl.ExpenseBlockedDatesList();

            int pageSize = 8;
            int pageNumber = (page ?? 1);

            if (status != null && status != "")
            {
                ViewBag.Status = status;
            }
            else
            {
                ViewBag.Status = "Blocked";
                status = "Blocked";
            }
            var blockedlist = (from s in exdataesList where s.Status == status select s);

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

            if (!String.IsNullOrWhiteSpace(searchString))
            {
                blockedlist = blockedlist.Where(s => s.Name.Trim().ToUpper().Contains(searchString.Trim().ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    blockedlist = blockedlist.OrderBy(s => s.BlockingFromDate);
                    break;
                default:
                    blockedlist = blockedlist.OrderByDescending(s => s.BlockingFromDate);
                    break;
            }

            return PartialView("ExpenseBlocking", blockedlist.ToPagedList(pageNumber, pageSize));

        }

        [HttpPost]
        public ActionResult UnblockDates(int? id, string comment)
        {
            Session["url"] = "/Admin/ExpenseBlocking";
            string UserSession = (HttpContext.Session["AccountMgr"] != null) ? HttpContext.Session["AccountMgr"].ToString() : "Admin Session Null";
            UserBL userBL = new UserBL();
            ExpenseBlockedDates exdates = new ExpenseBlockedDates();
            exdates.ModifiedBy = Session["AccountMgr"].ToString();
            exdates.Status = "Unblocked";
            exdates.Id = id;
            exdates.Comments = comment;
            int? err = userBL.BlockDates(exdates);
            if (err == 0)
            {
                //Employees emp = new Employees();
                //emp = userBL.GetEmployeeByID(null, id);
                //exdates.EmpMailId = emp.CompanyMailID;
                //exdates.ACMailId = emp.AccountManagerEmailID;
                //exdates.UserId = emp.Employee_ID;
                //exdates.Name = emp.FirstName;
                //exdates.ManagerMailId = emp.ManagerEmailID;
                //exdates.BlockingFromDate = DateTime.Parse(fromDate);
                //exdates.BlockingToDate = DateTime.Parse(todate);
                exdates = userBL.GetBlockedDatesById(id);
                userBL.sendmailforUnblocking(exdates);
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "Admin successfully updated the status");
                return Json(new { Available = true, Text = "You have successfully updated the status!!!" });
            }
            else
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "not updated the status");
                return Json(new { Available = false, Text = "Your request not submitted" });
            }
            return View();

        }

        [HttpPost]
        public ActionResult ProfileImageUpload()
        {



            if (Session["ProfileImage"] != null)
            {


                foreach (string file in Request.Files)
                {
                    //HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;

                    //foreach (HttpPostedFile filee in Request.Files)
                    //{
                    //    string extension = System.IO.Path.GetExtension(filee.FileName);
                    //}

                    var files = Request.Files[0];

                    string fname = files.FileName;

                    string filename = Path.GetFileName(fname);


                    var fileContent = Request.Files[file];

                    string FileExtension = filename.Substring(filename.LastIndexOf('.') + 1).ToLower();



                    if (fileContent != null && fileContent.ContentLength > 0 && (FileExtension == "jpeg" || FileExtension == "png" || FileExtension == "gif" || FileExtension == "tif" || FileExtension == "jpg") && fileContent.ContentLength <= 6144000)
                    {
                        var stream = fileContent.InputStream;

                        var fileName = RemoveSpecialCharactersSpaces(DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + "_" + Session["ProfileImage"].ToString() + "_") + filename;


                        var path = Path.Combine(Server.MapPath("~/Content/ProfileImg"), fileName);

                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }

                        Employees employee = new Employees();

                        employee.Employee_ID = Session["ProfileImage"].ToString().Trim();

                        employee.Temimagepath = fileName.Trim();

                        userBL.UpdateprofileImage(employee);
                    }




                }

            }
            Session["ProfileImage"] = null;
            return View();
        }

        [HttpGet]
        public ActionResult ExpenditureReportForAdmin(int? Page, string Zone, string Employee, string SDate, string EDate, string SoNumrOrNot)
        {
            Session["url"] = "/Admin/ExpenditureReportForAdmin";

            #region objectCreations
            UserExpensesHeaderList userExpenseHeaderList = new UserExpensesHeaderList();
            UserExpensesHeaderList objUserExpenseList = new UserExpensesHeaderList();
            UserExpenseDetailsList useExpenseDetailsList = new UserExpenseDetailsList();
            ServiceOrderNos_List serviceorderNosList = new ServiceOrderNos_List();
            UserExpensesHeaderList userExpenditureHeaderList = new UserExpensesHeaderList();
            Employees emp = new Employees();
            #endregion

            int pageNumber;
            pageNumber = (Page ?? 1);
            DateTime ExpenditureSDate = Convert.ToDateTime(SDate);
            DateTime ExpenditureEDate = Convert.ToDateTime(EDate);

            var ExpenseReports = (from expeseReports in userExpenditureHeaderList select expeseReports).ToList();

            if (SDate != null && EDate != null)
            {
                if (SoNumrOrNot == "true" || SoNumrOrNot == "false")
                {
                    #region serviceorderNos
                    userExpenditureHeaderList = userBL.GetExpenditureReportByServiceOrderNo(1);
                    if (SoNumrOrNot == "true")
                    {
                        #region withSONos
                        if (SoNumrOrNot == "true" && SDate != null && EDate != null && (string.IsNullOrEmpty(Zone)) && (string.IsNullOrEmpty(Employee)))
                        {
                            ExpenseReports = (from userExpensesList in userExpenditureHeaderList.Where(x => x.ServiceOrderNo != null && x.FromDate >= ExpenditureSDate && x.FromDate <= ExpenditureEDate) select userExpensesList).ToList();
                            ViewBag.FromServiceOrderNos = "Available";
                        }
                        else if (SoNumrOrNot == "true" && SDate != null && EDate != null && !(string.IsNullOrEmpty(Zone)) && (string.IsNullOrEmpty(Employee)))
                        {
                            ExpenseReports = (from userExpensesList in userExpenditureHeaderList.Where(x => x.ServiceOrderNo != null && x.FromDate >= ExpenditureSDate && x.FromDate <= ExpenditureEDate && x.ZoneID == Convert.ToInt32(Zone)) select userExpensesList).ToList();
                            ViewBag.FromServiceOrderNos = "AvailableWithZone";
                        }
                        else if (SoNumrOrNot == "true" && SDate != null && EDate != null && (string.IsNullOrEmpty(Zone)) && !(string.IsNullOrEmpty(Employee)))
                        {
                            ExpenseReports = (from userExpensesList in userExpenditureHeaderList.Where(x => x.ServiceOrderNo != null && x.FromDate >= ExpenditureSDate && x.FromDate <= ExpenditureEDate && x.empID == Convert.ToInt32(Employee)) select userExpensesList).ToList();
                            ViewBag.FromServiceOrderNos = "AvailableWithEmployee";
                        }
                        else
                        {
                            ExpenseReports = (from userExpensesList in userExpenditureHeaderList.Where(x => x.ServiceOrderNo != null && x.FromDate >= ExpenditureSDate && x.FromDate <= ExpenditureEDate && x.ZoneID == Convert.ToInt32(Zone) && x.empID == Convert.ToInt32(Employee)) select userExpensesList).ToList();
                            ViewBag.FromServiceOrderNos = "AvailableWithAll";
                        }
                        #endregion
                    }
                    else
                    {
                        #region WithOut SONos
                        if (SoNumrOrNot == "false" && SDate != null && EDate != null && (string.IsNullOrEmpty(Zone)) && (string.IsNullOrEmpty(Employee)))
                        {
                            ExpenseReports = (from userSOExpenseList in userExpenditureHeaderList.Where(x => x.ServiceOrderNo == null && x.ExplanationforSONo != null && x.FromDate >= ExpenditureSDate && x.FromDate <= ExpenditureEDate) select userSOExpenseList).ToList();
                            ViewBag.FromServiceOrderNos = "NotAvailable";
                        }
                        else if (SoNumrOrNot == "false" && SDate != null && EDate != null && !(string.IsNullOrEmpty(Zone)) && (string.IsNullOrEmpty(Employee)))
                        {
                            ExpenseReports = (from userSOExpenseList in userExpenditureHeaderList.Where(x => x.ServiceOrderNo == null && x.ExplanationforSONo != null && x.FromDate >= ExpenditureSDate && x.FromDate <= ExpenditureEDate && x.ZoneID == Convert.ToInt32(Zone)) select userSOExpenseList).ToList();
                            ViewBag.FromServiceOrderNos = "NotAvailableWithZone";
                        }
                        else if (SoNumrOrNot == "false" && SDate != null && EDate != null && (string.IsNullOrEmpty(Zone)) && !(string.IsNullOrEmpty(Employee)))
                        {
                            ExpenseReports = (from userSOExpenseList in userExpenditureHeaderList.Where(x => x.ServiceOrderNo == null && x.ExplanationforSONo != null && x.FromDate >= ExpenditureSDate && x.FromDate <= ExpenditureEDate && x.empID == Convert.ToInt32(Employee)) select userSOExpenseList).ToList();
                            ViewBag.FromServiceOrderNos = "NotAvailableWithEmployee";
                        }
                        else
                        {
                            ExpenseReports = (from userSOExpenseList in userExpenditureHeaderList.Where(x => x.ServiceOrderNo == null && x.ExplanationforSONo != null && x.FromDate >= ExpenditureSDate && x.FromDate <= ExpenditureEDate && x.ZoneID == Convert.ToInt32(Zone) && x.empID == Convert.ToInt32(Employee)) select userSOExpenseList).ToList();
                            ViewBag.FromServiceOrderNos = "NotAvailableWithAll";
                        }
                        #endregion
                    }
                    #endregion
                    ViewBag.SelectedSOStatus = SoNumrOrNot;
                }
                else
                {
                    if (!(string.IsNullOrEmpty(Employee)))
                    {
                        #region EmployeeExpenditure
                        userExpenditureHeaderList = userBL.GetExpenditureReport(ExpenditureSDate, ExpenditureEDate, Convert.ToInt32(Employee));

                        for (int i = 0; i < userExpenditureHeaderList.Count; i++)
                        {
                            useExpenseDetailsList = userBL.GetExpenditureDetailsByID(userExpenditureHeaderList[i].Id);

                            if (userExpenditureHeaderList[i].ServiceOrderNo == null || userExpenditureHeaderList[i].SerialNoforInstrument == null && (useExpenseDetailsList[0].ExpenseTypeDetails != "Mobile Bill" || useExpenseDetailsList[0].ExpenseTypeDetails != "Internet Bill"))
                            {
                                serviceorderNosList = userBL.GetMultipleSONOsbyExpenseHeaderID(userExpenditureHeaderList[i].Id);
                                userExpenditureHeaderList[i].objServiceOrderNosList = serviceorderNosList;
                            }

                            userExpenditureHeaderList[i].Id = userExpenditureHeaderList[i].Id;
                            userExpenditureHeaderList[i].UserExpenseDetailsList = useExpenseDetailsList;
                            objUserExpenseList.Add(userExpenditureHeaderList[i]);

                            ViewBag.profilepath = useExpenseDetailsList[0].ProfilePicturePath != null ? "../../Content/ProfileImg/" + useExpenseDetailsList[0].ProfilePicturePath : "../../Content/Images/prf-no-img.png";
                            ViewBag.Fullname = useExpenseDetailsList[0].FullName.ToUpperInvariant();

                            ViewBag.Zone = useExpenseDetailsList[0].Zone.ToString();
                            ExpenseReports = objUserExpenseList;
                            ViewBag.TotalReportAmount = userExpenditureHeaderList[0].TotalReportAmount;
                        }

                        #endregion
                        ViewBag.ForEmployee = "Employee";
                    }
                    else
                    {
                        decimal lumpsumAmountCalculation = 0;
                        #region between dates
                        userExpenditureHeaderList = userBL.GetExpenditureReport(ExpenditureSDate, ExpenditureEDate, 0);
                        DataTable dtDatesList = new DataTable();
                        dtDatesList = userBL.GetDatesListinBetween(ExpenditureSDate, ExpenditureEDate);

                        for (int j = 0; j < userExpenditureHeaderList.Count; j++)
                        {
                            if (userExpenditureHeaderList[j].LumpsumAmount != 0)
                            {
                                for (int i = 0; i < dtDatesList.Rows.Count; i++)
                                {
                                    string MonthName = dtDatesList.Rows[i]["Month"].ToString();
                                    int days = Convert.ToInt32(dtDatesList.Rows[i]["Days"].ToString());
                                    int NoofDays = Convert.ToInt32(dtDatesList.Rows[i]["NoOfDays"].ToString());
                                    int YearName = Convert.ToInt32(dtDatesList.Rows[i]["YearName"].ToString());
                                    switch (MonthName)
                                    {
                                        case "January":
                                        case "March":
                                        case "May":
                                        case "July":
                                        case "August":
                                        case "October":
                                        case "December":
                                            if (days == 31)
                                            {
                                                lumpsumAmountCalculation = lumpsumAmountCalculation + userExpenditureHeaderList[j].LumpsumAmount;
                                            }
                                            else
                                            {
                                                decimal spanAmount = userExpenditureHeaderList[j].LumpsumAmount / 31;
                                                float dayspan = Convert.ToSingle(days) / Convert.ToSingle(10);

                                                if (ExpenditureSDate.Day != 1 && ExpenditureEDate.Day != 1)
                                                {
                                                    if (days <= 29)
                                                    {
                                                        days = days + 1;
                                                    }
                                                }

                                                lumpsumAmountCalculation = lumpsumAmountCalculation + Convert.ToDecimal(Convert.ToSingle(spanAmount) * days);
                                            }
                                            break;
                                        case "February":

                                            bool leapYear = DateTime.IsLeapYear(YearName);

                                            if ((days == 28 && leapYear == false) || (days == 29 && leapYear == true))
                                            {
                                                lumpsumAmountCalculation = lumpsumAmountCalculation + userExpenditureHeaderList[j].LumpsumAmount;
                                            }
                                            else
                                            {
                                                decimal spanAmount = 0;
                                                if (NoofDays == 28)
                                                {
                                                    spanAmount = userExpenditureHeaderList[j].LumpsumAmount / 28;
                                                }
                                                else
                                                {
                                                    spanAmount = userExpenditureHeaderList[j].LumpsumAmount / 29;
                                                }

                                                float dayspan = Convert.ToSingle(days) / Convert.ToSingle(10);
                                                lumpsumAmountCalculation = lumpsumAmountCalculation + Convert.ToDecimal(Convert.ToSingle(spanAmount) * days);
                                            }
                                            break;
                                        default:
                                            if (days == 30)
                                            {
                                                lumpsumAmountCalculation = lumpsumAmountCalculation + userExpenditureHeaderList[j].LumpsumAmount;
                                            }
                                            else
                                            {
                                                decimal spanAmount = userExpenditureHeaderList[j].LumpsumAmount / 3;
                                                float dayspan = Convert.ToSingle(days) / Convert.ToSingle(10);
                                                lumpsumAmountCalculation = lumpsumAmountCalculation + Convert.ToDecimal(Convert.ToSingle(spanAmount) * dayspan);
                                            }
                                            break;
                                    }
                                }
                                userExpenditureHeaderList[j].LumpsumAmount = lumpsumAmountCalculation;
                                lumpsumAmountCalculation = 0;
                            }
                        }

                        ExpenseReports = (from expensesBetweenDates in userExpenditureHeaderList select expensesBetweenDates).ToList();

                        if (!(string.IsNullOrEmpty(Zone)))
                        {
                            ExpenseReports = (from expsesBetweenByZone in userExpenditureHeaderList.Where(x => x.ZoneID
 == Convert.ToInt32(Zone))
                                              select expsesBetweenByZone).ToList();
                        }

                        ViewBag.BetweenDates = "betweenDates";

                        if (userExpenditureHeaderList.Count > 0)
                        {
                            ViewBag.TotalReportAmount = userExpenditureHeaderList[0].TotalReportAmount;
                        }

                        #endregion
                    }
                }
            }

            ViewBag.FromDate = SDate;
            ViewBag.ToDate = EDate;

            #region All zones list
            ZonesList zones = new ZonesList();
            zones = userBL.GetZones();
            int defZones = 0;
            if (!(string.IsNullOrEmpty(Zone)))
            {
                defZones = Convert.ToInt32(Zone);
            }
            List<SelectListItem> zonesList = new List<SelectListItem>();
            foreach (var item in zones)
            {
                zonesList.Add(new SelectListItem()
                {
                    Text = item.Zone,
                    Value = item.Id.ToString(),
                    Selected = (item.Id == defZones ? true : false)
                });
            }
            ViewBag.ZonesList = zonesList;
            #endregion

            #region All Employees List
            Universal.BusinessEntities.EmployeesList empList = new Universal.BusinessEntities.EmployeesList();
            empList = userBL.GetEmployees();
            List<SelectListItem> employeeList = new List<SelectListItem>();
            int? defEmp = null;
            if (!(string.IsNullOrEmpty(Employee)))
            {
                defEmp = Convert.ToInt32(Employee);
            }
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


            return View(ExpenseReports.ToPagedList(pageNumber, ExpenseReports.Count != 0 ? ExpenseReports.Count : 1));
        }




        [HttpGet]
        public ActionResult LeaveReports(int? Page, string Zone, string Employee, string SDate, string EDate, string Status)
        {
            #region objectCreations
            LeaveList objLeaveList = new LeaveList();
            int pageSize = 7;
            int pageNumber = (Page ?? 1);
            Employees emp = new Employees();
            int? Employee_ID;
            #endregion

            DateTime LeaveSDate = DateTime.Now;
            DateTime LeaveEDate = DateTime.Now;

            ViewBag.FromDate = SDate;
            ViewBag.ToDate = EDate;

            if (!string.IsNullOrEmpty(Status))
            {
                ViewBag.StatusDL = Status;
            }

            if (!(string.IsNullOrEmpty(SDate)))
            {
                LeaveSDate = Convert.ToDateTime(SDate);
            }

            if (!(string.IsNullOrEmpty(EDate)))
            {
                LeaveEDate = Convert.ToDateTime(EDate);
            }

            emp = userBL.GetEmployeeByID(Session["AccountMgr"].ToString(), null);
            Employee_ID = emp.Id;

            var leaveslist = (from empleavelist1 in objLeaveList select empleavelist1);

            if (!(string.IsNullOrEmpty(Zone)) || !(string.IsNullOrEmpty(Employee)) || !(string.IsNullOrEmpty(SDate)) || !(string.IsNullOrEmpty(EDate)) || !(string.IsNullOrEmpty(Status)))
            {
                

                objLeaveList = userBL.GetAllLeaves(LeaveSDate, LeaveEDate);

                leaveslist = from empLeaves in objLeaveList select empLeaves;

                if (SDate != null && EDate != null)
                {
                    if (!(string.IsNullOrEmpty(Employee)))
                    {
                        if (!(string.IsNullOrEmpty(Status)))
                        {
                            if (Status == "true")
                            {
                                leaveslist = (from emLeave in objLeaveList.Where(x => x.Employee_ID == Convert.ToInt32(Employee) && x.LeaveStatus == true) select emLeave).ToList();
                            }
                            else if (Status == "false")
                            {
                                leaveslist = (from emLeave in objLeaveList.Where(x => x.Employee_ID == Convert.ToInt32(Employee) && x.LeaveStatus == false) select emLeave).ToList();
                            }
                            else
                            {
                                leaveslist = (from emLeave in objLeaveList.Where(x => x.Employee_ID == Convert.ToInt32(Employee) && x.LeaveStatus == null) select emLeave).ToList();
                            }
                        }
                        else
                        {
                            leaveslist = (from emLeave in objLeaveList.Where(x => x.Employee_ID == Convert.ToInt32(Employee)) select emLeave).ToList();
                        }
                    }
                    else
                    {
                        if (!(string.IsNullOrEmpty(Zone)))
                        {
                            if (!(string.IsNullOrEmpty(Status)))
                            {
                                if (Status == "true")
                                {
                                    leaveslist = (from emLeave in objLeaveList.Where(x => x.ZoneId == Convert.ToInt32(Zone) && x.LeaveStatus == true)
                                                  select emLeave).ToList();
                                }
                                else if (Status == "false")
                                {
                                    leaveslist = (from emLeave in objLeaveList.Where(x => x.ZoneId == Convert.ToInt32(Zone) && x.LeaveStatus == false)
                                                  select emLeave).ToList();
                                }
                                else
                                {
                                    leaveslist = (from emLeave in objLeaveList.Where(x => x.ZoneId == Convert.ToInt32(Zone) && x.LeaveStatus == null)
                                                  select emLeave).ToList();
                                }
                            }
                            else
                            {
                                leaveslist = (from emLeave in objLeaveList.Where(x => x.ZoneId == Convert.ToInt32(Zone))
                                              select emLeave).ToList();
                            }
                        }
                        else
                        {
                            if (!(string.IsNullOrEmpty(Status)))
                            {
                                if (Status == "true")
                                {
                                    leaveslist = (from emLeave in objLeaveList.Where(x => x.LeaveStatus == true) select emLeave).ToList();
                                }
                                else if (Status == "false")
                                {
                                    leaveslist = (from emLeave in objLeaveList.Where(x => x.LeaveStatus == false) select emLeave).ToList();
                                }
                                else
                                {
                                    leaveslist = (from emLeave in objLeaveList.Where(x => x.LeaveStatus == null) select emLeave).ToList();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                leaveslist = userBL.GetLeaves(null, Employee_ID);
                    
              
            }
            DateTime ExpenditureSDate = Convert.ToDateTime(SDate);
            DateTime ExpenditureEDate = Convert.ToDateTime(EDate);


            #region All zones list
            ZonesList zones = new ZonesList();
            zones = userBL.GetZones();
            int defZones = 0;
            if (!(string.IsNullOrEmpty(Zone)))
            {
                defZones = Convert.ToInt32(Zone);
            }
            List<SelectListItem> zonesList = new List<SelectListItem>();
            foreach (var item in zones)
            {
                zonesList.Add(new SelectListItem()
                {
                    Text = item.Zone,
                    Value = item.Id.ToString(),
                    Selected = (item.Id == defZones ? true : false)
                });
            }
            ViewBag.ZonesList = zonesList;
            #endregion

            #region All Employees List
            Universal.BusinessEntities.EmployeesList empList = new Universal.BusinessEntities.EmployeesList();
            empList = userBL.GetEmployees();
            List<SelectListItem> employeeList = new List<SelectListItem>();
            int? defEmp = null;
            if (!(string.IsNullOrEmpty(Employee)))
            {
                defEmp = Convert.ToInt32(Employee);
            }
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

            return View(leaveslist.ToPagedList(pageNumber, leaveslist.ToList().Count != 0 ? leaveslist.ToList().Count : 1));
        }


        //
        // GET: Admin/JobPosting
        public ActionResult JobPosting()
        {
            Session["url"] = "/Admin/JobPosting";
            return View();
        }

        //
        // POST: Admin/JobPosting
        [HttpPost]
        public ActionResult JobPosting(Jobs jobs)
        {

            Session["url"] = "/Admin/JobPosting";
            string UserSession = (HttpContext.Session["AccountMgr"] != null) ? HttpContext.Session["AccountMgr"].ToString() : "Admin Session Null";
            int? err = userBL.PostNewJob(jobs, 1);
            if (err == null)
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs"), UserSession.ToUpper(), "Admin successfully Posted a New Job");
                return Json(new { Available = true, Text = "You have successfully Posted a New Job!!!" });
            }
            else
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs"), UserSession.ToUpper(), "Admin unable Posted a New Job");
                return Json(new { Available = false, Text = "Your request not submitted" });
            }
        }

        [HttpPost]
        public string UploadEventImages()
        {
            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var fileContent = Request.Files[i];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        var stream = fileContent.InputStream;
                        var fileName = RemoveSpecialCharactersSpaces(DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + i + "_" + Session["EventId"] + "_") + fileContent.FileName;

                        EventImages eventImages = new EventImages();
                        eventImages.Fk_EventId = int.Parse(Session["EventId"].ToString());
                        eventImages.ImageName = fileName;
                        int? err = userBL.InsertEventImages(eventImages);
                        var path = Path.Combine(Server.MapPath("~/Content/EventImages"), fileName);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }

                    }

                }

            }
            catch (Exception)
            {
                return "Images upload Failed";
            }


            return "Images uploaded successfully";
        }


        //
        // GET: Admin/JobsList
        public ActionResult JobsList(string sortOrder, string currentFilter, string searchString, int? page)
        {
            Session["url"] = "/Admin/JobsList";
            UserBL userbl = new UserBL();
            JobsCollection jobsCollection = new JobsCollection();
            jobsCollection = userbl.GetJobsList();
            var AllJobs = (from s in jobsCollection select s);
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

            if (!String.IsNullOrWhiteSpace(searchString))
            {
                AllJobs = AllJobs.Where(s => s.JobTitle.Trim().ToUpper().Contains(searchString.Trim().ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    AllJobs = AllJobs.OrderBy(s => s.ModifiedDate);
                    break;
                default:
                    AllJobs = AllJobs.OrderByDescending(s => s.ModifiedDate);
                    break;
            }
            int pageSize = 7;
            int pageNumber = (page ?? 1);
            return PartialView("JobsList", AllJobs.ToPagedList(pageNumber, pageSize));

        }
        //
        // GET: Admin/JobView
        public ActionResult JobView(int? id)
        {
            Session["url"] = "/Admin/JobsList";
            UserBL userbl = new UserBL();
            Universal.BusinessEntities.Jobs jobdetails = new Universal.BusinessEntities.Jobs();
            jobdetails = userbl.JobdetailsById(id);
            return View(jobdetails);
        }


        //
        // GET: Admin/EventsList
        public ActionResult EventsList(string sortOrder, string currentFilter, string searchString, int? page)
        {
            Session["url"] = "/Admin/EventsList";
            UserBL userbl = new UserBL();
            EventsCollection eventsCollection = new EventsCollection();
            eventsCollection = userbl.GetEventsList();
            var AllEvents = (from s in eventsCollection select s);
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

            if (!String.IsNullOrWhiteSpace(searchString))
            {
                AllEvents = AllEvents.Where(s => s.Title.Trim().ToUpper().Contains(searchString.Trim().ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    AllEvents = AllEvents.OrderBy(s => s.Date);
                    break;
                default:
                    AllEvents = AllEvents.OrderByDescending(s => s.Date);
                    break;
            }
            int pageSize = 7;
            int pageNumber = (page ?? 1);
            return PartialView("EventsList", AllEvents.ToPagedList(pageNumber, pageSize));

        }


        //
        // GET: Admin/NewsPosting
        public ActionResult NewsPosting()
        {
            Session["url"] = "/Admin/NewsPosting";
            return View();
        }

        public ActionResult AssetsReports(string zone, string asset, string empId, string fromDate, string toDate, string sortOrder, string currentFilter, string searchString, int? Page, int? id)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-IN");
            UserBL userbl = new UserBL();
            EmployeeAssetsList objEmployeeAssetsList = new EmployeeAssetsList();
            objEmployeeAssetsList = userbl.GetEmployeeAssets();


            int pageNumber = (Page ?? 1);
            Session["url"] = "/Admin/AssetsReports";
            //ViewBag.CurrentSort = sortOrder;
            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var empAsset = (from empAssetsLst in objEmployeeAssetsList select empAssetsLst);

            #region zones list
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
            #region All Employees List
            Universal.BusinessEntities.EmployeesList empList = new Universal.BusinessEntities.EmployeesList();

            empList = userBL.GetEmployees();
            List<SelectListItem> employeeList = new List<SelectListItem>();
            int? defEmp = null;
            if (!(string.IsNullOrEmpty(empId)))
            {
                defEmp = Convert.ToInt32(empId);
            }
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

            if (!string.IsNullOrEmpty(zone) || !string.IsNullOrEmpty(asset) || !string.IsNullOrEmpty(empId) || !string.IsNullOrEmpty(fromDate) || !string.IsNullOrEmpty(toDate))
            {
                if (!string.IsNullOrEmpty(zone))
                {
                    empAsset = empAsset.Where(s => s.ZoneId == int.Parse(zone));

                }
                if (!string.IsNullOrEmpty(asset))
                {
                    ViewBag.Asset = asset;
                    if (asset != "count")
                    {
                        if (asset == "Laptop")
                            empAsset = empAsset.Where(s => s.Laptop == true);
                        else if (asset == "Datacard")
                            empAsset = empAsset.Where(s => s.Datacard == true);
                        else if (asset == "Multimeter")
                            empAsset = empAsset.Where(s => s.Multimeter == true);
                        else if (asset == "Simcard")
                            empAsset = empAsset.Where(s => s.Simcard == true);
                        else if (asset == "Toolkit")
                            empAsset = empAsset.Where(s => s.Toolkit == true);
                        else if (asset == "LaptopBag")
                            empAsset = empAsset.Where(s => s.LaptopBag == true);
                        else if (asset == "Cellphone")
                            empAsset = empAsset.Where(s => s.Cellphone == true);
                    }
                    else
                    {

                        DataTable dt = new DataTable();
                        dt.Columns.AddRange(new DataColumn[8] { new DataColumn("ZoneNameAsset"), new DataColumn("LaptopCount"), 
                            new DataColumn("DatacardCount"), new DataColumn("MultimeterCount"), new DataColumn("SimcardCount"), 
                            new DataColumn("ToolkitCount"), new DataColumn("LaptopBagCount"), new DataColumn("CellphoneCount") });

                        var countlist = (from empAssetsLst in objEmployeeAssetsList select empAssetsLst);
                        foreach (var item in zones)
                        {
                            var countlist1 = (from s in objEmployeeAssetsList where s.ZoneId == item.Id && s.Laptop == true select s);
                            var countlist2 = (from s in objEmployeeAssetsList where s.ZoneId == item.Id && s.Datacard == true select s);
                            var countlist4 = countlist.Where(s => s.ZoneId == item.Id && s.Multimeter == true);
                            var countlist5 = countlist.Where(s => s.ZoneId == item.Id && s.Simcard == true);
                            var countlist6 = countlist.Where(s => s.ZoneId == item.Id && s.Toolkit == true);
                            var countlist7 = countlist.Where(s => s.ZoneId == item.Id && s.LaptopBag == true);
                            var countlist8 = countlist.Where(s => s.ZoneId == item.Id && s.Cellphone == true);

                            dt.Rows.Add(item.Zone, int.Parse(countlist1.Count().ToString()),
                                int.Parse(countlist2.Count().ToString()), int.Parse(countlist4.Count().ToString()), int.Parse(countlist5.Count().ToString()),
                                int.Parse(countlist6.Count().ToString()), int.Parse(countlist7.Count().ToString()), int.Parse(countlist8.Count().ToString())
                                );

                        }



                        //List<DataRow> list = dt.AsEnumerable().ToList();

                        AssetsZonesWiseCollection assetsZonesWiseCollection = new AssetsZonesWiseCollection();
                        Universal.BusinessEntities.Core.ObjectBuilder.FillList<AssetsZonesWise>(dt, assetsZonesWiseCollection);


                        ViewBag.AssetsZonesWise = assetsZonesWiseCollection;

                    }

                }
                if (!string.IsNullOrEmpty(empId))
                {
                    empAsset = empAsset.Where(s => s.FK_EmployeeID == int.Parse(empId));
                }
                if (!string.IsNullOrEmpty(fromDate) || !string.IsNullOrEmpty(toDate))
                {
                    if (!string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
                    {
                        var fromdateConvert = Convert.ToDateTime(fromDate);
                        var todateConvert = DateTime.Now.AddDays(1);
                        empAsset = empAsset.Where(s => s.CreatedDate >= fromdateConvert && s.CreatedDate <= todateConvert);

                    }
                    else if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                    {
                        var fromdateConvert = Convert.ToDateTime(fromDate).AddMinutes(-330);
                        var todateConvert = Convert.ToDateTime(toDate).AddDays(1);
                        empAsset = empAsset.Where(s => s.CreatedDate >= fromdateConvert && s.CreatedDate <= todateConvert);

                    }

                }


            }
            if (searchString != null)
            {
                Page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;




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

            if (asset != "count")
                ViewBag.Total = empAsset.Count();
            //int pageSize = empAsset.Count();
            return View("AssetsReports", empAsset.ToPagedList(pageNumber, empAsset.ToList().Count != 0 ? empAsset.ToList().Count : 1));

        }

        //
        // POST: Admin/NewsPosting
        [HttpPost]
        public ActionResult NewsPosting(News news)
        {
            Session["url"] = "/Admin/NewsPosting";
            string UserSession = (HttpContext.Session["AccountMgr"] != null) ? HttpContext.Session["AccountMgr"].ToString() : "Admin Session Null";
            int? err = userBL.PostNews(news, 1);
            if (err == null)
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs"), UserSession.ToUpper(), "Admin successfully Posted a News");
                return Json(new { Available = true, Text = "You have successfully Posted a News!!!" });
            }
            else
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs"), UserSession.ToUpper(), "Admin Unsuccessfully Posted a News");
                return Json(new { Available = false, Text = "Your request not submitted" });
            }

        }


        //
        // GET: Admin/EventPosting
        public ActionResult EventPosting()
        {
            Session["url"] = "/Admin/EventPosting";
            return View();
        }
        //
        // GET: Admin/EventView
        public ActionResult EventView(int? id)
        {
            Session["url"] = "/Admin/EventView/" + id;
            UserBL userbl = new UserBL();
            Events events = new Events();
            events = userbl.EventDetailsById(id);
            return View(events);
        }

        //
        // GET: Admin/UpdateEvent
        public ActionResult UpdateEvent(int? id)
        {
            Session["url"] = "/Admin/UpdateEvent/" + id;
            UserBL userbl = new UserBL();
            Events events = new Events();
            events = userbl.EventDetailsById(id);
            return View(events);
        }

        //
        // POST: Admin/UpdateEvent
        [HttpPost]
        public ActionResult UpdateEvent(Events events)
        {

            UserBL userbl = new UserBL();
            EventImages eventImages = new EventImages();
            var imagesDeleted = Request["DeletedUrl"];
            if (imagesDeleted != null)
            {
                if (imagesDeleted.IndexOf(',') != -1)
                {
                    string[] allmails = imagesDeleted.Split(',');

                    foreach (string imagename in allmails)
                    {
                        var pathString = Path.Combine(Server.MapPath("~/Content/EventImages"), imagename);
                        //var image1 = imagename.Substring(imagename.IndexOf("_USEventSNO_"), imagename.Length - (imagename.IndexOf("_USEventSNO_")));
                        // var image = image1.Substring(11, image1.Length - 11);


                        //var originalDirectory = new DirectoryInfo(string.Format("..\\..\\Content\\EventImages", Server.MapPath(@"").Remove(Server.MapPath(@"").Length - 7)));
                        //string pathString = System.IO.Path.Combine(originalDirectory.ToString(), imagename);
                        FileInfo fi = new FileInfo(pathString);
                        if (fi.Exists)
                        {
                            fi.Delete();
                            eventImages.Fk_EventId = events.Id;
                            eventImages.ImageName = imagename;
                            int? err1 = userBL.DeleteEventImages(eventImages);
                        }
                    }

                }
                else
                {
                    //string imagename = imagesDeleted;
                    //var image1 = imagename.Substring(imagename.IndexOf("_USEventSNO_"), imagename.Length - (imagename.IndexOf("_USEventSNO_")));
                    //var image = image1.Substring(11, image1.Length - 11);
                    //var originalDirectory = new DirectoryInfo(string.Format("..\\..\\Content\\EventImages", Server.MapPath(@"").Remove(Server.MapPath(@"").Length - 7)));
                    //string pathString = System.IO.Path.Combine(originalDirectory.ToString(), imagesDeleted);
                    var pathString = Path.Combine(Server.MapPath("~/Content/EventImages"), imagesDeleted);
                    FileInfo fi = new FileInfo(pathString);
                    if (fi.Exists)
                    {
                        fi.Delete();
                        eventImages.Fk_EventId = events.Id;
                        eventImages.ImageName = imagesDeleted;
                        int? err1 = userBL.DeleteEventImages(eventImages);
                    }
                }
            }

            int? err = userBL.PostNewEvent(events, 2);
            if (err == null)
            {
                Session["EventId"] = events.Id;
                return Json(new { Available = true, Text = "You have successfully Updated the Event!!!" });
            }
            else
            {
                return Json(new { Available = false, Text = "Your request not submitted" });
            }

            // return View(events);
        }
        //
        // GET: Admin/UpdateNews
        public ActionResult UpdateNews(int? id)
        {
            Session["url"] = "/Admin/NewsList";
            UserBL userbl = new UserBL();
            News news = new News();
            news = userbl.NewsDetailsById(id);
            return View(news);
        }
        //
        // POST: Admin/UpdateNews
        [HttpPost]
        public ActionResult UpdateNews(News news)
        {
            Session["url"] = "/Admin/NewsList";
            string UserSession = (HttpContext.Session["AccountMgr"] != null) ? HttpContext.Session["AccountMgr"].ToString() : "Admin Session Null";
            int? err = userBL.PostNews(news, 2);
            if (err == null)
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs"), UserSession.ToUpper(), "Admin Updated successfully a News");
                return Json(new { Available = true, Text = "You have successfully Updated the News!!!" });
            }
            else
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs"), UserSession.ToUpper(), "Admin Updated Unsuccessfully a News");
                return Json(new { Available = false, Text = "Request not submitted" });
            }
        }
        //
        // POST: Admin/EventPosting
        [HttpPost]
        public ActionResult EventPosting(Events events)
        {
            Session["url"] = "/Admin/EventPosting";
            string UserSession = (HttpContext.Session["AccountMgr"] != null) ? HttpContext.Session["AccountMgr"].ToString() : "Admin Session Null";
            int? err = userBL.PostNewEvent(events, 1);
            if (err != null)
            {
                Session["EventId"] = err;
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs"), UserSession.ToUpper(), "Admin successfully Posted a New Event");
                return Json(new { Available = true, Text = "You have successfully Posted a New Event!!!" });
            }
            else
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs"), UserSession.ToUpper(), "Admin Unsuccessfully Posted a New Event");
                return Json(new { Available = false, Text = "Your request not submitted" });
            }

        }

        //
        // GET: Admin/UpdateJob
        public ActionResult UpdateJob(int? id)
        {
            Session["url"] = "/Admin/JobsList";
            UserBL userbl = new UserBL();
            Universal.BusinessEntities.Jobs jobdetails = new Universal.BusinessEntities.Jobs();
            jobdetails = userbl.JobdetailsById(id);
            jobdetails.ContactEmail = jobdetails.ContactEmail.TrimEnd();
            return PartialView("UpdateJob", jobdetails);
        }

        //
        // POST: Admin/UpdateJob
        [HttpPost]
        public ActionResult UpdateJob(Jobs jobs)
        {
            Session["url"] = "/Admin/JobsList";
            string UserSession = (HttpContext.Session["AccountMgr"] != null) ? HttpContext.Session["AccountMgr"].ToString() : "Admin Session Null";
            int? err = userBL.PostNewJob(jobs, 2);
            if (err == null)
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "Successfully Updated the Job");
                return Json(new { Available = true, Text = "You have successfully Updated the Job!!!" });
            }
            else
            {
                LogFiles logfiles = new LogFiles();
                logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "Request not submitted");
                return Json(new { Available = false, Text = "Request not submitted" });
            }
        }

        //
        // GET: Admin/CompareSNumbers
        public ActionResult CompareSNumbers()
        {
            Session["url"] = "/Admin/CompareSNumbers";
            return View();
        }
        //
        // GET: Admin/CompareSNumbers
        public ActionResult MisMatchingSOCNumbers()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CompareSNumbersExcel(string sortOrder, string currentFilter, string searchString, int? Page)
        {
            DataSet ds = new DataSet();
            int pageNumber = (Page ?? 1);
            int pageSize = 20;
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
            foreach (string file in Request.Files)
            {
                Session.Remove("Results");
                try
                {

                    var fileContent = Request.Files[file];

                    if (fileContent.ContentLength > 0)
                    {
                        string fileExtension = System.IO.Path.GetExtension(fileContent.FileName);

                        if (fileExtension == ".xls" || fileExtension == ".xlsx")
                        {
                            var fileName = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + "_" + fileContent.FileName;

                            string fileLocation = Server.MapPath("~/Content/SOExcelSheets/") + fileName;
                            if (System.IO.File.Exists(fileLocation))
                            {

                                System.IO.File.Delete(fileLocation);
                            }
                            fileContent.SaveAs(fileLocation);
                            string excelConnectionString = string.Empty;
                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            //connection String for xls file format.
                            if (fileExtension == ".xls")
                            {
                                excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                                fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                            }
                            //connection String for xlsx file format.
                            else if (fileExtension == ".xlsx")
                            {
                                excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                                fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            }
                            //Create Connection to Excel work book and add oledb namespace
                            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                            excelConnection.Open();
                            DataTable dt = new DataTable();

                            dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            if (dt == null)
                            {
                                return null;
                            }
                            String[] excelSheets = new String[dt.Rows.Count];
                            int t = 0;
                            //excel data saves in temp file here.
                            foreach (DataRow row in dt.Rows)
                            {
                                excelSheets[t] = row["TABLE_NAME"].ToString();
                                t++;
                            }
                            OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);
                            string query = string.Format("Select * from [{0}]", excelSheets[0]);
                            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                            {
                                dataAdapter.Fill(ds);
                            }
                        }
                        UserBL userBL = new UserBL();

                        DataTable dtable = new DataTable();
                        SOCNumersExcelList sOCNumersExcelList = new SOCNumersExcelList();
                        SOCNumersExcelList sOCNumersExcelListNotMatching = new SOCNumersExcelList();
                        dtable = ds.Tables[0];
                        //Changing column names
                        dtable.Columns["Proposal #"].ColumnName = "Proposal";
                        dtable.Columns["Status"].ColumnName = "Status";
                        dtable.Columns["ASP Manager"].ColumnName = "ASPManager";
                        dtable.Columns["ASP Company"].ColumnName = "ASPCompany";
                        dtable.Columns["Value LC"].ColumnName = "ValueLC";
                        dtable.Columns["Currency Format"].ColumnName = "CurrencyFormat";
                        dtable.Columns["Value CLC"].ColumnName = "ValueCLC";
                        dtable.Columns["Created On"].ColumnName = "CreatedOn";
                        dtable.Columns["ASP Invoice #"].ColumnName = "ASPInvoice";
                        dtable.Columns["S# Confirmation #"].ColumnName = "SConfirmation";
                        dtable.Columns["S#C# Item #"].ColumnName = "SCItem";
                        dtable.Columns["S#C# Status"].ColumnName = "SCStatus";
                        dtable.Columns["S#C# Description"].ColumnName = "SCDescription";
                        dtable.Columns["S# Order #"].ColumnName = "SOrder";
                        dtable.Columns["S# Order Item #"].ColumnName = "SOrderItem";
                        dtable.Columns["S#O# Description"].ColumnName = "SODescription";
                        dtable.Columns["Customer"].ColumnName = "Customer";
                        dtable.Columns["Customer City"].ColumnName = "CustomerCity";
                        dtable.Columns["Customer Country"].ColumnName = "CustomerCountry";
                        dtable.Columns["Currency"].ColumnName = "Currency";
                        dtable.Columns["Start Date"].ColumnName = "StartDate";
                        dtable.Columns["End Date"].ColumnName = "EndDate";
                        dtable.Columns["Type"].ColumnName = "Type";
                        dtable.Columns["ASP Person"].ColumnName = "ASPPerson";
                        dtable.Columns["Product #"].ColumnName = "Product";
                        dtable.Columns["Product Description"].ColumnName = "ProductDescription";
                        dtable.Columns["SYS Unit"].ColumnName = "SYSUnit";
                        dtable.Columns["Serial Number"].ColumnName = "SerialNumber";
                        dtable.Columns["Avg# Standard H#"].ColumnName = "AvgStandardH";
                        dtable.Columns["Elapsed H#"].ColumnName = "ElapsedH";
                        dtable.Columns["Factor"].ColumnName = "Factor";
                        dtable.Columns["Compensation"].ColumnName = "Compensation";
                        dtable.Columns["Amount"].ColumnName = "Amount";
                        dtable.Columns["Paid"].ColumnName = "Paid";
                        dtable.Columns["Avg# Travel H#"].ColumnName = "AvgTravelH";
                        dtable.Columns["Elapsed Travel H#"].ColumnName = "ElapsedTravelH";
                        dtable.Columns["Travel Compensation"].ColumnName = "TravelCompensation";
                        dtable.Columns["Mileage"].ColumnName = "Mileage";
                        dtable.Columns["Travel Factor"].ColumnName = "TravelFactor";
                        dtable.Columns["Travel Amount"].ColumnName = "TravelAmount";
                        dtable.Columns["Travel Paid"].ColumnName = "TravelPaid";
                        dtable.Columns["Misc# Charge"].ColumnName = "MiscCharge";
                        dtable.Columns["Total"].ColumnName = "Total";
                        dtable.Columns["Note"].ColumnName = "Note";
                        dtable.Columns["Special Code"].ColumnName = "SpecialCode";
                        dtable.Columns["Error Message"].ColumnName = "ErrorMessage";
                        Universal.BusinessEntities.Core.ObjectBuilder.FillList<SOCNumersExcel>(dtable, sOCNumersExcelList);
                        for (int i = 0; i < sOCNumersExcelList.Count; i++)
                        {

                            var sorder = sOCNumersExcelList[i].SOrder;
                            var socrder = sOCNumersExcelList[i].SConfirmation;
                            if (!string.IsNullOrEmpty(sorder) && !string.IsNullOrEmpty(socrder))
                            {
                                sorder = RemoveSpecialCharacters(sorder.Trim());
                                socrder = RemoveSpecialCharacters(socrder.Trim());
                                UserExpensesHeaderList userExpenditureHeaderList = new UserExpensesHeaderList();
                                userExpenditureHeaderList = userBL.GetExpenditureNonMatchingSONumbers(sorder, socrder);
                                if (userExpenditureHeaderList.Count == 0)
                                {
                                    //bool has = sOCNumersExcelListNotMatching.Any(cus => cus.SConfirmation == socrder && cus.SOrder == sorder);
                                    //if (has == false)
                                    sOCNumersExcelListNotMatching.Add(sOCNumersExcelList[i]);
                                }
                            }
                        }
                        // var socnumberslist = sOCNumersExcelListNotMatching.Except(sOCNumersExcelList).ToList();
                        var nonmatchLinq = (from s in sOCNumersExcelListNotMatching select s);
                        switch (sortOrder)
                        {
                            case "name_desc":
                                nonmatchLinq = nonmatchLinq.OrderByDescending(s => s.SOrder);
                                break;

                            default:
                                nonmatchLinq = nonmatchLinq.OrderBy(s => s.SOrder);
                                break;
                        }
                        Session["Results"] = nonmatchLinq.ToList();
                        String siteRowHtml = RenderViewToString(this, "_NotMatchingSOCList", nonmatchLinq.ToPagedList(pageNumber, pageSize));
                        return Json(siteRowHtml, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    return Json("Error occurred while processing the data!!", JsonRequestBehavior.AllowGet);
                }
            }

            return View();
        }

        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if (c >= '0' && c <= '9')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        public static string RemoveSpecialCharactersSpaces(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_]+", "");
        }
        public static string RenderViewToString(Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData,
                                                  controller.TempData, sw);

                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);

                return sw.GetStringBuilder().ToString();
            }
        }


        public ActionResult NotMatchinSOCList(string sortOrder, string currentFilter, string searchString, int? Page)
        {
            int pageNumber = (Page ?? 1);
            int pageSize = 20;
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

            var results = (List<SOCNumersExcel>)Session["Results"];
            var nonmatchLinq = (from s in results select s);
            if (!String.IsNullOrWhiteSpace(searchString))
            {
                nonmatchLinq = nonmatchLinq.Where(s => s.SConfirmation.Trim().ToUpper().Contains(searchString.Trim().ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    nonmatchLinq = nonmatchLinq.OrderByDescending(s => s.SOrder);
                    break;

                default:
                    nonmatchLinq = nonmatchLinq.OrderBy(s => s.SOrder);
                    break;
            }
            return PartialView("_NotMatchingSOCList", nonmatchLinq.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult UploadData(int? id)
        {
            DataSet ds = new DataSet();
            //string UserSession = (HttpContext.Session["AccountMgr"] != null) ? HttpContext.Session["AccountMgr"].ToString() : "Admin Session Null";
            foreach (string file in Request.Files)
            {
                Session.Remove("Results");
                try
                {

                    var fileContent = Request.Files[file];
                    if (fileContent.ContentLength > 0)
                    {
                        string fileExtension = System.IO.Path.GetExtension(fileContent.FileName);
                        if (fileExtension == ".xls" || fileExtension == ".xlsx")
                        {
                            var fileName = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + "_" + fileContent.FileName;
                            string fileLocation = Server.MapPath("~/Content/SOExcelSheets/") + fileName;
                            if (System.IO.File.Exists(fileLocation))
                            {
                                System.IO.File.Delete(fileLocation);
                            }
                            fileContent.SaveAs(fileLocation);
                            string excelConnectionString = string.Empty;
                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            //connection String for xls file format.
                            if (fileExtension == ".xls")
                            {
                                excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                                fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                            }
                            //connection String for xlsx file format.
                            else if (fileExtension == ".xlsx")
                            {
                                excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                                fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            }
                            //Create Connection to Excel work book and add oledb namespace
                            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                            excelConnection.Open();
                            DataTable dt = new DataTable();
                            dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            if (dt == null)
                            {
                                return null;
                            }
                            String[] excelSheets = new String[dt.Rows.Count];
                            int t = 0;
                            //excel data saves in temp file here.
                            foreach (DataRow row in dt.Rows)
                            {
                                excelSheets[t] = row["TABLE_NAME"].ToString();
                                t++;
                            }
                            OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);
                            string query = string.Format("Select * from [{0}]", excelSheets[0]);
                            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                            {
                                dataAdapter.Fill(ds);
                            }
                        }
                        DataTable dtable = new DataTable();
                        UserBL userBL = new UserBL();
                        dtable = ds.Tables[0];


                        int? err = 0;
                        for (int i = 0; i < dtable.Columns.Count; i++)
                        {
                            dtable.Columns[i].ColumnName = RemoveSpecialCharactersSpaces(dtable.Columns[i].ColumnName);
                        }

                        if (id == 1)
                        {
                            SalaryDetailsList salaryDetailsList = new SalaryDetailsList();
                            Universal.BusinessEntities.Core.ObjectBuilder.FillList<SalaryDetails>(dtable, salaryDetailsList);
                            for (int i = 0; i < salaryDetailsList.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(salaryDetailsList[i].Empcode))
                                {
                                    DataTable dtData = new DataTable();
                                    DataTable dtSalaryByMonth = new DataTable();

                                    dtData = userBL.GetSalaryDetailsByMonthEmployee(salaryDetailsList[i].Empcode.Trim());
                                    SalaryDetailsList salList = new SalaryDetailsList();
                                    Universal.BusinessEntities.Core.ObjectBuilder.FillList<SalaryDetails>(dtData, salList);
                                    var matchedsalaries = (from s in salList where s.Month == salaryDetailsList[i].Month select s).ToList();
                                    if (matchedsalaries.Count == 0)
                                    {
                                        err = userBL.InsertSalaryDetails(salaryDetailsList[i], 1);
                                    }
                                    else
                                    {
                                        err = userBL.InsertSalaryDetails(salaryDetailsList[i], 2);
                                    }
                                }

                            }
                            if (err == null)
                            {
                                //LogFiles logfiles = new LogFiles();
                                //logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"),UserSession.ToUpper() ,"Admin successfully inserted Salary Details");
                                return Json(new { Available = true, Text = "You have successfully inserted Salary Details!!" });
                            }
                            else
                            {
                                //LogFiles logfiles = new LogFiles();
                                //logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "Error occurred while processing the data");
                                return Json(new { Available = false, Text = "Error occurred while processing the data!!" });
                            }
                        }
                        else if (id == 2)
                        {
                            dtable.Columns.Remove("EmployeeName");
                            SkillsMetricsList skillsMetricsList = new SkillsMetricsList();
                            Universal.BusinessEntities.Core.ObjectBuilder.FillList<SkillsMetrics>(dtable, skillsMetricsList);

                            for (int i = 0; i < skillsMetricsList.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(skillsMetricsList[i].Empid))
                                    err = userBL.InsertSkillMetrics(skillsMetricsList[i]);
                            }
                            if (err == null)
                            {
                                //LogFiles logfiles = new LogFiles();
                                //logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "Admin successfully inserted Salary Details");
                                return Json(new { Available = true, Text = "You have successfully inserted Skill Set!!" });
                            }
                            else
                            {
                                //LogFiles logfiles = new LogFiles();
                                //logfiles.ErrorLog(HttpContext.Server.MapPath("/Content/Logs/"), UserSession.ToUpper(), "Error occurred while processing the data");
                                return Json(new { Available = false, Text = "Error occurred while processing the data!!" });
                            }
                        }

                        if (id == 4)
                        {
                            SalaryDetailsCollection_NEW salaryDetailsCollection_NEW = new SalaryDetailsCollection_NEW();
                            Universal.BusinessEntities.Core.ObjectBuilder.FillList<SalaryDetails_NEW>(dtable, salaryDetailsCollection_NEW);
                            for (int i = 0; i < salaryDetailsCollection_NEW.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(salaryDetailsCollection_NEW[i].Empcode))
                                {
                                    DataTable dtData = new DataTable();
                                    DataTable dtSalaryByMonth = new DataTable();

                                    dtData = userBL.GetSalaryDetailsByMonthEmployeeNewFormat(salaryDetailsCollection_NEW[i].Empcode.Trim());
                                    SalaryDetailsCollection_NEW salarynewlist = new SalaryDetailsCollection_NEW();
                                    Universal.BusinessEntities.Core.ObjectBuilder.FillList<SalaryDetails_NEW>(dtData, salarynewlist);
                                    var matchedsalaries = (from s in salarynewlist where s.Month1== salaryDetailsCollection_NEW[i].Month1 select s).ToList();
                                    if (matchedsalaries.Count == 0)
                                    {
                                        err = userBL.InsertSalaryDetailsNewFormat(salaryDetailsCollection_NEW[i], 1);
                                    }
                                    else
                                    {
                                        err = userBL.InsertSalaryDetailsNewFormat(salaryDetailsCollection_NEW[i], 2);
                                    }
                                }

                            }
                            if (err == null)
                            {
                               return Json(new { Available = true, Text = "You have successfully inserted Salary Details!!" });
                            }
                            else
                            {
                              return Json(new { Available = false, Text = "Error occurred while processing the data!!" });
                            }
                        }
                        return Json("", JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                  return Json("Error occurred while processing the data!!", JsonRequestBehavior.AllowGet);
                }
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }



        //public ActionResult Assets(int? flag, int? Page, string SEmployeeID, string SZoneID, string SFromDate, string SToDate, string Search)
        //{

        //    EmployeeAssets objEmployeeAssets = new EmployeeAssets();

        //    objEmployeeAssets.flag = flag;
        //    Session["url"] = "/Admin/Assets";

        //    #region All Employees List
        //    Universal.BusinessEntities.EmployeesList empList = new Universal.BusinessEntities.EmployeesList();
        //    UserBL userBL = new UserBL();
        //    empList = userBL.GetEmployees();
        //    List<SelectListItem> employeeList = new List<SelectListItem>();
        //    int? defEmp = null;
        //    foreach (var item in empList)
        //    {
        //        employeeList.Add(new SelectListItem()
        //        {
        //            Text = item.Employee_ID + " [" + item.FirstName + "] ",
        //            Value = item.Id.ToString(),
        //            Selected = (item.Id == defEmp ? true : false)
        //        });
        //    }
        //    ViewBag.EmployeesList = employeeList;
        //    #endregion

        //    #region zones list
        //    ZonesList zones = new ZonesList();
        //    zones = userBL.GetZones();
        //    string defZones = "";
        //    List<SelectListItem> zonesList = new List<SelectListItem>();

        //    foreach (var item in zones)
        //    {
        //        zonesList.Add(new SelectListItem()
        //        {
        //            Text = item.Zone,
        //            Value = item.Id.ToString(),
        //            Selected = (item.Zone == defZones ? true : false)
        //        });
        //    }

        //    ViewBag.Zones = zonesList;
        //    #endregion

        //    return View(objEmployeeAssets);

        //}


        public ActionResult EmployeeStatus(string zone, string sortOrder, string currentFilter, string SearchString, string empstatus, string fromdate, string todate, int? Page, int? id)
        {

            Session["url"] = "/Admin/EmployeeStatus";
            UserBL userBL = new UserBL();
            Universal.BusinessEntities.EmployeesList empList = new Universal.BusinessEntities.EmployeesList();
            empList = userBL.GetEmployees();

            int pageSize = 7;
            int pageNumber = (Page ?? 1);
            Session["url"] = "/Admin/EmployeeStatus";
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var emps = (from empLst in empList select empLst);

            switch (sortOrder)
            {
                case "name_desc":
                    emps = emps.OrderByDescending(s => s.FullName);
                    break;

                default:
                    emps = emps.OrderBy(s => s.FullName);
                    break;
            }



            #region zones list
            ZonesList zones = new ZonesList();
            zones = userBL.GetZones();
            string defZones = "";
            List<SelectListItem> zonesList = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(zone))
            {
                defZones = zone;
            }
            foreach (var item in zones)
            {
                zonesList.Add(new SelectListItem()
                {
                    Text = item.Zone,
                    Value = item.Id.ToString(),
                    Selected = (item.Zone == defZones ? true : false)
                });
            }
            zonesList.Add(new SelectListItem()
            {
                Text = "Employees Count Zonewise",
                Value = "count"

            });
            ViewBag.Zones = zonesList;
            #endregion

            if (!string.IsNullOrEmpty(empstatus))
            {
                ViewBag.StatusDDL = empstatus;
            }
            if (!string.IsNullOrEmpty(zone) || !string.IsNullOrEmpty(empstatus) || !string.IsNullOrEmpty(fromdate) || !string.IsNullOrEmpty(todate))
            {
                if (!string.IsNullOrEmpty(zone))
                {
                    if (zone != "count")
                    {
                        emps = emps.Where(s => s.FK_LocationorZone == int.Parse(zone));
                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.AddRange(new DataColumn[3] { new DataColumn("ZoneName"), new DataColumn("Active"), new DataColumn("Inactive") });


                        foreach (var item in zones)
                        {
                            var empszonewiseActive = (from empLst in empList select empLst);
                            empszonewiseActive = empszonewiseActive.Where(s => s.FK_LocationorZone == item.Id && s.Status == true);
                            var empszonewiseInactive = (from empLst in empList select empLst);
                            empszonewiseInactive = empszonewiseInactive.Where(s => s.FK_LocationorZone == item.Id && s.Status == false);


                            dt.Rows.Add(item.Zone, int.Parse(empszonewiseActive.Count().ToString()), int.Parse(empszonewiseInactive.Count().ToString()));

                        }
                        ZonesWiseStatusCountList zonesWiseStatusCountList = new ZonesWiseStatusCountList();
                        Universal.BusinessEntities.Core.ObjectBuilder.FillList<ZonesWiseStatusCount>(dt, zonesWiseStatusCountList);
                        ViewBag.StatusZonesWise = zonesWiseStatusCountList;

                    }
                }
                if (!string.IsNullOrEmpty(empstatus))
                {
                    emps = emps.Where(s => s.Status == bool.Parse(empstatus));
                }
                if ((!string.IsNullOrEmpty(fromdate) || !string.IsNullOrEmpty(todate)))
                {
                    if (string.IsNullOrEmpty(todate))
                    {
                        todate = DateTime.Now.ToString();
                    }
                    //if ((!string.IsNullOrEmpty(fromdate) && !string.IsNullOrEmpty(todate)))
                    //{
                    if (empstatus == "true")
                    {
                        emps = emps.Where(s => s.Status == bool.Parse(empstatus) && s.DOJ >= DateTime.Parse(fromdate) && s.DOJ <= DateTime.Parse(todate));
                    }
                    else if (empstatus == "false")
                    {
                        emps = emps.Where(s => s.Status == bool.Parse(empstatus) && s.CompanyLeftDate >= DateTime.Parse(fromdate) && s.CompanyLeftDate <= DateTime.Parse(todate));

                    }
                    else
                    {
                        emps = emps.Where(s => (s.DOJ >= DateTime.Parse(fromdate) && s.DOJ <= DateTime.Parse(todate)) || (s.CompanyLeftDate >= DateTime.Parse(fromdate) && s.CompanyLeftDate <= DateTime.Parse(todate)));
                    }

                }

            }

            if (zone != "count")
            {
                ViewBag.ZonesCount = emps.Count();
            }
            return View("EmployeeStatus", emps.ToPagedList(pageNumber, emps.ToList().Count != 0 ? emps.ToList().Count : 1));

        }


    }
}




