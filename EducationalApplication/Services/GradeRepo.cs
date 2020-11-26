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
	public class GradeRepo : RepositoryBase<Grade>, IGradeRepo
	{
		public GradeRepo(ApplicationDbContext DbContext)
       : base(DbContext)
		{
			_DbContext = DbContext;
		}
		public async Task<IEnumerable<Grade>> GetAll()
		{
			return await FindAll(null)
			  .OrderByDescending(s => s.Id)
			  .ToListAsync();
		}
		public async Task<Grade> GetById(int Id)
		{
			return await FindByCondition(s => s.Id.Equals(Id))
				.FirstOrDefaultAsync();
		}

		public void CreateOrUpdate(Grade model)
		{
			if (model.Id == 0)
			{
				_DbContext.Grades.Add(model);
			}
			else
			{
				Update(model);
			}
		}

		public async Task<IEnumerable<Grade>> search(string txtsearch)
		{
			return await FindByCondition(s => s.Name.Contains(txtsearch))
			.ToListAsync();
		}
		public  void Remove(Grade model) => Delete(model);

	}
}
