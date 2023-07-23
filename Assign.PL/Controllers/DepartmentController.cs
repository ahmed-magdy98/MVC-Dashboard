using Assign.BLL.Interfaces;
using Assign.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Assign.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(/*IDepartmentRepository departmentRepository*/IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_departmentRepository = departmentRepository;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            ////1.ViewData
            // ViewData["Message"] = "Hello from Department Controller! [ViewData]";

            ////2.ViewBag
            // ViewBag.MessageViewBag = "Hello from Department Controller! [ViewBag]";

            var departments = await _unitOfWork.DepartmentRepository.GetAll();
            return View(departments);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.DepartmentRepository.Add(department);

                // 3. TempData
                TempData["Message"] = "Hello from Department Controller! (Create → Index)[TempData]";

                return RedirectToAction("Index");
            }
            return View();
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return NotFound();

            var department = await _unitOfWork.DepartmentRepository.Get(id);
            if (department is null)
                return NotFound();

            return View(department);
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null)
                return NotFound();

            var department = await _unitOfWork.DepartmentRepository.Get(id);
            if (department is null)
                return NotFound();

            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, Department department)
        {
            if (id != department.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _unitOfWork.DepartmentRepository.Update(department);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View(department);
                }
            }

            return View(department);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return NotFound();

            var department = await _unitOfWork.DepartmentRepository.Get(id);
            if (department is null)
                return NotFound();

            await _unitOfWork.DepartmentRepository.Delete(department);
            return RedirectToAction("Index");
        }

        //[HttpPost, ActionName("Delete")]
        //public IActionResult Delete(int? id, Department department)
        //{
        //    if (id != department.Id)
        //        return NotFound();

        //    try
        //    {
        //        _departmentRepository.Delete(department);
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception)
        //    {
        //        return View(department);
        //    }
        //} 
        #endregion
    }
}
