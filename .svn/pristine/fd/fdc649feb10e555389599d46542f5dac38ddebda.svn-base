using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Universal.BusinessEntities;
using System.Data;
using Universal.DataAccessLayer;


namespace Universal.BusinessLogicLayer
{
    public class UserBL
    {
        UserDA userda = new UserDA();

        public Employees ValidateUser(Employees emp)
        {
            DataTable customer = null;
            UserDA userda = new UserDA();
            Employees employees = new Employees();
            try
            {
                customer = userda.ValidateUser(emp);
                Universal.BusinessEntities.Core.ObjectBuilder.Fill<Employees>(customer, employees);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return employees;
        }

        public int? CreateUserGroup(UserGroups userGroup)
        {
            int? errcount = null;
            UserDA usrda = new UserDA();
            try
            {

                errcount = usrda.CreateUserGroup(userGroup);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return errcount;
        }


        public int? CreateTravel(TravelDesk travelDesk, int flag)
        {
            int? errcount = null;
            UserDA usrda = new UserDA();
            try
            {

                errcount = usrda.CreateTravel(travelDesk, flag);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return errcount;
        }

        public int? CreateNewZone(Zones zones)
        {
            int? errcount = null;
            UserDA usrda = new UserDA();
            try
            {

                errcount = usrda.CreateNewZone(zones);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return errcount;
        }

        public int? CreateLeave(Leave Leave)
        {
            int? errcount = null;
            UserDA usrda = new UserDA();

            int? nodaysapplied = 0;

            TimeSpan DateDiff = Leave.LeaveTo.Value.Subtract(Leave.LeaveFrom.Value);



            for (int i = 0; i <= DateDiff.Days; i++)
            {
                if (Leave.LeaveFrom.Value.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
                {

                }
                else
                {

                    nodaysapplied++;

                }

            }

            Leave.NoofDays = nodaysapplied++;

            try
            {

                errcount = usrda.CreateLeave(Leave);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return errcount;



        }

        public ZonesList GetZonesList()
        {
            DataTable status = null;
            UserDA userda = new UserDA();
            ZonesList zoneslist = new ZonesList();
            try
            {
                status = new DataTable();
                status = userda.GetZonesList();
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<Zones>(status, zoneslist);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return zoneslist;
        }

        public EventsCollection GetEventsList()
        {
            DataTable status = null;
            UserDA userda = new UserDA();
            EventsCollection eventsCollection = new EventsCollection();
            try
            {
                status = new DataTable();
                status = userda.GetEventsList();
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<Events>(status, eventsCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return eventsCollection;
        }
        public NewsCollection GetNewsList()
        {
            DataTable status = null;
            UserDA userda = new UserDA();
            NewsCollection newsCollection = new NewsCollection();
            try
            {
                status = new DataTable();
                status = userda.GetNewsList();
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<News>(status, newsCollection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newsCollection;
        }
        public EmployeesList GetEmployees()
        {
            DataTable dtEmployees = new DataTable();

            EmployeesList emps = new EmployeesList();

            try
            {
                dtEmployees = userda.GetEmployees();
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<Employees>(dtEmployees, emps);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return emps;
        }

        public LeaveList GetLeaves(int? userid, int? managerid)
        {
            DataTable dtLeaves = new DataTable();

            LeaveList lList = new LeaveList();

            try
            {
                dtLeaves = userda.GetLeaves(userid, managerid);
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<Leave>(dtLeaves, lList);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return lList;
        }



        public LeaveList GetAllLeaves(DateTime LeaveSDate, DateTime LeaveEDate)
        {
            DataTable dtLeaves = new DataTable();

            LeaveList lList = new LeaveList();

            try
            {
                dtLeaves = userda.GetAllLeaves(LeaveSDate, LeaveEDate);
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<Leave>(dtLeaves, lList);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return lList;
        }




        public UserGroupsList GetUserGroupList()
        {
            DataTable status = null;
            UserDA userda = new UserDA();
            UserGroupsList UserGroupsList = new UserGroupsList();
            try
            {
                status = new DataTable();
                status = userda.GetUserGroupList();
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<UserGroups>(status, UserGroupsList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return UserGroupsList;
        }

        public int? CreateEmployee(Employees emp, int flag)
        {
            int? errCount = null;

            try
            {
                errCount = userda.CreateEmployee(emp, flag);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return errCount;
        }


        public int? ApplyToJob(Jobs_AppliedCandidates jobs_AppliedCandidates)
        {
            int? errCount = null;

            try
            {
                errCount = userda.ApplyToJob(jobs_AppliedCandidates);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return errCount;
        }

        public UserGroupsList GetUserGroups()
        {
            DataTable dtUserGroups = new DataTable();

            UserGroupsList users = new UserGroupsList();

            try
            {
                dtUserGroups = userda.GetUserGroups();
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<UserGroups>(dtUserGroups, users);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return users;
        }

        public Jobs_AppliedCandidatesCollection IsAlreadyApplied(string emailId, int? jobid)
        {
            DataTable dt = new DataTable();

            Jobs_AppliedCandidatesCollection jobs_AppliedCandidatesCollection = new Jobs_AppliedCandidatesCollection();

            try
            {
                dt = userda.IsAlreadyApplied(emailId, jobid);
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<Jobs_AppliedCandidates>(dt, jobs_AppliedCandidatesCollection);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return jobs_AppliedCandidatesCollection;
        }

        public ZonesList GetZones()
        {
            DataTable dtZonesData = new DataTable();

            ZonesList zones = new ZonesList();

            try
            {
                dtZonesData = userda.GetZones();
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<Zones>(dtZonesData, zones);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return zones;
        }

        public StatesList GetStates()
        {
            DataTable dtStates = new DataTable();

            StatesList states = new StatesList();

            try
            {
                dtStates = userda.GetStates();
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<States>(dtStates, states);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return states;
        }

        public CitiesList GetCities()
        {
            DataTable dtCities = new DataTable();

            CitiesList cities = new CitiesList();

            try
            {
                dtCities = userda.GetCities();
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<City>(dtCities, cities);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return cities;
        }

        public Zones GetZone(int? id)
        {
            DataSet zonestable = new DataSet();

            Zones zones = new Zones();
            UserDA userda = new UserDA();
            try
            {
                zonestable = userda.GetZone(id);
                Universal.BusinessEntities.Core.ObjectBuilder.Fill<Zones>(zonestable.Tables[0], zones);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return zones;
        }

        public Employees GetAccountManagerDetails()
        {
            Employees empsAccMgr = new Employees();
            DataSet dtAccountManager = new DataSet();

            try
            {
                dtAccountManager = userda.GetAccountManagerDetails();
                Universal.BusinessEntities.Core.ObjectBuilder.Fill<Employees>(dtAccountManager.Tables[0], empsAccMgr);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return empsAccMgr;
        }

        public DataTable GetManagerDetails(int? ZoneId)
        {
            DataTable dtManagerDetails = new DataTable();

            try
            {
                dtManagerDetails = userda.GetManagerDetails(ZoneId);
            }
            catch (Exception Ex)
            {

                throw;
            }

            return dtManagerDetails;
        }

        public Employees GetEmployeeByID(string userid, int? Id)
        {
            DataSet emps = new DataSet();

            Employees emp = new Employees();
            try
            {
                emps = userda.GetEmployeeByID(userid, Id);
                Universal.BusinessEntities.Core.ObjectBuilder.Fill<Employees>(emps.Tables[0], emp);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return emp;
        }

        public object CheckEmployeeID(string EmpID)
        {
            object EmployeeID = new object();

            try
            {
                EmployeeID = userda.CheckEmployeeID(EmpID);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return EmployeeID;
        }

        public object VerifyZoneName(string zonename)
        {
            object zone = new object();
            try
            {
                zone = userda.VerifyZoneName(zonename);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return zone;
        }

        public object VerifyExpenseName(string expensename)
        {

            object expense = new object();
            try
            {
                expense = userda.VerifyExpenseType(expensename);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return expense;


        }



        public object VerifyUserGroupID(string UserGroupID)
        {
            object usergroup = new object();
            try
            {
                usergroup = userda.VerifyUserGroupID(UserGroupID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return usergroup;
        }

        public object VerifyPassportNumber(string passport1)
        {
            object passport = new object();
            try
            {
                passport = userda.VerifyPassportNumber(passport1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return passport;
        }
        public object CheckAadharID(string AadharID)
        {
            object Aadhar = new object();
            try
            {
                Aadhar = userda.CheckAadharID(AadharID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Aadhar;
        }

        public object CheckPancard(string Pancard)
        {
            object pancardid = new object();
            try
            {
                pancardid = userda.CheckPancard(Pancard);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pancardid;
        }

        public object CheckDrivingLicenceNo(string DrivingLicence)
        {
            object drivinglicence = new object();
            try
            {
                drivinglicence = userda.CheckDrivingLicenceNo(DrivingLicence);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return drivinglicence;
        }

        public UserGroups GetUserGroup(int? id)
        {
            DataSet usergrouptable = new DataSet();

            UserGroups usergroups = new UserGroups();
            UserDA userda = new UserDA();
            try
            {
                usergrouptable = userda.GetUserGroup(id);
                Universal.BusinessEntities.Core.ObjectBuilder.Fill<UserGroups>(usergrouptable.Tables[0], usergroups);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return usergroups;
        }
        public TravelDeskList GetTravelList()
        {
            DataTable status = null;
            UserDA userda = new UserDA();
            TravelDeskList travellist = new TravelDeskList();
            try
            {
                status = new DataTable();
                status = userda.GetTravelList();
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<TravelDesk>(status, travellist);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return travellist;
        }

        public object CheckExpenditureDateInLeave(DateTime ExpenditureDate, int? EmployeeID)
        {
            object Expendituredate = new object();

            try
            {
                Expendituredate = userda.CheckExpenditureDateInLeave(ExpenditureDate, EmployeeID);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return Expendituredate;
        }

        public object CheckExpenditureDate(DateTime ExpenditureDate, int? EmployeeID)
        {
            object LocalExpendituredate = new object();

            try
            {
                LocalExpendituredate = userda.CheckExpenditureDate(ExpenditureDate, EmployeeID);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return LocalExpendituredate;
        }

        public int? CreateExpenditure(UserExpenses_Header ObjUserExpenseHeader, UserExpense_Details ObjeUserExpenseDetails, Employees emp, out int? detailID)
        {
            int? ExpenseHeaderID = 0;
            try
            {
                ExpenseHeaderID = userda.CreateExpenditure(ObjUserExpenseHeader, ObjeUserExpenseDetails, emp, out detailID);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return ExpenseHeaderID;
        }

        public DataTable GetExpenditureTypes()
        {
            DataTable dtExpenditureTypes = new DataTable();

            try
            {
                dtExpenditureTypes = userda.GetExpenditureTypes();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return dtExpenditureTypes;
        }

        public DataTable GetExpenditureTypeDetails()
        {
            DataTable dtExpenditureTypeDetails = new DataTable();

            try
            {
                dtExpenditureTypeDetails = userda.GetExpenditureTypeDetails();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return dtExpenditureTypeDetails;
        }

        public UserExpenseDetailsList GetExpenditureDetailsByID(int? ExpenseHeaderID)
        {
            DataTable dtExpenditureDetailsByID = new DataTable();

            UserExpenseDetailsList dtExpenseDetails = new UserExpenseDetailsList();

            try
            {
                dtExpenditureDetailsByID = userda.GetExpenditureDetailsByID(ExpenseHeaderID);


                Universal.BusinessEntities.Core.ObjectBuilder.FillList<UserExpense_Details>(dtExpenditureDetailsByID, dtExpenseDetails);

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return dtExpenseDetails;
        }

        public int? CreateExpenditureDetails(UserExpenses_Header ObjUserExpenseHeader, UserExpense_Details ObjeUserExpenseDetails, Employees emp, out int? detailsID)
        {
            int? res = 0;

            try
            {
                res = userda.CreateExpenditureDetails(ObjUserExpenseHeader, ObjeUserExpenseDetails, emp, out detailsID);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return res;
        }

        public int? ExpenditureSubmit(bool status, int ExpenseHeaderID, string SONo, string SOExplanation, string SerialNoInstrument, string OrderConfirmationNo, string OrderConfirmationDate)
        {
            int? res = 0;

            try
            {
                res = userda.ExpenditureSubmit(status, ExpenseHeaderID, SONo, SOExplanation, SerialNoInstrument, OrderConfirmationNo, OrderConfirmationDate);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return res;
        }

        public UserExpensesHeaderList GetSubmittedExpenditure(int? EmployeeID)
        {
            DataTable dtExpenditureHeader = new DataTable();

            UserExpensesHeaderList userExpenditureHeaderList = new UserExpensesHeaderList();

            try
            {
                dtExpenditureHeader = userda.GetSubmittedExpenditure(EmployeeID);
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<UserExpenses_Header>(dtExpenditureHeader, userExpenditureHeaderList);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return userExpenditureHeaderList;
        }

        public UserExpensesHeaderList GetDraftedExpenditure(int? EmployeeID)
        {
            DataTable dtExpenditureHeader = new DataTable();

            UserExpensesHeaderList userExpenseHeaderList = new UserExpensesHeaderList();

            try
            {
                dtExpenditureHeader = userda.GetDraftedExpenditure(EmployeeID);
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<UserExpenses_Header>(dtExpenditureHeader, userExpenseHeaderList);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }


            return userExpenseHeaderList;
        }

        public int? CreateExpenseType(ExpenseType expensetype)
        {
            int? errcount = null;
            UserDA usrda = new UserDA();
            try
            {

                errcount = usrda.CreateExpenseType(expensetype);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return errcount;

        }

        public ExpenseType GetExpense(int? id)
        {
            DataSet Exptable = new DataSet();

            ExpenseType expensetype = new ExpenseType();
            UserDA userda = new UserDA();
            try
            {
                Exptable = userda.GetExpenseType(id);
                Universal.BusinessEntities.Core.ObjectBuilder.Fill<ExpenseType>(Exptable.Tables[0], expensetype);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return expensetype;
        }

        public ExpenseTypeList GetExpenseTypeList()
        {
            DataTable dtExpenseType = new DataTable();

            ExpenseTypeList expensetypelist = new ExpenseTypeList();

            try
            {
                dtExpenseType = userda.GetExpenseType();
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<ExpenseType>(dtExpenseType, expensetypelist);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return expensetypelist;

        }

        public TravelDesk GetTravelDesk(int? id)
        {
            DataSet taveltable = new DataSet();
            TravelDesk tlist = new TravelDesk();
            UserDA userda = new UserDA();
            try
            {
                taveltable = userda.GetTravelDesk(id);
                Universal.BusinessEntities.Core.ObjectBuilder.Fill<TravelDesk>(taveltable.Tables[0], tlist);
                if (tlist.Id != null)
                {
                    DataTable dt = new DataTable();
                    TravelUplaodTickets travelUploadTickets = new TravelUplaodTickets();
                    dt = userda.GetTravelTicketsListById(id);
                    Universal.BusinessEntities.Core.ObjectBuilder.FillList<TravelUploadTicket>(dt, travelUploadTickets);
                    tlist.travelTicketsCollection = travelUploadTickets;
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return tlist;
        }


        public ExpenseTypeDetailsList GetExpenditureTypeDetailsByID(int ExpenseTypeHeaderID)
        {
            DataTable dtExpenditureTypeDetailsByID = new DataTable();

            ExpenseTypeDetailsList expensetypedetailsList = new ExpenseTypeDetailsList();

            try
            {
                dtExpenditureTypeDetailsByID = userda.GetExpenditureTypeDetailsByID(ExpenseTypeHeaderID);
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<ExpenseTypeDetails>(dtExpenditureTypeDetailsByID, expensetypedetailsList);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return expensetypedetailsList;
        }

        public Employees ValidateUserId(Employees emp)
        {
            DataTable customer = null;
            UserDA userda = new UserDA();
            Employees employees = new Employees();
            try
            {
                customer = userda.ValidateUserId(emp);
                Universal.BusinessEntities.Core.ObjectBuilder.Fill<Employees>(customer, employees);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return employees;
        }

        public UserExpensesHeaderList GetUsersExpendituresforManager(int? EmployeeID, string ExpType)
        {
            DataTable dtExpenditureHeader = new DataTable();

            UserExpensesHeaderList userExpenseHeaderList = new UserExpensesHeaderList();

            try
            {
                dtExpenditureHeader = userda.GetUsersExpendituresforManager(EmployeeID, ExpType);
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<UserExpenses_Header>(dtExpenditureHeader, userExpenseHeaderList);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }


            return userExpenseHeaderList;
        }

        public UserExpensesHeaderList GetUsersExpendituresforAccountManager(string ExpType)
        {
            DataTable dtExpenditureHeader = new DataTable();

            UserExpensesHeaderList userExpenseHeaderList = new UserExpensesHeaderList();

            try
            {
                dtExpenditureHeader = userda.GetUsersExpendituresforAccountManager(ExpType);
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<UserExpenses_Header>(dtExpenditureHeader, userExpenseHeaderList);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }


            return userExpenseHeaderList;
        }

        public int? UpdateExpenditureStatus(UserExpenses_Header Exp, string userType)
        {
            int? result;
            try
            {
                result = userda.UpdateExpenditureStatus(Exp, userType);
            }
            catch (Exception ex)
            {

                throw ex;
            }



            return result;
        }

        public UserExpensesHeaderList GetUserExpenses(int? empID, DateTime MaxDate)
        {
            DataTable dtExpenditureHeader = new DataTable();

            UserExpensesHeaderList userExpenseHeaderList = new UserExpensesHeaderList();

            try
            {
                dtExpenditureHeader = userda.GetUserExpenses(empID, MaxDate);
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<UserExpenses_Header>(dtExpenditureHeader, userExpenseHeaderList);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }


            return userExpenseHeaderList;
        }

        public object CheckExpenditureDateInTraining(DateTime ExpenditureDate, int? EmployeeID)
        {
            object Expendituredate = new object();

            try
            {
                Expendituredate = userda.CheckExpenditureDateInTraining(ExpenditureDate, EmployeeID);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return Expendituredate;
        }


        public Leave LeaveDetailsById(int Id)
        {

            Leave LeaveDetails = new Leave();

            try
            {

                DataTable dtleavedetails = new DataTable();

                dtleavedetails = userda.LeaveDetailsById(Id);

                Universal.BusinessEntities.Core.ObjectBuilder.Fill<Leave>(dtleavedetails, LeaveDetails);

            }
            catch
            {

            }

            return LeaveDetails;


        }

        public int? UpdateLeaveStatus(Leave LeaveDetails)
        {

            int? updateresult = null;

            try
            {
                updateresult = userda.UpdateLeaveStatus(LeaveDetails);


            }
            catch
            {

            }

            return updateresult;




        }


        public DataTable ValidateLeaveDates(DateTime? sdate, DateTime? edate, int? empid)
        {
            DataTable getResult;

            try
            {

                getResult = userda.ValidateLeaveDates(sdate, edate, empid);

            }
            catch (Exception Ex)
            {

                throw Ex;
            }



            return getResult;


        }





        public Leave GenerateLeaveReport(Leave Leavedetails, LeaveList leavesList, DateTime? year)
        {


            Leave leave = new Leave();

            var empleavelist = (from elvlst in leavesList.Where(x => x.Employee_ID == Leavedetails.Employee_ID) select elvlst).ToList();

            var yearlyreport = (from yr in empleavelist
                                where
                                    (yr.LeaveFrom.Value.Year == year.Value.Year &&
                                    yr.LeaveTo.Value.Year == year.Value.Year)
                                select yr).ToList();

            int? yrreport = (from count in yearlyreport select count.NoofDays).Sum();

            int? yearlypending = (from pending in yearlyreport where pending.LeaveStatus == null select pending.NoofDays).Sum();

            int? yearlyapproved = (from approved in yearlyreport where approved.LeaveStatus == true select approved.NoofDays).Sum();

            int? yearlyrejected = (from rejected in yearlyreport where rejected.LeaveStatus == false select rejected.NoofDays).Sum();



            int? hpleavespending = (from hpcount in yearlyreport where hpcount.LeaveType == "Hospitalized (Admit)" && hpcount.LeaveStatus == null select hpcount.NoofDays).Sum();

            int? hpleavesapproved = (from hpcount in yearlyreport where hpcount.LeaveType == "Hospitalized (Admit)" && hpcount.LeaveStatus == true select hpcount.NoofDays).Sum();

            int? hpleavesrejected = (from hpcount in yearlyreport where hpcount.LeaveType == "Hospitalized (Admit)" && hpcount.LeaveStatus == false select hpcount.NoofDays).Sum();

            int? clleavespending = (from hpcount in yearlyreport where hpcount.LeaveType == "Casual Leave" && hpcount.LeaveStatus == null select hpcount.NoofDays).Sum();

            int? clleavesapproved = (from hpcount in yearlyreport where hpcount.LeaveType == "Casual Leave" && hpcount.LeaveStatus == true select hpcount.NoofDays).Sum();

            int? clleavesrejected = (from hpcount in yearlyreport where hpcount.LeaveType == "Casual Leave" && hpcount.LeaveStatus == false select hpcount.NoofDays).Sum();

            int? slleavespending = (from hpcount in yearlyreport where hpcount.LeaveType == "Sick Leave" && hpcount.LeaveStatus == null select hpcount.NoofDays).Sum();

            int? slleavesapproved = (from hpcount in yearlyreport where hpcount.LeaveType == "Sick Leave" && hpcount.LeaveStatus == true select hpcount.NoofDays).Sum();

            int? slleavesrejected = (from hpcount in yearlyreport where hpcount.LeaveType == "Sick Leave" && hpcount.LeaveStatus == false select hpcount.NoofDays).Sum();

            int? flleavespending = (from hpcount in yearlyreport where hpcount.LeaveType == "Funeral Leave" && hpcount.LeaveStatus == null select hpcount.NoofDays).Sum();

            int? flleavesapproved = (from hpcount in yearlyreport where hpcount.LeaveType == "Funeral Leave" && hpcount.LeaveStatus == true select hpcount.NoofDays).Sum();

            int? flleavesrejected = (from hpcount in yearlyreport where hpcount.LeaveType == "Funeral Leave" && hpcount.LeaveStatus == false select hpcount.NoofDays).Sum();


            int? hpleave = 0;
            int? clleave = 0;
            int? slleave = 0;
            int? flleave = 0;

            var yrlessthantoyr = (from subreport in empleavelist
                                  where
                                      (subreport.LeaveFrom.Value.Year == year.Value.Year) &&
                                      (subreport.LeaveTo.Value.Year != year.Value.Year)
                                  select subreport).ToList();

            int yrval = year.Value.Year;

            TimeSpan day = new TimeSpan(1, 0, 0, 0);

            TimeSpan? subyrreportdays = new TimeSpan(0, 0, 0, 0);

            TimeSpan? subreportonedays = new TimeSpan(0, 0, 0, 0);

            TimeSpan? subreporttwodays = new TimeSpan(0, 0, 0, 0);


            if (yrlessthantoyr.Count > 0)
            {
                DateTime yearlastdate = new DateTime(yrval, 12, 31);


                int? nodaysapplied = 0;




                for (int j = 0; j <= yearlastdate.Subtract(yrlessthantoyr[0].LeaveFrom.Value).Days; j++)
                {
                    if (yrlessthantoyr[0].LeaveFrom.Value.AddDays(j).DayOfWeek == DayOfWeek.Sunday)
                    {

                    }
                    else
                    {

                        nodaysapplied++;

                    }

                }

                if (yrlessthantoyr[0].LeaveStatus == null)

                    yearlypending = (yearlypending == null ? 0 : yearlypending) + nodaysapplied;

                else if (yrlessthantoyr[0].LeaveStatus == true)

                    yearlyapproved = (yearlyapproved == null ? 0 : yearlyapproved) + nodaysapplied;

                else
                    yearlyrejected = (yearlyrejected == null ? 0 : yearlyrejected) + nodaysapplied;




                if (yrlessthantoyr[0].LeaveType == "Hospitalized (Admit)")
                {

                    hpleave = nodaysapplied;

                    if (yrlessthantoyr[0].LeaveStatus == null)

                        hpleavespending = nodaysapplied + hpleavespending;

                    else if (yrlessthantoyr[0].LeaveStatus == true)

                        hpleavesapproved = nodaysapplied + hpleavesapproved;

                    else
                        hpleavesrejected = nodaysapplied + hpleavesrejected;

                }

                if (yrlessthantoyr[0].LeaveType == "Casual Leave")
                {
                    clleave = nodaysapplied;

                    if (yrlessthantoyr[0].LeaveStatus == null)

                        clleavespending = nodaysapplied + clleavespending;

                    else if (yrlessthantoyr[0].LeaveStatus == true)

                        clleavesapproved = nodaysapplied + clleavesapproved;

                    else
                        clleavesrejected = nodaysapplied + clleavesrejected;

                }

                if (yrlessthantoyr[0].LeaveType == "Sick Leave")
                {
                    slleave = nodaysapplied;

                    if (yrlessthantoyr[0].LeaveStatus == null)

                        slleavespending = nodaysapplied + slleavespending;

                    else if (yrlessthantoyr[0].LeaveStatus == true)

                        slleavesapproved = nodaysapplied + slleavesapproved;

                    else
                        slleavesrejected = nodaysapplied + slleavesrejected;

                }
                if (yrlessthantoyr[0].LeaveType == "Funeral Leave")
                {

                    flleave = nodaysapplied;

                    if (yrlessthantoyr[0].LeaveStatus == null)

                        flleavespending = nodaysapplied + flleavespending;

                    else if (yrlessthantoyr[0].LeaveStatus == true)

                        flleavesapproved = nodaysapplied + flleavesapproved;

                    else
                        flleavesrejected = nodaysapplied + flleavesrejected;

                }



            }




            var yrlessthanfromyr = (from subreport in empleavelist
                                    where
                                        (subreport.LeaveFrom.Value.Year != year.Value.Year) &&
                                        (subreport.LeaveTo.Value.Year == year.Value.Year)
                                    select subreport).ToList();




            if (yrlessthanfromyr.Count > 0)
            {
                DateTime yearfirstdate = new DateTime(yrval, 1, 1);

                int? nodaysapplied = 0;

                int g = yrlessthanfromyr[0].LeaveTo.Value.Subtract(yearfirstdate).Days;

                for (int j = 0; j <= yrlessthanfromyr[0].LeaveTo.Value.Subtract(yearfirstdate).Days; j++)
                {
                    if (yearfirstdate.AddDays(j).DayOfWeek == DayOfWeek.Sunday)
                    {

                    }
                    else
                    {

                        nodaysapplied++;

                    }

                }

                if (yrlessthanfromyr[0].LeaveStatus == null)

                    yearlypending = (yearlypending == null ? 0 : yearlypending) + nodaysapplied;

                else if (yrlessthanfromyr[0].LeaveStatus == true)

                    yearlyapproved = (yearlyapproved == null ? 0 : yearlyapproved) + nodaysapplied;

                else
                    yearlyrejected = (yearlyrejected == null ? 0 : yearlyrejected) + nodaysapplied;


                if (yrlessthanfromyr[0].LeaveType == "Hospitalized (Admit)")
                {

                    hpleave = nodaysapplied + hpleave;

                    if (yrlessthanfromyr[0].LeaveStatus == null)

                        hpleavespending = nodaysapplied + hpleavespending;

                    else if (yrlessthanfromyr[0].LeaveStatus == true)

                        hpleavesapproved = nodaysapplied + hpleavesapproved;

                    else
                        hpleavesrejected = nodaysapplied + hpleavesrejected;

                }

                if (yrlessthanfromyr[0].LeaveType == "Casual Leave")
                {

                    clleave = nodaysapplied + clleave;

                    if (yrlessthanfromyr[0].LeaveStatus == null)

                        clleavespending = nodaysapplied + clleavespending;

                    else if (yrlessthanfromyr[0].LeaveStatus == true)

                        clleavesapproved = nodaysapplied + clleavesapproved;

                    else
                        clleavesrejected = nodaysapplied + clleavesrejected;

                }

                if (yrlessthanfromyr[0].LeaveType == "Sick Leave")
                {
                    slleave = nodaysapplied + slleave;

                    if (yrlessthanfromyr[0].LeaveStatus == null)

                        slleavespending = nodaysapplied + slleavespending;

                    else if (yrlessthanfromyr[0].LeaveStatus == true)

                        slleavesapproved = nodaysapplied + slleavesapproved;

                    else
                        slleavesrejected = nodaysapplied + slleavesrejected;

                }
                if (yrlessthanfromyr[0].LeaveType == "Funeral Leave")
                {
                    flleave = nodaysapplied + flleave;

                    if (yrlessthanfromyr[0].LeaveStatus == null)

                        flleavespending = nodaysapplied + flleavespending;

                    else if (yrlessthanfromyr[0].LeaveStatus == true)

                        flleavesapproved = nodaysapplied + flleavesapproved;

                    else
                        flleavesrejected = nodaysapplied + flleavesrejected;

                }



            }


            leave.HospitalizedLeave = (from hpcount in yearlyreport where hpcount.LeaveType == "Hospitalized (Admit)" select hpcount.NoofDays).Sum() + hpleave;

            leave.CasualLeave = (from hpcount in yearlyreport where hpcount.LeaveType == "Casual Leave" select hpcount.NoofDays).Sum() + clleave;

            leave.SickLeave = (from hpcount in yearlyreport where hpcount.LeaveType == "Sick Leave" select hpcount.NoofDays).Sum() + slleave;

            leave.FuneralLeave = (from hpcount in yearlyreport where hpcount.LeaveType == "Funeral Leave" select hpcount.NoofDays).Sum() + flleave;



            leave.ApprovedLeaves = yearlyapproved == null ? 0 : yearlyapproved;

            leave.RejectedLeaves = yearlyrejected == null ? 0 : yearlyrejected;

            leave.PendingLeaves = yearlypending == null ? 0 : yearlypending;

            int? reportyr = yearlyapproved + yearlyrejected + yearlypending;

            leave.TotalNumberofLeaves = reportyr;


            leave.Employee_ID = Leavedetails.Employee_ID;

            return leave;
        }


        public DataTable ValidateTravelDeskDates(DateTime fromdate, DateTime todate, int? empid)
        {
            DataTable getResult;

            try
            {

                getResult = userda.ValidateTravelDeskDates(fromdate, todate, empid);

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return getResult;

        }


        public UserExpensesHeaderList GetManagerActionExpenses(int? EmpID, DateTime MaxDate)
        {
            DataTable dtExpenditureHeader = new DataTable();

            UserExpensesHeaderList userExpenseHeaderList = new UserExpensesHeaderList();

            try
            {
                dtExpenditureHeader = userda.GetManagerActionExpenses(EmpID, MaxDate);
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<UserExpenses_Header>(dtExpenditureHeader, userExpenseHeaderList);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }


            return userExpenseHeaderList;
        }
        public void sendmail(TravelDesk travelDesk)
        {


            UserBL userbl = new UserBL();
            Universal.BusinessEntities.EmployeesList empList = new Universal.BusinessEntities.EmployeesList();
            empList = userbl.GetEmployees();

            var group = (from ml in empList where ml.Id == Convert.ToInt16(travelDesk.FK_EmployeeId) select ml.FK_UserGroup).ToList();

            int? groupid = Convert.ToInt16(group[0]);

            var webmail = (from ml in empList where ml.Id == Convert.ToInt16(travelDesk.FK_EmployeeId) select ml.CompanyMailID).ToList();

            var managermail = (from ml in empList where ml.Id == Convert.ToInt16(travelDesk.FK_EmployeeId) select ml.ManagerEmailID).ToList();

            var accountmanagermail = (from ml in empList where ml.Id == Convert.ToInt16(travelDesk.FK_EmployeeId) select ml.AccountManagerEmailID).ToList();


            if (groupid == 1 || groupid == 2 || groupid == 4)
            {
                if (groupid == 1)
                {
                    managermail = webmail;

                    accountmanagermail = webmail;

                }
                else if (groupid == 2)
                {
                    managermail = webmail;

                    accountmanagermail = (from ml in empList where ml.Id == Convert.ToInt16(travelDesk.FK_EmployeeId) select ml.AccountManagerEmailID).ToList();


                }
                //else if (groupid == 4)
                //{
                //    managermail = webmail;
                //    accountmanagermail = (from ml in empList where ml.Id == Convert.ToInt16(travelDesk.EmployeeId) select ml.AccountManagerEmailID).ToList();
                //}
                else
                {
                    //managermail = webmail;
                    managermail = (from ml in empList where ml.Id == Convert.ToInt16(travelDesk.FK_EmployeeId) select ml.AccountManagerEmailID).ToList();

                    accountmanagermail = (from ml in empList where ml.Id == Convert.ToInt16(travelDesk.FK_EmployeeId) select ml.AccountManagerEmailID).ToList();

                }

            }
            else
            {

                managermail = (from ml in empList where ml.Id == Convert.ToInt16(travelDesk.FK_EmployeeId) select ml.ManagerEmailID).ToList();

                accountmanagermail = (from ml in empList where ml.Id == Convert.ToInt16(travelDesk.FK_EmployeeId) select ml.AccountManagerEmailID).ToList();


            }



            var traveldeskmail = (from ml in empList where ml.FK_UserGroup == 4 select ml.CompanyMailID).ToList();

            var name = (from ml in empList where ml.Id == Convert.ToInt16(travelDesk.FK_EmployeeId) select ml.FullName).ToList();

            var empid = (from ml in empList where ml.Id == Convert.ToInt16(travelDesk.FK_EmployeeId) select ml.Employee_ID).ToList();


            Emails mail = new Emails();
            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>Universal Surgicals</title></head><body style=\"background-color:#e7e8ea;\"> <div style=\"max-width:800px; width:100%; margin:0px auto;\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr bgcolor=\"#333645\" style=\"padding: 30px;\"><td height=\"150px;\" style=\"padding-left:30px;\"> <img src=\"http://universal.globaldatasolutions.com/Content/Images/login-logo.png\"></td></tr><tr bgcolor=\"#FFFFFF\"><td  style=\"font-family:'Open Sans';padding:40px;\"><div style=\"width:100%; font-size:18px; font-weight: bold; font-weight:800px; color:#3dbeba;\">  Dear " + name[0].ToString() + "(" + empid[0].ToString() + "),</div><div style=\"color:#868686; font-size:13px; margin-top:8px;\">New Travel itinerary assigned by Travel Desk with following details.</div><div style=\"border-radius: 5px; border:  solid 1px #e7e8ea; padding:20px;  font-size:14px;  font-weight: bold; color:#868686; margin-top:20px;\"><div><b>Traveling Details:</b></div><table width=\"100%\"><tr><td style=\"font-size:13px;  font-weight: normal; color:#868686;\"><div style=\"width:100%; text-align:center\">Travel Purpose: </div> <div style=\"width:100%; text-align:center\"><span style=\"color:#3dbeba; text-transform: capitalize;\"> " + travelDesk.Purpose + "</span> </div></td><td style=\"font-size:13px;  font-weight: normal; color:#868686;\"><div style=\"width:100%; text-align:center\">Travel Date: </div><div style=\"width:100%; text-align:center\"><span style=\"color:#3dbeba;\">" + travelDesk.TraveledFromDate.Value.ToShortDateString() + "</span></div></td><td style=\"font-size:13px;  font-weight: normal; color:#868686;\"><div style=\"width:100%; text-align:center\">From Location: </div><div style=\"width:100%; text-align:center\"><span style=\"color:#3dbeba;text-transform: capitalize;\">" + travelDesk.FromLocation + "</span></div></td><td style=\"font-size:13px;  font-weight: normal; color:#868686;\"><div style=\"width:100%; text-align:center\">To Location:</div><div style=\"width:100%; text-align:center\"><span style=\"color:#3dbeba;text-transform: capitalize;\">" + travelDesk.ToLocation + "</span></div></td><td style=\"font-size:13px;  font-weight: normal; color:#868686;\"><div style=\"width:100%; text-align:center\">Mode of Transport: </div><div style=\"width:100%; text-align:center\"> <span style=\"color:#3dbeba;\">" + travelDesk.ModeofTransportFrom + " </span></div></td>");
            sb.Append("<td style=\"font-size:13px;  font-weight: normal; color:#868686;\"> <div style=\"width:100%; text-align:center\">");
            if (travelDesk.TraveledToDate != null)
            {
                sb.Append("Return Date:</div> <div style=\"width:100%; text-align:center\"><span style=\"color:#3dbeba;\">" + travelDesk.TraveledToDate.Value.ToShortDateString() + " </span></div>");
            }
            sb.Append("</td>");
            sb.Append("<td style=\"font-size:13px;  font-weight: normal; color:#868686;\"> <div style=\"width:100%; text-align:center\">");
            if (travelDesk.TraveledToDate != null)
            {
                sb.Append("Return Mode of Transport :</div> <div style=\"width:100%; text-align:center\"><span style=\"color:#3dbeba;\">" + travelDesk.ModeofTransport + " </span></div>");
            }
            sb.Append("</td>");
            sb.Append("</tr></table>");
            sb.Append("<div style=\"margin-top:20px;\"><b>Accommodation Details:</b></div>");
            sb.Append("<table width=\"100%\">");
            sb.Append("<tr>");
            sb.Append("<td style=\"font-size:13px;  font-weight: normal; color:#868686;\"><div style=\"width:100%; text-align:center\"> ");
            if (travelDesk.HotelDetails != null)
            {
                sb.Append("Hotel Details:</div><div style=\"width:100%; text-align:center\"><span style=\"color:#3dbeba;text-transform: capitalize;\">" + travelDesk.HotelDetails + "</span></div>");
            }
            sb.Append("</td>");
            sb.Append("<td style=\"font-size:13px;  font-weight: normal; color:#868686;\"><div style=\"width:100%; text-align:center\">");
            if (travelDesk.HotelBookingAmount != null)
            {
                sb.Append("Hotel Booking Amount:</div><div style=\"width:100%; text-align:center\"><span style=\"color:#3dbeba;\">" + travelDesk.HotelBookingAmount + "</span></div>");
            }
            sb.Append("</td>");
            sb.Append("<td style=\"font-size:13px;  font-weight: normal; color:#868686;\"><div style=\"width:100%; text-align:center\">");
            if (travelDesk.CheckedIndate != null)
            {
                sb.Append("Checkedin Date:</div><div style=\"width:100%; text-align:center\"><span style=\"color:#3dbeba;\">" + travelDesk.CheckedIndate.Value.ToShortDateString() + "</span></div>");
            }
            sb.Append("</td>");
            sb.Append("<td style=\"font-size:13px;  font-weight: normal; color:#868686;\"><div style=\"width:100%; text-align:center\">");
            if (travelDesk.CheckedOutdate != null)
            {
                sb.Append("Checkedout Date:</div><div style=\"width:100%; text-align:center\"><span style=\"color:#3dbeba;\">" + travelDesk.CheckedOutdate.Value.ToShortDateString() + "</span></div>");
            }
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<div style=\"margin-top:20px;\"><b>Advance Amount:</b></div>");
            sb.Append("<table width=\"100%\">");
            sb.Append("<tr>");
            sb.Append("<td style=\"font-size:13px;  font-weight: normal; color:#868686;\"> <span style=\"color:#3dbeba;\">");
            if (travelDesk.AdvanceAmount != null)
            {
                sb.Append("" + travelDesk.AdvanceAmount + "");
            }
            sb.Append("</span> </td></tr></table>");
            sb.Append("</div>");
            sb.Append("<div style=\"color:#868686; font-size:13px; margin-top:15px;\">For more details please visit your Universal's dashboard. If you have any queries, please do not hesitate to contactTravel Desk Manager.</div>");
            sb.Append("<div style=\"color:#868686; font-size:13px; margin-top:15px; color:#3dbeba; float: right;  font-weight: 700; text-align: center;\"><b>Regards,</b><br/><b>Travel Desk</b><br/><b>Universal Surgicals</b></div>");
            sb.Append("</td></tr></table></div></body></html>");
            if (managermail.Count != 0 && webmail.Count != 0 && accountmanagermail.Count != 0 && traveldeskmail.Count != 0)
            {
                if (managermail[0] != null && webmail[0] != null && accountmanagermail[0] != null && traveldeskmail[0] != null)
                {

                    string cc = managermail[0].ToString() + "," + accountmanagermail[0].ToString();
                    string[] strArr = cc.Split(',');
                    mail.GDSSendBulkeMail(System.Configuration.ConfigurationManager.AppSettings["webmail"].ToString(), webmail[0].ToString(), "", cc, sb.ToString(), " Travel itinerary assigned by Travel Desk @ " + DateTime.Now.ToShortDateString() + " ", "", "Iwjil");
                }
            }
        }

        public ExpenseBlockedDatesList ExpenseBlockedDatesList()
        {
            DataTable list = new DataTable();
            ExpenseBlockedDatesList exblocklist = new ExpenseBlockedDatesList();
            try
            {
                list = userda.ExpenseBlockedDatesList();
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<ExpenseBlockedDates>(list, exblocklist);
            }
            catch (Exception EX)
            {

                throw EX;
            }

            return exblocklist;
        }

        public int? UpdatePaidAmount(string HeaderID, string PaidAmount)
        {
            int? updateresult = null;

            try
            {
                updateresult = userda.UpdatePaidAmount(HeaderID, PaidAmount);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return updateresult;
        }



        public UserExpensesHeaderList GetAllEmployeesSubmittedExpenditureRecords()
        {
            DataTable dtAllEmployeesExpenses = new DataTable();
            UserExpensesHeaderList userHeaderExpensesList = new UserExpensesHeaderList();

            try
            {
                dtAllEmployeesExpenses = userda.GetAllEmployeesSubmittedExpenditureRecords();
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<UserExpenses_Header>(dtAllEmployeesExpenses, userHeaderExpensesList);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return userHeaderExpensesList;
        }



        public int? BlockDates(ExpenseBlockedDates exblockedDates)
        {
            int? updateresult = null;

            try
            {
                updateresult = userda.BlockDates(exblockedDates);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return updateresult;
        }


        public object CheckExpenditureDateInBlockedList(DateTime ExpenditureDate, int? EmployeeID)
        {
            object Expendituredate = new object();

            try
            {
                Expendituredate = userda.CheckExpenditureDateInBlockedList(ExpenditureDate, EmployeeID);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return Expendituredate;

        }

        public DataTable GetClientManagerNames()
        {
            DataTable dtClientManagers = new DataTable();

            try
            {
                dtClientManagers = userda.GetClientManagerNames();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return dtClientManagers;
        }

        public object CheckMobileExpenditure(DateTime ExpenditureDate, int? EmployeeID)
        {
            object Expendituredate = new object();

            try
            {
                Expendituredate = userda.CheckMobileExpenditure(ExpenditureDate, EmployeeID);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return Expendituredate;
        }

        public object CheckInternetExpenditure(DateTime ExpenditureDate, int? EmployeeID)
        {
            object Expendituredate = new object();

            try
            {
                Expendituredate = userda.CheckInternetExpenditure(ExpenditureDate, EmployeeID);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return Expendituredate;
        }

        public void mailSend(UserExpenses_Header userexpenseheader, string ExpenseHeaderID)
        {
            UserBL userBL = new UserBL();

            Universal.BusinessEntities.Employees empList = new Universal.BusinessEntities.Employees();
            UserExpensesHeaderList userExpenditureHeaderList = new UserExpensesHeaderList();

            userExpenditureHeaderList = userBL.GetAllEmployeesSubmittedExpenditureRecords();

            var expenditureforEmployee = (from userexpense in userExpenditureHeaderList.Where(x => x.Id == Convert.ToInt32(ExpenseHeaderID)) select userexpense).ToList();

            int? empID = expenditureforEmployee[0].FK_EmployeeId;

            string UserID = null;

            empList = userBL.GetEmployeeByID(UserID, empID);

            Emails mail = new Emails();
            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>Universal Surgicals</title></head><body style=\"background-color:#e7e8ea;\"> <div style=\"max-width:800px; width:100%; margin:0px auto;\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr bgcolor=\"#333645\" style=\"padding: 30px;\"><td height=\"150px;\" style=\"padding-left:30px;\"> <img src=\"http://universal.globaldatasolutions.com/Content/Images/login-logo.png\"></td></tr><tr bgcolor=\"#FFFFFF\"><td  style=\"font-family:'Open Sans';padding:40px;\"><div style=\"width:100%; font-size:18px; font-weight: bold; font-weight:800px; color:#3dbeba;\">  Dear " + empList.FirstName + " " + empList.LastName + " (" + empList.Employee_ID + ") ,</div><div style=\"color:#868686; font-size:13px; margin-top:8px;\">Your expense submitted for (" + expenditureforEmployee[0].FromDate.Value.ToString("dd/MM/yyyy") + ") has been “Rejected” by your Account Manager with the following details;</div><div style=\"border-radius: 5px; border:  solid 1px #e7e8ea; padding:20px;  font-size:14px;  font-weight: bold; color:#868686; margin-top:20px;\"><table width=\"100%\"><tr><td style=\"font-size:13px;  font-weight: normal; color:#868686;\"><div style=\"width:100%; text-align:center\">Account Manager Comment:</div><div style=\"width:100%; text-align:center\"><span style=\"color:#3dbeba;text-transform: capitalize;\"> " + expenditureforEmployee[0].AccountManagerComment + "</span></div> </td><td style=\"font-size:13px;  font-weight: normal; color:#868686;\"><div style=\"width:100%; text-align:center\">Manager Comment:</div> <div style=\"width:100%; text-align:center\"><span style=\"color:#3dbeba;text-transform: capitalize;\">" + expenditureforEmployee[0].ManagerComments + " </span></div></td><td style=\"font-size:13px;  font-weight: normal; color:#868686;\"><div style=\"width:100%; text-align:center\">SO No:</div><div style=\"width:100%; text-align:center\"><span style=\"color:#3dbeba;\">" + expenditureforEmployee[0].ServiceOrderNo + "</span></div></td><td style=\"font-size:13px;  font-weight: normal; color:#868686;\"><div style=\"width:100%; text-align:center\">Amount:</div><div style=\"width:100%; text-align:center\"><span style=\"color:#3dbeba;\">" + expenditureforEmployee[0].TotalAmount + "</span></div></td></tr></table></div>");
            sb.Append("<div style=\"color:#868686; font-size:13px; margin-top:15px;\">For more details please visit your Universal's dashboard.<br/> Please contact your manager and account manager for further clarification. </div>");
            sb.Append("<div style=\"color:#868686; font-size:13px; margin-top:15px; color:#3dbeba; float: right;  font-weight: 700; text-align: center;\"><b>Regards,</b><br/><b>Expense Management Support !</b><br/><b>Universal Surgicals</b></div>");
            sb.Append("</td></tr></table></div></body></html>");
            if (empList.FK_UserGroup == 3)
            {
                mail.GDSSendBulkeMail(System.Configuration.ConfigurationManager.AppSettings["webmail"].ToString(), empList.CompanyMailID, "", empList.ManagerEmailID, sb.ToString(), " Expenses submitted for (" + expenditureforEmployee[0].FromDate.Value.ToString("dd/MM/yyyy") + ") has been “Rejected” by Account Manager ", "", "Iwjil");

            }
            else if (empList.FK_UserGroup == 2)
            {
                mail.GDSSendBulkeMail(System.Configuration.ConfigurationManager.AppSettings["webmail"].ToString(), empList.CompanyMailID, "", "", sb.ToString(), " Expenses submitted for (" + expenditureforEmployee[0].FromDate.Value.ToString("dd/MM/yyyy") + ") has been “Rejected” by Account Manager ", "", "Iwjil");
            }
            else if (empList.FK_UserGroup == 4)
            {
                mail.GDSSendBulkeMail(System.Configuration.ConfigurationManager.AppSettings["webmail"].ToString(), empList.CompanyMailID, "", "", sb.ToString(), " Expenses submitted for (" + expenditureforEmployee[0].FromDate.Value.ToString("dd/MM/yyyy") + ") has been “Rejected” by Account Manager ", "", "Iwjil");
            }
            else
            {
                mail.GDSSendBulkeMail(System.Configuration.ConfigurationManager.AppSettings["webmail"].ToString(), empList.CompanyMailID, "", "", sb.ToString(), " Expenses submitted for (" + expenditureforEmployee[0].FromDate.Value.ToString("dd/MM/yyyy") + ") has been “Rejected” by Account Manager ", "", "Iwjil");
            }

        }



        public void MailSend(UserExpenses_Header userexpenseheader, string ExpenseHeaderID)
        {
            UserBL userBL = new UserBL();

            Universal.BusinessEntities.Employees empList = new Universal.BusinessEntities.Employees();
            UserExpensesHeaderList userExpenditureHeaderList = new UserExpensesHeaderList();

            userExpenditureHeaderList = userBL.GetAllEmployeesSubmittedExpenditureRecords();

            var expenditureforEmployee = (from userexpense in userExpenditureHeaderList.Where(x => x.Id == Convert.ToInt32(ExpenseHeaderID)) select userexpense).ToList();

            int? empID = expenditureforEmployee[0].FK_EmployeeId;

            string UserID = null;

            empList = userBL.GetEmployeeByID(UserID, empID);

            Emails mail = new Emails();

            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>Universal Surgicals</title></head><body style=\"background-color:#e7e8ea;\"> <div style=\"max-width:800px; width:100%; margin:0px auto;\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr bgcolor=\"#333645\" style=\"padding: 30px;\"><td height=\"150px;\" style=\"padding-left:30px;\"> <img src=\"http://universal.globaldatasolutions.com/Content/Images/login-logo.png\"></td></tr><tr bgcolor=\"#FFFFFF\"><td  style=\"font-family:'Open Sans';padding:40px;\"><div style=\"width:100%; font-size:18px; font-weight: bold; font-weight:800px; color:#3dbeba;\">  Dear " + empList.FirstName + " " + empList.LastName + "(" + empList.Employee_ID + ") ,</div><div style=\"color:#868686; font-size:13px; margin-top:8px;\">Your expense submitted for (" + expenditureforEmployee[0].FromDate.Value.ToString("dd/MM/yyyy") + ") has been upheld by your Account Manager with the following details;</div><div style=\"border-radius: 5px; border:  solid 1px #e7e8ea; padding:20px;  font-size:14px;  font-weight: bold; color:#868686; margin-top:20px;\"><table width=\"100%\"><tr><td style=\"font-size:13px;  font-weight: normal; color:#868686;\"><div style=\"width:100%; text-align:center\">Account Manager Comment:</div><div style=\"width:100%; text-align:center\"> <span style=\"color:#3dbeba;text-transform: capitalize;\"> " + expenditureforEmployee[0].AccountManagerComment + "	</span></div> </td><td style=\"font-size:13px;  font-weight: normal; color:#868686;\"><div style=\"width:100%; text-align:center\">Manager Comment:</div><div style=\"width:100%; text-align:center\"> <span style=\"color:#3dbeba;text-transform: capitalize;\">" + expenditureforEmployee[0].ManagerComments + " </span></div></td><td style=\"font-size:13px;  font-weight: normal; color:#868686;\"><div style=\"width:100%; text-align:center\">SO No: </div><div style=\"width:100%; text-align:center\"><span style=\"color:#3dbeba;\">" + expenditureforEmployee[0].ServiceOrderNo + "</span></div></td><td style=\"font-size:13px;  font-weight: normal; color:#868686;\"><div style=\"width:100%; text-align:center\">Amount:</div><div style=\"width:100%; text-align:center\"><span style=\"color:#3dbeba;\">" + expenditureforEmployee[0].TotalAmount + "</span></div></td></tr></table></div>");
            sb.Append("<div style=\"color:#868686; font-size:13px; margin-top:15px;\">For more details please visit your Universal's dashboard.<br/> Please contact your manager and account manager for further clarification. </div><br/>");
            sb.Append("<div style=\"color:#868686; font-size:13px; margin-top:15px; color:#3dbeba; float: right;  font-weight: 700; text-align: center;\"><b>Regards,</b><br/><b>Expense Management Support !</b><br/><b>Universal Surgicals</b></div>");
            sb.Append("</td></tr></table></div></body></html>");
            if (empList.FK_UserGroup == 3)
            {
                mail.GDSSendBulkeMail(System.Configuration.ConfigurationManager.AppSettings["webmail"].ToString(), empList.CompanyMailID, "", empList.ManagerEmailID, sb.ToString(), " Expenses submitted for (" + expenditureforEmployee[0].FromDate.Value.ToString("dd/MM/yyyy") + ") has been “Upheld” by Account Manager ", "", "Iwjil");

            }
            else if (empList.FK_UserGroup == 2)
            {
                mail.GDSSendBulkeMail(System.Configuration.ConfigurationManager.AppSettings["webmail"].ToString(), empList.CompanyMailID, "", "", sb.ToString(), " Expenses submitted for (" + expenditureforEmployee[0].FromDate.Value.ToString("dd/MM/yyyy") + ") has been “Upheld” by Account Manager ", "", "Iwjil");
            }
            else if (empList.FK_UserGroup == 4)
            {
                mail.GDSSendBulkeMail(System.Configuration.ConfigurationManager.AppSettings["webmail"].ToString(), empList.CompanyMailID, "", "", sb.ToString(), " Expenses submitted for (" + expenditureforEmployee[0].FromDate.Value.ToString("dd/MM/yyyy") + ") has been “Upheld” by Account Manager ", "", "Iwjil");
            }
            else
            {
                mail.GDSSendBulkeMail(System.Configuration.ConfigurationManager.AppSettings["webmail"].ToString(), empList.CompanyMailID, "", "", sb.ToString(), " Expenses submitted for (" + expenditureforEmployee[0].FromDate.Value.ToString("dd/MM/yyyy") + ") has been “Upheld” by Account Manager ", "", "Iwjil");
            }
        }

        public void Mail(Leave leave)
        {


            Emails mail = new Emails();


            UserBL userbl = new UserBL();

            Universal.BusinessEntities.EmployeesList empList = new Universal.BusinessEntities.EmployeesList();

            empList = userbl.GetEmployees();

            var group = (from ml in empList where ml.Id == leave.Employee_ID select ml.FK_UserGroup).ToList();

            int? groupid = Convert.ToInt16(group[0]);

            var webmail = (from ml in empList where ml.Id == leave.Employee_ID select ml.CompanyMailID).ToList();

            var managermail = (from ml in empList where ml.Id == leave.Employee_ID select ml.ManagerEmailID).ToList();

            var accountmanagermail = (from ml in empList where ml.Id == leave.Employee_ID select ml.AccountManagerEmailID).ToList();

            var name = (from ml in empList where ml.Id == leave.Employee_ID select ml.FullName).ToList();




            if (groupid == 1 || groupid == 2 || groupid == 4)
            {
                if (groupid == 1)
                {
                    managermail = webmail;

                    accountmanagermail = webmail;

                }
                else if (groupid == 2)
                {
                    managermail = webmail;

                    accountmanagermail = (from ml in empList where ml.Id == leave.Employee_ID select ml.AccountManagerEmailID).ToList();

                }
                //else if (groupid == 4)
                //{
                //    managermail = webmail;
                //    accountmanagermail = (from ml in empList where ml.Id == Convert.ToInt16(travelDesk.EmployeeId) select ml.AccountManagerEmailID).ToList();
                //}
                else
                {
                    //managermail = webmail;
                    managermail = (from ml in empList where ml.Id == leave.Employee_ID select ml.AccountManagerEmailID).ToList();

                    accountmanagermail = (from ml in empList where ml.Id == leave.Employee_ID select ml.AccountManagerEmailID).ToList();

                }

            }
            else
            {

                managermail = (from ml in empList where ml.Id == leave.Employee_ID select ml.ManagerEmailID).ToList();

                accountmanagermail = (from ml in empList where ml.Id == leave.Employee_ID select ml.AccountManagerEmailID).ToList();

            }

            if (leave.LeaveStatus == true)
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>Universal Surgicals</title></head><body style=\"background-color:#e7e8ea;\"> <div style=\"max-width:800px; width:100%; margin:0px auto;\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr bgcolor=\"#333645\" style=\"padding: 30px;\"><td height=\"150px;\" style=\"padding-left:30px;\"> <img src=\"http://universal.globaldatasolutions.com/Content/Images/login-logo.png\"></td></tr><tr bgcolor=\"#FFFFFF\"><td  style=\"font-family:'Open Sans';padding:40px;\"><div style=\"width:100%; font-size:18px; font-weight: bold; font-weight:800px; color:#3dbeba;\">  Dear " + name[0].ToString() + ",</div><br/>");
                sb.Append("<div style=\"border-radius: 5px; border:  solid 1px #e7e8ea; padding:20px;  font-size:14px;  font-weight: bold; color:#868686; margin-top:20px;\"><table width=\"100%\"><tr><td style=\"font-size:13px;  font-weight: normal; color:#868686;\">Your leave request submitted for <b>From date:</b> " + leave.LeaveFrom.Value.ToShortDateString() + " <b>To date:</b> " + leave.LeaveTo.Value.ToShortDateString() + " has been <b>accepted</b> by your Manager. <br/></td> </tr></table></div>");
                sb.Append("<div style=\"color:#868686; font-size:13px; margin-top:15px; color:#3dbeba; float: right;  font-weight: 700; text-align: center;\"><b>Thanks,</b><br/><b>Expense Management Support !</b><br/><b>Universal Surgicals</b></div>");
                sb.Append("</td></tr></table></div></body></html>");



                if (managermail.Count != 0 && webmail.Count != 0 && accountmanagermail.Count != 0)
                {
                    if (managermail[0] != null && webmail[0] != null && accountmanagermail[0] != null)


                        mail.GDSSendBulkeMail(System.Configuration.ConfigurationManager.AppSettings["webmail"].ToString(), webmail[0].ToString(), "", accountmanagermail[0].ToString(), sb.ToString(), "Leave request Accepted by your Manager!! ", "", "Iwjil");
                }
            }
            else
            {
                StringBuilder Spend = new StringBuilder();
                Spend.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>Universal Surgicals</title></head><body style=\"background-color:#e7e8ea;\"> <div style=\"max-width:800px; width:100%; margin:0px auto;\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr bgcolor=\"#333645\" style=\"padding: 30px;\"><td height=\"150px;\" style=\"padding-left:30px;\"> <img src=\"http://universal.globaldatasolutions.com/Content/Images/login-logo.png\"></td></tr><tr bgcolor=\"#FFFFFF\"><td  style=\"font-family:'Open Sans';padding:40px;\"><div style=\"width:100%; font-size:18px; font-weight: bold; font-weight:800px; color:#3dbeba;\">  Dear " + name[0].ToString() + ",</div><br/>");
                Spend.Append("<div style=\"border-radius: 5px; border:  solid 1px #e7e8ea; padding:20px;  font-size:14px;  font-weight: bold; color:#868686; margin-top:20px;\"><table width=\"100%\"><tr><td style=\"font-size:13px;  font-weight: normal; color:#868686;\">Your leave request submitted for  <b>From date:</b> " + leave.LeaveFrom.Value.ToShortDateString() + " <b>To date:</b> " + leave.LeaveTo.Value.ToShortDateString() + " has been <b>rejected</b> by your Manager. <br/></td> </tr></table></div>");
                Spend.Append("<div style=\"color:#868686; font-size:13px; margin-top:15px; color:#3dbeba; float: right;  font-weight: 700; text-align: center;\"><b>Thanks,</b><br/><b>Expense Management Support !</b><br/><b>Universal Surgicals</b></div>");
                Spend.Append("</td></tr></table></div></body></html>");


                if (managermail.Count != 0 && webmail.Count != 0 && accountmanagermail.Count != 0)
                {

                    if (managermail[0] != null && webmail[0] != null && accountmanagermail[0] != null)
                        mail.GDSSendBulkeMail(System.Configuration.ConfigurationManager.AppSettings["webmail"].ToString(), webmail[0].ToString(), "", accountmanagermail[0].ToString(), Spend.ToString(), "Leave request Rejected by your Manager!! ", "", "Iwjil");
                }
            }

        }


        public UserExpensesHeaderList GetInstrumentNoExpenditures(int? EmployeeID)
        {
            DataTable dtGetInstrumentNoExpenditures = new DataTable();
            UserExpensesHeaderList userHeaderExpensesList = new UserExpensesHeaderList();

            try
            {
                dtGetInstrumentNoExpenditures = userda.GetInstrumentNoExpenditures(EmployeeID);
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<UserExpenses_Header>(dtGetInstrumentNoExpenditures, userHeaderExpensesList);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return userHeaderExpensesList;
        }
        public int? InsertSalaryDetails(SalaryDetails salaryDetails, int? flag)
        {
            int? result = 0;

            try
            {
                result = userda.InsertSalaryDetails(salaryDetails, flag);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return result;
        }

        public int? InsertSalaryDetailsNewFormat(SalaryDetails_NEW salaryDetails, int? flag)
        {
            int? result = 0;

            try
            {
                result = userda.InsertSalaryDetailsNewFormat(salaryDetails, flag);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return result;
        }
        public int? InsertSkillMetrics(SkillsMetrics skillsMetrics)
        {
            int? result = 0;
            DataTable dt = new DataTable();
            try
            {
                if (!string.IsNullOrEmpty(skillsMetrics.Empid))
                {

                    dt = userda.ValidateUserForSkill(skillsMetrics.Empid.Trim());
                    if (dt.Rows.Count == 0)
                        result = userda.InsertSkillMetrics(skillsMetrics, 1);
                    else
                        result = userda.InsertSkillMetrics(skillsMetrics, 2);
                }

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return result;
        }

        public int? UpdateOrderConfirmationNo(UserExpenses_Header objUserExpenseHeader)
        {
            int? result = 0;

            try
            {
                result = userda.UpdateOrderConfirmationNo(objUserExpenseHeader);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return result;
        }

        public UserExpensesHeaderList GetPaidExpenditures()
        {
            DataTable dtExpendituresPaid = new DataTable();

            UserExpensesHeaderList objuserExpenditure = new UserExpensesHeaderList();

            try
            {
                dtExpendituresPaid = userda.GetPaidExpenditures();

                Universal.BusinessEntities.Core.ObjectBuilder.FillList<UserExpenses_Header>(dtExpendituresPaid, objuserExpenditure);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return objuserExpenditure;
        }

        public void sendmailforUnblocking(ExpenseBlockedDates exdates)
        {
            int ExpenseDay = exdates.BlockingFromDate.Day;
            int ExpenseMonth = exdates.BlockingFromDate.Month;
            int Expenseyear = exdates.BlockingFromDate.Year;

            DateTime ClosingDate = exdates.ModifiedDate.Value.AddDays(2);

            string MonthName = "";

            int NoofDaysInAMonth = DateTime.DaysInMonth(Expenseyear, ExpenseMonth);

            switch (ExpenseMonth)
            {
                case 1:
                    MonthName = "January";
                    break;
                case 2:
                    MonthName = "February";
                    break;
                case 3:
                    MonthName = "March";
                    break;
                case 4:
                    MonthName = "April";
                    break;
                case 5:
                    MonthName = "May";
                    break;
                case 6:
                    MonthName = "June";
                    break;
                case 7:
                    MonthName = "July";
                    break;
                case 8:
                    MonthName = "August";
                    break;
                case 9:
                    MonthName = "September";
                    break;
                case 10:
                    MonthName = "October";
                    break;
                case 11:
                    MonthName = "November";
                    break;
                case 12:
                    MonthName = "December";
                    break;
                default:
                    break;
            }

            string SpanDates = "";

            if (ExpenseDay > 0 && ExpenseDay < 11)
            {
                SpanDates = "(01st-10th)";
            }
            else if (ExpenseDay > 10 && ExpenseDay < 21)
            {
                SpanDates = "(11th-20th)";
            }
            else
            {
                if (NoofDaysInAMonth == 31)
                {
                    SpanDates = "(21st-" + NoofDaysInAMonth + "st)";
                }
                else if (NoofDaysInAMonth == 30)
                {
                    SpanDates = "(21st-" + NoofDaysInAMonth + "th)";
                }
                else
                {
                    SpanDates = "(21st-" + NoofDaysInAMonth + "th)";
                }
            }

            Emails mail = new Emails();
            StringBuilder sb = new StringBuilder();

            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>Universal Surgicals</title></head><body style=\"background-color:#e7e8ea;\"> <div style=\"max-width:800px; width:100%; margin:0px auto;\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr bgcolor=\"#333645\" style=\"padding: 30px;\"><td height=\"150px;\" style=\"padding-left:30px;\"> <img src=\"http://universal.globaldatasolutions.com/Content/Images/login-logo.png\"></td></tr><tr bgcolor=\"#FFFFFF\"><td  style=\"font-family:'Open Sans';padding:40px;\"><div style=\"width:100%; font-size:18px; font-weight: bold; font-weight:800px; color:#3dbeba;\">  Dear " + exdates.Name + " (" + exdates.UserId + "),</div><br/>");
            sb.Append("<div style=\"border-radius: 5px; border:  solid 1px #e7e8ea; padding:20px;  font-size:14px;  font-weight: bold; color:#868686; margin-top:20px;\"><table width=\"100%\"><tr><td style=\"font-size:13px;  font-weight: normal; color:#868686;\"> Your expense submission access for " + MonthName + "  " + SpanDates + " span has been unblocked for 2 days, please submit all your pending expenses for that particular period by " + ClosingDate.ToString("MMM dd, yyyy").ToUpper() + " .  <br/> <br/><b>Account Manager's Comment : </b> " + exdates.Comments + " <br/> <br/> For any clarifications or queries please contact your Account manager.  <br/></td> </tr></table></div>");
            sb.Append("<div style=\"color:#868686; font-size:13px; margin-top:15px; color:#3dbeba; float: right;  font-weight: 700; text-align: center;\"><b>Regards,</b><br/><b>Expense Management Support !</b><br/><b>Universal Surgicals </b></div>");
            sb.Append("</td></tr></table></div></body></html>");
            if (exdates.ACMailId != null && !(string.IsNullOrEmpty(exdates.ACMailId)))
            {
                mail.GDSSendBulkeMail(System.Configuration.ConfigurationManager.AppSettings["webmail"].ToString(), exdates.EmpMailId, "", exdates.ACMailId, sb.ToString(), " Access Unblocked for 2 days @ " + MonthName + " " + SpanDates + " span !  ", "", "Iwjil");
            }
          
            else
            {
                mail.GDSSendBulkeMail(System.Configuration.ConfigurationManager.AppSettings["webmail"].ToString(), exdates.EmpMailId, "", exdates.EmpMailId, sb.ToString(), " Access Unblocked for 2 days @ " + MonthName + " " + SpanDates + " span !  ", "", "Iwjil");
            }
        }


        public ExpenseBlockedDates GetBlockedDatesById(int? Id)
        {

            ExpenseBlockedDates exblock = new ExpenseBlockedDates();

            try
            {

                DataTable dtleavedetails = new DataTable();

                dtleavedetails = userda.GetBlockedDatesById(Id);

                Universal.BusinessEntities.Core.ObjectBuilder.Fill<ExpenseBlockedDates>(dtleavedetails, exblock);

            }
            catch
            {

            }

            return exblock;


        }

        public object VerifyOldPassword(string password, int? id)
        {
            object oldpassword = new object();
            try
            {
                oldpassword = userda.VerifyOldPassword(password, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oldpassword;
        }

        public object updatepassword(string password, int? id)
        {
            object newpassword = new object();
            try
            {
                newpassword = userda.updatepassword(password, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newpassword;
        }

        public void changepassword(string password, int? id)
        {
            UserBL userBL = new UserBL();
            Employees emp = new Employees();
            emp = userBL.GetEmployeeByID(null, id);
            Emails mail = new Emails();
            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>Universal Surgicals</title></head><body style=\"background-color:#e7e8ea;\"> <div style=\"max-width:800px; width:100%; margin:0px auto;\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr bgcolor=\"#333645\" style=\"padding: 30px;\"><td height=\"150px;\" style=\"padding-left:30px;\"> <img src=\"http://universal.globaldatasolutions.com/Content/Images/login-logo.png\"></td></tr><tr bgcolor=\"#FFFFFF\"><td  style=\"font-family:'Open Sans';padding:40px;\"><div style=\"width:100%; font-size:18px; font-weight: bold; font-weight:800px; color:#3dbeba;\">Dear " + emp.FirstName + " " + emp.LastName + " (" + emp.Employee_ID + "),</div><div style=\"border-radius: 5px; border:  solid 1px #e7e8ea; padding:20px;  font-size:14px;  font-weight: bold; color:#868686; margin-top:20px;\"><table width=\"100%\"><tr><td style=\"font-size:13px;  font-weight: normal; color:#868686;\"> <b>Your password has been reset successfully.</b> </td> </tr></table></div>");
            sb.Append("<div style=\"color:#868686; font-size:13px; margin-top:15px;\">Please contact your account manager immediately if you have not made this changes.<br/></div>");
            sb.Append("<div style=\"color:#868686; font-size:13px; margin-top:15px; color:#3dbeba; float: right;  font-weight: 700; text-align: center;\"><b>Expense Management Support !</b><br/><b>Universal Surgicals</b></div>");
            sb.Append("</td></tr></table></div></body></html>");

            mail.GDSSendBulkeMail(System.Configuration.ConfigurationManager.AppSettings["webmail"].ToString(), emp.CompanyMailID, "", "", sb.ToString(), "Your password has been reset successfully !", "", "Iwjil");

        }

        public int? UpdateprofileImage(Employees employee)
        {

            int? result = null;

            try
            {
                result = userda.UpdateProfileImage(employee);
            }
            catch
            {


            }

            return result;

        }

        public int? InsertMultipleServiceOrderNos(int? ExpenseHeaderID, ServiceOrderNos objServiceOrderNosList, Employees emp)
        {
            int resMulti = 0;
            try
            {
                resMulti = userda.InsertMultipleServiceOrderNos(ExpenseHeaderID, objServiceOrderNosList, emp);
            }
            catch (Exception)
            {

                throw;
            }
            return resMulti;
        }

        public ServiceOrderNos_List GetMultipleSONOsbyExpenseHeaderID(int? ExpenseHeaderID)
        {
            DataTable dtData = new DataTable();
            ServiceOrderNos_List objServiceOrderNosList = new ServiceOrderNos_List();

            try
            {
                dtData = userda.GetMultipleSONOsbyExpenseHeaderID(ExpenseHeaderID);
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<ServiceOrderNos>(dtData, objServiceOrderNosList);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return objServiceOrderNosList;
        }

        public EmployeesList GetEmployeesByDOB(int date, int month)
        {
            DataTable dtData = new DataTable();
            EmployeesList employeesList = new EmployeesList();
            try
            {
                dtData = userda.GetEmployeesByDOB(date, month);
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<Employees>(dtData, employeesList);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return employeesList;
        }

        public int? DeleteMultipleSONOs(string SONOID)
        {
            int? res;

            try
            {
                res = userda.DeleteMultipleSONos(SONOID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return res;
        }

        public int? DeleteEventImages(EventImages evImages)
        {
            int? res;

            try
            {
                res = userda.DeleteEventImages(evImages);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return res;
        }
        public int UpdateMultipleSONOs(ServiceOrderNos objserviceOrderNos)
        {
            int resUpdateMultipleSONOs;

            try
            {
                resUpdateMultipleSONOs = userda.UpdateMultipleSONOs(objserviceOrderNos);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return resUpdateMultipleSONOs;
        }

        public UserExpensesHeaderList GetExpenditureReport(DateTime ExpenditureSDate, DateTime ExpenditureEDate, int? EmpID)
        {
            DataTable dtExpenditureHeader = new DataTable();

            UserExpensesHeaderList userExpenseHeaderList = new UserExpensesHeaderList();

            try
            {
                dtExpenditureHeader = userda.GetExpenditureReport(ExpenditureSDate, ExpenditureEDate, EmpID);
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<UserExpenses_Header>(dtExpenditureHeader, userExpenseHeaderList);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }


            return userExpenseHeaderList;
        }

        public void RegistrationMail(Employees emp)
        {
            Emails mail = new Emails();
            StringBuilder sb = new StringBuilder();
            string url = System.Configuration.ConfigurationManager.AppSettings["SystemURL"];
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>Universal Surgicals</title></head><body style=\"background-color:#e7e8ea;\"> <div style=\"max-width:800px; width:100%; margin:0px auto;\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr bgcolor=\"#333645\" style=\"padding: 30px;\"><td height=\"150px;\" style=\"padding-left:30px;\"> <img src=\"http://universal.globaldatasolutions.com/Content/Images/login-logo.png\"></td></tr><tr bgcolor=\"#FFFFFF\"><td  style=\"font-family:'Open Sans';padding:40px;\"><div style=\"width:100%; font-size:18px; font-weight: bold; font-weight:800px; color:#3dbeba;\">  Dear " + emp.FirstName + " " + emp.LastName + " ,</div><div style=\"color:#868686; font-size:13px; margin-top:8px;\"> <b>Welcome to Universal Surgicals-- Expense Management ! </b></div><div style=\"color:#868686; font-size:13px; margin-top:8px;\"> Your account has been successfully created by your Account Manager. Below are the login details; </div><div style=\"border-radius: 5px; border:  solid 1px #e7e8ea; padding:20px;  font-size:14px;  font-weight: bold; color:#868686; margin-top:20px;\"><table width=\"100%\"><tr><td style=\"font-size:13px;  font-weight: normal; color:#868686;\"><div style=\"width:100%; text-align:center\"><b>User Id:-</b></div><div style=\"width:100%; text-align:center\"><span style=\"color:#3dbeba;\"> " + emp.Employee_ID + "</span></div> </td><td style=\"font-size:13px;  font-weight: normal; color:#868686;\"><div style=\"width:100%; text-align:center\"><b>Password:-</b></div> <div style=\"width:100%; text-align:center\"><span style=\"color:#3dbeba;\">" + emp.Employee_ID + " </span></div></td></tr></table></div>");
            sb.Append("<div style=\"color:#868686; font-size:13px; margin-top:15px;\">For security reasons please change your password as soon as possible.</div>");
            sb.Append("<div style=\"color:#868686; font-size:13px; margin-top:15px;\"> " + url + " to login your account.</div>");
            sb.Append("<div style=\"color:#868686; font-size:13px; margin-top:15px; color:#3dbeba; float: right;  font-weight: 700; text-align: center;\"><b>Regards,</b><br/><b>Expense Management Support !</b><br/><b>Universal Surgicals</b></div>");
            sb.Append("</td></tr></table></div></body></html>");
            mail.GDSSendBulkeMail(System.Configuration.ConfigurationManager.AppSettings["webmail"].ToString(), emp.CompanyMailID, "", "", sb.ToString(), " Welcome to Universal Surgicals-- Expense Management !! ", "", "Iwjil");
        }

        public UserExpensesHeaderList GetExpenditureReportByServiceOrderNo(int value)
        {
            DataTable dtExpenditureHeader = new DataTable();

            UserExpensesHeaderList userExpenseHeaderList = new UserExpensesHeaderList();

            try
            {
                dtExpenditureHeader = userda.GetExpenditureReportByServiceOrderNo(value);
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<UserExpenses_Header>(dtExpenditureHeader, userExpenseHeaderList);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }


            return userExpenseHeaderList;
        }
        public UserExpensesHeaderList GetExpenditureNonMatchingSONumbers(string SOOrder, string SOCNumber)
        {
            DataTable dtExpenditureHeader = new DataTable();

            UserExpensesHeaderList userExpenseHeaderList = new UserExpensesHeaderList();

            try
            {
                dtExpenditureHeader = userda.GetExpenditureNonMatchingSONumbers(SOOrder, SOCNumber);
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<UserExpenses_Header>(dtExpenditureHeader, userExpenseHeaderList);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }


            return userExpenseHeaderList;
        }


        public JobsCollection GetJobsList()
        {
            DataSet ds = new DataSet();
            JobsCollection jobsCollection = new JobsCollection();
            try
            {
                ds = userda.GetJobsList();
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<Jobs>(ds.Tables[0], jobsCollection);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return jobsCollection;
        }


        public int? PostNewJob(Jobs jobs, int flag)
        {
            int? errCount = null;
            UserDA userDA = new UserDA();
            try
            {
                errCount = userDA.PostNewJob(jobs, flag);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return errCount;
        }
        public int? InsertEventImages(EventImages eventImages)
        {
            int? errCount = null;
            UserDA userDA = new UserDA();
            try
            {
                errCount = userDA.InsertEventImages(eventImages);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return errCount;
        }
        public int? PostNewEvent(Events events, int flag)
        {
            int? errCount = null;
            UserDA userDA = new UserDA();
            try
            {
                errCount = userDA.PostNewEvent(events, flag);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return errCount;
        }
        public int? PostNews(News news, int flag)
        {
            int? errCount = null;
            UserDA userDA = new UserDA();
            try
            {
                errCount = userDA.PostNews(news, flag);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return errCount;
        }

        public Jobs JobdetailsById(int? id)
        {
            Jobs jobdetails = new Jobs();
            try
            {
                DataTable dtjobdetails = new DataTable();
                dtjobdetails = userda.JobdetailsById(id);
                Universal.BusinessEntities.Core.ObjectBuilder.Fill<Jobs>(dtjobdetails, jobdetails);
                Jobs_AppliedCandidatesCollection jobs_AppliedCandidatesCollection = new Jobs_AppliedCandidatesCollection();
                jobs_AppliedCandidatesCollection = GetAppliedCandidateDetails(id);
                jobdetails.jobs_AppliedCandidatesCollection = jobs_AppliedCandidatesCollection;
            }

            catch
            {

            }
            return jobdetails;
        }

        public Jobs_AppliedCandidatesCollection GetAppliedCandidateDetails(int? JobId)
        {
            Jobs_AppliedCandidatesCollection jobdetaisid = new Jobs_AppliedCandidatesCollection();
            try
            {
                DataTable dtjobcandidates = new DataTable();
                dtjobcandidates = userda.GetAppliedCandidateDetails(JobId);
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<Jobs_AppliedCandidates>(dtjobcandidates, jobdetaisid);


            }
            catch
            {

            }
            return jobdetaisid;
        }


        public Events EventDetailsById(int? id)
        {
            Events events = new Events();
            try
            {
                DataTable dt = new DataTable();
                dt = userda.EventDetailsById(id);
                Universal.BusinessEntities.Core.ObjectBuilder.Fill<Events>(dt, events);
                if (events.Id != null)
                {
                    DataTable dtable = new DataTable();
                    EventImagesCollection eimgCollection = new EventImagesCollection();
                    dtable = userda.EventImagesListById(id);
                    Universal.BusinessEntities.Core.ObjectBuilder.FillList<EventImages>(dtable, eimgCollection);
                    events.eventImagesCollection = eimgCollection;
                }

            }

            catch
            {

            }
            return events;
        }
        public News NewsDetailsById(int? id)
        {
            News news = new News();
            try
            {
                DataTable dt = new DataTable();
                dt = userda.NewsDetailsById(id);
                Universal.BusinessEntities.Core.ObjectBuilder.Fill<News>(dt, news);
            }

            catch
            {

            }
            return news;
        }

        public DataTable GetDatesListinBetween(DateTime ExpenditureSDate, DateTime ExpenditureEDate)
        {
            DataTable dtDatesList = new DataTable();

            try
            {
                dtDatesList = userda.GetDatesListinBetween(ExpenditureSDate, ExpenditureEDate);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }


            return dtDatesList;
        }

        public int? CreateEmployeeAssets(EmployeeAssets objEmployeeAssets)
        {
            int? errCount = null;
            UserDA userDA = new UserDA();
            try
            {
                errCount = userDA.CreateEmployeeAssets(objEmployeeAssets);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return errCount;
        }

        public EmployeeAssetsList GetEmployeeAssets()
        {
            DataTable dtEmployeeAssets = new DataTable();
            EmployeeAssetsList objEmployeeAssetsList = new EmployeeAssetsList();

            try
            {
                dtEmployeeAssets = userda.GetEmployeeAssets();
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<EmployeeAssets>(dtEmployeeAssets, objEmployeeAssetsList);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }


            return objEmployeeAssetsList;
        }

        public DataTable dtGetEmployeeAssets()
        {
            DataTable dtEmployeeAssets = new DataTable();
            try
            {
                dtEmployeeAssets = userda.GetEmployeeAssets();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return dtEmployeeAssets;
        }

        public SkillsMetrics GetSkillMetrics(string Employeeid)
        {
            DataTable dtSkillmetrics = new DataTable();
            SkillsMetrics objSkillmetrics = new SkillsMetrics();
            try
            {
                dtSkillmetrics = userda.GetSkillMetrics(Employeeid);
                Universal.BusinessEntities.Core.ObjectBuilder.Fill<SkillsMetrics>(dtSkillmetrics, objSkillmetrics);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return objSkillmetrics;
        }



        public SalaryDetails GetSalaryDetails(string EmployeeId)
        {
            DataTable dtSalaryDetailsList = new DataTable();
            SalaryDetails objSalaryDetailsList = new SalaryDetails();
            try
            {
                dtSalaryDetailsList = userda.GetSalaryDetails(EmployeeId);
                Universal.BusinessEntities.Core.ObjectBuilder.Fill<SalaryDetails>(dtSalaryDetailsList, objSalaryDetailsList);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return objSalaryDetailsList;
        }
        public SalaryDetails EmpGetSalaryDetailsForMonth(string EmployeeId,int month)
        {
            DataTable dtSalaryDetailsList = new DataTable();
            SalaryDetails objSalaryDetailsList = new SalaryDetails();
            try
            {
                dtSalaryDetailsList = userda.EmpGetSalaryDetailsForMonth(EmployeeId,month);
                Universal.BusinessEntities.Core.ObjectBuilder.Fill<SalaryDetails>(dtSalaryDetailsList, objSalaryDetailsList);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return objSalaryDetailsList;
        }
        public SalaryDetails_NEW GetSalaryDetailsNewFormat(string EmployeeId)
        {
            DataTable dtSalaryDetailsList = new DataTable();
            SalaryDetails_NEW objSalaryDetailsList = new SalaryDetails_NEW();
            try
            {
                dtSalaryDetailsList = userda.GetSalaryDetailsByMonthEmployeeNewFormat(EmployeeId);
                Universal.BusinessEntities.Core.ObjectBuilder.Fill<SalaryDetails_NEW>(dtSalaryDetailsList, objSalaryDetailsList);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return objSalaryDetailsList;
        }
        public SalaryDetails_NEW EmpGetSalaryDetailsForMonthNewFormat(string EmployeeId,int month)
        {
            DataTable dtSalaryDetailsList = new DataTable();
            SalaryDetails_NEW objSalaryDetailsList = new SalaryDetails_NEW();
            try
            {
                dtSalaryDetailsList = userda.EmpGetSalaryDetailsForMonthNewFormat(EmployeeId,month);
                Universal.BusinessEntities.Core.ObjectBuilder.Fill<SalaryDetails_NEW>(dtSalaryDetailsList, objSalaryDetailsList);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return objSalaryDetailsList;
        }
        public DataTable GetSalaryDetailsByMonthEmployee(string EmployeeId)
        {
            DataTable dtSalaryDetailsList = new DataTable();
            try
            {
                dtSalaryDetailsList = userda.GetSalaryDetails(EmployeeId);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return dtSalaryDetailsList;
        }

    
        public DataTable GetSalaryDetailsByMonthEmployeeNewFormat(string EmployeeId)
        {
            DataTable dtSalaryDetailsList = new DataTable();
            try
            {
                dtSalaryDetailsList = userda.GetSalaryDetailsByMonthEmployeeNewFormat(EmployeeId);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return dtSalaryDetailsList;
        }
        public object CheckExpenditureDateInTravelDesk(DateTime ExpenditureDate1, int? EmployeeID)
        {
            object Expendituredate = new object();

            try
            {
                Expendituredate = userda.CheckExpenditureDateInTravelDesk(ExpenditureDate1, EmployeeID);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return Expendituredate;
        }

        public UserExpensesHeaderList GetEmployeeMonthlyExpenditures()
        {
            DataTable dtExpenditureHeader = new DataTable();
            UserExpensesHeaderList userExpenseHeaderList = new UserExpensesHeaderList();

            try
            {
                dtExpenditureHeader = userda.GetEmployeeMonthlyExpenditures();
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<UserExpenses_Header>(dtExpenditureHeader, userExpenseHeaderList);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }


            return userExpenseHeaderList;
        }

        public object GetSpanAmountForEmmployee(DateTime ExpenditureSDate, DateTime ExpenditureEDate, int FK_EmpID)
        {
            UserDA objUserDA = new UserDA();
            object result = new object();

            try
            {
                result = objUserDA.GetSpanAmountForEmmployee(ExpenditureSDate, ExpenditureEDate, FK_EmpID);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return result;
        }

        public UserExpensesHeaderList GetEmployeeSpanRecords(DateTime SDate, DateTime EDate, string EmpID)
        {
            UserExpensesHeaderList objUserExpenseHeaderList = new UserExpensesHeaderList();
            DataTable dtData = new DataTable();

            try
            {
                dtData = userda.GetEmployeeSpanRecords(SDate, EDate, EmpID);
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<UserExpenses_Header>(dtData, objUserExpenseHeaderList);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return objUserExpenseHeaderList;
        }
        public int? InsertTravelUploadTicket(TravelUploadTicket trvelUpload)
        {
            int? errCount = null;
            UserDA userDA = new UserDA();
            try
            {
                errCount = userDA.InsertTravelUploadTicket(trvelUpload);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return errCount;
        }
        public int? DeleteTravelTicket(TravelUploadTicket travelUploadTicket)
        {
            int? resTravel;
            try
            {
                resTravel = userda.DeleteTravelTicket(travelUploadTicket);
            }
            catch (Exception)
            {

                throw;
            }
            return resTravel;
        }
        public TravelUplaodTickets GetTravelTicketsListById(int? id)
        {
            DataTable dt = new DataTable();
            TravelUplaodTickets uploadList = new TravelUplaodTickets();

            try
            {
                dt = userda.GetTravelTicketsListById(id);
                Universal.BusinessEntities.Core.ObjectBuilder.FillList<TravelUploadTicket>(dt, uploadList);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return uploadList;
        }

        public object GetEmployeeExpenditureCountInSpanDates(int? EmpID, string StartDate, string EndDate)
        {
            UserDA objUserDA = new UserDA();
            object result = new object();

            try
            {
                result = objUserDA.GetEmployeeExpenditureCountInSpanDates(EmpID, StartDate, EndDate);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return result;
        }
    }
}


