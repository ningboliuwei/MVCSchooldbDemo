using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCSchooldbDemo.Models.Data
{
    public class Student
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Sno { get; set; }
        public string Sname { get; set; }
        public int Sage { get; set; }
        public string Sdept { get; set; }
        public string Sphoto { get; set; }
    }
}