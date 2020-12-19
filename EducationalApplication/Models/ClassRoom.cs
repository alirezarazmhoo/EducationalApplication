using EducationalApplication.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Models
{
	public class ClassRoom
	{
		public int Id { get; set; }
		[Required(ErrorMessage ="نام کلاس را وارد کنید")]
		public string Name { get; set; }
		[Required(ErrorMessage = "کد کلاس را وارد کنید")]
		public int Code { get; set; }
		public int MajorId { get; set; }
		public Major Major { get; set; }
		public int GradeId { get; set; }
		public Grade Grade { get; set; }
		public ICollection<Students>  Students { get; set; }
		public ICollection<TeachersToClassRoom> TeachersToClassRoom { get; set; }
		public DateTime CreateDate { get; set; } = DateTime.Now;
	}

}
