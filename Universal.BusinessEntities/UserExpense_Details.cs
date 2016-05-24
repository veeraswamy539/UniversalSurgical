using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Universal.BusinessEntities
{
    public class UserExpense_Details : Core.BusinessBase<UserExpense_Details>
    {
        [Core.DataBaseFieldMapper("Id", typeof(int?))]
        public int Id { get; set; }

        [Core.DataBaseFieldMapper("FK_UserExpenseHeader", typeof(int?))]
        public int? FK_UserExpenseHeader { get; set; }

        [Core.DataBaseFieldMapper("FromLocation", typeof(string))]
        [StringLength(50, MinimumLength = 5)]
        public string FromLocation { get; set; }

        [Core.DataBaseFieldMapper("ToLocation", typeof(string))]
        [StringLength(50, MinimumLength = 5)]
        public string ToLocation { get; set; }

        [Core.DataBaseFieldMapper("FK_ExpenseType", typeof(int?))]
        public int? FK_ExpenseType { get; set; }

        [Core.DataBaseFieldMapper("FK_ExpenseTypeDetails", typeof(int?))]
        public int? FK_ExpenseTypeDetails { get; set; }

        [Core.DataBaseFieldMapper("ReasonForOther", typeof(string))]
        public string ReasonForOther { get; set; }

        [Core.DataBaseFieldMapper("TraveledKms", typeof(decimal))]
        public decimal TraveledKms { get; set; }

        [Core.DataBaseFieldMapper("Conveyance", typeof(decimal))]
        public decimal Conveyance { get; set; }

        [Core.DataBaseFieldMapper("Amount", typeof(decimal))]
        public decimal Amount { get; set; }

        [Core.DataBaseFieldMapper("InvoiceNo", typeof(string))]
        [StringLength(50, MinimumLength = 5)]
        public string InvoiceNo { get; set; }

        [Core.DataBaseFieldMapper("CommentsforExpenseType", typeof(string))]
        [StringLength(500, MinimumLength = 5)]
        public string CommentsforExpenseType { get; set; }

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


        [Core.DataBaseFieldMapper("FullName", typeof(string))]
        public string FullName { get; set; }

        [Core.DataBaseFieldMapper("ProfilePicturePath", typeof(string))]
        public string ProfilePicturePath { get; set; }

        [Core.DataBaseFieldMapper("SON", typeof(string))]
        public string SON { get; set; }

        [Core.DataBaseFieldMapper("SOC", typeof(string))]
        public string SOC { get; set; }

        [Core.DataBaseFieldMapper("SOCDate", typeof(DateTime?))]
        public DateTime? SOCDate { get; set; }

        [Core.DataBaseFieldMapper("TotalExpenseAmount", typeof(decimal))]
        public decimal TotalExpenseAmount { get; set; }

        [Core.DataBaseFieldMapper("Localization", typeof(string))]
        public string Localization { get; set; }

        [Core.DataBaseFieldMapper("Managername", typeof(string))]
        public string Managername { get; set; }

        [Core.DataBaseFieldMapper("Zone", typeof(string))]
        public string Zone { get; set; }

       [Core.DataBaseFieldMapper("ClientName", typeof(string))]
        public string ClientName{ get; set; }

       [Core.DataBaseFieldMapper("ExpenseDate", typeof(DateTime))]
       public DateTime ExpenseDate { get; set; }

        [Core.DataBaseFieldMapper("ReservedFields", typeof(string))]
        [StringLength(50, MinimumLength = 5)]
        public string ReservedFields { get; set; }

        [Core.DataBaseFieldMapper("ReservedFields1", typeof(string))]
        [StringLength(50, MinimumLength = 5)]
        public string ReservedFields1 { get; set; }

        [Core.DataBaseFieldMapper("ReservedFields2", typeof(string))]
        [StringLength(50, MinimumLength = 5)]
        public string ReservedFields2 { get; set; }

        [Core.DataBaseFieldMapper("ReservedFields3", typeof(string))]
        [StringLength(50, MinimumLength = 5)]
        public string ReservedFields3 { get; set; }

        [Core.DataBaseFieldMapper("ReservedFields4", typeof(string))]
        [StringLength(50, MinimumLength = 5)]
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

        [Core.DataBaseFieldMapper("AccountManagerStatus", typeof(string))]
        public string AccountManagerStatus { get; set; }

        [Core.DataBaseFieldMapper("ManagerStatus", typeof(string))]
        public string ManagerStatus { get; set; }

        [Core.DataBaseFieldMapper("AccountManagerComment", typeof(string))]
        public string AccountManagerComment { get; set; }

        [Core.DataBaseFieldMapper("ManagerComments", typeof(string))]
        public string ManagerComments { get; set; }

        [Core.DataBaseFieldMapper("ExpenseType", typeof(string))]
        public string ExpenseType { get; set; }

        [Core.DataBaseFieldMapper("ExpenseTypeDetails", typeof(string))]
        public string ExpenseTypeDetails { get; set; }

        [Core.DataBaseFieldMapper("MobileNo", typeof(string))]
        public string MobileNo { get; set; }

        [Core.DataBaseFieldMapper("TravelDeskAmount", typeof(decimal))]
        public decimal TravelDeskAmount { get; set; }


        [Core.DataBaseFieldMapper("AdvanceAmount", typeof(decimal))]
        public decimal AdvanceAmount { get; set; }

        [Core.DataBaseFieldMapper("UpheldAmount", typeof(decimal))]
        public decimal UpheldAmount { get; set; }

        [Core.DataBaseFieldMapper("SONoforinstruments", typeof(string))]
        public string SerialNoforInstrument { get; set; }

    }
}
