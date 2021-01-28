using EducationalApplication.Data;
using EducationalApplication.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Models
{
	public class EducationPost
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string IconUrl { get; set; }
		public ICollection<Media> Medias { get; set; }
		public int Number { get; set; }
		public accessType AccessType { get; set; }
		public bool Pin { get; set; }
		public int? CategoryId { get; set; }
		public Category Category { get; set; }
		public string ApplicationUserId { get; set; }
		public ApplicationUser  ApplicationUser { get; set; }
		public ICollection<Comment> Comments { get; set; }
		public int ViewCount { get; set; }
		public EducationPostStatus Status { get; set; }
		[NotMapped]
		public string GroupsIds { get; set; }

		[NotMapped]
		public string StudentListToPost { get; set; }

		[NotMapped]
		public string TeacherListToPost { get; set; }



		public ICollection<UsersToEducationPost> UsersToEducationPosts { get; set;  }

	}



}
