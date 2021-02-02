using EducationalApplication.Data;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
using EducationalApplication.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Services
{
	public class FavoritRepo : RepositoryBase<FavoritRepo>, IFavoritRepo
	{
		public FavoritRepo(ApplicationDbContext DbContext)
		: base(DbContext)
		{
			_DbContext = DbContext;
		}
		public async Task Create(Favorit model)
		{

			if (!await _DbContext.Favorits.AnyAsync(s => s.ApplicationUserId == model.ApplicationUserId && s.EducationPostId == model.EducationPostId))
			{
				await _DbContext.Favorits.AddAsync(model);
			}

		}
		public void Remove(Favorit model)
		{
			_DbContext.Favorits.Remove(model);
		}

		public async Task<IEnumerable<Favorit>> GetAll(string UserId)
		{
			int n;
			int _studentId = 0;
			if (int.TryParse(UserId, out n))
			{
				_studentId = Convert.ToInt32(UserId);
				if (await _DbContext.Students.FirstOrDefaultAsync(s => s.Id.Equals(_studentId)) != null)
				{
					return await _DbContext.Favorits.Where(s => s.StudentsId == _studentId).Include(s => s.EducationPost).Include(s => s.Banner).ToListAsync();
				}
				else
				{
					return null;
				}
			}
			else
			{
				if (await _DbContext.Users.FirstOrDefaultAsync(s => s.Id == UserId) != null)
				{
					return await _DbContext.Favorits.Where(s => s.ApplicationUserId.Equals(UserId)).ToListAsync();
				}
				else
				{
					return null;
				}
			}
		}
	}
}
