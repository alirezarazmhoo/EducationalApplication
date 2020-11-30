using EducationalApplication.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Models
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }
		public int CreatorId { get; set; }
		public UserType UserType { get; set; }
	}
}
