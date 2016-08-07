﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace MVCSchooldbDemo.Models.Data
{
    public class StudentInfo
    {
        [Key]
        public long Id { get; set; }
        public string Sno { get; set; }
        public string Sname { get; set; }
		public string Ssex { get; set; }
        public int Sage { get; set; }
        public string Sdept { get; set; }
        public string SphotoGuid { get; set; }
    }
}