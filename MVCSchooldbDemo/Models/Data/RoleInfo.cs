using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCSchooldbDemo.Models.Data
{
	[Table("TB_Role")]
	public class RoleInfo
	{
		[Key]
		public long Id { get; set; }

		public string Name { get; set; }
	}
}