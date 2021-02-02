using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
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
			return View(_unitofwork.IAboutUsRepo.Get());
		}

		[HttpPost]
		public async Task<JsonResult> Create(AboutUs model)
		{
			await _unitofwork.IAboutUsRepo.UpdateAbutus(model);
			await _unitofwork.SaveAsync();
			return Json(new { success = true });
		}


	}
}