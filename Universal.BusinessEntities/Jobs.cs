using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Universal.BusinessEntities
{
    public class Jobs : Core.BusinessBase<Jobs>
    {
        public Jobs_AppliedCandidatesCollection jobs_AppliedCandidatesCollection { get; set; }
        [Core.DataBaseFieldMapper("Id", typeof(int?))]
        public int? Id { get; set; }

        [Core.DataBaseFieldMapper("JobTitle", typeof(string))]
        [StringLength(150, ErrorMessage = "Please enter maximum 150 characters only")]
        [Required(ErrorMessage = "Please Enter Job Title")]
        public string JobTitle { get; set; }

        [Core.DataBaseFieldMapper("JobDescription", typeof(string))]
        [StringLength(400, ErrorMessage = "Please enter maximum 400 characters only")]
        [Required(ErrorMessage = "Please Enter Job Description")]
        public string JobDescription { get; set; }

        [Core.DataBaseFieldMapper("JobDetails", typeof(string))]
        [StringLength(300, ErrorMessage = "Please enter maximum 300 characters only")]
        [Required(ErrorMessage = "Please Enter Job Details")]
        public string JobDetails { get; set; }

        [Core.DataBaseFieldMapper("MinExperience", typeof(int?))]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please Enter numbers only")]
        [Required(ErrorMessage = "Please Enter Minimum Experience")]
        public int? MinExperience { get; set; }

        [Core.DataBaseFieldMapper("MaxExperience", typeof(int?))]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please Enter numbers only")]
        public int? MaxExperience { get; set; }

        [Core.DataBaseFieldMapper("JobLocation", typeof(string))]
        [StringLength(50, ErrorMessage = "Please enter maximum 50 characters only")]
        [Required(ErrorMessage = "Please Enter Job Location")]
        public string JobLocation { get; set; }

        [Core.DataBaseFieldMapper("Industry", typeof(string))]
        [StringLength(50, ErrorMessage = "Please enter maximum 50 characters only")]
        [Required(ErrorMessage = "Please Enter Industry")]
        public string Industry { get; set; }

        [Core.DataBaseFieldMapper("Role", typeof(string))]
        [StringLength(50, ErrorMessage = "Please enter maximum 50 characters only")]
        [Required(ErrorMessage = "Please Enter Role")]
        public string Role { get; set; }

        [Core.DataBaseFieldMapper("Education", typeof(string))]
        [StringLength(50, ErrorMessage = "Please enter maximum 50 characters only")]
        [Required(ErrorMessage = "Please Enter Education")]
        public string Education { get; set; }

        [Core.DataBaseFieldMapper("Specialization", typeof(string))]
        [StringLength(50, ErrorMessage = "Please enter maximum 50 characters only")]
        [Required(ErrorMessage = "Please Enter Specialization")]
        public string Specialization { get; set; }

        [Core.DataBaseFieldMapper("ContactName", typeof(string))]
        [RegularExpression(@"^[a-zA-Z]+[ .a-zA-Z_]*$", ErrorMessage = "Please Enter Valid Name")]
        [StringLength(100, ErrorMessage = "Please enter maximum 100 characters only")]
        [Required(ErrorMessage = "Please Enter ContactName")]
        public string ContactName { get; set; }

        [Core.DataBaseFieldMapper("ContactEmail", typeof(string))]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})\s*", ErrorMessage = "E-mail is not valid")]
        [StringLength(50, ErrorMessage = "Please enter maximum 50 characters only")]
        [Required(ErrorMessage = "Please Enter Contact Email")]
        public string ContactEmail { get; set; }

        [Core.DataBaseFieldMapper("ContactPhoneNumber", typeof(string))]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Please Enter 10 Digits"), Required(ErrorMessage = "Please Enter Contact Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string ContactPhoneNumber { get; set; }

        [Core.DataBaseFieldMapper("IsActive", typeof(bool))]
        [Required(ErrorMessage = "Please Select Status")]
        public bool IsActive { get; set; }

        [Core.DataBaseFieldMapper("CreatedDate", typeof(DateTime))]
        public DateTime CreatedDate { get; set; }

        [Core.DataBaseFieldMapper("ModifiedDate", typeof(DateTime))]
        public DateTime ModifiedDate { get; set; }

        [Core.DataBaseFieldMapper("CandidatesCount", typeof(DateTime))]
        public int Count { get; set; }

        //[Core.DataBaseFieldMapper("Name", typeof(string))]
        //[RegularExpression(@"^[a-zA-Z]+[ a-zA-Z_]*$", ErrorMessage = "Please Enter Valid  Name")]
        //[StringLength(50, MinimumLength = 2, ErrorMessage = "Please enter minimum of 2 characters and maximum of 50 characters"), Required(ErrorMessage = "Please Enter Name")]
        //public string Name { get; set; }

        //[Core.DataBaseFieldMapper("EmailId", typeof(string))]
        //[StringLength(50, MinimumLength = 8)]
        //[RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})\s*", ErrorMessage = "E-mail is not valid")]
        //public string EmailId { get; set; }

        //[Core.DataBaseFieldMapper("ResumeFileName", typeof(string))]
        //[Required(ErrorMessage = "Please Upload Resume")]
        //public string ResumeFileName { get; set; }
    }
}
