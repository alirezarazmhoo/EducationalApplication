using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationalApplication.Infrastructure;
using EducationalApplication.Models;
using EducationalApplication.Models.ViewModels;
using EducationalApplication.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationalApplication.Controllers
{
    public class StudentsController : Controller
    {
        private IUnitOfWorkRepo _unitofwork;
        public StudentsController(IUnitOfWorkRepo unitOfWork)
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
                var items = shouldsearch == false ? await _unitofwork.IStudentRepo.GetAll()
                    : await _unitofwork.IStudentRepo.search(searchString);
                return View(PaginatedList<Students>.CreateAsync(items.AsQueryable(), pageNumber ?? 1, pageSize));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpPost]
        public async Task<JsonResult> Register(Students model, IFormFile file, int? StudentId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, responseText = CustomeMessages.Empty });
                }
                else
                {
                    if (StudentId != null)
                    {
                        model.Id = StudentId.Value;
                    }
                    await _unitofwork.IStudentRepo.AddOrUpdate(model, file);
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
        public async Task<JsonResult> Remove(int StudentId)
        {
            try
            {
                var item = await _unitofwork.IStudentRepo.GetById(StudentId);
                if (item == null)
                {
                    return Json(new { success = false, responseText = CustomeMessages.Try });
                }
                _unitofwork.IStudentRepo.Remove(item);
                await _unitofwork.SaveAsync();
                return Json(new { success = true, responseText = CustomeMessages.Succcess });
            }
            catch (Exception)
            {
                return Json(new { success = false, responseText = CustomeMessages.Fail });
            }
        }
        [HttpPost]
        public async Task<JsonResult> Deatails(int ItemId)
        {
            try
            {
                var item = await _unitofwork.IStudentRepo.GetById(ItemId);
                if (item == null)
                {
                    return Json(new { success = false, responseText = CustomeMessages.Fail });
                }
                List<FileViewModels> fileViewModels = new List<FileViewModels>();
                List<EditViewModels> edit = new List<EditViewModels>();
                List<EditViewModels> MajorId = new List<EditViewModels>();
                List<EditViewModels> GradeId = new List<EditViewModels>();
                edit.Add(new EditViewModels() { key = "FullName", value = item.FullName });
                edit.Add(new EditViewModels() { key = "Address", value = item.Address });
                edit.Add(new EditViewModels() { key = "StudentId", value = item.Id.ToString() });
                edit.Add(new EditViewModels() { key = "Mobile", value = item.Mobile.ToString() });
                edit.Add(new EditViewModels() { key = "NationalCode", value = item.NationalCode });
                edit.Add(new EditViewModels() { key = "Password", value = item.Password });
                edit.Add(new EditViewModels() { key = "UserName", value = item.UserName });
                if (!string.IsNullOrEmpty(item.Url))
                {
                    fileViewModels.Add(new FileViewModels() { id = 0, url = item.Url });
                }
                MajorId.Add(new EditViewModels() { key = item.MajorId.ToString(), value = "" });
                GradeId.Add(new EditViewModels() { key = item.GradeId.ToString(), value = "" });
                return Json(new { success = true, listItem = edit.ToList(), studentfiles = fileViewModels.ToList() , majoritem = MajorId ,gradeitem = GradeId });
            }
            catch (Exception)
            {
                return Json(new { success = false, responseText = CustomeMessages.Fail });
            }
        }

        public async Task<JsonResult> LoadMajors()
        {
            List<ComboBoxViewModel> items = new List<ComboBoxViewModel>();

            try
            {
                foreach (var item in await _unitofwork.IMajorRepo.GetAll())
                {
                    items.Add(new ComboBoxViewModel() { id = item.Id, name = item.Name });

                }
                return Json(new { success = true, list = items.ToList() });
            }
            catch (Exception ex)
            {
                return Json(new { response = false, responseText = "Faild To Get Genres Data" });

            }
        }
        public async Task<JsonResult> LoadGrades()
        {
            List<ComboBoxViewModel> items = new List<ComboBoxViewModel>();

            try
            {
                foreach (var item in await _unitofwork.IGradeRepo.GetAll())
                {
                    items.Add(new ComboBoxViewModel() { id = item.Id, name = item.Name });

                }
                return Json(new { success = true, list = items.ToList() });
            }
            catch (Exception ex)
            {
                return Json(new { response = false, responseText = "Faild To Get Genres Data" });

            }
        }

    }
}