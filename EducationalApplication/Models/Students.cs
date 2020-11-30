using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Models;
using EducationalApplication.Models.Enums;

namespace EducationalApplication.Models
{
	public class Students
	{
		public int Id { get; set; }
		[Required]
		public string FullName { get; set; }
		[Required]
		public long Mobile { get; set; }
		public string Address { get; set; }
		[Required]
		public string NationalCode { get; set; }
		public string ApiToken { get; set; }
		public string Url { get; set; }
		
		public string UserName { get; set; }
		
		public string Password { get; set; }
		public int MajorId { get; set; }
		public Major Major { get; set; }
		public int GradeId  { get; set; }
		public Grade Grade { get; set; }

		public UserType UserType { get; set; } = UserType.Student;

	}
}
