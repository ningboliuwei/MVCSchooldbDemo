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
		public int 大便 { get; set; }
		public int 小便 { get; set; }
		public int 修饰 { get; set; }
		public int 用厕 { get; set; }
		public int 进食 { get; set; }
		public int 转移 { get; set; }
		public int 活动 { get; set; }
		public int 穿衣 { get; set; }
		public int 上下楼梯 { get; set; }
		public int 洗澡 { get; set; }
		public int 总分 { get; set; }
		public string 被评估人 { get; set; }
		public DateTime 评估日期 { get; set; }
		public string 康复护士 { get; set; }
	}
}