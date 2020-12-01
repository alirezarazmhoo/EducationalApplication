using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EducationalApplication.Models;
using EducationalApplication.Models.Enums;

namespace EducationalApplication.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{


		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Media> Medias { get; set; }
		public DbSet<EducationPost> EducationPosts { get; set; }
		//public DbSet<Teacher> Teachers { get; set; }
		public DbSet<SchoolName>SchoolName{ get; set; }
		public DbSet<Grade> Grades { get; set; }
		public DbSet<Major> Majors { get; set; }
		public DbSet<Students> Students { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Banner> Banners { get; set; }
		public DbSet<BannerToPost> BannerToPosts { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Seed();
		}
	}
}
