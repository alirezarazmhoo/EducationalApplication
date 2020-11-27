using EducationalApplication.Models;
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
		IQueryable<Students> Authorize(Students model);
		void Remove(Students model);
		Task<IEnumerable<Students>> GetAll();
		Task<IEnumerable<Students>> search(string txtsearch);
	}
}
