using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Models
{
	public class Test
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "نام مدرسه راوارد کنید")]
		[MinLength(5, ErrorMessage = "طول حداقل باید 5 کاراکتر باشد")]
		[MaxLength(50, ErrorMessage = "نام مدرسه بسیار طولانی است ")]
		public string Code { get; set;  }
	}
}
