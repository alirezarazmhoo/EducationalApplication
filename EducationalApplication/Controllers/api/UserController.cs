using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Data;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
using EducationalApplication.Models.Enums;
using EducationalApplication.Models.ViewModels;
using EducationalApplication.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationalApplication.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUnitOfWorkRepo _unitofwork;
        private ApplicationDbContext _Context;
        UsersViewModel UsersViewModel = new UsersViewModel();
        ApplicationUser _UserItem = new  ApplicationUser();
        public UserController(IUnitOfWorkRepo unitOfWork , ApplicationDbContext DbContext)
        {
            _unitofwork = unitOfWork;
            _Context = DbContext; 
        }
        [Route("Login")]
        [HttpPost]
        public ApiModel Login(ApplicationUser model)
        {
            if (string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.UserName))
            {
                return ApiResponse.Fail(null , 403 , CustomeMessages.Empty);
            }
            var item = _unitofwork.IUserRepo.Authorize(model).FirstOrDefault();
            var Student = _unitofwork.IStudentRepo.Authorize(model.UserName , model.Password).FirstOrDefault();
          
            if (item == null && Student == null)
            {
                return ApiResponse.Fail(null, 404, CustomeMessages.UserNotFound);
            }
            else
            {
                UserType userType = item != null ? item.UserType : UserType.Student;
                if(userType == UserType.Student)
                {
                    _UserItem.Address = Student.Address;
                    _UserItem.FullName = Student.FullName;
                    _UserItem.Id = Student.Id.ToString();
                    _UserItem.Mobile = Student.Mobile;
                    _UserItem.NationalCode = Student.NationalCode;
                    _UserItem.Password = Student.Password;
                    _UserItem.Url = Student.Url;
                    _UserItem.UserName = Student.UserName;
                    _UserItem.UserType = Student.UserType;
                    UsersViewModel.User = _UserItem;
                    UsersViewModel.School = _Context.SchoolName.FirstOrDefault().Name; 
                    return ApiResponse.Success(UsersViewModel);
                }
                else
                {
                    _UserItem.Address = item.Address;
                    _UserItem.FullName = item.FullName;
                    _UserItem.Id = item.Id.ToString();
                    _UserItem.Mobile = item.Mobile;
                    _UserItem.NationalCode = item.NationalCode;
                    _UserItem.Password = item.Password;
                    _UserItem.Url = item.Url;
                    _UserItem.UserName = item.UserName;
                    _UserItem.UserType = item.UserType;
                    UsersViewModel.User = _UserItem; 
                    UsersViewModel.School = _Context.SchoolName.FirstOrDefault().Name;
                    return ApiResponse.Success(UsersViewModel);
                }
            }
        }
        [Route("ForgetPassword")]
        [HttpGet]
        public async Task<ApiModel> ForgetPassword(string Mobile)
        {
            if(string.IsNullOrEmpty(Mobile))
            {
                return ApiResponse.Fail(null, 403, CustomeMessages.Empty);
            }
            else
            {
                if(await _unitofwork.IUserRepo.ForgetPassword(Convert.ToInt64(Mobile)))
                {
                    return ApiResponse.Success(null , "پیامک با موفقیت ارسال شد");
                }
                else
                {
                    return ApiResponse.Fail(null, 403, CustomeMessages.UserNotFound);
                }
            }
        }
        [Route("GetRelatedUsers")]
        [HttpGet]
        public async Task<ApiModel> GetRelatedUsers(string Id)
        {
            if (await _unitofwork.IUserRepo.GetById(Id) == null)
            {
                return ApiResponse.Fail(null, 404, $"کاربری با ای دی {Id} یافت نشد.");
            }
            else
            {
                return ApiResponse.Success(await _unitofwork.IUserRepo.GetRelatedUsers(await _unitofwork.IUserRepo.GetById(Id)));
            }
        }
        [Route("GetRelatedTeachers")]
        [HttpGet]
        public async Task<ApiModel> GetRelatedTeachers(int Id)
        {
            if (await _unitofwork.IStudentRepo.GetById(Id) == null)
            {
                return ApiResponse.Fail(null, 404, $"کاربری با ای دی {Id} یافت نشد.");
            }
            else
            {
                return ApiResponse.Success(await _unitofwork.IStudentRepo.GetRelatedTeachers(Id));
            }
        }
        [Route("GetRelatedEducationPost")]
        public async Task<ApiModel> GetRelatedEducationPostByTeacherId(string UserId, int StudentId)
        {
            if (await _unitofwork.IUserRepo.GetById(UserId) == null)
            {
                return ApiResponse.Fail(null, 404, $"معلمی با ای دی {UserId} یافت نشد.");
            }
            if (await _unitofwork.IStudentRepo.GetById(StudentId) == null)
            {
                return ApiResponse.Fail(null, 404, $"دانش آموزی با ای دی {StudentId} یافت نشد.");
            }
            return ApiResponse.Success(await _unitofwork.IStudentRepo.GetRelatedEducationPostByTeacherId(UserId, StudentId));
        }
    }
}