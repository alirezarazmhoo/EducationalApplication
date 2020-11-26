using EducationalApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Infrastructure
{
	public interface IMajorRepo
	{
		Task<IEnumerable<Major>> GetAll();
		Task<Major> GetById(int Id);
		void CreateOrUpdate(Major model);
		void Remove(Major model);
		Task<IEnumerable<Major>> search(string txtsearch);
	}
}
