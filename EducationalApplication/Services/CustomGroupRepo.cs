using EducationalApplication.Data;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
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
            return await FindByCondition(b => b.Id.Equals(Id)).FirstOrDefaultAsync();
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
    }
}
