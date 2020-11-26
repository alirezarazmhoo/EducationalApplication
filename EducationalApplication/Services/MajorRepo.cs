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
	public class MajorRepo: RepositoryBase<Major>, IMajorRepo
	{
		public MajorRepo(ApplicationDbContext DbContext)
        : base(DbContext)
		{
			_DbContext = DbContext;
		}
		public async Task<IEnumerable<Major>> GetAll()
		{
			return await FindAll(null)
			  .OrderByDescending(s => s.Id)
			  .ToListAsync();
		}
		public async Task<Major> GetById(int Id)
		{
			return await FindByCondition(s => s.Id.Equals(Id))
				.FirstOrDefaultAsync();
		}
		public void CreateOrUpdate(Major model)
		{
			if (model.Id == 0)
			{
				_DbContext.Majors.Add(model);
			}
			else
			{
				Update(model);
			}
		}
		public async Task<IEnumerable<Major>> search(string txtsearch)
		{
			return await FindByCondition(s => s.Name.Contains(txtsearch))
			.ToListAsync();
		}
		public void Remove(Major model) => Delete(model);
	}
}
