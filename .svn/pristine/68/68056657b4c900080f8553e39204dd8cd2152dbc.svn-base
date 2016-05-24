using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Universal.BusinessEntities
{
    public class TravelDesk : Core.BusinessBase<TravelDesk>
    {
        public TravelUploadTicket travelImages { get; set; }
        public TravelUplaodTickets travelTicketsCollection { get; set; }
        
        [Core.DataBaseFieldMapper("Id", typeof(int?))]
        public int? Id { get; set; }

        [Core.DataBaseFieldMapper("FK_EmployeeId", typeof(int?))]
        [Required(ErrorMessage = "Please Select Employee")]
        public int? FK_EmployeeId { get; set; }


        [Core.DataBaseFieldMapper("TraveledFromDate", typeof(DateTime?))]
        [DataType(DataType.Date, ErrorMessage = "Please select a valid date")]
        [Required(ErrorMessage = "Please Enter Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> TraveledFromDate { get; set; }

        [Core.DataBaseFieldMapper("TraveledToDate", typeof(DateTime?))]
        [DataType(DataType.Date, ErrorMessage = "Please select a valid date")]
        [Required(ErrorMessage = "Please select travel date ")]
        public DateTime? TraveledToDate { get; set; }

        [Core.DataBaseFieldMapper("FromLocation", typeof(string))]
        [Required(ErrorMessage = "Please Enter From Location")]
        [StringLength(100, MinimumLength = 2)]
        public string FromLocation { get; set; }

        [Core.DataBaseFieldMapper("ToLocation", typeof(string))]
        [Required(ErrorMessage = "Please Enter To Location")]
        [StringLength(100, MinimumLength = 2)]
        public string ToLocation { get; set; }

        [Core.DataBaseFieldMapper("TravelPurpose", typeof(string))]
        [Required(ErrorMessage = "Please Enter Purpose")]
        [StringLength(100, MinimumLength = 5)]
        public string Purpose { get; set; }

        [Core.DataBaseFieldMapper("ModeofTransportFrom", typeof(string))]
        [Required(ErrorMessage = "Please Select Mode of Transport")]
        public string ModeofTransportFrom { get; set; }

        [Core.DataBaseFieldMapper("ModeofTronsport", typeof(string))]
        [Required(ErrorMessage = "Please Select Mode of Transport")]
        public string ModeofTransport { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Amount must be a number")]
        [Core.DataBaseFieldMapper("TotalTicketAmount", typeof(float?))]
        [Required(ErrorMessage = "Please Enter Amount")]
        public float? TotalTicketAmount { get; set; }

        [Core.DataBaseFieldMapper("HotelBooking", typeof(string))]
        [Required(ErrorMessage = "Please Select Hotel Booking")]
        public string HotelBooking { get; set; }

        [Core.DataBaseFieldMapper("HotelDetails", typeof(string))]
        [Required(ErrorMessage = "Please Enter Hotel Details")]
        [StringLength(1000, MinimumLength = 5)]
        public string HotelDetails { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Amount must be a number")]
        [Core.DataBaseFieldMapper("HotelAmount", typeof(float?))]
        [Required(ErrorMessage = "Please Enter Hotel Amount")]
        public float? HotelBookingAmount { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Amount must be a number")]
        [Required(ErrorMessage = "Please Enter Advance Amount")]
        [Core.DataBaseFieldMapper("AdvanceAmount", typeof(float?))]
        public float? AdvanceAmount { get; set; }

        [Core.DataBaseFieldMapper("CreatedBy", typeof(string))]
        [StringLength(50, MinimumLength = 2)]
        public string CreatedBy { get; set; }

        [Core.DataBaseFieldMapper("CreatedDate", typeof(DateTime?))]
        public DateTime? CreatedDate { get; set; }

        [Core.DataBaseFieldMapper("ModifiedBy", typeof(string))]
        [StringLength(50, MinimumLength = 2)]
        public string ModifiedBy { get; set; }

        [Core.DataBaseFieldMapper("ModifiedDate", typeof(DateTime?))]
        public DateTime? ModifiedDate { get; set; }

        [Core.DataBaseFieldMapper("Employee_ID", typeof(string))]
        [Required(ErrorMessage = "Please Select Employee")]
        public string EmployeeId { get; set; }

        [Core.DataBaseFieldMapper("EmployeeName", typeof(string))]
        public string EmployeeName { get; set; }

        [Core.DataBaseFieldMapper("CheckedIndate", typeof(DateTime?))]
        [DataType(DataType.Date, ErrorMessage = "Please select a valid date")]
        [Required(ErrorMessage = "Please Enter CheckedIn Date")]
        public DateTime? CheckedIndate { get; set; }

        [Core.DataBaseFieldMapper("CheckedOutdate", typeof(DateTime?))]
        [DataType(DataType.Date, ErrorMessage = "Please select a valid date")]
        [Required(ErrorMessage = "Please Enter CheckedOut Date")]
        public DateTime? CheckedOutdate { get; set; }

        [Core.DataBaseFieldMapper("OverallComments", typeof(string))]
        [StringLength(1000, MinimumLength = 5)]
        public string OverallComments { get; set; }

        [Core.DataBaseFieldMapper("ManagerId", typeof(string))]
        public string ManagerId { get; set; }

    }
}
