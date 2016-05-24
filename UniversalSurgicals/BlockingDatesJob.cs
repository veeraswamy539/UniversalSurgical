using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using System.Text;
using Universal.BusinessEntities;
using Universal.BusinessLogicLayer;
using System.IO;
using System.Net;
using System.Threading;
using System.Globalization;
namespace UniversalSurgicals
{
    public class BlockingDatesJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            #region object creations for entities
            UserBL userBL = new UserBL();
            EmployeesList objEmployeesList = new EmployeesList();
            TravelDeskList objTravelDeskList = new TravelDeskList();
            ExpenseBlockedDatesList exdataesList = new ExpenseBlockedDatesList();
            #endregion

            exdataesList = userBL.ExpenseBlockedDatesList();
            objEmployeesList = userBL.GetEmployees();
            objTravelDeskList = userBL.GetTravelList();

            var objEmployees = objEmployeesList.Where(s => s.Status == true).ToList();
            var objTravelList = objTravelDeskList.Where(s => s.Purpose == "TrainingExpenditure").ToList();

            #region Assign Values to properties
            int CurrentMonth = DateTime.UtcNow.AddHours(5.5).Month;
            int Currentyear = DateTime.UtcNow.AddHours(5.5).Year;
            int CurrentDay = DateTime.UtcNow.AddHours(5.5).Day;
            DateTime PreviousMonthYear = DateTime.UtcNow.AddHours(5.5).AddMonths(-1);
            int PreviourMonth = PreviousMonthYear.Month;
            int PreviousYear = PreviousMonthYear.Year;
            string StartDate;
            string EndDate;
            object resCount = new object();
            int NoofDaysInCurrentMonth = DateTime.DaysInMonth(DateTime.UtcNow.AddHours(5.5).Year, DateTime.UtcNow.AddHours(5.5).Month);
            int NoofDaysInPreviousMonth = DateTime.DaysInMonth(PreviousYear, PreviourMonth);
            #endregion

            for (int i = 0; i < objTravelList.Count; i++)
            {
                DateTime TravelTodate = Convert.ToDateTime(objTravelList[i].TraveledToDate);

                int TravelDay = TravelTodate.Day;

                if (TravelDay >= 1 && TravelDay <= 10)
                {
                    if (DateTime.UtcNow.AddHours(5.5).Day == 23)
                    {
                        BlockTrainingExpenditureDates(Convert.ToDateTime(objTravelList[i].TraveledFromDate).ToShortDateString(), Convert.ToDateTime(objTravelList[i].TraveledToDate).ToShortDateString(), objTravelList[i].FK_EmployeeId);
                    }
                }
                else if (TravelDay >= 11 && TravelDay <= 20)
                {
                    if (DateTime.UtcNow.AddHours(5.5).Day == 03)
                    {
                        BlockTrainingExpenditureDates(Convert.ToDateTime(objTravelList[i].TraveledFromDate).ToShortDateString(), Convert.ToDateTime(objTravelList[i].TraveledToDate).ToShortDateString(), objTravelList[i].FK_EmployeeId);
                    }
                }
                else
                {
                    if (DateTime.UtcNow.AddHours(5.5).Day == 13)
                    {
                        BlockTrainingExpenditureDates(Convert.ToDateTime(objTravelList[i].TraveledFromDate).ToShortDateString(), Convert.ToDateTime(objTravelList[i].TraveledToDate).ToShortDateString(), objTravelList[i].FK_EmployeeId);
                    }
                }

                #region Commented for travel desk blocking

                //DateTime GracePeriodOfTravelDate = TravelTodate.AddDays(12);
                //ExpenseBlockedDates exblock = new ExpenseBlockedDates();
                //if (GracePeriodOfTravelDate <= DateTime.UtcNow.AddHours(5.5))
                //{
                    //StartDate = Convert.ToDateTime(objTravelList[i].TraveledFromDate).ToShortDateString();
                    //EndDate = Convert.ToDateTime(objTravelList[i].TraveledToDate).ToShortDateString();

                    //var travelEmployeeDetails = (from ts in objEmployees where ts.Id == objTravelList[i].FK_EmployeeId select ts).ToList();

                    //exblock.BlockingFromDate = Convert.ToDateTime(StartDate);
                    //exblock.BlockingToDate = Convert.ToDateTime(EndDate);
                    //exblock.Status = "Blocked";
                    //exblock.EmpId = travelEmployeeDetails[0].Id;
                    //exblock.Name = travelEmployeeDetails[0].FirstName;
                    //exblock.EmpMailId = travelEmployeeDetails[0].CompanyMailID;
                    //exblock.UserId = travelEmployeeDetails[0].Employee_ID;
                    //exblock.BlockingType = "TrainingExpenditure";
                    
                    //var blockedlist = (from s in exdataesList
                    //                   where s.UserId == exblock.UserId && s.BlockingFromDate == exblock.BlockingFromDate && s.BlockingToDate ==
                    //                       exblock.BlockingToDate
                    //                   select s).ToList();

                    //if (blockedlist.Count == 0)
                    //{
                    //    int? errcnt = userBL.BlockDates(exblock);
                    //}

                //}

                #endregion
            }

            #region BlockedAfterTwoDaysFunctionality

            for (int i = 0; i < exdataesList.Count; i++)
            {
                if (exdataesList[i].ModifiedBy != null && exdataesList[i].ModifiedDate != null)
                {
                    DateTime ModifiedDate = Convert.ToDateTime(exdataesList[i].ModifiedDate);
                    DateTime AddedModifiedDate = ModifiedDate.AddDays(2);

                    if (AddedModifiedDate.ToShortDateString() == DateTime.UtcNow.AddHours(5.5).ToShortDateString())
                    {
                        ExpenseBlockedDates exdates = new ExpenseBlockedDates();
                        exdates.Status = "Blocked";
                        exdates.Id = exdataesList[i].Id;
                        exdates.Comments = null;
                        int? err = userBL.BlockDates(exdates);
                    }
                }
            }

            #endregion

            for (int i = 0; i < objEmployees.Count; i++)
            {
                if (CurrentDay == 1 || CurrentDay == 2)
                {
                    #region AlertMail for 21 - end of month
                    StartDate = new DateTime(PreviousYear, PreviourMonth, 21).ToShortDateString();
                    EndDate = new DateTime(PreviousYear, PreviourMonth, NoofDaysInPreviousMonth).ToShortDateString();
                    resCount = userBL.GetEmployeeExpenditureCountInSpanDates(objEmployees[i].Id, StartDate, EndDate);

                    if (Convert.ToInt32(resCount) != 10)
                    {
                        AlertMailForSpanOfExpenditureSubmit(objEmployees[i].Id);
                    }
                    #endregion
                }
                else if (CurrentDay == 11 || CurrentDay == 12)
                {
                    #region AlertMail For 1 - 10 Days
                    StartDate = new DateTime(Currentyear, CurrentMonth, 01).ToShortDateString();
                    EndDate = new DateTime(Currentyear, CurrentMonth, 10).ToShortDateString();
                    resCount = userBL.GetEmployeeExpenditureCountInSpanDates(objEmployees[i].Id, StartDate, EndDate);

                    if (Convert.ToInt32(resCount) != 10)
                    {
                        AlertMailForSpanOfExpenditureSubmit(objEmployees[i].Id);
                    }
                    #endregion
                }
                else if (CurrentDay == 21 || CurrentDay == 22)
                {
                    #region Alert Mail For 11 - 20 Days
                    StartDate = new DateTime(Currentyear, CurrentMonth, 11).ToShortDateString();
                    EndDate = new DateTime(Currentyear, CurrentMonth, 20).ToShortDateString();
                    resCount = userBL.GetEmployeeExpenditureCountInSpanDates(objEmployees[i].Id, StartDate, EndDate);

                    if (Convert.ToInt32(resCount) != 10)
                    {
                        AlertMailForSpanOfExpenditureSubmit(objEmployees[i].Id);
                    }
                    #endregion
                }
                else if (CurrentDay == 3 || CurrentDay == 13 || CurrentDay == 23)
                {
                    #region ExpenseBlockedFunctionality
                    ExpenseBlockedDates exblock = new ExpenseBlockedDates();

                    if (CurrentDay == 3)
                    {
                        StartDate = new DateTime(PreviousYear, PreviourMonth, 21).ToShortDateString();
                        EndDate = new DateTime(PreviousYear, PreviourMonth, NoofDaysInPreviousMonth).ToShortDateString();
                    }
                    else if (CurrentDay == 13)
                    {
                        StartDate = new DateTime(Currentyear, CurrentMonth, 01).ToShortDateString();
                        EndDate = new DateTime(Currentyear, CurrentMonth, 10).ToShortDateString();
                    }
                    else
                    {
                        StartDate = new DateTime(Currentyear, CurrentMonth, 11).ToShortDateString();
                        EndDate = new DateTime(Currentyear, CurrentMonth, 20).ToShortDateString();
                    }

                    exblock.BlockingFromDate = Convert.ToDateTime(StartDate);
                    exblock.BlockingToDate = Convert.ToDateTime(EndDate);
                    exblock.Status = "Blocked";
                    exblock.EmpId = objEmployees[i].Id;
                    exblock.Name = objEmployees[i].FirstName;
                    exblock.EmpMailId = objEmployees[i].CompanyMailID;
                    exblock.UserId = objEmployees[i].Employee_ID;
                    exblock.BlockingType = "Expenditure";
                    var blockedlist = (from s in exdataesList
                                       where s.UserId == exblock.UserId && s.BlockingFromDate == exblock.BlockingFromDate && s.BlockingToDate ==
                                           exblock.BlockingToDate
                                       select s).ToList();

                    resCount = userBL.GetEmployeeExpenditureCountInSpanDates(objEmployees[i].Id, StartDate, EndDate);

                    if (blockedlist.Count == 0 && Convert.ToInt32(resCount) != 10)
                    {
                        int? errcnt = userBL.BlockDates(exblock);
                    }

                    #endregion
                }
            }

            #region old Blocked Dates

            //UserExpensesHeaderList userExpenditureHeaderList = new UserExpensesHeaderList();
            //userExpenditureHeaderList = userBL.GetAllEmployeesSubmittedExpenditureRecords();
            //var objUserExpenseHeaderList1 = userExpenditureHeaderList.Select(s => s.FromDate).Distinct().Select(c => userExpenditureHeaderList.FirstOrDefault(r => r.FromDate == c)).ToList();
            //var expense = (from d in userExpenditureHeaderList group d by new { d.EmpUserID, d.FromDate } into my select my.First()).ToList();
            ////expense = (from a in expense.Where(x => x.FromDate == expense.Max(a => a.FromDate)) select a).ToList();
            ////expense = (from d in userExpenditureHeaderList group d by new {d.EmpUserID, d.FromDate} into my select my.First()).ToList();
            //expense = (from d in expense where d.FromDate == expense.Where(t => t.EmpUserID == d.EmpUserID).Max(r => r.FromDate) select d).ToList();
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-IN");
            //for (int i = 0; i < expense.Count; i++)
            //{
            //    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-IN");
            //    string nfomrat = (String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(expense[i].FromDate)));
            //    DateTime newdate = Convert.ToDateTime(nfomrat);
            //    string dfomrat = (String.Format("{0:dd/MM/yyyy}", DateTime.Now));
            //    DateTime dt = Convert.ToDateTime(dfomrat);
            //    DateTime dayAddedDate = newdate.AddDays(1);
            //    TimeSpan ts = dt.Subtract(newdate);
            //    int? differenceInDays = ts.Days;
            //    if (differenceInDays >= 7)
            //    {
            //        Emails mail = new Emails();
            //        StringBuilder sb = new StringBuilder();
            //        ExpenseBlockedDates exblock = new ExpenseBlockedDates();
            //        exblock.BlockingFromDate = dayAddedDate;
            //        exblock.BlockingToDate = dt;
            //        exblock.Status = "Blocked";
            //        exblock.EmpId = expense[i].empID;
            //        exblock.Name = expense[i].EmployeeName;
            //        exblock.EmpMailId = expense[i].EmployeeMailID;
            //        exblock.ManagerMailId = expense[i].ManagerEmailID;
            //        exblock.ACMailId = expense[i].AccountManagerEmailID;
            //        exblock.UserId = expense[i].EmpUserID;
            //        if (differenceInDays == 7 || differenceInDays == 8 || differenceInDays == 9)
            //        {
            //            if (differenceInDays == 7)
            //            {
            //                sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>Universal Surgicals</title></head><body style=\"background-color:#e7e8ea;\"> <div style=\"max-width:800px; width:100%; margin:0px auto;\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr bgcolor=\"#333645\" style=\"padding: 30px;\"><td height=\"150px;\" style=\"padding-left:30px;\"> <img src=\"http://universal.globaldatasolutions.com/Content/Images/login-logo.png\"></td></tr><tr bgcolor=\"#FFFFFF\"><td  style=\"font-family:'Open Sans';padding:40px;\"><div style=\"width:100%; font-size:18px; font-weight: bold; font-weight:800px; color:#3dbeba;\">  Dear " + exblock.Name + "(" + exblock.UserId + ") ,</div><br/>");
            //                sb.Append("<div style=\"border-radius: 5px; border:  solid 1px #e7e8ea; padding:20px;  font-size:14px;  font-weight: bold; color:#868686; margin-top:20px;\"><table width=\"100%\"><tr><td style=\"font-size:13px;  font-weight: normal; color:#868686;\"> You have not submitted your expenses from " + exblock.BlockingFromDate.ToString("dd/MM/yyyy") + " to " + exblock.BlockingToDate.ToString("dd/MM/yyyy") + " , please submit your expenses now to avoid any consequences. You have 3 days left to submit all your pending expenditures.<br/><br/>Please contact your manager or account manager for any clarification.<br/><br/></td> </tr></table></div>");
            //                sb.Append("<div style=\"color:#868686; font-size:13px; margin-top:15px; color:#3dbeba; float: right;  font-weight: 700; text-align: center;\"><b>Regards,</b><br/><b>Expense Management Support !</b><br/><b>Universal Surgicals </b></div>");
            //                sb.Append("</td></tr></table></div></body></html>");
            //            }
            //            else if (differenceInDays == 8)
            //            {
            //                sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>Universal Surgicals</title></head><body style=\"background-color:#e7e8ea;\"> <div style=\"max-width:800px; width:100%; margin:0px auto;\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr bgcolor=\"#333645\" style=\"padding: 30px;\"><td height=\"150px;\" style=\"padding-left:30px;\"> <img src=\"http://universal.globaldatasolutions.com/Content/Images/login-logo.png\"></td></tr><tr bgcolor=\"#FFFFFF\"><td  style=\"font-family:'Open Sans';padding:40px;\"><div style=\"width:100%; font-size:18px; font-weight: bold; font-weight:800px; color:#3dbeba;\">  Dear " + exblock.Name + "(" + exblock.UserId + ") ,</div><br/>");
            //                sb.Append("<div style=\"border-radius: 5px; border:  solid 1px #e7e8ea; padding:20px;  font-size:14px;  font-weight: bold; color:#868686; margin-top:20px;\"><table width=\"100%\"><tr><td style=\"font-size:13px;  font-weight: normal; color:#868686;\"> You have not submitted your expenses from " + exblock.BlockingFromDate.ToString("dd/MM/yyyy") + " to " + exblock.BlockingToDate.ToString("dd/MM/yyyy") + " , please submit your expenses now to avoid any consequences. You have 2 days left to submit all your pending expenditures.<br/><br/>Please contact your manager or account manager for any clarification.<br/><br/></td> </tr></table></div>");
            //                sb.Append("<div style=\"color:#868686; font-size:13px; margin-top:15px; color:#3dbeba; float: right;  font-weight: 700; text-align: center;\"><b>Regards,</b><br/><b>Expense Management Support !</b><br/><b>Universal Surgicals </b></div>");
            //                sb.Append("</td></tr></table></div></body></html>");
            //            }
            //            else if (differenceInDays == 9)
            //            {
            //                sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>Universal Surgicals</title></head><body style=\"background-color:#e7e8ea;\"> <div style=\"max-width:800px; width:100%; margin:0px auto;\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr bgcolor=\"#333645\" style=\"padding: 30px;\"><td height=\"150px;\" style=\"padding-left:30px;\"> <img src=\"http://universal.globaldatasolutions.com/Content/Images/login-logo.png\"></td></tr><tr bgcolor=\"#FFFFFF\"><td  style=\"font-family:'Open Sans';padding:40px;\"><div style=\"width:100%; font-size:18px; font-weight: bold; font-weight:800px; color:#3dbeba;\">  Dear " + exblock.Name + "(" + exblock.UserId + ") ,</div><br/>");
            //                sb.Append("<div style=\"border-radius: 5px; border:  solid 1px #e7e8ea; padding:20px;  font-size:14px;  font-weight: bold; color:#868686; margin-top:20px;\"><table width=\"100%\"><tr><td style=\"font-size:13px;  font-weight: normal; color:#868686;\"> You have not submitted your expenses from " + exblock.BlockingFromDate.ToString("dd/MM/yyyy") + " to " + exblock.BlockingToDate.ToString("dd/MM/yyyy") + " , please submit your expenses now to avoid any consequences. You have 1 days left to submit all your pending expenditures.<br/><br/>Please contact your manager or account manager for any clarification.<br/><br/></td> </tr></table></div>");
            //                sb.Append("<div style=\"color:#868686; font-size:13px; margin-top:15px; color:#3dbeba; float: right;  font-weight: 700; text-align: center;\"><b>Regards,</b><br/><b>Expense Management Support !</b><br/><b>Universal Surgicals </b></div>");
            //                sb.Append("</td></tr></table></div></body></html>");
            //            }

            //            if (exblock.ACMailId != null && !(string.IsNullOrEmpty(exblock.ACMailId)))
            //            {
            //                mail.GDSSendBulkeMail("support@gdatasol.com", exblock.EmpMailId, "", exblock.ACMailId, sb.ToString(), " Blocked expenses access for " + exblock.BlockingFromDate.ToString("dd/MM/yyyy") + " to " + exblock.BlockingToDate.ToString("dd/MM/yyyy") + "  ", "", "Iwjil");
            //            }
            //            else
            //            {
            //                mail.GDSSendBulkeMail("support@gdatasol.com", exblock.EmpMailId, "", exblock.EmpMailId, sb.ToString(), " Blocked expenses access for " + exblock.BlockingFromDate.ToString("dd/MM/yyyy") + " to " + exblock.BlockingToDate.ToString("dd/MM/yyyy") + "  ", "", "Iwjil");
            //            }
            //        }
            //        else if (differenceInDays == 10 && expense[i].BlockedStatus != "Blocked")
            //        {
            //            ExpenseBlockedDatesList exdataesList = new ExpenseBlockedDatesList();
            //            exdataesList = userBL.ExpenseBlockedDatesList();

            //            var blockedlist = (from s in exdataesList where s.UserId == exblock.UserId && s.BlockingFromDate == exblock.BlockingFromDate && s.BlockingToDate == exblock.BlockingToDate  select s).ToList();

            //            if (blockedlist.Count == 0)
            //            {
            //                int? errcnt = userBL.BlockDates(exblock);
            //                StringBuilder Spend = new StringBuilder();
            //                Spend.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>Universal Surgicals</title></head><body style=\"background-color:#e7e8ea;\"> <div style=\"max-width:800px; width:100%; margin:0px auto;\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr bgcolor=\"#333645\" style=\"padding: 30px;\"><td height=\"150px;\" style=\"padding-left:30px;\"> <img src=\"http://universal.globaldatasolutions.com/Content/Images/login-logo.png\"></td></tr><tr bgcolor=\"#FFFFFF\"><td  style=\"font-family:'Open Sans';padding:40px;\"><div style=\"width:100%; font-size:18px; font-weight: bold; font-weight:800px; color:#3dbeba;\">  Dear " + exblock.Name + "(" + exblock.UserId + ") ,</div><br/>");
            //                Spend.Append("<div style=\"border-radius: 5px; border:  solid 1px #e7e8ea; padding:20px;  font-size:14px;  font-weight: bold; color:#868686; margin-top:20px;\"><table width=\"100%\"><tr><td style=\"font-size:13px;  font-weight: normal; color:#868686;\"> Your expense submission access has been blocked from " + exblock.BlockingFromDate.ToString("dd/MM/yyyy") + " to " + exblock.BlockingToDate.ToString("dd/MM/yyyy") + " period due to <b>No submission</b> of expenses continuously for 10 days. <br/><br/>If you have valid reason for delay then please contact your account manager to enable your access to submit expenses of this period.</td> </tr></table></div>");
            //                Spend.Append("<div style=\"color:#868686; font-size:13px; margin-top:15px; color:#3dbeba; float: right;  font-weight: 700; text-align: center;\"><b>Regards,</b><br/><b>Expense Management Support !</b><br/><b>Universal Surgicals </b></div>");
            //                Spend.Append("</td></tr></table></div></body></html>");

            //                if (exblock.ACMailId != null && !(string.IsNullOrEmpty(exblock.ACMailId)))
            //                {
            //                    mail.GDSSendBulkeMail("support@gdatasol.com", exblock.EmpMailId, "", exblock.ACMailId, Spend.ToString(), " Blocked expenses access for " + exblock.BlockingFromDate.ToString("dd/MM/yyyy") + " to " + exblock.BlockingToDate.ToString("dd/MM/yyyy") + "  ", "", "Iwjil");
            //                }
            //                else
            //                {
            //                    mail.GDSSendBulkeMail("support@gdatasol.com", exblock.EmpMailId, "", exblock.EmpMailId, Spend.ToString(), " Blocked expenses access for " + exblock.BlockingFromDate.ToString("dd/MM/yyyy") + " to " + exblock.BlockingToDate.ToString("dd/MM/yyyy") + "  ", "", "Iwjil");
            //                }
            //            }
                        
            //        }
            //    }
            //}

            #endregion

            
        }

        private void BlockTrainingExpenditureDates(string FromDate, string ToDate, int? EmplID)
        {
            #region object creations for entities
            UserBL userBL = new UserBL();
            EmployeesList objEmployeesList = new EmployeesList();
            TravelDeskList objTravelDeskList = new TravelDeskList();
            ExpenseBlockedDatesList exdataesList = new ExpenseBlockedDatesList();
            ExpenseBlockedDates exblock = new ExpenseBlockedDates();

            exdataesList = userBL.ExpenseBlockedDatesList();
            objEmployeesList = userBL.GetEmployees();
            objTravelDeskList = userBL.GetTravelList();
            
            string StartDate;
            string EndDate;
            
            var objEmployees = objEmployeesList.Where(s => s.Status == true).ToList();
            var objTravelList = objTravelDeskList.Where(s => s.Purpose == "TrainingExpenditure").ToList();
            #endregion


            StartDate = Convert.ToDateTime(FromDate).ToShortDateString();
            EndDate = Convert.ToDateTime(ToDate).ToShortDateString();

            var travelEmployeeDetails = (from ts in objEmployees where ts.Id == EmplID select ts).ToList();

            exblock.BlockingFromDate = Convert.ToDateTime(StartDate);
            exblock.BlockingToDate = Convert.ToDateTime(EndDate);
            exblock.Status = "Blocked";
            exblock.EmpId = travelEmployeeDetails[0].Id;
            exblock.Name = travelEmployeeDetails[0].FirstName;
            exblock.EmpMailId = travelEmployeeDetails[0].CompanyMailID;
            exblock.UserId = travelEmployeeDetails[0].Employee_ID;
            exblock.BlockingType = "TrainingExpenditure";

            var blockedlist = (from s in exdataesList
                               where s.UserId == exblock.UserId && s.BlockingFromDate == exblock.BlockingFromDate && s.BlockingToDate ==
                                   exblock.BlockingToDate
                               select s).ToList();

            if (blockedlist.Count == 0)
            {
                int? errcnt = userBL.BlockDates(exblock);
            }

        }

        private void AlertMailForSpanOfExpenditureSubmit(int? EmpID)
        {
            Employees objEmployeeDetails = new Employees();
            UserBL objUserbl = new UserBL();
            objEmployeeDetails = objUserbl.GetEmployeeByID(null, EmpID);

            
            Emails eMails = new Emails();
            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>Universal Surgicals</title></head><body style=\"background-color:#e7e8ea;\"> <div style=\"max-width:800px; width:100%; margin:0px auto;\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr bgcolor=\"#333645\" style=\"padding: 30px;\"><td height=\"150px;\" style=\"padding-left:30px;\"> <img src=\"http://universal.globaldatasolutions.com/Content/Images/login-logo.png\"></td></tr><tr bgcolor=\"#FFFFFF\"><td  style=\"font-family:'Open Sans';padding:40px;\"><div style=\"width:100%; font-size:18px; font-weight: bold; font-weight:800px; color:#3dbeba;\">  Dear " + objEmployeeDetails.FirstName + ",</div><br/>");
            sb.Append("<div style=\"border-radius: 5px; border:  solid 1px #e7e8ea; padding:20px;  font-size:14px;  font-weight: bold; color:#868686; margin-top:20px;\"><table width=\"100%\"><tr><td style=\"font-size:13px;  font-weight: normal; color:#868686;\"> Please submit all your pending expenditures for this phase immediately ! <br/><br/> The Universal Surgical's new expenditure submission policy defines that all employees are hereby instructed to submit their expenses within the three cycle of a month, i.e. 1st to 10th, 11th to 20th and 21st to 31st. <br/><br/> According to the expense submission’s policy, you need to submit all your expenses reports as per below schedule; <br/><br/> For all reports falls under; <br/><br/> <b>* 1st to 10th -   Submit before 12th EOD of every month.</b><br/><b>* 11th to 20th -      Submit before 22nd EOD of every month.</b><br/><b> * 21st to 30th (Month End)-      Submit before 2nd EOD of adjacent month.</b><br/><br/>If you fail to submit your expenses on or before schedule, the particular period phase will be block and you would not be able to submit the missed dates report until and unless your account manager allows you the grace access.<br/><br/>For more details contact your account manager.<br/><br/>Please ignore this email if you have already submitted your expenditures reports.<br/></td> </tr></table></div>");
            sb.Append("<div style=\"color:#868686; font-size:13px; margin-top:15px; color:#3dbeba; float: right;  font-weight: 700; text-align: center;\"><b>Regards,</b><br/><b>Expense Management Support !</b><br/><b>Universal Surgicals </b></div>");
            sb.Append("</td></tr></table></div></body></html>");

            eMails.GDSSendBulkeMail(System.Configuration.ConfigurationManager.AppSettings["webmail"].ToString(), objEmployeeDetails.CompanyMailID, "", "", sb.ToString(), " Attention ! Submit all your pending expenditures immediately.", "", "Iwjil");

        }

    }
}