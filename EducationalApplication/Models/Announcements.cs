﻿using EducationalApplication.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace EducationalApplication.Models
{
	public class Announcements
	{
		public int Id { get; set; }
		[Required]
		public string Text { get; set; }
		public DateTime CreateDate { get; set; } = DateTime.Now;
		public DateTime ExpireDate { get; set; } = DateTime.Now;
		public string ApplicationUserId { get; set; }
		public bool IsOnlyForTeacher { get; set; } = false; 
		public ApplicationUser ApplicationUser { get; set; }
		public int AvailableDays { get; set; }
	}
}
