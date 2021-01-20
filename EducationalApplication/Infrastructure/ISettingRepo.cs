using EducationalApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Infrastructure
{
	public interface ISettingRepo
	{
	    Task UpdateSetting(Setting model);

		Task<Setting> Get( ); 

	}
}
