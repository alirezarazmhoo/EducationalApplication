using EducationalApplication.Data;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Services
{
    public class FavoritRepo : RepositoryBase<FavoritRepo>, IFavoritRepo
    {
        public FavoritRepo(ApplicationDbContext DbContext)
        : base(DbContext)
        {
            _DbContext = DbContext;
        }
        public async Task Create(Favorit model)
        {
           await _DbContext.Favorits.AddAsync(model);
        }
        public  void Remove(Favorit model)
        {
             _DbContext.Favorits.Remove(model);
        }
    }
}
