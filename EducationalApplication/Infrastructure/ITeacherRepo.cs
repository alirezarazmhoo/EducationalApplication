using EducationalApplication.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace EducationalApplication.Infrastructure
{
   public interface ITeacherRepo
	{
		Task AddOrUpdate(Teacher model, IFormFile File);
		Task<Teacher> GetById(int Id);
		IQueryable<Teacher> Authorize(Teacher model);
		void Remove(Teacher model);
		Task<IEnumerable<Teacher>> GetAll();
		Task<IEnumerable<Teacher>> search(string txtsearch);
		
	}
}
