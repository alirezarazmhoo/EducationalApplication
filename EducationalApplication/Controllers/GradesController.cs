using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Infrastructure;
using EducationalApplication.Utility;
using Microsoft.AspNetCore.Mvc;
using EducationalApplication.Models;
using EducationalApplication.Models.ViewModels;

namespace EducationalApplication.Controllers
{
    public class GradesController : Controller
    {
        private IUnitOfWorkRepo _unitofwork;
        public GradesController(IUnitOfWorkRepo unitOfWork)
        {
            _unitofwork = unitOfWork;
        }
        public async Task<IActionResult> Index(string searchString, int? pageNumber)
        {
            bool shouldsearch = false;
            try
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    shouldsearch = true;
                }
                int pageSize = 3;
                var items = shouldsearch == false ? await _unitofwork.IGradeRepo.GetAll()
                    : await _unitofwork.IGradeRepo.search(searchString);
                return View(PaginatedList<Grade>.CreateAsync(items.AsQueryable(), pageNumber ?? 1, pageSize));
            }
            catch (Exception)
            {
                return Content(CustomeMessages.Try);
            }
        }
        [HttpPost]
        public async Task<JsonResult> Register(Grade model, int? GradeId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, responseText = CustomeMessages.Empty });
                }
                else
                {
                    if (GradeId != null)
                    {
                        model.Id = GradeId.Value;
                    }
                    _unitofwork.IGradeRepo.CreateOrUpdate(model);
                    await _unitofwork.SaveAsync();
                    return Json(new { success = true, responseText = CustomeMessages.Succcess });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false, responseText = CustomeMessages.Fail });
            }
        }
        [HttpPost]
        public async Task<JsonResult> Remove(int GradeId)
        {
            try
            {
                var item = await _unitofwork.IGradeRepo.GetById(GradeId);
                if (item == null)
                {
                    return Json(new { success = false, responseText = CustomeMessages.Try });
                }
                _unitofwork.IGradeRepo.Remove(item);
                await _unitofwork.SaveAsync();
                return Json(new { success = true, responseText = CustomeMessages.Succcess });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = CustomeMessages.Fail });

            }
        }
        [HttpPost]
        public async Task<JsonResult> Deatails(int ItemId)
        {
            try
            {
                var item = await _unitofwork.IGradeRepo.GetById(ItemId);
                if (item == null)
                {
                    return Json(new { success = false, responseText = CustomeMessages.Fail });
                }
                List<EditViewModels> edit = new List<EditViewModels>();
                edit.Add(new EditViewModels() { key = "Name", value = item.Name });
                edit.Add(new EditViewModels() { key = "Code", value = item.Code });
                edit.Add(new EditViewModels() { key = "GradeId", value = item.Id.ToString() });

     
                return Json(new { success = true, listItem = edit.ToList() });
            }
            catch (Exception)
            {
                return Json(new { success = false, responseText = CustomeMessages.Fail });
            }
        }

    }
}