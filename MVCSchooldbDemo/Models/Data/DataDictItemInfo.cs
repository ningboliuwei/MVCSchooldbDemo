using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using SQLite.CodeFirst;

namespace MVCSchooldbDemo.Models.Data
{
    [Table("TB_数据字典")]
    [Authorize]
    public class DataDictItemInfo
    {
        [Key]
        public long Id { get; set; }
        [Unique]
        public string 项目名 { get; set; }
        public string 项目值 { get; set; }
    }
}