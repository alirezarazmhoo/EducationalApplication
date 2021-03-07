using EducationalApplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Models
{
	public class Ticket
	{
		public int Id { get; set; }
		public string Message { get; set; }
		public DateTime Date { get; set; }
		public int? StudentId { get; set; }
		public Students Student { get; set; }
		public string ApplicationUserId { get; set;  }
		public ApplicationUser ApplicationUser { get; set; }
		public string TeacherReceiver { get; set; }
		public int StudentReceiver { get; set; }
		public ICollection<TicketMedia> Medias { get; set; }
	}
}
