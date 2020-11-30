using EducationalApplication.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Models.ViewModels
{
	public class EducationViewModel
	{
		public int Id { get; set; }
		public string title { get; set; }
		public string description { get; set; }
		public string iconurl { get; set; }
		public ICollection<Media> Medias { get; set; }
		public int Number { get; set; }
		public accessType AccessType { get; set; }
		public bool Pin { get; set; }
		public category category { get; set; }
		public int TeacherId { get; set; }
		public Teacher Teacher { get; set; }

	}
}
