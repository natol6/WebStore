﻿using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
using WebStore.Data;
using WebStore.ViewModels;
using WebStore.Services;
using WebStore.Services.Interfaces;

namespace WebStore.Controllers
{
    //[Route("Staff/{action=Index}/{Id?}")]
    public class EmployeesController : Controller
    {
        private readonly ILogger<EmployeesController> _Logger;
        private readonly IEmployeesData _EmployeesData;
        public EmployeesController(IEmployeesData EmployeesData, ILogger<EmployeesController> logger)
        {
            _Logger = logger;
            _EmployeesData = EmployeesData;
        }
        public IActionResult Index()
        {
            var employees = _EmployeesData.GetAll();
             var result = new List<EmployeeViewModel>();
            foreach(var empl in employees)
            {
                result.Add(new EmployeeViewModel
                {
                    Id = empl.Id,
                    LastName = empl.LastName,
                    FirstName = empl.FirstName,
                    Patronymic = empl.Patronymic,
                    Age = empl.Age,
                    Position = empl.Position,
                    DateOfEmployment = empl.DateOfEmployment,
                });
            }
            return View(result);
        }
        
        public IActionResult Details(int id)
        {
            var employee = _EmployeesData.GetById(id);
            if (employee == null)
                return NotFound();
            ViewBag.Image = String.Format("{0}.png", employee.Id);
            var model = new EmployeeViewModel
                 {
                    Id = employee.Id,
                    LastName = employee.LastName,
                    FirstName = employee.FirstName,
                    Patronymic = employee.Patronymic,
                    Age = employee.Age,
                    Position = employee.Position,
                    DateOfEmployment = employee.DateOfEmployment,
                };
            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.Positions = TestData.Positions;
            return View("Edit", new EmployeeViewModel());
        }
        public IActionResult Edit(int? id)
        {
            ViewBag.Positions = TestData.Positions;
            if (id == null)
                return View(new EmployeeViewModel());
            
            var employee = _EmployeesData.GetById((int)id);
            if (employee == null)
            {
                _Logger.LogWarning("Попытка редактирования отсутствующего сотрудника с Id: {0}", id);
                return NotFound();
            }
                

            var model = new EmployeeViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Patronymic = employee.Patronymic,
                Age = employee.Age,
                Position = employee.Position,
                DateOfEmployment = employee.DateOfEmployment,
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);
            var employee = new Employee
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Patronymic = model.Patronymic,
                Age = model.Age,
                Position = model.Position,
                DateOfEmployment = model.DateOfEmployment,
            };
            if(model.Id == 0)
            {
                 _EmployeesData.Add(employee);
                _Logger.LogInformation("Создан новый сотрудник {0} ", employee);
            }
                
            else if (!_EmployeesData.Edit(employee))
            {
                _Logger.LogInformation("Неуспешная попытка изменения информации о сотруднике {0} ", employee);
                return NotFound();
            }
                
            _Logger.LogInformation("Изменена информация о сотруднике {0} ", employee);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id) 
        { 
            if(id < 0)
                return BadRequest();
            var employee = _EmployeesData.GetById(id);
            if (employee == null)
                return NotFound();
            var model = new EmployeeViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Patronymic = employee.Patronymic,
                Age = employee.Age,
                Position = employee.Position,
                DateOfEmployment = employee.DateOfEmployment,
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!_EmployeesData.Delete(id))
            {
                _Logger.LogInformation("Неуспешная попытка удаления сотрудника Id: {0} ", id);
                return NotFound();
            }

            _Logger.LogInformation("Удален сотрудник Id: {0} ", id);
            return RedirectToAction("Index");
        }

    }
}
