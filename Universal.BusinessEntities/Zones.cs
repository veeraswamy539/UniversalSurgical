using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Universal.BusinessEntities
{
    public class Zones : Core.BusinessBase<Zones>
    {
        [Core.DataBaseFieldMapper("Id", typeof(int?))]
        public int? Id { get; set; }

        [Core.DataBaseFieldMapper("Zone", typeof(string))]
        [StringLength(50, MinimumLength = 2), Required]
        public string Zone { get; set; }

        [Core.DataBaseFieldMapper("Description", typeof(string))]
        [StringLength(100, MinimumLength = 5)]
        public string Description { get; set; }

        [Core.DataBaseFieldMapper("NoofUsers", typeof(string))]
        public string NoEmployees {get;set;}

        [Core.DataBaseFieldMapper("NoofMgrs", typeof(string))]
        public string NoManagers {get;set;}


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
