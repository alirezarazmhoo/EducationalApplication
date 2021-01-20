using EducationalApplication.Models;
using EducationalApplication.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Infrastructure
{
   public interface IEducationPostRepo
	{
		Task AddOrUpdate(EducationPost model, IFormFile Icon, IFormFile[] files , string TeacherList, string StudentList);
		Task<IEnumerable<EducationPost>> GetAll(string Id);
		Task<EducationPost>  GetById(int Id);
	    Task Remove(EducationPost model);
		Task RemoveFile(int Id);
		Task<IEnumerable<EducationPost>> GetByCategory(int Id);
		Task<IEnumerable<Comment>> GetEducationPostCommnet(int Id);
		Task TeacherAddView(AddViewToPostViewModel<string> model);
		Task StudentAddView(AddViewToPostViewModel<int> model);
		Task<IEnumerable<EducationPost>> AdminGetAll();
		Task Accept(int Id);
		Task Reject(int Id);
		Task<EducationPost> AdminGetById(int Id); 
	}
}
