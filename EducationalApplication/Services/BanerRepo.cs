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
    public class BanerRepo:RepositoryBase<Banner>,IBanerRepo
    {
        public BanerRepo(ApplicationDbContext DbContext)
            : base(DbContext)
        {
            _DbContext = DbContext;
        }
        public async Task<List<Banner>> GetAll(string Id)
        {
            return await FindAll(null).Include(c => c.BannerToPosts).Include(c => c.Category)/*.Where(s=>s.ApplicationUserId == Id)*/.OrderByDescending(c => c.Id)
                .ToListAsync();
        }
        public async Task<Banner> GetById(int Id)
        {
             return await FindByCondition(b => b.Id.Equals(Id)).FirstOrDefaultAsync();
        }
        public async Task AddOrUpdate(Banner model, IFormFile _File)
        {
          if(model.Id == 0)
            {

                if (_File != null)
                {
                    var fileName = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(_File.FileName).ToLower(); ;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Banner\File", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        _File.CopyTo(fileStream);
                    }
                    model.Url = "/Upload/Banner/File/" + fileName;
                }
                model.CreateDate=DateTime.Now;
                model.AvailableDate = DateTime.Now; 
              Create(model);
            }

            else
            {
                var getBanner = await GetById(model.Id);
                if (getBanner != null)
                {
                    if (_File != null)
                    {
                        //*************  Delete Image
                        //if (!string.IsNullOrEmpty(getBanner.Url))
                        //{
                        //    File.Delete($"wwwroot/{getBanner.Url}");
                        //}
                        //*************   Add Image
                        var fileName = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(_File.FileName).ToLower(); ;
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Banner\File", fileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            _File.CopyTo(fileStream);
                        }
                        model.Url = "/Upload/Banner/File/" + fileName;
                    }
                    getBanner.CreateDate = DateTime.Now;
                    getBanner.CategoryId = model.CategoryId;
                    getBanner.AvailableDate = model.AvailableDate;
                    getBanner.BannerPlace = model.BannerPlace;
                    getBanner.CreditDays = model.CreditDays;
                    getBanner.Description = model.Description;
                    getBanner.ShowOnMainPage = model.ShowOnMainPage;
                    getBanner.SocialNetWorkLink = model.SocialNetWorkLink;
                    getBanner.Url = model.Url;
                    getBanner.ApplicationUserId = model.ApplicationUserId;
                    Update(model);
                }
            }
        }
        public  void Remove(Banner model)
        {

            if (!string.IsNullOrEmpty(model.Url))
            {
                File.Delete($"wwwroot/{model.Url}");
            }
            Delete(model);
           
        }
        public async Task RemoveFile(int Id)
        {
            Banner Item = await _DbContext.Banners.FindAsync(Id);
            if (Item != null)
            {
                File.Delete($"wwwroot/{Item.Url}");
            }

        }
    }
}
