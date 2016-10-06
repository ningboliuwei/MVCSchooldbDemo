using System.Data.Entity;
using MVCSchooldbDemo.Migrations;
using MVCSchooldbDemo.Models.Data;

namespace MVCSchooldbDemo
{
    public sealed class SchooldDbContext : DbContext
    {
        //您的上下文已配置为从您的应用程序的配置文件(App.config 或 Web.config)
        //使用“Schooldb”连接字符串。默认情况下，此连接字符串针对您的 LocalDb 实例上的
        //“MVCSchooldbDemo.Views.Student.Schooldb”数据库。
        // 
        //如果您想要针对其他数据库和/或数据库提供程序，请在应用程序配置文件中修改“Schooldb”
        //连接字符串。
        public SchooldDbContext()
            : base("name=Schooldb")
        {
        }

        //为您要在模型中包含的每种实体类型都添加 DbSet。有关配置和使用 Code First  模型
        //的详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=390109。

        public DbSet<StudentInfo> Students { get; set; }
        public DbSet<UploadedFileInfo> UploadFiles { get; set; }

        public DbSet<UserInfo> Users { get; set; }

        public DbSet<RoleInfo> Roles { get; set; }

        public DbSet<PatientInfo> Patients { get; set; }
        public DbSet<DataDictItemInfo> DataDictItems { get; set; }

		public DbSet<XZKFYDCFInfo> XZKFYDCFs { get; set; }//心脏康复运动处方

		public DbSet<XZKFYDZLJLInfo> XZKFYDZLJLs { get; set; }//心脏康复运动治疗记录

        public DbSet<BADLInfo> BADLs { get; set; }//心脏康复运动治疗记录

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
			            Database.SetInitializer(new SchoolDbContextInitializer(modelBuilder));
//			Database.SetInitializer(new MigrateDatabaseToLatestVersion<SchooldDbContext, Configuration>());
		}
    }
}