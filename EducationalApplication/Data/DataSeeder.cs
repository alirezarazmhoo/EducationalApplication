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

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Administrator", NormalizedName = "ADMINISTRATOR".ToUpper() });

 //           var hasher = new PasswordHasher<ApplicationUser>();

 //         modelBuilder.Entity<ApplicationUser>().HasData(
 //    new ApplicationUser
 //    {
 //        Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
 //        UserName = "MainOwner",
 //        NormalizedUserName = "MainOwner",
 //        PasswordHash = hasher.HashPassword(null, "123456")
 //    }
 //);
        }
    }
}
