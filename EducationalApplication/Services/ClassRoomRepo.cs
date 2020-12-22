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
			  Include(s=>s.Major).Include(s=>s.TeachersToClassRoom).Include(s=>s.Students)
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
		public async Task<bool> CheckCode(int code)
		{
	     return await _DbContext.ClassRooms.AnyAsync(s => s.Code == code);
		}
		public async Task<IEnumerable<Students>> GetAvalibleStudents(string txtSearch , int Id)
		{
			if (!string.IsNullOrEmpty(txtSearch))
			{
			return await _DbContext.Students.Where(s => s.ClassRoomId.HasValue == false || s.ClassRoomId == Id).Where(s=>s.FullName.Contains(txtSearch)).Include(s => s.Major).Include(s => s.Grade).ToListAsync();
			}
			else
			{
			return await _DbContext.Students.Where(s => s.ClassRoomId.HasValue == false || s.ClassRoomId == Id).Include(s=>s.Major).Include(s=>s.Grade).ToListAsync();
			}
		}

		public async Task RemovePerson(string Id, int Mode)
		{
	        int n;
			if(Mode == 1)
			{
				if(int.TryParse(Id, out n))
				{
					int StudentId = int.Parse(Id);
					Students Item =await _DbContext.Students.FirstOrDefaultAsync(s => s.Id == StudentId);
					if(Item != null)
					{
						Item.ClassRoomId = null;
					}
				}
			}
		}

		public async Task AddPerson(string Id, int Mode , int ClassId)
		{
			int n;
			if (Mode == 1)
			{
				if (int.TryParse(Id, out n))
				{
					int StudentId = int.Parse(Id);
					Students Item = await _DbContext.Students.FirstOrDefaultAsync(s => s.Id == StudentId);
					if (Item != null)
					{
						Item.ClassRoomId = ClassId;
					}
			}
	}
}
}
}
