using EducationalApplication.Data;
using System;
using System.Collections.Generic;

namespace EducationalApplication.Models
{
	public class CustomGroup
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime Date { get; set; } = DateTime.Now;
		public string ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
		public ICollection<UsersToCustomGroups> UsersToCustomGroups { get; set; }

	}
}
