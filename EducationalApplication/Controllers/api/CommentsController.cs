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
    public class CommentsController : ControllerBase
    {
        private IUnitOfWorkRepo _unitofwork;
        public CommentsController(IUnitOfWorkRepo unitOfWork)
        {
            _unitofwork = unitOfWork;
        }
        [HttpGet("GetAll")]
        public async Task<ApiModel> GetAll(string UserId , int Id)
        {
            try
            {
                if (await _unitofwork.IUserRepo.GetById(UserId) == null)
                {
                    return ApiResponse.Fail(null, 404, $"معلمی با ای دی {UserId} یافت نشد.");
                }
                if (await _unitofwork.IEducationPostRepo.GetById(Id) == null)
                {
                    return ApiResponse.Fail(null, 404, $"اطلاعاتی یافت نشد.");
                }
                return ApiResponse.Success(await _unitofwork.ICommentRepo.GetAll(Id));
            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(ex.Message);
            }
        }
        [HttpPost("Add")]
        public async Task<ApiModel> Add(Comment model)
        {
            try
            {
                if (await _unitofwork.IStudentRepo.GetById(model.StudentsId) == null)
                {
                    return ApiResponse.Fail(null, 404, $"دانش آموزی با ای دی {model.StudentsId} یافت نشد.");
                }
                await _unitofwork.ICommentRepo.Create(model);
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
                Comment item = await _unitofwork.ICommentRepo.GetById(model.Id);
                if (item == null)
                {
                    return ApiResponse.Fail(null, 404, $"کامنت مورد  نظر وجود ندارد ");
                }
                await   _unitofwork.ICommentRepo.Remove(item);
                await _unitofwork.SaveAsync();
                return ApiResponse.Success();
            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(ex.Message);
            }
        }

        [HttpPost("ChangeStatus")]
        public async Task<ApiModel> ChangeStatus(Comment model)
        {
            try
            {
                Comment item = await _unitofwork.ICommentRepo.GetById(model.Id);
                if (item == null)
                {
                    return ApiResponse.Fail(null, 404, $"کامنت مورد  نظر وجود ندارد ");
                }
                await _unitofwork.ICommentRepo.ChaneStatus(model);
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