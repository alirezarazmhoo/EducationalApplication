using EducationalApplication.Models;
using EducationalApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Infrastructure
{
 public	interface ICustomGroupRepo
	{
        Task<List<CustomGroup>> GetAll(string Id);
        Task<CustomGroup> GetById(int banerId);
        Task AddOrUpdate(CustomGroup model);
        void Remove(CustomGroup model);
        Task<IEnumerable<CustomGroup>> search(string txtsearch, string UserId);
        Task AddStudentToGroup(StudentAndCustomGroupViewModel model);
        Task AddTeacherToGroup(TeacherAndCustomGroupViewModel model);
        Task RemoveStudentFromGroup(int UserId, int CustomGrupId);
        Task RemoveTeacherFromGroup(string UserId, int CustomGrupId); 
    }
}
