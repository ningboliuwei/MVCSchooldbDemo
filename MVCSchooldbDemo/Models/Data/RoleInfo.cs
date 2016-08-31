using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCSchooldbDemo.Models.Data
{
    [Table("TB_Role")]
    public class RoleInfo
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
    }
}