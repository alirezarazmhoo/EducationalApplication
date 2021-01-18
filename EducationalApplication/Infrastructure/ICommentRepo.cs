using EducationalApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Infrastructure
{
	public interface ICommentRepo
	{
		Task<IEnumerable<Comment>> GetAll(int Id);
		Task<Comment> GetById(int Id);
		Task Create(Comment model);
		Task Remove(Comment model);
		Task ChaneStatus(Comment model); 

	}
}
