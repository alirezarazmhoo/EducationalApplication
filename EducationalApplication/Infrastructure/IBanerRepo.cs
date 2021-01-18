using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Models;
using Microsoft.AspNetCore.Http;

namespace EducationalApplication.Infrastructure
{
	public interface IBanerRepo
	{
		Task<List<Banner>> GetAll(string Id);
		Task<Banner> GetById(int banerId);
		Task AddOrUpdate(Banner baner, IFormFile _File);
		void Remove(Banner model);
		Task RemoveFile(int Id);
		Task RemoveEducationPostFromBanner(int BannerId, int PostId);
		Task AddPostToBanner(int BannerId, int PostId); 
	}
}
