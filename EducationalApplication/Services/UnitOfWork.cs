using EducationalApplication.Data;
using EducationalApplication.Infrastructure;
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
		private ITeacherRepo _ITeacherRepo;
		private ISchoolNameRepo _ISchoolNameRepo;
		private IGradeRepo _IGradeRepo;
		private IMajorRepo _IMajorRepo;
		private IStudentRepo _IStudentRepo;


		public UnitOfWork(ApplicationDbContext DbContext)
		{
			_DbContext = DbContext;
		}
		public async Task SaveAsync()
		{
			await _DbContext.SaveChangesAsync();
		}
		public IEducationPostRepo  IEducationPostRepo
		{
			get
			{
				return _IEducationPostRepo = _IEducationPostRepo ?? new EducationPostRepo(_DbContext);
			}
		}
		public ITeacherRepo  ITeacherRepo
		{
			get
			{
				return _ITeacherRepo = _ITeacherRepo ?? new TeacherRepo(_DbContext);
			}
		}
		public ISchoolNameRepo  ISchoolNameRepo
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
	}
}
