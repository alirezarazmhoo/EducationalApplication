using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
using EducationalApplication.Models.ViewModels;
using EducationalApplication.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EducationalApplication.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomGroupController : ControllerBase
    {
        private IUnitOfWorkRepo _unitofwork;
        public CustomGroupController(IUnitOfWorkRepo unitOfWork)
        {
            _unitofwork = unitOfWork;
        }
        [HttpGet("GetAll")]
        public async Task<ApiModel> GetAll(string UserId)
        {
            try
            {
                if (await _unitofwork.IUserRepo.GetById(UserId) == null)
                {
                    return ApiResponse.Fail(null, 404, $"معلمی با ای دی {UserId} یافت نشد.");
                }          
                return ApiResponse.Success(await _unitofwork.ICustomGroupRepo.GetAll(UserId));
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
                CustomGroup item = await _unitofwork.ICustomGroupRepo.GetById(Id);
                return ApiResponse.Success(item);

            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(ex.Message);
            }
        }

        [HttpPost("Add")]
        public async Task<ApiModel> Add(CustomGroup model)
        {
            try
            {
                if (await _unitofwork.IUserRepo.GetById(model.ApplicationUserId) == null)
                {
                    return ApiResponse.Fail(null, 404, $"کاربری با ای دی {model.ApplicationUserId} یافت نشد.");
                }
                await _unitofwork.ICustomGroupRepo.AddOrUpdate(model);
                await _unitofwork.SaveAsync();
                return ApiResponse.Success();
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
                CustomGroup item = await _unitofwork.ICustomGroupRepo.GetById(Id);
                if (item == null)
                {
                    return ApiResponse.Fail(null, 404, $"گروه مورد  نظر وجود ندارد ");
                }
                _unitofwork.ICustomGroupRepo.Remove(item);
                await _unitofwork.SaveAsync();
                return ApiResponse.Success();
            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(ex.Message);
            }
        }
        [Route("Search")]
        public async Task<ApiModel> Search(string txtSearch, string UserId)
        {
            try
            {
                if (await _unitofwork.IUserRepo.GetById(UserId) == null)
                {
                    return ApiResponse.Fail(null, 404, $"معلمی با ای دی {UserId} یافت نشد.");
                }
                var item = await _unitofwork.ICustomGroupRepo.search(txtSearch, UserId);
                if (item == null)
                {
                    return ApiResponse.Fail(null, 404, $"داده ای یافت نشد ");
                }
                return ApiResponse.Success(item);
            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(ex.Message);
            }
        }
    }
}