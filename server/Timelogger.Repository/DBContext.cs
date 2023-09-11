using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Timelogger.Entities;

namespace Timelogger.Repository
{
	public class DBContext : DbContext
	{
		public DBContext(DbContextOptions<DBContext> options)
			: base(options)
		{
		}

		public DbSet<Project> Projects { get; set; }
		public DbSet<TimeRegistration> TimeRegistrations { get; set; }
		
		public static void SeedDatabase(IServiceScope scope)
		{
			var context = scope.ServiceProvider.GetService<DBContext>();
			var testProject1 = new Project
			{
				Id = 1,
				Name = "e-conomic Interview",
				Deadline = new DateTime(2015, 10, 01)
			};

			var testProject2 = new Project
			{
				Id = 2,
				Name = "Nexa Cloud",
				Deadline = new DateTime(2023, 10, 01)
			};

			var testProject3 = new Project
			{
				Id = 3,
				Name = "Safar Voyage",
				Deadline = new DateTime(2025, 01, 01)
			};
			
			var testProject4 = new Project
			{
				Id = 4,
				Name = "Green Step",
				Deadline = new DateTime(2024, 05, 24)
			};
			
			var testProject5 = new Project
			{
				Id = 5,
				Name = "Fit Zen",
				Deadline = new DateTime(2016, 12, 03)
			};

			context.Projects.Add(testProject1);
			context.Projects.Add(testProject2);
			context.Projects.Add(testProject3);
			context.Projects.Add(testProject4);
			context.Projects.Add(testProject5);

			var testProject2TimeRegistration1 = new TimeRegistration
			{
				Id = 1,
				ProjectId = 2,
				Start = new DateTime(2015, 10, 01, 8, 0, 0),
				End = new DateTime(2015, 10, 01, 10, 0, 0)
			};

			context.TimeRegistrations.Add(testProject2TimeRegistration1);

			context.SaveChanges();
		}
	}
}
