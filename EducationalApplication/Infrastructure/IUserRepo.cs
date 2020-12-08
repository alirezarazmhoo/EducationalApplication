using EducationalApplication.Data;
using EducationalApplication.Models;
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


	}
}
