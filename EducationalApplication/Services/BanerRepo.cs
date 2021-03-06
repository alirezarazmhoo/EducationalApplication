﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Data;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
using EducationalApplication.Models.ViewModels;
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
        public async Task<List<BannerViewModel>> GetAll(string Id)
        {
            var SettingItem = await _DbContext.Settings.FirstOrDefaultAsync();
            List<Banner> banners = new List<Banner>();
            List<BannerViewModel> bannerViewModels = new List<BannerViewModel>();
            if (SettingItem.NeedBannersToAccept)
            {
                banners = await _DbContext.Banners.Include(c => c.PostsInBanner).Include(c => c.Category).Where(s=>s.ApplicationUserId == Id).OrderByDescending(c => c.Pin == true).ThenByDescending(s => s.Id)
                 .ToListAsync();
            }
            else
            {
                banners = await _DbContext.Banners.Include(c => c.PostsInBanner).Include(c => c.Category).Where(s=>s.ApplicationUserId == Id).OrderByDescending(c => c.Pin == true).ThenByDescending(s => s.Id)
                .ToListAsync();
            }
                foreach (var item in banners)
                {
                    bannerViewModels.Add(new BannerViewModel()
                    {
                        ApplicationUser = item.ApplicationUser,
                        ApplicationUserId = item.ApplicationUserId,
                        AvailableDate = item.AvailableDate,
                        BannerPlace = item.BannerPlace,
                        BannerStatus = item.BannerStatus,
                        Category = item.Category,
                        CategoryId = item.CategoryId,
                        CreateDate = item.CreateDate,
                        CreditDays = item.CreditDays,
                        Description = item.Description,
                        Id = item.Id,
                        IsOnlyForTeacher = item.IsOnlyForTeacher,
                        Pin = item.Pin,
                        ShowOnMainPage = item.ShowOnMainPage,
                        SocialNetWorkLink = item.SocialNetWorkLink,
                        Url = item.Url,
                        PostsInBanner = item.PostsInBanner.Select(s => s.EducationPostId).ToList()
                    });
                }
            return bannerViewModels; 
            }
        public async Task<Banner> GetById(int Id)
        {
            var SettingItem = await _DbContext.Settings.FirstOrDefaultAsync();
            if (SettingItem.NeedBannersToAccept)
            {
                return await FindByCondition(b => b.Id.Equals(Id) && b.BannerStatus == Models.Enums.BannerStatus.Accepted).FirstOrDefaultAsync();
            }
            else
            {
                return await FindByCondition(b => b.Id.Equals(Id)).FirstOrDefaultAsync();
            }
        }
        public async Task AddOrUpdate(Banner model, IFormFile _File)
        {
           List<BannerToPost> bannerToPosts = new List<BannerToPost>();
            var SettingItem = await _DbContext.Settings.FirstOrDefaultAsync();

            string[] _BannerList = new string[] { };
            int EducationId = 0; 
            if (model.Id == 0)
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
                model.BannerStatus = SettingItem.NeedEducationPostsToAccept ? Models.Enums.BannerStatus.Waiting : Models.Enums.BannerStatus.Accepted;
                _DbContext.Banners.Add(model);
                _DbContext.SaveChanges();
                if(!string.IsNullOrEmpty(model.BannerToPosts) && string.IsNullOrEmpty(model.SocialNetWorkLink) && model.CategoryId == null )
                {
                    _BannerList = model.BannerToPosts.Split(",");

                    for (int i = 0; i < _BannerList.Count(); i++)
                    {
                        EducationId = int.Parse(_BannerList[i]);
                        if (await _DbContext.EducationPosts.AnyAsync(s => s.Id == EducationId))
                        {

                         bannerToPosts.Add(new BannerToPost() { BannerId = model.Id, EducationPostId = EducationId });
                            
                        }
                    }
                    await _DbContext.BannerToPosts.AddRangeAsync(bannerToPosts);
                }
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
                        var fileName = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(_File.FileName).ToLower(); 
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
                    getBanner.Pin = model.Pin;
                    getBanner.IsOnlyForTeacher = model.IsOnlyForTeacher; 
                    model.BannerStatus = SettingItem.NeedEducationPostsToAccept ? Models.Enums.BannerStatus.Waiting : Models.Enums.BannerStatus.Accepted;
                    Update(model);
                    if (!string.IsNullOrEmpty(model.BannerToPosts) && string.IsNullOrEmpty(model.SocialNetWorkLink) && model.CategoryId == null)
                    {
                        _DbContext.BannerToPosts.RemoveRange(_DbContext.BannerToPosts.Where(s => s.BannerId == model.Id));
                        _BannerList = model.BannerToPosts.Split(",");

                        for (int i = 0; i < _BannerList.Count(); i++)
                        {
                            EducationId = int.Parse(_BannerList[i]);
                            if (await _DbContext.EducationPosts.AnyAsync(s => s.Id == EducationId))
                            {                             
                            bannerToPosts.Add(new BannerToPost() { BannerId = model.Id, EducationPostId = EducationId });
                            }
                        }
                        await _DbContext.BannerToPosts.AddRangeAsync(bannerToPosts);
                    }
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
        public async Task RemoveEducationPostFromBanner(int BannerId , int PostId)
        {
            if(await _DbContext.Banners.AnyAsync(s=>s.Id == BannerId) && await _DbContext.BannerToPosts.AnyAsync(s=>s.EducationPostId == PostId))
            {
                BannerToPost bannerToPost = await _DbContext.BannerToPosts.FirstOrDefaultAsync(s => s.BannerId == BannerId && s.EducationPostId == PostId);
                _DbContext.BannerToPosts.Remove(bannerToPost);
            }
        }
        public async Task AddPostToBanner(int BannerId , int PostId)
        {
            Banner banner = await _DbContext.Banners.FirstOrDefaultAsync(s => s.Id == BannerId);
            EducationPost educationPost = await _DbContext.EducationPosts.FirstOrDefaultAsync(s => s.Id == PostId);
            BannerToPost bannerToPost = new BannerToPost(); 
            if(banner !=null && educationPost !=null)
            {
                if(string.IsNullOrEmpty(banner.SocialNetWorkLink) && banner.CategoryId == null)
                {

                    bannerToPost.EducationPostId = PostId;
                    bannerToPost.BannerId = BannerId;
                    await _DbContext.BannerToPosts.AddAsync(bannerToPost);
                }
            }
        }
        public async Task<IEnumerable<Banner>> AdminGetAll()
        {
            List<Banner> Items = new List<Banner>();
            Items = await _DbContext.Banners.Include(s => s.ApplicationUser).OrderByDescending(s => s.Id).ToListAsync();
            return Items;
        }
        public async Task Accept(int Id)
        {
            var Item = await _DbContext.Banners.FirstOrDefaultAsync(s => s.Id == Id);
            if (Item != null)
            {
                Item.BannerStatus = Models.Enums.BannerStatus.Accepted;
            }
        }
        public async Task Reject(int Id)
        {
            var Item = await _DbContext.Banners.FirstOrDefaultAsync(s => s.Id == Id);
            if (Item != null)
            {
                Item.BannerStatus = Models.Enums.BannerStatus.Rejected;
            }
        }
        public async Task<Banner> AdminGetById(int Id)
        {
            Banner Item = new Banner();
            Item = await _DbContext.Banners.Include(s=>s.ApplicationUser).Where(s => s.Id == Id).FirstOrDefaultAsync();
            return Item;
        }
        public async Task<List<BannerViewModel>> GetAllForMainPage(string Id)
        {
            ClassRoom classRoomItem = new ClassRoom();
            List<ApplicationUser> TeacherList = new List<ApplicationUser>();
            List<TeachersToClassRoom> teachersToClassRooms = new List<TeachersToClassRoom>();
            List<Banner> banners = new List<Banner>();
            Students studentItem = new Students();
            ApplicationUser ApplicationUserItem = new ApplicationUser();
            List<BannerViewModel> bannerViewModels = new List<BannerViewModel>();
            int _StudentId = 0; 
            var SettingItem = await _DbContext.Settings.FirstOrDefaultAsync();
            if (int.TryParse(Id, out int n))
            {
             _StudentId = Convert.ToInt32(Id);
             studentItem = await _DbContext.Students.FirstOrDefaultAsync(s => s.Id.Equals(_StudentId));
                classRoomItem = await _DbContext.ClassRooms.FirstOrDefaultAsync(s => s.Id.Equals(studentItem.ClassRoomId));
                teachersToClassRooms = await _DbContext.TeachersToClassRooms.Where(s => s.ClassRoomId == classRoomItem.Id).ToListAsync();
                foreach (var item in teachersToClassRooms)
                {
                        TeacherList.Add(await _DbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(item.ApplicationUserId)));
                }
                foreach (var item in TeacherList)
                {
                    if (SettingItem.NeedBannersToAccept)
                    {
                    banners.AddRange(await _DbContext.Banners.Include(c => c.PostsInBanner).Include(c => c.Category).Where(s => s.ApplicationUserId.Equals(item.Id) && s.BannerStatus == Models.Enums.BannerStatus.Accepted && s.IsOnlyForTeacher == false).OrderByDescending(c => c.Pin == true).ThenByDescending(s => s.Id).ToListAsync());
                    }
                    else
                    {
                        banners.AddRange(await _DbContext.Banners.Include(c => c.PostsInBanner).Include(c => c.Category).Where(s => s.ApplicationUserId.Equals(item.Id) &&  s.IsOnlyForTeacher == false).OrderByDescending(c => c.Pin == true).ThenByDescending(s => s.Id).ToListAsync());
                    }
                }
            }
            else
            {
                 ApplicationUserItem  = await _DbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(Id));
                if(ApplicationUserItem != null)
                {
                    if(ApplicationUserItem.UserType == Models.Enums.UserType.Teacher)
                    {
                        banners = await _DbContext.Banners.Include(c => c.PostsInBanner).Include(c => c.Category).OrderByDescending(c => c.Pin == true).ThenByDescending(s => s.Id).ToListAsync();
                    }
                    else
                    {
                        banners   =  await _DbContext.Banners.Include(c => c.PostsInBanner).Include(c => c.Category).OrderByDescending(c => c.Pin == true).ThenByDescending(s => s.Id).ToListAsync();
                    }
                }       
            }
            foreach (var item in banners)
            {
                bannerViewModels.Add(new BannerViewModel()
                {
                    ApplicationUser = item.ApplicationUser,
                    ApplicationUserId = item.ApplicationUserId,
                    AvailableDate = item.AvailableDate,
                    BannerPlace = item.BannerPlace,
                    BannerStatus = item.BannerStatus,
                    Category = item.Category,
                    CategoryId = item.CategoryId,
                    CreateDate = item.CreateDate,
                    CreditDays = item.CreditDays,
                    Description = item.Description,
                    Id = item.Id,
                    IsOnlyForTeacher = item.IsOnlyForTeacher,
                    Pin = item.Pin,
                    ShowOnMainPage = item.ShowOnMainPage,
                    SocialNetWorkLink = item.SocialNetWorkLink,
                    Url = item.Url,
                    PostsInBanner = item.PostsInBanner.Select(s => s.EducationPostId).ToList()
                });
            }
            return bannerViewModels;
        }
    }
}
