using EducationalApplication.Data;
using EducationalApplication.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Models
{
	public class Banner
	{
		[Key]
		public int Id { get; set; }
		public string Description { get; set; }
		public string SocialNetWorkLink { get; set; }
		public string Url { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime AvailableDate { get; set; }
		public int CreditDays { get; set; }
		public BannerPlace  BannerPlace { get; set; }
		[NotMapped]
		public string BannerToPosts { get; set; }
		public ICollection<BannerToPost> PostsInBanner { get; set; }
		public int? CategoryId { get; set; }	
		public Category Category { get; set; }
		public string ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
		public bool ShowOnMainPage { get; set; }
		public BannerStatus BannerStatus { get; set; }
		public bool Pin { get; set; }

	}
}
