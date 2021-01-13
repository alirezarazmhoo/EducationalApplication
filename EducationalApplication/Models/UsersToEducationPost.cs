using EducationalApplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Models
{
	public class UsersToEducationPost
	{
		public int Id { get; set; }
		public string ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
		public int? StudentsId { get; set; }
		public Students Students { get; set; }
		public int EducationPostId { get; set; }
		public EducationPost EducationPost { get; set; }
	}
}
