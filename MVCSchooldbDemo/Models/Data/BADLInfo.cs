using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCSchooldbDemo.Models.Data
{
	[Table("T_Barthel指数评价表")]
	public class BADLInfo
	{
		[Key]
		public long Id { get; set; }

		public DateTime 评估日期 { get; set; }
	}
}