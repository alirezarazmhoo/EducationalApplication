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
        [HttpPost("AddStudentToGroup")]
        public async Task<ApiModel> AddStudentToGroup(StudentAndCustomGroupViewModel model)
        {
            try
            {
                if (await _unitofwork.ICustomGroupRepo.GetById(model.GroupId) == null)
                {
                    return ApiResponse.Fail(null, 404, $"گروهی با ای دی {model.GroupId} یافت نشد .");
                }
                await _unitofwork.ICustomGroupRepo.AddStudentToGroup(model);
                await _unitofwork.SaveAsync();
                return ApiResponse.Success();
            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(ex.Message);
            }
        }
        [HttpPost("AddTeacherToGroup")]
        public async Task<ApiModel> AddTeacherToGroup(TeacherAndCustomGroupViewModel model)
        {
            try
            {
                if (await _unitofwork.ICustomGroupRepo.GetById(model.GroupId) == null)
                {
                    return ApiResponse.Fail(null, 404, $"گروهی با ای دی {model.GroupId} یافت نشد .");
                }
                await _unitofwork.ICustomGroupRepo.AddTeacherToGroup(model);
                await _unitofwork.SaveAsync();
                return ApiResponse.Success();
            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(ex.Message);
            }
        }

        [HttpPost("RemoveStudentFromGroup")]
        public async Task<ApiModel> RemoveStudentFromGroup(RemoveUserFromCustomGroupViewModel<int> model)
        {
            if (await _unitofwork.ICustomGroupRepo.GetById(model. CustomGrupId) == null)
            {
                return ApiResponse.Fail(null, 404, $"گروهی با ای دی {model. CustomGrupId} یافت نشد .");
            }
            try
            {
            await  _unitofwork.ICustomGroupRepo.RemoveStudentFromGroup(model. UserId,model. CustomGrupId);
            await  _unitofwork.SaveAsync();
            return ApiResponse.Success();
            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(ex.Message);
            }
        }
        [HttpPost("RemoveTeacherFromGroup")]
        public async Task<ApiModel> RemoveTeacherFromGroup(RemoveUserFromCustomGroupViewModel<string> model)
        {
            if (await _unitofwork.ICustomGroupRepo.GetById(model. CustomGrupId) == null)
            {
                return ApiResponse.Fail(null, 404, $"گروهی با ای دی {model. CustomGrupId} یافت نشد .");
            }
            try
            {
                await _unitofwork.ICustomGroupRepo.RemoveTeacherFromGroup(model. UserId,model. CustomGrupId);
                await _unitofwork.SaveAsync();
                return ApiResponse.Success();
            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(ex.Message);
            }
        }
        [Route("GetRelatedStudentsFromCustomGroup")]
        [HttpGet]
        public async Task<ApiModel> GetRelatedStudentsFromCustomGroup(int groupId, string userId)
        {

            if (await _unitofwork.IUserRepo.GetById(userId) == null)
            {
                return ApiResponse.Fail(null, 404, $"کاربری با ای دی {userId} یافت نشد.");
            }
            else
            {
                return ApiResponse.Success(await _unitofwork.IUserRepo.GetRelatedStudents(groupId, userId));
            }
        }
    }
}