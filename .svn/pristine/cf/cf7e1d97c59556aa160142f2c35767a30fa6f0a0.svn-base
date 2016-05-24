using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Universal.BusinessEntities
{
    public class Jobs_AppliedCandidates : Core.BusinessBase<Jobs_AppliedCandidates>
    {
        [Core.DataBaseFieldMapper("Id", typeof(int?))]
        public int? Id { get; set; }

        [Core.DataBaseFieldMapper("JobId", typeof(int?))]
        public int? JobId { get; set; }

        [Core.DataBaseFieldMapper("Name", typeof(string))]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z_]*$", ErrorMessage = "Please Enter Valid  Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Please enter minimum of 2 characters and maximum of 50 characters"), Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }

        [Core.DataBaseFieldMapper("EmailId", typeof(string))]
        [StringLength(50, MinimumLength = 8)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})\s*", ErrorMessage = "E-mail is not valid")]
        public string EmailId { get; set; }

        [Core.DataBaseFieldMapper("Phone", typeof(string))]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Please Enter 10 Digits"), Required(ErrorMessage = "Please Enter Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Core.DataBaseFieldMapper("Address", typeof(string))]
        [Required(ErrorMessage = "Please Enter Address")]
        public string Address { get; set; }

        [Core.DataBaseFieldMapper("Availability", typeof(string))]
        //[Required(ErrorMessage = "Please Select Availability")]
        public string Availability { get; set; }

        [Core.DataBaseFieldMapper("PastExperience", typeof(string))]
        [StringLength(50, MinimumLength = 1)]
        [Required(ErrorMessage = "Please Select Availability")]
        public string PastExperience { get; set; }

        [Core.DataBaseFieldMapper("TrainingRequired", typeof(bool))]
        [Required(ErrorMessage = "Please Select Training Required")]
        public bool TrainingRequired { get; set; }

        [Core.DataBaseFieldMapper("ResumeFileName", typeof(string))]
        [Required(ErrorMessage = "Please Upload Resume")]
        public string ResumeFileName { get; set; }

        [Core.DataBaseFieldMapper("CreatedDate", typeof(DateTime))]
        [DataType(DataType.DateTime), Required]
        public DateTime? CreatedDate { get; set; }

        [Core.DataBaseFieldMapper("ModifiedDate", typeof(DateTime?))]
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }

    }
}
