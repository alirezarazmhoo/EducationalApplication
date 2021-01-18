using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Infrastructure
{
	public interface IUnitOfWorkRepo
	{
		IEducationPostRepo IEducationPostRepo { get; }
		IUserRepo IUserRepo { get; }
		ISchoolNameRepo ISchoolNameRepo { get;  }
		IGradeRepo IGradeRepo { get; }
		IMajorRepo IMajorRepo { get; }
		IStudentRepo IStudentRepo { get; }
        IBanerRepo IBannerRepo { get; }
		IClassRoomRepo IClassRoomRepo { get; }
		ICategoryRepo ICategoryRepo { get; }
        ICustomGroupRepo ICustomGroupRepo { get; }
		ICommentRepo ICommentRepo { get; }
		Task SaveAsync();
	}
}
