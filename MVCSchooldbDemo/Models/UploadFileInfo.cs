using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSchooldbDemo.Models
{
    public class UploadFileInfo
    {
        public string Guid { get; set; }
        public string FileExtension { get; set; }
        public string TargetFileName { get; set; }
        public string TargetFilePath { get; set; }
        public string TargetFileUrl { get; set; }
        public DateTime UploadTime { get; set; }
    }
}