using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;


namespace Universal.BusinessEntities
{


    public class Leave : Core.BusinessBase<Leave>
    {
        [Core.DataBaseFieldMapper("Id", typeof(int?))]
        public int? Id { get; set; }

        [Core.DataBaseFieldMapper("Employee_ID", typeof(int?))]
       
        public int? Employee_ID { get; set; }

        [Core.DataBaseFieldMapper("EmpUserID", typeof(string))]
        public string EmpUserID { get; set; }

        [Core.DataBaseFieldMapper("ManagerUserId", typeof(int?))]
        public int? ManagerUserId { get; set; }


        [Core.DataBaseFieldMapper("LeaveFrom", typeof(DateTime?))]
       // [DataType(DataType.DateTime, ErrorMessage = "Please select a valid date"), Required(ErrorMessage = "Please Enter Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> LeaveFrom { get; set; }

       

        [Core.DataBaseFieldMapper("LeaveTo", typeof(DateTime?))]
      //  [DataType(DataType.DateTime, ErrorMessage = "Please select a valid date"), Required(ErrorMessage = "Please Enter Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> LeaveTo { get; set; }

        [DataType(DataType.Text, ErrorMessage = "Please select a valid date"), Required(ErrorMessage = "Please Enter Date")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string InLeaveFrom { get; set; }

        [DataType(DataType.Text, ErrorMessage = "Please select a valid date"), Required(ErrorMessage = "Please Enter Date")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string InLeaveTo { get; set; }


        [Core.DataBaseFieldMapper("LeaveType", typeof(string))]
        [Required(ErrorMessage = "Please Select Leave Type")]
        public string LeaveType { get; set; }

        [Core.DataBaseFieldMapper("LeaveComment", typeof(string))]
        [StringLength(250,MinimumLength=5 ,ErrorMessage = "Please enter atleast 5 characters"), Required]
        public string LeaveComment { get; set; }


        [Core.DataBaseFieldMapper("CreatedDate", typeof(DateTime?))]
        public DateTime? CreatedDate { get; set; }

        [Core.DataBaseFieldMapper("LeaveStatus", typeof(bool))]
        public bool? LeaveStatus{ get; set; }


        [Core.DataBaseFieldMapper("ZoneId",typeof(int))]
        public int? ZoneId { get; set; }

        [Core.DataBaseFieldMapper("ZoneName", typeof(string))]
        public string ZoneName { get; set; }


        [Core.DataBaseFieldMapper("NoOfDays", typeof(int?))]
        public int? NoofDays { get; set; }

        [Core.DataBaseFieldMapper("EmployeeID", typeof(string))]
        public string EmployeeID { get; set; }


           [Core.DataBaseFieldMapper("EMPFullName", typeof(string))]
        public string EMPFullName { get; set; }


           [Core.DataBaseFieldMapper("Managerid", typeof(int?))]
           public int? Managerid { get; set; }


          [Core.DataBaseFieldMapper("UserGroup", typeof(int?))]
           public int? UserGroup {get; set; }

        [Core.DataBaseFieldMapper("AccountManagerId", typeof(int?))]
          public int? AccountManagerId { get; set;}

           public int? TotalNumberofLeaves { get; set; }

           public int? ApprovedLeaves { get; set; }

           public int? RejectedLeaves { get; set; }

           public int? PendingLeaves { get; set; }


           public int? TotalNumberofLeavesother { get; set; }

           public int? ApprovedLeavesother { get; set; }

           public int? RejectedLeavesother { get; set; }

           public int? PendingLeavesother { get; set; }


           public int? HospitalizedLeave { get; set; }

           public int? CasualLeave { get; set; }

           public int? SickLeave { get; set; }

           public int? FuneralLeave { get; set; }

           public int? HospitalizedLeaveOther { get; set; }

           public int? CasualLeaveOther { get; set; }

           public int? SickLeaveOther { get; set; }

           public int? FuneralLeaveOther { get; set; }
       
    }






}
