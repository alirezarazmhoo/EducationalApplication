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
			string[] _StudentList = new string[] { };
			string[] _TeacherList = new string[] { };
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
				_DbContext.EducationPosts.Add(model);
				_DbContext.SaveChanges();
				if (files != null && files.Count() > 0)
				{
					foreach (var item in files)
					{
						var fileName = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(item.FileName).ToLower(); ;
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
				if (StudentList != null && StudentList.Length > 0)
				{
					_StudentList = StudentList.Split(",");
					for (int i = 0; i < _StudentList.Count(); i++)
					{
						StudentId = Int32.Parse(_StudentList[i]);
						if (await _DbContext.Students.AnyAsync(s => s.Id == StudentId))
						{
							if (!usersToEducationPost.Any(s => s.StudentsId == StudentId))
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
							lenght = item.Length > 0 ? MediaSize.SizeSuffix(item.Length) :"0",
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
				//Studetns To Post
				if (TeacherList.Length > 0 || StudentList.Length > 0)
				{
					_DbContext.UsersToEducationPosts.RemoveRange(_DbContext.UsersToEducationPosts.Where(s => s.EducationPostId == MainItem.Id));
				}
				if (StudentList != null && StudentList.Length > 0)
				{
					_StudentList = StudentList.Split(",");
					for (int i = 0; i < _StudentList.Count(); i++)
					{
						StudentId = Int32.Parse(_StudentList[0]);

						if (await _DbContext.Students.AnyAsync(s => s.Id == StudentId))
						{
							if (!usersToEducationPost.Any(s => s.StudentsId == StudentId))
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
		public async Task<IEnumerable<EducationPost>> GetAll(string Id)
		{
			var Items = await _DbContext.EducationPosts.Include(s => s.Medias)/*.Where(s=>s.ApplicationUserId == Id)*/.OrderByDescending(s => s.Id).ToListAsync();
			return Items;
		}
		public async Task<EducationPost> GetById(int Id)
		{
			var Items = await _DbContext.EducationPosts.Include(s => s.Medias).Where(s => s.Id == Id).FirstOrDefaultAsync();
			return Items;
		}
		public async Task Remove(EducationPost model)
		{
			var MainItem = await GetById(model.Id);

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
			return await FindByCondition(s => s.CategoryId == Id).ToListAsync();
		}
		public async Task<IEnumerable<Comment>> GetEducationPostCommnet(int Id)
		{
			List<Comment> comments = new List<Comment>();
			var Item = _DbContext.EducationPosts.FirstOrDefaultAsync(s=>s.Id == Id);
			if(Item != null)
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

			if(Teacher != null)
			{
				var EducationPost = await _DbContext.EducationPosts.FirstOrDefaultAsync(s => s.Id == model.EducationPostId);

				if(await _DbContext.EducationPostViews.AnyAsync(s=>s.EducationPostId
				== model.EducationPostId && s.TeacherId == model.UserId) == false)
				{
					EducationPost.ViewCount += 1;

					educationPostView.EducationPostId = model.EducationPostId;
					educationPostView.TeacherId = model.UserId; 
				await	_DbContext.EducationPostViews.AddAsync(educationPostView);


				}
			  await	_DbContext.SaveChangesAsync();

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

	}
}
