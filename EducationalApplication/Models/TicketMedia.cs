using EducationalApplication.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Models
{
	public class TicketMedia
	{
		public int Id { get; set; }
		public string Url { get; set; }
		public string DurationTime { get; set; }
		public string lenght { get; set; }
		public int TiketId { get; set; }
		public Ticket  Tiket { get; set; }
		public MediaType MediaType { get; set; }
	}
}
