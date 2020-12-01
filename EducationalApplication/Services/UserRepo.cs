using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Data;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
using EducationalApplication.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EducationalApplication.Services
{
	public class UserRepo : RepositoryBase<ApplicationUser>, IUserRepo
	{
		
		public UserRepo(ApplicationDbContext DbContext  )
	   : base(DbContext)
		{
			_DbContext = DbContext;
	
		}
		public async Task<IEnumerable<ApplicationUser>> GetAll()
		{
			return await FindAll(null)
		  .OrderByDescending(s => s.Id)
		  .ToListAsync();
		}
		public async Task AddOrUpdate(ApplicationUser model, IFormFile _File)
		{
			Random random = new Random();
			//if(string.IsNullOrEmpty(model.Id))
			//{
			try
			{
				if (_File != null)
				{
					var fileName = Path.GetFileName(_File.FileName);
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Teacher\File", fileName);
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						_File.CopyTo(stream);
					}
					model.Url = "/Upload/Teacher/File/" + fileName;
				}
				model.UserName = model.Mobile.ToString();
				string UserPass = random.Next(10000, 99999).ToString();
				var user = new ApplicationUser { UserName = model.Mobile.ToString(), Email = model.Mobile + "yahoo.com", Password = UserPass, FullName = model.FullName, Address = model.Address, Mobile = model.Mobile, NationalCode = model.NationalCode, Url = model.Url, UserType = UserType.Teacher, PhoneNumber = "0" };
				//var result = await _userManager.CreateAsync(user, UserPass);
				Create(user);
			}
			catch(Exception ex)
			{

			}
			//}
			//else
			//{

				//var item = await _DbContext.Users.FindAsync(model.Id);
				//if (_File != null)
				//{
				//	if(item != null)
				//	{
				//	if (!string.IsNullOrEmpty(item.Url))
				//	{
				//		File.Delete($"wwwroot/{item.Url}");
				//	}
				//	}
				//	var fileName = Path.GetFileName(_File.FileName);
				//	var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Teacher\File", fileName);
				//	using (var stream = new FileStream(filePath, FileMode.Create))
				//	{
				//		_File.CopyTo(stream);
				//	}
				//	item.Url = "/Upload/Teacher/File/" + fileName;
				//}
				//if (item != null)
				//{
				//	item.Mobile = model.Mobile;
				//	item.NationalCode = model.NationalCode;
				//	item.FullName = model.FullName;
				//	item.Address = model.Address;
				//}

			//}
		}
		public async Task<ApplicationUser>  GetById(string Id)
		{
			return await FindByCondition(s => s.Id.Equals(Id))
				.FirstOrDefaultAsync();
		
		}
		public IQueryable<ApplicationUser> Authorize(ApplicationUser model)
		{
			var item = FindByCondition(s=>s.UserName == model.UserName && s.Password == model.Password);
			return item;
		}
		public  void Remove(ApplicationUser model)
		{
			if (!string.IsNullOrEmpty(model.Url))
			{
	    	File.Delete($"wwwroot/{model.Url}");
			}
			Delete(model);
		}
		public async Task<IEnumerable<ApplicationUser>> search(string txtsearch)
		{
			return await FindByCondition(s => s.FullName.Contains(txtsearch))
			.ToListAsync();
		}
	}
}
