﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Data;
using EducationalApplication.Models;
using EducationalApplication.Models.Enums;

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
		public string Name { get; set; }
		public string Id { get; set; }
	}
	public class ShortStuudentViewModel
	{
		public int Id { get; set; }
	}

	public class StudentAndCustomGroupViewModel 
	{
		public List<int> StudentId { get; set; }
		public int GroupId { get; set; }
	}
	public class TeacherAndCustomGroupViewModel
	{
		public List<string> TeacherId { get; set; }
		public int GroupId { get; set; }
	}
	public class  RemoveUserFromCustomGroupViewModel<TType>
	{
		public List<TType> UsersId { get; set; }

		public int CustomGrupId { get; set; }
	}

	public class AddViewToPostViewModel<TType>
	{
		public TType UserId { get; set; }
		public int EducationPostId { get; set; }
	}
	public class InputIdViewModel
	{
		public int Id { get; set; }
	}

	public class EducationPostViewModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string IconUrl { get; set; }
		public ICollection<Media> Medias { get; set; }
		public int Number { get; set; }
		public accessType AccessType { get; set; }
		public bool Pin { get; set; }
		public int? CategoryId { get; set; }
		public Category Category { get; set; }
		public string ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
		public ICollection<Comment> Comments { get; set; }
		public int ViewCount { get; set; }
		public EducationPostStatus Status { get; set; }

		public IEnumerable<int> Students { get; set; }
		public IEnumerable<int> Teachers { get; set; }
		public IEnumerable<CustomGroupsToEducationPostsViewModel>  CustomGroupsToEducationPosts { get; set; }



	}
	public class ShortEducationPostViewModel
	{
		public int Id { get; set; }
		public string Title { get; set; }

	}
	public class ArrayInputIdViewModel
	{
		public int[] Id { get; set; }
	}
	public class BannerViewModel
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public string SocialNetWorkLink { get; set; }
		public string Url { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime AvailableDate { get; set; }
		public int CreditDays { get; set; }
		public BannerPlace BannerPlace { get; set; }
	
		public ICollection<int> PostsInBanner { get; set; }
		public int? CategoryId { get; set; }
		public Category Category { get; set; }
		public string ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
		public bool ShowOnMainPage { get; set; }
		public BannerStatus BannerStatus { get; set; }
		public bool Pin { get; set; }
		public bool IsOnlyForTeacher { get; set; }
	}

	public class CustomGroupsToEducationPostsViewModel
	{
		public int  Id { get; set;  }
		public bool IsForStudent { get; set; }
		public int IdHelper { get; set; }

	}


	public class CustomGroupViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool IsForTeacher { get; set; }
		public DateTime Date { get; set; } = DateTime.Now;
		public string ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
		public List<UsersToCustomGroupsViewModel> UsersToCustomGroups { get; set; }


	}
	
	public class UsersToCustomGroupsViewModel
	{
		public int Id { get; set; }
		public int ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }

		public string StudentName { get; set; }
		public int? StudentsId { get; set; }
		public Students Students { get; set; }
		public int CustomGroupId { get; set; }
		public CustomGroup CustomGroup { get; set; }



	}






	}
