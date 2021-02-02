using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Models;
using Microsoft.AspNetCore.Identity;

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
            modelBuilder.Entity<Setting>().HasData(
             new Setting
             {
                 Id = 1,
                 NeedEducationPostsToAccept = false,
                 NeedBannersToAccept = false,
                 PostCanShow = 100,
                 BannerCanShow = 100
             });

			var userId = UserConfiguration.MainAdminId;
			var adminUser = new ApplicationUser
			{
				Id = userId,
				UserName = "MainOwner",
				NormalizedUserName = "mainowner",
				Email = "mainowner@email.com",
				NormalizedEmail = "mainowner@email.com",
				EmailConfirmed = true,
				LockoutEnabled = false,
				SecurityStamp = Guid.NewGuid().ToString(),
			};

			var ph = new PasswordHasher<ApplicationUser>();
			adminUser.PasswordHash = ph.HashPassword(adminUser, "123456");

			modelBuilder.Entity<ApplicationUser>().HasData(adminUser);
			var adminRoleId = UserConfiguration.AdminRoleId;
			modelBuilder.Entity<IdentityRole>().HasData(
				new IdentityRole { Name = "admin", NormalizedName = "admin", Id = adminRoleId, ConcurrencyStamp = adminRoleId }			
			);
			modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
			{
				RoleId = adminRoleId,
				UserId = userId
			});
		}
    }
}
