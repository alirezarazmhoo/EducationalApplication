using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EducationalApplication.Infrastructure
{
	public interface IRepositoryBase<T>
	{
		IQueryable<T> FindAll(Expression<Func<T, object>> expression);
		IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
		void Create(T entity);
		void Update(T entity);
		void Delete(T entity);

	}
}
