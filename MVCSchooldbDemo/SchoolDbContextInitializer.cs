using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MVCSchooldbDemo.Models.Data;
using SQLite.CodeFirst;

namespace MVCSchooldbDemo
{
	public class SchoolDbContextInitializer : SqliteDropCreateDatabaseWhenModelChanges<SchooldDbContext>
	{
		public SchoolDbContextInitializer(DbModelBuilder modelBuilder) :base(modelBuilder)
		{
		}

		protected override void Seed(SchooldDbContext context)
		{
		}
	}
}