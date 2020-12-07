﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Windows.Documents;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
using EducationalApplication.Models.ViewModels;
using EducationalApplication.Utility;
using Microsoft.AspNetCore.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
namespace EducationalApplication.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private IUnitOfWorkRepo _unitofwork;
        public BannerController(IUnitOfWorkRepo unitOfWork)
        {
            _unitofwork = unitOfWork;
        }
 
        [HttpGet("GetAll")]
        public async Task<ApiModel> GetAllBanner(string Id)
        {
            try
            {
                if (await _unitofwork.IBannerRepo.GetAll(Id) == null)
                {
                    return ApiResponse.Fail(null, 404, $"اطلاعاتی یافت نشد.");
                }
                return ApiResponse.Success(await _unitofwork.IBannerRepo.GetAll(Id));
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
                Banner item = await _unitofwork.IBannerRepo.GetById(Id);
                return ApiResponse.Success(item);

            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(ex.Message);
            }

        }
        [HttpPost("Add")]
        public async Task<ApiModel> Add([FromForm] Banner model, [FromForm] IFormFile file)
        {
            try
            {
                if (await _unitofwork.IUserRepo.GetById(model.ApplicationUserId) == null)
                {
                    return ApiResponse.Fail(null, 404, $"معلمی با ای دی {model.ApplicationUserId} یافت نشد.");
                }
            await _unitofwork.IBannerRepo.AddOrUpdate(model, file);
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
                Banner item = await _unitofwork.IBannerRepo.GetById(Id);
                if(item == null)
                {
                    return ApiResponse.Fail(null, 404, $"بنر مورد نظر وجود ندارد ");
                }
                  _unitofwork.IBannerRepo.Remove(item);
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
