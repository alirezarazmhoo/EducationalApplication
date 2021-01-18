using EducationalApplication.Data;
using EducationalApplication.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Models
{
	public class Comment
	{
		public int Id { get; set; }
		public string ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
		public int StudentsId { get; set; }
		public Students Students { get; set; }
		public string Text { get; set; }
		public DateTime date { get; set; } = DateTime.Now;
		public CommentStatus CommentStatus { get; set; } = CommentStatus.Waiting; 
		public int EducationPostId { get; set; }
		public EducationPost EducationPost { get; set; }
	}
}
