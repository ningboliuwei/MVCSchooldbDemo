using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCSchooldbDemo.Models.Data
{
	public class XZKFYDZLJLInfo
	{
		[Key]
		public long Id { get; set; }

		public string 病人编号 { get; set; }
		public string 靶心率 { get; set; }
	}
}