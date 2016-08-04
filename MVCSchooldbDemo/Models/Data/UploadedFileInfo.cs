using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCSchooldbDemo.Models.Data
{
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