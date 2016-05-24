using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Universal.BusinessEntities
{
   public class EventImages: Core.BusinessBase<EventImages>
    {
       [Core.DataBaseFieldMapper("Id", typeof(int?))]
       public int? Id { get; set; }

       [Core.DataBaseFieldMapper("Fk_EventId", typeof(int?))]
       public int? Fk_EventId { get; set; }

       [Core.DataBaseFieldMapper("ImageName", typeof(string))]
       public string ImageName { get; set; }
    }
}
