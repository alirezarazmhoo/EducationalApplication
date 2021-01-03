using EducationalApplication.Models;
using EducationalApplication.Models.ViewModels;
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
		Task Remove(ClassRoom model);
		Task RemovePerson(string Id , int mode , int? ClassId);
		Task<IEnumerable<ClassRoom>> search(string txtsearch);
		Task<bool> CheckCode(int code);
		Task<IEnumerable<Students>> GetAvalibleStudents(string txtSearch, int Id);
		Task AddPerson(string Id, int Mode, int ClassId);
		Task<IEnumerable<TeacherToClassRoomViewModel>> GetAllTeachers(int? ClassId, string txtSearch);
		Task<AllPersonsClassRoomViewModel> GetAllPersons(int? ClassId , string txtSearch);
		Task<string> GetName(int ClassId); 

	}
}
