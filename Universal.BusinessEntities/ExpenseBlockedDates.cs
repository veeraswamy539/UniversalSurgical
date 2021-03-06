﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Universal.BusinessEntities
{
    public class ExpenseBlockedDates : Core.BusinessBase<ExpenseBlockedDates>
    {
        [Core.DataBaseFieldMapper("Id", typeof(int?))]
        public int? Id { get; set; }

        [Core.DataBaseFieldMapper("EmpId", typeof(int?))]
        public int? EmpId { get; set; }

        [Core.DataBaseFieldMapper("BlockingFromDate", typeof(DateTime))]
        public DateTime BlockingFromDate { get; set; }

        [Core.DataBaseFieldMapper("BlockingToDate", typeof(DateTime))]
        public DateTime BlockingToDate { get; set; }

        [Core.DataBaseFieldMapper("Status", typeof(string))]
        public string Status { get; set; }

        [Core.DataBaseFieldMapper("CreatedBy", typeof(string))]
        public string CreatedBy { get; set; }

        [Core.DataBaseFieldMapper("CreatedDate", typeof(DateTime?))]
        public DateTime? CreatedDate { get; set; }

        [Core.DataBaseFieldMapper("ModifiedBy", typeof(string))]
        public string ModifiedBy { get; set; }

        [Core.DataBaseFieldMapper("ModifiedDate", typeof(DateTime?))]
        public DateTime? ModifiedDate { get; set; }

        [Core.DataBaseFieldMapper("Comments", typeof(string))]
        [Required (ErrorMessage="Please enter Comment")]
        public string Comments { get; set; }

        [Core.DataBaseFieldMapper("BlockingType", typeof(string))]
        public string BlockingType { get; set; }

        [Core.DataBaseFieldMapper("Name", typeof(string))]
        public string Name { get; set; }

        [Core.DataBaseFieldMapper("UserId", typeof(string))]
        public string UserId { get; set; }

        [Core.DataBaseFieldMapper("EmpMailId", typeof(string))]
        public string EmpMailId { get; set; }

        [Core.DataBaseFieldMapper("ManagerMailId", typeof(string))]
        public string ManagerMailId { get; set; }

        [Core.DataBaseFieldMapper("ACMailId", typeof(string))]
        public string ACMailId { get; set; }
    }
}
