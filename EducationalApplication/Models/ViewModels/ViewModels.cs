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
	public class ClassRoomViewModel :ClassRoom
	{
		public int TeacherCount { get; set; }
		public int StudentCount { get; set; }

	}

	public class TeacherToClassRoomViewModel 
	{
		public ApplicationUser Teacher { get; set; }
		public  bool IsInClass  { get; set; }
		public int ClassId { get; set; }
	}

	public class AllPersonsClassRoomViewModel { 
	    public List<TeacherToClassRoomViewModel> Teachers { get; set; }
		public List<Students> Students { get; set; }
	}
	public class TeacherAndStudents
	{
		public List<Students> Students { get; set; }
		public List<ApplicationUser> Teachers { get; set; }

	}
	public class ShortTeacherViewModel
	{
		public string Id { get; set; }
	
	
	}
	public class ShortStuudentViewModel
	{
		public int Id { get; set; }
	}
	

}
