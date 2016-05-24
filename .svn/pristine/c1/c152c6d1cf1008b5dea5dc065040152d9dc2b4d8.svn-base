using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Universal.BusinessEntities
{
    public class Events : Core.BusinessBase<Events>
    {
        public EventImages eventImages { get; set; }
        public EventImagesCollection eventImagesCollection { get; set; }

        [Core.DataBaseFieldMapper("Id", typeof(int?))]
        public int? Id { get; set; }

        [Core.DataBaseFieldMapper("Title", typeof(string))]
        [Required(ErrorMessage = "Please Enter Event Title")]
        [StringLength(50, ErrorMessage = "Please enter maximum 50 characters only")]
        public string Title { get; set; }

        [Core.DataBaseFieldMapper("Date", typeof(DateTime))]
        [Required(ErrorMessage = "Please Enter Event Date")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime, ErrorMessage = "Please Select Valid Date")]
        public DateTime Date { get; set; }

        [Core.DataBaseFieldMapper("Venue", typeof(string))]
        [Required(ErrorMessage = "Please Enter Event Venue")]
        [StringLength(50, ErrorMessage = "Please enter maximum 50 characters only")]
        public string Venue { get; set; }

        [Core.DataBaseFieldMapper("Description", typeof(string))]
        [Required(ErrorMessage = "Please Enter Event Description")]
        [StringLength(500, ErrorMessage = "Please enter maximum 500 characters only")]
        public string Description { get; set; }

        [Core.DataBaseFieldMapper("Status", typeof(bool))]
        [Required(ErrorMessage = "Please Select Status")]
        public bool Status { get; set; }

        [Core.DataBaseFieldMapper("CreatedDate", typeof(DateTime))]
        public DateTime CreatedDate { get; set; }

        [Core.DataBaseFieldMapper("ModifiedDate", typeof(DateTime))]
        public DateTime ModifiedDate { get; set; }

        [Core.DataBaseFieldMapper("CoverImage", typeof(string))]
        public string CoverImage { get; set; }
    }
}
