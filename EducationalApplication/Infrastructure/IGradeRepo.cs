using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Models; 
namespace EducationalApplication.Infrastructure
{
	public interface IGradeRepo
	{
		Task<IEnumerable<Grade>> GetAll();
		Task<Grade> GetById(int Id);
		void CreateOrUpdate(Grade model);
		void Remove(Grade model);
		Task<IEnumerable<Grade>> search(string txtsearch);
	}
}
