using EducationalApplication.Models;
using EducationalApplication.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Infrastructure
{
	public interface IStudentRepo
	{
		Task AddOrUpdate(Students model, IFormFile File);
		Task<Students> GetById(int Id);
		IQueryable<Students> Authorize(string UserName, string Password);
		void Remove(Students model);
		Task<IEnumerable<Students>> GetAll();
		Task<IEnumerable<Students>> search(string txtsearch);
		Task<IEnumerable<ShortTeacherViewModel>> GetRelatedTeachers(int Id);
		Task<IEnumerable<EducationPost>> GetRelatedEducationPostByTeacherId(string UserId, int StudentId);
		Task EditProfile(Students model, IFormFile _File); 
	}
}
