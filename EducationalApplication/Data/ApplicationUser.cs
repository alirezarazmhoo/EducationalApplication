using EducationalApplication.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Data
{
	public class ApplicationUser :IdentityUser
	{
	
		public int IdHelper { get; set; }
		public string FullName { get; set; }
		public long Mobile { get; set; }
		public string Address { get; set; }
		public string NationalCode { get; set; }
		public string ApiToken { get; set; }
		public string Url { get; set; }
		public string Password { get; set; }
		public DateTime CreateDate { get; set; } = DateTime.Now;
		public UserType UserType { get; set; } 
	}
}
