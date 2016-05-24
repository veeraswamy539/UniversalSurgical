using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Universal.BusinessEntities
{
    public class ExpenseTypeDetails : Core.BusinessBase<ExpenseTypeDetails>
    {
        [Core.DataBaseFieldMapper("Id", typeof(int?))]
        public int Id { get; set; }

        [Core.DataBaseFieldMapper("Description", typeof(string))]
        [StringLength(50, MinimumLength = 5), Required]
        public string Description { get; set; }

        [Core.DataBaseFieldMapper("FK_ExpenseType", typeof(int?))]
        public int? FK_ExpenseType { get; set; }
    }
}
