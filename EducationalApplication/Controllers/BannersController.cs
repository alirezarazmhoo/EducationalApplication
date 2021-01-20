using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
using EducationalApplication.Utility;
using Microsoft.AspNetCore.Mvc;

namespace EducationalApplication.Controllers
{
    public class BannersController : Controller
    {
        private IUnitOfWorkRepo _unitofwork;
        public BannersController(IUnitOfWorkRepo unitOfWork)
        {
            _unitofwork = unitOfWork;
        }
        public async Task<IActionResult> Index(int? pageNumber)
        {
            try
            {
                int pageSize = 10;
                var items = await _unitofwork.IBannerRepo.AdminGetAll();
                return View(PaginatedList<Banner>.CreateAsync(items.AsQueryable(), pageNumber ?? 1, pageSize));
            }
            catch (Exception)
            {
                return Content(CustomeMessages.Try);
            }
        }
        public async Task<IActionResult> Accept(int Id)
        {
            if (Id != 0)
            {
                await _unitofwork.IBannerRepo.Accept(Id);
                await _unitofwork.SaveAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Reject(int Id)
        {
            if (Id != 0)
            {
                await _unitofwork.IBannerRepo.Reject(Id);
                await _unitofwork.SaveAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> GetById(int Id)
        {
            if (Id != 0)
            {
                return View(await _unitofwork.IBannerRepo.AdminGetById(Id));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}