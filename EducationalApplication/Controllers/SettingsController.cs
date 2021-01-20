using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace EducationalApplication.Controllers
{
	public class SettingsController : Controller
	{
		private IUnitOfWorkRepo _unitofwork;
		public SettingsController(IUnitOfWorkRepo unitOfWork)
		{
			_unitofwork = unitOfWork;
		}
		public async Task<IActionResult> Index()
		{
			return View(await _unitofwork.ISettingRepo.Get());
		}
		[HttpPost]
		public async Task<IActionResult> Update(Setting model)
		{
		await	_unitofwork.ISettingRepo.UpdateSetting(model);
			await _unitofwork.SaveAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}