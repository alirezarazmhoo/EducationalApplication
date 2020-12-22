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
		Task RemovePerson(string Id , int mode );
		Task<IEnumerable<ClassRoom>> search(string txtsearch);
		Task<bool> CheckCode(int code);
		Task<IEnumerable<Students>> GetAvalibleStudents(string txtSearch, int Id);
		Task AddPerson(string Id, int Mode, int ClassId); 
	}
}
