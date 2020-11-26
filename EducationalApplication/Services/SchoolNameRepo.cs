using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Models;
using EducationalApplication.Infrastructure;
using EducationalApplication.Data;

namespace EducationalApplication.Services
{
	public class SchoolNameRepo : RepositoryBase<SchoolName>, ISchoolNameRepo
	{
		public SchoolNameRepo(ApplicationDbContext DbContext)
	 : base(DbContext)
		{
			_DbContext = DbContext;
		}
		public  void Change(SchoolName model)
		{
			SchoolName item = _DbContext.SchoolName.FirstOrDefault();
			if(item != null)
			{
				item.Name = model.Name; 
			}
		}
		public SchoolName Get()
		{
		return	_DbContext.SchoolName.FirstOrDefault();
		}

	}
}
