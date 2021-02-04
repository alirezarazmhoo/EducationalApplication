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
			return await FindByCondition(s=>s.ApplicationUserId == Id && s.ExpireDate >= DateTime.Now )
		  .OrderByDescending(s => s.Id )
		  .ToListAsync();
		}

		public async Task<Announcements> GetById(int Id)
		{
			return await FindByCondition(s=>s.Id.Equals(Id)).FirstOrDefaultAsync(); 
		}
		public async Task AddOrUpdate(Announcements model)
		{
			var SettingItem = await _DbContext.Settings.FirstOrDefaultAsync();
			Announcements announcements = new Announcements();
			if (model.Id == 0)
			{
				announcements.Text = model.Text;
				announcements.ApplicationUserId = model.ApplicationUserId;

				announcements.AvailableDays = model.AvailableDays == 0 ? SettingItem.Announcement : model.AvailableDays;

				announcements.ExpireDate = DateTime.Now.AddDays(announcements.AvailableDays);
				await _DbContext.Announcements.AddAsync(announcements);
			}
			else
			{
				announcements =await _DbContext.Announcements.FirstOrDefaultAsync(s => s.Id == model.Id);
				announcements.AvailableDays = model.AvailableDays == 0 ? SettingItem.Announcement : model.AvailableDays;
				announcements.Text = model.Text; 
				//announcements.ExpireDate = DateTime.Now.AddDays(announcements.AvailableDays);
			}
		}
	
		public async Task Remove(Announcements model)
		{
			Announcements announcementsItem =await _DbContext.Announcements.FirstOrDefaultAsync(s => s.Id.Equals(model.Id));
			if(announcementsItem != null)
			{
			_DbContext.Announcements.Remove(announcementsItem);
			}
		}
		public async Task<List<Announcements>> GetAllForMainPage(string Id)
		{
			ClassRoom classRoomItem = new ClassRoom();
			List<ApplicationUser> TeacherList = new List<ApplicationUser>();
			List<TeachersToClassRoom> teachersToClassRooms = new List<TeachersToClassRoom>();
			List<Announcements> announcements = new List<Announcements>();
			Students studentItem = new Students();
			ApplicationUser ApplicationUserItem = new ApplicationUser();
			int _StudentId = 0;
			if (int.TryParse(Id, out int n))
			{
				_StudentId = Convert.ToInt32(Id);
				studentItem = await _DbContext.Students.FirstOrDefaultAsync(s => s.Id.Equals(_StudentId));
				classRoomItem = await _DbContext.ClassRooms.FirstOrDefaultAsync(s => s.Id.Equals(studentItem.ClassRoomId));
				teachersToClassRooms = await _DbContext.TeachersToClassRooms.Where(s => s.ClassRoomId == classRoomItem.Id).ToListAsync();
				foreach (var item in teachersToClassRooms)
				{
					TeacherList.Add(await _DbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(item.ApplicationUserId)));
				}
				foreach (var item in TeacherList)
				{
					announcements.AddRange(await _DbContext.Announcements.Where(s => s.ApplicationUserId.Equals(item.Id) &&  s.ExpireDate >= DateTime.Now).ToListAsync());
				}
			}
			else
			{
				ApplicationUserItem = await _DbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(Id));
				if (ApplicationUserItem != null)
				{
					if (ApplicationUserItem.UserType == Models.Enums.UserType.Teacher)
					{
						announcements.AddRange(await _DbContext.Announcements.Where(s => s.ApplicationUserId.Equals(ApplicationUserItem.Id) || s.IsOnlyForTeacher &&  s.ExpireDate >= DateTime.Now).ToListAsync());
					}
					else
					{
						announcements.AddRange(await _DbContext.Announcements.Where(s=> s.ExpireDate >= DateTime.Now).ToListAsync());
					}
				}
			}
			return announcements;
		}

	}
}
