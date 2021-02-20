using EducationalApplication.Data;
using EducationalApplication.Models;
using EducationalApplication.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace EducationalApplication.Infrastructure
{
   public interface IUserRepo
	{
		Task AddOrUpdate(ApplicationUser model, IFormFile File);
		Task<ApplicationUser> GetById(string Id);
		IQueryable<ApplicationUser> Authorize(ApplicationUser model);
		void Remove(ApplicationUser model);
		Task<IEnumerable<ApplicationUser>> GetAll();
		Task<IEnumerable<ApplicationUser>> search(string txtsearch);
		Task<bool> ForgetPassword(long Mobile);
		Task<TeacherAndStudents> GetRelatedUsers(ApplicationUser model);
		Task<IEnumerable<Students>> GetRelatedStudents(int groupId, string userId);
		Task<IEnumerable<ApplicationUser>> GetRelatedTeachres(int groupId, string userId); 
		Task EditProfile(ApplicationUser model, IFormFile _File); 

	}
}
