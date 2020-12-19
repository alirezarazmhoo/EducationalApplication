using EducationalApplication.Data;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
using EducationalApplication.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EducationalApplication.Models.Enums;

namespace EducationalApplication.Services
{
	public class EducationPostRepo : RepositoryBase<EducationPost>, IEducationPostRepo
	{
        public EducationPostRepo(ApplicationDbContext DbContext)
        : base(DbContext)
		{
			_DbContext = DbContext;
		}
		public async Task AddOrUpdate(EducationPost model , IFormFile Icon, IFormFile[] files)
		{
            if (model.Id == 0)
            {
                if (Icon != null && Icon.Length > 1)
                {
                    var fileName = Guid.NewGuid().ToString().Replace('-', '0') + "." + Icon.FileName.Split('.')[1];
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Post\Icon", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        Icon.CopyTo(fileStream);
                    }
                    model.IconUrl = "/Upload/Post/Icon/" + fileName;
                }
                _DbContext.EducationPosts.Add(model);
                _DbContext.SaveChanges();
                if (files != null && files.Count() > 0)
                {
                    foreach (var item in files)
                    {
                        var fileName = Guid.NewGuid().ToString().Replace('-', '0') + "." + item.FileName.Split('.')[1];
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Post\Media\", fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            item.CopyTo(stream);
                        }
                        _DbContext.Medias.Add(new Media()
                        {
                            Title = Path.GetFileName(item.FileName),
                            Url = "/Upload/Post/Media/" + fileName,
                            EducationPostId = model.Id,
                            lenght = item.Length > 0 ? MediaSize.SizeSuffix(item.Length) : "0",
                            DurationTime = FormatCheck.GetFormat(item) == MediaType.Audio ? DurationFile.GetDuration(filePath).ToString() : "0",
                            MediaType = FormatCheck.GetFormat(item)
                        });
                    }
                }
              
            }
            else
            {
                    var MainItem = await GetById(model.Id);
                if (Icon != null && Icon.Length > 1)
                {
                    if (!string.IsNullOrEmpty(MainItem.IconUrl))
                    {
                        File.Delete($"wwwroot/{MainItem.IconUrl}");
                    }

                    var fileName = Guid.NewGuid().ToString().Replace('-', '0') + "." + Icon.FileName.Split('.')[1];
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Post\Icon", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        Icon.CopyTo(fileStream);
                    }
                    MainItem.IconUrl = "/Upload/Post/Icon/" + fileName;
                }
                if (files != null && files.Count() > 0)
                {
                    foreach (var item in files)
                    {
                        var fileName = Guid.NewGuid().ToString().Replace('-', '0') + "." + item.FileName.Split('.')[1];
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Post\Media\", fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            item.CopyTo(stream);
                        }
                        _DbContext.Medias.Add(new Media()
                        {
                            Title = Path.GetFileName(item.FileName),
                            Url = "/Upload/Post/Media/" + fileName,
                            EducationPostId = model.Id,
                            lenght = item.Length > 0 ? MediaSize.SizeSuffix(item.Length) : "0",
                            DurationTime = FormatCheck.GetFormat(item) == MediaType.Audio ? DurationFile.GetDuration(filePath).ToString() : "0",
                            MediaType = FormatCheck.GetFormat(item)
                        });
                    }
                }
                MainItem.AccessType = model.AccessType;
                MainItem.category = model.category;
                MainItem.Description = model.Description;
         
                MainItem.Number = model.Number;
                MainItem.Title = model.Title;
                //Update(model);
            }
        }
        public async Task<IEnumerable<EducationPost>> GetAll(string Id)
        {
            var Items =await _DbContext.EducationPosts.Include(s=>s.Medias)/*.Where(s=>s.ApplicationUserId == Id)*/.OrderByDescending(s=>s.Id).ToListAsync();
            return Items; 
        }
        public async Task<EducationPost>  GetById(int Id)
        {
            var Items = await _DbContext.EducationPosts.Include(s=>s.Medias).Where(s => s.Id == Id).FirstOrDefaultAsync();
            return Items;
        }

        public async Task Remove(EducationPost model)
        {
            var MainItem =await GetById(model.Id);

            if (!string.IsNullOrEmpty(MainItem.IconUrl))
            {
                File.Delete($"wwwroot/{MainItem.IconUrl}");
            }

            if (MainItem.Medias.Count > 0)
            {
                foreach (var item in MainItem.Medias)
                {
                    File.Delete($"wwwroot/{item.Url}");
                }
            }
            Delete(MainItem);
        }

        public async Task RemoveFile(int Id)
        {
            Media Item =await _DbContext.Medias.FindAsync(Id);
            if(Item != null)
            {       
            File.Delete($"wwwroot/{Item.Url}");
            }
       
            _DbContext.Medias.Remove(Item);
        }
    }
}
