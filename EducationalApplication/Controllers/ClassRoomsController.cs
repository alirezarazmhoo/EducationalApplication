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
    public class ClassRoomsController : Controller
    {
        private IUnitOfWorkRepo _unitofwork;
        public ClassRoomsController(IUnitOfWorkRepo unitOfWork)
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
                var items = shouldsearch == false ? await _unitofwork.IClassRoomRepo.GetAll()
                    : await _unitofwork.IClassRoomRepo.search(searchString);
                return View(PaginatedList<ClassRoom>.CreateAsync(items.AsQueryable(), pageNumber ?? 1, pageSize));
            }
            catch (Exception)
            {
                return Content(CustomeMessages.Try);
            }
        }
        [HttpPost]
        public async Task<JsonResult> Register(ClassRoom model, int? ClassRoomId)
        {
            try
            {
                ModelState.Remove("ClassRoomId");
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, responseText = CustomeMessages.Empty });
                }
                if (await _unitofwork.IClassRoomRepo.CheckCode(model.Code) && !ClassRoomId.HasValue)
                {
                    return Json(new { success = false, responseText = CustomeMessages.ItrateCode });
                }
                else
                {
                    if (ClassRoomId != null)
                    {
                        model.Id = ClassRoomId.Value;
                    }
                    await _unitofwork.IClassRoomRepo.CreateOrUpdate(model);
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
        public async Task<JsonResult> Remove(int ClassRoomId)
        {
            try
            {
                var item = await _unitofwork.IClassRoomRepo.GetById(ClassRoomId);
                if (item == null)
                {
                    return Json(new { success = false, responseText = CustomeMessages.Try });
                }
               await _unitofwork.IClassRoomRepo.Remove(item);
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
                var item = await _unitofwork.IClassRoomRepo.GetById(ItemId);
                if (item == null)
                {
                    return Json(new { success = false, responseText = CustomeMessages.Fail });
                }
                List<EditViewModels> edit = new List<EditViewModels>();            
                edit.Add(new EditViewModels() { key = "Name", value = item.Name });
                edit.Add(new EditViewModels() { key = "ClassRoomId", value = item.Id.ToString() });
                edit.Add(new EditViewModels() { key = "Code", value = item.Code.ToString() });
                edit.Add(new EditViewModels() { key = "GradeId", value = item.GradeId.ToString() });
                edit.Add(new EditViewModels() { key = "MajorId", value = item.MajorId.ToString() });
                return Json(new { success = true, listItem = edit.ToList() });
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
            catch (Exception )
            {
                return Json(new { response = false, responseText = CustomeMessages.Fail });

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
            catch (Exception)
            {
                return Json(new { response = false, responseText = CustomeMessages.Fail });

            }
        }
        public async Task<IActionResult>  AssignStudent(string searchString,int? Id ,int? pageNumber)
        {           
            try
            {           
                int pageSize = 5;
                ViewBag.ClassId = Id.Value;
                var items = await _unitofwork.IClassRoomRepo.GetAvalibleStudents(searchString , Id.Value);
                return View(PaginatedList<Students>.CreateAsync(items.AsQueryable(), pageNumber ?? 1, pageSize));
            }
            catch (Exception)
            {
                return Content(CustomeMessages.Try);
            }
        }
   
        public async Task<IActionResult> AddToClassRoom(int?Id , string UserId  , int Mode , int? pageNumber)
        {
            try
            {
                if(Mode == 1)
                {
                if(!Id.HasValue || string.IsNullOrEmpty(UserId))
                {
                    return RedirectToAction("AssignStudent", "ClassRooms", new { Id = Id.Value, pageNumber = pageNumber });

                }
                await _unitofwork.IClassRoomRepo.AddPerson(UserId, Mode, Id.Value);
                await  _unitofwork.SaveAsync();
                return RedirectToAction("AssignStudent", "ClassRooms", new { Id = Id.Value , pageNumber= pageNumber });
                }
                if(Mode == 2)
                {
                    if (!Id.HasValue || string.IsNullOrEmpty(UserId))
                    {
                        return RedirectToAction("AssignTeacher", "ClassRooms", new { Id = Id.Value, pageNumber = pageNumber });
                    }
                    await _unitofwork.IClassRoomRepo.AddPerson(UserId, Mode, Id.Value);
                    await _unitofwork.SaveAsync();
                    return RedirectToAction("AssignTeacher", "ClassRooms", new { Id = Id.Value, pageNumber = pageNumber });

                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                return Content(CustomeMessages.Try);

            }
        }
        public async Task<IActionResult> RemoveFromClassRoom(int? Id, string UserId, int Mode, int? pageNumber)
        {
            try
            {
                if(Mode == 1)
                {
                if (!Id.HasValue || string.IsNullOrEmpty(UserId))
                {
                    return RedirectToAction("AssignStudent", "ClassRooms", new { Id = Id.Value, pageNumber = pageNumber });
                }
                await _unitofwork.IClassRoomRepo.RemovePerson(UserId, Mode , null);
                await _unitofwork.SaveAsync();
                return RedirectToAction("AssignStudent", "ClassRooms", new { Id = Id.Value, pageNumber = pageNumber });
                }
                if(Mode == 2)
                {
                    if (!Id.HasValue || string.IsNullOrEmpty(UserId))
                    {
                        return RedirectToAction("AssignTeacher", "ClassRooms", new { Id = Id.Value, pageNumber = pageNumber });
                    }
                    await _unitofwork.IClassRoomRepo.RemovePerson(UserId, Mode, Id.Value);
                    await _unitofwork.SaveAsync();
                    return RedirectToAction("AssignTeacher", "ClassRooms", new { Id = Id.Value, pageNumber = pageNumber });
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                return Content(CustomeMessages.Try);
            }
        }
        public async Task<IActionResult> AssignTeacher(string searchString, int? Id, int? pageNumber)
        {
            try
            {
                int pageSize = 5;
                ViewBag.ClassId = Id.Value;
                var items = await _unitofwork.IClassRoomRepo.GetAllTeachers( Id.Value, searchString);
                return View(PaginatedList<TeacherToClassRoomViewModel>.CreateAsync(items.AsQueryable(), pageNumber ?? 1, pageSize));
            }
            catch (Exception)
            {
                return Content(CustomeMessages.Try);
            }
        }

        public async Task<IActionResult> GetAllPersons(string searchString, int? Id)
        {
            try
            {
                ViewBag.ClassId = Id.Value;
                ViewBag.ClassName = await _unitofwork.IClassRoomRepo.GetName(Id.Value);
                return View(await _unitofwork.IClassRoomRepo.GetAllPersons(Id.Value , searchString));
            }
            catch (Exception)
            {
                return Content(CustomeMessages.Try);
            }
        }

            
    }
}