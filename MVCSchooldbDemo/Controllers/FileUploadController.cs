using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using MVCSchooldbDemo.Models;

namespace MVCSchooldbDemo.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: FileUpload
        public ActionResult Upload(HttpPostedFileBase fileData, string targetDir)
        {
            if (fileData != null)
            {
                try
                {
                    var targetDirUrl = $"~/UploadFiles/{targetDir}/";
                    var saveDir = Server.MapPath(targetDirUrl);

                    if (!Directory.Exists(saveDir))
                    {
                        Directory.CreateDirectory(saveDir);
                    }

                    var sourceFileName = fileData.FileName;
                    var fileExtension = Path.GetExtension(sourceFileName);
                    var guid = Guid.NewGuid().ToString();
                    string saveFileName = $"{guid}{fileExtension}";
                    string saveFilePath = $"{saveDir}\\{saveFileName}";
                    string targetFileUrl = $"{targetDir}/{saveFileName}";

                    fileData.SaveAs(saveFilePath);

                    return new JsonResult
                    {
                        Data =
                            new UploadFileInfo
                            {
                                FileExtension = fileExtension,
                                Guid = guid,
                                TargetFilePath = saveFilePath,
                                TargetFileName = saveFileName,
                                TargetFileUrl = targetFileUrl,
                                UploadTime = DateTime.Now
                            }
                    };
                }
                catch (Exception ex)
                {
                    return Content(ex.Message);
                }
            }
            return Content("uploaded file is null");
            ;
        }
    }
}