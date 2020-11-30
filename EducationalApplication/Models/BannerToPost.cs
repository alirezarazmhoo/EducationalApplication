using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Models
{
	public class BannerToPost
	{
		public int Id { get; set; }
		public int BannerId { get; set; }
		public Banner Banner { get; set; }
		public int EducationPostId { get; set; }
		public EducationPost EducationPost { get; set; }
	}
}
