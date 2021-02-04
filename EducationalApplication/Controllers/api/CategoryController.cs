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
    public class CategoryController : ControllerBase
    {
        private IUnitOfWorkRepo _unitofwork;
        public CategoryController(IUnitOfWorkRepo unitOfWork)
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
                if (await _unitofwork.ICategoryRepo.GetAll(UserId) == null)
                {
                    return ApiResponse.Fail(null, 404, $"اطلاعاتی یافت نشد.");
                }
                return ApiResponse.Success(await _unitofwork.ICategoryRepo.GetAll(UserId));
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
                Category item = await _unitofwork.ICategoryRepo.GetById(Id);
                return ApiResponse.Success(item);

            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(ex.Message);
            }
        }
        [HttpPost("Add")]
        public async Task<ApiModel> Add([FromForm] Category model, [FromForm] IFormFile file)
        {
            try
            {
                if (await _unitofwork.IUserRepo.GetById(model.ApplicationUserId) == null)
                {
                    return ApiResponse.Fail(null, 404, $"معلمی با ای دی {model.ApplicationUserId} یافت نشد.");
                }
                await _unitofwork.ICategoryRepo.CreateOrUpdate(model, file);
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
                Category item = await _unitofwork.ICategoryRepo.GetById(Id);
                if (item == null)
                {
                    return ApiResponse.Fail(null, 404, $"بنر مورد  نظر وجود ندارد ");
                }
                _unitofwork.ICategoryRepo.Remove(item);
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
                Category item = await _unitofwork.ICategoryRepo.GetById(Id);
                if (item == null)
                {
                    return ApiResponse.Fail(null, 404, $"بنر مورد نظر وجود ندارد ");
                }
                await _unitofwork.ICategoryRepo.RemoveFile(Id);
                return ApiResponse.Success();
            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(ex.Message);
            }
        }

        [Route("Search")]
        public async Task<ApiModel> Search(string txtSearch , string UserId)
        {
            try
            {
                if (await _unitofwork.IUserRepo.GetById(UserId) == null)
                {
                    return ApiResponse.Fail(null, 404, $"معلمی با ای دی {UserId} یافت نشد.");
                }
                var item = await _unitofwork.ICategoryRepo.search(txtSearch , UserId);
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
        [Route("GetForMainPage")]
        public async Task<ApiModel> GetForMainPage(string Id)
        {
            try
            {
                return ApiResponse.Success(await _unitofwork.ICategoryRepo.GetAllForMainPage(Id));
            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(ex.Message);
            }
        }
    }
}