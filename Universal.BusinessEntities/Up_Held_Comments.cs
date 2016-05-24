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

namespace Universal.BusinessEntities
{
    public class Up_Held_Comments : Core.BusinessBase<Up_Held_Comments>
    {
        [Core.DataBaseFieldMapper("Id", typeof(int?))]
        public int Id {get; set;}

        [Core.DataBaseFieldMapper("FK_UserExpenseHeader", typeof(int?))]
        public int FK_UserExpenseHeader { get; set; }

        [Core.DataBaseFieldMapper("ManagerUpheldComments", typeof(string))]
        public string ManagerUpheldComments { get; set; }

        [Core.DataBaseFieldMapper("ManagerUpheldCommentDate", typeof(DateTime))]
        public DateTime ManagerUpheldCommentDate { get; set; }

        [Core.DataBaseFieldMapper("AccountManagerUpheldComments", typeof(string))]
        public string AccountManagerUpheldComments { get; set; }

        [Core.DataBaseFieldMapper("AccountManagerUpheldCommentDate", typeof(DateTime))]
        public DateTime AccountManagerUpheldCommentDate { get; set; }
    }
}
