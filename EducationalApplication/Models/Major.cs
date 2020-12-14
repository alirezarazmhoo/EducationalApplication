using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Models
{
	public class Major
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "کد مقطع راوارد کنید")]
		[MaxLength(10, ErrorMessage = "کد رشته تحصیلی بسیار طولانی است")]
		[DisplayName("کد")]
		public string Code { get; set; }
		[Required(ErrorMessage = "نام مقطع راوارد کنید")]
		[MaxLength(50, ErrorMessage = "نام رشته تحصیلی بسیار طولانی است ")]
		[DisplayName("نام")]
		public string Name { get; set; }
	
	}
}
