using EducationalApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Infrastructure
{
	public interface IClassRoomRepo
	{
		Task<IEnumerable<ClassRoom>>  GetAll();
		Task<ClassRoom> GetById(int Id);
		Task CreateOrUpdate(ClassRoom model);
		void Remove(ClassRoom model);
		//Task RemovePerson(string Id );
		Task<IEnumerable<ClassRoom>> search(string txtsearch); 

	}
}
