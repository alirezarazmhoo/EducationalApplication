using EducationalApplication.Data;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Services
{
	public class CategoryRepo : RepositoryBase<Category>, ICategoryRepo
	{
		public CategoryRepo(ApplicationDbContext DbContext)
       : base(DbContext)
		{
			_DbContext = DbContext;
		}
		public async Task<IEnumerable<Category>> GetAll(string Id)
		{
		return await FindAll(null).Where(s=>s.ApplicationUserId == Id)
		  .OrderByDescending(s => s.Id)
		  .ToListAsync();
		}
		public async Task<Category> GetById(int Id)
		{
		  return await FindByCondition(s => s.Id.Equals(Id))
          .FirstOrDefaultAsync();
		}
		public async Task CreateOrUpdate(Category model, IFormFile _File)
		{
			if (model.Id == 0)
			{
				if (_File != null)
				{
					
					var fileName = Guid.NewGuid().ToString().Replace('-', '0') + "." + _File.FileName.Split('.')[1];
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Category\File", fileName);
					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						_File.CopyTo(fileStream);
					}
					model.Url = "/Upload/Category/File/" + fileName;
				}
				_DbContext.Categories.Add(model);
			}
			else
			{
				var getCategory = await GetById(model.Id);
				if(getCategory != null)
				{
					if (_File != null)
					{
						if (!string.IsNullOrEmpty(getCategory.Url))
						{
							File.Delete($"wwwroot/{getCategory.Url}");
						}
						var fileName = Guid.NewGuid().ToString().Replace('-', '0') + "." + _File.FileName.Split('.')[1];
						var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Category\File", fileName);
						using (var fileStream = new FileStream(filePath, FileMode.Create))
						{
							_File.CopyTo(fileStream);
						}
						getCategory.Url = "/Upload/Category/File/" + fileName;
					}
				    model.Name = model.Name;
					model.Url = getCategory.Url; 
					
				Update(model);
				}
			}
		}
		public void Remove(Category model)
		{
			if (!string.IsNullOrEmpty(model.Url))
			{
				File.Delete($"wwwroot/{model.Url}");
			}
			Delete(model);
		}
		public async Task RemoveFile(int Id)
		{
			Category Item = await _DbContext.Categories.FindAsync(Id);
			if (Item != null)
			{
				File.Delete($"wwwroot/{Item.Url}");
			}

		}
	    public async Task<IEnumerable<Category>> search(string txtsearch, string UserId)
		{
	   return await FindByCondition(s => s.Name.Contains(txtsearch) && s.ApplicationUserId.Equals(UserId))
		.ToListAsync();
		}
	}
}
