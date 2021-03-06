﻿using EducationalApplication.Data;
using EducationalApplication.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalApplication.Models
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }
		public bool IsOnlyForTeacher { get; set; } = false;
		public string ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
	}
}
