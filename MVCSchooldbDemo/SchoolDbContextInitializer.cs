using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MVCSchooldbDemo.Models.Data;
using MVCSchooldbDemo.Views.Student;
using SQLite.CodeFirst;

namespace MVCSchooldbDemo
{
	public class SchoolDbContextInitializer : SqliteCreateDatabaseIfNotExists<SchooldDbContext>
	{
		public SchoolDbContextInitializer(DbModelBuilder modelBuilder) :base(modelBuilder)
		{
		}

		protected override void Seed(SchooldDbContext context)
		{
			context.Set<Student>().Add(new Student() {Sage = 10, Sdept = "CS", Sname = "Jane", Sno = "95012"});
			context.Set<Student>().Add(new Student() { Sage = 20, Sdept = "MA", Sname = "Tom", Sno = "95013" });
		}
	}
}