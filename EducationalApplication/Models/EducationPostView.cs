using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Models
{
	public class EducationPostView
	{
		public int Id { get; set; }
		public int EducationPostId { get; set; }
		public int StudentId { get; set; }
		public string TeacherId { get; set; }

	}
}
