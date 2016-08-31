using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSchooldbDemo.Models.Data
{
    [Table("TB_数据字典")]
    [Authorize]
    public class DataDictItemInfo
    {
        [Key]
        public string Id { get; set; }
        public string TypeName { get; set; }
        public string Values { get; set; }
    }
}