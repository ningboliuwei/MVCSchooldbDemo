using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCSchooldbDemo.Models.Data
{
	[Table("T_心脏康复运动处方")]
	public class XZKFYDCFInfo
	{
		[Key]
		public long Id { get; set; }

		public string 病人编号 { get; set; }
		public string 基础心率 { get; set; }
		public string 运动峰值心率 { get; set; }
		public string 基础血压 { get; set; }
		public string 运动峰值血压 { get; set; }
		public string 心肺联合运动试验平板运动实验结果 { get; set; }
	}
}