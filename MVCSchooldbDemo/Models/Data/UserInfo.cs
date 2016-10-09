using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCSchooldbDemo.Models.Data
{
	[Table("TB_Users")]
	public class UserInfo
	{
		[Key]
		public long Id { get; set; }

		public string Account { get; set; }
		public string Password { get; set; }
		public string FullName { get; set; }
		public long RoleId { get; set; }
	}
}