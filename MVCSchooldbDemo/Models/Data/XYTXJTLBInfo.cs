using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCSchooldbDemo.Models.Data
{
	[Table("T_西雅图心绞痛量表")]
	public class XYTXJTLBInfo
	{
		[Key]
		public long Id { get; set; }

		public DateTime 评估日期 { get; set; }
	}
}