using EducationalApplication.Data;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
using EducationalApplication.Models.Enums;
using EducationalApplication.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Services
{
	public class TicketRepo : RepositoryBase<Ticket>, ITiketRepo
	{
		public TicketRepo(ApplicationDbContext DbContext)
	    : base(DbContext)
		{
			_DbContext = DbContext;
		}
		public async Task<IEnumerable<Ticket>> TeacherGetAll(string Id)
		{
			return await FindByCondition(s => s.TeacherReceiver.Equals(Id)).Include(s => s.Medias)
			  .OrderByDescending(s => s.Id)
			  .ToListAsync();
		}
		public async Task<IEnumerable<Ticket>> StudentGetAll(int Id)
		{
			return await FindByCondition(s => s.StudentReceiver.Equals(Id)).Include(s => s.Medias)
			  .OrderByDescending(s => s.Id)
			  .ToListAsync();
		}
		public async Task<Ticket> GetById(int Id)
		{
			return await FindByCondition(s => s.Id.Equals(Id)).FirstOrDefaultAsync();
		}
		public async Task Create(Ticket model, IFormFile[] files)
		{
			await _DbContext.Tikets.AddAsync(model);
			await _DbContext.SaveChangesAsync();
			if (files != null && files.Count() > 0)
			{
				foreach (var item in files)
				{
					var fileName = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(item.FileName).ToLower();
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Tiket\File\", fileName);
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						item.CopyTo(stream);
					}
					_DbContext.TiketMedias.Add(new TicketMedia()
					{
						Url = "/Upload/Tiket/File/" + fileName,
						TiketId = model.Id,
						lenght = item.Length > 0 ? MediaSize.SizeSuffix(item.Length) : "0",
						DurationTime = FormatCheck.GetFormat(item) == MediaType.Audio ? DurationFile.GetDuration(filePath).ToString() : "0",
						MediaType = FormatCheck.GetFormat(item)
					});

				}
			}
		}
		public async Task Remove(Ticket model)
		{
			var Item = await _DbContext.Tikets.Include(s => s.Medias).FirstOrDefaultAsync(s => s.Id == model.Id);
			if (Item != null)
			{
				_DbContext.Tikets.Remove(Item);
			}
			if (Item.Medias.Count > 0)
			{
				foreach (var item in Item.Medias)
				{
					File.Delete($"wwwroot/{item.Url}");
				}
			}
			_DbContext.TiketMedias.RemoveRange(await _DbContext.TiketMedias.Where(s => s.TiketId == model.Id).ToListAsync());
		}
	}
}
