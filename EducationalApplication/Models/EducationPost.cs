﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Models
{
	public class EducationPost
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string IconUrl { get; set; }
		public ICollection<Media> Medias { get; set; }
		public int Number { get; set; }
		public accessType AccessType { get; set; }
		public bool Pin { get; set; }

		public category category { get; set; }

		public int TeacherId { get; set; }

		public Teacher Teacher { get; set; }
	}

	public enum accessType
	{
		Free,
		IncludesCost
	}

	public enum category
	{
		Math ,
		Science ,
		Geography ,
		biology
	}

}
