using System;
using System.IO;
using System.Linq;
using System.Web;
using MVCSchooldbDemo.Models.Data;
using MVCSchooldbDemo.Views.Student;

namespace MVCSchooldbDemo.Common
{
	public class UploadFileHelper
	{
		public static UploadedFileInfo Upload(HttpPostedFileBase fileData, string targetDirectory)
		{
			var uploadedFileRootDirectory = "UploadedFiles";
			if (fileData != null)
			{
				try
				{
					var targetSaveDirectory = HttpContext.Current.Server.MapPath($"~/{uploadedFileRootDirectory}/{targetDirectory}");

					if (!Directory.Exists(targetSaveDirectory))
					{
						Directory.CreateDirectory(targetSaveDirectory);
					}

					var sourceFileName = fileData.FileName;
					var fileExtension = Path.GetExtension(sourceFileName);
					var guid = Guid.NewGuid().ToString();
					var baseDirectory = $"{uploadedFileRootDirectory}/{targetDirectory}";
					var saveFileName = $"{guid}{fileExtension}";
					var saveFilePath = $"{targetSaveDirectory}\\{saveFileName}";

					fileData.SaveAs(saveFilePath);

					var file = new UploadedFileInfo
					{
						FileExtension = fileExtension,
						Guid = guid,
						FileName = saveFileName,
						BaseDirectory = baseDirectory,
						UploadTime = DateTime.Now
					};

					var db = new SchooldDbContext();
					db.UploadFiles.Add(file);
					db.SaveChanges();

					return file;
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
			}
			return null;
		}

		public static UploadedFileInfo GetFileInfoByGuid(string guid)
		{
			var db = new SchooldDbContext();
			return db.UploadFiles.ToList().Find(g => g.Guid == guid);
		}
	}
}