using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Models
{
	public class Media
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Url { get; set; }
		public int EducationPostId { get; set; }
		public string DurationTime { get; set; }
		public string lenght { get; set; }
		public EducationPost EducationPost { get; set; }
		public MediaType MediaType { get; set; }
	}
	public enum MediaType
	{
		Pdf , 
		Word , 
		Picture ,
		Video ,
		Audio , 
		Undefined , 
		PowerPoint
	}
}
