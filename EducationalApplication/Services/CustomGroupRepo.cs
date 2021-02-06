using EducationalApplication.Data;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
using EducationalApplication.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Services
{
	public class CustomGroupRepo : RepositoryBase<CustomGroup>, ICustomGroupRepo
	{
        public CustomGroupRepo(ApplicationDbContext DbContext)
          : base(DbContext)
        {
            _DbContext = DbContext;
        }
        public async Task<List<CustomGroup>> GetAll(string Id)
        {
            return await FindByCondition(s=>s.ApplicationUserId == Id). ToListAsync();
        }
        public async Task<CustomGroup> GetById(int Id)
        {
            return await FindByCondition(b => b.Id.Equals(Id)).Include(s => s.UsersToCustomGroups).FirstOrDefaultAsync();
        }
        public async Task AddOrUpdate(CustomGroup model)
        {
            if (model.Id == 0)
            {
               await _DbContext.CustomGroups.AddAsync(model);
            }
            else
            {
                Update(model);
            }
        }
        public async Task<IEnumerable<CustomGroup>> search(string txtsearch , string UserId)
        {
            return await FindByCondition(s => s.Name.Contains(txtsearch) && s.ApplicationUserId == UserId)
            .ToListAsync();
        }
        public void Remove(CustomGroup model) => Delete(model);


        public async Task AddStudentToGroup(StudentAndCustomGroupViewModel model)
        {
            List<UsersToCustomGroups> MainModel = new List<UsersToCustomGroups>();
            Students student = new Students();
            foreach (var item in model.StudentId)
            {
                student = await _DbContext.Students.FirstOrDefaultAsync(s => s.Id == item); 

                if ( student !=null &&  await _DbContext.UsersToCustomGroups.AnyAsync(s => s.StudentsId == item && s.CustomGroupId == model.GroupId) == false)
                {
                MainModel.Add(new UsersToCustomGroups() { CustomGroupId = model.GroupId, StudentsId = item,StudentName = student.FullName });
                }
            }

          await  _DbContext.UsersToCustomGroups.AddRangeAsync(MainModel);
        }

        public async Task AddTeacherToGroup(TeacherAndCustomGroupViewModel model)
        {
            List<UsersToCustomGroups> MainModel = new List<UsersToCustomGroups>();
            foreach (var item in model.TeacherId)
            {
                if (await _DbContext.Users.AnyAsync(s => s.Id == item) && await _DbContext.UsersToCustomGroups.AnyAsync(s => s.ApplicationUserId == item) == false)
                {
                    MainModel.Add(new UsersToCustomGroups() { CustomGroupId = model.GroupId, ApplicationUserId = item });
                }
            }
            await _DbContext.UsersToCustomGroups.AddRangeAsync(MainModel);
        }
        public async Task RemoveStudentFromGroup(List<int> UserId, int CustomGrupId)
        {
            CustomGroup customGroup = await _DbContext.CustomGroups.FindAsync(CustomGrupId);
            UsersToCustomGroups usersToCustomGroups = new UsersToCustomGroups();
            if (customGroup != null)
            {
                for (int i = 0; i < UserId.Count(); i++)
                {
                    usersToCustomGroups = await _DbContext.UsersToCustomGroups.FirstOrDefaultAsync(s => s.StudentsId == UserId[i] && s.CustomGroupId == CustomGrupId);
                    if (usersToCustomGroups != null)
                    {
                        _DbContext.UsersToCustomGroups.Remove(usersToCustomGroups);
                    }
                }
            }
        
        }
        
        public async Task RemoveTeacherFromGroup(string UserId, int CustomGrupId)
        {
            CustomGroup customGroup = await _DbContext.CustomGroups.FindAsync(CustomGrupId);
            UsersToCustomGroups usersToCustomGroups = await _DbContext.UsersToCustomGroups.FirstOrDefaultAsync(s => s.ApplicationUserId == UserId);
            if (await _DbContext.Users.AnyAsync(s => s.Id == UserId) && customGroup != null)
            {
                _DbContext.UsersToCustomGroups.Remove(usersToCustomGroups);
            }
        }
    }
}
