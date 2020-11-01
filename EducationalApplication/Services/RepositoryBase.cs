using EducationalApplication.Data;
using EducationalApplication.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EducationalApplication.Services
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationDbContext _DbContext;

        public RepositoryBase(ApplicationDbContext DbContext)
        {
            this._DbContext = DbContext;
        }
        public IQueryable<T> FindAll(Expression<Func<T, object>> expression)
        {
            if (expression != null)
            {
                return this._DbContext.Set<T>().Include(expression).AsNoTracking();
            }
            else
            {
                return this._DbContext.Set<T>().AsNoTracking();

            }

        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this._DbContext.Set<T>()
                .Where(expression).AsNoTracking();
        }
        public void Create(T entity)
        {
            this._DbContext.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            this._DbContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            this._DbContext.Set<T>().Remove(entity);
        }
    }
}
