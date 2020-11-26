using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
using EducationalApplication.Models.ViewModels;
using EducationalApplication.Utility;
using Microsoft.AspNetCore.Mvc;

namespace EducationalApplication.Controllers
{
    public class MajorsController : Controller
    {
        private IUnitOfWorkRepo _unitofwork;
        public MajorsController(IUnitOfWorkRepo unitOfWork)
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
                var items = shouldsearch == false ? await _unitofwork.IMajorRepo.GetAll()
                    : await _unitofwork.IMajorRepo.search(searchString);
                return View(PaginatedList<Major>.CreateAsync(items.AsQueryable(), pageNumber ?? 1, pageSize));
            }
            catch (Exception)
            {
                return Content(CustomeMessages.Try);
            }
        }
        [HttpPost]
        public async Task<JsonResult> Register(Major model, int? MajorId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, responseText = CustomeMessages.Empty });
                }
                else
                {
                    if (MajorId != null)
                    {
                        model.Id = MajorId.Value;
                    }
                    _unitofwork.IMajorRepo.CreateOrUpdate(model);
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
        public async Task<JsonResult> Remove(int MajorId)
        {
            try
            {
                var item = await _unitofwork.IMajorRepo.GetById(MajorId);
                if (item == null)
                {
                    return Json(new { success = false, responseText = CustomeMessages.Try });
                }
                _unitofwork.IMajorRepo.Remove(item);
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
                var item = await _unitofwork.IMajorRepo.GetById(ItemId);
                if (item == null)
                {
                    return Json(new { success = false, responseText = CustomeMessages.Fail });
                }
                List<EditViewModels> edit = new List<EditViewModels>();
                edit.Add(new EditViewModels() { key = "Name", value = item.Name });
                edit.Add(new EditViewModels() { key = "Code", value = item.Code });
                edit.Add(new EditViewModels() { key = "MajorId", value = item.Id.ToString() });


                return Json(new { success = true, listItem = edit.ToList() });
            }
            catch (Exception)
            {
                return Json(new { success = false, responseText = CustomeMessages.Fail });
            }
        }
    }
}