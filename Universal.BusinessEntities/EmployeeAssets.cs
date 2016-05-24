using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Universal.BusinessEntities
{
    public class EmployeeAssets : Core.BusinessBase<EmployeeAssets>
    {
        [Core.DataBaseFieldMapper("Id", typeof(int?))]
        public int? Id { get; set; }

        [Core.DataBaseFieldMapper("FK_EmployeeID", typeof(int?))]
        [Required(ErrorMessage="Please Select Employee")]
        public int FK_EmployeeID { get; set; }

        [Core.DataBaseFieldMapper("Employee_ID", typeof(string))]
        public string Employee_ID { get; set; }

        [Core.DataBaseFieldMapper("Laptop", typeof(bool))]
        public bool Laptop { get; set; }

        [Core.DataBaseFieldMapper("LaptopNo", typeof(string))]
        public string LaptopNo { get; set; }

        [Core.DataBaseFieldMapper("LaptopBag", typeof(bool))]
        public bool LaptopBag { get; set; }

        [Core.DataBaseFieldMapper("LaptopBagName", typeof(string))]
        public string LaptopBagName { get; set; }

        [Core.DataBaseFieldMapper("Datacard", typeof(bool))]
        public bool Datacard { get; set; }

        [Core.DataBaseFieldMapper("DatacardNo", typeof(string))]
        public string DatacardNo { get; set; }

        [Core.DataBaseFieldMapper("Simcard", typeof(bool))]
        public bool Simcard { get; set; }

        [Core.DataBaseFieldMapper("SimcardNo", typeof(string))]
        public string SimcardNo { get; set; }

        [Core.DataBaseFieldMapper("Multimeter", typeof(bool))]
        public bool Multimeter { get; set; }

        [Core.DataBaseFieldMapper("MultimeterNo", typeof(string))]
        public string MultimeterNo { get; set; }

        [Core.DataBaseFieldMapper("Toolkit", typeof(bool))]
        public bool Toolkit { get; set; }

        [Core.DataBaseFieldMapper("ToolkitNo", typeof(string))]
        public string ToolkitNo { get; set; }

        [Core.DataBaseFieldMapper("Cellphone", typeof(bool))]
        public bool Cellphone { get; set; }

        [Core.DataBaseFieldMapper("CellphoneNo", typeof(string))]
        //[StringLength(10, MinimumLength = 10, ErrorMessage = "Please Enter 10 Digits"), Required(ErrorMessage = "Please Enter Mobile Number")]
        public string CellphoneNo { get; set; }

        [Core.DataBaseFieldMapper("Others", typeof(string))]
        public string Others { get; set; }

        [Core.DataBaseFieldMapper("CreatedBy", typeof(string))]
        public string CreatedBy { get; set; }

        [Core.DataBaseFieldMapper("CreatedDate", typeof(DateTime?))]
        public DateTime? CreatedDate { get; set; }

        [Core.DataBaseFieldMapper("ModifiedBy", typeof(string))]
        public string ModifiedBy { get; set; }

        [Core.DataBaseFieldMapper("ModifiedDate", typeof(DateTime?))]
        public DateTime? ModifiedDate { get; set; }


        [Core.DataBaseFieldMapper("ZoneName", typeof(string))]
        public string ZoneName { get; set; }

        [Core.DataBaseFieldMapper("ZoneId", typeof(int?))]
        public int? ZoneId { get; set; }

         [Core.DataBaseFieldMapper("EMPFullName", typeof(string))]
        public string EMPFullName { get; set; }
         
        public int? flag { get; set; }
    } 
}
