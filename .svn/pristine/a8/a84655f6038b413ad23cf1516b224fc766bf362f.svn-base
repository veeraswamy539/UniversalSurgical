using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace UniversalSurgicals
{
    public class LogFiles
    {
        private string SLogFormat;
        private string SErrorTime;


        public void CreateLogFiles()
        {
            //sLogFormat used to create log files format :
            // dd/mm/yyyy hh:mm:ss AM/PM ==> Log Message
            SLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> " ;
            //this variable used to create log filename format "
            //for example filename : ErrorLogYYYYMMDD
            string sYear = DateTime.Now.Year.ToString();
            string sMonth = DateTime.Now.Month.ToString();
            string sDay = DateTime.Now.Day.ToString();
            SErrorTime = sYear + sMonth + sDay;
        }
        public void ErrorLog(string sPathName, string mail, string sErrMsg)
        {
            //string LogPath =  System.Configuration.ConfigurationManager.AppSettings["LogPath"].ToString();

            string subPath = "/Content/Logs/" + mail; // your code goes here

            bool exists = System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(subPath));

            string pathmodified = HttpContext.Current.Server.MapPath(subPath + "/");
            if (!exists)

                System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(subPath));

            string filename = "Log_" + DateTime.Now.ToString("dd-MM-yyyy") + mail + ".txt";
            string filepath = pathmodified + filename;

            if (File.Exists(filepath))
            {
                StreamWriter sw = new StreamWriter(filepath, true);

                sw.WriteLine(DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> " + sErrMsg);
                sw.Flush();
                sw.Close();
            }
            else
            {
                //StreamWriter sw = new StreamWriter(sPathName + sErrorTime, true);

                StreamWriter sw = File.CreateText(filepath);
                sw.WriteLine(DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> " + sErrMsg);
                sw.Flush();
                sw.Close();
            }
            
           

           
        }

    }
}