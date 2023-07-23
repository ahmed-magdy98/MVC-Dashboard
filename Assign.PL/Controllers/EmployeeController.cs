using Assign.BLL.Interfaces;
using Assign.BLL.Repositories;
using Assign.DAL.Entities;
using Assign.PL.Helper;
using Assign.PL.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Assign.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(/*IEmployeeRepository employeeRepository*/
            //IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            //    _employeeRepository = employeeRepository;
            //    _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        #region Index 
        public async Task<IActionResult> Index(string SearchValue = "")
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchValue))
            {
                employees = await _unitOfWork.EmployeeRepository.GetAll();
            }
            else
            {
                employees = await _unitOfWork.EmployeeRepository.Search(SearchValue);
            }
            var mappedEmployees = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            return View(mappedEmployees);

        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                ///Manual Mapping
                ///var mappedEmployee = new Employee()
                ///{
                ///    Id= employee.Id,
                ///    Name = employee.Name,
                ///    Address= employee.Address,
                ///    Age= employee.Age,
                ///    DepartmentId= employee.DepartmentId,
                ///    Email= employee.Email,
                ///    HireDate= employee.HireDate,
                ///    IsActive= employee.IsActive,
                ///    PhoneNumber= employee.PhoneNumber,
                ///    Salary= employee.Salary
                ///};
                ///


                employeeViewModel.ImageUrl = DocumentSettings.UploadFile(employeeViewModel.Image, "Imgs");
                var mappedEmployee = _mapper.Map<Employee>(employeeViewModel);

                await _unitOfWork.EmployeeRepository.Add(mappedEmployee);
                return RedirectToAction("Index");
            }
            return View(employeeViewModel);
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return NotFound();

            var employee = await _unitOfWork.EmployeeRepository.Get(id);

            var mappedEmployee = _mapper.Map<EmployeeViewModel>(employee);

            var departmentName = await _unitOfWork.EmployeeRepository.GetDepartmentByEmployeeId(id);

            if (employee is null)
                return NotFound();

            return View(mappedEmployee);
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null)
                return NotFound();

            ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAll();
            var employee = await _unitOfWork.EmployeeRepository.Get(id);

            var mappedEmployee = _mapper.Map<EmployeeViewModel>(employee);

            if (employee is null)
                return NotFound();

            return View(mappedEmployee);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, EmployeeViewModel employeeViewModel)
        {
            if (id != employeeViewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    employeeViewModel.ImageUrl = DocumentSettings.UploadFile(employeeViewModel.Image, "Imgs");

                    var mappedEmployee = _mapper.Map<Employee>(employeeViewModel);
                    await _unitOfWork.EmployeeRepository.Update(mappedEmployee);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View(employeeViewModel);
                }
            }

            return View(employeeViewModel);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return NotFound();

            var employee = await _unitOfWork.EmployeeRepository.Get(id);
            if (employee is null)
                return NotFound();

            DocumentSettings.DeleteFile("Imgs", employee.ImageUrl);
            await _unitOfWork.EmployeeRepository.Delete(employee);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
