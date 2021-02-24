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
	public class CommentRepo : RepositoryBase<Comment>, ICommentRepo
	{
       public CommentRepo(ApplicationDbContext DbContext)
	   : base(DbContext)
		{
			_DbContext = DbContext;
		}
		public async Task<IEnumerable<Comment>> GetAll(int Id)
		{
			return await FindByCondition(s=>s.EducationPostId == Id).Include(s=>s.Students)
			  .OrderByDescending(s => s.Id)
			  .ToListAsync();
		}
		public async Task<Comment> GetById(int Id)
		{
			return await FindByCondition(s => s.Id.Equals(Id)).FirstOrDefaultAsync();
		}
		public async Task Create(Comment model , IFormFile[] files)
		{
			await _DbContext.Comments.AddAsync(model);
			await _DbContext.SaveChangesAsync();
			if (files != null && files.Count() > 0)
			{
				foreach (var item in files)
				{
					var fileName = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(item.FileName).ToLower();
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Comment\File\", fileName);
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						item.CopyTo(stream);
					}
					_DbContext.CommentMedias.Add(new CommentMedia()
					{
						Url = "/Upload/Comment/File/" + fileName,
						CommentId = model.Id,
						lenght = item.Length > 0 ? MediaSize.SizeSuffix(item.Length) : "0",
						DurationTime = FormatCheck.GetFormat(item) == MediaType.Audio ? DurationFile.GetDuration(filePath).ToString() : "0",
						MediaType = FormatCheck.GetFormat(item)
					});
				}
			}
		}
		public async Task Remove(Comment model)
		{
			var Item = await _DbContext.Comments.Include(s=>s.CommentMedias).FirstOrDefaultAsync(s => s.Id == model.Id);
			if(Item != null)
			{
				_DbContext.Comments.Remove(Item);
			}
			if (Item.CommentMedias.Count > 0)
			{
				foreach (var item in Item.CommentMedias)
				{
					File.Delete($"wwwroot/{item.Url}");
				}
			}
			_DbContext.CommentMedias.RemoveRange(await _DbContext.CommentMedias.Where(s=>s.CommentId == model.Id).ToListAsync());
			_DbContext.Comments.RemoveRange(await _DbContext.Comments.Where(s => s.IdParent == Item.Id).ToListAsync());
		}

		public async Task ChaneStatus(Comment model)
		{
			var Item =await _DbContext.Comments.FirstOrDefaultAsync(s=>s.Id == model.Id);
			if(Item != null)
			{
				if(model.CommentStatus == Models.Enums.CommentStatus.Accepted)
				{
					Item.CommentStatus = Models.Enums.CommentStatus.Accepted; 
				}
				else
				{
					Item.CommentStatus = Models.Enums.CommentStatus.Rejected;

				}
			}
		}

	}
}
