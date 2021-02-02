using EducationalApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Infrastructure
{
public interface IAboutUsRepo
	{
		AboutUs Get();
		Task UpdateAbutus(AboutUs model);  
	}
}
