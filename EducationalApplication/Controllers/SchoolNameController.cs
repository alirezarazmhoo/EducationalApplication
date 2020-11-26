using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EducationalApplication.Models;
using EducationalApplication.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EducationalApplication.Controllers
{
    public class SchoolNameController : Controller
    {
        private IUnitOfWorkRepo _unitofwork;
        public SchoolNameController(IUnitOfWorkRepo unitOfWork)
        {
            _unitofwork = unitOfWork;
        }
        public IActionResult Index()
        {

            ViewData["Name"] = _unitofwork.ISchoolNameRepo.Get().Name;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>  Index(SchoolName model)
        {
            if (ModelState.IsValid)
            {
                try                {
                    _unitofwork.ISchoolNameRepo.Change(model);
                      await _unitofwork.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("خطا", "سیستم قادر به پاسخ گویی نیست");
                }
            }
            ViewData["Name"] = _unitofwork.ISchoolNameRepo.Get().Name;

            return View();

        }
    }
}