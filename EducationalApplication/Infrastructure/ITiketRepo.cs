using EducationalApplication.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Infrastructure
{
	public interface ITiketRepo
	{
		Task<IEnumerable<Ticket>> TeacherGetAll(string Id);
		Task<IEnumerable<Ticket>> StudentGetAll(int Id);
		Task<Ticket> GetById(int Id);
		Task Create(Ticket model, IFormFile[] files);
		Task Remove(Ticket model);
	}
}
