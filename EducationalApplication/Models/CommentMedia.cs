using EducationalApplication.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Models
{
	public class CommentMedia
	{
		public int Id { get; set; }
		public string Url { get; set; }
		public string DurationTime { get; set; }
		public string lenght { get; set; }
		public int CommentId { get; set; }
		public Comment  Comment { get; set; }
		public MediaType MediaType { get; set; }
	}
}
