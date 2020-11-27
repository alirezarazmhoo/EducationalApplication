using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Infrastructure
{
	public interface IUnitOfWorkRepo
	{
		IEducationPostRepo IEducationPostRepo { get; }
		ITeacherRepo ITeacherRepo { get; }
		ISchoolNameRepo ISchoolNameRepo { get;  }
		IGradeRepo IGradeRepo { get; }
		IMajorRepo IMajorRepo { get; }
		IStudentRepo IStudentRepo { get; }

		Task SaveAsync();
	}
}
