using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
using EducationalApplication.Models.ViewModels;
using EducationalApplication.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EducationalApplication.Controllers.api
{
	[Route("api/[controller]")]
	[ApiController]
	public class TicketsController : ControllerBase
	{
		private IUnitOfWorkRepo _unitofwork;
		public TicketsController(IUnitOfWorkRepo unitOfWork)
		{
			_unitofwork = unitOfWork;
		}
		[HttpGet("TeacherGetAll")]
		public async Task<ApiModel> TeacherGetAll(string Id)
		{
			try
			{
				if (await _unitofwork.IUserRepo.GetById(Id) == null)
				{
					return ApiResponse.Fail(null, 404, $"معلمی با ای دی {Id} یافت نشد.");
				}
				return ApiResponse.Success(await _unitofwork.ITiketRepo.TeacherGetAll(Id));
			}
			catch (Exception ex)
			{
				return ApiResponse.Fail(ex.Message);
			}
		}
		[HttpGet("StudentGetAll")]
		public async Task<ApiModel> StudentGetAll(int Id)
		{
			try
			{
				if (await _unitofwork.IStudentRepo.GetById(Id) == null)
				{
					return ApiResponse.Fail(null, 404, $"دانش آموزی با ای دی {Id} یافت نشد.");
				}
				return ApiResponse.Success(await _unitofwork.ITiketRepo.StudentGetAll(Id));
			}
			catch (Exception ex)
			{
				return ApiResponse.Fail(ex.Message);
			}
		}
		[Route("Add")]
		[HttpPost]
		public async Task<ApiModel> Add([FromForm] Ticket model, IFormFile[] file)
		{
			try
			{
				await _unitofwork.ITiketRepo.Create(model , file);
				await _unitofwork.SaveAsync();
				return ApiResponse.Success();
			}
			catch (Exception ex)
			{
				return ApiResponse.Fail(ex.Message);
			}
		}
		[HttpPost("Remove")]
		public async Task<ApiModel> Remove(InputIdViewModel model)
		{
			try
			{
				var item = await _unitofwork.ITiketRepo.GetById(model.Id);
				await _unitofwork.ITiketRepo.Remove(item);
				await _unitofwork.SaveAsync();
				return ApiResponse.Success();
			}
			catch (Exception ex)
			{
				return ApiResponse.Fail(ex.Message);
			}
		}
	}
}
