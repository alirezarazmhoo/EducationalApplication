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
            ApplicationUser applicationUserItem = await _DbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(Id)); 
            if(applicationUserItem.UserType == Models.Enums.UserType.Manager)
			{
                return await FindByCondition(s => s.ApplicationUserId == Id && s.ApplicationUser.UserType == Models.Enums.UserType.Manager).ToListAsync();
            }
			else
			{

            return await FindByCondition(s=>s.ApplicationUserId == Id). ToListAsync();
			}

        }
        public async Task<CustomGroupViewModel> GetById(int Id)
        {
            CustomGroup customGroupItem = await _DbContext.CustomGroups.Include(s => s.UsersToCustomGroups).Include(s=>s.ApplicationUser).Where(s => s.Id.Equals(Id)).FirstOrDefaultAsync();
            CustomGroupViewModel customGroupViewModel = new CustomGroupViewModel();
            List<UsersToCustomGroupsViewModel> toCustomGroupsViewModels = new List<UsersToCustomGroupsViewModel>();
            if (customGroupItem != null)
			{
                customGroupViewModel.ApplicationUser =null;
                customGroupViewModel.ApplicationUserId = customGroupItem.ApplicationUserId;
                customGroupViewModel.Date = customGroupItem.Date;
                customGroupViewModel.Id = customGroupItem.Id;
                customGroupViewModel.IsForTeacher = customGroupItem.IsForTeacher;
                customGroupViewModel.Name = customGroupItem.Name;             
			foreach (var item in customGroupItem.UsersToCustomGroups)
			{
                    var teacheritem = await _DbContext.Users.FirstOrDefaultAsync(s => s.Id == item.ApplicationUserId);
                
                toCustomGroupsViewModels.Add(new UsersToCustomGroupsViewModel() { ApplicationUserId = teacheritem == null? 0 : teacheritem.IdHelper, ApplicationUser = null, StudentName = item.StudentName, CustomGroupId = item.CustomGroupId, Id = item.Id, StudentsId = item.StudentsId, Students = null});
            }
            customGroupViewModel.UsersToCustomGroups = toCustomGroupsViewModels;

            return customGroupViewModel; 
            }
            else
			{
                return null;
			}

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
        public void Remove(int id)
		{
            var Item = _DbContext.CustomGroups.FirstOrDefault(s => s.Id.Equals(id));
            if(Item != null)
			{
                _DbContext.CustomGroups.Remove(Item);
			}
		}
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
               var  teacherItem = await _DbContext.Users.FirstOrDefaultAsync(s => s.IdHelper == Convert.ToInt32(item) );
                if (await _DbContext.Users.AnyAsync(s => s.Id == teacherItem.Id) && await _DbContext.UsersToCustomGroups.AnyAsync(s => s.ApplicationUserId == teacherItem.Id &&  s.CustomGroupId == model.GroupId) == false)
                {
                    MainModel.Add(new UsersToCustomGroups() { CustomGroupId = model.GroupId, ApplicationUserId = teacherItem.Id, StudentName = teacherItem.FullName});
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
        
        public async Task RemoveTeacherFromGroup(List<string>  UserId, int CustomGrupId)
        {
            CustomGroup customGroup = await _DbContext.CustomGroups.FindAsync(CustomGrupId);
            UsersToCustomGroups usersToCustomGroups = new UsersToCustomGroups();
           
            if (customGroup != null)
            {
                for (int i = 0; i < UserId.Count(); i++)
                {

                    var TeacherItem = await _DbContext.Users.FirstOrDefaultAsync(s => s.IdHelper == Convert.ToInt32(UserId[i]));


                    usersToCustomGroups = await _DbContext.UsersToCustomGroups.FirstOrDefaultAsync(s => s.ApplicationUserId == TeacherItem.Id && s.CustomGroupId == CustomGrupId);
                    if (usersToCustomGroups != null)
                    {
                        _DbContext.UsersToCustomGroups.Remove(usersToCustomGroups);
                    }
                }
            }
        }
    }
}
