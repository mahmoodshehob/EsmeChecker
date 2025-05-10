//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using System;

//namespace EsmeChecker.DataAccess.Data
//{
//    public class SqlServerDbContext : DbContext
//	{
//		private IConfiguration _config;

//		public SqlServerDbContext(IConfiguration config, DbContextOptions<SqlServerDbContext> options)
//			: base(options)
//		{
//			_config = config;
//		}

//		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//		{
//			if (!optionsBuilder.IsConfigured)
//			{
//				// Use the connection string from appsettings.json
//				{
//					if (_config.GetSection("ASPNETCORE_ENVIRONMENT").Value == "Development")
//					{
//						optionsBuilder.UseSqlServer("Server=localhost\\mshehobsql,1433;Database=EsmeCheckerDb;User Id=vs_shehob;password=vs_sh123;TrustServerCertificate=true;Encrypt=false;MultipleActiveResultSets=true");
//					}
//					else
//					{
//						optionsBuilder.UseSqlServer("Server=GECOL-SQL.libyana.ly;Database=GecolDB;User ID=gecol_user;Password=JOe7Zsx7lRzcUUv;TrustServerCertificate=true;Encrypt=false;MultipleActiveResultSets=true");


//					}
//				}
//			}
//		}
//	}
//}