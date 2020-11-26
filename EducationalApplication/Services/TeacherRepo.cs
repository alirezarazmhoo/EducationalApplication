using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Data;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EducationalApplication.Services
{
	public class TeacherRepo : RepositoryBase<Teacher>, ITeacherRepo
	{
	    public TeacherRepo(ApplicationDbContext DbContext)
	   : base(DbContext)
		{
			_DbContext = DbContext;
		}
		public async Task<IEnumerable<Teacher>> GetAll()
		{
			return await FindAll(null)
		  .OrderByDescending(s => s.Id)
		  .ToListAsync();
		}
		public async Task AddOrUpdate(Teacher model, IFormFile _File)
		{
			Random random = new Random();
			if(model.Id == 0)
			{
			if (_File != null)
			{
				var fileName = Path.GetFileName(_File.FileName);
				var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Teacher\File", fileName);
				using (var stream = new FileStream(filePath, FileMode.Create))
				{
						_File.CopyTo(stream);
				}
				model.Url ="/Upload/Teacher/File/"+ fileName ;
			}
			model.UserName = model.Mobile.ToString(); 
			model.Password = random.Next(10000, 99999).ToString(); 
			Create(model);
			}
			else
			{
			
				var item =await  _DbContext.Teachers.FindAsync(model.Id);
				if (_File != null)
				{
					if(item != null)
					{
					if (!string.IsNullOrEmpty(item.Url))
					{
						File.Delete($"wwwroot/{item.Url}");
					}
					}
					var fileName = Path.GetFileName(_File.FileName);
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Teacher\File", fileName);
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						_File.CopyTo(stream);
					}
					item.Url = "/Upload/Teacher/File/" + fileName;
				}
				if (item != null)
				{
					item.Mobile = model.Mobile;
					item.NationalCode = model.NationalCode;
					item.FullName = model.FullName;
					item.Address = model.Address;
				}

			}
		}
		public async Task<Teacher>  GetById(int Id)
		{
			return await FindByCondition(s => s.Id.Equals(Id))
				.FirstOrDefaultAsync();
		
		}
		public IQueryable<Teacher> Authorize(Teacher model)
		{
			var item = FindByCondition(s=>s.UserName == model.UserName && s.Password == model.Password);
			return item;
		}
		public  void Remove(Teacher model)
		{
			if (!string.IsNullOrEmpty(model.Url))
			{
	    	File.Delete($"wwwroot/{model.Url}");
			}
			Delete(model);
		}
		public async Task<IEnumerable<Teacher>> search(string txtsearch)
		{
			return await FindByCondition(s => s.FullName.Contains(txtsearch))
			.ToListAsync();
		}
	}
}
