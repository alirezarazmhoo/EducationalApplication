using EducationalApplication.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
