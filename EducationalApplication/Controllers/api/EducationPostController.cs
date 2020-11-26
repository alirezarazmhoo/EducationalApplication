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
        ApiModel ApiModel = new ApiModel();
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
            
                if (_unitofwork.ITeacherRepo.GetById(model.TeacherId) == null)
                {
                    return ApiResponse.Fail(null ,404, $"معلمی با ای دی {model.TeacherId} یافت نشد.");
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
        public ApiModel GetAll(int Id)
        {
            try
            {

            if (_unitofwork.ITeacherRepo.GetById(Id)== null)
            {
              return ApiResponse.Fail(null, 404, $"معلمی با ای دی {Id} یافت نشد.");
            }

                return ApiResponse.Success(_unitofwork.IEducationPostRepo.GetAll(Id));
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


    }
}