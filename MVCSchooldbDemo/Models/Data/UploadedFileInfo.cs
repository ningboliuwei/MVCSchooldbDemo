using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCSchooldbDemo.Models.Data
{
    [Table("T_UploadedFile")]
    public class UploadedFileInfo
    {
		[Key]
        public string Guid { get; set; }
        public string FileExtension { get; set; }
		public string BaseDirectory { get; set; }
        public string FileName { get; set; }
        public DateTime UploadTime { get; set; }
    }
}