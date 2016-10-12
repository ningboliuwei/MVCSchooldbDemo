using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCSchooldbDemo.Models.Data
{
	[Table("T_病人基本资料")]
	public class PatientInfo
	{
		[Key]
		public long Id { get; set; }

		public string 编号 { get; set; }
		public string 住院号 { get; set; }
		public string 姓名 { get; set; }
		public string 性别 { get; set; }
		public DateTime 出生日期 { get; set; }
		public DateTime 入组日期 { get; set; }
		public string 民族 { get; set; }
		public string 婚姻状况 { get; set; }
		public string 文化程度 { get; set; }
		public string 职业 { get; set; }
		public string 年收入 { get; set; }
		public string 保险类别 { get; set; }
		public string 联系电话 { get; set; }
		public string 联系地址 { get; set; }
		public string 住所至康复中心交通是否便利 { get; set; }
		public string 是否参与工作 { get; set; }
		public string 吸烟 { get; set; }
		public string 药物不良反应 { get; set; }
		public string 目前的疾病 { get; set; }
	}
}