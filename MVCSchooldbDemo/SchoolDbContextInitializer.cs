using System.Data.Entity;
using MVCSchooldbDemo.Models.Data;
using SQLite.CodeFirst;

namespace MVCSchooldbDemo
{
	public class SchoolDbContextInitializer : SqliteCreateDatabaseIfNotExists<SchooldDbContext>
	{
		public SchoolDbContextInitializer(DbModelBuilder modelBuilder) : base(modelBuilder)
		{
		}

		protected override void Seed(SchooldDbContext context)
		{
			context.Users.Add(new UserInfo
			{
				Account = "Admin",
				FullName = "刘慰",
				Password = "698D51A19D8A121CE581499D7B701668",
				RoleId = 1
			}); //密码为：111
			context.Roles.Add(new RoleInfo {Name = "管理员"});
		}
	}
}