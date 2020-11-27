﻿using EducationalApplication.Data;
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
	public class StudentRepo : RepositoryBase<Students>, IStudentRepo
	{
		public StudentRepo(ApplicationDbContext DbContext)
	   : base(DbContext)
		{
			_DbContext = DbContext;
		}
		public async Task<IEnumerable<Students>> GetAll()
		{
			return await FindAll(null).Include(s=>s.Grade).Include(s=>s.Major)
		  .OrderByDescending(s => s.Id)
		  .ToListAsync();
		}
		public async Task AddOrUpdate(Students model, IFormFile _File)
		{
			Random random = new Random();
			if (model.Id == 0)
			{
				if (_File != null)
				{
					var fileName = Path.GetFileName(_File.FileName);
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Student\File", fileName);
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						_File.CopyTo(stream);
					}
					model.Url = "/Upload/Student/File/" + fileName;
				}
				model.UserName = model.Mobile.ToString();
				model.Password = random.Next(10000, 99999).ToString();
				Create(model);
			}
			else
			{

				var item = await _DbContext.Students.FindAsync(model.Id);
				if (_File != null)
				{
					if (item != null)
					{
						if (!string.IsNullOrEmpty(item.Url))
						{
							File.Delete($"wwwroot/{item.Url}");
						}
					}
					var fileName = Path.GetFileName(_File.FileName);
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Student\File", fileName);
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						_File.CopyTo(stream);
					}
					item.Url = "/Upload/Student/File/" + fileName;
				}
				if (item != null)
				{
					item.Mobile = model.Mobile;
					item.MajorId = model.MajorId;
					item.GradeId = model.GradeId;
					item.NationalCode = model.NationalCode;
					item.FullName = model.FullName;
					item.Address = model.Address;
				}

			}
		}
		public async Task<Students> GetById(int Id)
		{
			return await FindByCondition(s => s.Id.Equals(Id))
				.FirstOrDefaultAsync();

		}
		public IQueryable<Students> Authorize(Students model)
		{
			var item = FindByCondition(s => s.UserName == model.UserName && s.Password == model.Password);
			return item;
		}
		public void Remove(Students model)
		{
			if (!string.IsNullOrEmpty(model.Url))
			{
				File.Delete($"wwwroot/{model.Url}");
			}
			Delete(model);
		}
		public async Task<IEnumerable<Students>> search(string txtsearch)
		{
			return await FindByCondition(s => s.FullName.Contains(txtsearch)).Include(s => s.Grade).Include(s => s.Major)
			.ToListAsync();
		}
	}
}