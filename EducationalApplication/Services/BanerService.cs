using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Data;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EducationalApplication.Services
{
    public class BanerService:RepositoryBase<Banner>,IBanerServices
    {
        private ApplicationDbContext _DbContext;

        public BanerService(ApplicationDbContext DbContext)
            : base(DbContext)
        {
            _DbContext = DbContext;
        }


        public async Task<List<Banner>> GetAllBaner()
        {
            //var item=await  _DbContext.Banners.Include(c => c.BannerToPosts).ToListAsync();
            //return item;
            return await FindAll(null).Include(c => c.BannerToPosts).Include(c => c.Category).OrderByDescending(c => c.Id)
                .ToListAsync();


        }

        public async Task<Banner> GetbanerById(int Id)
        {
            //var item = await _DbContext.Banners.FindAsync(Id);
            //return  item;

            return await FindByCondition(b => b.Id.Equals(Id)).FirstOrDefaultAsync();
        }

        public async Task AddBaner(Banner baner, IFormFile BanerAvatar)
        {
            try
            {
                if (BanerAvatar != null)
                {

                    var fileName = Guid.NewGuid().ToString().Replace('-', '0') + "." + BanerAvatar.FileName.Split('.')[1];
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Banner\File", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        BanerAvatar.CopyTo(fileStream);
                    }
                    baner.Url = "/Upload/Banner/File/" + fileName;
                }
              //  baner.ApplicationUserId=_DbContext.User
              baner.CreateDate=DateTime.Now;

              Create(baner);

            }
            catch (Exception e)
            {
            
            }




        }

        public async Task Editbanner( Banner baner, IFormFile BanerAvatar)
        {
            Task<Banner> getBanner= GetbanerById(baner.Id);

            try
            {
                if (BanerAvatar != null)
                {
                    //*************  Delete Image
                    if (!string.IsNullOrEmpty(baner.Url))
                    {
                        File.Delete($"wwwroot/{baner.Url}");
                    }

                    //*************   Add Image
                    var fileName = Guid.NewGuid().ToString().Replace('-', '0') + "." + BanerAvatar.FileName.Split('.')[1];
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Banner\File", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        BanerAvatar.CopyTo(fileStream);
                    }
                    baner.Url = "/Upload/Banner/File/" + fileName;
                }


                getBanner.Result.CreateDate = DateTime.Now;
                getBanner.Result.CategoryId = baner.CategoryId;
                getBanner.Result.AvailableDate = baner.AvailableDate;
                getBanner.Result.BannerPlace = baner.BannerPlace;
                getBanner.Result.CreditDays = baner.CreditDays;
                getBanner.Result.Description = baner.Description;
                getBanner.Result.ShowOnMainPage = baner.ShowOnMainPage;
                getBanner.Result.SocialNetWorkLink = baner.SocialNetWorkLink;
                getBanner.Result.Url = baner.Url;
                getBanner.Result. ApplicationUserId= baner.ApplicationUserId;

                Update(baner);
            }
            catch (Exception e)
            {
              
            }



        }

        public void Remove(Banner baner)
        {


            if (!string.IsNullOrEmpty(baner.Url))
            {
                File.Delete($"wwwroot/{baner.Url}");
            }

            Delete(baner);


        }
    }
}
