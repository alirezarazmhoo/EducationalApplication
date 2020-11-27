using EducationalApplication.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Infrastructure
{
   public interface IEducationPostRepo
	{
		 Task AddOrUpdate(EducationPost model, IFormFile Icon, IFormFile[] files);
		Task<IEnumerable<EducationPost>> GetAll(int Id);
		Task<EducationPost>  GetById(int Id);
		 Task Remove(EducationPost model);
		Task RemoveFile(int Id);


	}
}
