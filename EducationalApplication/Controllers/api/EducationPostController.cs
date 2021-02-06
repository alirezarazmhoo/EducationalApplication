using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
using EducationalApplication.Models.ViewModels;
using EducationalApplication.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationalApplication.Controllers.api
{
	[Route("api/[controller]")]
	[ApiController]
	public class EducationPostController : ControllerBase
	{
		private IUnitOfWorkRepo _unitofwork;
		public EducationPostController(IUnitOfWorkRepo unitOfWork)
		{
			_unitofwork = unitOfWork;
		}
		[Route("Add")]
		[HttpPost]
		public async Task<ApiModel> Add([FromForm]EducationPost model, IFormFile Icon, IFormFile[] file )
		{
			try
			{
				if (await _unitofwork.IUserRepo.GetById(model.ApplicationUserId) == null)
				{
					return ApiResponse.Fail(null, 404, $"معلمی با ای دی {model.ApplicationUserId} یافت نشد.");
				}
				var TeacherList = model.TeacherListToPost;
				var StudentList =model.StudentListToPost;
				await _unitofwork.IEducationPostRepo.AddOrUpdate(model, Icon, file  , TeacherList , StudentList ,model.GroupsIds,model.TeacherGroupsIds);
				await _unitofwork.SaveAsync();
				return ApiResponse.Success();
			}
			catch (Exception ex)
			{
				return ApiResponse.Fail(ex.Message);
			}
		}
		[Route("GetAll")]
		public async Task<ApiModel> GetAll(string Id)
		{
			try
			{
				return ApiResponse.Success(await _unitofwork.IEducationPostRepo.GetAll(Id));
			}
			catch (Exception ex)
			{
				return ApiResponse.Fail(ex.Message);
			}
		}
		[Route("GetById")]
		public async Task<ApiModel> GetById(int Id)
		{
			try
			{
				EducationPostViewModel item = await _unitofwork.IEducationPostRepo.GetById(Id);
				return ApiResponse.Success(item);
			}
			catch (Exception ex)
			{
				return ApiResponse.Fail(ex.Message);
			}
		}
		[Route("Remove")]
		public async Task<ApiModel> Remove(int Id)
		{
			try
			{
				EducationPostViewModel item = await _unitofwork.IEducationPostRepo.GetById(Id);
				await _unitofwork.IEducationPostRepo.Remove(item);
				await _unitofwork.SaveAsync();
				return ApiResponse.Success();
			}
			catch (Exception ex)
			{
				return ApiResponse.Fail(ex.Message);
			}
		}
		[Route("RemoveFile")]
		public async Task<ApiModel> RemoveFile(int Id)
		{
			try
			{
				await _unitofwork.IEducationPostRepo.RemoveFile(Id);
				await _unitofwork.SaveAsync();
				return ApiResponse.Success();
			}
			catch (Exception ex)
			{
				return ApiResponse.Fail(ex.Message);
			}
		}
		[Route("GetByCategory")]
		public async Task<ApiModel> GetByCategory(int Id)
		{
			try
			{
				return ApiResponse.Success(await _unitofwork.IEducationPostRepo.GetByCategory(Id));
			}
			catch (Exception ex)
			{
				return ApiResponse.Fail(ex.Message);
			}
		}
		[Route("GetRelatedComment")]
		public async Task<ApiModel> GetRelatedComment(int Id)
		{
			try
			{
				return ApiResponse.Success(await _unitofwork.IEducationPostRepo.GetEducationPostCommnet(Id));
			}
			catch (Exception ex)
			{
				return ApiResponse.Fail(ex.Message);
			}
		}
		[HttpPost("StudentAddView")]
		public async Task<ApiModel> StudentAddVeiw(AddViewToPostViewModel<int> model)
		{
			try
			{
				await _unitofwork.IEducationPostRepo.StudentAddView(model); 
				return ApiResponse.Success();
			}
			catch (Exception ex)
			{
				return ApiResponse.Fail(ex.Message);
			}
		}
		[HttpPost("TeacherAddView")]
		public async Task<ApiModel> TeacherAddView(AddViewToPostViewModel<string> model)
		{
			try
			{
				await _unitofwork.IEducationPostRepo.TeacherAddView(model);
				return ApiResponse.Success();
			}
			catch (Exception ex)
			{
				return ApiResponse.Fail(ex.Message);
			}
		}
		[HttpGet("GetTotalComment")]
		public async Task<ApiModel> GetTotalComment(int Id)
		{
			try
			{
				return ApiResponse.Success(await _unitofwork.IEducationPostRepo.CommentCount(Id));
			}
			catch (Exception ex)
			{
				return ApiResponse.Fail(ex.Message);
			}
		}
		[HttpGet("GetRelatedPostsByCategory")]
		public async Task<ApiModel> GetRelatedPostsByCategory(int Id)
		{
			try
			{
				 var items = await _unitofwork.IEducationPostRepo.GetRelatedEducationPostsInCateogry(Id);
				return ApiResponse.Success(items);
			}
			catch (Exception ex)
			{
				return ApiResponse.Fail(ex.Message);
			}
		}
		[HttpGet("GetCollectionPosts")]
		public async Task<ApiModel> GetCollectionPosts(string Id)
		{
			try
			{
				var items = await _unitofwork.IEducationPostRepo.GetEducationPostByArray(Id);
				return ApiResponse.Success(items);
			}
			catch (Exception ex)
			{
				return ApiResponse.Fail(ex.Message);
			}
		}




	}
}