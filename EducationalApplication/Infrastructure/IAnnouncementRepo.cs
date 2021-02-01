using EducationalApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Infrastructure
{
	public interface IAnnouncementRepo
	{
		Task<List<Announcements>> GetAll(string Id);
		Task<Announcements> GetById(int Id);
		Task AddOrUpdate(Announcements baner);
		Task<List<Announcements>> GetForStudent(int Id);
        Task Remove(Announcements model);
		//Task<List<Announcements>> GetForTeacher(int Id);
	}
}
