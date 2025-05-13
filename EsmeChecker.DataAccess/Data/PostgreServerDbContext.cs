using EsmeChecker.Entities;
using EsmeChecker.Models.StaticData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsmeChecker.DataAccess.Data
{

	public class PostgreServerDbContext : DbContext
	{
		public PostgreServerDbContext(DbContextOptions<PostgreServerDbContext> options) : base(options)
		{

		}

		//// DbSet properties for your tables
		public DbSet<Category> Categories { get; set; }
		public DbSet<Emplowee> Emplowees { get; set; }
		public DbSet<MaxMinConfig> MaxMinConfigs { get; set; }


		



		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//base.OnModelCreating(modelBuilder);
			modelBuilder.HasDefaultSchema(SD.Schema.EsmeCheckers);

			modelBuilder.Entity<Category>().HasData(
				//new Category { Id = 1, Name = "NSM", CreateDate = new DateTime(2025, 4, 26, 21, 6, 43, DateTimeKind.Utc) },
				//new Category { Id = 2, Administration = "Commercial",	Department = "Product Development",	Unit = "VAS", CreateDate = new DateTime(2025, 4, 26, 21, 6, 43, DateTimeKind.Utc) },
								
				new Category { Id = 1, Administration = "Telecom", Department = "Network Service", Unit = "VAS", CreateDate = DateTime.UtcNow },
				new Category { Id = 2, Administration = "Commercial", Department = "Product Development", Unit = "VAS", CreateDate = DateTime.UtcNow }
			);

			// Seed Emplowees (must reference CategoryId manually)
			modelBuilder.Entity<Emplowee>().HasData(
				new Emplowee
				{
					Id = 1,
					Msisdn = "218947776156",
					Name = "Mahmood Shehob",
					Email = "m.shehob@libyana.ly",
					Postion = "vas Engineer",
					CategoryId = 1, // FK to "NSM"
					CreateDate = DateTime.UtcNow,
					ModifyDate = default
				},
				new Emplowee
				{
					Id = 2,
					Msisdn = "218947777544",
					Name = "Aisha Zeglam",
					Email = "a.zeglam@libyana.ly",
					Postion = "vas Engineer",
					CategoryId = 1, // FK to "Commercial"
					Allow=false,
					CreateDate = DateTime.UtcNow,
					ModifyDate = default
				},
				new Emplowee
				{
					Id = 3,
					Msisdn = "218947775684",
					Name = "Said Grada",
					Email = "s.grada@libyana.ly",
					Postion = "vas Engineer",
					CategoryId = 1, // FK to "Commercial"
					CreateDate = DateTime.UtcNow,
					ModifyDate = default
				},
				new Emplowee
				{
					Id = 4,
					Msisdn = "218947775683",
					Name = "Makhzoum Alshuhoumi",
					Email = "m.alshuhoumi@libyana.ly",
					Postion = "vas Engineer",
					CategoryId = 1, // FK to "Commercial"
					CreateDate = DateTime.UtcNow,
					ModifyDate = default
				},
				new Emplowee
				{
					Id = 5,
					Msisdn = "218947777131",
					Name = "Mohamed Elsharef",
					Email = "M.Elsharef@libyana.ly",
					Postion = "emplowee",
					CategoryId = 2, // FK to "Commercial"
					CreateDate = DateTime.UtcNow,
					ModifyDate = default
				},
				new Emplowee
				{
					Id =6,
					Msisdn = "218947776081",
					Name = "Ghada Ali",
					Email = "GHADAH.ALI@Libyana.ly",
					Postion = "emplowee",
					CategoryId = 2, // FK to "Commercial"
					CreateDate = DateTime.UtcNow,
					ModifyDate = default
				}
			);
		}

		//protected override void OnModelCreating(ModelBuilder modelBuilder)
		//{
		//	// Set the default schema to expo24schema
		//	modelBuilder.HasDefaultSchema("expo24schema");

		//	// Fluent API configurations for SubscriptionReport
		//	modelBuilder.Entity<SubscriptionReport>()
		//		.Property(s => s.Msisdn)
		//		.HasMaxLength(12)
		//		.IsRequired();

		//	modelBuilder.Entity<SubscriptionReport>()
		//		.Property(s => s.VoucherCard)
		//		.HasMaxLength(255);

		//	base.OnModelCreating(modelBuilder);
		//}
	}
}