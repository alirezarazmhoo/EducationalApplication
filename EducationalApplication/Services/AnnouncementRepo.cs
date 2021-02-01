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
	public class AnnouncementRepo : RepositoryBase<Announcements>, IAnnouncementRepo
	{
		public AnnouncementRepo(ApplicationDbContext DbContext)
	   : base(DbContext)
		{
			_DbContext = DbContext;
		}
		public async Task<List<Announcements>> GetAll(string Id)
		{
			return await FindByCondition(s=>s.ApplicationUserId == Id)
		  .OrderByDescending(s => s.Id )
		  .ToListAsync();
		}

		public async Task<Announcements> GetById(int Id)
		{
			return await FindByCondition(s=>s.Id.Equals(Id)).FirstOrDefaultAsync(); 
		}
		public async Task AddOrUpdate(Announcements model)
		{
			if(model.Id == 0)
			{
			await _DbContext.Announcements.AddAsync(model);
			}
			else
			{
				Update(model);
			}
		}
		public async Task<List<Announcements>> GetForStudent(int Id)
		{
			Students studentItem = await _DbContext.Students.FirstOrDefaultAsync(s => s.Id.Equals(Id));
			ClassRoom classRoomItem = new ClassRoom();
			List<ApplicationUser> TeacherList = new List<ApplicationUser>();
			List<TeachersToClassRoom> teachersToClassRooms = new List<TeachersToClassRoom>();
			List<Announcements> announcements = new List<Announcements>();
 			if(studentItem != null)
			{
				classRoomItem = await _DbContext.ClassRooms.FirstOrDefaultAsync(s=>s.Id.Equals(studentItem.ClassRoomId));
				teachersToClassRooms = await _DbContext.TeachersToClassRooms.Where(s => s.ClassRoomId == classRoomItem.Id).ToListAsync();
				foreach (var item in teachersToClassRooms)
				{
					TeacherList.Add(await _DbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(item.ApplicationUserId)));
				}
				foreach (var item in TeacherList)
				{
					announcements.AddRange(await _DbContext.Announcements.Where(s => s.ApplicationUserId.Equals(item.Id)).ToListAsync());
				}
				return announcements;
			}
			return null; 

		}
		public async Task Remove(Announcements model)
		{
			Announcements announcementsItem =await _DbContext.Announcements.FirstOrDefaultAsync(s => s.Id.Equals(model.Id));
			if(announcementsItem != null)
			{
			_DbContext.Announcements.Remove(announcementsItem);
			}
		}
	
	}
}
