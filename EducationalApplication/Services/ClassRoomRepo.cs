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
	public class ClassRoomRepo : RepositoryBase<ClassRoom> , IClassRoomRepo
	{
		public ClassRoomRepo(ApplicationDbContext DbContext)
         : base(DbContext)
		{
			_DbContext = DbContext;
		}
		public async Task <IEnumerable<ClassRoom>> GetAll()
		{
			return await FindAll(null)
			  .OrderByDescending(s => s.Id).Include(s=>s.Grade).
			  Include(s=>s.Major).Include(s=>s.TeachersToClassRoom)
			  .ToListAsync();
		}
		public async Task<ClassRoom> GetById(int Id)
		{
			return await FindByCondition(s => s.Id.Equals(Id))
				.FirstOrDefaultAsync();
		}

		public async Task CreateOrUpdate(ClassRoom model)
		{
			if (model.Id == 0)
			{
			await	_DbContext.ClassRooms.AddAsync(model);
			}
			else
			{
				Update(model);
			}
		}

		public async Task<IEnumerable<ClassRoom>> search(string txtsearch)
		{
			return await FindByCondition(s => s.Name.Contains(txtsearch))
			.ToListAsync();
		}
		public void  Remove(ClassRoom model) => Delete(model);


	}
}
