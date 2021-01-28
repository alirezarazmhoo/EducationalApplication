using EducationalApplication.Data;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
using EducationalApplication.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EducationalApplication.Models.Enums;
using EducationalApplication.Models.ViewModels;

namespace EducationalApplication.Services
{
	public class EducationPostRepo : RepositoryBase<EducationPost>, IEducationPostRepo
	{


		public EducationPostRepo(ApplicationDbContext DbContext)
		: base(DbContext)
		{
			_DbContext = DbContext;

		}
		public async Task AddOrUpdate(EducationPost model, IFormFile Icon, IFormFile[] files, string TeacherList, string StudentList)
		{
			List<UsersToEducationPost> usersToEducationPost = new List<UsersToEducationPost>();
			var SettingItem = await _DbContext.Settings.FirstOrDefaultAsync();
			string[] _StudentList = new string[] { };
			string[] _TeacherList = new string[] { };
			string[] _GroupsIdes = new string[] { };
			int groupId = 0;
			string TeacherId = string.Empty;
			int StudentId = 0;
			if (model.Id == 0)
			{
				if (Icon != null && Icon.Length > 1)
				{
					var fileName = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(Icon.FileName).ToLower(); ;
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Post\Icon", fileName);
					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						Icon.CopyTo(fileStream);
					}
					model.IconUrl = "/Upload/Post/Icon/" + fileName;
				}
				model.Status = SettingItem.NeedEducationPostsToAccept ? EducationPostStatus.Waiting : EducationPostStatus.Accepted;
				_DbContext.EducationPosts.Add(model);
				_DbContext.SaveChanges();
				if (files != null && files.Count() > 0)
				{
					foreach (var item in files)
					{
						var fileName = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(item.FileName).ToLower();
						var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Post\Media\", fileName);
						using (var stream = new FileStream(filePath, FileMode.Create))
						{
							item.CopyTo(stream);
						}
						_DbContext.Medias.Add(new Media()
						{
							Title = Path.GetFileName(item.FileName),
							Url = "/Upload/Post/Media/" + fileName,
							EducationPostId = model.Id,
							lenght = item.Length > 0 ? MediaSize.SizeSuffix(item.Length) : "0",
							DurationTime = FormatCheck.GetFormat(item) == MediaType.Audio ? DurationFile.GetDuration(filePath).ToString() : "0",
							MediaType = FormatCheck.GetFormat(item)
						});
					}
				}
				//Studetns To Post
				//if (!string.IsNullOrEmpty(model.GroupsIds))
				//{
				//	_GroupsIdes = model.GroupsIds.Split(",");
				//	for (int i = 0; i < _GroupsIdes.Count(); i++)
				//	{
				//		groupId = Int32.Parse(_GroupsIdes[i]);
				//		var R = await _DbContext.UsersToCustomGroups.Where(s => s.CustomGroupId == groupId).Select(s=>s.StudentsId).ToListAsync();

				//		for (int j = 0; j < R.Count(); j++)
				//		{
				//			_StudentList[j] = R[j].ToString(); 
				//		}
				//	}
				//}
				if (!string.IsNullOrEmpty(StudentList))
				{
					_StudentList = StudentList.Split(",");
					for (int i = 0; i < _StudentList.Count(); i++)
					{
						StudentId = Int32.Parse(_StudentList[i]);
						if (await _DbContext.Students.AnyAsync(s => s.Id == StudentId))
						{
							if (!usersToEducationPost.Any(s => s.StudentsId == StudentId && s.EducationPostId == model.Id))
							{
								usersToEducationPost.Add(new UsersToEducationPost() { StudentsId = StudentId, EducationPostId = model.Id });
							}
						}
					}
				}
				if (TeacherList != null && TeacherList.Length > 0)
				{
					_TeacherList = TeacherList.Split(",");
					for (int i = 0; i < _TeacherList.Count(); i++)
					{
						TeacherId = _TeacherList[i];
						if (await _DbContext.Users.AnyAsync(s => s.Id == TeacherId))
						{
							if (!usersToEducationPost.Any(s => s.ApplicationUserId == TeacherId))
							{

								usersToEducationPost.Add(new UsersToEducationPost() { ApplicationUserId = TeacherId, EducationPostId = model.Id });
							}
						}
					}
				}
				if (_TeacherList.Length > 0 || _StudentList.Length > 0)
				{
					await _DbContext.UsersToEducationPosts.AddRangeAsync(usersToEducationPost);
				}
			}
			else
			{
				var MainItem = await GetById(model.Id);
				if (Icon != null && Icon.Length > 1)
				{
					if (!string.IsNullOrEmpty(MainItem.IconUrl))
					{
						File.Delete($"wwwroot/{MainItem.IconUrl}");
					}
					var fileName = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(Icon.FileName).ToLower();
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Post\Icon", fileName);
					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						Icon.CopyTo(fileStream);
					}
					MainItem.IconUrl = "/Upload/Post/Icon/" + fileName;
				}
				if (files != null && files.Count() > 0)
				{
					foreach (var item in files)
					{
						var fileName = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(item.FileName).ToLower();
						var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Post\Media\", fileName);
						using (var stream = new FileStream(filePath, FileMode.Create))
						{
							item.CopyTo(stream);
						}
						_DbContext.Medias.Add(new Media()
						{
							Title = Path.GetFileName(item.FileName),
							Url = "/Upload/Post/Media/" + fileName,
							EducationPostId = model.Id,
							lenght = item.Length > 0 ? MediaSize.SizeSuffix(item.Length) : "0",
							DurationTime = FormatCheck.GetFormat(item) == MediaType.Audio ? DurationFile.GetDuration(filePath).ToString() : "0",
							MediaType = FormatCheck.GetFormat(item)
						});
					}
				}
				MainItem.AccessType = model.AccessType;
				MainItem.CategoryId = model.CategoryId;
				MainItem.Description = model.Description;
				MainItem.Number = model.Number;
				MainItem.Title = model.Title;
				MainItem.Status = SettingItem.NeedEducationPostsToAccept ? EducationPostStatus.Waiting : EducationPostStatus.Accepted;
				//Studetns To Post
				if (!string.IsNullOrEmpty(StudentList))
				{
					_DbContext.UsersToEducationPosts.RemoveRange(_DbContext.UsersToEducationPosts.Where(s => s.EducationPostId == MainItem.Id));
				}
				if (!string.IsNullOrEmpty(StudentList))
				{
					_StudentList = StudentList.Split(",");
					for (int i = 0; i < _StudentList.Count(); i++)
					{
						StudentId = Int32.Parse(_StudentList[i]);

						if (await _DbContext.Students.AnyAsync(s => s.Id == StudentId))
						{
							if (usersToEducationPost.Any(s => s.StudentsId == StudentId && s.EducationPostId == MainItem.Id) ==false)
							{
								usersToEducationPost.Add(new UsersToEducationPost() { StudentsId = StudentId, EducationPostId = model.Id });
							}
						}
					}
				}

				if (TeacherList != null && TeacherList.Length > 0)
				{
					_TeacherList = TeacherList.Split(",");
					for (int i = 0; i < _TeacherList.Count(); i++)
					{
						TeacherId = _TeacherList[0];
						if (await _DbContext.Users.AnyAsync(s => s.Id == TeacherId))
						{
							if (!usersToEducationPost.Any(s => s.ApplicationUserId == TeacherId))
							{
								usersToEducationPost.Add(new UsersToEducationPost() { ApplicationUserId = TeacherId, EducationPostId = model.Id });
							}
						}
					}
				}
				if (_TeacherList.Length > 0 || _StudentList.Length > 0)
				{
					await _DbContext.UsersToEducationPosts.AddRangeAsync(usersToEducationPost);
				}
			}
		}
		public async Task<IEnumerable<EducationPostViewModel>> GetAll(string Id)
		{
			var SettingItem = await _DbContext.Settings.FirstOrDefaultAsync();
			List<EducationPost> Items = new List<EducationPost>();
			List<EducationPostViewModel> educationPostViewModel = new List<EducationPostViewModel>();

			if (SettingItem.NeedEducationPostsToAccept)
			{
				Items = await _DbContext.EducationPosts.Where(s => s.Status == EducationPostStatus.Accepted).Include(s => s.Medias).Include(s=>s.UsersToEducationPosts)/*.Where(s=>s.ApplicationUserId == Id)*/.OrderByDescending(s => s.Id).ToListAsync();
			}
			else
			{
				Items = await _DbContext.EducationPosts.Include(s => s.Medias).Include(s => s.UsersToEducationPosts)/*.Where(s=>s.ApplicationUserId == Id)*/.OrderByDescending(s => s.Id).ToListAsync();
			}
			foreach (var item in Items)
			{
				educationPostViewModel.Add(new EducationPostViewModel() { AccessType = item.AccessType, ApplicationUserId = item.ApplicationUserId, CategoryId = item.CategoryId, Description = item.Description, IconUrl = item.IconUrl, Id = item.Id, Medias = item.Medias, Number = item.Number, Pin = item.Pin, Status = item.Status, Title = item.Title, ViewCount = item.ViewCount  , Students = item.UsersToEducationPosts.Select(s=>s.StudentsId.Value) });
			}
			return educationPostViewModel;
		}
		public async Task<EducationPostViewModel> GetById(int Id)
		{
			EducationPost Item = new EducationPost();
			EducationPostViewModel educationPostViewModel = new EducationPostViewModel();
			List<Media> mediasList = new List<Media>();
			var SettingItem = await _DbContext.Settings.FirstOrDefaultAsync();

			if (SettingItem.NeedEducationPostsToAccept)
			{
				Item = await _DbContext.EducationPosts.Include(s => s.Medias).Include(s=>s.UsersToEducationPosts).Where(s => s.Id == Id && s.Status == EducationPostStatus.Accepted).FirstOrDefaultAsync();
			}
			else
			{
				Item = await _DbContext.EducationPosts.Include(s => s.Medias).Include(s => s.UsersToEducationPosts).Where(s => s.Id == Id).FirstOrDefaultAsync();
			}
			if(Item != null)
			{
			educationPostViewModel.AccessType = Item.AccessType;
			educationPostViewModel.Title = Item.Title;
			educationPostViewModel.ViewCount = Item.ViewCount;
			educationPostViewModel.Status = Item.Status;
			educationPostViewModel.Number = Item.Number;
			educationPostViewModel.Id = Item.Id;
			educationPostViewModel.IconUrl = Item.IconUrl;
			educationPostViewModel.Description = Item.Description;
			educationPostViewModel.CategoryId = Item.CategoryId;
			educationPostViewModel.ApplicationUserId = Item.ApplicationUserId;
			educationPostViewModel.AccessType = Item.AccessType;
			educationPostViewModel.Students = Item.UsersToEducationPosts.Select(s=>s.StudentsId.Value);  
			foreach (var item in Item.Medias)
			{
				mediasList.Add(new Media() { DurationTime = item.DurationTime, EducationPostId = item.EducationPostId, Id = item.Id, lenght = item.lenght, Title = item.Title, Url = item.Url, MediaType = FormatCheck.GetFormat(item.Url) });
			}
			educationPostViewModel.Medias = mediasList;
			return educationPostViewModel;
			}
			return null; 
		}
		public async Task Remove(EducationPostViewModel model)
		{
			var MainItem = await _DbContext.EducationPosts.FirstOrDefaultAsync(s=>s.Id == model.Id);
			await GetById(model.Id);
			if (!string.IsNullOrEmpty(MainItem.IconUrl))
			{
				File.Delete($"wwwroot/{MainItem.IconUrl}");
			}
			if (MainItem.Medias.Count > 0)
			{
				foreach (var item in MainItem.Medias)
				{
					File.Delete($"wwwroot/{item.Url}");
				}
			}
			Delete(MainItem);
		}
		public async Task RemoveFile(int Id)
		{
			Media Item = await _DbContext.Medias.FindAsync(Id);
			if (Item != null)
			{
				File.Delete($"wwwroot/{Item.Url}");
			}

			_DbContext.Medias.Remove(Item);
		}
		public async Task<IEnumerable<EducationPost>> GetByCategory(int Id)
		{
			var SettingItem = await _DbContext.Settings.FirstOrDefaultAsync();
			if (SettingItem.NeedEducationPostsToAccept)
			{
				return await FindByCondition(s => s.CategoryId == Id && s.Status == EducationPostStatus.Accepted).ToListAsync();
			}
			else
			{
				return await FindByCondition(s => s.CategoryId == Id).ToListAsync();
			}
		}
		public async Task<IEnumerable<Comment>> GetEducationPostCommnet(int Id)
		{
			List<Comment> comments = new List<Comment>();
			var Item = await _DbContext.EducationPosts.FirstOrDefaultAsync(s => s.Id == Id);
			if (Item != null)
			{
				var Comments = await _DbContext.Comments.Where(s => s.EducationPostId == Id).ToListAsync();
				comments.AddRange(Comments);
				return comments;
			}
			else
			{
				return null;
			}
		}

		public async Task TeacherAddView(AddViewToPostViewModel<string> model)
		{
			var Teacher = await _DbContext.Users.FirstOrDefaultAsync(s => s.Id == model.UserId);

			EducationPostView educationPostView = new EducationPostView();

			if (Teacher != null)
			{
				var EducationPost = await _DbContext.EducationPosts.FirstOrDefaultAsync(s => s.Id == model.EducationPostId);

				if (await _DbContext.EducationPostViews.AnyAsync(s => s.EducationPostId
				 == model.EducationPostId && s.TeacherId == model.UserId) == false)
				{
					EducationPost.ViewCount += 1;

					educationPostView.EducationPostId = model.EducationPostId;
					educationPostView.TeacherId = model.UserId;
					await _DbContext.EducationPostViews.AddAsync(educationPostView);


				}
				await _DbContext.SaveChangesAsync();

			}
		}

		public async Task StudentAddView(AddViewToPostViewModel<int> model)
		{
			EducationPostView educationPostView = new EducationPostView();

			var Student = await _DbContext.Students.FirstOrDefaultAsync(s => s.Id == model.UserId);
			if (Student != null)
			{
				var EducationPost = await _DbContext.EducationPosts.FirstOrDefaultAsync(s => s.Id == model.EducationPostId);

				if (await _DbContext.EducationPostViews.AnyAsync(s => s.EducationPostId
				 == model.EducationPostId && s.StudentId == model.UserId) == false)
				{
					EducationPost.ViewCount += 1;

					educationPostView.EducationPostId = model.EducationPostId;
					educationPostView.StudentId = model.UserId;
					await _DbContext.EducationPostViews.AddAsync(educationPostView);
				}
				await _DbContext.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<EducationPost>> AdminGetAll()
		{
			List<EducationPost> Items = new List<EducationPost>();
			Items = await _DbContext.EducationPosts.Include(s => s.ApplicationUser).OrderByDescending(s => s.Id).ToListAsync();
			return Items;
		}
		public async Task Accept(int Id)
		{
			var Item = await _DbContext.EducationPosts.FirstOrDefaultAsync(s => s.Id == Id);
			if (Item != null)
			{
				Item.Status = EducationPostStatus.Accepted;
			}
		}
		public async Task Reject(int Id)
		{
			var Item = await _DbContext.EducationPosts.FirstOrDefaultAsync(s => s.Id == Id);
			if (Item != null)
			{
				Item.Status = EducationPostStatus.Rejected;
			}
		}
		public async Task<EducationPost> AdminGetById(int Id)
		{
			EducationPost Item = new EducationPost();
			Item = await _DbContext.EducationPosts.Include(s => s.Medias).Include(s => s.ApplicationUser).Where(s => s.Id == Id).FirstOrDefaultAsync();
			return Item;
		}
		public async Task<int> CommentCount(int Id)
		{
			EducationPost educationPostItem = await _DbContext.EducationPosts.FirstOrDefaultAsync(s => s.Id == Id);
			if (educationPostItem != null)
			{
				return await _DbContext.Comments.Where(s => s.EducationPostId == Id).CountAsync();
			}
			else
			{
				return 0;
			}
		}



	}
}
