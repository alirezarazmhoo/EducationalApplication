using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Data;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
using EducationalApplication.Models.Enums;
using EducationalApplication.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Utility;

namespace EducationalApplication.Services
{
	public class UserRepo : RepositoryBase<ApplicationUser>, IUserRepo
	{
		public UserRepo(ApplicationDbContext DbContext)
	   : base(DbContext)
		{
			_DbContext = DbContext;

		}
		public async Task<IEnumerable<ApplicationUser>> GetAll()
		{
			return await FindAll(null)
		  .OrderByDescending(s => s.CreateDate)
		  .ToListAsync();
		}
		public async Task AddOrUpdate(ApplicationUser model, IFormFile _File)
		{
			Random random = new Random();
			List<SmsParameters> smsParameters = new List<SmsParameters>();

			if (string.IsNullOrEmpty(model.Id))
			{

				if (_File != null)
				{
					var fileName = Path.GetFileName(_File.FileName);
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Teacher\File", fileName);
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						_File.CopyTo(stream);
					}
					model.Url = "/Upload/Teacher/File/" + fileName;
				}
				model.UserName = model.Mobile.ToString();
				string UserPass = random.Next(10000, 99999).ToString();
				var user = new ApplicationUser { UserName = model.Mobile.ToString(), Email = model.Mobile + "yahoo.com", Password = UserPass, FullName = model.FullName, Address = model.Address, Mobile = model.Mobile, NationalCode = model.NationalCode, Url = model.Url, UserType = UserType.Teacher, PhoneNumber = "0" };
				Create(user);
				smsParameters.Add(new SmsParameters() { Parameter = "UserName", ParameterValue = user.UserName });
				smsParameters.Add(new SmsParameters() { Parameter = "Password", ParameterValue = user.Password });
				var mob = model.Mobile.ToString();
				mob = mob.Insert(0, "0");
				SendSms.CallSmSMethodAdvanced(Convert.ToInt64(mob), 38324, smsParameters);
			}
			else
			{

				var item = await _DbContext.Users.FindAsync(model.Id);
				if (_File != null)
				{
					if (item != null)
					{
						if (!string.IsNullOrEmpty(item.Url))
						{
							File.Delete($"wwwroot/{item.Url}");
						}
					}
					var fileName = Path.GetFileName(_File.FileName);
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Teacher\File", fileName);
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						_File.CopyTo(stream);
					}
					item.Url = "/Upload/Teacher/File/" + fileName;
				}
				if (item != null)
				{
					item.Mobile = model.Mobile;
					item.NationalCode = model.NationalCode;
					item.FullName = model.FullName;
					item.Address = model.Address;
				}
			}
		}
		public async Task<ApplicationUser> GetById(string Id)
		{
			return await FindByCondition(s => s.Id.Equals(Id))
				.FirstOrDefaultAsync();

		}
		public IQueryable<ApplicationUser> Authorize(ApplicationUser model)
		{
			var item = FindByCondition(s => s.UserName == model.UserName && s.Password == model.Password);
			return item;
		}
		public void Remove(ApplicationUser model)
		{
			if (!string.IsNullOrEmpty(model.Url))
			{
				File.Delete($"wwwroot/{model.Url}");
			}
			Delete(model);
		}
		public async Task<IEnumerable<ApplicationUser>> search(string txtsearch)
		{
			return await FindByCondition(s => s.FullName.Contains(txtsearch))
			.ToListAsync();
		}

		public async Task<bool> ForgetPassword(long Mobile)
		{
			var TeacherItem = await _DbContext.Users.Where(s => s.Mobile == Mobile).FirstOrDefaultAsync();
			var StudentItem = await _DbContext.Students.Where(s => s.Mobile == Mobile).FirstOrDefaultAsync();
			if (TeacherItem != null)
			{
				return SendSms.CallSmSMethod(Mobile, 38082, "VerificationCode", TeacherItem.Password);
			}
			else if (StudentItem != null)
			{
				return SendSms.CallSmSMethod(Mobile, 38082, "VerificationCode", StudentItem.Password);
			}
			else
			{
				return false;
			}
		}
		public async Task<TeacherAndStudents> GetRelatedUsers(ApplicationUser model)
		{
			TeacherAndStudents MainList = new TeacherAndStudents();
			List<Students> students = new List<Students>();
			if (model.UserType == UserType.Manager)
			{
				MainList.Students = await _DbContext.Students.ToListAsync();
				MainList.Teachers = await _DbContext.Users.Where(s => s.UserType == UserType.Teacher).ToListAsync();
				return MainList;
			}
			else
			{
				var Item = await _DbContext.TeachersToClassRooms.Include(s => s.ClassRoom.Students).Where(s => s.ApplicationUserId == model.Id).ToListAsync();
				foreach (var item in Item)
				{
					students.AddRange(item.ClassRoom.Students);
				}
				MainList.Students = students;
				MainList.Teachers = null;
				return MainList;
			}
		}

		public async Task<IEnumerable<Students>> GetRelatedStudents(int groupId, string userId)
		{
			CustomGroup customGroup = await _DbContext.CustomGroups.FirstOrDefaultAsync(s => s.Id == groupId);
			if (customGroup != null && await _DbContext.Users.AnyAsync(s => s.Id == userId))
			{
				List<UsersToCustomGroups> usersToCustomGroups = await _DbContext.UsersToCustomGroups.Where(s => s.CustomGroupId == customGroup.Id).ToListAsync();
				List<TeachersToClassRoom> TeacherClassRooms = await _DbContext.TeachersToClassRooms.Where(s => s.ApplicationUserId == userId).ToListAsync();
				List<Students> AllStudents = new List<Students>();
				List<Students> MainListStudent = new List<Students>();
				ApplicationUser applicationUserItem = await _DbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(userId));
				if(applicationUserItem.UserType == UserType.Manager)
				{
					AllStudents = await _DbContext.Students.ToListAsync();
					foreach (var item in AllStudents)
					{
						if (!usersToCustomGroups.Any(s => s.StudentsId == item.Id))
						{
							Students studentItem = await _DbContext.Students.FirstOrDefaultAsync(s => s.Id == item.Id);
							MainListStudent.Add(studentItem);
						}
					}
				}
				else
				{

				foreach (var item in TeacherClassRooms)
				{
					var studentItem = await _DbContext.Students.Where(s=>s.ClassRoomId == item.ClassRoomId).ToListAsync();
					AllStudents.AddRange(studentItem);
				}
				foreach (var item in AllStudents)
				{
					if (!usersToCustomGroups.Any(s => s.StudentsId == item.Id))
					{
						Students studentItem = await _DbContext.Students.FirstOrDefaultAsync(s => s.Id == item.Id);
						MainListStudent.Add(studentItem);
					}
				}
				}
				return MainListStudent;
			}
			else
			{
				return null;
			}
		}
		public async Task<IEnumerable<ApplicationUser>> GetRelatedTeachres(int groupId, string userId)
		{
			CustomGroup customGroup = await _DbContext.CustomGroups.FirstOrDefaultAsync(s => s.Id == groupId);
			List<UsersToCustomGroups> usersToCustomGroups = new List<UsersToCustomGroups>();
			List<ApplicationUser> applicationUsers = new List<ApplicationUser>();
			List<ApplicationUser> MainList = new List<ApplicationUser>();

			if (customGroup != null && await _DbContext.Users.AnyAsync(s => s.Id == userId))
			{
				usersToCustomGroups = await _DbContext.UsersToCustomGroups.Where(s => s.CustomGroupId == customGroup.Id && s.ApplicationUserId!=null ).ToListAsync();
				applicationUsers = await _DbContext.Users.Where(s=> s.UserType != UserType.Manager).ToListAsync();
				foreach (var item in applicationUsers)
				{
					if(! usersToCustomGroups.Any(s=>s.CustomGroupId.Equals(groupId)  && s.ApplicationUserId.Equals(item.Id)))
					{
						MainList.Add(item);
					}
				}
				foreach (var item in MainList)
				{
					
					item.Mobile = 0; 
				}
				return MainList;
			}
			else
			{
				return null;
			}
		}
		public async Task EditProfile(ApplicationUser model, IFormFile _File)
		{
			ApplicationUser UserItem = await _DbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(model.Id));
			if (UserItem != null)
			{
				UserItem.Address = model.Address;
				UserItem.FullName = model.FullName;
				UserItem.Mobile = model.Mobile;
				UserItem.Password = model.Password;
				if (!string.IsNullOrEmpty(model.Url))
				{
					if (!string.IsNullOrEmpty(model.Url))
					{
						File.Delete($"wwwroot/{model.Url}");
					}
					var fileName = Path.GetFileName(_File.FileName);
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Student\File", fileName);
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						_File.CopyTo(stream);
					}
					UserItem.Url = "/Upload/Teacher/File/" + fileName;

				}
			}
		}



	}
}
