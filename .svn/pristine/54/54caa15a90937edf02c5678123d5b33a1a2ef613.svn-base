using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Universal.BusinessEntities;
using Universal.BusinessLogicLayer;
using System.IO;


namespace UniversalSurgicals.API
{
    /// <summary>
    /// Summary description for UniversalApi
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //[System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class UniversalApi : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod]
        public JobsCollection GetJobs()
        {

            UserBL userBl = new UserBL();
            JobsCollection jobs = userBl.GetJobsList();
            return jobs;
        }

        [WebMethod]
        public EventsCollection GetEvents()
        {

            UserBL userBl = new UserBL();
            EventsCollection events = userBl.GetEventsList();
            return events;
        }

        [WebMethod]
        public Events GetEventById(int? id)
        {

            UserBL userBl = new UserBL();
            Events events = userBl.EventDetailsById(id);
            return events;
        }
        [WebMethod]
        public bool ApplyToJob(Jobs_AppliedCandidates jobsApplied)
        {
            bool status = false;
            UserBL userBl = new UserBL();
            Jobs_AppliedCandidatesCollection jcoll = new Jobs_AppliedCandidatesCollection();
            jcoll = userBl.IsAlreadyApplied(jobsApplied.EmailId.Trim(), jobsApplied.JobId);
            if (jcoll.Count == 0)
            {
               
                int? err = userBl.ApplyToJob(jobsApplied);
                status = true;
            }
            return status;
        }

        [WebMethod]
        public bool UploadResume(int id,string email)
        {
            bool status = false;
            foreach (string file in  HttpContext.Current.Request.Files)
            {

                var fileContent = HttpContext.Current.Request.Files[file];

                if (fileContent != null && fileContent.ContentLength > 0)
                {
                    var stream = fileContent.InputStream;
                    var fileName = email.Remove(email.IndexOf("@"),1) + id;
                    var path = Path.Combine(Server.MapPath("~/Content/Resumes"), fileName);

                    using (var fileStream = System.IO.File.Create(path))
                    {
                        stream.CopyTo(fileStream);
                    }

                }

            }
            return status;
        }
    }
}
