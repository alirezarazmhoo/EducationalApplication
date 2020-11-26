using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
using EducationalApplication.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationalApplication.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private IUnitOfWorkRepo _unitofwork;
        ApiModel ApiModel = new ApiModel();
        public TeacherController(IUnitOfWorkRepo unitOfWork)
        {
            _unitofwork = unitOfWork;
        }

        [Route("Login")]
        [HttpPost]
        public ApiModel Login(Teacher model)
        {
            if(string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.UserName))
            {
                ApiModel.status = 404;
                ApiModel.success = false;
                ApiModel.message = $"اطلاعات ورودی ناقص است";
                ApiModel.data = null;
                return ApiModel; 
            }
            var item = _unitofwork.ITeacherRepo.Authorize(model);
            if(item.Count() == 0)
            {
                ApiModel.status = 404;
                ApiModel.success = false;
                ApiModel.message = $"کاریری با چنین مشخصاتی یافت نشد.";
                ApiModel.data = null;
                return ApiModel;
            }
            else
            {
                ApiModel.status = 200;
                ApiModel.success = true;
                ApiModel.message = $"موفقیت آمیز";
                ApiModel.data = item;
                return ApiModel;
            }
        }

        }
}