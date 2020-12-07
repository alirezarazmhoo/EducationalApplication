using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Data;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
using EducationalApplication.Models.ViewModels;
using EducationalApplication.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationalApplication.Controllers
{
    public class TeachersController : Controller
    {
        private IUnitOfWorkRepo _unitofwork;
        public TeachersController(IUnitOfWorkRepo unitOfWork)
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
                var items = shouldsearch == false ? await _unitofwork.IUserRepo.GetAll()
                    : await _unitofwork.IUserRepo.search(searchString);
                return View(PaginatedList<ApplicationUser>.CreateAsync(items.AsQueryable(), pageNumber ?? 1, pageSize));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpPost]
        public async Task<JsonResult> Register(ApplicationUser model, IFormFile file, string TeacherId)
        {
            try
            {
                if (string.IsNullOrEmpty(model.FullName) || model.Mobile == 0)
                {
                    return Json(new { success = false, responseText = CustomeMessages.Empty });
                }
                else
                {
                    if (!string.IsNullOrEmpty(TeacherId))
                    {
                        model.Id = TeacherId;
                    }
                    else
                    {
                        model.Id = string.Empty;
                    }
                    await _unitofwork.IUserRepo.AddOrUpdate(model, file);
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
        public async Task<JsonResult> Remove(string TeacherId)
        {
            try
            {
                var item = await _unitofwork.IUserRepo.GetById(TeacherId);
                if (item == null)
                {
                    return Json(new { success = false, responseText = CustomeMessages.Try });
                }
                _unitofwork.IUserRepo.Remove(item);
                await _unitofwork.SaveAsync();
                return Json(new { success = true, responseText = CustomeMessages.Succcess });

            }
            catch (Exception)
            {
                return Json(new { success = false, responseText = CustomeMessages.Fail });

            }
        }
        [HttpPost]
        public async Task<JsonResult> Deatails(string ItemId)
        {
            try
            {
                var item = await _unitofwork.IUserRepo.GetById(ItemId);
                if (item == null)
                {
                    return Json(new { success = false, responseText = CustomeMessages.Fail });
                }
                List<FileViewModels> fileViewModels = new List<FileViewModels>();
                List<EditViewModels> edit = new List<EditViewModels>();
                edit.Add(new EditViewModels() { key = "FullName", value = item.FullName });
                edit.Add(new EditViewModels() { key = "Address", value = item.Address });
                edit.Add(new EditViewModels() { key = "TeacherId", value = item.Id.ToString() });
                edit.Add(new EditViewModels() { key = "Mobile", value = item.Mobile.ToString() });
                edit.Add(new EditViewModels() { key = "NationalCode", value = item.NationalCode });
                edit.Add(new EditViewModels() { key = "Password", value = item.Password });
                edit.Add(new EditViewModels() { key = "UserName", value = item.UserName });
                if (!string.IsNullOrEmpty(item.Url))
                {
                    fileViewModels.Add(new FileViewModels() { id = 0, url = item.Url });
                }
                return Json(new { success = true, listItem = edit.ToList(), teacherfiles = fileViewModels.ToList() });
            }
            catch (Exception)
            {
                return Json(new { success = false, responseText = CustomeMessages.Fail });
            }
        }
    }
}