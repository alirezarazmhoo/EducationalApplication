using EducationalApplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Models
{
	public class CustomGroupsToEducationPosts
	{
		public int Id { get; set; }
		public int CustomGroupId { get; set; }
		public CustomGroup CustomGroup { get; set; }
		public int EducationPostId { get; set; }
		public EducationPost  EducationPost { get; set; }
	}
}
