using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EducationalApplication.Controllers
{
    public class AbutUsController : Controller
    {
        private IUnitOfWorkRepo _unitofwork;
        public AbutUsController(IUnitOfWorkRepo unitOfWork)
        {
            _unitofwork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}