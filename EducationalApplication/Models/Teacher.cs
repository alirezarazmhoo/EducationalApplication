using EducationalApplication.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Models
{
	public class Teacher
	{
		public int Id { get; set; }
		public string FullName { get; set; }
		public long Mobile { get; set; }
		public string Address { get; set; }
		public string NationalCode { get; set; }
		public string ApiToken { get; set; }
		public string Url { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public UserType UserType { get; set; } = UserType.Teacher;

	}
}
