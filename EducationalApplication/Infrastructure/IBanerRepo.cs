using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Models;
using EducationalApplication.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace EducationalApplication.Infrastructure
{
	public interface IBanerRepo
	{
		Task<List<BannerViewModel>> GetAll(string Id);
		Task<Banner> GetById(int banerId);
		Task AddOrUpdate(Banner baner, IFormFile _File);
		void Remove(Banner model);
		Task RemoveFile(int Id);
		Task RemoveEducationPostFromBanner(int BannerId, int PostId);
		Task AddPostToBanner(int BannerId, int PostId);

		Task<IEnumerable<Banner>> AdminGetAll();
		Task Accept(int Id);
		Task Reject(int Id);
		Task<Banner> AdminGetById(int Id);
		Task<List<BannerViewModel>> GetAllForMainPage(string Id);


	}
}
