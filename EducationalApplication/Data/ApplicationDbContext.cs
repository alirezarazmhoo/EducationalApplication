using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EducationalApplication.Models;
using EducationalApplication.Models.Enums;
using Microsoft.AspNetCore.Identity;

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
		public DbSet<SchoolName>SchoolName{ get; set; }
		public DbSet<Grade> Grades { get; set; }
		public DbSet<Major> Majors { get; set; }
		public DbSet<Students> Students { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Banner> Banners { get; set; }
		public DbSet<BannerToPost> BannerToPosts { get; set; }
		public DbSet<ClassRoom> ClassRooms { get; set; }
		public DbSet<TeachersToClassRoom> TeachersToClassRooms { get; set; }
		public DbSet<CustomGroup> CustomGroups { get; set; }
		public DbSet<UsersToCustomGroups> UsersToCustomGroups { get; set; }
		public DbSet<UsersToEducationPost> UsersToEducationPosts { get; set; }
		public DbSet<Comment>  Comments { get; set; }
		public DbSet<EducationPostView> EducationPostViews { get; set; }
		public DbSet<Setting> Settings { get; set; }
		public DbSet<Favorit> Favorits { get; set; }
		public DbSet<Announcements>  Announcements { get; set; }
		public DbSet<AboutUs> AboutUs { get; set; }
		public DbSet<CustomGroupsToEducationPosts>  CustomGroupsToEducationPosts { get; set; }
		public DbSet<CommentMedia>  CommentMedias { get; set; }	
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
	
			modelBuilder.Seed();
			//modelBuilder.Entity<Comment>().Property(s => s.date).HasDefaultValue(DateTime.MinValue);
		}
	}
}
