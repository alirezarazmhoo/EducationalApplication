using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Models.Enums
{
	public enum accessType
	{
		Free,
		IncludesCost
	}

	public enum category
	{
		Math,
		Science,
		Geography,
		biology
	}
	public enum MediaType
	{
		Pdf,
		Word,
		Picture,
		Video,
		Audio,
		Undefined,
		PowerPoint
	}

	public enum BannerPlace
	{
		Top , 
		Bottom
	}

	public enum UserType
	{
		Manager , 
		Teacher , 
		Student
	}


}
