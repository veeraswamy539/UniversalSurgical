using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Universal.BusinessEntities
{
    public class ServiceOrderNos : Core.BusinessBase<ServiceOrderNos>
    {
        [Core.DataBaseFieldMapper("Id", typeof(int?))]
        public int Id { get; set; }

        [Core.DataBaseFieldMapper("FK_UserExpense_Header_Id", typeof(int?))]
        public int? FK_UserExpense_Header_Id { get; set; }

        [Core.DataBaseFieldMapper("ServiceOrderNo", typeof(string))]
        [StringLength(50, MinimumLength = 5)]
        public string ServiceOrderNo { get; set; }

        [Core.DataBaseFieldMapper("ServiceOrderConfirmationNo", typeof(string))]
        [StringLength(50, MinimumLength = 5)]
        public string ServiceOrderConfirmationNo { get; set; }

        [Core.DataBaseFieldMapper("ServiceOrderConfirmationDate", typeof(DateTime?))]
        public DateTime? ServiceOrderConfirmationDate { get; set; }

        [Core.DataBaseFieldMapper("ExplanationForSO", typeof(string))]
        [StringLength(100, MinimumLength = 5)]
        public string ExplanationforSONo { get; set; }

        [Core.DataBaseFieldMapper("SONoforinstruments", typeof(string))]
        public string SerialNoforInstrument { get; set; }
    }
}
