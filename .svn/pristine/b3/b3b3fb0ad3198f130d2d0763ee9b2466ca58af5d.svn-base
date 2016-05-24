using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Universal.BusinessEntities
{
    public class UserExpenses_Header : Core.BusinessBase<UserExpenses_Header>
    {

        public UserExpense_Details UserExpenseDetails { get; set; }
        public UserExpenseDetailsList UserExpenseDetailsList { get; set; }


        public ServiceOrderNos objServiceOrderNos { get; set; }
        public ServiceOrderNos_List objServiceOrderNosList { get; set; }


        [Core.DataBaseFieldMapper("Id", typeof(int?))]
        public int Id { get; set; }

        [Core.DataBaseFieldMapper("FK_EmployeeID", typeof(int?))]
        [Required]
        public int? FK_EmployeeId { get; set; }


        [Core.DataBaseFieldMapper("IPAddress", typeof(string))]
        [StringLength(100, MinimumLength = 5)]
        public string IPAddress { get; set; }

        [Core.DataBaseFieldMapper("UserExpenseType", typeof(string))]
        public string UserExpenseType { get; set; }

        [Core.DataBaseFieldMapper("Localization", typeof(string))]
        [StringLength(15, MinimumLength = 5)]
        public string Localization { get; set; }

        [Core.DataBaseFieldMapper("FromDate", typeof(DateTime?))]
        [DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }

        [Core.DataBaseFieldMapper("ToDate", typeof(DateTime?))]
        [DataType(DataType.Date)]
        public DateTime? ToDate { get; set; }

        [Core.DataBaseFieldMapper("ExpenseSheetName", typeof(string))]
        [StringLength(50, MinimumLength = 5)]
        public string ExpenseSheetName { get; set; }

        [Core.DataBaseFieldMapper("LeaveComment", typeof(string))]
        [StringLength(500, MinimumLength = 5)]
        public string LeaveComment { get; set; }

        [Core.DataBaseFieldMapper("ClientName", typeof(string))]
        [StringLength(100, MinimumLength = 5)]
        public string ClientName { get; set; }

        [Core.DataBaseFieldMapper("ClientManagerName", typeof(string))]
        [StringLength(50)]
        public string ClientManagerName { get; set; }

        [Core.DataBaseFieldMapper("ServiceOrderNo", typeof(string))]
        [StringLength(50, MinimumLength = 5)]
        public string ServiceOrderNo { get; set; }

        [Core.DataBaseFieldMapper("ExplanationforSONo", typeof(string))]
        [StringLength(100, MinimumLength = 5)]
        public string ExplanationforSONo { get; set; }

        [Core.DataBaseFieldMapper("ServiceOrderConfirmationNo", typeof(string))]
        [StringLength(50, MinimumLength = 5)]
        public string ServiceOrderConfirmationNo { get; set; }

        [Core.DataBaseFieldMapper("SOConfirmationDate", typeof(DateTime?))]
        public DateTime? SOConfirmationDate { get; set; }

        [Core.DataBaseFieldMapper("TotalAmount", typeof(decimal))]
        public decimal TotalAmount { get; set; }

        [Core.DataBaseFieldMapper("AdvanceAmount", typeof(decimal))]
        public decimal AdvanceAmount { get; set; }

        [Core.DataBaseFieldMapper("Comments", typeof(string))]
        [StringLength(500, MinimumLength = 5)]
        public string Comments { get; set; }

        [Core.DataBaseFieldMapper("ManagerStatus", typeof(string))]
        [StringLength(10, MinimumLength = 5)]
        public string ManagerStatus { get; set; }

        [Core.DataBaseFieldMapper("ManagerApprovedDate", typeof(DateTime?))]
        public DateTime? ManagerApprovedDate { get; set; }

        [Core.DataBaseFieldMapper("ManagerComments", typeof(string))]
        [StringLength(100, MinimumLength = 5)]
        public string ManagerComments { get; set; }

        [Core.DataBaseFieldMapper("ManagerUpholdorRejectDate", typeof(DateTime?))]
        public DateTime? ManagerUpholdorRejectDate { get; set; }

        [Core.DataBaseFieldMapper("AccountManagerStatus", typeof(string))]
        [StringLength(10, MinimumLength = 5)]
        public string AccountManagerStatus { get; set; }

        [Core.DataBaseFieldMapper("AccountManagerApprovedDate", typeof(DateTime?))]
        public DateTime? AccountManagerApprovedDate { get; set; }

        [Core.DataBaseFieldMapper("AccountManagerComment", typeof(string))]
        [StringLength(100, MinimumLength = 5)]
        public string AccountManagerComment { get; set; }

        [Core.DataBaseFieldMapper("AccountManagerUpholdorRejectDate", typeof(DateTime?))]
        public DateTime? AccountManagerUpholdorRejectDate { get; set; }

        [Core.DataBaseFieldMapper("CreatedBy", typeof(string))]
        [StringLength(100, MinimumLength = 5), Required]
        public string CreatedBy { get; set; }

        [Core.DataBaseFieldMapper("CreatedDate", typeof(DateTime?))]
        [Required]
        public DateTime? CreatedDate { get; set; }

        [Core.DataBaseFieldMapper("ModifiedBy", typeof(string))]
        [StringLength(100, MinimumLength = 5)]
        public string ModifiedBy { get; set; }

        [Core.DataBaseFieldMapper("ModifiedDate", typeof(DateTime?))]
        public DateTime? ModifiedDate { get; set; }

   
        [Core.DataBaseFieldMapper("ReservedFields", typeof(string))]
        public string ReservedFields { get; set; }

        [Core.DataBaseFieldMapper("ReservedFields1", typeof(string))]
        public string ReservedFields1 { get; set; }

        [Core.DataBaseFieldMapper("ReservedFields2", typeof(string))]
        public string ReservedFields2 { get; set; }

        [Core.DataBaseFieldMapper("ReservedFields3", typeof(string))]
        public string ReservedFields3 { get; set; }

        [Core.DataBaseFieldMapper("ReservedFields4", typeof(string))]
        public string ReservedFields4 { get; set; }

        [Core.DataBaseFieldMapper("ReservedFields5", typeof(int?))]
        public int? ReservedFields5 { get; set; }

        [Core.DataBaseFieldMapper("ReservedFields6", typeof(int?))]
        public int? ReservedFields6 { get; set; }

        [Core.DataBaseFieldMapper("ReservedFields7", typeof(int?))]
        public int? ReservedFields7 { get; set; }

        [Core.DataBaseFieldMapper("ReservedFields8", typeof(int?))]
        public int? ReservedFields8 { get; set; }

        [Core.DataBaseFieldMapper("ReservedFields9", typeof(decimal))]
        public decimal ReservedFields9 { get; set; }

        [Core.DataBaseFieldMapper("ReservedFields10", typeof(decimal))]
        public decimal ReservedFields10 { get; set; }

        [Core.DataBaseFieldMapper("ReservedFields11", typeof(DateTime?))]
        public DateTime? ReservedFields11 { get; set; }

        [Core.DataBaseFieldMapper("ReservedFields12", typeof(DateTime?))]
        public DateTime? ReservedFields12 { get; set; }


        [Required(ErrorMessage = "Please Select Status")]
        public string TempStatus{ get; set; }

        [Required(ErrorMessage = "Please Enter Comments")]
        public string TempComments{ get; set; }

         [Core.DataBaseFieldMapper("EmployeeId", typeof(string))]
        public string EmployeeId { get; set; }

        [Core.DataBaseFieldMapper("EmpID", typeof(int?))]
        public int? empID { get; set; }

        [Core.DataBaseFieldMapper("EmpUserID", typeof(string))]
        public string EmpUserID { get; set; }

        [Core.DataBaseFieldMapper("EmpName", typeof(string))]
        public string EmpName { get; set; }

        [Core.DataBaseFieldMapper("ExpenseTotalAmount", typeof(decimal))]
        public decimal ExpenseTotalAmount { get; set; }

        [Core.DataBaseFieldMapper("ExpenseDate", typeof(DateTime))]
        public DateTime ExpenseDate { get; set; }

        [Core.DataBaseFieldMapper("SubmittedRecords", typeof(int?))]
        public int? SubmittedRecords { get; set; }

        [Core.DataBaseFieldMapper("Description", typeof(string))]
        public string ExpenseDescription { get; set; }

        [Core.DataBaseFieldMapper("PaidAmount", typeof(decimal))]
        public decimal PaidAmount { get; set; }

        [Core.DataBaseFieldMapper("PaidAmountDate", typeof(DateTime?))]
        public DateTime? PaidAmountDate { get; set; }

        [Core.DataBaseFieldMapper("Employee_ID", typeof(string))]
        public string Employee_ID { get; set; }

        [Core.DataBaseFieldMapper("EmployeeName", typeof(string))]
        public string EmployeeName { get; set; }

        [Core.DataBaseFieldMapper("EmployeeMailID", typeof(string))]
        public string EmployeeMailID { get; set; }

        [Core.DataBaseFieldMapper("ZoneID", typeof(int?))]
        public int? ZoneID { get; set; }

        [Core.DataBaseFieldMapper("ZoneName", typeof(string))]
        public string ZoneName { get; set; }

        [Core.DataBaseFieldMapper("ManagerID", typeof(int?))]
        public int? ManagerID { get; set; }

        [Core.DataBaseFieldMapper("ManagerName", typeof(string))]
        public string ManagerName { get; set; }

        [Core.DataBaseFieldMapper("ManagerEmailID", typeof(string))]
        public string ManagerEmailID { get; set; }

        [Core.DataBaseFieldMapper("AccountManagerID", typeof(int?))]
        public int? AccountManagerID { get; set; }

        [Core.DataBaseFieldMapper("AccountManagerName", typeof(string))]
        public string AccountManagerName { get; set; }

        [Core.DataBaseFieldMapper("AccountManagerEmailID", typeof(string))]
        public string AccountManagerEmailID { get; set; }

        [Core.DataBaseFieldMapper("ROLE", typeof(string))]
        public string ROLE { get; set; }

        [Core.DataBaseFieldMapper("Status", typeof(bool))]
        public bool Status { get; set; }


        [Core.DataBaseFieldMapper("SubmittedDate", typeof(DateTime))]
        public DateTime SubmittedDate { get; set; }

        [Core.DataBaseFieldMapper("BlockedStatus", typeof(string))]
        public string BlockedStatus { get; set; }

        [Core.DataBaseFieldMapper("ServiceOrderConfirmationDate", typeof(DateTime?))]
        public DateTime? ServiceOrderConfirmationDate { get; set; }

        [Core.DataBaseFieldMapper("SONoforinstruments", typeof(string))]
        public string SerialNoforInstrument { get; set; }

        [Core.DataBaseFieldMapper("PendingAmount", typeof(decimal))]
        public decimal PaidPendingAmount { get; set; }

        [Core.DataBaseFieldMapper("ReportTotalAmount", typeof(decimal))]
        public decimal TotalReportAmount { get; set; }

        [Core.DataBaseFieldMapper("FK_UserHeaderID", typeof(int?))]
        public int? FK_UserHeaderID { get; set; }

        [Core.DataBaseFieldMapper("MultipleSONoID", typeof(int?))]
        public int? MultipleSONoID { get; set; }

        [Core.DataBaseFieldMapper("TotalAdvanceAmount", typeof(decimal))]
        public decimal TotalAdvanceAmount { get; set; }

        [Core.DataBaseFieldMapper("LumpsumAmount", typeof(decimal))]
        public decimal LumpsumAmount { get; set; }

        [Core.DataBaseFieldMapper("MonthName", typeof(decimal))]
        public string MonthName { get; set; }

        [Core.DataBaseFieldMapper("MonthNo", typeof(decimal))]
        public int MonthNo { get; set; }

        [Core.DataBaseFieldMapper("MonthYear", typeof(decimal))]
        public int YearNo { get; set; }

    }
}
