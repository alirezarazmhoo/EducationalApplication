using EducationalApplication.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Infrastructure
{
	public interface ICategoryRepo
	{
		Task<IEnumerable<Category>> GetAll(string Id);
		Task<Category> GetById(int Id);
		Task CreateOrUpdate(Category model , IFormFile _File);
		void Remove(Category model);
		Task<IEnumerable<Category>> search(string txtsearch , string UserId);
		Task RemoveFile(int Id);
		Task<List<Category>> GetAllForMainPage(string Id); 
	}
}
