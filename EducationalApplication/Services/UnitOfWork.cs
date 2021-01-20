using EducationalApplication.Data;
using EducationalApplication.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Services
{
	public class UnitOfWork : IUnitOfWorkRepo
	{
		private ApplicationDbContext _DbContext;
		private IEducationPostRepo _IEducationPostRepo;
		private IUserRepo _IUserRepo;
		private ISchoolNameRepo _ISchoolNameRepo;
		private IGradeRepo _IGradeRepo;
		private IMajorRepo _IMajorRepo;
		private IStudentRepo _IStudentRepo;
		private IBanerRepo _IBannerRepo;
		private IClassRoomRepo _IClassRoomRepo;
		private ICategoryRepo _ICategoryRepo;
		private ICustomGroupRepo _ICustomGroupRepo;
		private ICommentRepo _ICommentRepo;
		private ISettingRepo _ISettingRepo;


		public UnitOfWork(ApplicationDbContext DbContext)
		{
			_DbContext = DbContext;
		}
		public async Task SaveAsync()
		{
			await _DbContext.SaveChangesAsync();
		}
		public IEducationPostRepo IEducationPostRepo
		{
			get
			{
				return _IEducationPostRepo = _IEducationPostRepo ?? new EducationPostRepo(_DbContext);
			}
		}
		public IUserRepo IUserRepo
		{
			get
			{
				return _IUserRepo = _IUserRepo ?? new UserRepo(_DbContext);
			}
		}
		public ISchoolNameRepo ISchoolNameRepo
		{
			get
			{
				return _ISchoolNameRepo = _ISchoolNameRepo ?? new SchoolNameRepo(_DbContext);
			}
		}
		public IGradeRepo IGradeRepo
		{
			get
			{
				return _IGradeRepo = _IGradeRepo ?? new GradeRepo(_DbContext);
			}
		}
		public IMajorRepo IMajorRepo
		{
			get
			{
				return _IMajorRepo = _IMajorRepo ?? new MajorRepo(_DbContext);
			}
		}
		public IStudentRepo IStudentRepo
		{
			get
			{
				return _IStudentRepo = _IStudentRepo ?? new StudentRepo(_DbContext);
			}
		}
		public IBanerRepo IBannerRepo
		{
			get
			{
				return _IBannerRepo = _IBannerRepo ?? new BanerRepo(_DbContext);
			}
		}

		public IClassRoomRepo IClassRoomRepo
		{
			get
			{
				return _IClassRoomRepo = _IClassRoomRepo ?? new ClassRoomRepo(_DbContext);
			}
		}
		public ICategoryRepo ICategoryRepo
		{
			get
			{
				return _ICategoryRepo = _ICategoryRepo ?? new CategoryRepo(_DbContext);
			}
		}
		public ICustomGroupRepo ICustomGroupRepo
		{
			get
			{
				return _ICustomGroupRepo = _ICustomGroupRepo ?? new CustomGroupRepo(_DbContext);
			}
		}
		public ICommentRepo ICommentRepo
		{
			get
			{
				return _ICommentRepo = _ICommentRepo ?? new CommentRepo(_DbContext);
			}
		}

		public ISettingRepo ISettingRepo
		{
			get
			{
				return _ISettingRepo = _ISettingRepo ?? new SettingRepo(_DbContext);
			}
		}

	}
}
