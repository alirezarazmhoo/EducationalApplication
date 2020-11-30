using EducationalApplication.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Models
{
	public class Banner
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public string SocialNetWorkLink { get; set; }
		public string Url { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime AvailableDate { get; set; }
		public int CreditDays { get; set; }
		public BannerPlace  BannerPlace { get; set; }
		public ICollection<BannerToPost> BannerToPosts { get; set; }
		public int? CategoryId { get; set; }
		public Category Category { get; set; }


		public bool ShowOnMainPage { get; set; }
	}
}
