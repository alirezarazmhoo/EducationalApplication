using EducationalApplication.Data;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Services
{
	public class SettingRepo : RepositoryBase<Setting>, ISettingRepo
	{
		public SettingRepo(ApplicationDbContext DbContext)
	   : base(DbContext)
		{
			_DbContext = DbContext;
		}

		public async Task UpdateSetting(Setting model)
		{
			var Item = await _DbContext.Settings.FirstOrDefaultAsync();
			if(Item != null)
			{

			Item.BannerCanShow = model.BannerCanShow;
			Item.PostCanShow = model.PostCanShow;
			Item.NeedBannersToAccept = model.NeedBannersToAccept;
			Item.NeedEducationPostsToAccept = model.NeedEducationPostsToAccept;
				Item.Announcement = model.Announcement; 
			}
		}

		public async Task<Setting> Get()
		{
			var Item =await _DbContext.Settings.FirstOrDefaultAsync();
			if(Item != null)
			{
				return Item;  
			}
			else
			{
				return null; 
			}
			
		
		}


	}
}
