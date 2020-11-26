using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Models;
namespace EducationalApplication.Data
{
	public static class DataSeeder
	{
		public static void Seed(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<SchoolName>().HasData(
		   new SchoolName
		   {
			   Id = 1,
               Name = "نامشخص"
		   }
			);
		}
	}
}
