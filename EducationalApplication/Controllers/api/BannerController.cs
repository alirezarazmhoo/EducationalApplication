using Microsoft.AspNetCore.Mvc;
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
using Microsoft.AspNetCore.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EducationalApplication.Controllers.api
{

    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {

        private IUnitOfWorkRepo _unitofwork;
        ApiModel ApiModel = new ApiModel();
        public BannerController(IUnitOfWorkRepo unitOfWork)
        {
            _unitofwork = unitOfWork;
        }



        // GET: api/<BannerController>
        [HttpGet("GetAll")]
        public async Task<ApiModel> GetAllBanner()
        {
            try
            {

                if (await _unitofwork.IBannerRepo.GetAllBaner() == null)
                {
                    return ApiResponse.Fail(null, 404, $"اطلاعاتی یافت نشد.");
                }

                return ApiResponse.Success(await _unitofwork.IBannerRepo.GetAllBaner());
            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(ex.Message);
            }

        }

        // GET api/<BannerController>/5
        [HttpGet("GetById/{id}")]
        public async Task<ApiModel> GetById([FromRoute] int Id)
        {
            try
            {
                Banner item = await _unitofwork.IBannerRepo.GetbanerById(Id);
                return ApiResponse.Success(item);

            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(ex.Message);
            }

        }

        // POST api/<BannerController>
        [HttpPost("Add")]

        public async Task<ApiModel> Add([Microsoft.AspNetCore.Mvc.FromBody] Banner banner, [FromForm] IFormFile file)
        {
            try
            {
            await _unitofwork.IBannerRepo.AddBaner(banner, file);
            await _unitofwork.SaveAsync();
                return ApiResponse.Success(banner);
            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(ex.Message);
            }



        }

        // PUT api/<BannerController>/5
        [HttpPost("Edit")]
        public async Task<ApiModel> Editbanner([Microsoft.AspNetCore.Mvc.FromBody] Banner banner, [FromForm] IFormFile file)
        {

            await _unitofwork.IBannerRepo.Editbanner(banner, file);
            await _unitofwork.SaveAsync();
            return ApiResponse.Success(banner);

        }

        // DELETE api/<BannerController>/5
        [HttpDelete("Delete")]
        public async Task<ApiModel> Delete(int Id)
        {
            try
            {
                Banner item = await _unitofwork.IBannerRepo.GetbanerById(Id);
                  _unitofwork.IBannerRepo.Remove(item);

             
               
                await _unitofwork.SaveAsync();
                return ApiResponse.Success(item);

            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(ex.Message);
            }
        }







        //[Microsoft.AspNetCore.Mvc.HttpPost("upload")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> UploadFile(IFormFile file/*, CancellationToken cancellationToken*/)
        //{
        //    //if (CheckIfExcelFile(file))
        //    //{
        //    await WriteFile(file);
        //    //}
        //    //else
        //    //{
        //    //    return BadRequest(new { message = "Invalid file extension" });
        //    //}

        //    return Ok();
        //}
        ////private bool CheckIfExcelFile(IFormFile file)
        ////{
        ////    var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
        ////    return (extension == ".xlsx" || extension == ".xls"); // Change the extension based on your need
        ////}

        //private async Task<bool> WriteFile(IFormFile file)
        //{
        //    bool isSaveSuccess = false;
        //    string fileName;
        //    try
        //    {
        //        var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
        //        fileName = DateTime.Now.Ticks + extension; //Create a new Name for the file due to security reasons.

        //        var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "~\\Upload\\Banner\\File");

        //        if (!Directory.Exists(pathBuilt))
        //        {
        //            Directory.CreateDirectory(pathBuilt);
        //        }

        //        var path = Path.Combine(Directory.GetCurrentDirectory(), "~\\Upload\\Banner\\File",
        //            fileName);

        //        using (var stream = new FileStream(path, FileMode.Create))
        //        {
        //            await file.CopyToAsync(stream);
        //        }

        //        isSaveSuccess = true;
        //    }
        //    catch (Exception e)
        //    {
        //        //log error
        //    }

        //    return isSaveSuccess;
        //}





    }
}
