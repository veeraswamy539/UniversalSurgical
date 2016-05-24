using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Universal.BusinessEntities
{
    public class SOCNumersExcel : Core.BusinessBase<SOCNumersExcel>
    {
        [Core.DataBaseFieldMapper("Proposal", typeof(string))]
        public string Proposal { get; set; }

        [Core.DataBaseFieldMapper("Status", typeof(string))]
        public string Status { get; set; }

        [Core.DataBaseFieldMapper("ASPCompany", typeof(string))]
        public string ASPManager { get; set; }

        [Core.DataBaseFieldMapper("ASPCompany", typeof(string))]
        public string ASPCompany { get; set; }

        [Core.DataBaseFieldMapper("ValueLC", typeof(string))]
        public string ValueLC { get; set; }

        [Core.DataBaseFieldMapper("CurrencyFormat", typeof(string))]
        public string CurrencyFormat { get; set; }

        [Core.DataBaseFieldMapper("ValueCLC", typeof(string))]
        public string ValueCLC { get; set; }

        [Core.DataBaseFieldMapper("CreatedOn", typeof(string))]
        public string CreatedOn { get; set; }

        [Core.DataBaseFieldMapper("abcd", typeof(string))]
        public string ASPInvoice { get; set; }

        [Core.DataBaseFieldMapper("SConfirmation", typeof(string))]
        public string SConfirmation { get; set; }

        [Core.DataBaseFieldMapper("SCItem", typeof(string))]
        public string SCItem { get; set; }

        [Core.DataBaseFieldMapper("SCStatus", typeof(string))]
        public string SCStatus { get; set; }

        [Core.DataBaseFieldMapper("SCDescription", typeof(string))]
        public string SCDescription { get; set; }

        [Core.DataBaseFieldMapper("SOrder", typeof(string))]
        public string SOrder { get; set; }

        [Core.DataBaseFieldMapper("SOrderItem", typeof(string))]
        public string SOrderItem { get; set; }

        [Core.DataBaseFieldMapper("SODescription", typeof(string))]
        public string SODescription { get; set; }

        [Core.DataBaseFieldMapper("Customer", typeof(string))]
        public string Customer { get; set; }

        [Core.DataBaseFieldMapper("CustomerCity", typeof(string))]
        public string CustomerCity { get; set; }

        [Core.DataBaseFieldMapper("CustomerCountry", typeof(string))]
        public string CustomerCountry { get; set; }

        [Core.DataBaseFieldMapper("abcd", typeof(string))]
        public string Currency { get; set; }

        [Core.DataBaseFieldMapper("StartDate", typeof(string))]
        public string StartDate { get; set; }

        [Core.DataBaseFieldMapper("EndDate", typeof(string))]
        public string EndDate { get; set; }

        [Core.DataBaseFieldMapper("Type", typeof(string))]
        public string Type { get; set; }

        [Core.DataBaseFieldMapper("ASPPerson", typeof(string))]
        public string ASPPerson { get; set; }

        [Core.DataBaseFieldMapper("Product", typeof(string))]
        public string Product { get; set; }

        [Core.DataBaseFieldMapper("ProductDescription", typeof(string))]
        public string ProductDescription { get; set; }

        [Core.DataBaseFieldMapper("SYSUnit", typeof(string))]
        public string SYSUnit { get; set; }

        [Core.DataBaseFieldMapper("SerialNumber", typeof(string))]
        public string SerialNumber { get; set; }

        [Core.DataBaseFieldMapper("AvgStandardH", typeof(string))]
        public string AvgStandardH { get; set; }

        [Core.DataBaseFieldMapper("ElapsedH", typeof(string))]
        public string ElapsedH { get; set; }

        [Core.DataBaseFieldMapper("Factor", typeof(string))]
        public string Factor { get; set; }

        [Core.DataBaseFieldMapper("Compensation", typeof(string))]
        public string Compensation { get; set; }

        [Core.DataBaseFieldMapper("Amount", typeof(string))]
        public string Amount { get; set; }

        [Core.DataBaseFieldMapper("Paid", typeof(string))]
        public string Paid { get; set; }

        [Core.DataBaseFieldMapper("AvgTravelH", typeof(string))]
        public string AvgTravelH { get; set; }

        [Core.DataBaseFieldMapper("ElapsedTravelH", typeof(string))]
        public string ElapsedTravelH { get; set; }

        [Core.DataBaseFieldMapper("TravelCompensation", typeof(string))]
        public string TravelCompensation { get; set; }

        [Core.DataBaseFieldMapper("Mileage", typeof(string))]
        public string Mileage { get; set; }

        [Core.DataBaseFieldMapper("TravelFactor", typeof(string))]
        public string TravelFactor { get; set; }

        [Core.DataBaseFieldMapper("TravelAmount", typeof(string))]
        public string TravelAmount { get; set; }

        [Core.DataBaseFieldMapper("TravelPaid", typeof(string))]
        public string TravelPaid { get; set; }

        [Core.DataBaseFieldMapper("MiscCharge", typeof(string))]
        public string MiscCharge { get; set; }

        [Core.DataBaseFieldMapper("Total", typeof(string))]
        public string Total { get; set; }

        [Core.DataBaseFieldMapper("Note", typeof(string))]
        public string Note { get; set; }

        [Core.DataBaseFieldMapper("SpecialCode", typeof(string))]
        public string SpecialCode { get; set; }

        [Core.DataBaseFieldMapper("ErrorMessage", typeof(string))]
        public string ErrorMessage { get; set; }

    }
}
