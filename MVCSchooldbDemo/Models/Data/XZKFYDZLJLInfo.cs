using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCSchooldbDemo.Models.Data
{
	[Table("T_心脏康复运动治疗记录")]
	public class XZKFYDZLJLInfo
	{
		[Key]
		public long Id { get; set; }

		public string 病人编号 { get; set; }
		public string 靶心率 { get; set; }
	}
}