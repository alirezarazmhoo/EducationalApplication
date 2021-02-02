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
	public class AboutUsRepo : RepositoryBase<AboutUs>, IAboutUsRepo
	{
		public AboutUsRepo(ApplicationDbContext DbContext)
	   : base(DbContext)
		{
			_DbContext = DbContext;
		}
		public AboutUs Get()
		{
		return	_DbContext.AboutUs.FirstOrDefault();
		}

		public async Task UpdateAbutus(AboutUs model)
		{
			var Item = _DbContext.AboutUs.FirstOrDefault();
			if(Item != null)
			{
				Item.Text = model.Text; 
			}
		}


	}
}
