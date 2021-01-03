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
        public async Task<ApiModel>  Add([FromForm]EducationPost model, IFormFile Icon, IFormFile[] file)
        {
            try
            {
                if (await _unitofwork.IUserRepo.GetById(model.ApplicationUserId) == null)
                {
                    return ApiResponse.Fail(null ,404, $"معلمی با ای دی {model.ApplicationUserId} یافت نشد.");
                }
               await _unitofwork.IEducationPostRepo.AddOrUpdate(model, Icon, file);
                await _unitofwork.SaveAsync();
                return ApiResponse.Success();
            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(ex.Message);
            }
        }
        [Route("GetAll")]
        public async Task<ApiModel>  GetAll(string Id)
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
        public async Task<ApiModel>  GetById(int Id)
        {
            try
            {
                EducationPost item = await _unitofwork.IEducationPostRepo.GetById(Id);
                return ApiResponse.Success(item);

            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(ex.Message);
            }
        }
        [Route("Remove")]
        public async Task<ApiModel>  Remove(int Id)
        {
            try
            {
                EducationPost item = await _unitofwork.IEducationPostRepo.GetById(Id);
              await  _unitofwork.IEducationPostRepo.Remove(item);
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



    }
}