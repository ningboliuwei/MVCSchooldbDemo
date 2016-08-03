﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MVCSchooldbDemo.Models.Data;
using MVCSchooldbDemo.Views.Student;
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
			context.Students.Add(new Student() {Sage = 20, Sdept = "CS", Sname = "Jane", Sno = "95012", Ssex="女"});
		}
	}
}