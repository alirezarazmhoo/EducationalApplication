using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Data;
using EducationalApplication.Models; 
namespace EducationalApplication.Models.ViewModels
{
	public class EditViewModels
	{
		public string key { get; set; }
		public string value { get; set; }
	}
	public class  FileViewModels
	{
		public int id { get; set; }
		public string url { get; set; }
	}
	public class ComboBoxViewModel
	{
		public int id { get; set; }
		public string name { get; set; }
	}

	public class UsersViewModel
	{
		public ApplicationUser User { get; set; }
		public string School { get; set; }
	}
}
