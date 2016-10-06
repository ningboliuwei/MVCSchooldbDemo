using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCSchooldbDemo.Models.Data
{
    [Table("T_工具性日常生活活动量表")]
    public class GJXRCSHHDLBInfo
    {
        [Key]
        public long Id { get; set; }
        public DateTime 评估日期 { get; set; }
    }
}