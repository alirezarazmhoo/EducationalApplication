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
	public class ClassRoomRepo : RepositoryBase<ClassRoom> , IClassRoomRepo
	{
		public ClassRoomRepo(ApplicationDbContext DbContext)
         : base(DbContext)
		{
			_DbContext = DbContext;
		}

		public async Task<string> GetName(int ClassId)
		{
			var Item = await _DbContext.ClassRooms.Where(s => s.Id == ClassId).FirstOrDefaultAsync();
			if (Item != null)
			{
				return Item.Name;
			}
			else
			{
				return string.Empty;
			}
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
			return await FindByCondition(s => s.Name.Contains(txtsearch)).Include(s => s.Grade).
			  Include(s => s.Major).Include(s => s.TeachersToClassRoom).Include(s => s.Students)
			.ToListAsync();
		}
		public async Task  Remove(ClassRoom model)
		{
			var Teachers =await _DbContext.TeachersToClassRooms.Where(s => s.ClassRoomId == model.Id).ToListAsync();
			var Student = await _DbContext.Students.Where(s => s.ClassRoomId == model.Id).ToListAsync();
			if(Teachers.Count > 0)
			{

			_DbContext.TeachersToClassRooms.RemoveRange(Teachers);
			}
			if(Student.Count > 0)
			{
	   	     await _DbContext.Students.ForEachAsync(s => s.ClassRoomId = null);
			}
			Delete(model);

		}
		public async Task<bool> CheckCode(int code)
		{
	     return await _DbContext.ClassRooms.AnyAsync(s => s.Code == code);
		}
		public async Task<IEnumerable<Students>> GetAvalibleStudents(string txtSearch , int Id)
		{
			if (!string.IsNullOrEmpty(txtSearch))
			{
			return await _DbContext.Students.Where(s => s.ClassRoomId.HasValue == false || s.ClassRoomId == Id).Where(s=>s.FullName.Contains(txtSearch)).Include(s => s.Major).Include(s => s.Grade).OrderByDescending(s=>s.Id).ToListAsync();
			}
			else
			{
			return await _DbContext.Students.Where(s => s.ClassRoomId.HasValue == false || s.ClassRoomId == Id).Include(s=>s.Major).Include(s=>s.Grade).OrderByDescending(s=>s.Id).ToListAsync();
			}
		}

		public async Task RemovePerson(string Id, int Mode , int? ClassId)
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
			if(Mode ==2)
			{
				ApplicationUser TeacherItem =await _DbContext.Users.FirstOrDefaultAsync(s=>s.Id.Equals(Id));
				TeachersToClassRoom Item =await _DbContext.TeachersToClassRooms.FirstOrDefaultAsync(s=>s.ApplicationUserId == TeacherItem.Id && s.ClassRoomId == ClassId);
				if(Item != null && TeacherItem !=null)
				{
					_DbContext.TeachersToClassRooms.Remove(Item);
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
			if (Mode == 2)
				{
					TeachersToClassRoom teachersToClass = new TeachersToClassRoom();
					teachersToClass.ClassRoomId = ClassId;
					teachersToClass.ApplicationUserId = Id;
					_DbContext.TeachersToClassRooms.Add(teachersToClass); 
				}
            }

		public async Task<IEnumerable<TeacherToClassRoomViewModel>> GetAllTeachers(int? ClassId , string txtSearch)
		{
			List<TeacherToClassRoomViewModel> teacherToClass = new List<TeacherToClassRoomViewModel>();
			List<TeacherToClassRoomViewModel> MainList = new List<TeacherToClassRoomViewModel>();
			List<TeachersToClassRoom> teachersToClassRooms =await _DbContext.TeachersToClassRooms.Where(s=>s.ClassRoomId == ClassId.Value).ToListAsync();
			List<ApplicationUser> AllTeachers = await _DbContext.Users.Where(s => s.UserType == Models.Enums.UserType.Teacher).ToListAsync();
			IEnumerable<TeachersToClassRoom> Teachers = await _DbContext.TeachersToClassRooms.Include(s=>s.ApplicationUser).Where(s => s.ApplicationUser.UserType == Models.Enums.UserType.Teacher).ToListAsync();
			foreach (var item in teachersToClassRooms)
			{
				
					teacherToClass.Add(new TeacherToClassRoomViewModel() { Teacher = item.ApplicationUser, ClassId = item.ClassRoomId ,IsInClass = true });
				
			}
			foreach (var item in AllTeachers)
			{
				if(!teacherToClass.Any(s=>s.Teacher.Id == item.Id))
				{
					
					teacherToClass.Add(new TeacherToClassRoomViewModel() { Teacher = item, ClassId = 0, IsInClass = false });
				}
			}
			if (!string.IsNullOrEmpty(txtSearch))
			{
			return teacherToClass.Where(s=>s.Teacher.FullName.Contains(txtSearch)).OrderByDescending(s=>s.Teacher.CreateDate); 
			}
			else
			{
			return teacherToClass.OrderByDescending(s => s.Teacher.CreateDate); 
			}

		}
		public async Task<AllPersonsClassRoomViewModel> GetAllPersons(int? ClassId , string txtSearch)
		{
			AllPersonsClassRoomViewModel MainList = new AllPersonsClassRoomViewModel();
			List<TeacherToClassRoomViewModel> TeacherToClassRoom = new List<TeacherToClassRoomViewModel>();
			var Teachers =await _DbContext.TeachersToClassRooms.Include(s=>s.ApplicationUser)
				.Where(s => s.ClassRoomId == ClassId && s.ApplicationUser.UserType
				== Models.Enums.UserType.Teacher).ToListAsync();
			var Students = await _DbContext.Students.
				Include(s=>s.Major).Include(s=>s.Grade)
				.Where(s => s.ClassRoomId == ClassId).ToListAsync();
			foreach (var item in Teachers)
			{
				TeacherToClassRoom.Add(new TeacherToClassRoomViewModel() {  Teacher = item.ApplicationUser});
			}

			MainList.Students = Students;
			MainList.Teachers = TeacherToClassRoom; 

			return MainList; 
		}
}
}
