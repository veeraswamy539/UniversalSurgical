using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Universal.BusinessEntities;
using Universal.BusinessLogicLayer;
using Quartz;
using System.IO;
using System.Net;
using System.Threading;
using System.Globalization;
using System.Text;

namespace UniversalSurgicals
{
    public class BirthDayReminderJob: IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-IN");
            #region BirthDay Mail Functionality
            UserBL userBL = new UserBL();
            EmployeesList emps = new EmployeesList();
            //string datefomrat = (String.Format("{0:dd/MM/yyyy}", DateTime.Now));
            DateTime dtToday = DateTime.UtcNow.AddHours(5.3);

            emps = userBL.GetEmployeesByDOB(dtToday.Day, dtToday.Month);
            //HttpContext.Current.Session["Day"] = dtToday.Day;
            //HttpContext.Current.Session["Month"] = dtToday.Month;
            if (emps.Count > 0)
            {
                Emails mail = new Emails();

                foreach (var i in emps)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>Universal Surgicals</title></head><body style=\";\"> <div style=\"max-width:800px; width:100%; margin:0px auto;\"><table width=\"100%\" border=\"1px solid #ccc\" cellspacing=\"0\" cellpadding=\"0\"><tr bgcolor=\"\" style=\"padding: 30px;\"><td height=\"10px;\" style=\"padding-left:30px;\"> <img src=\"http://universal.globaldatasolutions.com/Content/Images/login-logo.png\"></td></tr><tr bgcolor=\"\" style=\"background-image: url('http://universal.globaldatasolutions.com/Content/Images/BirthDayBackground.jpg');background-repeat: repeat;\"  ><td  style=\"font-family:'Open Sans';padding:40px;\"><div style=\"width:100%; font-size:18px; font-weight: bold; font-weight:800px; color:#3dbeba;\">  Dear " + i.FirstName + " ,</div><br/>");
                    sb.Append("<div style=\" padding:20px;  font-size:14px;  font-weight: bold; color:#868686; margin-top:20px;\"><table width=\"100%\"><tr><td style=\"font-size:16px;  font-weight: bold; color:#FFCF4C;\">Wish you a very happy birthday from the Universal Surgicals team. We hope that your next year will be a year of doing what you want, and getting what you want..</td> </tr></table></div>");
                    sb.Append("<div style=\"color:#868686; font-size:13px; margin-top:15px; color:#3dbeba; float: right;  font-weight: 700; text-align: center;\"><b>Regards,</b><br/><b>Expense Management Support !</b><br/><b>Universal Surgicals </b></div>");
                    sb.Append("</td></tr></table></div></body></html>");
                    if (i.AccountManagerEmailID != null && i.ManagerEmailID != null)
                    {
                        string cc = i.ManagerEmailID + "," + i.AccountManagerEmailID;
                        mail.GDSSendBulkeMail("admin@universalsurgicals.in", i.CompanyMailID, "", cc, sb.ToString(), " Happy Birthday,  " + i.FirstName + "!  ", "", "Iwjil");
                    }
                    else if (i.AccountManagerEmailID == null)
                    {
                        mail.GDSSendBulkeMail("admin@universalsurgicals.in", i.CompanyMailID, "", "", sb.ToString(), " Happy Birthday,  " + i.FirstName + "!  ", "", "Iwjil");
                    }

                    else
                        mail.GDSSendBulkeMail("admin@universalsurgicals.in", i.CompanyMailID, "", i.AccountManagerEmailID, sb.ToString(), " Happy Birthday,  " + i.FirstName + "!  ", "", "Iwjil");

                }
            }

            #endregion
        }
    }
}