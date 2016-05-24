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
using System.Web.Mvc;


namespace Universal.BusinessEntities
{

    public class Employees : Core.BusinessBase<Employees>
    {
        [Core.DataBaseFieldMapper("Id", typeof(int?))]
        public int? Id { get; set; }

        [Core.DataBaseFieldMapper("Employee_ID", typeof(string))]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Please enter minimum of 6 characters and maximum of 20 characters"), Required(ErrorMessage = "Please Enter Employee ID")]
        public string Employee_ID { get; set; }

        [Core.DataBaseFieldMapper("Password", typeof(string))]
        [StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }

       
        [StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter New Password")]
        public string NewPassword { get; set; }

       
        [StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter Confirm Password")]
        [Compare("NewPassword",ErrorMessage="Password and Confirm Password did not match")]
        public string ConfirmPassword { get; set; }

        [Core.DataBaseFieldMapper("FirstName", typeof(string))]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z_]*$", ErrorMessage = "Please Enter Valid First Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Please enter minimum of 2 characters and maximum of 50 characters"), Required(ErrorMessage = "Please Enter First Name")]
        public string FirstName { get; set; }

        [Core.DataBaseFieldMapper("MiddleName", typeof(string))]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z_]*$", ErrorMessage = "Please Enter Valid Middle Name")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Please enter minimum of 1 character and maximum of 50 characters")]
        public string MiddleName { get; set; }

        [Core.DataBaseFieldMapper("LastName", typeof(string))]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z_]*$", ErrorMessage = "Please Enter valid Last Name")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Please enter minimum of 1 character and maximum of 50 characters"), Required(ErrorMessage = "Please Enter Last Name")]
        public string LastName { get; set; }

        [Core.DataBaseFieldMapper("FullName", typeof(string))]
        public string FullName { get; set; }

        [Core.DataBaseFieldMapper("FathersName", typeof(string))]
        [RegularExpression(@"^[a-zA-Z]+[ .a-zA-Z_]*$", ErrorMessage = "Please Enter Valid Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Please enter minimum of 2 characters and maximum of 50 characters"), Required(ErrorMessage = "Please Enter Father's Name")]
        public string FathersName { get; set; }

        [Core.DataBaseFieldMapper("MothersName", typeof(string))]
        [RegularExpression(@"^[a-zA-Z]+[ .a-zA-Z_]*$", ErrorMessage = "Please Enter Valid Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Please enter minimum of 2 characters and maximum of 50 characters")]
        public string MothersName { get; set; }

        [Core.DataBaseFieldMapper("DOB", typeof(DateTime))]
        [DataType(DataType.DateTime, ErrorMessage = "Please Select  Valid Date"), Required(ErrorMessage = "Please Enter DOB")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DOB { get; set; }

        [Core.DataBaseFieldMapper("Gender", typeof(string))]
        [Required(ErrorMessage = "Please Select Gender")]
        public string Gender { get; set; }

        [Core.DataBaseFieldMapper("PermanentAddress", typeof(int?))]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Please enter minimum of 10 characters and maximum of 500 characters "), Required(ErrorMessage = "Please Enter Permanent Address")]
        public string PermanentAddress { get; set; }

        [Core.DataBaseFieldMapper("FK_PermanentCity", typeof(int?))]
        [Required(ErrorMessage = "Please Select District")]
        public int? FK_PermanentCity { get; set; }

        [Core.DataBaseFieldMapper("FK_PermanentState", typeof(int?))]
        [Required(ErrorMessage = "Please Select State")]
        public int? FK_PermanentState { get; set; }

        [Core.DataBaseFieldMapper("PermanentCityname", typeof(string))]
        [RegularExpression(@"^[a-zA-Z]+[ .a-zA-Z_]*$", ErrorMessage = "City can accept space and alphabet only")]
        [Required(ErrorMessage = "Enter Permanent City Name")]
        public string PermanentCityname { get; set; }


        [Core.DataBaseFieldMapper("PermanentPincode", typeof(string))]
        [Required(ErrorMessage = "Please Enter Pincode")]
        [RegularExpression(@"^(\d{6})$", ErrorMessage = "Pincode should be 6 digits only")]
        public string PermanentPincode { get; set; }

        [Core.DataBaseFieldMapper("TempAddress", typeof(string))]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Please enter minimum of 10 characters and maximum of 500 characters ")]
        public string TempAddress { get; set; }

        [Core.DataBaseFieldMapper("FK_TempCity", typeof(int?))]
        public int? FK_TempCity { get; set; }

        [Core.DataBaseFieldMapper("FK_TempState", typeof(int?))]
        public int? FK_TempState { get; set; }

        [Core.DataBaseFieldMapper("CurrentCityname", typeof(string))]
        [RegularExpression(@"^[a-zA-Z]+[ .a-zA-Z_]*$", ErrorMessage = "City can accept space and alphabet only")]
        public string CurrentCityname { get; set; }


        [Core.DataBaseFieldMapper("TempPincode", typeof(string))]
        [RegularExpression(@"^(\d{6})$", ErrorMessage = "Pincode should be 6 digits only")]
        public string TempPincode { get; set; }


        [Core.DataBaseFieldMapper("MobileNo", typeof(string))]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Please Enter 10 Digits"), Required(ErrorMessage = "Please Enter Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        public string MobileNo { get; set; }

        [Core.DataBaseFieldMapper("EmergencyContactPersonName", typeof(string))]
        [RegularExpression(@"^[a-zA-Z]+[ .a-zA-Z_]*$", ErrorMessage = "Please Enter Valid Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Please enter minimum of 2 characters and maximum of 50 characters"), Required(ErrorMessage = "Please Enter Emergency Contact Person Name")]
        public string EmergencyContactPerson { get; set; }

        [Core.DataBaseFieldMapper("EmergencyContactNo", typeof(string))]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Please Enter 10 Digits"), Required(ErrorMessage = "Please Enter Emergency Contact No")]
        [DataType(DataType.PhoneNumber)]
        public string EmergencyContactNo { get; set; }

        [Core.DataBaseFieldMapper("BloodGroup", typeof(string))]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Please enter minimum of 2 characters and maximum of 15 characters")]
        public string BloodGroup { get; set; }

        [Core.DataBaseFieldMapper("ProfilePicture", typeof(byte?))]
        public byte? ProfilePicture { get; set; }

        [Core.DataBaseFieldMapper("ProfilePicturePath", typeof(string))]
        //[StringLength(1000)]
        public string ProfilePicturePath { get; set; }

        [Core.DataBaseFieldMapper("PANNo", typeof(string))]
        //[Required(ErrorMessage = "Please Enter PAN Number")]
        [RegularExpression(@"[A-Za-z]{5}\d{4}[A-Za-z]{1}", ErrorMessage = "Please Enter Valid PAN Number")]
        public string PANNo { get; set; }

        [Core.DataBaseFieldMapper("AadharNo", typeof(string))]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "Please Enter Valid Aadhar Number")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please Enter 12 Digits")]
        public string AadharNo { get; set; }

        [Core.DataBaseFieldMapper("VoterId", typeof(string))]
        [StringLength(25, MinimumLength = 10)]
        public string VoterId { get; set; }

        [Core.DataBaseFieldMapper("DrivingLicenceNo", typeof(string))]
        [StringLength(25, MinimumLength = 6)]
        public string DrivingLicenceNo { get; set; }

        [Core.DataBaseFieldMapper("RCNo", typeof(string))]
        [StringLength(10, MinimumLength = 10)]
        public string RCNo { get; set; }

        [Core.DataBaseFieldMapper("DOJ", typeof(DateTime))]
        [DataType(DataType.DateTime), Required(ErrorMessage = "Please Enter DOJ")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DOJ { get; set; }

        [Core.DataBaseFieldMapper("DrivingLicenceExpiryDate", typeof(DateTime))]
        [DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DrivingLicenceExpiryDate { get; set; }


        [Core.DataBaseFieldMapper("PFNo", typeof(string))]
        [StringLength(15, MinimumLength = 5)]
        public string PFNo { get; set; }

        [Core.DataBaseFieldMapper("UANforPFNo", typeof(string))]
        [StringLength(25, MinimumLength = 5)]
        public string UANforPFNo { get; set; }

        [Core.DataBaseFieldMapper("ESINo", typeof(string))]
        [StringLength(20, MinimumLength = 5)]
        public string ESINo { get; set; }

        [Core.DataBaseFieldMapper("SAPBussinessId", typeof(string))]
        [StringLength(50, MinimumLength = 5)]
        public string SAPBussinessId { get; set; }

        [Core.DataBaseFieldMapper("PassportNumber", typeof(string))]
        [RegularExpression(@"^[A-Za-z]{1}[0-9]{7}$", ErrorMessage = "Please Enter Valid Passport Number")]
        public string PassportNumber { get; set; }

        [Core.DataBaseFieldMapper("PassportValidateDate", typeof(DateTime))]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PassportValidateDate { get; set; }

        [Core.DataBaseFieldMapper("BankName", typeof(string))]
        [StringLength(50, MinimumLength = 3)]
        public string BankName { get; set; }

        [Core.DataBaseFieldMapper("BranchandLocation", typeof(string))]
        [StringLength(100, MinimumLength = 5)]
        public string BranchandLocation { get; set; }

        [Core.DataBaseFieldMapper("BankAccountNo", typeof(string))]
        [StringLength(30, MinimumLength = 4)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Digits")]
        public string BankAccountNo { get; set; }

        [Core.DataBaseFieldMapper("BankIFSCCode", typeof(string))]
        [StringLength(20, MinimumLength = 3)]
        public string BankIFSCCode { get; set; }

        [Core.DataBaseFieldMapper("PersonalEmailID", typeof(string))]
        [StringLength(50, MinimumLength = 8)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})\s*", ErrorMessage = "E-mail is not valid")]
        public string PersonalEmailID { get; set; }

        [Core.DataBaseFieldMapper("CompanyMailID", typeof(string))]
        [StringLength(50, MinimumLength = 8)]
        [Required(ErrorMessage = "Please Enter Company Email Id")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})\s*", ErrorMessage = "E-mail is not valid")]
        public string CompanyMailID { get; set; }

        [Core.DataBaseFieldMapper("ManagerName", typeof(string))]
        public string ManagerName { get; set; }

        [Core.DataBaseFieldMapper("ManagerEmailID", typeof(string))]
        public string ManagerEmailID { get; set; }

        [Core.DataBaseFieldMapper("AccountManagerName", typeof(string))]
        public string AccountManagerName { get; set; }

        [Core.DataBaseFieldMapper("AccountManagerEmailID", typeof(string))]
        public string AccountManagerEmailID { get; set; }


        [Core.DataBaseFieldMapper("FK_UserGroup", typeof(int?))]
        [Required(ErrorMessage = "Please Select UserGroup")]
        public int? FK_UserGroup { get; set; }

        [Core.DataBaseFieldMapper("VehicleType", typeof(string))]
        [Required(ErrorMessage = "Please Select Vehicle Type")]
        public string VehicleType { get; set; }

        [Core.DataBaseFieldMapper("Conveyance", typeof(decimal))]
        [Required(ErrorMessage = "Please Enter Conveyance Amount")]
        [Range(0.50, Double.MaxValue, ErrorMessage = "Please enter valid amount")]
        //[RegularExpression(@"^(\d{1,6})$", ErrorMessage = "Amount should contain digits but not morethan 6 digits ")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Please enter valid amount")]

        public decimal Conveyance { get; set; }

        [Core.DataBaseFieldMapper("FK_LocationorZone", typeof(int?))]
        [Required(ErrorMessage = "Please Select Zone")]
        public int? FK_LocationorZone { get; set; }

        [Core.DataBaseFieldMapper("Status", typeof(bool))]
        [Required(ErrorMessage = "Please Select Status")]
        public bool Status { get; set; }

        [Core.DataBaseFieldMapper("CreatedBy", typeof(string))]
        [StringLength(100, MinimumLength = 3), Required]
        public string CreatedBy { get; set; }

        [Core.DataBaseFieldMapper("CreatedDate", typeof(DateTime))]
        [DataType(DataType.DateTime), Required]
        public DateTime? CreatedDate { get; set; }

        [Core.DataBaseFieldMapper("ModifiedBy", typeof(string))]
        [StringLength(100, MinimumLength = 3)]
        public string ModifiedBy { get; set; }

        [Core.DataBaseFieldMapper("ModifiedDate", typeof(DateTime?))]
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }

        [Core.DataBaseFieldMapper("NoOfLeaves", typeof(int?))]
        //[Required(ErrorMessage = "Please Enter Leaves")]
        public int? NoOfLeaves { get; set; }


        [Core.DataBaseFieldMapper("ZoneName", typeof(string))]
        public string ZoneName { get; set; }

        [Core.DataBaseFieldMapper("Position", typeof(string))]
        public string Position { get; set; }
        public string Temimagepath { get; set; }
        public string ReservedFields { get; set; }

        public string ReservedFields1 { get; set; }

        public string ReservedFields2 { get; set; }

        public string ReservedFields3 { get; set; }

        public string ReservedFields4 { get; set; }

        public int? ReservedFields5 { get; set; }

        public int? ReservedFields6 { get; set; }

        public int? ReservedFields7 { get; set; }

        public int? ReservedFields8 { get; set; }

        public decimal ReservedFields9 { get; set; }

        public decimal ReservedFields10 { get; set; }

        public DateTime? ReservedFields11 { get; set; }

        public DateTime? ReservedFields12 { get; set; }


        [Required(ErrorMessage = "Please Select Manager")]
        [Core.DataBaseFieldMapper("ManagerId", typeof(int?))]
        public int? ManagerId { get; set; }

        [Required(ErrorMessage = "Please Select Account Manager")]
        [Core.DataBaseFieldMapper("AccountManagerId", typeof(int?))]
        public int? AccountManagerId { get; set; }


        [Core.DataBaseFieldMapper("ManagerUserId", typeof(string))]
        public string ManagerUserId { get; set; }

        [Core.DataBaseFieldMapper("ROLE", typeof(string))]
        public string Role { get; set; }


        [Core.DataBaseFieldMapper("CompanyLeftDate", typeof(DateTime?))]
        [DataType(DataType.DateTime, ErrorMessage = "Please Select Valid Date")]
        public DateTime? CompanyLeftDate { get; set; }


        [Core.DataBaseFieldMapper("LeavingReason", typeof(string))]
        public string LeavingReason { get; set; }
        [Core.DataBaseFieldMapper("TempState", typeof(string))]
        public string TempState { get; set; }

        [Core.DataBaseFieldMapper("TempCity", typeof(string))]
        public string TempCity { get; set; }

        [Core.DataBaseFieldMapper("StateName", typeof(string))]
        public string Statename { get; set; }

        [Core.DataBaseFieldMapper("CityName", typeof(string))]
        public string Cityname { get; set; }

        [Core.DataBaseFieldMapper("GenderName", typeof(string))]
        public string GenderName { get; set; }

        [Required(ErrorMessage = "Please Select Marital Status")]
        [Core.DataBaseFieldMapper("MaritalStatus", typeof(string))]
        public string MaritalStatus { get; set; }


        public string LeftDate { get; set; }

        //[Required(ErrorMessage = "Please Enter NOK")]
        //[Core.DataBaseFieldMapper("NOK", typeof(string))]
        //public string NOK { get; set; }

        [Core.DataBaseFieldMapper("NOK", typeof(string))]
        [RegularExpression(@"^[a-zA-Z]+[ .a-zA-Z_]*$", ErrorMessage = "Please Enter valid NOK")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Please enter minimum of 2 and maximum of 50 characters"), Required(ErrorMessage = "Please Enter NOK")]
        public string NOK { get; set; }
        
        [Required(ErrorMessage = "Please Select Payment Option")]
        public string LumpsumAmountDDL { get; set; }


        [Core.DataBaseFieldMapper("LumpsumAmount", typeof(decimal))]
        [Required(ErrorMessage = "Please Enter Lumpsum Amount")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Please enter valid amount")]
        [Range(1, Double.MaxValue, ErrorMessage = "Please enter valid amount")]
        public decimal LumpsumAmount { get; set; }

        public bool SameAddress { get; set; }

    }

    public class ZonesWiseStatusCount : Core.BusinessBase<ZonesWiseStatusCount>
    {
         [Core.DataBaseFieldMapper("ZoneName", typeof(string))]
        public string ZoneName { get; set; }
         [Core.DataBaseFieldMapper("Active", typeof(int))]
        public int Active { get; set; }
         [Core.DataBaseFieldMapper("Inactive", typeof(int))]
        public int Inactive { get; set; }
    }
}
