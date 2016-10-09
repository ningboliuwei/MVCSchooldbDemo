using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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