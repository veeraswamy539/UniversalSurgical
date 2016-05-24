using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Universal.BusinessEntities
{
    public class ExpenseType : Core.BusinessBase<ExpenseType>
    {
        [Core.DataBaseFieldMapper("Id", typeof(int?))]
        public int? id { get; set; }

        [Core.DataBaseFieldMapper("ExpanseName", typeof(string))]
        [StringLength(100, MinimumLength = 5), Required]
        public string ExpenseName { get; set; }

        [Core.DataBaseFieldMapper("Description", typeof(string))]
        [StringLength(100, MinimumLength = 5),Required]
        public string Description { get; set; }


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

       
    }
}
