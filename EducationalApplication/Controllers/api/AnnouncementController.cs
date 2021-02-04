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
    public class AnnouncementController : ControllerBase
    {
        private IUnitOfWorkRepo _unitofwork;
        public AnnouncementController(IUnitOfWorkRepo unitOfWork)
        {
            _unitofwork = unitOfWork;
        }
		[HttpGet("GetAll")]
		public async Task<ApiModel> GetAll(string Id)
		{
			try
			{
				if (await _unitofwork.IUserRepo.GetById(Id) == null)
				{
					return ApiResponse.Fail(null, 404, $"معلمی با ای دی {Id} یافت نشد.");
				}
				return ApiResponse.Success(await _unitofwork.IAnnouncementRepo.GetAll(Id));
			}
			catch (Exception ex)
			{
				return ApiResponse.Fail(ex.Message);
			}
		}

		[HttpGet("GetById")]
		public async Task<ApiModel> GetById(int Id)
		{
			try
			{
				Announcements item = await _unitofwork.IAnnouncementRepo.GetById(Id);
				return ApiResponse.Success(item);
			}
			catch (Exception ex)
			{
				return ApiResponse.Fail(ex.Message);
			}
		}
		[HttpPost("AddOrUpdate")]
		public async Task<ApiModel> AddOrUpdate(Announcements model)
		{
			try
			{
				if (await _unitofwork.IUserRepo.GetById(model.ApplicationUserId) == null)
				{
					return ApiResponse.Fail(null, 404, $"معلمی با ای دی {model.ApplicationUserId} یافت نشد.");
				}
				await _unitofwork.IAnnouncementRepo.AddOrUpdate(model);
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
			
				Announcements item = await _unitofwork.IAnnouncementRepo.GetById(model.Id);
				if (item == null)
				{
					return ApiResponse.Fail(null, 404, $"اطلاعیه مورد نظر وجود ندارد ");
				}
				await _unitofwork.IAnnouncementRepo.Remove(item);
				await _unitofwork.SaveAsync();
				return ApiResponse.Success();
			}
			catch (Exception ex)
			{
				return ApiResponse.Fail(ex.Message);
			}
		}
		[Route("GetForMainPage")]
		public async Task<ApiModel> GetForMainPage(string Id)
		{
			try
			{
				return ApiResponse.Success(await _unitofwork.IAnnouncementRepo.GetAllForMainPage(Id));
			}
			catch (Exception ex)
			{
				return ApiResponse.Fail(ex.Message);
			}
		}
	}
}