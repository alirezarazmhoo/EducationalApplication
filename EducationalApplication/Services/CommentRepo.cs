using EducationalApplication.Data;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
			return await FindByCondition(s=>s.EducationPostId == Id)
			  .OrderByDescending(s => s.Id)
			  .ToListAsync();
		}
		public async Task<Comment> GetById(int Id)
		{
			return await FindByCondition(s => s.Id.Equals(Id)).FirstOrDefaultAsync();
		}
		public async Task Create(Comment model)
		{
	      await _DbContext.Comments.AddAsync(model);
		}
		public async Task Remove(Comment model)
		{
			var Item = await _DbContext.Comments.FirstOrDefaultAsync(s => s.Id == model.Id);
			if(Item != null)
			{
				_DbContext.Comments.Remove(Item);
			}
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
