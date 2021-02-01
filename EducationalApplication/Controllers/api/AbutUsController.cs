using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models.ViewModels;
using EducationalApplication.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationalApplication.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbutUsController : ControllerBase
    {
        private IUnitOfWorkRepo _unitofwork;
        public AbutUsController(IUnitOfWorkRepo unitOfWork)
        {
            _unitofwork = unitOfWork;
        }

        [HttpGet("Get")]
        public  ApiModel Get()
        {
            return ApiResponse.Success( _unitofwork.IAboutUsRepo.Get());
        }

    }
}