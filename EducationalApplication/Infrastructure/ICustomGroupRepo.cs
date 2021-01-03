using EducationalApplication.Models;
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
    }
}
