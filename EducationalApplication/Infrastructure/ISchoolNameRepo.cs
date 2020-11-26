using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Models;
namespace EducationalApplication.Infrastructure
{
  public interface ISchoolNameRepo
	{
		void Change(SchoolName model);
		SchoolName Get();
		
	}
}
