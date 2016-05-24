using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Universal.BusinessEntities
{
   public class News: Core.BusinessBase<News>
    {
        [Core.DataBaseFieldMapper("Id", typeof(int?))]
        public int? Id { get; set; }

        [Core.DataBaseFieldMapper("Title", typeof(string))]
        [Required(ErrorMessage = "Please Enter News Title")]
        [StringLength(100, ErrorMessage = "Please enter maximum 100 characters only")]
        public string Title { get; set; }

        [Core.DataBaseFieldMapper("Description", typeof(string))]
        [Required(ErrorMessage = "Please Enter News Description")]
        [StringLength(1000, ErrorMessage = "Please enter maximum 1000 characters only")]
        public string Description { get; set; }

        [Core.DataBaseFieldMapper("Status", typeof(bool))]
        [Required(ErrorMessage = "Please Select Status")]
        public bool Status { get; set; }

        [Core.DataBaseFieldMapper("CreatedDate", typeof(DateTime))]
        public DateTime CreatedDate { get; set; }

        [Core.DataBaseFieldMapper("ModifiedDate", typeof(DateTime))]
        public DateTime ModifiedDate { get; set; }
    }
}
