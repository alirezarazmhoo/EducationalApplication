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
    public class FavoritController : ControllerBase
    {
        private IUnitOfWorkRepo _unitofwork;
        public FavoritController(IUnitOfWorkRepo unitOfWork)
        {
            _unitofwork = unitOfWork;
        }
        [HttpPost("Add")]
        public async Task<ApiModel> Add(Favorit model)
        {
            try
            {
                await _unitofwork.IFavoritRepo.Create(model);
                await _unitofwork.SaveAsync();
                return ApiResponse.Success(null , "به لیست علاقه مندی ها افزوده شد");
            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(ex.Message);
            }
        }
        [HttpPost("Remove")]
        public async Task<ApiModel> Remove(Favorit model)
        {
            try
            {
                _unitofwork.IFavoritRepo.Remove(model);
                await _unitofwork.SaveAsync();
                return ApiResponse.Success();
            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<ApiModel> GetAll(string Id)
        {
            return ApiResponse.Success(await _unitofwork.IFavoritRepo.GetAll(Id));
        }
    }
}
