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
		public async Task AddOrUpdate(EducationPost model, IFormFile Icon, IFormFile[] files, string TeacherList, string StudentList, string customgrouplist , string teachercustomgrouplist)
		{
			#region Parameters
			List<UsersToEducationPost> usersToEducationPost = new List<UsersToEducationPost>();
			List<UsersToEducationPost> usersToEducationPosthelper = new List<UsersToEducationPost>();
			List<CustomGroupsToEducationPosts> customGroupsToEducationPosts = new List<CustomGroupsToEducationPosts>();
			List<UsersToEducationPost> usersincustomgroup = new List<UsersToEducationPost>();
			ApplicationUser applicationUserItem = new ApplicationUser();
			var SettingItem = await _DbContext.Settings.FirstOrDefaultAsync();
			string[] _StudentList = new string[] { };
			string[] _TeacherList = new string[] { };
			string[] _GroupsIdes = new string[] { };
			string[] _TeacherGroupsIdes = new string[] { };
			string TeacherId = string.Empty;
			int StudentId = 0;
			int groupId = 0;
			int teachergroupId = 0;
			#endregion
			#region Main
			if (model.Id == 0)
			{
				if (Icon != null && Icon.Length > 1)
				{
					var fileName = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(Icon.FileName).ToLower();
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
				//CustomGroupsStudetns To Post
				if (!string.IsNullOrEmpty(customgrouplist))
				{
					_GroupsIdes = customgrouplist.Split(",");
					for (int i = 0; i < _GroupsIdes.Count(); i++)
					{
						groupId = Int32.Parse(_GroupsIdes[i]);
						if (await _DbContext.CustomGroups.AnyAsync(s => s.Id.Equals(groupId)))
						{
							customGroupsToEducationPosts.Add(new CustomGroupsToEducationPosts() { CustomGroupId = groupId, EducationPostId = model.Id });
							_DbContext.CustomGroupsToEducationPosts.Add(new CustomGroupsToEducationPosts() { CustomGroupId = groupId, EducationPostId = model.Id });
						}
					}
					foreach (var item in customGroupsToEducationPosts)
					{
						if (await _DbContext.CustomGroups.AnyAsync(s => s.Id.Equals(item.CustomGroupId)))
						{
							foreach (var item2 in await _DbContext.UsersToCustomGroups.Where(s => s.CustomGroupId.Equals(item.CustomGroupId)).ToListAsync())
							{
								if (!usersToEducationPost.Any(s => s.StudentsId == item2.StudentsId && s.EducationPostId == model.Id))
								{
									usersToEducationPost.Add(new UsersToEducationPost() { StudentsId = item2.StudentsId, EducationPostId = model.Id });
								}
							}
						}
					}
				}
				//End 
				//CustomGroupsTeachers To Post
				applicationUserItem = await _DbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(model.ApplicationUserId));
				if (applicationUserItem != null && applicationUserItem.UserType == UserType.Manager)
				{
					if (!string.IsNullOrEmpty(teachercustomgrouplist))
					{
						_TeacherGroupsIdes = teachercustomgrouplist.Split(",");
						for (int i = 0; i < _TeacherGroupsIdes.Count(); i++)
						{
							teachergroupId = Int32.Parse(_TeacherGroupsIdes[i]);
							if (await _DbContext.CustomGroups.AnyAsync(s => s.Id.Equals(teachergroupId)))
							{
								customGroupsToEducationPosts.Add(new CustomGroupsToEducationPosts() { CustomGroupId = teachergroupId, EducationPostId = model.Id });
							}
						}
						foreach (var item in customGroupsToEducationPosts)
						{
							if (await _DbContext.CustomGroups.AnyAsync(s => s.Id.Equals(item.CustomGroupId)))
							{
								foreach (var item2 in await _DbContext.UsersToCustomGroups.Where(s => s.CustomGroupId.Equals(item.CustomGroupId)).ToListAsync())
								{
									if (!usersToEducationPost.Any(s => s.ApplicationUserId == item2.ApplicationUserId && s.EducationPostId == model.Id))
									{
										usersToEducationPost.Add(new UsersToEducationPost
										{
											ApplicationUserId = item2.ApplicationUserId,
											EducationPostId = model.Id
										});
									}
								}
							}
						}
					}
				}
				//End
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
				if (! string.IsNullOrEmpty(TeacherList))
				{
					_TeacherList = TeacherList.Split(",");
					for (int i = 0; i < _TeacherList.Count(); i++)
					{
						TeacherId = _TeacherList[i];
						var TecherItemHelper = await _DbContext.Users.FirstOrDefaultAsync(s => s.IdHelper == Convert.ToInt32(TeacherId)); 
						if (TecherItemHelper !=null)
						{
							if (!usersToEducationPost.Any(s => s.ApplicationUserId == TecherItemHelper.Id && s.EducationPostId == model.Id))
							{
								usersToEducationPost.Add(new UsersToEducationPost() { ApplicationUserId = TecherItemHelper.Id, EducationPostId = model.Id });
							}
						}
					}
				}
				if (usersToEducationPost.Count() > 0)
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
				//StudentGroupToPost
				applicationUserItem = await _DbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(model.ApplicationUserId));
				if (applicationUserItem != null && applicationUserItem.UserType == UserType.Manager)
				{
					if (string.IsNullOrEmpty(teachercustomgrouplist))
					{
						_DbContext.CustomGroupsToEducationPosts.RemoveRange(await _DbContext.CustomGroupsToEducationPosts.Where(s => s.EducationPostId == model.Id && s.CustomGroup.IsForTeacher).ToListAsync());
					}
					if (string.IsNullOrEmpty(TeacherList) && string.IsNullOrEmpty(teachercustomgrouplist))
					{
						_DbContext.UsersToEducationPosts.RemoveRange(_DbContext.UsersToEducationPosts.Where(s => s.EducationPostId == MainItem.Id && s.ApplicationUserId !=null));
					}

					if (!string.IsNullOrEmpty(teachercustomgrouplist))
					{
						_DbContext.CustomGroupsToEducationPosts.RemoveRange(_DbContext.CustomGroupsToEducationPosts.Where(s => s.EducationPostId == MainItem.Id));
						_TeacherGroupsIdes = teachercustomgrouplist.Split(",");
						for (int i = 0; i < _TeacherGroupsIdes.Count(); i++)
						{
							teachergroupId = Int32.Parse(_TeacherGroupsIdes[i]);
							if (await _DbContext.CustomGroups.AnyAsync(s => s.Id.Equals(teachergroupId)))
							{
								customGroupsToEducationPosts.Add(new CustomGroupsToEducationPosts() { CustomGroupId = teachergroupId, EducationPostId = model.Id });
								_DbContext.CustomGroupsToEducationPosts.Add(new CustomGroupsToEducationPosts()
								{
									CustomGroupId = teachergroupId,
									EducationPostId = model.Id
								});
							}
						}
						foreach (var item in customGroupsToEducationPosts)
						{
							if (await _DbContext.CustomGroups.AnyAsync(s => s.Id.Equals(item.CustomGroupId)))
							{
								foreach (var item2 in await _DbContext.UsersToCustomGroups.Where(s => s.CustomGroupId.Equals(item.CustomGroupId)).ToListAsync())
								{
									if (!usersToEducationPost.Any(s => s.ApplicationUserId == item2.ApplicationUserId && s.EducationPostId == model.Id))
									{
										usersToEducationPost.Add(new UsersToEducationPost
										{
											ApplicationUserId = item2.ApplicationUserId,
											EducationPostId = model.Id
										});
									}
								}
							}
						}
						foreach (var item in usersToEducationPost)
						{
							var UserEducationPostItem = await _DbContext.UsersToEducationPosts.FirstOrDefaultAsync(s => s.ApplicationUserId == item.ApplicationUserId && s.EducationPostId == item.EducationPostId);
							if (UserEducationPostItem != null)
							{
							_DbContext.UsersToEducationPosts.Remove(UserEducationPostItem);
							}
						}
					}
				}

				if (string.IsNullOrEmpty(customgrouplist))
				{
					_DbContext.CustomGroupsToEducationPosts.RemoveRange(await _DbContext.CustomGroupsToEducationPosts.Where(s => s.EducationPostId == model.Id && s.CustomGroup.IsForTeacher == false).ToListAsync());
				}
				if (string.IsNullOrEmpty(StudentList) && string.IsNullOrEmpty(customgrouplist))
				{
					_DbContext.UsersToEducationPosts.RemoveRange(_DbContext.UsersToEducationPosts.Where(s => s.EducationPostId == MainItem.Id && s.StudentsId.HasValue));
				}
				if (!string.IsNullOrEmpty(customgrouplist))
				{
				
					if(await _DbContext.CustomGroupsToEducationPosts.Where(s=>s.EducationPostId.Equals(model.Id)).CountAsync()> 0)
					{
						var CustomGroupList = await _DbContext.CustomGroupsToEducationPosts.Where(s => s.EducationPostId.Equals(model.Id)).ToListAsync();
						foreach (var item in CustomGroupList)
						{
							var StudentItems = await _DbContext.UsersToCustomGroups.Where(s => s.CustomGroupId.Equals(item.CustomGroupId)).Select(s => s.StudentsId).ToListAsync();
							foreach (var item2 in StudentItems)
							{
								var studentitem = await _DbContext.UsersToEducationPosts.FirstOrDefaultAsync(s => s.StudentsId == item2 && s.EducationPostId == model.Id); 
								if(studentitem != null)
								{
								 _DbContext.UsersToEducationPosts.Remove(studentitem);
								}
							}
						}
						_DbContext.CustomGroupsToEducationPosts.RemoveRange(await _DbContext.CustomGroupsToEducationPosts.Where(s => s.EducationPostId == model.Id).ToListAsync());
					}
				
					customGroupsToEducationPosts.Clear();
					_GroupsIdes = customgrouplist.Split(",");
					for (int i = 0; i < _GroupsIdes.Count(); i++)
					{
						groupId = Int32.Parse(_GroupsIdes[i]);
						if (await _DbContext.CustomGroups.AnyAsync(s => s.Id.Equals(groupId)))
						{
							customGroupsToEducationPosts.Add(new CustomGroupsToEducationPosts() { CustomGroupId = groupId, EducationPostId = model.Id });
							_DbContext.CustomGroupsToEducationPosts.Add(new CustomGroupsToEducationPosts() { CustomGroupId = groupId, EducationPostId = model.Id });
						}
					}
					foreach (var item in customGroupsToEducationPosts)
					{
						if (await _DbContext.CustomGroups.AnyAsync(s => s.Id.Equals(item.CustomGroupId)))
						{
							foreach (var item2 in await _DbContext.UsersToCustomGroups.Where(s => s.CustomGroupId.Equals(item.CustomGroupId)).ToListAsync())
							{
								if (!usersToEducationPost.Any(s => s.StudentsId == item2.StudentsId && s.EducationPostId == model.Id))
								{
									usersToEducationPost.Add(new UsersToEducationPost
									{
										StudentsId = item2.StudentsId,
										EducationPostId = model.Id
									});
								}
							}
						}
					}
				
				}
				//End
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
							if (usersToEducationPost.Any(s => s.StudentsId == StudentId && s.EducationPostId == MainItem.Id) == false)
							{
								usersToEducationPost.Add(new UsersToEducationPost() { StudentsId = StudentId, EducationPostId = model.Id });
							}
						}
					}
				}
				if (! string.IsNullOrEmpty(TeacherList))
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
		        if (usersToEducationPost.Count() > 0)
			    {
					await _DbContext.UsersToEducationPosts.AddRangeAsync(usersToEducationPost);
				}
			}
			#endregion
		}
		public async Task<IEnumerable<EducationPostViewModel>> GetAll(string Id)
		{
			var SettingItem = await _DbContext.Settings.FirstOrDefaultAsync();
			List<EducationPost> Items = new List<EducationPost>();
			List<EducationPostViewModel> educationPostViewModel = new List<EducationPostViewModel>();
			if (SettingItem.NeedEducationPostsToAccept)
			{
				Items = await _DbContext.EducationPosts.Where(s => s.Status == EducationPostStatus.Accepted).Include(s => s.Medias).Include(s => s.UsersToEducationPosts).Where(s=>s.ApplicationUserId == Id).OrderByDescending(s => s.Id).ToListAsync();
			}
			else
			{
				Items = await _DbContext.EducationPosts.Include(s => s.Medias).Include(s => s.UsersToEducationPosts).Include(s=>s.CustomGroupsToEducationPosts).Where(s=>s.ApplicationUserId == Id).OrderByDescending(s => s.Id).ToListAsync();
			}
			foreach (var item in Items)
			{
				educationPostViewModel.Add(new EducationPostViewModel() { AccessType = item.AccessType, ApplicationUserId = item.ApplicationUserId, CategoryId = item.CategoryId, Description = item.Description, IconUrl = item.IconUrl, Id = item.Id, Medias = item.Medias, Number = item.Number, Pin = item.Pin, Status = item.Status, Title = item.Title, ViewCount = item.ViewCount, Students = GetStudentsFromEducationPost(item.Id) , Teachers = GetTeachersFromEducationPost(item.Id) ,  CustomGroupsToEducationPosts = GetGroups(item.Id) });
			}
			return educationPostViewModel.OrderByDescending(s => s.Pin == true);
		}
		private IEnumerable<int> GetTeachersFromEducationPost(int id)
		{
			List<int> MainList = new List<int>();

			foreach (var item in _DbContext.UsersToEducationPosts.Include(s=>s.ApplicationUser).Where(s => s.EducationPostId.Equals(id)).ToList())
			{
				if (! string.IsNullOrEmpty(item.ApplicationUserId))
				{
					MainList.Add(item.ApplicationUser.IdHelper);
				}
			}
			return MainList;
		}
		private IEnumerable<int> GetStudentsFromEducationPost(int id)
		{
			List<int> MainList = new List<int>();
			foreach (var item in _DbContext.UsersToEducationPosts.Where(s=>s.EducationPostId.Equals(id)).ToList())
			{
				if (item.StudentsId.HasValue)
				{
					MainList.Add(item.StudentsId.Value);
				}
			}
			return MainList;
		}
		private IEnumerable<CustomGroupsToEducationPostsViewModel> GetGroups(int id)
		{
			var customGroupsItem =  _DbContext.CustomGroupsToEducationPosts.Where(s => s.EducationPostId == id).ToList();
			List<CustomGroupsToEducationPostsViewModel> customGroupsToEducationPostsViewModel = new List<CustomGroupsToEducationPostsViewModel>();
			foreach (var item in customGroupsItem)
			{
				var CustomGropItem = _DbContext.CustomGroups.Include(s=>s.ApplicationUser).FirstOrDefault(s => s.Id.Equals(item.CustomGroupId));
				if (CustomGropItem != null)
				{
					customGroupsToEducationPostsViewModel.Add(new CustomGroupsToEducationPostsViewModel() { Id = item.CustomGroupId, IdHelper = item.EducationPost.ApplicationUser.IdHelper, IsForStudent = CheckCustomGroupType(item.CustomGroupId) });
				}
			}
			return customGroupsToEducationPostsViewModel; 
		}
		private bool CheckCustomGroupType(int Id)
		{
			CustomGroup customGroupItem = _DbContext.CustomGroups.FirstOrDefault(s=>s.Id.Equals(Id));
			
			if(_DbContext.UsersToCustomGroups.Any(s=>s.CustomGroupId == customGroupItem.Id && s.StudentsId.HasValue))
			{
				return true; 
			}
			else
			{
				return false; 
			}
		}
		public async Task<EducationPostViewModel> GetById(int Id)
		{
			EducationPost Item = new EducationPost();
			EducationPostViewModel educationPostViewModel = new EducationPostViewModel();
			List<Media> mediasList = new List<Media>();
			var SettingItem = await _DbContext.Settings.FirstOrDefaultAsync();

			if (SettingItem.NeedEducationPostsToAccept)
			{
				Item = await _DbContext.EducationPosts.Include(s => s.Medias).Include(s => s.UsersToEducationPosts).Where(s => s.Id == Id && s.Status == EducationPostStatus.Accepted).FirstOrDefaultAsync();
			}
			else
			{
				Item = await _DbContext.EducationPosts.Include(s => s.Medias).Include(s => s.UsersToEducationPosts).Where(s => s.Id == Id).FirstOrDefaultAsync();
			}
			if (Item != null)
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
				educationPostViewModel.Students = Item.UsersToEducationPosts.Select(s => s.StudentsId.Value);
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
			var MainItem = await _DbContext.EducationPosts.FirstOrDefaultAsync(s => s.Id == model.Id);
			List<Favorit> favorits = new List<Favorit>();
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

			_DbContext.Favorits.RemoveRange(await _DbContext.Favorits.Where(s => s.EducationPostId == model.Id).ToListAsync());
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
				var Comments = await _DbContext.Comments.Include(s => s.ApplicationUser).Include(s => s.Students).Where(s => s.EducationPostId == Id && s.CommentStatus == CommentStatus.Accepted).ToListAsync();
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
				return await _DbContext.Comments.Where(s => s.EducationPostId == Id && s.CommentStatus == CommentStatus.Accepted).CountAsync();
			}
			else
			{
				return 0;
			}
		}
		public async Task<IEnumerable<EducationPostViewModel>> GetRelatedEducationPostsInCateogry(int Id)
		{
			Category categoryItem = await _DbContext.Categories.FirstOrDefaultAsync(s => s.Id.Equals(Id));
			List<EducationPostViewModel> educationPostViewModels = new List<EducationPostViewModel>();
			if (categoryItem != null)
			{
				foreach (var item in await _DbContext.EducationPosts.Where(s => s.CategoryId.Equals(categoryItem.Id)).ToListAsync())
				{
					educationPostViewModels.Add(new EducationPostViewModel() { AccessType = item.AccessType, ApplicationUserId = item.ApplicationUserId, CategoryId = item.CategoryId, Description = item.Description, IconUrl = item.IconUrl, Id = item.Id, Medias = item.Medias, Number = item.Number, Pin = item.Pin, Status = item.Status, Title = item.Title, ViewCount = item.ViewCount, Students = item.UsersToEducationPosts.Select(s => s.StudentsId.Value) });
				}
				return educationPostViewModels;
			}
			else
			{
				return null;
			}
		}
		public async Task<IEnumerable<EducationPostViewModel>> GetEducationPostByArray(string Id)
		{
			List<EducationPost> educationPosts = new List<EducationPost>();
			List<EducationPostViewModel> shortEducationPostViewModel = new List<EducationPostViewModel>();
			string[] _EducationIdes = new string[] { };
			int EducationPostId = 0;
			if (!string.IsNullOrEmpty(Id))
			{
				_EducationIdes = Id.Split(",");
				for (int i = 0; i < _EducationIdes.Count(); i++)
				{
					EducationPostId = Int32.Parse(_EducationIdes[i]);

					if (await _DbContext.EducationPosts.AnyAsync(s => s.Id.Equals(EducationPostId)))
					{
						var educationpostitem = await _DbContext.EducationPosts.Include(s => s.Medias).Include(s => s.UsersToEducationPosts).FirstOrDefaultAsync(s => s.Id.Equals(EducationPostId));
						shortEducationPostViewModel.Add(new EducationPostViewModel() { AccessType = educationpostitem.AccessType, ApplicationUserId = educationpostitem.ApplicationUserId, CategoryId = educationpostitem.CategoryId, Description = educationpostitem.Description, IconUrl = educationpostitem.IconUrl, Id = educationpostitem.Id, Medias = educationpostitem.Medias, Number = educationpostitem.Number, Pin = educationpostitem.Pin, Status = educationpostitem.Status, Title = educationpostitem.Title, ViewCount = educationpostitem.ViewCount, Students = educationpostitem.UsersToEducationPosts.Select(s => s.StudentsId.Value) });
					}
				}
				return shortEducationPostViewModel;
			}
			else
			{
				return null;
			}
		}
	}
}
