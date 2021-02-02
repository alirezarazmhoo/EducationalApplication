using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Models
{
	public class Setting
	{
		public int Id { get; set; }

		public bool NeedEducationPostsToAccept { get; set; }
		
		public bool NeedBannersToAccept { get; set; }

		public int BannerCanShow { get; set; }
		public int PostCanShow { get; set; }
		public int Announcement { get; set; }

	}
}
