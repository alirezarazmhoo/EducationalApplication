using EducationalApplication.Data;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
using EducationalApplication.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Utility;

namespace EducationalApplication.Services
{
	public class StudentRepo : RepositoryBase<Students>, IStudentRepo
	{
		public StudentRepo(ApplicationDbContext DbContext)
	   : base(DbContext)
		{
			_DbContext = DbContext;
		}
		public async Task<IEnumerable<Students>> GetAll()
		{
			return await FindAll(null).Include(s=>s.Grade).Include(s=>s.Major)
		  .OrderByDescending(s => s.Id)
		  .ToListAsync();
		}
		public async Task AddOrUpdate(Students model, IFormFile _File)
		{
			Random random = new Random();
			List<SmsParameters> smsParameters = new List<SmsParameters>();
			if (model.Id == 0)
			{
				if (_File != null)
				{
					var fileName = Path.GetFileName(_File.FileName);
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Student\File", fileName);
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						_File.CopyTo(stream);
					}
					model.Url = "/Upload/Student/File/" + fileName;
				}
				model.UserName = model.Mobile.ToString();
				model.Password = random.Next(10000, 99999).ToString();
				Create(model);
				smsParameters.Add(new SmsParameters() {  Parameter = "UserName" , ParameterValue =model.UserName });
				smsParameters.Add(new SmsParameters() { Parameter = "Password", ParameterValue = model.Password });
				var mob = model.Mobile.ToString();	
			    mob = mob.Insert(0, "0");				
				SendSms.CallSmSMethodAdvanced(Convert.ToInt64(mob), 38325, smsParameters);
			}
			else
			{
				var item = await _DbContext.Students.FindAsync(model.Id);
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
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Student\File", fileName);
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						_File.CopyTo(stream);
					}
					item.Url = "/Upload/Student/File/" + fileName;
				}
				if (item != null)
				{
					item.Mobile = model.Mobile;
					item.MajorId = model.MajorId;
					item.GradeId = model.GradeId;
					item.NationalCode = model.NationalCode;
					item.FullName = model.FullName;
					item.Address = model.Address;
				}
			}
		}
		public async Task<Students> GetById(int Id)
		{
			return await FindByCondition(s => s.Id.Equals(Id))
				.FirstOrDefaultAsync();
		}
		public IQueryable<Students> Authorize(string UserName,string Password)
		{
			var item = FindByCondition(s => s.UserName == UserName && s.Password == Password);
			return item;
		}
		public void Remove(Students model)
		{
			if (!string.IsNullOrEmpty(model.Url))
			{
				File.Delete($"wwwroot/{model.Url}");
			}
			Delete(model);
		}
		public async Task<IEnumerable<Students>> search(string txtsearch)
		{
			return await FindByCondition(s => s.FullName.Contains(txtsearch)).Include(s => s.Grade).Include(s => s.Major)
			.ToListAsync();
		}
		public async Task<IEnumerable<ShortTeacherViewModel>> GetRelatedTeachers(int Id)
		{
			List<ShortTeacherViewModel> shortTeacherViewModels = new List<ShortTeacherViewModel>();
		    Students StudentItem =await _DbContext.Students.FirstOrDefaultAsync(s => s.Id == Id);
			if(StudentItem != null)
			{
				IEnumerable<TeachersToClassRoom>  teachersToClassRoom =await _DbContext.TeachersToClassRooms.Include(s=>s.ApplicationUser).Where(s => s.ClassRoomId == StudentItem.ClassRoomId).ToListAsync();

				foreach (var item in teachersToClassRoom)
				{
					shortTeacherViewModels.Add(new ShortTeacherViewModel() { Id = item.ApplicationUserId, Name = item.ApplicationUser.FullName });
				}
				return shortTeacherViewModels;
			}
			return null; 
		}
		public async Task<IEnumerable<EducationPost>> GetRelatedEducationPostByTeacherId(string UserId ,  int StudentId)
		{
			ApplicationUser TeacherItem = await _DbContext.Users.FirstOrDefaultAsync(s => s.Id == UserId); 
			Students studentItem = await _DbContext.Students.FirstOrDefaultAsync(s => s.Id == StudentId);
			List<EducationPost> usersToEducationPosts = new List<EducationPost>();
			if ( TeacherItem !=null && studentItem != null)
			{
				return (usersToEducationPosts = await _DbContext.UsersToEducationPosts.Include(s=>s.EducationPost).Where(s => s.StudentsId == StudentId && s.ApplicationUserId == UserId).Select(s => s.EducationPost).ToListAsync());
			}
			else
			{
				return null; 
			}
		}



	}
}
